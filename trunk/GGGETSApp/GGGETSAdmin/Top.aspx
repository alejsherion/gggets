<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="GGGETSAdmin.Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>首页顶部</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 0px; margin: 0px; background-image: url('Images/banner-r.gif'); background-repeat: no-repeat; height: 58px; width: 100%;" 
        align="right">
        <div style="padding-top: 28px;">
            <asp:Label ID="lblLoginName" runat="server" Font-Names="微软雅黑" Font-Size="Small" Text="用户名:"></asp:Label>
            <asp:Label ID="lblLoginInfo" runat="server" Font-Names="微软雅黑" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
            <asp:Label ID="lblRole" runat="server" Font-Names="微软雅黑" Font-Size="Small" Text="当前角色:"></asp:Label>
            <asp:Label ID="lblRoleInfo" runat="server" Font-Names="微软雅黑" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
        </div>
    </div>
    </form>
</body>
</html>
