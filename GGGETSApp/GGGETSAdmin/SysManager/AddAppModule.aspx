<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAppModule.aspx.cs" Inherits="GGGETSAdmin.SysManager.AddAppModule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>管理模块</title>
    <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div align="center">
        <table class="DataView" width="98%">
            <thead>
                <tr class="Row">
                    <td class="FieldHeader" align="right" width="20%">
                       <asp:Label ID="lblIsLeft" runat="server" Text="页面类型："></asp:Label>
                    </td>
                    <td class="FieldHeader" align="left">
                        <asp:RadioButtonList ID="rbtnIsLeft" runat="server" 
                            RepeatDirection="Horizontal" 
                            onselectedindexchanged="rbtnIsLeft_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="父页面" Value="0" Selected="True"/>
                         <asp:ListItem Text="子页面" Value="1"  />
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr class="Row" runat="server" id="trParentDirectory" visible="false">
                    <td class="FieldHeader" align="right" width="20%" runat="server">
                         <asp:Label ID="Label1" runat="server" Text="父级页面："></asp:Label></td>
                    <td class="FieldHeader" align="left" runat="server">
                        <asp:DropDownList ID="dropParentId" runat="server">
                        </asp:DropDownList> </td>
                </tr>
                <tr class="Row" runat="server" id="trURl" visible="false">
                    <td class="FieldHeader" align="right" width="20%" runat="server">
                         <asp:Label ID="lblURL" runat="server" Text="访问地址："></asp:Label></td>
                    <td class="FieldHeader" align="left" runat="server">
                        <asp:TextBox ID="txtURL" runat="server" MaxLength="200" Width="300" style="text-transform:uppercase"></asp:TextBox>
                        
                        </td>
                </tr>
                 <tr class="Row" runat="server" id="trPrivilegeDesc" visible="false">
                    <td class="FieldHeader" align="right" width="20%">
                           <asp:Label ID="lblPrivilegeDesc" runat="server" Text="权限："></asp:Label></td>
                    <td class="FieldHeader" align="left">
                        <asp:CheckBoxList ID="chklPrivilegeDesc" runat="server" RepeatColumns="3" 
                            RepeatDirection="Horizontal">
                        </asp:CheckBoxList> </td>
                </tr>
                 <tr class="Row" runat="server" id="trRoleID" visible="false">
                    <td class="FieldHeader" align="right" width="20%">
                           角色:</td>
                    <td class="FieldHeader" align="left">
                          <asp:CheckBoxList ID="chkRoleID" runat="server" RepeatColumns="3" 
                              RepeatDirection="Horizontal">
                        </asp:CheckBoxList></td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right" width="20%">
                         <asp:Label ID="lblDescription" runat="server" Text="描述：" style="text-transform:uppercase"></asp:Label></td>
                    <td class="FieldHeader" align="left">
                        <asp:TextBox ID="txtDescription" runat="server" MaxLength="50" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valrDescription" runat="server" ErrorMessage="必填项" ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
                        </td>
                </tr>
               
                <tr class="Row">
                    <td class="FieldHeader" align="right" width="20%">
                         <asp:Label ID="lblRemark" runat="server" Text="备注：" style="text-transform:uppercase"></asp:Label></td>
                    <td class="FieldHeader" align="left">
                        <asp:TextBox ID="txtRemark" runat="server" Rows="5" TextMode="MultiLine" Width="300" MaxLength="200" style="text-transform:uppercase"></asp:TextBox></td>
                </tr>
                <tr align="center">
                    <td align="center" colspan="2">
                        &nbsp;
                            <asp:Button ID="btnAdd" runat="server" Text="设    置" CssClass="bluebuttoncss" 
                            Width="150px" onclick="btnAdd_Click"   />&nbsp;
                            <asp:Button ID="btnComeBack" runat="server" Text="返    回" CssClass="bluebuttoncss" 
                            Width="150px" onclick="btnComeBack_Click"   CausesValidation="false"/>
                            </td>
                </tr>           
            </thead>
        </table>
    </div>
    </form>
</body>
</html>
