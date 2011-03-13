<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="HAWBItemAdd.aspx.cs" Inherits="GGGETSAdmin.HAWBManage.HAWBItemAdd" Theme="logisitc"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="DataView">
            <thead>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_BarCodeTitle" runat="server" Text="运单号："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_BarCode" runat="server" Width="250"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_ShipperCountryTitle" runat="server" Text="发件地区三字码："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_ShipperRegion" runat="server" Width="50"></asp:Label>
                    </td>
                    <td style="width:250px">
                        <asp:Label ID="lbl_ConsigneeRegionTitle" runat="server" Text="收件地区三字码："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_ConsigneeRegion" runat="server" Width="50"></asp:Label>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right" style="width:120px">
                        <asp:Label ID="lbl_ShipperNameTitle" runat="server" Text="发件公司："></asp:Label>
                    </td>
                    <td colspan="8" align="left">
                        <asp:Label ID="lbl_ShipperName" runat="server" Width="350"></asp:Label>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_ConsigneeNameTitle" runat="server" Text="收件公司："></asp:Label>
                    </td>
                    <td colspan="8" align="left">
                        <asp:Label ID="lbl_ConsigneeName" runat="server" Width="350"></asp:Label>
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_BoxType" runat="server" Text="包裹类型："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rbt_BoxType" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="True" OnSelectedIndexChanged="rbt_BoxType_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="文件"></asp:ListItem>
                            <asp:ListItem Selected="True"  Value="1" Text="小包裹"></asp:ListItem>
                            <asp:ListItem Value="2" Text="普货"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="FieldHeader" style="width:100px">
                        <asp:Label ID="lbl_BillTax" runat="server" Text="付款人："></asp:Label>
                    </td>
                    <td align="left" style="width:400px">
                        <asp:RadioButtonList runat="server" ID="rbt_BillTax" RepeatDirection="Horizontal" BorderStyle="None">
                            <asp:ListItem Selected="True" Value="0" Text="发件人"></asp:ListItem>
                            <asp:ListItem Value="1" Text="收件人"></asp:ListItem>
                            <asp:ListItem Value="2" Text="第三方"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_SpecialInstruction" runat="server" Text="通关指示："></asp:Label>
                    </td>
                    <td align="left" colspan="4">
                        <asp:RadioButtonList ID="Rbl_SpecialInstruction" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="有" Text="有"></asp:ListItem>
                            <asp:ListItem Value="无" Text="无" Selected="True" ></asp:ListItem>
                            <asp:ListItem Value="其它" Text="其它"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="DataView" id="Dvitem" runat="server">
            <tbody>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="left" style="width:80px">
                        <asp:Label ID="lbl_ItemPiece" runat="server" Text="件数："></asp:Label>
                    </td>
                    <td align="left" style="width:50px">
                        <asp:TextBox ID="Txt_ItemPiece" runat="server" Width="50px" TabIndex="1" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_ItemName" runat="server" Text="物品名称："></asp:Label>
                    </td>
                    <td align="left" style="width:200px">
                        <asp:TextBox ID="Txt_ItemName" runat="server" Width="160" TabIndex="2" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_ItemType" runat="server" Text="物品类型："></asp:Label>
                    </td>
                    <td align="left" style="width:200px">
                        <asp:TextBox ID="Txt_ItemType" runat="server" Width="160" TabIndex="3" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_ItemPice" runat="server" Text="物品价值："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_ItemPice" runat="server" Width="50px" TabIndex="4" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" colspan="3">
                        <asp:Button ID="but_AddItem" runat="server" Text="添加物品" TabIndex="5" CssClass="InputBtn" OnClick="but_AddItem_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="11" align="center">
                        <asp:GridView ID="GV_item" runat="server" AutoGenerateColumns="False" 
                            onrowcancelingedit="GV_item_RowCancelingEdit" onrowcommand="GV_item_RowCommand" 
                            onrowdeleting="GV_item_RowDeleting" onrowediting="GV_item_RowEditing" 
                            onrowupdating="GV_item_RowUpdating" DataKeyNames="ItemID" 
                            ShowFooter="True" onrowdatabound="GV_item_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="件数">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ItemPiece" runat="server" Text='<%# Eval("Piece") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_ItemPiece" runat="server" Text='<%# Eval("Piece") %>' style="text-transform:uppercase"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="90px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="物品名称">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ItemName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_ItemName" runat="server" Text='<%# Eval("Name") %>' style="text-transform:uppercase"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="170px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="物品类型">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ItemType" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_ItemType" runat="server" Text='<%# Eval("Remark") %>' style="text-transform:uppercase"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="170px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="物品价值">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ItemPice" runat="server" Text='<%# Eval("UnitAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_ItemPice" runat="server" Text='<%# Eval("UnitAmount") %>' style="text-transform:uppercase"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="90px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="小计">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_SumPice" runat="server" Text='<%# Eval("TotalAmount")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" CancelText="取消" EditText="修改" 
                                    UpdateText="更新" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:ButtonField CommandName="Delete" ButtonType="Link" Text="删除" />
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
                        <asp:Label ID="lbl_BoxPiece" runat="server" Text="件数："></asp:Label>
                    </td>
                    <td align="left" style="width:50px">
                        <asp:TextBox ID="txt_BoxPiece" runat="server" Width="50" TabIndex="6" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_BoxWeight" runat="server" Text="重量(kg)："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_BoxWeight" runat="server" Width="80" TabIndex="7" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_BoxHeight" runat="server" Text="高度(cm)："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_BoxHeight" runat="server" Width="80" TabIndex="8" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_BoxLength" runat="server" Text="长度(cm)："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_BoxLength" runat="server" Width="80" TabIndex="9" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_BoxWidth" runat="server" Text="宽度(cm)："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_BoxWidth" runat="server" Width="80" TabIndex="10" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" colspan="3">
                        <asp:Button ID="but_AddBox" runat="server" Text="添加包裹" TabIndex="11" CssClass="InputBtn" OnClick="but_AddBox_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="11" align="center">
                        <asp:GridView ID="gv_Box" runat="server" AutoGenerateColumns="False"
                            onrowediting="gv_Box_RowEditing" onrowupdating="gv_Box_RowUpdating" 
                            DataKeyNames="BoxID" onrowcommand="gv_Box_RowCommand" 
                            onrowcancelingedit="gv_Box_RowCancelingEdit" onrowdeleting="gv_Box_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="件数">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BoxPiece" runat="server" Text='<%# Eval("Piece") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_BoxPiece" runat="server" Text='<%# Eval("Piece") %>' style="text-transform:uppercase"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="110px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="重量(kg)">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BoxWeight" runat="server" Text='<%# Eval("Weight") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_BoxWeight" runat="server" Text='<%# Eval("Weight") %>' style="text-transform:uppercase"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="110px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="高度(cm)">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BoxHeight" runat="server" Text='<%# Eval("Height") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_BoxHeight" runat="server" Text='<%# Eval("Height") %>' style="text-transform:uppercase"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="110px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="长度(cm)">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BoxLength" runat="server" Text='<%# Eval("Length") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_BoxLength" runat="server" Text='<%# Eval("Length") %>' style="text-transform:uppercase"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="110px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="宽度(cm)">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BoxWidth" runat="server" Text='<%# Eval("Width") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_BoxWidth" runat="server" Text='<%# Eval("Width") %>' style="text-transform:uppercase"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle Width="110px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" ButtonType="Link" CancelText="取消" 
                                    EditText="修改" UpdateText="更新">
                                <ControlStyle Width="60px" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:ButtonField CommandName="Delete" ButtonType="Link" Text="删除" />
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
                        <asp:Label ID="lbl_WeightType" runat="server" Text="计重类型："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_WeightType" runat="server" TabIndex="12">
                            <asp:ListItem Value="0" Text="实重"></asp:ListItem>
                            <asp:ListItem Value="1" Text="泡重"></asp:ListItem>
                            <asp:ListItem Value="2" Text="加权"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_TotalWeight" runat="server" Text="总重量："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="txt_TotalWeight" runat="server" CssClass="TextBox"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_PieceTile" runat="server" Text="包裹件数："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_Piece" runat="server" CssClass="TextBox"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_VolumeWeight" runat="server" Text="体积重：" Visible="false"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_VolumeWeight" runat="server" CssClass="TextBox" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_Remark" runat="server" Text="备注："></asp:Label>
                    </td>
                    <td align="left" colspan="7">
                        <asp:TextBox ID="txt_Remark" runat="server" Width="500" Height="100" TextMode="MultiLine" TabIndex="13" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_Carrier" runat="server" Text="承运公司名称："></asp:Label>
                    </td>
                    <td align="left" colspan="7">
                        <asp:TextBox ID="Txt_Carrier" runat="server" Width="422px" TabIndex="14" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" style="width:280px">
                        <asp:Label ID="lbl_CarrierHAWBBarCode" runat="server" Text="承运公司运单号："></asp:Label>
                    </td>
                    <td align="left" colspan="7">
                        <asp:TextBox ID="Txt_CarrierHAWBBarCode" runat="server" Width="422px" TabIndex="15" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="FooterBtnBar">
            <asp:Button ID="But_AddHAWB" runat="server" Text="创 建" ValidationGroup="1" TabIndex="16" CssClass="InputBtn"
                OnClick="But_AddHAWB_Click" />
            <asp:Button ID="But_Rurnet" runat="server" Text="返 回" CssClass="InputBtn" TabIndex="17" OnClick="But_Rurnet_Click" />
        </div>
    </div>
</asp:Content>
