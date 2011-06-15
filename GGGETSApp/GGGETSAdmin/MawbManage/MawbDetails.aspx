<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="MawbDetails.aspx.cs" Inherits="GGGETSAdmin.MawbManage.MawbDetails"
    Theme="logisitc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="DataView">
            <tbody>
                <tr class="Row">
                    <td class="FieldHeader" align="left" style="width: 80px">
                        <asp:Label ID="lbl_SumHAWBNo" runat="server" Text="总运单号:"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lbl_MAWBBarCode" runat="server"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txt_CreateTime" runat="server"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_LockTime" runat="server" Text="锁定时间:"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="txt_LockTime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_FLTNo" runat="server" Text="航班号:"></asp:Label>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtn_FLTNo" runat="server" OnClick="lbtn_FLTNo_Click"></asp:LinkButton>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_Pice" runat="server" Text="总体积:"></asp:Label>
                    </td>
                    <td style="width: 90px">
                        <asp:Label ID="txt_TotalVolume" runat="server"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_TotalWeight" runat="server" Text="总重量:"></asp:Label>
                    </td>
                    <td style="width: 70px">
                        <asp:Label ID="Txt_TotalWeight" runat="server"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_Status" runat="server" Text="当前状态:"></asp:Label>
                    </td>
                    <td style="width: 50px">
                        <asp:Label ID="txt_Status" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td colspan="8">
                        <asp:GridView ID="gv_MAWB" runat="server" CssClass="DataView" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="行号">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Number" runat="server" Text='<%# N() %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="包号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtn_BagBarCode" PostBackUrl='<%# "../PackageManage/PackageDetails.aspx?BarCode="+Eval("BarCode") %>'
                                            runat="server" Text='<%# Eval("BarCode") %>'>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle Width="120px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="包裹重量">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BarCoder" runat="server" Text='<%# Eval("TotalWeight") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="目的地三字码">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Weight" runat="server" Text='<%# Eval("DestinationRegionCode") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Status").ToString().Replace("0","打开").Replace("1","关闭")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否混包">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_IsMixed" runat="server" Text='<%# Eval("Status").ToString().Replace("1","是").Replace("0","否")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="FooterBtnBar">
        
        <asp:Button ID="But_Update" runat="server" Text="修 改" CssClass="InputBtn" OnClick="But_Next_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="But_Conel" runat="server" Text="返 回" CssClass="InputBtn" OnClick="But_Conel_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_DeriveSince" runat="server" Text="导出总运单清单" CssClass="InputBtn"
            OnClick="btn_DeriveSince_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_DeriveAccept" runat="server" Text="导出承运单清单" 
            CssClass="InputBtn" onclick="btn_DeriveAccept_Click"/>
    </div>
</asp:Content>
