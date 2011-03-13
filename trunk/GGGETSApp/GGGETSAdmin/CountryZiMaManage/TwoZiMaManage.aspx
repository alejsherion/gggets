<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="TwoZiMaManage.aspx.cs" Inherits="GGGETSAdmin.CountryZiMaManage.TwoZiMaManage"
    Theme="logisitc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="">
        <table class="DataView">
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_CountryCode" runat="server" Text="国家二字码:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Txt_CountryCode" runat="server" Width="80" TabIndex="1" Style="text-transform: uppercase"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_CountryName" runat="server" Text="国家全称:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_CountryName" runat="server" Width="80" TabIndex="2" Style="text-transform: uppercase"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_Demand" TabIndex="3" runat="server" Text="查 询" CssClass="InputBtn"
                        OnClick="btn_Demand_Click" />
                </td>
            </tr>
        </table>
        <div style="height: 350px; overflow-x: auto; overflow-y: auto;">
            <asp:GridView ID="gv_Country" runat="server" AutoGenerateColumns="False" OnRowEditing="gv_Country_RowEditing"
                OnRowUpdating="gv_Country_RowUpdating" DataKeyNames="ID" OnRowCancelingEdit="gv_Country_RowCancelingEdit"
                OnRowDeleting="gv_Country_RowDeleting" 
                OnRowCommand="gv_Country_RowCommand" PageSize="5000">
                <Columns>
                    <asp:TemplateField HeaderText="国家二字码">
                        <ItemTemplate>
                            <asp:Label ID="lbl_CountryCode" runat="server" Text='<%# Eval("CountryCode1") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_CountryCode" runat="server" MaxLength="2" Text='<%# Eval("CountryCode1") %>'
                                Style="text-transform: uppercase"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="国家全称">
                        <ItemTemplate>
                            <asp:Label ID="lbl_CountryName" runat="server" Text='<%# Eval("CountryName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_CountryName" runat="server" Text='<%# Eval("CountryName") %>'
                                Style="text-transform: uppercase"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                    <%--
                <asp:ButtonField CommandName="Delete" ButtonType="Link" Text="删除" />--%>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_Delete" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="删除" CommandArgument='<%# Eval("CountryCode1") %>' OnClientClick="return confirm('是否确认删除？');" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
