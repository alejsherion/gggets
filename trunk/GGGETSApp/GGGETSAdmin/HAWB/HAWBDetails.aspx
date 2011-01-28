<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HAWBDetails.aspx.cs" Inherits="GGGETSAdmin.HAWB.HAWBDetails"
    Theme="logisitc" %>

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
                <thead>
                    <tr class="Header">
                        <th colspan="4">
                            订单号：<asp:Label ID="lbl_Hid" runat="server"></asp:Label>
                             | Owner: William
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="Row">
                        <td class="FieldHeader">
                            条形码：
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="Txt_BarCode" runat="server" Enabled="false" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <asp:LinkButton ID="btnScan" CssClass="LinkBtn" runat="server">识别</asp:LinkButton>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader" style="width: 150px">
                            承运公司名称：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_Carrier" runat="server" Enabled="false" Width="250px" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader" style="width: 150px">
                            承运公司运单号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_CarrierHAWBID" runat="server" Enabled="false" Width="250px"
                                CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            客户帐号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_Account" runat="server" Enabled="false" Width="250px" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader">
                            负责人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_Owner" runat="server" Enabled="false" Width="250px" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader">
                            结算方式：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ServiceType" runat="server" Enabled="false" Width="250px" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader">
                            服务方式：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_SettleType" runat="server" Enabled="false" Width="250px" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="EditRow">
                        <td align="right">
                            发件用户：
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="Txt_ShipperID" runat="server" Enabled="false" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <%--<asp:DropDownList ID="ddl1" Width="500px" runat="server">
                            </asp:DropDownList>--%>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            发件国家/地区码：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ShipperCountry" runat="server" Enabled="false" Width="100px"
                                CssClass="TextBox"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="Txt_ShipperRegion" runat="server" Enabled="false" Width="110px"
                                CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader">
                            发件联系人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ShipperContactor" runat="server" Enabled="false" Width="250px"
                                CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader">
                            发件联系电话：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ShipperTel" runat="server" Enabled="false" Width="250px" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader">
                            发件邮政编码：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ShipperZipCode" runat="server" Enabled="false" Width="250px"
                                CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            发件人地址：
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="Txt_ShipperAddress" Width="500px" Rows="4" TextMode="MultiLine"
                                CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="EditRow">
                        <td align="right">
                            <a href="#">+</a> 收件用户：
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="Txt_ConsigneeID" runat="server" Enabled="false" Width="250px" CssClass="TextBox"></asp:TextBox>
                            <%--<asp:DropDownList ID="DropDownList1" Width="500px" runat="server">
                            </asp:DropDownList>--%>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            收件国家/地区码：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ConsigneeCountry" runat="server" Enabled="false" Width="100px"
                                CssClass="TextBox"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="Txt_ConsigneeRegion" runat="server" Enabled="false" Width="110px"
                                CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader">
                            收件联系人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ConsigneeContactor" runat="server" Enabled="false" Width="250px"
                                CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader">
                            收件联系电话：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ConsigneeTel" Width="250px" MaxLength="20" CssClass="TextBox"
                                runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="FieldHeader">
                            收件邮政编码：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_ConsigneeZipCode" runat="server" Enabled="false" Width="250px"
                                CssClass="TextBox" MaxLength="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            收件人地址：
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="Txt_ConsigneeAddress" Width="500px" Rows="4" TextMode="MultiLine"
                                CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
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
                                           <%-- <%= n++ %>--%>
                                            <asp:Label ID="lbl_n" runat="server" Text='<%# N() %>'></asp:Label>
                                            <%--<asp:Label ID="lbl_Order" runat="server" Text='<%# Eval("cTel") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="重量(KG)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Weight" runat="server" Text='<%# Eval("Weight") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="高度(CM)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Hight" runat="server" Text='<%# Eval("Height") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="宽度(CM)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Width" runat="server" Text='<%# Eval("Width") %>'></asp:Label>
                                        </ItemTemplate>                               
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="长度(CM)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Length" runat="server" Text='<%# Eval("Length") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="支付费用">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_TransPays" runat="server" Text='<%# Eval("TransPays") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="支付货币">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_TransCurrency" runat="server" Text='<%# Eval("TransCurrency") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <%--<EmptyDataTemplate>
                        <table border="1">
                            <tr>
                                <td>
                                    编号
                                </td>
                                <td>
                                    重量
                                </td>
                                <td>
                                    高度
                                </td>
                                <td>
                                    长度
                                </td>
                                <td>
                                    编辑
                                </td>
                                <td>
                                    删除
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    无数据
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>--%>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader" style="width: 150px">
                            重量：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_WeightType" runat="server" Enabled="false" Width="100px" CssClass="TextBox"></asp:TextBox>
                            <asp:TextBox ID="Txt_Weight" runat="server" Enabled="false" Width="150px" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader" style="width: 150px">
                            件数：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_Piece" Width="250" Enabled="false" CssClass="TextBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader">
                            特别通关指示：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="Rbl_" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Selected="True">有</asp:ListItem>
                                <asp:ListItem>无</asp:ListItem>
                                <asp:ListItem>其它</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="FieldHeader">
                            关税结算方式：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_Taxes" runat="server" Enabled="false" Width="250px" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            最后更新时间：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_UpdateTime" Width="250px" Enabled="false" CssClass="TextBox"
                                runat="server"></asp:TextBox>
                        </td>
                        <td class="FieldHeader">
                            运单创建时间：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_CreateTime" Width="250px" Enabled="false" CssClass="TextBox"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader">
                            运单状态：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_State" runat="server" Enabled="false" Width="250px" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader">
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            运单内容：
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="Txt_Description" Width="500px" Rows="3" TextMode="MultiLine" Enabled="false"
                                CssClass="TextBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader">
                            运单备注：
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="Txt_Remark" Width="500px" Rows="3" TextMode="MultiLine" Enabled="false"
                                CssClass="TextBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:PlaceHolder>
        <div class="FooterBtnBar">
            <asp:Button ID="But_Conel" runat="server" Text="返 回" OnClick="But_Conel_Click" CssClass="InputBtn" />
        </div>
    </div>
    </form>
</body>
</html>
