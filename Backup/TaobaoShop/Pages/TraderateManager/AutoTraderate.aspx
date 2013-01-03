<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoTraderate.aspx.cs" Inherits="TaobaoShop.Pages.TraderateManager.AutoTraderate" %>
<%@ Register src="../Controls/TopUC.ascx" tagname="TopUC" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet"  href="../../Styles/main.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.7.js"></script>
    <script language="javascript" type="text/javascript">
        function Sure() {
            if (confirm("这样做自动评价会使用选中的评语，要继续吗？")) {
                return true;
            }
            else {
                return false;
            }
        }

        function SureDel() {
            if (confirm("删除后无法恢复，要继续吗？")) {
                return true;
            }
            else {
                return false;
            }
        }

        function ClearOtherRdo(e) {
            var checkedrdo = $("#" + e.id).attr("checked", true);
            var otherrdo = $("#DataList1").find($("input[type='radio']").not(checkedrdo)).attr("checked", false);
        }
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
                        自动评价：</span>
                        <span style="color: rgb(102, 102, 102); font-family: &quot;宋体&quot;; font-size: 12px;">
                                省去您的管理时间，多条评价内容随你设置~</span></div>
                </div>
                <div>
                    <div id="divSearch">
                        <span style="font-family:'宋体'; font-size:12px; color: #333; font-weight: bold;">
                        自动好评状态： 
                        <asp:ImageButton ID="imgbtnSwitch" runat="server" 
                            ImageUrl="~/Image/Auto/on.png" onclick="imgbtnSwitch_Click" />
                        </span>

                        <span style="color: rgb(102, 102, 102); font-family: &quot;宋体&quot;; font-size: 12px;margin-left:20px;"> 开启自动好评，将自动给予买家回评，无需手动操</span>
                    </div>
                </div>

                <div style="text-align:left;vertical-align:middle; margin:6px 0px;  ">

                    <asp:Panel ID="Panel1" runat="server" Height="320px" Width="100%">
                        <asp:DataList ID="DataList1" runat="server" 
                            oncancelcommand="DataList1_CancelCommand" 
                            oneditcommand="DataList1_EditCommand" ondeletecommand="DataList1_DeleteCommand" 
                            onupdatecommand="DataList1_UpdateCommand" Width="100%">
                            <EditItemTemplate>
                            <div style="height:30px; font-size:12px; background-color:#EEEEEE;">
                                <div style="width:60px; float:left; background-color:#E4E4E4;vertical-align:middle; height:26px;  line-height:26px;text-align:center; "><asp:RadioButton ID="rdoContext" runat="server"  Checked='<%#Eval("state") %>' GroupName="item" onclick="ClearOtherRdo(this)" /></div>
                                <div style="width:630px; float:left;background-color:#EEEEEE;vertical-align:middle; height:26px;  line-height:26px;"><asp:TextBox ID="txtContext" runat="server"  Text='<%#Eval("Context") %>'  Width="90%" MaxLength="250"  ForeColor="#3333FF"></asp:TextBox></div>
                                <div style="width:80px; float:left;background-color:#EEEEEE;vertical-align:middle; height:26px;  line-height:26px;">
                                    <asp:LinkButton ID="lbtnSave" runat="server" CommandName="Update">保存</asp:LinkButton><span>|</span>
                                    <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel">取消</asp:LinkButton>
                                    </div>
                                </div>
                            </EditItemTemplate>
                            <ItemTemplate>
                            <div style="height:30px;font-size:12px; background-color:#EEEEEE;">
                            <div style="width:60px; float:left;background-color:#E4E4E4;vertical-align:middle; height:26px;  line-height:26px; text-align:center;">
                                <asp:RadioButton ID="rdoContext" runat="server"  Checked='<%#Eval("state") %>' GroupName="item" onclick="ClearOtherRdo(this)"  /></div>
                                <div style="width:630px; float:left;background-color:#EEEEEE;vertical-align:middle; height:26px;  line-height:26px;"><asp:Label ID="lblContext" runat="server" Text='<%#Eval("Context") %>'  ></asp:Label></div>
                                <div style="width:80px; float:left;background-color:#EEEEEE;vertical-align:middle; height:26px;  line-height:26px;">
                                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit">编辑</asp:LinkButton><span>|</span>
                                    <asp:LinkButton ID="lbtnDel" runat="server" CommandName="Delete" OnClientClick="return SureDel()">删除</asp:LinkButton></div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </asp:Panel>
                </div>

                <div style=" background-color:#FFF5EC;text-align:left;vertical-align:middle;height:40px;  line-height:40px;  ">
                    <div style="background-color:#FFDBBB;text-align:left;vertical-align:middle;  padding-left:20px;height:36px;  line-height:36px;  font-family:'宋体'; font-size:12px; color: #333; font-weight: bold;">
                    <div style="float:right; background-color:#FF7B00;text-align:center; margin:8px 20px 8px 0px; height:26px;line-height:26px;width:80px; ">
                        <asp:LinkButton ID="linkbtnSetType" runat="server" ForeColor="White" 
                            onclick="linkbtnSetType_Click" OnClientClick="return Sure()">确认设置</asp:LinkButton>
                    </div>
                        评价选项：
                        <asp:RadioButton ID="rdoAutoTraderateBuyerPay" runat="server" Checked="True" 
                            GroupName="TraderateType" Text="买家确认付款后自动评价" />
                            &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoAutoTraderateBuyerRated" runat="server" GroupName="TraderateType" 
                            Text="等买家先评价后再自动评价" />
                    </div>
                </div>

                <div style=" background-color:#FEEAD3;margin-top:6px;text-align:left;vertical-align:middle; padding-left:20px;padding-top:8px; padding-bottom:8px;">
                    <div><asp:TextBox ID="areaContext" runat="server" Height="32px" TextMode="MultiLine" 
                        Width="96%"></asp:TextBox></div>
                        <div>
                            <div style="float:right; background-color:#FFFFFF;text-align:center; margin:8px 20px 8px 0px;height:26px;line-height:26px;width:80px; ">
                                <asp:LinkButton ID="linkbtnAddContext" runat="server" ForeColor="Black" 
                                    onclick="linkbtnAddContext_Click">添加好评</asp:LinkButton>
                            </div>
                        </div>
                        <div style="clear:both; height:0px;"></div>
                </div>
            </div>
            <div id="leftMenu" runat="server"></div>
            
        </div>
    </div>
    </form>
</body>
</html>
