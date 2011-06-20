<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManager.aspx.cs" Inherits="GGGETSAdmin.Account.UserManager" Theme="logisitc" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管理用户</title>
    <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table class="DataView" width="98%" id="tbCondtion" runat="server">
        <%--    <thead>--%>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="Country" runat="server" Text="邮件："></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="txtEmail" runat="server" Width="120" CssClass="TextBox"></asp:TextBox>
                        <!-- 过滤，只能输入2个字母，其他特殊符号，中文，数字都过滤掉-->
                        <asp:RegularExpressionValidator ID="valeEmail" runat="server" 
                            ErrorMessage="邮件格式不对" ControlToValidate="txtEmail" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>  
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Region" runat="server" Text="联系电话："></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="txtTel" runat="server" Width="150" style="text-transform:uppercase" CssClass="TextBox"></asp:TextBox>
                        <!-- 过滤，只能输入3个字母，其他特殊符号，中文，数字都过滤掉-->
                       <asp:RegularExpressionValidator ID="valeTel" runat="server" 
                            ErrorMessage="电话格式不对" ControlToValidate="txtTel" 
                            ValidationExpression="((\d{11})|^(\d{2,4}[-_－—]?)?\d{3,8}([-_－—]?\d{3,8})?([-_－—]?\d{1,7})?$)|(^0?1[35]\d{9}$)"></asp:RegularExpressionValidator>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Account" runat="server" Text="登录名：" Width="100"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLoginName" runat="server" Width="120" TabIndex="2" 
                         style="text-transform:uppercase"  MaxLength="20" CssClass="TextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_HAWBType" runat="server" Text="会员状态:"></asp:Label>                       
                    </td>
                    <td align="left" style="width:80px">
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="150px">
                           
                        </asp:DropDownList>
                       </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间："></asp:Label>                        
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStartCreateTime" runat="server" Width="100" onClick="WdatePicker()" style="text-transform:uppercase" CssClass="TextBox"></asp:TextBox>-
                        <asp:TextBox ID="txtEndCreateTime" runat="server" Width="100" onClick="WdatePicker()" style="text-transform:uppercase" CssClass="TextBox"></asp:TextBox></td>
                    <td class="FieldHeader">
                        <asp:Button ID="btnQuery" runat="server" Text="查 询" CssClass="InputBtn" 
                            Width="50px" onclick="btnQueryClick"/>
                    </td>
                    <td class="FieldHeader">
                        <asp:Button ID="btnAdd" runat="server" Text="添 加" CssClass="InputBtn" 
                            Width="50px" onclick="btnAdd_Click" />
                    </td>
                </tr>         
            <%--</thead>--%>
        </table>
    </div>
    <div align="center">
        <asp:GridView ID="dgrdUsers" runat="server" AutoGenerateColumns="False" 
            Width="98%" onrowcommand="dgrdUsers_RowCommand" AllowPaging="true" 
            PageSize="20" onpageindexchanging="dgrdUsers_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                      <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="登录名">
                    <ItemTemplate>
                        <asp:Label ID="lblLoginName" runat="server" Text='<%# Eval("LoginName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="真实姓名">
                    <ItemTemplate>
                        <asp:Label ID="lblRealName" runat="server" Text='<%# Eval("RealName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="联系电话">
                    <ItemTemplate>
                        <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="电子邮件">
                    <ItemTemplate>
                        <asp:Label ID="lblHAWBType" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="会员状态">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# GetStatusByCode(Convert.ToString(Eval("Status"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建日期">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# Eval("CreateTime") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:Button ID="btnDel" runat="server" Text="删 除" CssClass="bluebuttoncss" CommandName="Del" CommandArgument='<%# Eval("UID") %>' OnClientClick="javascript:return confirm('确定删除该条记录吗?');"/>
                        <asp:Button ID="btnUpdate" runat="server" Text="修 改" CssClass="bluebuttoncss" CommandName="Update" CommandArgument='<%# Eval("UID") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
