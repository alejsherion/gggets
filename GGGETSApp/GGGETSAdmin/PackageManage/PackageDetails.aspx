<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PackageDetails.aspx.cs" Inherits="GGGETSAdmin.PackageManage.PackageDetails" Theme="logisitc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="FunctionBar">
    <table class="DataView">
        <tbody>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_BagBarCode" runat="server" Text="包号:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txt_BagBarCode" runat="server" Width="230"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                </td>
                <td style="width:140px">
                    <asp:Label ID="txt_CreateTime" runat="server"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_UpdateTime" runat="server" Text="修改时间:"></asp:Label>
                </td>
                <td style="width:140px">
                    <asp:Label ID="txt_UpdateTime" runat="server"></asp:Label>
                </td> 
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Region" runat="server" Text="目的地三字码:"></asp:Label>
                </td>
                <td style="width:50px">
                    <asp:Label ID="txt_Destination" runat="server" Width="50"></asp:Label>
                </td>              
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_MHAWb" runat="server" Text="总运单号:"></asp:Label>
                </td>
                <td>
                    <asp:LinkButton ID="lbtn_MHAWb" runat="server"></asp:LinkButton>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Pice" runat="server" Text="件数:"></asp:Label>
                </td>
                <td  style="width:80px">
                    <asp:Label ID="txt_Pice" runat="server"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_TotalWeight" runat="server" Text="总重量:"></asp:Label>
                </td>
                <td style="width:140px">
                    <asp:Label ID="Txt_TotalWeight" runat="server"></asp:Label>
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
                                    <asp:Label ID="lbl_Number" runat="server" Text='<%# N() %>'></asp:Label>
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
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="But_Next" runat="server" Text="修 改" CssClass="InputBtn" 
                        onclick="But_Next_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="But_Conel" runat="server" Text="返 回" CssClass="InputBtn" 
                        onclick="But_Conel_Click" />
                    </td>
            </tr>
        </tbody>
    </table>
</div>
</asp:Content>
