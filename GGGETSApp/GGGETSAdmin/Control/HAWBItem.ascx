﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HAWBItem.ascx.cs" Inherits="GGGETSAdmin.Control.HAWBItem" %>
<div>
    <table class="DataView">
        <thead>
            <tr class="Row">
                <th class="FieldHeader" colspan="1">
                    <asp:Label ID="lbl_Ordertile" runat="server" Text="运单号："></asp:Label>
                </th>
                <th align="left" colspan="3">
                    <asp:Label ID="lbl_Order" runat="server" CssClass="TextBox" Width="250"></asp:Label>
                </th>
                <th class="FieldHeader" colspan="2">
                    <asp:Label ID="lbl_ShipperCountryTile" runat="server" Text="发件国家二字码："></asp:Label>
                </th>
                <th>
                    <asp:Label ID="lbl_ShipperCountry" runat="server" CssClass="TextBox" Width="50"></asp:Label>
                </th>
                <th class="FieldHeader">
                   <asp:Label ID="lbl_ShipperRegiontile" runat="server" Text="发件地区三字码："></asp:Label>
                </th>
                <th>
                    <asp:Label ID="lbl_ShipperRegion" runat="server" CssClass="TextBox" Width="50"></asp:Label>
                </th>
            </tr>
            <tr class="Row">
                <th>
                    <asp:Label ID="lbl_ShipperName" runat="server" Text="发件公司："></asp:Label>
                </th>
                <th colspan="8" align="left">
                    <asp:Label ID="lbl_Txt_ShipperName" runat="server" CssClass="TextBox" Width="350"></asp:Label>
                </th>
            </tr>
            <tr class="Row">
                <th class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeNameTile" runat="server" Text="收件公司："></asp:Label>
                </th>
                <th colspan="8" align="left">
                    <asp:Label ID="lbl_ConsigneeName" runat="server" CssClass="TextBox" Width="350"></asp:Label>
                </th>
            </tr>
        </thead>
        <tbody>
            <tr class="Row">
                <td class="FieldHeader">
                   <asp:Label ID="lbl_BoxType" runat="server" Text="包裹类型："></asp:Label>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="rbt_BoxType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">文件</asp:ListItem>
                        <asp:ListItem Value="1">小包裹</asp:ListItem>
                        <asp:ListItem Value="2">普货</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_payer" runat="server" Text="付款人："></asp:Label>
                </td>
                <td align="left">
                    <asp:RadioButtonList runat="server" ID="rbt_payer" RepeatDirection="Horizontal" TabIndex="4"
                        BorderStyle="None">
                        <asp:ListItem Value="0">发件人</asp:ListItem>
                        <asp:ListItem Value="1">收件人</asp:ListItem>
                        <asp:ListItem Value="2">第三方</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_" runat="server" Text="通关指示："></asp:Label>
                </td>
                <td align="left" colspan="4">
                    <asp:RadioButtonList ID="Rbl_" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">有</asp:ListItem>
                        <asp:ListItem>无</asp:ListItem>
                        <asp:ListItem>其它</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </tbody>
    </table>
    <table class="DataView">
        <tbody>
            <tr class="EditRow">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ItemPiece" runat="server" Text="件数："></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="Txt_ItemPiece" runat="server" Width="58px"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ItemName" runat="server" Text="物品名称："></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_ItemName" runat="server" Width="160"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ItemType" runat="server" Text="物品类型："></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_ItemType" runat="server" Width="160"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ItemPice" runat="server" Text="物品价值："></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="Txt_ItemPice" runat="server" Width="58px"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Button ID="but_AddItem" runat="server" Text="添加物品" CssClass="InputBtn" />
                </td>
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
                                    <asp:Label ID="lbl_ItemType" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="170px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物品价值">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ItemPiece" runat="server" Text='<%# Eval("Piece") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="90px" />
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" />
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            </tbody>
    </table>
    <table class="DataView">
        <tbody>
            <tr class="EditRow">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_BoxPiece" runat="server" Text="件数："></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_BoxPiece" runat="server" Width="80"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_BoxWeight" runat="server" Text="重量："></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="Txt_BoxWeight" runat="server" Width="80"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_BoxHeight" runat="server" Text="高度："></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="Txt_BoxHeight" runat="server" Width="80"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_BoxLength" runat="server" Text="长度："></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="Txt_BoxLength" runat="server" Width="80"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_BoxWidth" runat="server" Text="宽度："></asp:Label></td>
                <td align="left">
                    <asp:TextBox ID="Txt_BoxWidth" runat="server" Width="80"></asp:TextBox>
                </td>
                <td class="FieldHeader" colspan="3">
                    <asp:Button ID="but_AddBox" runat="server" Text="添加包裹" CssClass="InputBtn" />
                </td>
            </tr>
            <tr>
                <td colspan="11" align="center">
                    <asp:GridView ID="gv_Box" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="件数">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxPiece" runat="server" Text='<%# Eval("Piece") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="110px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="重量">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxWeight" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="110px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="高度">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxHeight" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="110px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="长度">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxLength" runat="server" Text='<%# Eval("Piece") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="110px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="宽度">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BoxWidth" runat="server" Text='<%# Eval("Piece") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="110px" />
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" />
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </tbody>
    </table>
    <table class="DataView">
        <tbody>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_SettleType" runat="server" Text="计重类型："></asp:Label></td>
                <td align="left">
                    <asp:DropDownList ID="ddl_SettleType" runat="server">
                        <asp:ListItem Value="0">实重</asp:ListItem>
                        <asp:ListItem Value="1">泡重</asp:ListItem>
                        <asp:ListItem Value="2">加权</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_VolumeWeight" runat="server" Text="总重量："></asp:Label></td>
                <td align="left">
                    <asp:Label ID="txt_VolumeWeight" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_PieceTile" runat="server" Text="包裹件数："></asp:Label></td>
                <td align="left">
                    <asp:Label ID="lbl_Piece" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_TotalVolumeTile" runat="server" Text="总容积："></asp:Label></td>
                <td align="left">
                    <asp:Label ID="lbl_TotalVolume" runat="server" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Remark" runat="server" Text="备注："></asp:Label></td>
                <td align="left" colspan="7">
                    <asp:TextBox ID="txt_Remark" runat="server" Width="500" Height="100" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Carrier" runat="server" Text="承运公司名称："></asp:Label></td>
                <td align="left" colspan="7">
                    <asp:TextBox ID="Txt_Carrier" runat="server" Width="422px"></asp:TextBox></td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_CarrierHAWBID" runat="server" Text="承运公司运单号："></asp:Label></td>
                <td align="left" colspan="7">
                    <asp:TextBox ID="Txt_CarrierHAWBID" runat="server" Width="422px"></asp:TextBox></td>
            </tr>
        </tbody>
    </table>
    <div class="FooterBtnBar">
            <asp:Button ID="But_AddHAWB" runat="server" Text="创 建"
                ValidationGroup="1" CssClass="InputBtn" />
            <asp:Button ID="But_Rurnet" runat="server" Text="返 回"
                CssClass="InputBtn" />
        </div>
</div>
