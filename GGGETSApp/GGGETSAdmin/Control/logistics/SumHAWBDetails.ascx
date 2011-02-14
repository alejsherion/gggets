<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SumHAWBDetails.ascx.cs" Inherits="GGGETSAdmin.Control.logistics.SumHAWBDetails" %>
<div>
    <table class="DataView">
        <tbody>
            <tr class="Row">
                <td class="FieldHeader" align="left" style="width:80px">
                    <asp:Label ID="lbl_SumHAWBNo" runat="server" Text="总运单号:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox1" runat="server" Width="250"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txt_CreateTime" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_LockTime" runat="server" Text="锁定时间:"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label ID="txt_LockTime" runat="server" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_FLTNo" runat="server" Text="航班号:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txt_FLTNo" runat="server" Width="250" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Pice" runat="server" Text="件数:"></asp:Label>
                </td>
                <td style="width:90px">
                    <asp:Label ID="txt_Pice" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_TotalWeight" runat="server" Text="总重量:"></asp:Label>
                </td>
                <td style="width:70px">
                    <asp:Label ID="Txt_TotalWeight" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Status" runat="server" Text="当前状态:"></asp:Label>
                </td>
                <td style="width:50px"><asp:Label ID="txt_Status" runat="server"></asp:Label></td>                
            </tr>
            <tr class="Row">
                <td colspan="8">
                    <asp:GridView ID="gv_HAWB" runat="server" CssClass="DataView" 
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="包号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_BagNumber" runat="server" Text='<%# Eval("aaa") %>'>'></asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Width="220px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="包裹重量">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BarCoder" runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="目的地三字码">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Weight" runat="server" Text='<%# Eval("Weight") %>'></asp:Label>
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