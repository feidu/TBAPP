<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoRecommend.aspx.cs" Inherits="TaobaoShop.Pages.RecommendManager.AutoRecommend" %>
<%@ Register src="../Controls/TopUC.ascx" tagname="TopUC" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet"  href="../../Styles/main.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="Global">
    
        <uc1:TopUC ID="TopUC1" runat="server" />

        <div id="context"> 
            <div id="main">
                <div >
                    <div id="divTip"> 
                        <span style="color: rgb(51, 51, 51); font-family: &quot;宋体&quot;; font-size: 14px; font-weight: bold;">
                        自动橱窗</span><span style="font-family:'宋体'; font-size:14px; color: #333; font-weight: bold;">：</span>
                        <span style="color: rgb(102, 102, 102); font-family: &quot;宋体&quot;; font-size: 12px;">
                        想开就开，相关就关...<span 
                            style="color: rgb(153, 153, 153); font-family: &quot;宋体&quot;; font-size: 12px;">（自动橱窗开启后，当您的宝贝下架时，会自动接上宝贝推荐，让橱窗永不留空）</span></span></div>
                </div>
                <div>
                    <div id="divSearch">
                        <span style="font-family:'宋体'; font-size:12px; color: #333; font-weight: bold;">
                        自动橱窗状态： 
                        <asp:ImageButton ID="imgbtnSwitch" runat="server" 
                            ImageUrl="~/Image/Auto/on.png" onclick="imgbtnSwitch_Click" />
                        </span>
                    </div>
                </div>

                <div style="text-align:left;vertical-align:middle; padding-left:20px;height:30px;  line-height:30px;   ">
                    <asp:CheckBox ID="cboDelistFirst" runat="server" AutoPostBack="True" 
                        Checked="True" oncheckedchanged="cboDelistFirst_CheckedChanged" 
                        Text="快下架的宝贝优先推荐" />
                    </div>

                <div>
                    <div>
                        &nbsp;</div>
                </div>
            </div>
            <div id="leftMenu" runat="server"></div>
            
        </div>
    
    </div>
    </form>
</body>
</html>
