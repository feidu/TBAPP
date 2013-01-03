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
    public partial class Item_ModifyName : BasePage
    {
        string pageCode = "ModifyName";
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
            int haveday = Convert.ToInt32((base.endtime - DateTime.Now).TotalDays);
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
            if (rdoReplace.Checked)
            {
                RepItemName();
                BindItems();
                return;
            }
            if (rdoAdd.Checked)
            {
                AddItemName();
                BindItems();
                return;
            }
            if (rdoAll.Checked)
            {
                RepAllItemName();
                BindItems();
                return;
            }
        }

        private void RepAllItemName()
        {
            if (this.txtReplaceAll.Text == "")
            {
                Alert(this,"请填写修改名称");
                return;
            }
            string newName = this.txtReplaceAll.Text;
            foreach (DataListItem item in DataList1.Items)
            {
                CheckBox cbo = item.FindControl("cbolist") as CheckBox;
                if (cbo.Checked)
                {
                    long iid = Convert.ToInt64((item.FindControl("item") as System.Web.UI.HtmlControls.HtmlInputText).Value);
                    string oldName = (item.FindControl("lblName") as Label).Text;
                    if (newName.Length > 30)
                    {
                        Alert(this, "商品：[" + newName + "]新名字超过30个长度，请重新提交！");
                        return;
                    }
                    //改名
                    if (!oldName.Equals(newName))
                    {
                        Rename(iid,newName);
                    }
                }
            }
        }

        private void AddItemName()
        {
            if (this.txtFirstAdd.Text == "" && this.txtEndAdd.Text=="")
            {
                Alert(this, "请至少填写一个追加的名称！");
                return;
            }
            string firstAdd = this.txtFirstAdd.Text;
            string footerAdd = this.txtEndAdd.Text;
            foreach (DataListItem item in DataList1.Items)
            {
                CheckBox cbo = item.FindControl("cbolist") as CheckBox;
                if (cbo.Checked)
                {
                    long iid = Convert.ToInt64((item.FindControl("item") as System.Web.UI.HtmlControls.HtmlInputText).Value);
                    string oldName = (item.FindControl("lblName") as Label).Text;
                    string newName = firstAdd + oldName + footerAdd;
                    if (newName.Length > 30)
                    {
                        Alert(this, "商品：[" + newName + "]新名字超过30个长度，请重新提交！");
                        return;
                    }
                    //改名
                    if (!oldName.Equals(newName))
                    {
                        Rename(iid, newName);
                    }
                }
            }
        }

        private void RepItemName()
        {
            if (this.txtReplace.Text == "")
            {
                Alert(this, "请填写被替换的名称！");
                return;
            }
            string repName = this.txtReplace.Text;
            string repNew = this.txtReplaceNew.Text;
            foreach (DataListItem item in DataList1.Items)
            {
                CheckBox cbo = item.FindControl("cbolist") as CheckBox;
                if (cbo.Checked)
                {
                    long iid = Convert.ToInt64((item.FindControl("item") as System.Web.UI.HtmlControls.HtmlInputText).Value);
                    string oldName = (item.FindControl("lblName") as Label).Text;
                    string newName = oldName.Replace(repName, repNew);
                    if (newName.Length > 30)
                    {
                        Alert(this,"商品：["+newName+"]新名字超过30个长度，请重新提交！");
                        return;
                    }
                    //改名
                    if (!oldName.Equals(newName))
                    {
                        try
                        {
                            Rename(iid, newName);
                        }catch(Exception ex)
                        {
                            Alert(this,ex.Message);
                        }
                    }
                }
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

        private void Rename(long itemid,string newName)
        {
            tbClient = new DefaultTopClient(Config.ServerURL, Config.Appkey, Config.Secret);
            ItemUpdateRequest itemUpdateReq = new ItemUpdateRequest();
            itemUpdateReq.NumIid = itemid;
            itemUpdateReq.Title = newName;
            ItemUpdateResponse itemUpdateResp = tbClient.Execute(itemUpdateReq,base.sessionkey);
            if (itemUpdateResp.IsError)
            {
                Alert(this, itemUpdateResp.ErrMsg);
                //错误日志
                //itemUpdateResp.Body
            }
        }
    }
}