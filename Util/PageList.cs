using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Util
{
    /// <summary>
    /// ST.Page 分页 实体 
    /// </summary>
    public class PageList
    {
        private int pageSize = 20;
        /// <summary>
        /// 页大小 默认为20
        /// </summary>
        public int PageSize
        {
            set { pageSize = value; }
            get { return pageSize; }
        }
        private int pageID = 1;
        /// <summary>
        /// 页ID
        /// </summary>
        public int PageID
        {
            set { pageID = value; }
            get { return pageID; }
        }

        private int pageCount = 1;
        /// <summary>
        /// 页总数
        /// </summary>
        public int PageCount
        {
            set { pageCount = value; }
            get { return pageCount; }
        }
        private int recordCount = 1;
        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount
        {
            set { recordCount = value; }
            get { return recordCount; }
        }

    }

    /// <summary>
    /// ST.Page Url方式分页 v1.0
    /// </summary>
    public class PageListBll
    {
        private string pageName = "?PageID={0}";
        private string pageLink = "";

        public PageListBll()
        {
            for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
            {
                if (HttpContext.Current.Request.Params.Keys[i].ToString().ToLower() != "pageid")
                    pageLink += HttpContext.Current.Request.Params.Keys[i].ToString() + "=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Params[HttpContext.Current.Request.Params.Keys[i]].ToString()) + "&";
            }

            PageName = "?" + pageLink + "PageID={0}";
        }

        /// <summary>
        /// 自定连接地址 不带其它动态参数 仅 {0} 代表PageID
        /// </summary>
        /// <param name="pageName">指定连接页地址 {0}=PageID</param>
        public PageListBll(string pageHref)
        {
            this.PageName = pageHref;
        }

        /// <summary>
        /// 页面完整连接名称 动态参数 {0} 代表PageID
        /// </summary>
        public string PageName
        {
            set { pageName = value; }
            get { return pageName; }
        }

        #region 分页显示文字

        private string firstPageText = "首页";
        /// <summary>
        /// 首页 按钮显示文本
        /// </summary>
        public string FirstPageText
        {
            set { firstPageText = value; }
            get { return firstPageText; }
        }

        private string prevPageText = "上一页";
        /// <summary>
        /// 上一页 按钮显示文本
        /// </summary>
        public string PrevPageText
        {
            set { prevPageText = value; }
            get { return prevPageText; }
        }

        private string nextPageText = "下一页";
        /// <summary>
        /// 下一页 按钮显示文本
        /// </summary>
        public string NextPageText
        {
            set { nextPageText = value; }
            get { return nextPageText; }
        }

        private string lastPageText = "尾页";
        /// <summary>
        /// 尾页 按钮显示文本
        /// </summary>
        public string LastPageText
        {
            set { lastPageText = value; }
            get { return lastPageText; }
        }

        private int numericButtonCount = 10;
        /// <summary>
        /// 数值按钮的数目 默认10
        /// </summary>
        public int NumericButtonCount
        {
            set { numericButtonCount = value; }
            get { return numericButtonCount; }
        }

        #endregion

        #region 分页内容模板

        #endregion

        #region 是否显示区域

        private bool showNoRecordInfo = true;
        /// <summary>
        /// 无记录时 是否显示分页信息
        /// </summary>
        public bool ShowNoRecordInfo
        {
            set { showNoRecordInfo = value; }
            get { return showNoRecordInfo; }
        }
        private bool showPageIndex = true;
        /// <summary>
        /// 是否显示 当前页ID信息 默认显示(true)
        /// </summary>
        public bool ShowPageIndex
        {
            set { showPageIndex = value; }
            get { return showPageIndex; }
        }

        private bool showPageCount = true;
        /// <summary>
        /// 是否显示 当前页总数 默认显示(true)
        /// </summary>
        public bool ShowPageCount
        {
            set { showPageCount = value; }
            get { return showPageCount; }
        }

        private bool showRecordCount = true;
        /// <summary>
        /// 是否显示 记录总数 默认显示(true)
        /// </summary>
        public bool ShowRecordCount
        {
            set { showRecordCount = value; }
            get { return showRecordCount; }
        }

        private bool showPageListButton = true;
        /// <summary>
        /// 是否显示 页面按钮 默认显示(true)
        /// </summary>
        public bool ShowPageListButton
        {
            set { showPageListButton = value; }
            get { return showPageListButton; }
        }

        private bool showNumListButton = true;
        /// <summary>
        /// 是不显示 页面数字按钮 默认显示(true)
        /// </summary>
        public bool ShowNumListButton
        {
            set { showNumListButton = value; }
            get { return showNumListButton; }
        }

        #endregion

        public string GetPageList(PageList pl)
        {
            StringBuilder strPage = new StringBuilder("");

            if (pl.RecordCount == 0)
            {
                //return "<div class=\"PageInfo\" align=\"center\">\r Page&nbsp;<b>1</b>&nbsp;Of&nbsp;<b>1</b>&nbsp;,&nbsp;Record <b>0</b>&nbsp;&nbsp;&nbsp;&nbsp;首页&nbsp;&nbsp;&nbsp;&nbsp;上一页&nbsp;&nbsp;&nbsp;&nbsp;下一页&nbsp;&nbsp;&nbsp;&nbsp;尾页&nbsp;&nbsp; \r </div>";
                // {0} 首页 {1} 上一页 {2} 下一页 {3} 尾页
                //return string.Format("<div class=\"PageInfo\" align=\"center\">\r Page&nbsp;<b>1</b>&nbsp;Of&nbsp;<b>1</b>&nbsp;,&nbsp;Record <b>0</b>&nbsp;&nbsp;&nbsp;&nbsp;{0}&nbsp;&nbsp;&nbsp;&nbsp;{1}&nbsp;&nbsp;&nbsp;&nbsp;{2}&nbsp;&nbsp;&nbsp;&nbsp;{3}&nbsp;&nbsp; \r </div>", FirstPageText, PrevPageText, NextPageText, LastPageText);

                #region 无记录信息
                if (ShowNoRecordInfo == true)
                {
                    strPage.Append("<div class=\"PageInfo\" align=\"center\">\r");
                    //当前页
                    if (ShowPageIndex == true) { strPage.Append("当前第&nbsp;<b>1</b>&nbsp;页"); }
                    //当总数
                    if (ShowPageCount == true) { strPage.Append("&nbsp;共&nbsp;<b>1</b>&nbsp;页"); }
                    //记录总数
                    if (ShowRecordCount == true) { strPage.Append("&nbsp;,&nbsp;总记录数：<b>0</b>"); }

                    if (ShowPageListButton == true)
                    {
                        //判断首页ID//FirstPage
                        strPage.Append(string.Format("&nbsp;&nbsp;&nbsp;&nbsp;{0}&nbsp;&nbsp;", FirstPageText));

                        //判断上一页ID//PrevPage
                        strPage.Append(string.Format("&nbsp;&nbsp;{0}&nbsp;&nbsp;", PrevPageText));

                        //判断下一页ID//NextPage
                        strPage.Append(string.Format("&nbsp;&nbsp;{0}&nbsp;&nbsp;", NextPageText));

                        //判断最后页//LastPage
                        strPage.Append(string.Format("&nbsp;&nbsp;{0}", LastPageText));
                    }

                    strPage.Append("\r</div>");
                }
                return strPage.ToString();
                #endregion
            }

            pl.PageCount = (pl.RecordCount / pl.PageSize);

            //if (PageCount < (RecordCount / PageSize))
            //     PageCount = ( RecordCount / PageSize ) + (RecordCount % PageSize);

            if (pl.PageCount < ((double)pl.RecordCount / (double)pl.PageSize))
            {
                pl.PageCount++;
            }

            strPage.Append("<div class=\"PageInfo\" align=\"center\">\r");
            //当前页
            if (ShowPageIndex == true)
            {
                strPage.Append("当前第&nbsp;<b>" + pl.PageID.ToString() + "</b>&nbsp;页");
            }
            //当总数
            if (ShowPageCount == true)
            {
                strPage.Append("&nbsp;共&nbsp;<b>" + pl.PageCount + "</b>&nbsp;页");
            }
            //记录总数
            if (ShowRecordCount == true)
            {
                strPage.Append("&nbsp;,&nbsp;总记录数： <b>" + pl.RecordCount.ToString() + "</b>");
            }

            if (ShowPageListButton == true)
            {
                if (pl.PageID == 1)//判断首页ID//FirstPage
                {
                    //strPage.Append("&nbsp;&nbsp;&nbsp;&nbsp;首页&nbsp;&nbsp;");
                    strPage.Append(string.Format("&nbsp;&nbsp;&nbsp;&nbsp;{0}&nbsp;&nbsp;", FirstPageText));
                }
                else
                {
                    //strPage.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"" + string.Format(PageName, "1") + "\">首页</a>&nbsp;&nbsp;");
                    strPage.Append(string.Format("&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"" + string.Format(PageName, "1") + "\">{0}</a>&nbsp;&nbsp;", FirstPageText));
                }

                if (pl.PageID == 1)//判断上一页ID//PrevPage
                {
                    //strPage.Append("&nbsp;&nbsp;上一页&nbsp;&nbsp;");
                    strPage.Append(string.Format("&nbsp;&nbsp;{0}&nbsp;&nbsp;", PrevPageText));
                }
                else
                {
                    //strPage.Append("&nbsp;&nbsp;<a href=\"" + string.Format(PageName, (pl.PageID - 1)) + "\">上一页</a>&nbsp;&nbsp;");
                    strPage.Append(string.Format("&nbsp;&nbsp;<a href=\"" + string.Format(PageName, (pl.PageID - 1)) + "\">{0}</a>&nbsp;&nbsp;", PrevPageText));
                }

                if (pl.PageID == pl.PageCount)//判断下一页ID//NextPage
                {
                    //strPage.Append("&nbsp;&nbsp;下一页&nbsp;&nbsp;");
                    strPage.Append(string.Format("&nbsp;&nbsp;{0}&nbsp;&nbsp;", NextPageText));
                }
                else
                {
                    //strPage.Append("&nbsp;&nbsp;<a href=\"" + string.Format(PageName, (pl.PageID + 1)) + "\">下一页</a>&nbsp;&nbsp;");
                    strPage.Append(string.Format("&nbsp;&nbsp;<a href=\"" + string.Format(PageName, (pl.PageID + 1)) + "\">{0}</a>&nbsp;&nbsp;", NextPageText));
                }

                if (pl.PageID == pl.PageCount)//判断最后页//LastPage
                {
                    //strPage.Append("&nbsp;&nbsp;尾页");
                    strPage.Append(string.Format("&nbsp;&nbsp;{0}", LastPageText));
                }
                else
                {
                    //strPage.Append("&nbsp;&nbsp;<a href=\"" + string.Format(PageName, pl.PageCount) + "\">尾页</a>");
                    strPage.Append(string.Format("&nbsp;&nbsp;<a href=\"" + string.Format(PageName, pl.PageCount) + "\">{0}</a>", LastPageText));
                }

            }

            if (ShowNumListButton == true)
            {
                //分页码 顺序列表
                // int PageNumList = 10; //页连续数字 091012 换成 NumericButtonCount
                strPage.Append("&nbsp;&nbsp;<span class=\"PageNumButton\">");//Num List
                int loopNum = pl.PageID - (NumericButtonCount / 2);

                if (loopNum < 1)
                    loopNum = 1;

                for (int i = loopNum; i < loopNum + NumericButtonCount; i++)
                {
                    if (i > pl.PageCount)
                    {
                        break;
                    }

                    //if (i == pl.PageID)
                    //{
                    //    strPage.Append("&nbsp;<font color=red>" + i.ToString() + "</font>&nbsp;");
                    //    strPage.Append("&nbsp;<label class=\"PageIndex\">" + i.ToString() + "</label>&nbsp;");//span
                    //}
                    //else
                    //{
                    //    strPage.Append("&nbsp;<a href=\"" + string.Format(PageName, i) + "\">" + i.ToString() + "</a>&nbsp;");
                    //}
                }
                strPage.Append("</span>");
            }

            strPage.Append("\r</div>");
            return strPage.ToString();
        }

    }
}
