<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HWABInternalDetails.ascx.cs" Inherits="GGGETSAdmin.Control.HWABInternalDetails" %>
<div>
    <table class="DataView">
        <thead>
            <tr class="Header">
                <th colspan="6">
                    <asp:Label ID="lbl_BarCodeTitle" runat="server" Text="运单号："></asp:Label>
                    <asp:Label ID="lbl_Orde" runat="server" CssClass="TextBox"></asp:Label>
                    <a>William</a>
                </th>
            </tr>
        </thead>
        <tbody>
            <tr class="AlternatingRow">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Account" runat="server" Text="客户帐号：" Width="80"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="Txt_Account1" runat="server" Width="80" TabIndex="2" CssClass="TextBox"></asp:Label>-
                    <asp:Label ID="Txt_Account2" runat="server" Width="50" TabIndex="3" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_SettleType" runat="server" Text="结算方式：" Width="80"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DDl_SettleType" runat="server" Enabled="False">
                        <asp:ListItem>预付月结</asp:ListItem>
                        <asp:ListItem>预付现结</asp:ListItem>
                        <asp:ListItem>到付月结</asp:ListItem>
                        <asp:ListItem>到付现结</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </tbody>
    </table>
</div>
<div>
    <table class="DataView">
        <tbody>
            <tr class="EditRow">
                <td colspan="6" align="left">
                    <asp:Label ID="lbl_Shipper" runat="server" Text="发件人信息"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ShipperName" runat="server" Text="公司："></asp:Label>
                </td>
                <td align="left" colspan="5">
                    <asp:Label ID="Txt_ShipperName" runat="server" Width="525" TabIndex="5" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ShipperAddress" runat="server" Text="地址："></asp:Label>
                </td>
                <td align="left" colspan="5">
                    <asp:Label ID="Txt_ShipperAddress" runat="server" Width="525" TabIndex="6" TextMode="MultiLine" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ShipperCountry" runat="server" Text="国家："></asp:Label>
                </td>
                <td align="left" class="style2">
                    <asp:Label ID="Txt_ShipperCountry" runat="server" Width="80" TabIndex="7" MaxLength="2" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ShipperRegion" runat="server" Text="省份："></asp:Label>
                </td>
                <td align="left" class="style1">
                    <asp:Label ID="Txt_ShipperRegion" runat="server" Width="80" TabIndex="8" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_City" runat="server" Text="城市："></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="Txt_City" runat="server" Width="80" TabIndex="9" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ShipperZipCode" runat="server" Text="邮编："></asp:Label>
                </td>
                <td align="left" class="style2">
                    <asp:Label ID="Txt_ShipperZipCode" runat="server" Width="80" TabIndex="10" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ShipperContactor" runat="server" Text="姓名："></asp:Label>
                </td>
                <td align="left" class="style1">
                    <asp:Label ID="Txt_ShipperContactor" runat="server" Width="80" TabIndex="11" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ShipperTel" runat="server" Text="电话："></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="Txt_ShipperTel" runat="server" Width="80" TabIndex="12" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
</div>
<div>
    <table class="DataView">
        <tbody>
            <tr class="EditRow">
                <td colspan="6" align="left">
                    <asp:Label ID="lbl_Consignee" runat="server" Text="收件人信息"></asp:Label>
                    
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeName" runat="server" Text="公司："></asp:Label>
                </td>
                <td align="left" colspan="5">
                    <asp:Label ID="Txt_ConsigneeName" runat="server" Width="525px" TabIndex="14" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeAddress" runat="server" Text="地址："></asp:Label>
                </td>
                <td align="left" colspan="5">
                    <asp:Label ID="Txt_ConsigneeAddress" runat="server" Width="525px" TabIndex="15"
                        TextMode="MultiLine" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeCountry" runat="server" Text="国家："></asp:Label>
                </td>
                <td align="left" class="style2">
                    <asp:Label ID="Txt_ConsigneeCountry" runat="server" Width="80" TabIndex="16" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeRegion" runat="server" Text="省份："></asp:Label>
                </td>
                <td align="left" class="style1">
                    <asp:Label ID="Txt_ConsigneeRegion" runat="server" Width="80" TabIndex="17" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeCity" runat="server" Text="城市："></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="Txt_ConsigneeCity" runat="server" Width="80" TabIndex="18" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeZipCode" runat="server" Text="邮编："></asp:Label>
                </td>
                <td align="left" class="style2">
                    <asp:Label ID="Txt_ConsigneeZipCode" runat="server" Width="80" TabIndex="19" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeContactor" runat="server" Text="姓名："></asp:Label>
                </td>
                <td align="left" class="style1">
                    <asp:Label ID="Txt_ConsigneeContactor" runat="server" Width="80" TabIndex="20" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeTel" runat="server" Text="电话："></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="Txt_ConsigneeTel" runat="server" Width="80" TabIndex="21" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
    <table>
      <tbody>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_BoxType" runat="server" Text="包裹类型："></asp:Label>
                </td>
                <td align="left" colspan="3">
                    <asp:RadioButtonList ID="rbt_BoxType" runat="server" RepeatDirection="Horizontal" Enabled="false">
                        <asp:ListItem Value="0">文件</asp:ListItem>
                        <asp:ListItem Value="1">小包裹</asp:ListItem>
                        <asp:ListItem Value="2">普货</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_payer" runat="server" Text="付款人："></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:RadioButtonList runat="server" ID="rbt_payer" RepeatDirection="Horizontal" Enabled="false"
                        BorderStyle="None">
                        <asp:ListItem Value="0">发件人</asp:ListItem>
                        <asp:ListItem Value="1">收件人</asp:ListItem>
                        <asp:ListItem Value="2">第三方</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </tbody>
    </table>
    <table>
        <tbody>
            <tr class="EidtRow"> 
                <td colspan="9"><asp:Label ID="lbl_Box" runat="server" Text="货物信息"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="9" align="center">
                    <asp:GridView ID="gv_Box" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="物品名称">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_itemName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="件数">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxPiece" runat="server" Text='<%# Eval("Piece") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物品类型">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_itemType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="重量">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxWeight" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="高度">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxHeight" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="长度">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxLength" runat="server" Text='<%# Eval("Piece") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="宽度">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxWidth" runat="server" Text='<%# Eval("Piece") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </tbody>
    </table>
    <table>
        <tbody>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="Label1" runat="server" Text="计重类型："></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DropDownList1" runat="server" Enabled="false">
                        <asp:ListItem>实重</asp:ListItem>
                        <asp:ListItem>泡重</asp:ListItem>
                        <asp:ListItem>加权</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_VolumeWeight" runat="server" Text="总重量："></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="txt_VolumeWeight" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_PieceTitle" runat="server" Text="包裹件数："></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lbl_Piece" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_TotalVolumeTitle" runat="server" Text="总容积："></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lbl_TotalVolume" runat="server" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Remark" runat="server" Text="备注："></asp:Label>
                </td>
                <td align="left" colspan="7">
                    <asp:Label ID="txt_Remark" runat="server" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Carrier" runat="server" Text="承运公司名称："></asp:Label>
                </td>
                <td align="left" colspan="7">
                    <asp:Label ID="Txt_Carrier" CssClass="TextBox" runat="server" Width="300"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_CarrierHAWBID" runat="server" Text="承运公司运单号："></asp:Label>
                </td>
                <td align="left" colspan="7">
                    <asp:Label ID="Txt_CarrierHAWBID" CssClass="TextBox" runat="server" Width="300"></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
    <div class="FooterBtnBar">
        <asp:Button ID="But_Modify" runat="server" Text="修 改" ValidationGroup="1" CssClass="InputBtn" />
        <asp:Button ID="But_Rurnet" runat="server" Text="返 回" CssClass="InputBtn" />
    </div>
</div>
