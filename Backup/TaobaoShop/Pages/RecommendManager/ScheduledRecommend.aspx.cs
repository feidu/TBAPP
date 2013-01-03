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

namespace TaobaoShop.Pages.RecommendManager
{
    public partial class ScheduledRecommend : BasePage
    {
        string pageCode = "ScheduledRecommend";
        ITopClient tbClient = null;
        public PageList PL = new PageList();
        public string PageListLink = "";

        Action.UserAction userAction = new Action.UserAction();
        Action.ScheduleRecommendAction scheduleRecommendAction = new Action.ScheduleRecommendAction();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckAcc(this);
            if (!IsPostBack)
            {
                BindMaster();
                BindRemainCount();
                BindOnsaleItem(this.txtTitleSearch.Text);
            }
        }

        private void BindRemainCount()
        {
            tbClient = new DefaultTopClient(Config.ServerURL, Config.Appkey, Config.Secret);
            ShopRemainshowcaseGetRequest req1 = new ShopRemainshowcaseGetRequest();
            ShopRemainshowcaseGetResponse resp1 = tbClient.Execute(req1, base.sessionkey);
            if (!resp1.IsError)
            {
                this.lblRemainCount.Text = resp1.Shop.RemainCount.ToString();
                if (resp1.Shop.RemainCount <= 0)
                {
                    this.lblRemainCount.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                //调用失败 ，可能是sessionkey过期
                this.lblRemainCount.Text = "";
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
            BindOnsaleItem(this.txtTitleSearch.Text);
        }

        private void BindOnsaleItem(string searchTitle)
        {
            if (Request.QueryString["PageID"] != null)
            {
                PL.PageID = Convert.ToInt16(Request.QueryString["PageID"]);
            }
            PL.PageSize = 4;
            int total = 0;

            tbClient = new DefaultTopClient(Config.ServerURL, Config.Appkey, Config.Secret);
            ItemsOnsaleGetRequest itemOnsaleReq = new ItemsOnsaleGetRequest();
            itemOnsaleReq.Fields = "num_iid,title,has_showcase";
            itemOnsaleReq.PageNo = PL.PageID;
            itemOnsaleReq.PageSize = PL.PageSize;
            if (searchTitle != "")
            {
                itemOnsaleReq.Q = searchTitle;
            }
            if (this.cboDelistFirst.Checked)
            {
                itemOnsaleReq.OrderBy = "delist_time:asc";
            }
            ItemsOnsaleGetResponse itemOnsaleResp = tbClient.Execute(itemOnsaleReq, base.sessionkey);
            if (!itemOnsaleResp.IsError)
            {
                DataList1.DataSource = itemOnsaleResp.Items;
                DataList1.DataBind();
                total = (int)itemOnsaleResp.TotalResults;
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
            Sche(user_id);
        }

        private void Sche(int user_id)
        {
            DateTime scheTime;
            try { scheTime = Convert.ToDateTime(this.txtScheduleTime.Text); }
            catch (Exception ex) { Alert(this, "日期输入有误！"); return; }
            if (scheTime <= DateTime.Now)
            {
                Alert(this, "设定的时间已经过期！"); return;
            }

            if (this.lblRemainCount.Text == "0")
            {
                Alert(this, "橱窗空位不足，到时可能无法推荐成功，可以手动取消一些宝贝橱窗推荐，但请先自动橱窗开关！"); return;
            }

            IList<tb_ScheduleRecommendQueueEntity> list = new List<tb_ScheduleRecommendQueueEntity>();
            foreach (DataListItem item in DataList1.Items)
            {
                CheckBox cbo = item.FindControl("cbolist") as CheckBox;
                if (cbo.Checked)
                {
                    long iid = Convert.ToInt64((item.FindControl("item") as System.Web.UI.HtmlControls.HtmlInputText).Value);
                    string Name = (item.FindControl("lblName") as Label).Text;
                    tb_ScheduleRecommendQueueEntity srqe = new tb_ScheduleRecommendQueueEntity();
                    srqe.created = DateTime.Now;
                    srqe.item_title = Name;
                    srqe.num_iid = iid;
                    srqe.state = false;
                    srqe.user_id = user_id;
                    srqe.Schedule = scheTime;
                    list.Add(srqe);
                }
            }
            EnQueueByScheduleRelist(list);
            Alert(this, "操作成功完成！");
        }

        private void EnQueueByScheduleRelist(IList<tb_ScheduleRecommendQueueEntity> list)
        {
            scheduleRecommendAction.EnQueueByScheduleRelist(list);
        }

        public object GetHasShowcase(object state)
        {
            if ((bool)state)
            {
                return "已推荐";
            }
            else
            {
                return "未推荐";
            }
        }

        protected void cboDelistFirst_CheckedChanged(object sender, EventArgs e)
        {
            BindOnsaleItem(this.txtTitleSearch.Text);
        }
    }
}