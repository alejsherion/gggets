<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="regionManagement.aspx.cs" Inherits="GGGETSAdmin.RegionZiMaManage.regionManagement" Theme="logisitc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="">
        <table class="DataView">
            <tr class="Row">
                <td class="FieldHeader" style="width:150px">
                    <asp:Label ID="lbl_CountryCode" runat="server" Text="国家二字码:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Txt_CountryCode" runat="server" Width="70" TabIndex="1" 
                        style="text-transform:uppercase"></asp:TextBox>
                </td>
                <td class="FieldHeader" style="width:150px">
                    <asp:Label ID="lbl_RegionCode" runat="server" Text="地区三字码:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_RegionCode" runat="server" Width="70"
                        TabIndex="2" style="text-transform:uppercase"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_RegionName" runat="server" Text="地区名称:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_RegionName" runat="server" Width="100"
                        TabIndex="3" style="text-transform:uppercase"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_Demand" TabIndex="4" runat="server" Text="查 询" CssClass="InputBtn" OnClick="btn_Demand_Click" />
                </td>
             </tr>
        </table>
        <div style="height: 350px;overflow-x:auto;overflow-y:auto;">
        <asp:GridView ID="gv_Region" runat="server"  CssClass="DataView" AutoGenerateColumns="False"
            OnRowEditing="gv_Region_RowEditing" onrowupdating="gv_Region_RowUpdating" 
            DataKeyNames="ID" onrowcancelingedit="gv_Region_RowCancelingEdit" 
            onrowdeleting="gv_Region_RowDeleting" onrowcommand="gv_Region_RowCommand" 
                PageSize="5000">
            <Columns>
                <asp:TemplateField HeaderText="地区三字码">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RegionCode" runat="server" Text='<%# Eval("RegionCode1") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="Txt_RegionCode" runat="server" MaxLength="3" Text='<%# Eval("RegionCode1") %>' style="text-transform:uppercase"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="地区名称">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RegionName" runat="server" Text='<%# Eval("RegionName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="Txt_RegionName" runat="server" Text='<%# Eval("RegionName") %>' style="text-transform:uppercase"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" />
                <%--<asp:ButtonField CommandName="Delete" ButtonType="Link" Text="删除" />--%>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btn_Delete" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="删除" CommandArgument='<%# Eval("RegionCode1") %>' OnClientClick="return confirm('是否确认删除？');" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
    </div>
</asp:Content>
