<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FLTNoDetails.ascx.cs"
    Inherits="GGGETSAdmin.Control.logistics.FLTNoDetails" %>
<div>
    <table class="DataView">
        <tbody>
            <tr class="Row">
                <td class="FieldHeader" align="left">
                    <asp:Label ID="lbl_FLTNo" runat="server" Text="航班号:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txt_FLTNo" runat="server" Width="250"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txt_CreateTime" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_UpdateTime" runat="server" Text="修改时间:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txt_UpdateTime" runat="server" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_AirportRegion" runat="server" Text="机场三字码:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_takeoff" runat="server" Width="50"></asp:TextBox>To
                    <asp:TextBox ID="txt_getto" runat="server" Width="50"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Pice" runat="server" Text="件数:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txt_Pice" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_TotalWeight" runat="server" Text="总重量:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Txt_TotalWeight" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_TotalVolume" runat="server" Text="总容积:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txt_TotalVolume" runat="server" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:GridView ID="Gv_HAWB" runat="server" AutoGenerateColumns="False" CssClass="DataView">
                        <Columns>
                            <asp:TemplateField HeaderText="行号">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Number" runat="server" Text='<%# Eval("bbbbb") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="总运单号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_SumHAWBNo" runat="server" Text='<%# Eval("aaaa") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Width="220" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="包号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_BagNo" runat="server" Text='<%# Eval("aaaa") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Width="220" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="重量">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Weight" runat="server" Text='<%# Eval("bbbbb") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Button ID="btn_Return" runat="server" Text="返 回" CssClass="InputBtn" />
                </td>
            </tr>
        </tbody>
    </table>
</div>
