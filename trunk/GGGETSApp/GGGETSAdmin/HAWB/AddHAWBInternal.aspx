<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddHAWBInternal.aspx.cs" Inherits="GGGETSAdmin.HAWB.AddHAWBInernal" Theme="logisitc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:PlaceHolder ID="phHAWB_Info" runat="server">
            <table class="DataView" cellspacing="1" cellpadding="3">
                <tbody>
                    <tr class="Row">
                        <td class="FieldHeader">
                            条形码：
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="Txt_BarCode" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <b style="color: #FF0000;">*</b>
                            <asp:RequiredFieldValidator ID="Rfv_BarCode" runat="server" ErrorMessage="*" ForeColor="Red"
                                ControlToValidate="Txt_BarCode" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:LinkButton ID="btnScan" CssClass="LinkBtn" runat="server">识别</asp:LinkButton>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader" style="width: 150px">
                            承运公司名称：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_Carrier" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <b style="color: #FF0000;">*</b>
                            <asp:RequiredFieldValidator ID="Rfv_Carrier" runat="server" ErrorMessage="*" ForeColor="Red"
                                ControlToValidate="Txt_Carrier" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                        <td class="FieldHeader" style="width: 150px">
                            承运公司运单号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_CarrierHAWBID" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <b style="color: #FF0000;">*</b>
                            <asp:RequiredFieldValidator ID="Rfv_Account" runat="server" ErrorMessage="*" ForeColor="Red"
                                ControlToValidate="Txt_Account" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            客户帐号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_Account" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <b style="color: #FF0000;">*</b>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                ForeColor="Red" ControlToValidate="Txt_Account" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                        <td class="FieldHeader">
                            负责人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_Owner" runat="server" Width="250px" CssClass="TextBox" Enabled="false"></asp:TextBox>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader">
                            结算方式：
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="DDl_SettleType" runat="server" Width="250px">
                                <asp:ListItem>预付月结</asp:ListItem>
                                <asp:ListItem>预付现结</asp:ListItem>
                                <asp:ListItem>到付月结</asp:ListItem>
                                <asp:ListItem>到付现结</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="FieldHeader">
                            服务方式：
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="DDl_ServiceType" runat="server" Width="250px">
                                <asp:ListItem>空运</asp:ListItem>
                                <asp:ListItem>快件</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="EditRow">
                        <td align="right">
                            发件用户：
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="Txt_ShipperID" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            发件地区码：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ShipperRegion" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="Rev_ShipperRegion" runat="server" ControlToValidate="Txt_ShipperRegion"
                                ErrorMessage="只能输入字母且只能为3位！" ValidationExpression="^[a-zA-Z]{3}$" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td class="FieldHeader">
                            发件联系人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ShipperContactor" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <b style="color: #FF0000;">*</b>
                            <asp:RequiredFieldValidator ID="Rfv_ShipperContacto" runat="server" ErrorMessage="*"
                                ForeColor="Red" ControlToValidate="Txt_ShipperContactor" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader">
                            发件联系电话：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ShipperTel" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <b style="color: #FF0000;">*</b>
                            <asp:RequiredFieldValidator ID="Rfv_ShipperTel" runat="server" ErrorMessage="*" ForeColor="Red"
                                ControlToValidate="Txt_ShipperTel" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="Rev_ShipperTel" runat="server" ControlToValidate="Txt_ShipperTel"
                                ErrorMessage="电话号码有误！" ValidationExpression="\d{3,4}-\d{7,8}-[0-9]*$|^[0-9]*$|\d{3,4}-\d{7,8}"
                                ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td class="FieldHeader">
                            发件邮政编码：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ShipperZipCode" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="Rev_ShipperZipCode" runat="server" ControlToValidate="Txt_ShipperZipCode"
                                ErrorMessage="输入正确的邮编！" ValidationExpression="\d{6}" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            发件人地址：
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="Txt_ShipperAddress" Width="500px" Rows="4" TextMode="MultiLine"
                                CssClass="TextBox" runat="server"></asp:TextBox>
                            <b style="color: #FF0000;">*</b>
                            <asp:RequiredFieldValidator ID="Rfv_ShipperAddress" runat="server" ErrorMessage="*"
                                ForeColor="Red" ControlToValidate="Txt_ShipperAddress" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="EditRow">
                        <td align="right">
                            <a href="#">+</a> 收件用户：
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="Txt_ConsigneeID" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <%--<asp:DropDownList ID="DropDownList1" Width="500px" runat="server">
                            </asp:DropDownList>--%>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            收件地区码：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ConsigneeRegion" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="Rev_ConsigneeRegion" runat="server" ControlToValidate="Txt_ConsigneeRegion"
                                ErrorMessage="只能输入字母且只能为3位！" ValidationExpression="^[a-zA-Z]{3}$" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td class="FieldHeader">
                            收件联系人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ConsigneeContactor" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <b style="color: #FF0000;">*</b>
                            <asp:RequiredFieldValidator ID="Rfv_ConsigneeContactor" runat="server" ErrorMessage="*"
                                ForeColor="Red" ControlToValidate="Txt_ConsigneeContactor" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader">
                            收件联系电话：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ConsigneeTel" Width="250px" MaxLength="20" CssClass="TextBox"
                                runat="server"></asp:TextBox>
                            <b style="color: #FF0000;">*</b>
                            <asp:RequiredFieldValidator ID="Rfv_ConsigneeTel" runat="server" ErrorMessage="*"
                                ForeColor="Red" ControlToValidate="Txt_ConsigneeTel" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="Rev_ConsigneeTel" runat="server" ControlToValidate="Txt_ConsigneeTel"
                                ErrorMessage="电话号码有误！" ValidationExpression="\d{3,4}-\d{7,8}-[0-9]*$|^[0-9]*$|\d{3,4}-\d{7,8}"
                                ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td class="FieldHeader">
                            收件邮政编码：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ConsigneeZipCode" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="Rev_ConsigneeZipCode" runat="server" ControlToValidate="Txt_ConsigneeZipCode"
                                ErrorMessage="输入正确的邮编！" ValidationExpression="\d{6}" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            发件人地址：
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="Txt_ConsigneeAddress" Width="500px" Rows="4" TextMode="MultiLine"
                                CssClass="TextBox" runat="server"></asp:TextBox>
                            <b style="color: #FF0000;">*</b>
                            <asp:RequiredFieldValidator ID="Rfv_ConsigneeAddress" runat="server" ErrorMessage="*"
                                ForeColor="Red" ControlToValidate="Txt_ConsigneeAddress" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phHAWB_Item" runat="server">
            <table class="DataView" cellspacing="0" cellpadding="3">
                <thead>
                    <tr class="Header">
                        <th colspan="4">
                            订单关联货物信息
                            <asp:Button ID="But_AddItem" runat="server" Text="添加货物信息" CssClass="InputBtn" OnClick="But_AddItme_Click"/>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="AlternatingRow">
                        <td colspan="4">
                            <asp:GridView ID="Gv_BaleXinXi" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="编号">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_n" runat="server" Text='<%# N() %>'></asp:Label>
                                            <asp:Label ID="lbl_Order" runat="server" Text='<%# Eval("HID") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="重量">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Weight" runat="server" Text='<%# Eval("Weight") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Txt_Weight" runat="server" Text='<%# Eval("Weight") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="高度" DataField="Height">
                                        <ControlStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="宽度" DataField="Width">
                                        <ControlStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="长度" DataField="Length">
                                        <ControlStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="支付费用" DataField="TransPays">
                                        <ControlStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="支付货币" DataField="TransCurrency">
                                        <ControlStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="编辑 " ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbt_Eit" runat="server" CausesValidation="False" CommandName="Edit"
                                                Text="修改" CssClass="LinkBtn"></asp:LinkButton>
                                                <asp:LinkButton ID="lbt_Up" runat="server" CausesValidation="False" CommandName="Update"
                                                Text="更新" CssClass="LinkBtn" Visible="false"></asp:LinkButton>
                                                <asp:LinkButton ID="lbt_cancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                Text="取消" CssClass="LinkBtn" Visible="false"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除 " ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbt_Delete" runat="server" CausesValidation="False" CommandName="Delete"
                                                Text="删除" CssClass="LinkBtn" OnClientClick="return confirm('你确定要删除吗？')"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr class="EditRow">
                        <td style="width: 150px">
                            重量：
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="DDl_WeightType" runat="server">
                                    <asp:ListItem>实重</asp:ListItem>
                                    <asp:ListItem>泡重</asp:ListItem>
                                    <asp:ListItem>加权</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                            <asp:TextBox ID="Txt_Weight" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td style="width: 150px">
                            件数：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_Piece" Width="250" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader">
                            特别通关指示：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="Rbl_" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">有</asp:ListItem>
                                <asp:ListItem>无</asp:ListItem>
                                <asp:ListItem>其它</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="FieldHeader">
                            关税结算方式：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_Taxes" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            运单截止日期：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_DeadlineTime" runat="server" onfocusin="calendar()" Width="104px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="Rev_DeadlineTime" runat="server" ControlToValidate="Txt_DeadlineTime"
                                ErrorMessage="请输入正确的日期！" ValidationExpression="^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))(\s(((0?[0-9])|([1-2][0-3]))\:([0-5]?[0-9])((\s)|(\:([0-5]?[0-9])))))?$"
                                ForeColor="Red" Font-Size="Large"></asp:RegularExpressionValidator>
                        </td>
                        <td class="FieldHeader">
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader">
                            运单内容：
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="Txt_Description" Width="500px" Rows="3" TextMode="MultiLine" CssClass="TextBox"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader">
                            运单备注：
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="Txt_Remark" Width="500px" Rows="3" TextMode="MultiLine" CssClass="TextBox"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:PlaceHolder>
        <div class="FooterBtnBar">
            <asp:Button ID="But_AddHAWB" runat="server" Text="提 交" OnClick="But_AddHAWB_Click"
                ValidationGroup="1" CssClass="InputBtn" />
            <asp:Button ID="But_Rurnet" runat="server" Text="返 回" OnClick="But_Rurnet_Click"
                CssClass="InputBtn" />
        </div>
    </div>
    </form>
</body>
</html>
