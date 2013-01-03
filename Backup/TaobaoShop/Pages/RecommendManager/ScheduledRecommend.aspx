<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduledRecommend.aspx.cs" Inherits="TaobaoShop.Pages.RecommendManager.ScheduledRecommend" %>
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
             Sche();
         }

         function Sche() {
             var txt = $("#txtScheduleTime").val();
             if (txt == "") {
                 alert("请设定时间再预览！");
                 return false;
             }

             var idlist = new Array();
             $("input[id^='DataList1_item']").each(function (i) {
                 idlist[i] = this.value;
             });

             for (var i = 0; i < idlist.length; i++) {
                 if ($("#DataList1_cbolist_" + i).attr("checked") == "checked") {
                    $("span[iid='" + idlist[i] + "']").html(txt+" 上架");
                 }
            }
            //检查是否存在已推荐并提示
            var flag=false;
            for (var i = 0; i < idlist.length; i++) {
                if ($("#DataList1_cbolist_" + i).attr("checked") == "checked") {
                    if ($("#DataList1_lblHasShowcase_" + i).attr("value") == "已推荐") {
                        flag = true;
                    }
                }
            }
            if (flag)
                alert("注意：有宝贝已推荐，该宝贝可能无法正常推荐！");

        }

        function checktime() {
            var txt;
            txt = $("#txtScheduleTime").val();
            if (txt == "") {
                alert("请设定时间再提交！");
                return false;
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
                        定时橱窗</span><span style="font-family:'宋体'; font-size:14px; color: #333; font-weight: bold;">：</span>
                        <span style="color: rgb(102, 102, 102); font-family: &quot;宋体&quot;; font-size: 12px;">
                        让您的宝贝第一时间成为“黄金橱窗”</span></div>
                </div>
                <div>
                    <div id="divSearch">
                        <span style="font-family:'宋体'; font-size:12px; color: #333; font-weight: bold;">
                        橱窗宝贝： 
                        </span>
                        <span>
                        <asp:TextBox ID="txtTitleSearch" runat="server" Width="450px"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="搜索" />
                        </span>
                        <span>
                            &nbsp;<asp:CheckBox 
                            ID="cboDelistFirst" runat="server" Text="按快下架的宝贝排序" AutoPostBack="True" 
                            Checked="True" oncheckedchanged="cboDelistFirst_CheckedChanged" />
                        </span>
                    </div>
                </div>
                <div style="margin-top:8px;">
                <div style="float:left;background-color:#FF7B00;height:30px; line-height:30px;width:120px; text-align:center; color:#FFF;">定时橱窗推荐</div>
                <div style="float:left;background-color:#E6E6E6;height:30px; line-height:30px;width:120px; text-align:center;color:#000"><a href="RecommendManager.aspx" target="_self">橱窗推荐管理</a></div>
                <div style="height:3px;background-color:#FF7B00;clear:both;"></div>
                </div>
                <div style="text-align:left;vertical-align:middle; padding-left:20px;height:30px;  line-height:30px;   ">
                    <input type="checkbox" id="check_all"  value="" checked="checked" onclick="return btnCheckAll_onclick()" />全选/取消
                    &nbsp;&nbsp;
                    橱窗空位数：<asp:Label ID="lblRemainCount" runat="server" Text=""></asp:Label>
                    </div>
                <div>
                    <asp:Panel ID="Panel1" runat="server" Height="350px" Width="100%">
                    <asp:DataList ID="DataList1" runat="server">
                        <ItemTemplate>
                        <div style="height:30px; ">
                            <div style="width:60px; float:left;background-color:#E4E4E4;vertical-align:middle; height:26px;  line-height:26px; text-align:center;"><asp:CheckBox ID="cbolist" runat="server" Checked="true" />
                            <input type="text" id="item" value='<% #Eval("NumIid") %>' style="display:none;" runat="server" /></div>
                            <div style="width:430px; float:left;background-color:#EEEEEE;vertical-align:middle; height:26px;  line-height:26px; overflow:hidden;"><asp:Label ID="lblName" runat="server" Text='<% #Eval("Title") %>'></asp:Label></div>
                            <div style="width:100px;float:left;background-color:#EEEEEE;vertical-align:middle; height:26px;  line-height:26px;overflow:hidden;"><span type="text" id="lblHasShowcase" value='<%#GetHasShowcase(Eval("HasShowcase"))%>' style="color: #090; " runat="server"/><%#GetHasShowcase(Eval("HasShowcase"))%></span></div>
                            <div style="width:20px; float:left;vertical-align:middle; height:26px;  line-height:26px;">&gt;</div>
                            <div style="width:160px;float:left;background-color:#EEEEEE;vertical-align:middle; height:26px;  line-height:26px;overflow:hidden;"><span type="text" ID="lblNewName" iid='<% #Eval("NumIid") %>' style="color: #090; " runat="server"/></span></div>
                        </div>
                        </ItemTemplate>
                    </asp:DataList>
                    <%=PageListLink%>
                    </asp:Panel>
                </div>
                <div  style="background:#FFF5EC; border-style:solid;  border-width:1px;border-color:#FFDBBB;padding:8px;">
                    <div> 
                        自定时间：
                        &nbsp;<asp:TextBox ID="txtScheduleTime" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></asp:TextBox>
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
