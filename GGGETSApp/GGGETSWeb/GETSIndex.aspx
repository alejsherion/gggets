<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GETSIndex.aspx.cs" Inherits="GGGETSWeb.GETSIndex" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/navi.js" type="text/javascript"></script>
    <script src="Scripts/swap.js" type="text/javascript"></script>
    <script src="Scripts/Ad.js" type="text/javascript"></script>
    <link href="Styles/e_common.css" rel="stylesheet" type="text/css" />
    <link href="Styles/e_under.css" rel="stylesheet" type="text/css" />
    <link href="Styles/index.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Ad.css" rel="stylesheet" type="text/css" />
    <script>
        function DynamicAd(path) {
            //debugger;
            //var frameObj = document.all.GGGETContent;
            var frameObj = document.getElementById("GGGETContent");
            if (path == "Home")
                frameObj.src = document.getElementById('<%= HFHome.ClientID %>').innerHTML;
            if (path == "Services")
                frameObj.src = document.getElementById('<%= HFServices.ClientID %>').innerHTML;
            if (path == "Casestudy")
                frameObj.src = document.getElementById('<%= HFCaseStudy.ClientID %>').innerHTML;
            if (path == "Network")
                frameObj.src = document.getElementById('<%= HFNetwork.ClientID %>').innerHTML;
            if (path == "CompanyInfo")
                frameObj.src = document.getElementById('<%= HFCompanyInfo.ClientID %>').innerHTML;

//            if (path.indexOf("Home") >= 0)
//                $("#main_image").fadeIn(2000); 
//            else
//                $("#main_image").fadeOut(2000);
               if (path.indexOf("Home") == -1)
                    $("#main_image").hide(); 

        }

        //根据国家化绑定超链标签
        function BindInitUrl() {
            //debugger;
            var frameObj = document.getElementById("GGGETContent");
            frameObj.src = document.getElementById('<%= HFHome.ClientID %>').innerHTML;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <DIV id="head_bg">
    <TABLE id="TOP" border="0" cellSpacing="0" summary="header" cellPadding="0">
  <TBODY>
  <TR>
    <TD id="head_left">
      <H1><A href="#"><IMG border="0" alt="CHINA INTERNATIONAL GETS CO." src="Images/e_head_logo.jpg" width="343" height="31"></A></H1></TD>
    <TD id="head_right"><!-- InstanceBeginEditable name="headsubmenu" -->
      <UL>
        <LI>
        <img src="images/cn.png">
        <asp:LinkButton ID="lbCN" runat="server" meta:resourcekey="lbCNResource1" 
                OnClick="lbCN_Click" Text="中国"></asp:LinkButton>| 
        </LI>
        <LI>
        <img src="images/jp.png">
        <asp:LinkButton ID="lbJP" runat="server" meta:resourcekey="lbJPResource1" 
                OnClick="lbJP_Click" Text="日本"></asp:LinkButton> |
        </LI>
        <LI>
        <img src="images/us.png">
        <asp:LinkButton ID="lbUSA" runat="server" meta:resourcekey="lbUSAResource1" 
                OnClick="lbUSA_Click" Text="美国"></asp:LinkButton>
        </LI></UL><!-- InstanceEndEditable --></TD></TR></TBODY></TABLE></DIV>

        <DIV id="global_bg">
        <TABLE border="0" cellSpacing="0" summary="globalNavi" cellPadding="0">
          <TBODY>
          <TR>
            <TD>
              <UL>
                <LI class="global_margin" title="HOME">
                <%--<IMG id="Image9" border="0" name="Image9" alt="HOME" src="Images/e_n_home.jpg" width="155" height="33">--%>
                <p onclick="DynamicAd('Home')" onmouseover="this.style.color='#FFFFFF'" onmouseout="this.style.color='#fdcb0a'" style="width:140px ; cursor:pointer; background:#FB0000; border-color: #F55000; color:#fdcb0a; border-style: solid; border-width: 1px; font-size: 0.9em; padding: 5px; float:left">
                    <asp:Label ID="lblHome" runat="server" Text="首页" 
                        meta:resourcekey="lblHomeResource1" /></p></LI>
                <LI class="global_margin" title="guide">
                <%--<IMG id="Image10" border="0" name="Image10" alt="サービスガイド" src="Images/e_n_services.jpg" width="155" height="33">--%>
                <p onclick="DynamicAd('Services')" onmouseover="this.style.color='#FFFFFF'" onmouseout="this.style.color='#fdcb0a'" style="width:140px ; cursor:pointer; background:#FB0000; border-color: #F55000; color:#fdcb0a; border-style: solid; border-width: 1px; font-size: 0.9em; padding: 5px; float:left">
                    <asp:Label ID="lblServices" runat="server" Text="服务" 
                        meta:resourcekey="lblServicesResource1" /></p></LI>
                <LI class="global_margin" title="cases">
                <%--<IMG id="Image11" border="0" name="Image11" alt="cases" src="Images/e_n_casestudy.jpg" width="155" height="33">--%>
                <p onclick="DynamicAd('Casestudy')" onmouseover="this.style.color='#FFFFFF'" onmouseout="this.style.color='#fdcb0a'" style="width:145px ; cursor:pointer; background:#FB0000; border-color: #F55000; color:#fdcb0a; border-style: solid; border-width: 1px; font-size: 0.9em; padding: 5px; float:left">
                    <asp:Label ID="lblCaseStudy" runat="server" Text="服务费税表" 
                        meta:resourcekey="lblCaseStudyResource1" /></p></LI>
                <LI class="global_margin" title="network">
                <%--<IMG id="Image12" border="0" name="Image12" alt="network" src="Images/e_n_network.jpg" width="155" height="33">--%>
                <p onclick="DynamicAd('Network')" onmouseover="this.style.color='#FFFFFF'" onmouseout="this.style.color='#fdcb0a'" style="width:145px ; cursor:pointer; background:#FB0000; border-color: #F55000; color:#fdcb0a; border-style: solid; border-width: 1px; font-size: 0.9em; padding: 5px; float:left">
                    <asp:Label ID="lblNetwork" runat="server" Text="营业网络" 
                        meta:resourcekey="lblNetworkResource1" /></p></LI>
                <LI class="global_margin" title="company">
                <%--<IMG border=0 name=Image13 alt=company src="Images/e_n_company.jpg" width=155 height=33>--%>
                <p onclick="DynamicAd('CompanyInfo')" onmouseover="this.style.color='#FFFFFF'" onmouseout="this.style.color='#fdcb0a'" style="width:145px ; cursor:pointer; background:#FB0000; border-color: #F55000; color:#fdcb0a; border-style: solid; border-width: 1px; font-size: 0.9em; padding: 5px; float:left">
                    <asp:Label ID="lblCompany" runat="server" Text="关于我们" 
                        meta:resourcekey="lblCompanyResource1" /></p></LI></UL></TD></TR></TBODY></TABLE><DIV></DIV></DIV>
        <!--Flash-->
        <%--<table id="main_image" summary="マインイメージ" border="0" cellpadding="0" cellspacing="0">
        <tbody><tr>
        <td>
        <script src="Scripts/swfobject.js" type="text/javascript"></script>
        <div id="mainFlash"><embed type="application/x-shockwave-flash" src="Images/top.swf" id="main" name="main" bgcolor="#FFFFFF" quality="high" flashvars="flashVarText=this is passed in via FlashVars for example only" height="260" width="800"></div>
        <script type="text/javascript">
        // <![CDATA[
            var so = new SWFObject("Images/top.swf", "main", "800", "260", "6", "#FFFFFF");
            so.addVariable("flashVarText", "this is passed in via FlashVars for example only");
            so.write("mainFlash");
        // ]]>
        </script>
        </td>
        </tr>
        </tbody></table>--%>
        <!--广告-->
        <table id="main_image" summary="マインイメージ" border="0" cellpadding="0" cellspacing="0">
        <tbody><tr>
        <td>
        <div id="mainFlash">
        <div class="vouchimg relative">
			<a title="广告名01" target="_blank" id="picswitch" href="#"><img src="Images/ad/ad01.jpg" width="800px" height="260px" alt="" /></a>
			<div class="picswitch absolute" style="display:none;">
                <!-- a标签的title和href需要动态绑定 -->
			    <a title="广告名01" target="_blank" href="#"><span id="0" picurl="Images/ad/ad01.jpg" class="choose">广告名01</span></a>
                <a title="广告名02" target="_blank" href="#"><span id="1" picurl="Images/ad/ad02.jpg" class="unchoose">广告名02</span></a>
			    <a title="广告名03" target="_blank" href="#"><span id="2" picurl="Images/ad/ad03.jpg" class="unchoose">广告名03</span></a>
                <a title="广告名04" target="_blank" href="#"><span id="3" picurl="Images/ad/ad04.jpg" class="unchoose">广告名04</span></a>
			    <a title="广告名05" target="_blank" href="#"><span id="4" picurl="Images/ad/ad05.jpg" class="unchoose">广告名05</span></a>
		    </div>
        </div>
        </div>
        </td>
        </tr>
        </tbody></table>
        <!--填充内容-->
        <iframe id="GGGETContent" width="100%" height="600" frameborder="0" scrolling="no" title="Content" src=Home.aspx></iframe>
        <DIV id="foot">
        <TABLE id="foot_navi" border="0" cellSpacing="0" summary="footerNavi" cellPadding="0">
          <TBODY>
          <TR>
            <TD>
              <UL>
                <LI><A href="#">
                    <asp:Label ID="lblPolicy" runat="server" Text="政策" 
                        meta:resourcekey="lblPolicyResource1" /></A>　|</LI>
                <LI><A href="#">
                    <asp:Label ID="lblNotice" runat="server" Text="通知" 
                        meta:resourcekey="lblNoticeResource1" /></A></LI></UL></TD></TR></TBODY></TABLE>
        <TABLE id="foot_copy" border="0" cellSpacing="0" summary="Copyright" cellPadding="0">
          <TBODY>
          <TR>
            <TD><A href="#" target="_blank">
            <IMG border="0" alt="CHINA INTERNATIONAL GETS CO" src="Images/e_foot_logo.jpg" width="167" height="14"></A></TD>
            <TD id="copy_right">
            <p style="font-size: xx-small; color: #808080">Copyright(C) 2011 CHINA INTERNATIONAL GETS CO.,LTD. All rights reserved.</p>
            </TD></TR></TBODY></TABLE>
    </DIV>
    <!--Tips-->
    <div style="display:none;">
    <asp:Label ID="HFHome" runat="server" Text="Companyinfo.aspx" 
        meta:resourcekey="HFHomeResource1"></asp:Label>
    <asp:Label ID="HFServices" runat="server" Text="Services.aspx" 
        meta:resourcekey="HFServicesResource1"></asp:Label>
    <asp:Label ID="HFCaseStudy" runat="server" Text="Casestudy.aspx" 
        meta:resourcekey="HFCaseStudyResource1"></asp:Label>
    <asp:Label ID="HFNetwork" runat="server" Text="Network.aspx" 
        meta:resourcekey="HFNetworkResource1"></asp:Label>
    <asp:Label ID="HFCompanyInfo" runat="server" Text="Companyinfo.aspx" 
        meta:resourcekey="HFCompanyInfoResource1"></asp:Label></div>
    </form>
</body>
</html>
