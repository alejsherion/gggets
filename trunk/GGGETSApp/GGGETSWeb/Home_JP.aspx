<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home_JP.aspx.cs" Inherits="GGGETSWeb.Home_JP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>首页</title>
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
                    <asp:Label ID="lblHome" runat="server" Text="ホーム" /></LI>
              </UL></TD></TR></TBODY></TABLE>
    </H2>
    <DIV id="topBg">
    <TABLE border="0" cellSpacing="0" summary="company" cellPadding="0">
        <TBODY>
        <TR>
          <TD id="main">
            <DIV id="text_box">
            <H3><IMG alt="Careful handling like a mother cat carries her kitty" src="Images/flash_e_top.jpg" width="640" height="208"></H3>
            <H4>会社理念</H4>
            <P>GETS先進的なグローバルネットワークに基づき、安全、正確、スピード、　そして生命ためのサービスを考え、さまざまな取引先に提供するための中日速達サービスである、もっと幅広く共に利益を獲得を追求する</P>
            <H4>会社目标</H4>
            <P>新基準造り 光り輝く</P>
              </DIV><!-- goodsBox end --></TD><!-- right navi start --></TR></TBODY></TABLE>
    </DIV>
    </TD>
    </TR>
    </TBODY>
    </TABLE>
    </form>
</body>
</html>
