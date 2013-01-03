<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item_ScheduledRelist.aspx.cs" Inherits="TaobaoShop.Pages.ItemManager.Item_ScheduledRelist" %>
<%@ Register src="../Controls/TopUC.ascx" tagname="TopUC" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet"  href="../../Styles/main.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../../My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.7.js"></script>
    <script language="javascript" type="text/javascript">
// <![CDATA[
        function btnLook_onclick() {
            var checked = $("#rdoAuto").attr("checked");
            if (checked == "checked") {
                Auto();
            } else {
                Sche();
            }
        }

        function Sche() {
            var txt = $("#txtGoldTime").val();
            if (txt == "") {
                alert("请设定时间再预览！");
                return false;
            }

            var rdo1 = $("#rdo1").attr("checked");
            var rdo2 = $("#rdo2").attr("checked");
            var rdo3 = $("#rdo3").attr("checked");
            var day= $("#txtGoldTime").val();
            var hhbegin;
            var hhend;
            if (rdo1 == "checked") {
                hhbegin = 10;
                hhend = 11;
            }
            if (rdo2 == "checked") {
                hhbegin = 13;
                hhend = 16;
            }
            if (rdo3 == "checked") {
                hhbegin = 20;
                hhend = 22;
            }
            var idlist = new Array();
            $("input[id^='DataList1_item']").each(function (i) {
                idlist[i] = this.value;
            });
            var total = 0;
            var interval = 0;
            for (var i = 0; i < idlist.length; i++) {
                if ($("#DataList1_cbolist_" + i).attr("checked") == "checked") {
                    total++;
                }
            }
            interval = (hhend - hhbegin) * 60 / total; //间隔 分钟
            var fen = 00;
            for (var i = 0; i < idlist.length; i++) {
                if ($("#DataList1_cbolist_" + i).attr("checked") == "checked") {
                    
                    if (fen == 00) {
                        $("span[iid='" + idlist[i] + "']").html(txt + " " + hhbegin + ":00 上架");
                    }else{
                        $("span[iid='" + idlist[i] + "']").html(txt + " " + hhbegin + ":" + fen + " 上架");
                    }
                    fen = fen + interval;
                    while (fen >= 60) {
                        fen = fen-60;
                        hhbegin++;
                    }
                }
            }

        }

        function Auto() {
            var txt = $("#txtScheduleTime").val();
            if (txt == "") {
                alert("请设定时间再预览！");
                return false;
            }

            var idlist1 = new Array();
            $("input[id^='DataList1_item']").each(function (i) {
                idlist1[i] = this.value;
            });
            for (var i = 0; i < idlist1.length; i++) {
                if ($("#DataList1_cbolist_" + i).attr("checked") == "checked") {
                    $("span[iid='" + idlist1[i] + "']").html(txt + " 上架");
                }
            }
        }

        function checktime() {
            var txt;
            var checked = $("#rdoAuto").attr("checked");
            if (checked == "checked") {
                txt = $("#txtScheduleTime").val();
                if (txt == "") {
                    alert("请设定时间再提交！");
                    return false;
                }
            } else {
                txt = $("#txtGoldTime").val();
                if (txt == "") {
                    alert("请设定时间再提交！");
                    return false;
                }
            }
            
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
                    <div id="divTip"> 
                        <span style="color: rgb(51, 51, 51); font-family: &quot;宋体&quot;; font-size: 14px; font-weight: bold;">
                        定时上架</span><span style="font-family:'宋体'; font-size:14px; color: #333; font-weight: bold;">：</span>
                        <span style="color: rgb(102, 102, 102); font-family: &quot;宋体&quot;; font-size: 12px;">
                        让仓库中的宝贝第一时间赶上“黄金架”，无需手动等待</span></div>
                </div>
                <div>
                    <div id="divSearch">
                        <span style="font-family:'宋体'; font-size:12px; color: #333; font-weight: bold;">
                        仓库中的宝贝： 
                        </span>
                        <span>
                        <asp:TextBox ID="txtTitleSearch" runat="server" Width="450px"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="搜索" />
                        </span>
                    </div>
                </div>

                <div style="text-align:left;vertical-align:middle; padding-left:20px;height:30px;  line-height:30px;   ">
                    <input type="checkbox" id="check_all"  value="" checked="checked" onclick="return btnCheckAll_onclick()" />全选/取消</div>

                <div>
                    <asp:Panel ID="Panel1" runat="server" Height="350px" Width="100%">
                    <asp:DataList ID="DataList1" runat="server" RepeatColumns="4"  RepeatDirection="Horizontal">
                        <ItemTemplate>
                        
                        <table width="180" border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td width="180" height="180" align="center" valign="middle">&nbsp;<img  src='<% #Eval("PicUrl")%>' width="160" height="160" title='<%#Eval("Title") %>' style="border:0" runat="server"/>
                            <input type="text" id="item" value='<% #Eval("NumIid") %>' style="display:none;" runat="server" />
                            </td>
                          </tr>
                          <tr>
                            <td height="30">&nbsp;<asp:CheckBox ID="cbolist" runat="server" Checked="true"/><asp:Label ID="lblName" runat="server" Text='<%#GetText(Eval("Title")) %>' title='<%#Eval("Title") %>'></asp:Label></td>
                          </tr>
                          <tr>
                            <td height="30">&nbsp;<span type="text" ID="lblTime" iid='<% #Eval("NumIid") %>' style="color: #090; " runat="server"/></span></td>
                          </tr>
                        </table>
                        </ItemTemplate>
                    </asp:DataList>
                    <%=PageListLink%>
                    </asp:Panel>
                </div>
                <div  style="background:#FFF5EC; border-style:solid;  border-width:1px;border-color:#FFDBBB;padding:8px;">
                    <div> 
                    <input id="rdoAuto" name="type" type="radio" checked="true" runat="server"/>
                        自定时间：
                        &nbsp;<asp:TextBox ID="txtScheduleTime" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></asp:TextBox>
                    </div>
                    <div>
                    <input id="rdoSche" name="type" type="radio"  runat="server"/>
                        黄金时间：
                         &nbsp;<asp:TextBox ID="txtGoldTime" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                        <input id="rdo1" name="gold" type="radio" checked="true" runat="server"/>早上 10:00 - 11:00 
                        <input id="rdo2" name="gold" type="radio" runat="server"/>下午 13:00 - 16:00 
                        <input id="rdo3" name="gold" type="radio" runat="server"/>晚上 20:00 - 22:00
                    </div>
                    <div>
                        <input id="btnLook" type="button" value="预览" onclick="return btnLook_onclick()" />
                        <asp:Button ID="btnModify" runat="server" onclick="btnModify_Click" Text="提交"  OnClientClick="return checktime()"/>
                    </div>
                    
                </div>
            </div>
            <div id="leftMenu" runat="server"></div>
            
        </div>
    
    </div>
    </form>
</body>
</html>
