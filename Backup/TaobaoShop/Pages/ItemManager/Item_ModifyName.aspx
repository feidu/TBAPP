<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item_ModifyName.aspx.cs" Inherits="TaobaoShop.Pages.ItemManager.Item_ModifyName" %>
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

            var oldnamelist = new Array();
            $("span[id^='DataList1_lblName']").each(function (i) {
                oldnamelist[i] = this.innerHTML;
            });

            var rdoReplace=document.getElementById("rdoReplace");
            var rdoAdd=document.getElementById("rdoAdd");
            var rdoAll = document.getElementById("rdoAll");

            if (rdoReplace.checked) {
                var repName = $("#txtReplace").val();
                var newName = $("#txtReplaceNew").val();
                if (repName == "") {
                    alert("请填写要被替换的名称");
                    return false;
                }
                for (var i = 0; i < oldnamelist.length; i++) {
                    if ($("#DataList1_cbolist_" + i).attr("checked") ) {
                        oldnamelist[i] = oldnamelist[i].replace(repName, newName);
                        if (oldnamelist[i].length > 30) {
                            alert("商品：["+oldnamelist[i]+"] 长度超过30，提交时可能失败");
                        }
                    }
                }
                for (var i = 0; i < idlist.length; i++) {
                    $("span[iid='" + idlist[i] + "']").html(oldnamelist[i]);
                }
            }

            if (rdoAdd.checked) {
                var firstAdd = $("#txtFirstAdd").val();
                var footerAdd = $("#txtEndAdd").val();
                if (firstAdd == "" && footerAdd=="") {
                    alert("请至少填写一个要追加的名称");
                    return false;
                }
                for (var i = 0; i < oldnamelist.length; i++) {
                    if ($("#DataList1_cbolist_" + i).attr("checked")) {
                        oldnamelist[i] = firstAdd + oldnamelist[i] + footerAdd;
                        if (oldnamelist[i].length > 30) {
                            alert("商品：[" + oldnamelist[i] + "] 长度超过30，提交时可能失败");
                        }
                    }
                }
                for (var i = 0; i < idlist.length; i++) {
                    $("span[iid='" + idlist[i] + "']").html(oldnamelist[i]);
                }
            }

            if (rdoAll.checked) {
                var newName = $("#txtReplaceAll").val();
                if (newName == "") {
                    alert("请填写要修改的名称");
                    return false;
                }
                for (var i = 0; i < oldnamelist.length; i++) {
                    if ($("#DataList1_cbolist_" + i).attr("checked")) {
                        oldnamelist[i] = newName;
                    }
                    if (oldnamelist[i].length > 30) {
                        alert("商品：[" + oldnamelist[i] + "] 长度超过30，提交时可能失败");
                    }
                }
                for (var i = 0; i < idlist.length; i++) {
                    $("span[iid='" + idlist[i] + "']").html(oldnamelist[i]);
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
                    <div id="divTip"> <span style="font-family:'宋体'; font-size:14px; color: #333; font-weight: bold;">宝贝名称：</span>
                     <span style="font-family:'宋体'; font-size:12px; color: #666;">进一步优化自己的宝贝标题，从而更最大化的吸引买家的眼球~</span>
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
                    <asp:Panel ID="Panel1" runat="server" Height="350px">
                    <asp:DataList ID="DataList1" runat="server" Width="100%">
                        <ItemTemplate>
                        <div style="height:30px; ">
                            <div style="width:60px; float:left;background-color:#E4E4E4;vertical-align:middle; height:26px;  line-height:26px; text-align:center;"><asp:CheckBox ID="cbolist" runat="server" Checked="true" />
                            <input type="text" id="item" value='<% #Eval("NumIid") %>' style="display:none;" runat="server" /></div>
                            <div style="width:345px; float:left;background-color:#EEEEEE;vertical-align:middle; height:26px;  line-height:26px; overflow:hidden;"><asp:Label ID="lblName" runat="server" Text='<% #Eval("Title") %>'></asp:Label></div>
                            <div style="width:20px; float:left;vertical-align:middle; height:26px;  line-height:26px;">&gt;</div>
                            <div style="width:345px;float:left;background-color:#EEEEEE;vertical-align:middle; height:26px;  line-height:26px;overflow:hidden;"><span type="text" ID="lblNewName" iid='<% #Eval("NumIid") %>' style="color: #090; " runat="server"/></span></div>
                        </div>
                        </ItemTemplate>
                    </asp:DataList>
                    <%=PageListLink%>
                    </asp:Panel>
                </div>
                <div style=" background:#FFF5EC; border-style:solid;  border-width:1px;border-color:#FFDBBB;padding:8px;">
                    <div> 
                        <span>
                        <asp:RadioButton ID="rdoReplace" runat="server" GroupName="modify" 
                            Text="替换名称：" Checked="True" /> </span>
                        	<span>将宝贝名称：<asp:TextBox ID="txtReplace" runat="server" MaxLength="30"></asp:TextBox></span>
                            	<span>替换为：<asp:TextBox ID="txtReplaceNew" runat="server" MaxLength="30"></asp:TextBox></span>
                        </div>
                    <div>
                       <span> <asp:RadioButton ID="rdoAdd" runat="server" GroupName="modify" Text="增加名称："/></span>
                       <span> 宝贝前面加：<asp:TextBox ID="txtFirstAdd" runat="server" MaxLength="30"></asp:TextBox></span>
                        <span>	后面加：<asp:TextBox ID="txtEndAdd" runat="server" MaxLength="30"></asp:TextBox></span>
                        </div>
                    <div>
                       <span> <asp:RadioButton ID="rdoAll" runat="server" GroupName="modify" Text="全部更换："/>
                        全部更换为：<asp:TextBox ID="txtReplaceAll" runat="server" MaxLength="30"></asp:TextBox></span>
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
