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

namespace TaobaoShop.Pages.RecommendManager
{
    public partial class AutoRecommend : BasePage
    {
        string pageCode = "AutoRecommend";
        //ITopClient tbClient = null;
        Action.SwitchAction switchAction = new Action.SwitchAction();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckAcc(this);
            if (!IsPostBack)
            {
                BindMaster();
                BindSwitchState();
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

        private void BindSwitchState()
        {
            bool state = switchAction.GetSwitchState(base.nick, Util.Enum.AutoSwitchType.AutoRecommend.ToString());
            ViewState["state"] = state;
            BindBtnImg(state);

            //开关属性
            bool prostate = switchAction.GetSwitchPropertyState(base.nick, Util.Enum.AutoRecommendType.DelistFirst.ToString());
            this.cboDelistFirst.Checked = prostate;
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
            if (base.level == ((int)Util.Enum.UserSysLevel.Experience).ToString() || base.isOverTime)
            {
                Alert(this, "体验版用户不能具备此功能！");
                return;
            }

            bool oldstate = (bool)ViewState["state"];
            bool newstate = ChangSwitchState(oldstate);
            ViewState["state"] = newstate;
            BindBtnImg(newstate);
        }

        private bool ChangSwitchState(bool oldstate)
        {
            switchAction.ChangSwitchState(base.nick, Util.Enum.AutoSwitchType.AutoRecommend.ToString(), !oldstate);
            return !oldstate;
        }

        protected void cboDelistFirst_CheckedChanged(object sender, EventArgs e)
        {
            string stateDelistFirst = string.Empty;
            if (this.cboDelistFirst.Checked)
            {
                stateDelistFirst = "1";
            }
            else
            {
                stateDelistFirst = "0";
            }
            switchAction.SetSwitchProperty(base.nick, Util.Enum.AutoRecommendType.DelistFirst.ToString(), stateDelistFirst);
        }
    }
}