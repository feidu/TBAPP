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
    public partial class RecommendManager : BasePage
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
            itemOnsaleReq.Fields = "num_iid,title,has_showcase,delist_time";
            itemOnsaleReq.PageNo = PL.PageID;
            itemOnsaleReq.PageSize = PL.PageSize;
            if (searchTitle != "")
            {
                itemOnsaleReq.Q = searchTitle;
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

        public object GetDelistTime(object time)
        {
            return time + " 下架";
        }

        protected void btnDopromoted_Click(object sender, EventArgs e)
        {
            if (base.level == ((int)Util.Enum.UserSysLevel.Experience).ToString() || base.isOverTime)
            {
                Alert(this, "体验版用户不能具备此功能！");
                return;
            }
            Dopromoted();
        }

        private void Dopromoted()
        {
            int RemainCount = Convert.ToInt32(this.lblRemainCount.Text);
            foreach (DataListItem item in DataList1.Items)
            {
                if (RemainCount <= 0)
                {
                    return;
                }
                CheckBox cbo = item.FindControl("cbolist") as CheckBox;
                if (cbo.Checked)
                {
                    long iid = Convert.ToInt64((item.FindControl("item") as System.Web.UI.HtmlControls.HtmlInputText).Value);
                    tbClient = new DefaultTopClient(Config.ServerURL, Config.Appkey, Config.Secret);
                    ItemRecommendAddRequest req3 = new ItemRecommendAddRequest();
                    req3.NumIid = iid;
                    ItemRecommendAddResponse resp3 = tbClient.Execute(req3, base.sessionkey);
                    tb_RecommendResultEntity rre = new tb_RecommendResultEntity();
                    rre.nick = base.nick;
                    rre.operatTime = DateTime.Now;
                    rre.Result = resp3.Body;
                    rre.type = "M";
                    if (resp3.IsError)
                    {
                        //上橱窗失败，可能是sessionkey过期
                        rre.isSuccess = false;
                        scheduleRecommendAction.ResultWrite(rre);
                    }
                    else
                    {
                        rre.isSuccess = true;
                        scheduleRecommendAction.ResultWrite(rre);
                        RemainCount--;
                    }
                }
            }
            BindRemainCount();
            BindOnsaleItem(this.txtTitleSearch.Text);
        }


        protected void btnDoupromoted_Click(object sender, EventArgs e)
        {
            if (base.level == ((int)Util.Enum.UserSysLevel.Experience).ToString() || base.isOverTime)
            {
                Alert(this, "体验版用户不能具备此功能！");
                return;
            }
            Doupromoted();
        }

        private void Doupromoted()
        {
            foreach (DataListItem item in DataList1.Items)
            {
                CheckBox cbo = item.FindControl("cbolist") as CheckBox;
                if (cbo.Checked)
                {
                    long iid = Convert.ToInt64((item.FindControl("item") as System.Web.UI.HtmlControls.HtmlInputText).Value);
                    tbClient = new DefaultTopClient(Config.ServerURL, Config.Appkey, Config.Secret);
                    ItemRecommendDeleteRequest req3 = new ItemRecommendDeleteRequest();
                    req3.NumIid = iid;
                    ItemRecommendDeleteResponse resp3 = tbClient.Execute(req3, base.sessionkey);
                    tb_RecommendResultEntity rre = new tb_RecommendResultEntity();
                    rre.nick = base.nick;
                    rre.operatTime = DateTime.Now;
                    rre.Result = resp3.Body;
                    rre.type = "M";
                    if (resp3.IsError)
                    {
                        //取消橱窗失败，可能是sessionkey过期
                        rre.isSuccess = false;
                        scheduleRecommendAction.ResultWrite(rre);
                    }
                    else
                    {
                        rre.isSuccess = true;
                        scheduleRecommendAction.ResultWrite(rre);
                    }
                }
            }
            BindRemainCount();
            BindOnsaleItem(this.txtTitleSearch.Text);
        }
    }
}