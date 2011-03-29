<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompanyManagemnet.aspx.cs" Inherits="GGGETSAdmin.PersonnelManage.CompanyManage.CompanyManagemnet" Theme="logisitc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="DataView">
            <tbody>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_CompanyCode" runat="server" Text="公司账号:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_CompanyCode" runat="server" TabIndex="1" 
                            CssClass="TextBox" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_FullName" runat="server" Text="公司全称:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_FullName" runat="server" CssClass="TextBox" TabIndex="2" Width="300" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_ShortName" runat="server" Text="公司简称:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_ShortName" runat="server" CssClass="TextBox" TabIndex="3" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Contactor" runat="server" Text="联系人:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Contactor" runat="server" CssClass="TextBox" TabIndex="4" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_ContactorPhone" runat="server" Text="联系电话:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_ContactorPhone" runat="server" CssClass="TextBox" TabIndex="5"></asp:TextBox> 
                    </td>
                    <td class="FieldHeader" colspan="2">
                        <asp:Button ID="btn_Demand" runat="server" Text="查 询" CssClass="InputBtn" 
                            onclick="btn_Demand_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="height: 350px;overflow-x:auto;overflow-y:auto;">
        <asp:GridView ID="gv_Company" runat="server" AutoGenerateColumns="False" 
            onrowcommand="gv_Company_RowCommand" DataKeyNames="CompanyCode" 
                PageSize="2000" ondatabound="gv_Company_DataBound">
            <Columns>
                <asp:TemplateField HeaderText="公司账号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_CompanyCode" runat="server" Text='<%# Eval("CompanyCode") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="公司全称">
                    <ItemTemplate>
                        <asp:Label ID="lbl_FullName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="300px" />
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="公司简称">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ShortName" runat="server" Text='<%# Eval("ShortName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="150px" />
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="联系人">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Contactor" runat="server" Text='<%# Eval("Contactor") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="联系电话">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ContactorPhone" runat="server" Text='<%# Eval("ContactorPhone") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Eidt" CommandName="Eidt" runat="server" Text="详细" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Updata" CommandName="Updata" runat="server" Text="修改" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Delete" CommandName="Del" runat="server" Text="删除" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" OnClientClick="javascript:return confirm('确定删除该账号吗?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
    </div>
</asp:Content>
