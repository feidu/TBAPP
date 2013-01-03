<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopUC.ascx.cs" Inherits="TaobaoShop.Pages.Controls.TopUC" %>
    
<div id="top" style="text-align:center; margin: 0px auto;background-image:url(../../Image/Top/di.png);height:126px;">
    <div id="topBar" style="width:940px;text-align:center; margin: 0px auto;">hi,<asp:Label ID="lblNick" runat="server" Text=""></asp:Label>! 欢迎使用聚团购~
        <a style="font-family:宋体; font-size:12px; color: #C00; font-weight: bold;">~ 2012春季促销活动火爆进行中~ 各种促销手段琳琅满目~ 亲们~你们行动了吗？</a>
    </div>
    <div style="width:940px;text-align:center; margin: 0px auto;height:20px;"></div>
    <div style=" background-image:url(../../Image/Top/di2.png);width:940px;height:64px;text-align:center; margin: 0px auto;" >
        <div style="text-align:left;width:174px; height:56px;float:left; vertical-align:middle;"></div>
        <div style="text-align:left;width:70px; height:56px;float:left;margin-left:16px; vertical-align:middle;"></div>
        <div style="text-align:left;width:640px; height:56px;float:left;padding-left:8px;padding-right:8px; vertical-align:middle;">
            <div id="up" style="margin-top:8px;">
                <ul>
                    <li>
                        当前版本：<asp:Label ID="lblversion" runat="server" Text="v1.0.0.0"></asp:Label>
                    </li>
                    <li>使用至 <asp:Label ID="lblEndTime" runat="server" Text=""></asp:Label> 到期</li>
                    <li>距离到期还有<asp:Label ID="lblhaveday" runat="server" Text=""></asp:Label> 天</li>
                    <li>
                        <a href="#" target="_blank">延长时间</a>
                    </li>
                    <li>
                        <asp:LinkButton ID="linkbtnExit" runat="server" onclick="linkbtnExit_Click">退出</asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div id="down" style="margin-top:32px;clear:left;">
                <ul>
                    <li><a href="../Main/Main.aspx" target="_self">首页</a></li>
                    <li>|</li>
                    <li><a href="#" target="_blank">升级会员</a></li>
                    <li>|</li>
                    <li><a href="http://my.taobao.com/" target="_blank">我的淘宝</a></li>
                    <li>|</li>
                    <li><a href="http://mai.taobao.com/" target="_blank">卖家中心</a></li>
                    <li>|</li>
                    <li><a href="http://store.taobao.com/page/design.htm" target="_blank">店铺装修</a></li>
                    <li>|</li>
                    <li><a href="#" target="_blank">常见问题</a></li>
                    <li>|</li>
                    <li>保存桌面图标</li>
                    <li>|</li>
                    <li><asp:HyperLink ID="hlAuth" runat="server">授权</asp:HyperLink>  </li>
                     
                </ul>
            </div>
        </div>
    </div>
</div>

<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.7.js"></script>
    <script src="../../fancybox/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>
    <script src="../../fancybox/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <link media="screen" href="../../fancybox/jquery.fancybox-1.3.4.css" type="text/css" rel="stylesheet"/>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#reAuth").fancybox({
                'width': 710,
                'height':300,
                'zoomSpeedIn': 300, 
		        'zoomSpeedOut': 300, 
                'type': 'iframe'
            });
            //$("#reAuth").trigger('click');
        });
    </script>
    <a style="display:none;" id="reAuth" href="../../TimeOut.aspx" >授权</a>   

