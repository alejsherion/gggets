<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaxClearanceClear.aspx.cs" Inherits="GGGETSAdmin.CustomsClearance.TaxClearanceClear" Theme="logisitc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tax Clearance Operation</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table style="width: 100%;" class="DataView">
        <tr class="Row" height="40px">
            <td class="FieldHeader">
                <asp:Label ID="lblHAWBCode" runat="server" Text="运单号:"></asp:Label></td>
            <td>
                <asp:Label ID="lblHAWBBindCode" runat="server" Text=""></asp:Label></td>
            <td class="FieldHeader">
                <asp:Label ID="lblWeight" runat="server" Text="重量:"></asp:Label></td>
            <td>
                <asp:Label ID="lblBindWeight" runat="server" Text=""></asp:Label></td>
            <td class="FieldHeader">
                <asp:Label ID="lblStatus" runat="server" Text="状态:"></asp:Label></td>
            <td>
                <asp:Label ID="lblBindStatus" runat="server" Text=""></asp:Label></td>
            <td class="FieldHeader">
                <asp:Label ID="lblClearanceStatus" runat="server" Text="海关状态:"></asp:Label></td>
            <td>
                <asp:Label ID="lblBindClearanceStatus" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr class="Row" height="40px">
            <td class="FieldHeader">
                <asp:Label ID="lblExpense" runat="server" Text="费&nbsp;&nbsp;用:"></asp:Label></td>
            <td colspan="7">
                <asp:Label ID="lblBindExpense" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="Row">
            <td class="FieldHeader">
                <asp:Label ID="lblProject" runat="server" Text="项目:"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlProjects" runat="server">
                </asp:DropDownList>
            </td>
            <td class="FieldHeader">
                <asp:Label ID="lblCurrency" runat="server" Text="币种:"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlCurrency" runat="server">
                </asp:DropDownList>
            </td>
            <td class="FieldHeader">
                <asp:Label ID="lblMoney" runat="server" Text="金额:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtMoney" runat="server"></asp:TextBox>
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text="添加" onclick="btnAdd_Click" CssClass="InputBtn" />
            </td>
        </tr>
        <tr>
            <td colspan="8" align="center">
                <!--GRID-->
                <telerik:RadGrid ID="RGProjects" runat="server" AutoGenerateColumns="False" 
                    GridLines="None" onitemcommand="RGProjects_ItemCommand">
                <MasterTableView>
                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>

                <RowIndicatorColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>

                <ExpandCollapseColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="项目名">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="币种">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("CurrencyType") %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="金额">
                            <ItemTemplate>
                                <asp:Label ID="lblExprense" runat="server" Text='<%# Eval("Tax") %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="删除">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id") %>'>删除</asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr class="Row">
            <td colspan="8" align="center">
                <asp:Button ID="btnSave" runat="server" Text="保存" Visible="false" 
                    onclick="btnSave_Click" CssClass="InputBtn" />
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
