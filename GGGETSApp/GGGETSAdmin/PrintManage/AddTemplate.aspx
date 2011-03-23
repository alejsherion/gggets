<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTemplate.aspx.cs" Inherits="GGGETSAdmin.PrintManage.AddTemplate" Theme="logisitc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加模板</title>
    <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
     <table style="width: 100%;">
                <tr>
                    <td width="15%">模板编号：</td>
                    <td><asp:TextBox ID="txtTemplateCode" runat="server" Width="200px" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTemplateCode" runat="server" 
                            ErrorMessage="必填" ControlToValidate="txtTemplateCode" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                </tr>
                <tr>
                    <td>模板名：</td>
                    <td><asp:TextBox ID="txtName" runat="server" Width="200px" MaxLength="200"></asp:TextBox> 
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="必填" 
                            ControlToValidate="txtName" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                </tr>
                <tr>
                    <td>模板描述：</td>
                    <td><asp:TextBox ID="txtDesc" runat="server" Width="400px" MaxLength="380" 
                            TextMode="MultiLine"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td>打印方向:</td>
                    <td>
                        <asp:DropDownList ID="ddlPrintDirection" runat="server" Width="150px">
                         <asp:ListItem Value="0">由操作人员自行选择</asp:ListItem>
                         <asp:ListItem Value="1">纵向打印,固定纸张</asp:ListItem>
                         <asp:ListItem Value="2">横向打印,固定纸张</asp:ListItem>
                         <asp:ListItem Value="3">纵向,宽度固定,高度自由</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>纸张类别:</td>
                    <td>
                        <asp:RadioButtonList ID="rbltype" runat="server" 
                            RepeatDirection="Horizontal" AutoPostBack="True" 
                            onselectedindexchanged="rbltype_SelectedIndexChanged">
                            <asp:ListItem Text="固定纸张" Value="0" />
                            <asp:ListItem Text="自由纸张" Value="1"  Selected="True"/>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="trPagerWidth" runat="server">
                    <td>纸张宽度(0.1mm):</td>
                    <td><asp:TextBox ID="txtPagerWidth" runat="server" Width="40px"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender
                            ID="Custom" runat="server" FilterType="Numbers" 
                            TargetControlID="txtPagerWidth" InvalidChars=".">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr id="trPagerHeight" runat="server">
                    <td>纸张高度(0.1mm):</td>
                    <td>
                        <asp:TextBox ID="txtPagerHeight" runat="server" Width="40px"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender
                            ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" 
                            TargetControlID="txtPagerHeight" InvalidChars=".">
                        </cc1:FilteredTextBoxExtender>
                        </td>
                </tr>
                <tr runat="server" visible="false" id="trPaperType">
                    <td>纸张名:</td>
                    <td>
                        <asp:DropDownList ID="ddlPaperType" runat="server" Width="150px">
                            
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr >
                    <td>对应主表：</td>
                    <td>
                        <asp:DropDownList ID="ddlCorrespondingTable" runat="server" Width="150px">
                            
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAdd" runat="server" Text="新 增" onclick="btnAdd_Click" CssClass="InputBtn"/>
                        <asp:Button ID="Button1" runat="server" Text="返回上一级目录" CssClass="InputBtn" 
                            onclick="Button1_Click" CausesValidation="false" /></td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
