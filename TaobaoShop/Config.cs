using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaobaoShop
{
    public static class Config
    {
        private static string _domain = string.Empty;

        /// <summary>
        /// 客户端Cookies 域
        /// </summary>
        public static string Domain
        {
            set { _domain = value; }
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Domain"] != null)
                {
                    _domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
                    return _domain;
                }
                else
                {
                    return _domain;
                }
            }
        }

        private static string _appkey = "test";//系统分配 test
        /// <summary>
        /// *系统定义 App Key
        /// </summary>
        public static string Appkey
        {
            set { _appkey = value; }
            get
            {
                if (_appkey != "test")
                {
                    return _appkey;
                }

                if (System.Configuration.ConfigurationManager.AppSettings["AppKey"] != null)
                {
                    _appkey = System.Configuration.ConfigurationManager.AppSettings["AppKey"];
                    return _appkey;
                }
                else
                {
                    return _appkey;
                }
            }
        }

        private static string _secret = "test";//系统分配 test
        /// <summary>
        /// * 系统定义  App Secret
        /// </summary>
        public static string Secret
        {
            set { _secret = value; }
            get
            {
                if (_secret != "test")
                {
                    return _secret;
                }

                if (System.Configuration.ConfigurationManager.AppSettings["AppSecret"] != null)
                {
                    _secret = System.Configuration.ConfigurationManager.AppSettings["AppSecret"];
                    return _secret;
                }
                else
                {
                    return _secret;
                }
            }
        }

        private static bool _SandBox = false;
        /// <summary>
        /// 是否沙箱环境 (true=沙箱,false=正式环境,默认是正式环境)
        /// </summary>
        public static bool SendBox
        {
            set { _SandBox = value; }
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["SandBox"] != null)
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["SandBox"].ToString() == "1")
                    {
                        _SandBox = true;
                    }
                    else
                    {
                        _SandBox = false;
                    }
                }
                return _SandBox;
            }
        }

        private static string _serverURL = SendBox ? "http://gw.api.tbsandbox.com/router/rest" : "http://gw.api.taobao.com/router/rest"; //http://gw.api.tbsandbox.com/router/rest
        /// <summary>
        /// * 系统定义 调用入口 默认测试环境
        /// <para>正式环境：http://gw.api.taobao.com/router/rest</para>
        /// <para>测试环境：http://gw.api.tbsandbox.com/router/rest</para>
        /// <para>旧测试环境：http://gw.sandbox.taobao.com/router/rest</para>
        /// </summary>
        public static string ServerURL { set { _serverURL = value; } get { return _serverURL; } }

        private static string _containerURL = SendBox ? "http://container.api.tbsandbox.com/container?appkey={0}" : "http://container.open.taobao.com/container?appkey={0}";
        /// <summary>
        /// 获取授权码容器地址 返回地址：已取值(appkey)  正式环境：http://container.open.taobao.com/container?appkey={0}
        /// <para>测试环境：http://container.api.tbsandbox.com/container?appkey={0}</para>
        /// </summary>
        public static string ContainerURL
        {
            set { _containerURL = value; }
            get { return string.Format(_containerURL, Appkey); }
        }

        private static string _containerAuthCodeURL = SendBox ? "http://container.api.tbsandbox.com/container?authcode={0}" : "http://container.open.taobao.com/container?authcode={0}";
        /// <summary>
        /// 授权容器地址  正式环境：http://container.open.taobao.com/container?authcode={授权码}
        /// <para>测试环境：http://container.api.tbsandbox.com/container?authcode={0}</para>
        /// <para>旧测试环境：http://container.sandbox.taobao.com/container?authcode={0}</para>
        /// </summary>
        public static string ContainerAuthCodeURL
        {
            set { _containerAuthCodeURL = value; }
            get { return _containerAuthCodeURL; }
        }

        /// <summary>
        /// Cookies Name 记录名 + {AppKey} 组成
        /// </summary>
        private static string _cookiesName = "STApp{0}";
        public static string CookiesName
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["CookiesName"] != null)
                {
                    return string.Format(System.Configuration.ConfigurationManager.AppSettings["CookiesName"].ToString(), Appkey);
                }
                else
                {
                    return string.Format(_cookiesName, Appkey);
                }
            }
            set
            {
                _cookiesName = value;
            }
        }
    }
}