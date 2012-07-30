using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaobaoShop.Pages.Controls
{
    public partial class TopUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.hlAuth.NavigateUrl = string.Format(Config.ContainerURL, Config.Appkey) + "&scope=item";
            }
        }

        protected void linkbtnExit_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("../../Login.aspx");
            //这里请不要取消授权，会造成用户的自动开关全部无效。
        }
    }
}