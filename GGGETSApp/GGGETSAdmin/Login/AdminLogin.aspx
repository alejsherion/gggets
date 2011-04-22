<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="GGGETSAdmin.Login.AdminLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>国际快递</title>
    <link href="../Styles/login.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/login_admin.css" rel="stylesheet" type="text/css" />
</head>
<body class="bgimage" scroll="no">
    <form id="form1" runat="server">
    <div class="FirstDIV"></div>
		<div class="SecondDIV">
			<div class="login">
			    <ul>
			        <li><p>
                        <asp:Label ID="lblUserID" runat="server" Text="管理员账号：" /></p>
                        <asp:TextBox ID="txtuser" runat="server" CssClass="loginput" autocomplete="off" Width="182px" Height="22px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valruser" runat="server" ErrorMessage="请填写登录名" ControlToValidate="txtuser" Display="None"></asp:RequiredFieldValidator>
                    </li>
			        <li><p>
                        <asp:Label ID="lblPwd" runat="server" Text="管理员密码：" /></p>
                        <asp:TextBox ID="txtpwd"  runat="server" TextMode="Password" MaxLength="6" autocomplete="off" CssClass="loginput" Width="182px" Height="22px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valrpwd" runat="server" ErrorMessage="请填写密码" ControlToValidate="txtpwd"  Display="None"></asp:RequiredFieldValidator>
                    </li>
			        <li style="padding-left: 100px">
                        <asp:Button ID="Btnlogin" runat="server" OnClick="BtnLoginClick" Text="登  录" CssClass="logbutton" />
                        <asp:Label ID="labError" runat="server" Text="" Font-Size="12px" ForeColor="Red"  class="lab1"></asp:Label>
                        <asp:ValidationSummary ID="valsError" runat="server"  CssClass="val1" ShowMessageBox="True" ShowSummary="False" /> 
                    </li>
			    </ul>
			</div>
		</div>
    </form>
</body>
</html>
