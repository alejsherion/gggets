<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HAWBdemand.ascx.cs"
    Inherits="GGGETSAdmin.Control.logistics.HAWBdemand" %>
<div>
    <table class="DataView">
        <tbody>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_BarCode" runat="server" Text="运单号:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_BarCode" runat="server" Width="250"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Destination" runat="server" Text="目的地三字码:"></asp:Label>
                </td>
                <td style="width: 80px">
                    <asp:TextBox ID="txt_Destination" runat="server" Width="50"></asp:TextBox>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txt_UpCreateTime" runat="server" Width="80px"></asp:TextBox>-
                    <asp:TextBox ID="txt_ToCreateTime" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_Demand" runat="server" Text="查 询" CssClass="InputBtn" />
                </td>
            </tr>
            <tr class="Row">
                <td colspan="4">
                    <asp:GridView ID="gv_Box" runat="server" AutoGenerateColumns="False" CssClass="DataView">
                        <Columns>
                            <asp:TemplateField HeaderText="运单号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_BarCode" runat="server" Width="220" Text='<%# Eval("BarCode") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="创建时间">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_CreateTime" runat="server" Text='<%# Eval("bbbbb") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate><asp:CheckBox ID="Ck_Sum" runat="server" /></HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Ck" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader" align="right" colspan="4">
                    <asp:Button ID="btn_Close" runat="server" Text="删 除" CssClass="InputBtn" /></td>
            </tr>
        </tbody>
    </table>
</div>
