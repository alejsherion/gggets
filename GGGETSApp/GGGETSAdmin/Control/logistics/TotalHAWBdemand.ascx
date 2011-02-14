<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TotalHAWBdemand.ascx.cs" Inherits="GGGETSAdmin.Control.logistics.TotalHAWBdemand" %>
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
                <td>
                    <asp:Label ID="lbl_UpCity" runat="server" Text="起飞城市三字码:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_UpCity" runat="server" Width="90"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lbl_ToCity" runat="server" Text="到达城市三字码:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_ToCity" runat="server" Width="90"></asp:TextBox>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txt_UpCreateTime" runat="server" Width="80px"></asp:TextBox>-
                    <asp:TextBox ID="txt_ToCreateTime" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_UpdateTime" runat="server" Text="修改时间:"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txt_UpdateTime" runat="server" Width="80px"></asp:TextBox>-
                    <asp:TextBox ID="txt_ToUpdateTime" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_Demand" runat="server" Text="查 询" CssClass="InputBtn" />
                </td>
            </tr>
        </tbody>
    </table>
    <div>
        <asp:GridView ID="Gv_HAWB" runat="server" AutoGenerateColumns="False" CssClass="DataView">
            <Columns>
                <asp:TemplateField HeaderText="总运单号">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_SumHAWBNo" runat="server" Text='<%# Eval("aaaa") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Width="220" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="起飞城市三字码">
                    <ItemTemplate>
                        <asp:Label ID="lbl_UpCity" runat="server" Text='<%# Eval("bbbbb") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="到达城市三字码">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ToCity" runat="server" Text='<%# Eval("bbbbb") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建时间">
                    <ItemTemplate>
                        <asp:Label ID="lbl_CreateTime" runat="server" Text='<%# Eval("bbbbb") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="重量">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Weight" runat="server" Text='<%# Eval("bbbbb") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("bbbbb") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="Row">
            <asp:Button ID="btn_Close" runat="server" Text="删 除" CssClass="InputBtn" /></td>
        </div>
    </div>
</div>