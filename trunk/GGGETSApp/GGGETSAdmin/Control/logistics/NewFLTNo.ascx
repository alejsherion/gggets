﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewFLTNo.ascx.cs" Inherits="GGGETSAdmin.Control.logistics.NewFLTNo" %>
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
                <td class="FieldHeader" align="left" style="width:80px">
                    <asp:Label ID="lbl_SumHAWBNo" runat="server" Text="总运单号:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox1" runat="server" Width="250"></asp:TextBox>
                </td>
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
            </tr>
            <tr class="Row">
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
                <td colspan="2">
                    <asp:Button ID="btn_Save" runat="server" Text="保 存" CssClass="InputBtn" />
                </td>
                <td colspan="2">
                    <asp:Button ID="btn_SaveAndClose" runat="server" Text="保存并锁定" CssClass="InputBtn" />
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_BagNumber" runat="server" Text="包号:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="Txt_BagNumber" runat="server" Width="250"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Weight" runat="server" Text="重量:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txt_Weight" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td colspan="2">
                   <asp:Button ID="btn_Add" runat="server" Text="添 加" CssClass="InputBtn" />                
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:GridView ID="gv_Bag" runat="server" CssClass="DataView" 
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="行号">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Number" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="包号">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_bagNumber" runat="server" Text='<%# Eval("Item") %>'></asp:Label>
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