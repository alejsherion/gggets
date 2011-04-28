<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="GGGETSAdmin.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>国际快递后台管理系统</title>
</head>
<frameset rows="58,*,50" frameborder="no" border="0" framespacing="0">
<frame src="Top.aspx" name="topFrame" scrolling="No" noresize="noresize" id="topFrame" title="topFrame" /><!--顶部-->

<frameset id="attachucp" framespacing="0" border="0" frameborder="no" cols="210,9,*" rows="*">
<frame scrolling="yes" noresize="" name="leftFrame" src="./Menu.aspx" /><!--左侧-->
<frame id="leftbar" scrolling="no" noresize="" name="switchFrame" src="Switch.aspx" /><!--中间-->
<frame scrolling="yes" noresize="" border="0" name="mainFrame" src="Right.aspx" /><!--内容-->
</frameset>

<frame src="Bottom.aspx" name="bottomFrame" scrolling="No" noresize="noresize" id="bottomFrame" title="bottomFrame" /><!--底部-->
</frameset>

<noframes>
<body>
</body>
</noframes>
</html>
