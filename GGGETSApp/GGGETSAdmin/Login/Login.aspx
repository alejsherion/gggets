<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GGGETSAdmin.Login.Login" culture="auto" meta:resourcekey="PageResource2" uiculture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>国际快递</title>
    <style type="text/css">
    body{
	    margin:0;
	    padding:0;
	    background:#1d7ecb;
    }
    input{
	    width:186px;
	    line-height:25px;
	    border:1px solid #4287cc;
	    font-size:14px;
    }
    .page{
	    margin-left:auto;
	    margin-right:auto;
	    width:1004px;
	    height:710px;
	    position:relative;
    }
    .input1{
	    position:absolute;
	    left: 456px;
	    top: 275px;
		height:25px;
		
		
    }
    .lable1{
	    position:absolute;
	    left: 395px;
	    top: 280px;
		height:25px;
		color:White;
		font-size:14px;
    }
    .lable2{
	    position:absolute;
	    left: 400px;
	    top: 319px;
		height:25px;
		color:White;
		font-size:14px;
    }
    .input2{
	    position:absolute;
	    left: 456px;
	    top: 319px;
		height:25px;
    }
    .input3{
	    position:absolute;
	    left: 456px;
	    top: 363px;
		height:25px;
    }
    .butt{
	    position:absolute;
	    width:92px;
	    height:31px;
	    border:0;
	    left: 463px;
	    top: 369px;
    }
    .lab1{
	    position:absolute;
	    width:120px;
	    height:25px;
	    left: 557px;
	    top: 375px;
    }
       .val1{
	    position:absolute;
	    left: 563px;
	    top: 377px;
		height:25px;
		font-size:12px;
		color:Red;
    }
     .imageClass
        {
            padding-top: 10px;
            padding-bottom: 10px;
        }
    </style>
</head>
<body>
  <div class="page" style="">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <asp:Label ID="lbuser" runat="server" Text="登录名：" CssClass="lable1"></asp:Label>
    <asp:TextBox ID="txtuser" runat="server" class="input1" 
        meta:resourcekey="txtuserResource2"></asp:TextBox>
    <asp:RequiredFieldValidator
        ID="valruser" runat="server" ErrorMessage="请填写登录名" 
        ControlToValidate="txtuser" Display="None" meta:resourcekey="valruserResource2"></asp:RequiredFieldValidator>
    <asp:Label ID="lbpwd" runat="server" Text="密码：" CssClass="lable2"></asp:Label>
    <asp:TextBox ID="txtpwd"  runat="server" class="input2" TextMode="Password" 
        meta:resourcekey="txtpwdResource2"></asp:TextBox>
    <asp:RequiredFieldValidator
        ID="valrpwd" runat="server" ErrorMessage="请填写密码"  
        ControlToValidate="txtpwd"  Display="None" meta:resourcekey="valrpwdResource2"></asp:RequiredFieldValidator>
      <%--  <telerik:RadCaptcha ID="RadCaptcha1" runat="server" ErrorMessage="验证失败" CssClass="input3">
    <CaptchaImage ImageCssClass="imageClass" />
    </telerik:RadCaptcha>
     <asp:TextBox ID="txtRadCaptcha"  runat="server" class="input3" MaxLength="5"></asp:TextBox>--%>
    <asp:Button ID="Btnlogin" runat="server" OnClick="BtnLoginClick" class="butt" meta:resourcekey="BtnloginResource2" Text="登  录"/>
         <asp:Label ID="labError" runat="server"
             Text="" Font-Size="12px" ForeColor="Red"  class="lab1"></asp:Label>
    <asp:ValidationSummary ID="valsError" runat="server"  CssClass="val1" ShowMessageBox="True"
ShowSummary="False" meta:resourcekey="valsErrorResource1"/>  
    
       
    
        
    </form>
    </div>
</body>
</html>
