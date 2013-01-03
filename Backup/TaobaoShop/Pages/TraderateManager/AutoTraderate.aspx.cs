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

namespace TaobaoShop.Pages.TraderateManager
{
    public partial class AutoTraderate : BasePage
    {
        string pageCode = "AutoTraderate";
        //ITopClient tbClient = null;
        Action.SwitchAction switchAction = new Action.SwitchAction();
        Action.AutoTraderateAction autoTraderateAction = new Action.AutoTraderateAction();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckAcc(this);
            if (!IsPostBack)
            {
                BindMaster();
                BindSwitchState();
                BindDatalist();
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

        private void BindDatalist()
        {
            DataList1.DataSource = autoTraderateAction.GetContext(base.nick);
            DataList1.DataKeyField = "id";
            DataList1.DataBind();
        }

        private void BindSwitchState()
        {
            //自动开关
            bool state = switchAction.GetSwitchState(base.nick, Util.Enum.AutoSwitchType.AutoTraderate.ToString());
            ViewState["state"] = state;
            BindBtnImg(state);
            //开关属性
            bool prostate = switchAction.GetSwitchPropertyState(base.nick,Util.Enum.AutoTraderateType.AutoTraderateBuyerPay.ToString());
            this.rdoAutoTraderateBuyerPay.Checked = prostate;
            this.rdoAutoTraderateBuyerRated.Checked = !prostate;
        }

        private void BindBtnImg(bool state)
        {
            if (state)
            {
                imgbtnSwitch.ImageUrl = "~/Image/Auto/on.png";
            }
            else
            {
                imgbtnSwitch.ImageUrl = "~/Image/Auto/off.png";
            }
        }

        protected void imgbtnSwitch_Click(object sender, ImageClickEventArgs e)
        {
            //if (base.level == ((int)Util.Enum.UserSysLevel.Experience).ToString())
            //{
            //    Alert(this, "体验版用户不能具备此功能！");
            //    return;
            //}

            bool oldstate = (bool)ViewState["state"];
            bool newstate = ChangSwitchState(oldstate);
            ViewState["state"] = newstate;
            BindBtnImg(newstate);
        }

        private bool ChangSwitchState(bool oldstate)
        {
            switchAction.ChangSwitchState(base.nick, Util.Enum.AutoSwitchType.AutoTraderate.ToString(), !oldstate);
            return !oldstate;
        }

        protected void linkbtnSetType_Click(object sender, EventArgs e)
        {
            //if (base.level == ((int)Util.Enum.UserSysLevel.Experience).ToString() || base.isOverTime)
            //{
            //    Alert(this, "体验版用户不能具备此功能！");
            //    return;
            //}
            //设置开关属性
            string stateBuyerPay = string.Empty; 
            string stateBuyerRated = string.Empty;
            if (rdoAutoTraderateBuyerPay.Checked)
            {
                stateBuyerPay = "1"; stateBuyerRated = "0";
            }
            else
            {
                stateBuyerPay = "0"; stateBuyerRated = "1";
            }
            switchAction.SetSwitchProperty(base.nick, Util.Enum.AutoTraderateType.AutoTraderateBuyerPay.ToString(), stateBuyerPay);
            switchAction.SetSwitchProperty(base.nick, Util.Enum.AutoTraderateType.AutoTraderateBuyerRated.ToString(), stateBuyerRated);
            //采用好评预设内容
            ChangeContextCheck();
        }

        protected void DataList1_EditCommand(object source, DataListCommandEventArgs e)
        {
            this.DataList1.EditItemIndex = e.Item.ItemIndex;
            BindDatalist();
        }

        protected void DataList1_CancelCommand(object source, DataListCommandEventArgs e)
        {
            this.DataList1.EditItemIndex = -1;
            BindDatalist();
        }

        protected void DataList1_UpdateCommand(object source, DataListCommandEventArgs e)
        {
            //if (base.level == ((int)Util.Enum.UserSysLevel.Experience).ToString() || base.isOverTime)
            //{
            //    Alert(this, "体验版用户不能具备此功能！");
            //    return;
            //}
            int id = 0;
            try
            {
                id = Convert.ToInt32(DataList1.DataKeys[e.Item.ItemIndex]);
            }
            catch (Exception ex)
            {
                Alert(this, "ID格式错误"+ex.Message);
                return;
            }
            string context = ((TextBox)e.Item.FindControl("txtContext")).Text.Trim();
            if (context.Length == 0)
            {
                Alert(this, "请填写好评再保存");
                return;
            }
            autoTraderateAction.UpdateContext(id,context);
            DataList1.EditItemIndex = -1;
            BindDatalist();
        }

        protected void DataList1_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            //if (base.level == ((int)Util.Enum.UserSysLevel.Experience).ToString() || base.isOverTime)
            //{
            //    Alert(this, "体验版用户不能具备此功能！");
            //    return;
            //}
            int id = 0;
            try
            {
                id = Convert.ToInt32(DataList1.DataKeys[e.Item.ItemIndex]);
            }
            catch (Exception ex)
            {
                Alert(this, "ID格式错误" + ex.Message);
                return;
            }
            autoTraderateAction.DeleteContext(id);
            DataList1.EditItemIndex = -1;
            BindDatalist();
        }

        private void ChangeContextCheck()
        {
            foreach (DataListItem item in DataList1.Items)
            {
                if (((RadioButton)item.FindControl("rdoContext")).Checked)
                {
                    int id = 0;
                    try
                    {
                        id = Convert.ToInt32(DataList1.DataKeys[item.ItemIndex]);
                    }
                    catch (Exception ex)
                    {
                        Alert(this, "ID格式错误" + ex.Message);
                        return;
                    }
                    autoTraderateAction.SetUseContext(id,base.nick);
                    return;
                }
            }
        }

        protected void linkbtnAddContext_Click(object sender, EventArgs e)
        {
            //if (base.level == ((int)Util.Enum.UserSysLevel.Experience).ToString() || base.isOverTime)
            //{
            //    Alert(this, "体验版用户不能具备此功能！");
            //    return;
            //}

            string context = this.areaContext.Text.Trim();
            int total = autoTraderateAction.GetContextTotalByUser(base.nick);
            if (total >= 10)
            {
                Alert(this, "已经到达您预设评语个数上限，不能再添加好评了！");
                return;
            }

            if (context.Length > 250)
            {
                Alert(this,"字数请在250个以内！");
                return;
            }

            if (context.Length ==0)
            {
                Alert(this, "请填写好评再提交！");
                return;
            }

            autoTraderateAction.AddContext(context,base.nick);
            BindDatalist();
        }

        
    }
}