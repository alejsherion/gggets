<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BagDetails.ascx.cs" Inherits="GGGETSAdmin.Control.logistics.BagDetails" %>
<div>
    <table class="DataView">
        <tbody>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_BagNumber1" runat="server" Text="包号:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbl_BagNumber" runat="server" CssClass="TextBox" Width="230"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                </td>
                <td style="width:140px">
                    <asp:Label ID="txt_CreateTime" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_UpdateTime" runat="server" Text="修改时间:"></asp:Label>
                </td>
                <td style="width:140px">
                    <asp:Label ID="txt_UpdateTime" runat="server" CssClass="TextBox"></asp:Label>
                </td> 
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Destination" runat="server" Text="目的地三字码:"></asp:Label>
                </td>
                <td style="width:50px">
                    <asp:Label ID="txt_Destination" runat="server" CssClass="TextBox" Width="50"></asp:Label>
                </td>              
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_SumHAWBNo" runat="server" Text="总运单号:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Txt_SumHAWBNo" runat="server" CssClass="TextBox" Width="230"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Pice" runat="server" Text="件数:"></asp:Label>
                </td>
                <td  style="width:80px">
                    <asp:Label ID="txt_Pice" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_TotalWeight" runat="server" Text="总重量:"></asp:Label>
                </td>
                <td style="width:140px">
                    <asp:Label ID="Txt_TotalWeight" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Status" runat="server" Text="当前状态:"></asp:Label>
                </td>
                <td style="width:50px"><asp:Label ID="txt_Status" runat="server"></asp:Label></td>   
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
                           <%-- <asp:TemplateField>
                                <HeaderTemplate><asp:CheckBox ID="Ck_Sum" runat="server" /></HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Ck" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader" colspan="8">
                    <asp:Button ID="btn_Add" runat="server" Text="添 加" CssClass="InputBtn" />
                    <asp:Button ID="btn_Return" runat="server" Text="返 回" CssClass="InputBtn" />
                    <asp:Button ID="btn_Modify" runat="server" Text="删 除" CssClass="InputBtn" /></td>
            </tr>
        </tbody>
    </table>
</div>