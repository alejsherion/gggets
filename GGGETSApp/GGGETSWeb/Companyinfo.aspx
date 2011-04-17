<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Companyinfo.aspx.cs" Inherits="GGGETSWeb.Companyinfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公司自介</title>
    <META content="text/html; charset=utf-8" http-equiv="Content-Type">
    <META content="ja" http-equiv="Content-Language">
    <META content="text/css" http-equiv="Content-Style-Type">
    <META content="text/javascript" http-equiv="Content-Script-Type"><!-- InstanceBeginEditable name="doctitle" --><!-- InstanceEndEditable -->
    <META name="Description" content="">
    <META name="robots" content="ALL"><!-- InstanceBeginEditable name="head" --><!-- InstanceEndEditable -->
    <link href="Styles/e_common.css" rel="stylesheet" type="text/css" />
    <link href="Styles/e_under.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
        .style2
        {
            height: 6px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <TABLE id="contents" border="0" cellSpacing="0" summary="contents" cellPadding="0">
    <TBODY>
    <TR>
    <TD><!-- InstanceBeginEditable name="Main" -->
    <H2>
        <%--<IMG alt="ENGLISH" src="Images/e_title_english.jpg" width="800" height="50">--%>
        <TABLE id="header_navi" border="0" cellSpacing="0" summary="footerNavi" cellPadding="0">
          <TBODY>
          <TR>
            <TD>
              <UL>
                <LI>
                    <asp:Label ID="lblHome" runat="server" Text="公司介绍" /></LI>
              </UL></TD></TR></TBODY></TABLE>
    </H2>
    <DIV id="topBg">
    <TABLE border="0" cellSpacing="0" summary="company" cellPadding="0">
        <TBODY>
        <TR>
          <TD id="main">
            <DIV id="text_box">
            <H4>公司信息</H4>
            <!--内容填充区域-->
            <P>
            <table cellspacing="1" cellpadding="0" border="0" width="561" summary="会社情報" id="outline">
              <tbody><tr>
                <th width="198">公司名称</th>
                            <td width="360">盖世 有限公司 （GETS CO.,LTD.）</td>
               </tr>
               <tr>
                            <th class="style1">总公司地址</th>
                            <td class="style1">〒286－0048 千葉県成田市公津の杜2-34-4<br>
                                TEL: 04-7626-3898<br>
                                FAX: 04-7626-3898</td>
               </tr>
               <tr>
                <th>设立年月日</th>
                            <td>2010年</td>
               </tr>
               <tr>
                <th>营业开始</th>
                            <td>2010年</td>
               </tr>
               <tr>
                <th>资本金</th>
                            <td>500万</td>
               </tr>
               <tr>
                <th class="style2">法人代表</th>
                            <td class="style2">代表取締役社長　陳　聡</td>
               </tr>
               <tr>
                <th>董事</th>
                            <td>取締役　　大熊　辰</td>
               </tr>
               <tr>
                <th>行业种类</th>
                            <td>物流</td>
               </tr>
               <tr>
                <th>营业范围</th>
                            <td>（1）海运，陆运，空运 
                                <br />
                                （2）国内外的快递业务<br />
                                （3）清关，仓库，货物车辆运输业 
                                <br />
                                （4）出入口贸易及其代理业务<br />
                                （5）网络，通信机器等系统开发，软件开发及维护</td>
               </tr>
              </tbody></table>
    <BR></P></DIV><!-- goodsBox end --></TD><!-- right navi start --></TR></TBODY></TABLE>
    </DIV>
    </TD>
    </TR>
    </TBODY>
    </TABLE>
    </form>
</body>
</html>
