<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item_AutoRelist.aspx.cs" Inherits="TaobaoShop.Pages.ItemManager.Item_AutoRelist" %>
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
                        自动上架</span><span style="font-family:'宋体'; font-size:14px; color: #333; font-weight: bold;">：</span>
                        <span style="color: rgb(102, 102, 102); font-family: &quot;宋体&quot;; font-size: 12px;">
                        想开就开，想关就关...（自动上架开启后，当您的宝贝下架时，会将其自动上架，让宝贝永不下架~）</span></div>
                </div>
                <div>
                    <div id="divSearch">
                        <span style="font-family:'宋体'; font-size:12px; color: #333; font-weight: bold;">
                        自动上架状态： 
                        <asp:ImageButton ID="imgbtnSwitch" runat="server" 
                            ImageUrl="~/Image/Auto/on.png" onclick="imgbtnSwitch_Click" />
                        </span>
                    </div>
                </div>

                <div style="text-align:left;vertical-align:middle; padding-left:20px;height:30px;  line-height:30px;   ">
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
