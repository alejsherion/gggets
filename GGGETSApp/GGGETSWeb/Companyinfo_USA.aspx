<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Companyinfo_USA.aspx.cs" Inherits="GGGETSWeb.Companyinfo_USA" %>

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
                    <asp:Label ID="lblHome" runat="server" Text="COMPANY INFO" /></LI>
              </UL></TD></TR></TBODY></TABLE>
    </H2>
    <DIV id="topBg">
    <TABLE border="0" cellSpacing="0" summary="company" cellPadding="0">
        <TBODY>
        <TR>
          <TD id="main">
            <DIV id="text_box">
            <H3><IMG alt="Careful handling like a mother cat carries her kitty" src="Images/flash_e_company.jpg" width="640" height="208"></H3>
            <H4>Company Info</H4>
            <!--内容填充区域-->
            <P>
            <table cellspacing="1" cellpadding="0" border="0" width="561" summary="会社情報" id="outline">
              <tbody><tr>
                <th width="198">Company Name</th>
                            <td width="360">Yamato Global Logistics Japan Co., Ltd.</td>
               </tr>
               <tr>
                            <th height="82">Head Office</th>
                            <td><p>New River Bldg., 6th Fl, 1-10-14, Shinkawa, Chuo-ku, Tokyo 
                                104-0034 Japan<br>
                              TEL: 03-5542-8600<br>
                              TEL: 03-5542-8653<br>
                  
                              </p></td>
               </tr>
               <tr>
                <th>Date of Establishment</th>
                            <td>17-Nov-99</td>
               </tr>
               <tr>
                <th>Office Opening Date</th>
                            <td>1-Apr-00</td>
               </tr>
               <tr>
                <th>Capital</th>
                            <td>¥1.88　billion</td>
               </tr>
               <tr>
                <th>Representative</th>
                            <td>Shinichi Tsukamoto, President &amp; Chief Executive Officer<p></p></td>
               </tr>
               <tr>
                <th>Officers</th>
                            <td>Takashi Isobe, Managing Excutive Officer <br>
                              Masaki Yamauchi, Director <br>
                              Atsushi Ichino, Director<br>
                              Kiyoshi Okatani, Auditor<br>
                              Shiro Sasaki, Auditor<br>
                              Matsumaru Hiroshi, Auditor<p></p></td>
               </tr>
               <tr>
                <th>Offices</th>
                            <td>32 in Japan, 7 in Overseas (As of April 2010)</td>
               </tr>
               <tr>
                <th>Employees</th>
                            <td>559 (As of April 2010)</td>
               </tr>
               <tr>
                <th>Vehicles</th>
                            <td>114 (As of April 2010)</td>
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
