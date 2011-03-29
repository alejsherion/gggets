<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="HAWBItemAdd.aspx.cs" Inherits="GGGETSAdmin.HAWBManage.HAWBItemAdd" Theme="logisitc" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="DataView">
            <thead>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_BarCodeTitle" runat="server" Text="运单号：" 
                            meta:resourcekey="lbl_BarCodeTitleResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_BarCode" runat="server" Width="250px" 
                            meta:resourcekey="lbl_BarCodeResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_ShipperCountryTitle" runat="server" Text="发件地区三字码：" 
                            meta:resourcekey="lbl_ShipperCountryTitleResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_ShipperRegion" runat="server" Width="50px" 
                            meta:resourcekey="lbl_ShipperRegionResource1"></asp:Label>
                    </td>
                    <td style="width:250px">
                        <asp:Label ID="lbl_ConsigneeRegionTitle" runat="server" Text="收件地区三字码：" 
                            meta:resourcekey="lbl_ConsigneeRegionTitleResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_ConsigneeRegion" runat="server" Width="50px" 
                            meta:resourcekey="lbl_ConsigneeRegionResource1"></asp:Label>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right" style="width:120px">
                        <asp:Label ID="lbl_ShipperNameTitle" runat="server" Text="发件公司：" 
                            meta:resourcekey="lbl_ShipperNameTitleResource1"></asp:Label>
                    </td>
                    <td colspan="8" align="left">
                        <asp:Label ID="lbl_ShipperName" runat="server" Width="350px" 
                            meta:resourcekey="lbl_ShipperNameResource1"></asp:Label>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_ConsigneeNameTitle" runat="server" Text="收件公司：" 
                            meta:resourcekey="lbl_ConsigneeNameTitleResource1"></asp:Label>
                    </td>
                    <td colspan="8" align="left">
                        <asp:Label ID="lbl_ConsigneeName" runat="server" Width="350px" 
                            meta:resourcekey="lbl_ConsigneeNameResource1"></asp:Label>
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_BoxType" runat="server" Text="包裹类型：" 
                            meta:resourcekey="lbl_BoxTypeResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rbt_BoxType" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="True" 
                            OnSelectedIndexChanged="rbt_BoxType_SelectedIndexChanged" 
                            meta:resourcekey="rbt_BoxTypeResource1">
                            <asp:ListItem Value="0" Text="文件" meta:resourcekey="ListItemResource1"></asp:ListItem>
                            <asp:ListItem Selected="True"  Value="1" Text="小包裹" meta:resourcekey="ListItemResource2"></asp:ListItem>
                            <asp:ListItem Value="2" Text="普货" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="FieldHeader" style="width:100px">
                        <asp:Label ID="lbl_BillTax" runat="server" Text="付款人：" 
                            meta:resourcekey="lbl_BillTaxResource1"></asp:Label>
                    </td>
                    <td align="left" style="width:400px">
                        <asp:RadioButtonList runat="server" ID="rbt_BillTax" 
                            RepeatDirection="Horizontal" BorderStyle="None" 
                            meta:resourcekey="rbt_BillTaxResource1">
                            <asp:ListItem Selected="True" Value="0" Text="发件人" 
                                meta:resourcekey="ListItemResource4"></asp:ListItem>
                            <asp:ListItem Value="1" Text="收件人" meta:resourcekey="ListItemResource5"></asp:ListItem>
                            <asp:ListItem Value="2" Text="第三方" meta:resourcekey="ListItemResource6"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_SpecialInstruction" runat="server" Text="通关指示：" 
                            meta:resourcekey="lbl_SpecialInstructionResource1"></asp:Label>
                    </td>
                    <td align="left" colspan="4">
                        <asp:RadioButtonList ID="Rbl_SpecialInstruction" runat="server" 
                            RepeatDirection="Horizontal" meta:resourcekey="Rbl_SpecialInstructionResource1">
                            <asp:ListItem Value="有" Text="有" meta:resourcekey="ListItemResource7"></asp:ListItem>
                            <asp:ListItem Value="无" Text="无" Selected="True" 
                                meta:resourcekey="ListItemResource8" ></asp:ListItem>
                            <asp:ListItem Value="其它" Text="其它" meta:resourcekey="ListItemResource9"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="DataView" id="Dvitem" runat="server">
            <tbody>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="left" style="width:80px">
                        <asp:Label ID="lbl_ItemPiece" runat="server" Text="件数：" 
                            meta:resourcekey="lbl_ItemPieceResource1"></asp:Label>
                    </td>
                    <td align="left" style="width:50px">
                        <asp:TextBox ID="Txt_ItemPiece" runat="server" Width="50px" TabIndex="1" 
                            style="text-transform:uppercase" meta:resourcekey="Txt_ItemPieceResource1" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_ItemName" runat="server" Text="物品名称：" 
                            meta:resourcekey="lbl_ItemNameResource1"></asp:Label>
                    </td>
                    <td align="left" style="width:200px">
                        <asp:TextBox ID="Txt_ItemName" runat="server" Width="160px" TabIndex="2" 
                            style="text-transform:uppercase" meta:resourcekey="Txt_ItemNameResource1" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_ItemType" runat="server" Text="物品类型：" 
                            meta:resourcekey="lbl_ItemTypeResource1"></asp:Label>
                    </td>
                    <td align="left" style="width:200px">
                        <asp:TextBox ID="Txt_ItemType" runat="server" Width="160px" TabIndex="3" 
                            style="text-transform:uppercase" meta:resourcekey="Txt_ItemTypeResource1" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_ItemPice" runat="server" Text="物品价值：" 
                            meta:resourcekey="lbl_ItemPiceResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_ItemPice" runat="server" Width="50px" TabIndex="4" 
                            style="text-transform:uppercase" meta:resourcekey="Txt_ItemPiceResource1" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" colspan="3">
                        <asp:Button ID="but_AddItem" runat="server" Text="添加物品" TabIndex="5" 
                            CssClass="InputBtn" OnClick="but_AddItem_Click" 
                            meta:resourcekey="but_AddItemResource1" />
                    </td>
                </tr>
                <tr>
                    <td colspan="11" align="center">
                        <asp:GridView ID="GV_item" runat="server" AutoGenerateColumns="False" 
                            onrowcancelingedit="GV_item_RowCancelingEdit" onrowcommand="GV_item_RowCommand" 
                            onrowdeleting="GV_item_RowDeleting" onrowediting="GV_item_RowEditing" 
                            onrowupdating="GV_item_RowUpdating" DataKeyNames="ItemID" 
                            ShowFooter="True" onrowdatabound="GV_item_RowDataBound" 
                            meta:resourcekey="GV_itemResource1">
                            <Columns>
                                <asp:TemplateField HeaderText="件数" meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ItemPiece" runat="server" Text='<%# Eval("Piece") %>' 
                                            meta:resourcekey="lbl_ItemPieceResource2"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_ItemPiece" runat="server" Text='<%# Eval("Piece") %>' 
                                            style="text-transform:uppercase" meta:resourcekey="txt_ItemPieceResource2" CssClass="TextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="90px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="物品名称" meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ItemName" runat="server" Text='<%# Eval("Name") %>' 
                                            meta:resourcekey="lbl_ItemNameResource2"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_ItemName" runat="server" Text='<%# Eval("Name") %>' 
                                            style="text-transform:uppercase" meta:resourcekey="txt_ItemNameResource2" CssClass="TextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="170px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="物品类型" meta:resourcekey="TemplateFieldResource3">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ItemType" runat="server" Text='<%# Eval("Remark") %>' 
                                            meta:resourcekey="lbl_ItemTypeResource2"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_ItemType" runat="server" Text='<%# Eval("Remark") %>' 
                                            style="text-transform:uppercase" meta:resourcekey="txt_ItemTypeResource2" CssClass="TextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="170px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="物品价值" meta:resourcekey="TemplateFieldResource4">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ItemPice" runat="server" Text='<%# Eval("UnitAmount") %>' 
                                            meta:resourcekey="lbl_ItemPiceResource2"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_ItemPice" runat="server" Text='<%# Eval("UnitAmount") %>' 
                                            style="text-transform:uppercase" meta:resourcekey="txt_ItemPiceResource2" CssClass="TextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="90px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="小计" meta:resourcekey="TemplateFieldResource5">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_SumPice" runat="server" Text='<%# Eval("TotalAmount") %>' 
                                            meta:resourcekey="lbl_SumPiceResource1"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" CancelText="取消" EditText="修改" 
                                    UpdateText="更新" meta:resourcekey="CommandFieldResource1" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:ButtonField CommandName="Delete" ButtonType="Link" Text="删除" 
                                    meta:resourcekey="ButtonFieldResource1" />
                            </Columns>
                            <FooterStyle />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="DataView" id="DvBox" runat="server">
            <tbody>
                <tr class="AlternatingRow">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_BoxPiece" runat="server" Text="件数：" 
                            meta:resourcekey="lbl_BoxPieceResource1"></asp:Label>
                    </td>
                    <td align="left" style="width:50px">
                        <asp:TextBox ID="txt_BoxPiece" runat="server" Width="50px" TabIndex="6" 
                            style="text-transform:uppercase" meta:resourcekey="txt_BoxPieceResource1" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_BoxWeight" runat="server" Text="重量(kg)：" 
                            meta:resourcekey="lbl_BoxWeightResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_BoxWeight" runat="server" Width="80px" TabIndex="7" 
                            style="text-transform:uppercase" meta:resourcekey="Txt_BoxWeightResource1" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_BoxHeight" runat="server" Text="高度(cm)：" 
                            meta:resourcekey="lbl_BoxHeightResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_BoxHeight" runat="server" Width="80px" TabIndex="8" 
                            style="text-transform:uppercase" meta:resourcekey="Txt_BoxHeightResource1" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_BoxLength" runat="server" Text="长度(cm)：" 
                            meta:resourcekey="lbl_BoxLengthResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_BoxLength" runat="server" Width="80px" TabIndex="9" 
                            style="text-transform:uppercase" meta:resourcekey="Txt_BoxLengthResource1" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_BoxWidth" runat="server" Text="宽度(cm)：" 
                            meta:resourcekey="lbl_BoxWidthResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_BoxWidth" runat="server" Width="80px" TabIndex="10" 
                            style="text-transform:uppercase" meta:resourcekey="Txt_BoxWidthResource1" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" colspan="3">
                        <asp:Button ID="but_AddBox" runat="server" Text="添加包裹" TabIndex="11" 
                            CssClass="InputBtn" OnClick="but_AddBox_Click" 
                            meta:resourcekey="but_AddBoxResource1" />
                    </td>
                </tr>
                <tr>
                    <td colspan="11" align="center">
                        <asp:GridView ID="gv_Box" runat="server" AutoGenerateColumns="False"
                            onrowediting="gv_Box_RowEditing" onrowupdating="gv_Box_RowUpdating" 
                            DataKeyNames="BoxID" onrowcommand="gv_Box_RowCommand" 
                            onrowcancelingedit="gv_Box_RowCancelingEdit" 
                            onrowdeleting="gv_Box_RowDeleting" meta:resourcekey="gv_BoxResource1">
                            <Columns>
                                <asp:TemplateField HeaderText="件数" meta:resourcekey="TemplateFieldResource6">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BoxPiece" runat="server" Text='<%# Eval("Piece") %>' 
                                            meta:resourcekey="lbl_BoxPieceResource2"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_BoxPiece" runat="server" Text='<%# Eval("Piece") %>' 
                                            style="text-transform:uppercase" meta:resourcekey="txt_BoxPieceResource2" CssClass="TextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="110px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="重量(kg)" 
                                    meta:resourcekey="TemplateFieldResource7">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BoxWeight" runat="server" Text='<%# Eval("Weight") %>' 
                                            meta:resourcekey="lbl_BoxWeightResource2"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_BoxWeight" runat="server" Text='<%# Eval("Weight") %>' 
                                            style="text-transform:uppercase" meta:resourcekey="txt_BoxWeightResource2" CssClass="TextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="110px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="高度(cm)" 
                                    meta:resourcekey="TemplateFieldResource8">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BoxHeight" runat="server" Text='<%# Eval("Height") %>' 
                                            meta:resourcekey="lbl_BoxHeightResource2"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_BoxHeight" runat="server" Text='<%# Eval("Height") %>' 
                                            style="text-transform:uppercase" meta:resourcekey="txt_BoxHeightResource2" CssClass="TextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="110px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="长度(cm)" 
                                    meta:resourcekey="TemplateFieldResource9">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BoxLength" runat="server" Text='<%# Eval("Length") %>' 
                                            meta:resourcekey="lbl_BoxLengthResource2"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_BoxLength" runat="server" Text='<%# Eval("Length") %>' 
                                            style="text-transform:uppercase" meta:resourcekey="txt_BoxLengthResource2" CssClass="TextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="110px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="宽度(cm)" 
                                    meta:resourcekey="TemplateFieldResource10">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BoxWidth" runat="server" Text='<%# Eval("Width") %>' 
                                            meta:resourcekey="lbl_BoxWidthResource2"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_BoxWidth" runat="server" Text='<%# Eval("Width") %>' 
                                            style="text-transform:uppercase" meta:resourcekey="txt_BoxWidthResource2" CssClass="TextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="110px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" ButtonType="Link" CancelText="取消" 
                                    EditText="修改" UpdateText="更新" meta:resourcekey="CommandFieldResource2">
                                <ControlStyle Width="60px" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:ButtonField CommandName="Delete" ButtonType="Link" Text="删除" 
                                    meta:resourcekey="ButtonFieldResource2" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="DataView">
            <tbody>
                <tr class="Row">
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_WeightType" runat="server" Text="计重类型：" 
                            meta:resourcekey="lbl_WeightTypeResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_WeightType" runat="server" TabIndex="12" 
                            meta:resourcekey="ddl_WeightTypeResource1">
                            <asp:ListItem Value="0" Text="实重" meta:resourcekey="ListItemResource10"></asp:ListItem>
                            <asp:ListItem Value="1" Text="泡重" meta:resourcekey="ListItemResource11"></asp:ListItem>
                            <asp:ListItem Value="2" Text="加权" meta:resourcekey="ListItemResource12"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_TotalWeight" runat="server" Text="总重量：" 
                            meta:resourcekey="lbl_TotalWeightResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="txt_TotalWeight" runat="server"
                            meta:resourcekey="txt_TotalWeightResource1"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_PieceTile" runat="server" Text="包裹件数：" 
                            meta:resourcekey="lbl_PieceTileResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_Piece" runat="server"
                            meta:resourcekey="lbl_PieceResource1"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_VolumeWeight" runat="server" Text="体积重："
                            meta:resourcekey="lbl_VolumeWeightResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_VolumeWeight" runat="server" meta:resourcekey="Txt_VolumeWeightResource1"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_Remark" runat="server" Text="备注：" 
                            meta:resourcekey="lbl_RemarkResource1"></asp:Label>
                    </td>
                    <td align="left" colspan="7">
                        <asp:TextBox ID="txt_Remark" runat="server" Width="500px" Height="100px" 
                            TextMode="MultiLine" TabIndex="13" style="text-transform:uppercase" 
                            meta:resourcekey="txt_RemarkResource1" CssClass="TextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_Carrier" runat="server" Text="承运公司名称：" 
                            meta:resourcekey="lbl_CarrierResource1"></asp:Label>
                    </td>
                    <td align="left" colspan="7">
                        <asp:TextBox ID="Txt_Carrier" runat="server" Width="422px" TabIndex="14"  MaxLength="40"
                            style="text-transform:uppercase" meta:resourcekey="Txt_CarrierResource1" CssClass="TextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" style="width:280px">
                        <asp:Label ID="lbl_CarrierHAWBBarCode" runat="server" Text="承运公司运单号：" 
                            meta:resourcekey="lbl_CarrierHAWBBarCodeResource1"></asp:Label>
                    </td>
                    <td align="left" colspan="7">
                        <asp:TextBox ID="Txt_CarrierHAWBBarCode" runat="server" Width="422px" 
                            TabIndex="15" MaxLength="40" style="text-transform:uppercase" 
                            meta:resourcekey="Txt_CarrierHAWBBarCodeResource1" CssClass="TextBox"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="FooterBtnBar">
            <asp:Button ID="But_AddHAWB" runat="server" Text="创 建" ValidationGroup="1" 
                TabIndex="16" CssClass="InputBtn"
                OnClick="But_AddHAWB_Click" meta:resourcekey="But_AddHAWBResource1" />
            <asp:Button ID="But_Rurnet" runat="server" Text="返 回" CssClass="InputBtn" 
                TabIndex="17" OnClick="But_Rurnet_Click" 
                meta:resourcekey="But_RurnetResource1" />
        </div>
    </div>
</asp:Content>
