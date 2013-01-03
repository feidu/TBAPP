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

namespace TaobaoShop.Pages.ItemManager
{
    public partial class Item_ModifyPrice : BasePage
    {
        string pageCode = "ModifyPrice";
        public PageList PL = new PageList();
        public string PageListLink = "";
        ITopClient tbClient = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckAcc(this);
            if (!IsPostBack)
            {
                BindMaster();
                BindDdl();
                BindItems();
            }
        }

        private void BindDdl()
        {
            ListItem li1 = new ListItem();
            li1.Text = "出售中的宝贝";
            li1.Value = "onsale";
            this.ddlItemState.Items.Add(li1);

            ListItem li2 = new ListItem();
            li2.Text = "仓库中的宝贝";
            li2.Value = "inventory";
            this.ddlItemState.Items.Add(li2);

            this.ddlItemState.DataBind();

            //
            ListItem li3 = new ListItem();
            li3.Text = "加上";
            li3.Value = "add";
            this.ddlOperat.Items.Add(li3);

            ListItem li4 = new ListItem();
            li4.Text = "减去";
            li4.Value = "sub";
            this.ddlOperat.Items.Add(li4);

            ListItem li5 = new ListItem();
            li5.Text = "乘以";
            li5.Value = "mul";
            this.ddlOperat.Items.Add(li5);

            ListItem li6 = new ListItem();
            li6.Text = "除以";
            li6.Value = "div";
            this.ddlOperat.Items.Add(li6);

            this.ddlOperat.DataBind();
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
            itemInventoryReq.Fields = "num_iid,title,price";
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
            itemOnsaleReq.Fields = "num_iid,title,price";
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

        protected void BindMaster()
        {
            this.leftMenu.InnerHtml = Menu.GetMenuHtml(subliformat, subulformat, pliformat, pageCode);
            ((Label)this.TopUC1.FindControl("lblEndTime")).Text = base.endtime.ToString("yyyy-MM-dd");
            int haveday=Convert.ToInt32((base.endtime - DateTime.Now).TotalDays);
            ((Label)this.TopUC1.FindControl("lblhaveday")).Text = haveday >= 0 ? haveday.ToString() : "0";
            ((Label)this.TopUC1.FindControl("lblNick")).Text = base.nick;
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            if (base.level == ((int)Util.Enum.UserSysLevel.Experience).ToString() || base.isOverTime)
            {
                Alert(this, "体验版用户不能具备此功能！");
                return;
            }
            RepItemPrice();
            BindItems();
        }

        private double Calculation(double p1,string operat,double p2)
        {
            double res = 0;
            switch (operat)
            {
                case "add":
                    res= p1 + p2;
                    break;
                case "sub":
                    res = p1 - p2;
                    break;
                case "mul":
                    res = p1 * p2;
                    break;
                case "div":
                    res = p1 / p2;
                    break;
            }
            return res;
        }

        private void RepItemPrice()
        {
            if (txtMPrice.Text.Length == 0)
            {
                Alert(this, "请输入价格！");
                return;
            }
            try
            {
                double mPrice =  Convert.ToDouble(txtMPrice.Text);
                double newPrice = 0;
                foreach (DataListItem item in DataList1.Items)
                {
                    CheckBox cbo = item.FindControl("cbolist") as CheckBox;
                    if (cbo.Checked)
                    {
                        long iid = Convert.ToInt64((item.FindControl("item") as System.Web.UI.HtmlControls.HtmlInputText).Value);
                        double oldPrice =Convert.ToDouble( (item.FindControl("lblOldPrice") as Label).Text);
                        newPrice = Calculation(oldPrice, this.ddlOperat.SelectedValue,mPrice);
                        //改价格
                        if (oldPrice != newPrice)
                        {
                            RePrice(iid, newPrice.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Alert(this, ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindItems();
        }

        private void BindItems()
        {
            string itemState = this.ddlItemState.SelectedValue;
            switch (itemState)
            {
                case "onsale":
                    BindOnsaleItem(this.txtTitleSearch.Text);
                    break;
                case "inventory":
                    BindInventoryItem(this.txtTitleSearch.Text);
                    break;
            }
        }

        private void RePrice(long itemid, string newPrice)
        {
            tbClient = new DefaultTopClient(Config.ServerURL, Config.Appkey, Config.Secret);
            ItemPriceUpdateRequest req = new ItemPriceUpdateRequest();
            req.NumIid = itemid;
            req.Price = newPrice;
            ItemPriceUpdateResponse response = tbClient.Execute(req,base.sessionkey);
            if (response.IsError)
            {
                if (response.ErrCode == "42")
                {
                    Alert(this, "修改价格之前需要二次授权，授权后30分钟内操作可用！");
                }
                else
                {
                    Alert(this, response.ErrMsg);
                }
                //错误日志
                //itemUpdateResp.Body
            }
        }
        
    }
}