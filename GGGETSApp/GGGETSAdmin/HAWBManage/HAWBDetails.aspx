<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HAWBDetails.aspx.cs" Inherits="GGGETSAdmin.HAWBManage.HAWBDetails" Theme="logisitc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <asp:PlaceHolder ID="phHAWB" runat="server">
        <table class="DataView">
            <thead>
                <tr class="Header">
                    <th colspan="6">
                        <asp:Label ID="lbl_BarCode" runat="server" Text="运单号："></asp:Label>
                        <asp:Label ID="Txt_BarCode" runat="server"></asp:Label>
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
                        <asp:Label ID="Txt_Account1" runat="server" Width="80" TabIndex="2"></asp:Label>-
                        <asp:Label ID="Txt_Account2" runat="server" Width="50" TabIndex="3"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_SettleType" runat="server" Text="结算方式：" Width="80"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DDl_SettleType" runat="server" Enabled="false">
                            <asp:ListItem>预付月结</asp:ListItem>
                            <asp:ListItem>预付现结</asp:ListItem>
                            <asp:ListItem>到付月结</asp:ListItem>
                            <asp:ListItem>到付现结</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_Status" runat="server" Text="状态："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="txt_Status" runat="server"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:PlaceHolder>
</div>
<div>
    <asp:PlaceHolder ID="phShipper" runat="server">
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
                        <asp:Label ID="Txt_ShipperName" runat="server" Width="525" TabIndex="5"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_ShipperAddress" runat="server" Text="地址："></asp:Label>
                    </td>
                    <td align="left" colspan="5">
                        <asp:Label ID="Txt_ShipperAddress" runat="server" Width="525" TabIndex="6" TextMode="MultiLine"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_ShipperCountry" runat="server" Text="国家："></asp:Label>
                    </td>
                    <td align="left" class="style2">
                        <asp:Label ID="Txt_ShipperCountry" runat="server" Width="80" ></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_ShipperRegion" runat="server" Text="省份："></asp:Label>
                    </td>
                    <td align="left" class="style1">
                        <asp:Label ID="Txt_ShipperRegion" runat="server" Width="80" ></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_City" runat="server" Text="城市："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_City" runat="server" Width="80" ></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_ShipperZipCode" runat="server" Text="邮编："></asp:Label>
                    </td>
                    <td align="left" class="style2">
                        <asp:Label ID="Txt_ShipperZipCode" runat="server" Width="80" ></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_ShipperContactor" runat="server" Text="姓名："></asp:Label>
                    </td>
                    <td align="left" class="style1">
                        <asp:Label ID="Txt_ShipperContactor" runat="server" Width="80" ></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_ShipperTel" runat="server" Text="电话："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_ShipperTel" runat="server" Width="80" TabIndex="12" ></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:PlaceHolder>
</div>
<div>
    <asp:PlaceHolder ID="phConsignee" runat="server">
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
                        <asp:Label ID="Txt_ConsigneeName" runat="server" Width="525px" ></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_ConsigneeAddress" runat="server" Text="地址："></asp:Label>
                    </td>
                    <td align="left" colspan="5">
                        <asp:Label ID="Txt_ConsigneeAddress" runat="server" Width="525px" TabIndex="15"
                            TextMode="MultiLine" ></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_ConsigneeCountry" runat="server" Text="国家："></asp:Label>
                    </td>
                    <td align="left" class="style2">
                        <asp:Label ID="Txt_ConsigneeCountry" runat="server" Width="80" TabIndex="16" ></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_ConsigneeRegion" runat="server" Text="省份："></asp:Label>
                    </td>
                    <td align="left" class="style1">
                        <asp:Label ID="Txt_ConsigneeRegion" runat="server" Width="80" TabIndex="17" ></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_ConsigneeCity" runat="server" Text="城市："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_ConsigneeCity" runat="server" Width="80" TabIndex="18" ></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_ConsigneeZipCode" runat="server" Text="邮编："></asp:Label>
                    </td>
                    <td align="left" class="style2">
                        <asp:Label ID="Txt_ConsigneeZipCode" runat="server" Width="80" TabIndex="19" ></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_ConsigneeContactor" runat="server" Text="姓名："></asp:Label>
                    </td>
                    <td align="left" class="style1">
                        <asp:Label ID="Txt_ConsigneeContactor" runat="server" Width="80" TabIndex="20" ></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_ConsigneeTel" runat="server" Text="电话："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_ConsigneeTel" runat="server" Width="80" TabIndex="21" ></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:PlaceHolder>
</div>
<div>
    <asp:PlaceHolder ID="phDeliver" runat="server">
        <table class="DataView">
            <tbody id="Deliver" runat="server" visible="false">
                <tr class="EditRow">
                    <td colspan="6" align="left">
                        <asp:Label ID="lbl_Deliver" runat="server" Text="交付人信息" ></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverName" runat="server" Text="公司:" ></asp:Label>
                    </td>
                    <td align="left" colspan="5">
                        <asp:Label ID="Txt_DeliverName" runat="server" Width="525px" TabIndex="22" ></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverAddress" runat="server" Text="地址：" ></asp:Label>
                    </td>
                    <td align="left" colspan="5">
                        <asp:Label ID="Txt_DeliverAddress" runat="server" Width="525px" TabIndex="23" TextMode="MultiLine" ></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverCountry" runat="server" Text="国家：" ></asp:Label>
                    </td>
                    <td align="left" class="style2">
                        <asp:Label ID="Txt_DeliverCountry" runat="server" Width="80" TabIndex="24" ></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverRegion" runat="server" Text="省份：" ></asp:Label>
                    </td>
                    <td align="left" class="style1">
                        <asp:Label ID="Txt_DeliverRegion" runat="server" Width="80" TabIndex="25" ></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverCity" runat="server" Text="城市：" ></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_DeliverCity" runat="server" Width="80" TabIndex="26" ></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverZipCode" runat="server" Text="邮编：" ></asp:Label>
                    </td>
                    <td align="left" class="style2">
                        <asp:Label ID="Txt_DeliverZipCode" runat="server" Width="80" TabIndex="27" ></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverContactor" runat="server" Text="姓名：" ></asp:Label>
                    </td>
                    <td align="left" class="style1">
                        <asp:Label ID="Txt_DeliverContactor" runat="server" Width="80" TabIndex="28" ></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverTel" runat="server" Text="电话：" ></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_DeliverTel" runat="server" Width="80" TabIndex="29" ></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:PlaceHolder>
</div>
<div>
    <table>
        <tbody>
            <tr class="Row">
                <td class="FieldHeader">
                   <asp:Label ID="lbl_BoxType" runat="server" Text="包裹类型："></asp:Label>
                </td>
                <td align="left" colspan="3">
                    <asp:RadioButtonList ID="rbt_BoxType" runat="server" RepeatDirection="Horizontal" Enabled="false">
                        <asp:ListItem Value="0" Text="文件"></asp:ListItem>
                        <asp:ListItem Value="1" Text="小包裹"></asp:ListItem>
                        <asp:ListItem Value="2" Text="普货"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_payer" runat="server" Text="付款人："></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:RadioButtonList runat="server" ID="rbt_payer" RepeatDirection="Horizontal" Enabled="false" TabIndex="4"
                        BorderStyle="None">
                        <asp:ListItem Value="0" Text="发件人"></asp:ListItem>
                        <asp:ListItem Value="1" Text="收件人"></asp:ListItem>
                        <asp:ListItem Value="2" Text="第三方"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_SpecialInstruction" runat="server" Text="通关指示："></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:RadioButtonList ID="Rbl_SpecialInstruction" runat="server" RepeatDirection="Horizontal" Enabled="false">
                        <asp:ListItem Text="有"></asp:ListItem>
                        <asp:ListItem Text="无"></asp:ListItem>
                        <asp:ListItem Text="其它"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </tbody>
    </table>
    <table class="DataView">
        <tbody>
            <tr class="EditRow"> 
                <td colspan="9"><asp:Label ID="lbl_item" runat="server" Text="货物信息"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="9" align="center">
                    <asp:GridView ID="GV_item" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="件数">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ItemPiece" runat="server" Text='<%# Eval("Piece") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物品名称">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ItemName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="170px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物品类型">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ItemType" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="170px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物品价值">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ItemPiece" runat="server" Text='<%# Eval("UnitAmount") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="90px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            </tbody>
    </table>
    <table class="DataView">
        <tbody>
            <tr class="EditRow"> 
                <td colspan="9"><asp:Label ID="lbl_Box" runat="server" Text="包裹信息"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="9" align="center">
                    <asp:GridView ID="gv_Box" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="件数">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxPiece" runat="server" Text='<%# Eval("Piece") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="110px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="重量(kg)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxWeight" runat="server" Text='<%# Eval("Weight") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="110px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="高度(cm)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxHeight" runat="server" Text='<%# Eval("Height") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="110px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="长度(cm)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxLength" runat="server" Text='<%# Eval("Length") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="110px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="宽度(cm)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxWidth" runat="server" Text='<%# Eval("Width") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="110px" />
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
                    <asp:Label ID="lbl_WeightType" runat="server" Text="计重类型："></asp:Label></td>
                <td align="left">
                    <asp:DropDownList ID="ddl_WeightType" runat="server" Enabled="false">
                        <asp:ListItem Value="0" Text="实重"></asp:ListItem>
                        <asp:ListItem Value="1" Text="泡重"></asp:ListItem>
                        <asp:ListItem Value="2" Text="加权"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_VolumeWeight" runat="server" Text="总重量："></asp:Label></td>
                <td align="left">
                    <asp:Label ID="txt_TotalWeight" runat="server" ></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_PieceTile" runat="server" Text="包裹件数："></asp:Label></td>
                <td align="left">
                    <asp:Label ID="lbl_Piece" runat="server" ></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_TotalVolumeTile" runat="server" Text="总容积："></asp:Label></td>
                <td align="left">
                    <asp:Label ID="lbl_TotalVolume" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Remark" runat="server" Text="备注："></asp:Label></td>
                <td align="left" colspan="7">
                    <asp:Label ID="txt_Remark" runat="server" Width="500" Height="100" TextMode="MultiLine" ></asp:Label></td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Carrier" runat="server" Text="承运公司名称："></asp:Label></td>
                <td align="left" colspan="7">
                    <asp:Label ID="Txt_Carrier" runat="server" Width="422px" ></asp:Label></td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_CarrierHAWBID" runat="server" Text="承运公司运单号："></asp:Label></td>
                <td align="left" colspan="7">
                    <asp:Label ID="Txt_CarrierHAWBID" runat="server" Width="422px" ></asp:Label></td>
            </tr>
        </tbody>
    </table>
</div>
<div class="FooterBtnBar">
    <asp:Button ID="But_Next" runat="server" Text="修 改" CssClass="InputBtn" 
        TabIndex="30" onclick="But_Next_Click" />
    <asp:Button ID="But_Conel" runat="server" Text="返 回" CssClass="InputBtn" 
        TabIndex="31" onclick="But_Conel_Click" />
</div>
</asp:Content>
