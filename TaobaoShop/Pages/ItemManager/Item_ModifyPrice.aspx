<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item_ModifyPrice.aspx.cs" Inherits="TaobaoShop.Pages.ItemManager.Item_ModifyPrice" %>
<%@ Register src="../Controls/TopUC.ascx" tagname="TopUC" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet"  href="../../Styles/main.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.7.js"></script>
    <script language="javascript" type="text/javascript">
// <![CDATA[
        function btnLook_onclick() {

            var idlist = new Array();
            $("input[id^='DataList1_item']").each(function (i) {
                idlist[i] = this.value;
            });

            var oldPricelist = new Array();
            $("span[id^='DataList1_lblOldPrice']").each(function (i) {
                oldPricelist[i] =parseFloat(this.innerHTML);
            });

            var rdosswr = document.getElementById("rdosswr");
            var rdoqdxs = document.getElementById("rdoqdxs");
            var operat = $("#ddlOperat").val();
            var MPrice = parseFloat($("#txtMPrice").val());
            if (isNaN(MPrice)) {
                alert("请填写价格！");
                return false;
            }   

            for (var i = 0; i < oldPricelist.length; i++) {
                if ($("#DataList1_cbolist_" + i).attr("checked")) {
                    switch (operat) {
                        case "add":
                            oldPricelist[i] = oldPricelist[i] + MPrice;
                            break;
                        case "sub":
                            oldPricelist[i] = oldPricelist[i] - MPrice;
                            break;
                        case "mul":
                            oldPricelist[i] = oldPricelist[i] * MPrice;
                            break;
                        case "div":
                            oldPricelist[i] = oldPricelist[i] / MPrice;
                            break;
                    }
                    if (rdosswr.checked) {
                        oldPricelist[i] = fomatFloat(oldPricelist[i], 0);
                    }
                    if (rdoqdxs.checked) {
                        oldPricelist[i] = toDecimal(oldPricelist[i]);
                    }
                }
            }
            for (var i = 0; i < idlist.length; i++) {
                $("span[iid='" + idlist[i] + "']").html("现价："+oldPricelist[i].toString());
            }
        }

        function fomatFloat(src, pos) {
            return Math.round(src * Math.pow(10, pos)) / Math.pow(10, pos);
        }

        function toDecimal(x) {
            var s = x.toString();
            var rs = s.indexOf('.');
            if (rs >0) {
                s=s.substring(0,rs)
            }
            return parseFloat(s);
        }   

        function btnCheckAll_onclick() {
            if ($("#check_all").attr("checked")) {
                    $('input[type="checkbox"]:not(#check_all)').attr("checked", "checked");
                }
                else {
                    $('input[type="checkbox"]:not(#check_all)').removeAttr("checked");
                }
        }

// ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Global">
    
        <uc1:TopUC ID="TopUC1" runat="server" />

        <div id="context"> 
            <div id="main">
                <div >
                    <div id="divTip"> <span style="font-family:'宋体'; font-size:14px; color: #333; font-weight: bold;">
                        宝贝价格：</span>
                     <span style="font-family:'宋体'; font-size:12px; color: #666;">及时观察竞争同行的价格并随时更新自己的价格，让价格取得决定性优势...</span>
                     </div>
                </div>
                <div>
                    <div id="divSearch"><span style="font-family:'宋体'; font-size:12px; color: #333; font-weight: bold;">修改范围： </span>
                        <span>
                            <asp:DropDownList ID="ddlItemState" runat="server">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtTitleSearch" runat="server" Width="450px"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="搜索" />
                        </span>
                    </div>
                </div>

                <div style="text-align:left;vertical-align:middle; padding-left:20px;height:30px;  line-height:30px;   ">
                    <input type="checkbox" id="check_all"  value="" checked="checked" onclick="return btnCheckAll_onclick()" />全选/取消</div>

                <div>
                    <asp:Panel ID="Panel1" runat="server" Height="350px"  Width="100%">
                    <asp:DataList ID="DataList1" runat="server">
                        <ItemTemplate>
                        <div style="height:30px; ">
                            <div style="width:60px; float:left;background-color:#E4E4E4;vertical-align:middle; height:26px;  line-height:26px; text-align:center;"><asp:CheckBox ID="cbolist" runat="server" Checked="true" />
                            <input type="text" id="item" value='<% #Eval("NumIid") %>' style="display:none;" runat="server" /></div>
                            <div style="width:430px; float:left;;background-color:#EEEEEE;vertical-align:middle; height:26px;  line-height:26px; overflow:hidden;"><asp:Label ID="lblName" runat="server" Text='<% #Eval("Title") %>'></asp:Label></div>
                            <div style="width:130px;float:left;;background-color:#EEEEEE;vertical-align:middle; height:26px;  line-height:26px; overflow:hidden;">#原价：<asp:Label ID="lblOldPrice" runat="server" Text='<% #Eval("Price") %>'></asp:Label></div>
                            <div style="width:20px; float:left;">&gt;</div>
                            <div style="width:130px;float:left;;background-color:#EEEEEE;vertical-align:middle; height:26px;  line-height:26px; overflow:hidden;"><span type="text" ID="lblNewPrice" iid='<% #Eval("NumIid") %>' style="color: #090; " runat="server"/></span></div>
                        </div>
                        </ItemTemplate>
                    </asp:DataList>
                    <%=PageListLink%>
                    </asp:Panel>
                </div>
                <div style="background:#FFF5EC; border-style:solid;  border-width:1px;border-color:#FFDBBB;padding:8px;">
                    <div> 
                        修改价格：当前价格：<asp:DropDownList ID="ddlOperat" runat="server">
                        </asp:DropDownList>
&nbsp;<asp:TextBox ID="txtMPrice" runat="server"></asp:TextBox>
&nbsp;<span style="color: rgb(153, 153, 153); font-family: &quot;宋体&quot;; font-size: 12px;">算术法计算方式,附计算器辅助&gt;&gt;</span></div>
                    <div>
                        末尾小数： 
                        <asp:RadioButton ID="rdosswr" runat="server" Checked="True" GroupName="count" 
                            Text="四舍五入" />
&nbsp;<asp:RadioButton ID="rdoqdxs" runat="server" GroupName="count" Text="去掉小数点" />
                        </div>
                    <div>
                        <input id="btnLook" type="button" value="预览" onclick="return btnLook_onclick()" />
                        <asp:Button ID="btnModify" runat="server" onclick="btnModify_Click" Text="提交" />
                    </div>
                </div>
            </div>
            <div id="leftMenu" runat="server"></div>
            
        </div>
    
    </div>
    </form>
</body>
</html>
