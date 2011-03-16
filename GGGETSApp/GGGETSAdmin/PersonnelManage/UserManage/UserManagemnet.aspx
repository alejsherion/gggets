<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserManagemnet.aspx.cs" Inherits="GGGETSAdmin.PersonnelManage.UserManage.UserManagemnet" Theme="logisitc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../Scripts/calendar.js"></script>
    <div>
        <table class="DataView">
            <tbody>
                <tr class="AlternatingRow">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_LoginName" runat="server" Text="用户名:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_LoginName" runat="server" TabIndex="1" 
                            CssClass="TextBox" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_GetUpTime" runat="server" CssClass="TextBox" Width="100" TabIndex="2" onfocusin="calendar()"></asp:TextBox>
                    -
                        <asp:TextBox ID="Txt_StopTime" runat="server" TabIndex="3" 
                            CssClass="TextBox" onfocusin="calendar()" Width="100"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Button ID="btn_Demand" runat="server" Text="查 询" CssClass="InputBtn" 
                            onclick="btn_Demand_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="height: 350px;overflow-x:auto;overflow-y:auto;">
        <asp:GridView ID="gv_User" runat="server" AutoGenerateColumns="false" 
            onrowcommand="gv_User_RowCommand" PageSize="2000">
            <Columns>
                <asp:TemplateField HeaderText="用户名">
                    <ItemTemplate>
                        <asp:Label ID="lbl_LoginName" runat="server" Text='<%# Eval("LoginName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="真实姓名">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RealName" runat="server" Text='<%# Eval("RealName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="联系电话">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Phone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
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
