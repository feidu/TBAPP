using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace TaobaoShop
{
    public static class Menu
    {
        public static string GetMenuHtml(string subliformat, string subulformat, string pliformat, string pageCode)
        {
            string menuHtml = "<ul>";
            XmlDocument doc = new XmlDocument();
            string xmlpath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + "\\Pages\\frame\\menu.xml");
            doc.Load(xmlpath);
            XmlNode root = doc.FirstChild.NextSibling;
            if (root.HasChildNodes)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    string list2temp = string.Empty;
                    foreach (XmlNode list in node.ChildNodes)
                    {
                        string code = list.Attributes["code"].Value;
                        string style = "style=\"font-size:14px;\" onmouseover=\"this.style.backgroundColor='#EEEEEE'\" onmouseout=\"this.style.backgroundColor=''\"";
                        if (code==pageCode)
                        {
                            style = "style=\"background:#FF7B00;font-size:14px; color:#FFF; font-weight:bold;\" onmouseover=\"this.style.backgroundColor='#EEEEEE';color:#FFF;\" onmouseout=\"this.style.backgroundColor='#FF7B00';color:#000;\"";
                        }
                        string name = list.Attributes["name"].Value;
                        string url = list.Attributes["url"].Value;
                        string temp = string.Format(subliformat, style, url, name);
                        list2temp += temp;
                    }
                    if (list2temp != "")
                    {
                        menuHtml += string.Format(pliformat, node.Attributes["name"].Value);
                        menuHtml += string.Format(subulformat, list2temp);
                    }
                }
            }
            menuHtml += "</ul>";
            return menuHtml;
        }
    }
}