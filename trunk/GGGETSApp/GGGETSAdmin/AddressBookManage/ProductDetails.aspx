<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="GGGETSAdmin.ProductManage.ProductDetails" Theme="logisitc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>
                <table class="DataView">
                    <tr class="Row">
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_HSCode" runat="server" Text="HS编码："></asp:Label>
                        </td>
                        <td style="width: 200px">
                            <asp:Label ID="Txt_HSCode" runat="server" TabIndex="1" Width="200" MaxLength="20"></asp:Label>
                        </td>
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_HSName" runat="server" Text="商品名称："></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Txt_HSName" runat="server" TabIndex="2" Width="400" MaxLength="30"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_DiscountTax" runat="server" Text="优惠税率："></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Txt_DiscountTax" runat="server" TabIndex="3"></asp:Label>
                        </td>
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_GeneralTax" runat="server" Text="普通税率："></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Txt_GeneralTax" runat="server" TabIndex="4"></asp:Label>
                        </td>
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_ExportTax" runat="server" Text="出口税率："></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Txt_ExportTax" runat="server" TabIndex="5"></asp:Label>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_ConsumeTax" runat="server" Text="消费税率："></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Txt_ConsumeTax" runat="server" TabIndex="6"></asp:Label>
                        </td>
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_RiseTax" runat="server" Text="增值税率："></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Txt_RiseTax" runat="server" TabIndex="7"></asp:Label>
                        </td>
                        <td class="FieldHeader" align="right" style="width: 200px">
                            <asp:Label ID="lbl_CertificateSign" runat="server" Text="所需证件标志："></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Txt_CertificateSign" runat="server" TabIndex="8" MaxLength="20"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader" align="right" style="width: 250px">
                            <asp:Label ID="lbl_PricingSign" runat="server" Text="重点审价标志："></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Txt_PricingSign" runat="server" TabIndex="9" MaxLength="20"></asp:Label>
                        </td>
                        <td class="FieldHeader" align="right" style="width: 250px">
                            <asp:Label ID="lbl_TaxDemandSign" runat="server" Text="征税要求标记："></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Txt_TaxDemandSign" runat="server" TabIndex="10" MaxLength="20"></asp:Label>
                        </td>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_Remark" runat="server" Text="备注："></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="Txt_Remark" runat="server" TabIndex="11" TextMode="MultiLine" Height="50"
                                Width="400" MaxLength="130"></asp:Label>
                        </td>
                        <td class="FieldHeader" align="center">
                            <asp:Button ID="btn_Update" runat="server" TabIndex="12" CssClass="InputBtn" 
                                Text="修 改" onclick="btn_Update_Click" />
                        </td>
                        <td align="left">
                            <asp:Button ID="But_Conel" runat="server" Text="返 回" CssClass="InputBtn" 
                                TabIndex="13" onclick="But_Conel_Click"/>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="Gv_Productiy" runat="server" AutoGenerateColumns="False" PageSize="100"
                    DataKeyNames="HSID">
                    <Columns>
                        <asp:TemplateField HeaderText="行号">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nuber" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="属性名称">
                            <ItemTemplate>
                                <asp:Label ID="lbl_PropertyName" runat="server" Text='<%# Eval("PropertyName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="中文备注">
                            <ItemTemplate>
                                <asp:Label ID="lbl_ChineseRemark" runat="server" Text='<%# Eval("ChineseRemark") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="550px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
</asp:Content>
