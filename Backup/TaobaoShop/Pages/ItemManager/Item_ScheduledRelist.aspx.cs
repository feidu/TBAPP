using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Top.Api.Request;
using Top.Api.Response;
using Top.Api;
using Util;
using Entity;

namespace TaobaoShop.Pages.ItemManager
{
    public partial class Item_ScheduledRelist : BasePage
    {
        string pageCode = "ScheduledRelist";
        ITopClient tbClient = null;
        public PageList PL = new PageList();
        public string PageListLink = "";

        Action.UserAction userAction = new Action.UserAction();
        Action.ScheduleRelistAction scheduleRelistAction = new Action.ScheduleRelistAction();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckAcc(this);
            if (!IsPostBack)
            {
                BindMaster();
                BindInventoryItem(this.txtTitleSearch.Text);
            }
        }

        protected void BindMaster()
        {
            this.leftMenu.InnerHtml = Menu.GetMenuHtml(subliformat, subulformat, pliformat, pageCode);
            ((Label)this.TopUC1.FindControl("lblEndTime")).Text = base.endtime.ToString("yyyy-MM-dd");
            int haveday = Convert.ToInt32((base.endtime - DateTime.Now).TotalDays);
            ((Label)this.TopUC1.FindControl("lblhaveday")).Text = haveday >= 0 ? haveday.ToString() : "0";
            ((Label)this.TopUC1.FindControl("lblNick")).Text = base.nick;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindInventoryItem(this.txtTitleSearch.Text);
        }

        private void BindInventoryItem(string searchTitle)
        {
            if (Request.QueryString["PageID"] != null)
            {
                PL.PageID = Convert.ToInt16(Request.QueryString["PageID"]);
            }
            PL.PageSize = 4;
            int total = 0;

            tbClient = new DefaultTopClient(Config.ServerURL, Config.Appkey, Config.Secret);
            ItemsInventoryGetRequest itemInventoryReq = new ItemsInventoryGetRequest();
            itemInventoryReq.Fields = "num_iid,title,pic_url";
            itemInventoryReq.PageNo = PL.PageID;
            itemInventoryReq.PageSize = PL.PageSize;
            if (searchTitle != "")
            {
                itemInventoryReq.Q = searchTitle;
            }
            ItemsInventoryGetResponse itemInventoryResp = tbClient.Execute(itemInventoryReq, base.sessionkey);
            if (!itemInventoryResp.IsError)
            {
                DataList1.DataSource = itemInventoryResp.Items;
                DataList1.DataBind();

                total = (int)itemInventoryResp.TotalResults;
                PL.RecordCount = total;
                PageListLink = new PageListBll().GetPageList(PL);
            }
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            if (base.level == ((int)Util.Enum.UserSysLevel.Experience).ToString() || base.isOverTime)
            {
                Alert(this, "体验版用户不能具备此功能！");
                return;
            }
            int user_id = userAction.GetUserIdByNick(base.nick);
            if (user_id == 0)
            {
                Alert(this, "用户信息获取失败，请重新登录！");
                return;
            }

            if (rdoAuto.Checked)
            {
                Auto(user_id);
            }
            else
            {
                Sche(user_id);
            }
        }

        private void Sche(int user_id)
        {
            DateTime goldTime;
            try { goldTime = Convert.ToDateTime(this.txtGoldTime.Text); }
            catch (Exception ex) { Alert(this, "日期输入有误！"); return; }
            if (goldTime <= DateTime.Now)
            {
                Alert(this, "设定的时间已经过期！"); return;
            }

            int hhbegin=0;
            int hhend=0;
            if (this.rdo1.Checked) {
                hhbegin = 10;
                hhend = 11;
            }
            if (this.rdo2.Checked) {
                hhbegin = 13;
                hhend = 16;
            }
            if (this.rdo3.Checked) {
                hhbegin = 20;
                hhend = 22;
            }
            int total = 0;
            int interval = 0;
            foreach (DataListItem item in DataList1.Items)
            {
                CheckBox cbo = item.FindControl("cbolist") as CheckBox;
                if (cbo.Checked)
                {
                    total++;
                }
            }
            interval = (hhend - hhbegin) * 60 / total; //间隔 分钟
            int fen=0;
            IList<tb_ScheduleRelistQueueEntity> list = new List<tb_ScheduleRelistQueueEntity>();
            foreach (DataListItem item in DataList1.Items)
            {
                CheckBox cbo = item.FindControl("cbolist") as CheckBox;
                if (cbo.Checked)
                {
                    long iid = Convert.ToInt64((item.FindControl("item") as System.Web.UI.HtmlControls.HtmlInputText).Value);
                    string Name = (item.FindControl("lblName") as Label).Text;
                    tb_ScheduleRelistQueueEntity srqe = new tb_ScheduleRelistQueueEntity();
                    srqe.created = DateTime.Now;
                    srqe.item_title = Name;
                    srqe.num_iid = iid;
                    srqe.state = false;
                    srqe.user_id = user_id;
                    srqe.Schedule =goldTime.AddHours(hhbegin).AddMinutes(fen);
                    list.Add(srqe);

                    fen = fen + interval;
                    while (fen >= 60) {
                        fen = fen-60;
                        hhbegin++;
                    }
                }
            }
            EnQueueByScheduleRelist(list);
        }

        private void Auto(int user_id)
        {
            DateTime scheduleTime;
            try { scheduleTime = Convert.ToDateTime(this.txtScheduleTime.Text); }
            catch (Exception ex) { Alert(this, "日期输入有误！"); return; }
            if (scheduleTime <= DateTime.Now)
            {
                Alert(this, "设定的时间已经过期！"); return;
            }
            IList<tb_ScheduleRelistQueueEntity> list = new List<tb_ScheduleRelistQueueEntity>();
            foreach (DataListItem item in DataList1.Items)
            {
                CheckBox cbo = item.FindControl("cbolist") as CheckBox;
                if (cbo.Checked)
                {
                    long iid = Convert.ToInt64((item.FindControl("item") as System.Web.UI.HtmlControls.HtmlInputText).Value);
                    string Name = (item.FindControl("lblName") as Label).Text;
                    tb_ScheduleRelistQueueEntity srqe = new tb_ScheduleRelistQueueEntity();
                    srqe.created = DateTime.Now;
                    srqe.item_title = Name;
                    srqe.num_iid = iid;
                    srqe.Schedule = scheduleTime;
                    srqe.state = false;
                    srqe.user_id = user_id;
                    list.Add(srqe);
                }
            }
            EnQueueByScheduleRelist(list);
            Alert(this, "操作成功完成！");
        }

        private void EnQueueByScheduleRelist(IList<tb_ScheduleRelistQueueEntity> list)
        {
            scheduleRelistAction.EnQueueByScheduleRelist(list);
        }

        public object GetText(object s)
        {
            string str = (string)s;
            if (str.Length >= 10)
            {
                str = str.Substring(0, 10) + "...";
            }
            return str;
        }
    }
}