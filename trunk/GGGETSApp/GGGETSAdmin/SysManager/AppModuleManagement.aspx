<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppModuleManagement.aspx.cs" Inherits="GGGETSAdmin.SysManager.AppModuleManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模块管理</title>
      <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="DataView" width="89%" id="tbCondtion" runat="server">
            <tbody>
                <tr class="AlternatingRow">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_Name" runat="server" Text="描述："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" CssClass="TextBox" runat="server" Width="120px" 
                            MaxLength="200"></asp:TextBox>
                    </td>
                    
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartCreateTime" runat="server" CssClass="TextBox" TabIndex="2" onClick="WdatePicker()" Width="90"></asp:TextBox>-
                        <asp:TextBox ID="txtEndCreateTime" runat="server" TabIndex="3" 
                            CssClass="TextBox" onClick="WdatePicker()" Width="90"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td class="FieldHeader">
                        <asp:Label ID="lbl_RoleType" runat="server" Text="状态:"></asp:Label>
                    </td>
                    <td>
                         <asp:DropDownList ID="rbtnStatus" runat="server">
                          
                        </asp:DropDownList>
                       
                    </td>
                    <td class="FieldHeader">
                       
                    </td>
                    <td>
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   
                        <asp:Button ID="btnQuery" runat="server" Text="查 询" CssClass="InputBtn" onclick="btnQuery_Click" 
                            />&nbsp;<asp:Button ID="btnAdd" runat="server" Text="添 加" 
                            onclick="btnAdd_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:GridView ID="dgrdRole" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="ModuleID" onrowcommand="dgrdRole_RowCommand" AllowPaging="true" 
            PageSize="20" onpageindexchanging="dgrdRole_PageIndexChanging" Width="98%">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="描述">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Description" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="120px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="备注" >
                    <ItemTemplate>
                        <asp:Label ID="lbtn_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建时间">
                    <ItemTemplate>
                        <asp:Label ID="lbl_CreateTime" runat="server" Text='<%# Eval("CreateTime") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" CommandName="Update" runat="server" Text="修改" CommandArgument='<%# Eval("ModuleID") %>'></asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="lbtn_Delete" CommandName="Del" runat="server" Text="删除" CommandArgument='<%# Eval("ModuleID") %>' OnClientClick="javascript:return confirm('确定删除该条记录吗?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
