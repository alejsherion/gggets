<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DepartmentManagemnet.aspx.cs" Inherits="GGGETSAdmin.PersonnelManage.DepartmentManage.DepartmentManagemnet" Theme="logisitc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="DataView">
            <tbody>
                <tr class="AlternatingRow">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_CompanyCode" runat="server" Text="公司账号:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_CompanyCode" runat="server" TabIndex="1" 
                            CssClass="TextBox" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DepCode" runat="server" Text="部门账号:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_DepCode" runat="server" CssClass="TextBox" TabIndex="2" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DepName" runat="server" Text="部门名称:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_DepName" runat="server" TabIndex="3" 
                            CssClass="TextBox" Style="text-transform: uppercase" ></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Button ID="btn_Demand" runat="server" Text="查 询" CssClass="InputBtn" 
                            onclick="btn_Demand_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="height: 350px;overflow-x:auto;overflow-y:auto;">
        <asp:GridView ID="gv_Depar" runat="server" AutoGenerateColumns="False" 
            onrowcommand="gv_Depar_RowCommand" PageSize="2000">
            <Columns>
                <asp:TemplateField HeaderText="公司账号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_CompanyCode" runat="server" Text='<%# Eval("CompanyCode") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="部门账号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_DepCode" runat="server" Text='<%# Eval("DepCode") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="部门名称">
                    <ItemTemplate>
                        <asp:Label ID="lbl_DepName" runat="server" Text='<%# Eval("DepName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="结算方式">
                    <ItemTemplate>
                        <asp:Label ID="lbl_SettleType" runat="server" Text='<%# Eval("SettleType").ToString().Replace("0","预付月结").Replace("1","预付现结").Replace("2","到付月结").Replace("3","到付现结") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="计重方式">
                    <ItemTemplate>
                        <asp:Label ID="lbl_WeightCalType" runat="server" Text='<%# Eval("WeightCalType").ToString().Replace("0","按照0.5KG标准").Replace("1","按照分段标准") %>'></asp:Label>
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
                        <asp:LinkButton ID="lbtn_Delete" CommandName="Del" runat="server" Text="删除" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" OnClientClick="javascript:return confirm('确定删除该条运单吗?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
    </div>
</asp:Content>
