<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewBag.ascx.cs" Inherits="GGGETSAdmin.Control.logistics.NewBag" %>
<div>
    <table class="DataView">
        <tbody>
            <tr class="Row">
                <td class="FieldHeader" style="width:120px">
                    <asp:Label ID="lbl_BagNumber" runat="server" Text="包号:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="Txt_BagNumber" runat="server" Width="250"></asp:TextBox>
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
                    <asp:Label ID="lbl_Destination" runat="server" Text="目的地三字码:"></asp:Label>
                </td>
                <td style="width:80px">
                    <asp:TextBox ID="txt_Destination" runat="server" Width="50"></asp:TextBox>
                </td>
                <td class="FieldHeader" style="width:50px">
                    <asp:Label ID="lbl_Pice" runat="server" Text="件数:"></asp:Label>
                </td>
                <td  style="width:80px">
                    <asp:Label ID="txt_Pice" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_TotalWeight" runat="server" Text="总重量:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Txt_TotalWeight" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btn_Save" runat="server" Text="保 存" CssClass="InputBtn" />
                </td>
                <td>
                    <asp:Button ID="btn_SaveAndClose" runat="server" Text="保存并锁定" CssClass="InputBtn" />
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_BarCode" runat="server" Text="运单号:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txt_BarCode" runat="server" Width="250"></asp:TextBox>
                </td>
                <td colspan="2">
                   <asp:Button ID="btn_Add" runat="server" Text="添 加" CssClass="InputBtn" />                
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:GridView ID="gv_HAWB" runat="server" CssClass="DataView" 
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="编号">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Number" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="运单号">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BarCoder" runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="重量">
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
