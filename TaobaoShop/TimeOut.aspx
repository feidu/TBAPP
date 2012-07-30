<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeOut.aspx.cs" Inherits="TaobaoShop.TimeOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="fancybox-content" style="border-width: 10px; width: 700px; height: auto;">
            <div style="width: auto; height: auto; overflow: auto; position: relative;"><div style="width:700px;height:270px;overflow:auto;" id="authLink"><table cellspacing="0" cellpadding="0" border="0" align="center" width="652"><tbody><tr><td bgcolor="#EFEFEF" align="center" valign="middle" height="250"><table cellspacing="0" cellpadding="0" border="0" width="650"><tbody><tr><td bgcolor="#FFFFFF" align="center" valign="middle" height="248"><table cellspacing="0" cellpadding="0" border="0" width="650"><tbody><tr><td bgcolor="#EFEFEF" align="right" valign="middle" height="44"><table cellspacing="0" cellpadding="0" border="0" width="650"><tbody><tr><td align="right" width="608" valign="middle" style="font-family:微软雅黑; font-size:24px; color: #FF8C00; font-weight: bold;">淘宝新规: 用户实行30分钟安全授权操作</td><td width="10"></td></tr></tbody></table></td></tr><tr><td bgcolor="#FFFFFF" align="right" valign="bottom" height="30"><table cellspacing="0" cellpadding="0" border="0" width="650"><tbody><tr><td align="right" width="608" valign="middle"><span style="font-family:微软雅黑; font-size:16px; color: #666; font-weight: bold;">当您的授权时间超过30分钟的时候，系统会自动提醒您再次进行“淘宝安全授权”</span></td><td width="10"></td></tr></tbody></table></td></tr><tr><td bgcolor="#FFFFFF" align="right" valign="middle" height="20"><table cellspacing="0" cellpadding="0" border="0" width="650"><tbody><tr><td align="right" width="608" valign="middle"><span style="font-family:&quot;微软雅黑&quot;; font-size:16px; color: #FF8A00; font-weight: bold;">请您再次“授权”方便进行活动操作</span></td><td width="10">&nbsp;</td></tr></tbody></table></td></tr><tr><td bgcolor="#FFFFFF" align="right" valign="middle" height="64"><table cellspacing="0" cellpadding="0" border="0" width="650"><tbody><tr><td align="right" width="608" valign="middle"><span style="font-family:&quot;微软雅黑&quot;; font-size:16px; color: #666; font-weight: bold;"><a href="http://container.api.taobao.com/container?appkey=12313669&amp;scope=promotion">
                <asp:HyperLink ID="hlAuth" runat="server" Target="_parent">授权</asp:HyperLink>
            </a></span></td><td width="10">&nbsp;</td></tr></tbody></table></td></tr><tr><td bgcolor="#EFEFEF" align="right" valign="middle" height="25"><table cellspacing="0" cellpadding="0" border="0" width="650"><tbody><tr><td align="right" width="608" style="font-family:宋体; font-size:12px; color: #999;"> 感谢您对淘宝网安全授权的支持!~</td><td align="right" width="10" valign="middle">&nbsp;</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></div></div>
        </div>
    </form>
</body>
</html>
