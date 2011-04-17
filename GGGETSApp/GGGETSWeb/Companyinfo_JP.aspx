<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Companyinfo_JP.aspx.cs" Inherits="GGGETSWeb.Companyinfo_JP" %>

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
                    <asp:Label ID="lblHome" runat="server" Text="会社概要" /></LI>
              </UL></TD></TR></TBODY></TABLE>
    </H2>
    <DIV id="topBg">
    <TABLE border="0" cellSpacing="0" summary="company" cellPadding="0">
        <TBODY>
        <TR>
          <TD id="main">
            <DIV id="text_box">
            <H4>会社概要</H4>
            <!--内容填充区域-->
            <P>
            <table cellspacing="1" cellpadding="0" border="0" width="561" summary="会社情報" id="outline">
              <tbody><tr>
                <th width="198">会社名</th>
                            <td width="360">GETS株式会社 （GETS CO.,LTD.）</td>
               </tr>
               <tr>
                            <th class="style1">本社所在地</th>
                            <td class="style1">〒286－0048 千葉県成田市公津の杜2-34-4<br>
                                TEL: 04-7626-3898<br>
                                FAX: 04-7626-3898</td>
               </tr>
               <tr>
                <th>設立年月日</th>
                            <td>2010年</td>
               </tr>
               <tr>
                <th>営業開始日</th>
                            <td>2010年</td>
               </tr>
               <tr>
                <th>資本金</th>
                            <td>500万</td>
               </tr>
               <tr>
                <th class="style2">代表者</th>
                            <td class="style2">代表取締役社長　陳　聡</td>
               </tr>
               <tr>
                <th>役員</th>
                            <td>取締役　　大熊　辰</td>
               </tr>
               <tr>
                <th>業種</th>
                            <td>物流</td>
               </tr>
               <tr>
                <th>事業内容</th>
                            <td>（1）海上運送業、陸上運送業、航空運送業<br />
                                （2）国内外の宅配事業<br />
                                （3）通関業、倉庫業、貨物自動車運送事業<br />
                                （4）輸出入貿易業およびその代理業<br />
                                （5）コンピュータネットワーク、通信機器等のシステム開発、ソフトウエア開発及び保守作業</td>
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
