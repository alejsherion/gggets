<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Boxdemand.ascx.cs" Inherits="GGGETSAdmin.Control.logistics.Boxdemand" %>
<div>
    <table class="DataView">
        <tbody>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_SumHAWBNo" runat="server" Text="总运单号:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Txt_SumHAWBNo" runat="server" Width="250"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_LockTime" runat="server" Text="锁定时间:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txt_LockTime" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Status" runat="server" Text="当前状态:"></asp:Label>
                </td>
                <td><asp:Label ID="txt_Status" runat="server"></asp:Label></td>
                <td><asp:Button ID="btn_Demand" runat="server" Text="查 询" CssClass="InputBtn" /></td>
            </tr>
            <tr class="Row">
                <td colspan="7">
                    <asp:GridView ID="gv_HAWB" runat="server" CssClass="DataView" 
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="航班号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_FLTNo" runat="server" Text='<%# Eval("aaa") %>'>'></asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Width="220px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="包号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_BagNumber" runat="server" Text='<%# Eval("aaa") %>'>'></asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Width="220px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="包重量">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BarCoder" runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="目的地三字码">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Weight" runat="server" Text='<%# Eval("Weight") %>'></asp:Label>
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
                <td class="FieldHeader" align="right" colspan="5">
                    <asp:Button ID="btn_Close" runat="server" Text="删 除" CssClass="InputBtn" /></td>
            </tr>
        </tbody>
    </table>
</div>