using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Top.Api.Request;
using Top.Api.Response;
using Top.Api;
using Top.Api.Util;
using Entity;
using Action;

namespace TaobaoShop
{
    //此页面用为授权回调页面，获取并更新授权用户信息
    public partial class Login : BasePage
    {
        ITopClient tbClient = null;
        LoginAction loginAction = new LoginAction();
        //调用检查用户使用期限API的参数
        private string article_code = System.Configuration.ConfigurationManager.AppSettings["article_code"];
        private string item_code = System.Configuration.ConfigurationManager.AppSettings["item_code"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.hlAuth.NavigateUrl = string.Format(Config.ContainerURL, Config.Appkey) + "&scope=item";

                if (Request.QueryString["top_appkey"] != null)
                {
                    #region 登录操作
                    //验证回调地址参数
                    if (TopUtils.VerifyTopResponse(Request.QueryString["top_parameters"], Request.QueryString["top_session"], Request.QueryString["top_sign"], Config.Appkey, Config.Secret))
                    {
                        //获取用户昵称
                        string nick = string.Empty;
                        TopUtils.DecodeTopParams(Request.QueryString["top_parameters"].ToString()).TryGetValue("visitor_nick", out nick);
                        if (nick == "")
                        {
                            return;//授权用户有误
                        }
                        //获取并保存用户信息（包括更新会员到期时间）
                        AddUserOrUpdateUser(nick);
                        //为用户开通主动增值服务
                        PermitIncrement(nick);

                        Session["nick"] = nick;
                        Session["sessionkey"] = Request.QueryString["top_session"];
                        Session["time"] = DateTime.Now;
                        Response.Redirect("Pages/ItemManager/Item_AutoRelist.aspx");
                    }
                    #endregion
                }
            }
        }

        private string GetAuthEndTime(string nick)
        {
            string authEndTime = string.Empty;
            //订购关系查询
            VasSubscribeGetRequest vasSubscribeReq = new VasSubscribeGetRequest();
            vasSubscribeReq.Nick = nick;
            vasSubscribeReq.ArticleCode = article_code;
            VasSubscribeGetResponse vasSubscribeResp = tbClient.Execute(vasSubscribeReq);
            if (vasSubscribeResp.IsError)
            {
                //有可能article_code有误。
            }
            else
            {
                foreach (Top.Api.Domain.ArticleUserSubscribe s in vasSubscribeResp.ArticleUserSubscribes)
                {
                    if (s.ItemCode == item_code)
                    {
                        authEndTime = s.Deadline;
                    }
                }
            }
            return authEndTime;
        }

        private void AddUserOrUpdateUser(string nick)
        {
            tbClient = new DefaultTopClient(Config.ServerURL, Config.Appkey, Config.Secret);
            UserGetRequest userReq = new UserGetRequest();
            userReq.Fields = "user_id,uid,nick,sex,buyer_credit,seller_credit,location,created,last_visit,birthday,type,status,alipay_no,alipay_account,alipay_account,email,consumer_protection,alipay_bind";
            userReq.Nick = nick;
            UserGetResponse userResp = tbClient.Execute(userReq);
            if (userResp.IsError)
            {
                return;//userResp.ErrorMsg 读取用户信息失败，错误信息写入日志
            }
            //用户信息保存或修改到数据库，并获取level
            tb_UserEntity userE = new tb_UserEntity();
            userE.email = userResp.User.Email == null ? "" : userResp.User.Email;
            userE.nick = userResp.User.Nick;
            userE.type = userResp.User.Type;
            string authEndTime = GetAuthEndTime(nick);//到期会员时间获取
            try
            {
                userE.authEndTime = authEndTime == "" ? DateTime.Now.AddDays(-1) : Convert.ToDateTime(authEndTime);
                if (userE.authEndTime < DateTime.Now)
                {
                    userE.syslevel = ((int)Util.Enum.UserSysLevel.Experience).ToString();
                }
                else
                {
                    userE.syslevel = ((int)Util.Enum.UserSysLevel.Member).ToString();
                }
            }
            catch (Exception ex)
            {
                //日期格式转换错误
                return;
            }
            userE.SessionKey = Request.QueryString["top_session"];
            loginAction.AddUserOrUpdateUser(userE);
        }

        private void PermitIncrement(string nick)
        {
            //查询应用为用户开通的增量消息服务 
            IncrementCustomersGetRequest incrementgetReq = new IncrementCustomersGetRequest();
            incrementgetReq.Nicks = nick;
            incrementgetReq.Type = "get,notify";
            IncrementCustomersGetResponse incrementgetResp = tbClient.Execute(incrementgetReq);
            if (incrementgetResp.IsError)
            {
                //查询增值服务失败，错误信息写入日志
            }
            else
            {
                //app_customers[0].type 验证是否开通get,notify，未开通get,notify则开通服务
                int flag = 0;
                if (incrementgetResp.AppCustomers.Count > 0)
                {
                    List<string> types = incrementgetResp.AppCustomers[0].Type;
                    foreach (string type in types)
                    {
                        if (type == "notify" || type == "get")
                        {
                            flag++;
                        }
                    }
                }
                if (2 != flag)
                {
                    IncrementCustomerPermitRequest permitReq = new IncrementCustomerPermitRequest();
                    permitReq.Type = "get,notify,syn";
                    permitReq.Topics = "trade;refund;item";
                    permitReq.Status = "all;all;all";
                    IncrementCustomerPermitResponse permitResp = tbClient.Execute(permitReq, Request.QueryString["top_session"]);
                }
            }
        }
    }
}