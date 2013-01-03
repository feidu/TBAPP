using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PersistenceLayer;
using Entity;

namespace TaobaoShop
{
    public partial class BasePage : System.Web.UI.Page
    {
        public string nick;
        public string level;
        public bool isOverTime=false;
        public string sessionkey;
        public DateTime endtime;
        protected string subliformat = "<li {0}><a href=\"{1}\">{2}</a></li>";
        protected string pliformat = "<li style=\"background-image:url(../../Image/Left/di3.png);font-family:'宋体';font-size:14px; color: #333; font-weight: bold;\" >{0}</li>";
        //params:=0html,1=node.name
        protected string subulformat = "<ul>{0}</ul>";

        public BasePage()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            if (HttpContext.Current.Application["DatabaseSetting"] == null || HttpContext.Current.Application["DatabaseSetting"].ToString() != "Y")
            {
                string m_ApplicationPath = HttpContext.Current.Request.ApplicationPath;
                if (m_ApplicationPath == "")
                    m_ApplicationPath = "/";
                if (!m_ApplicationPath.EndsWith("/"))
                    m_ApplicationPath += "/";
                string DatabaseXml = m_ApplicationPath + "Config/DatabaseMap.xml";
                PersistenceLayer.Setting.Instance().DatabaseMapFile = Server.MapPath(DatabaseXml);
                HttpContext.Current.Application["DatabaseSetting"] = "Y";
            }
        }


        public void CheckAcc(System.Web.UI.Page page)
        {
            if (HttpContext.Current.Session["nick"] != null && HttpContext.Current.Session["sessionkey"]!=null)
            {
                if (HttpContext.Current.Session["nick"].ToString() != "" && HttpContext.Current.Session["sessionkey"].ToString() != "")
                {
                    DateTime authGetTime = Convert.ToDateTime(HttpContext.Current.Session["time"]);
                    if (authGetTime < DateTime.Now.AddMinutes(-29))
                    {
                        TimeOut(page);
                        //Alert("授权超过30分钟过期，请重新授权！", "../../Login.aspx");//短授权过期
                    }
                    nick = HttpContext.Current.Session["nick"].ToString();
                    sessionkey = HttpContext.Current.Session["sessionkey"].ToString();
                    Action.LoginAction loginAction = new Action.LoginAction();
                    tb_UserEntity user = loginAction.GetUserByNick(nick);
                    try
                    {
                        level = user.syslevel;//根据nick获取level 和到期时间
                        endtime = user.authEndTime;
                        if (endtime < DateTime.Now)
                        {
                            isOverTime = true;
                            //Alert("会员已过期，请续费！", "../../Login.aspx");//会员过期
                        }
                    }
                    catch
                    {
                        Alert("用户不存在，登录出错啦！", "../../Login.aspx");
                    }
                }
                else
                {
                    PRedirect("../../Login.aspx");
                }
            }
            else
            {
                PRedirect("../../Login.aspx");
            }
        }

        public void Alert(string ErrMessage)
        {
            Response.Write("<script language='javascript' defer>");
            Response.Write("alert('" + ErrMessage + "');");
            Response.Write("</script>");
        }

        public void Alert(System.Web.UI.Page page,string ErrMessage)
        {
            page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), 
                "message",
                "<script language='javascript' defer>alert('" + ErrMessage + "');</script>");
        }

        public void Alert(string ErrMessage, string url)
        {
            Response.Write("<script language='javascript' defer>");
            Response.Write("alert('" + ErrMessage + "');");
            Response.Write("location='" + url + "';");
            Response.Write("</script>");
        }

        public void TimeOut(System.Web.UI.Page page)
        {
            string script = "<script language='javascript' defer>$(document).ready(function () {$(\"#reAuth\").trigger('click');});</script>";
            page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page),
                "message",
                script);
        }

        public void Alert(System.Web.UI.Page page, string ErrMessage, string url)
        {
            page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), 
                "message",
                "<script language='javascript' defer>alert('" + ErrMessage + "');location='" + url + "';</script>");
        }

        public void Redirect(string url)
        {
            Response.Write("<script language='javascript'>");
            Response.Write("location='" + url + "';");
            Response.Write("</script>");
        }

        public void PRedirect(string url)
        {
            Response.Write("<script language='javascript'>");
            Response.Write("parent.location='" + url + "';");
            Response.Write("</script>");
        }

        public void CloseAndRefresh(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "message", "<script language='javascript' defer>alert(\"" + msg.ToString() + "\");parent.$('li[id$=\"btn_refresh\"] img:first-child')[0].click();parent.wBox.close();</script>");//parent.$('input[id$=\"btn_refresh\"]').click();
        }

        //public PagedDataSource GetPage(DataTable dt, Wuqi.Webdiyer.AspNetPager Pager)
        //{
        //    Pager.RecordCount = dt.Rows.Count;
        //    PagedDataSource pds = new PagedDataSource();
        //    pds.DataSource = dt.DefaultView;
        //    pds.AllowPaging = true;
        //    pds.CurrentPageIndex = Pager.CurrentPageIndex - 1;
        //    pds.PageSize = Pager.PageSize;
        //    return pds;
        //}

        public void ShowControl(Control c, string controlstring)
        {
            string[] strs = controlstring.Split(',');
            foreach (string str in strs)
            {
                if (c.FindControl(ConvertStr(str)) != null)
                {
                    c.FindControl(ConvertStr(str)).Visible = true;
                }
            }
        }

        private string ConvertStr(string str)
        {
            switch (str)
            {
                case "add":
                    return "btn_add";
                case "look":
                    return "btn_look";
                case "edit":
                    return "btn_edit";
                case "del":
                    return "btn_delete";
                case "config":
                    return "btn_config";
                default:
                    return "";
            }
        }

        //public void CheckAuth(string code, string ControlsStr, Control c)
        //{
        //    DAL.Action.Auth auth = new DAL.Action.Auth();
        //    string operate = auth.GetOperate(code, userID, ControlsStr);
        //    if (operate.IndexOf("view") >= 0)
        //    {
        //        if (c != null)
        //        {
        //            ShowControl(c, operate);
        //        }
        //    }
        //    else
        //    {
        //        Alert("没有权限进入", "../../Pages/frame/index.htm");
        //    }
        //}

        public ListItem CreateListItem(string strText, string strValue, string title)
        {
            ListItem li = new ListItem();
            li.Text = strText;
            li.Value = strValue;
            li.Attributes["title"] = title;
            return li;
        }

        public int filterSql(string sSql)
        {
            int srcLen, decLen = 0;
            sSql = sSql.ToLower().Trim();
            srcLen = sSql.Length;
            sSql = sSql.Replace("exec", "");
            sSql = sSql.Replace("delete", "");
            sSql = sSql.Replace("master", "");
            sSql = sSql.Replace("truncate", "");
            sSql = sSql.Replace("declare", "");
            sSql = sSql.Replace("create", "");
            sSql = sSql.Replace("xp_", "no");
            decLen = sSql.Length;
            if (srcLen == decLen) return 0; else return 1;
        }
    }
}