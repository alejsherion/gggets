<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HAWBInternal.ascx.cs"
    Inherits="GGGETSAdmin.Control.HAWBInternal" %>
<style>
    .style1
    {
        width: 74px;
    }
    .style2
    {
        width: 90px;
    }
    #login
    {
        position: relative;
        display: none;
        top: 10px;
        text-align: center;
        border: solid 1px;
        padding: 10px;
        z-index: 1;
    }
    
    body
    {
    }
    .STYLE1
    {
    }
</style>
<div class="FunctionBar">
    <div class="DivFloatLeft TipTab">
        <a></a>
    </div>
    <div class="DivFloatLeft">
        <select>
            <option value="">国际快递综合服务平台 >> </option>
        </select>
    </div>
    <div class="DivFloatRight">
        <asp:LinkButton ID="btnAdd" CssClass="LinkBtn" runat="server">Create a Formula Domain</asp:LinkButton>
    </div>
    <div class="Clear">
    </div>
</div>
<div>
    <table class="DataView">
        <thead>
            <tr class="Header">
                <th colspan="6">
                    <%----%>
                    <%--<asp:Label ID="lbl_Orede" runat="server"></asp:Label>--%>
                    <a>William</a>
                </th>
            </tr>
        </thead>
        <tbody>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_BarCode" runat="server" Text="运单号："></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="Txt_BarCode" runat="server" Width="300" TabIndex="1"></asp:TextBox><b
                        style="color: Red">*</b>
                </td>
            </tr>
            <tr class="AlternatingRow">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Account" runat="server" Text="客户帐号：" Width="80"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="Txt_Account1" runat="server" Width="80" TabIndex="2"></asp:TextBox>-
                    <asp:TextBox ID="Txt_Account2" runat="server" Width="50" TabIndex="3"></asp:TextBox><b
                        style="color: Red">*</b>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_SettleType" runat="server" Text="结算方式：" Width="80"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DDl_SettleType" runat="server">
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
                    <asp:TextBox ID="Txt_ShipperName" runat="server" Width="525" TabIndex="5"></asp:TextBox><b
                        style="color: Red">*</b>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ShipperAddress" runat="server" Text="地址："></asp:Label>
                </td>
                <td align="left" colspan="9">
                    <asp:TextBox ID="Txt_ShipperAddress" runat="server" Width="98%" TabIndex="6" TextMode="MultiLine"></asp:TextBox><b
                        style="color: Red">*</b>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ShipperCountry" runat="server" Text="国家："></asp:Label>
                </td>
                <td align="left" class="style2">
                    <asp:TextBox ID="Txt_ShipperCountry" runat="server" Width="80" TabIndex="7" MaxLength="2"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ShipperRegion" runat="server" Text="省份："></asp:Label>
                </td>
                <td align="left" class="style1">
                    <asp:TextBox ID="Txt_ShipperRegion" runat="server" Width="80" TabIndex="8"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_City" runat="server" Text="城市："></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="Txt_City" runat="server" Width="80" TabIndex="9"></asp:TextBox>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ShipperZipCode" runat="server" Text="邮编："></asp:Label>
                </td>
                <td align="left" class="style2">
                    <asp:TextBox ID="Txt_ShipperZipCode" runat="server" Width="80" TabIndex="10"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ShipperContactor" runat="server" Text="姓名："></asp:Label>
                </td>
                <td align="left" class="style1">
                    <asp:TextBox ID="Txt_ShipperContactor" runat="server" Width="80" TabIndex="11"></asp:TextBox>
                    <b style="color: Red">*</b>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ShipperTel" runat="server" Text="电话："></asp:Label>
                </td>
                <td align="left" style="width:auto">
                    <asp:TextBox ID="Txt_ShipperTel" runat="server" Width="80" TabIndex="12"></asp:TextBox>
                    <b style="color: Red">*</b>
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
                    <asp:TextBox ID="Txt_ConsigneeName" runat="server" Width="525px" TabIndex="14"></asp:TextBox><b
                        style="color: Red">*</b>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeAddress" runat="server" Text="地址："></asp:Label>
                </td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="Txt_ConsigneeAddress" runat="server" Width="98%" TabIndex="15"
                        TextMode="MultiLine"></asp:TextBox><b style="color: Red">*</b>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeCountry" runat="server" Text="国家："></asp:Label>
                </td>
                <td align="left" class="style2">
                    <asp:TextBox ID="Txt_ConsigneeCountry" runat="server" Width="80" TabIndex="16"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeRegion" runat="server" Text="省份："></asp:Label>
                </td>
                <td align="left" class="style1">
                    <asp:TextBox ID="Txt_ConsigneeRegion" runat="server" Width="80" TabIndex="17"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeCity" runat="server" Text="城市："></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="Txt_ConsigneeCity" runat="server" Width="80" TabIndex="18"></asp:TextBox>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeZipCode" runat="server" Text="邮编："></asp:Label>
                </td>
                <td align="left" class="style2">
                    <asp:TextBox ID="Txt_ConsigneeZipCode" runat="server" Width="80" TabIndex="19"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeContactor" runat="server" Text="姓名："></asp:Label>
                </td>
                <td align="left" class="style1">
                    <asp:TextBox ID="Txt_ConsigneeContactor" runat="server" Width="80" TabIndex="20"></asp:TextBox>
                    <b style="color: Red">*</b>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_ConsigneeTel" runat="server" Text="电话："></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="Txt_ConsigneeTel" runat="server" Width="80" TabIndex="21"></asp:TextBox>
                    <b style="color: Red">*</b>
                </td>
            </tr>
        </tbody>
    </table>
</div>
<div class="FooterBtnBar">
    <asp:Button ID="But_Next" runat="server" Text="下一页" CssClass="InputBtn" TabIndex="30" />
    <asp:Button ID="But_Conel" runat="server" Text="重  填" CssClass="InputBtn" TabIndex="31"
        OnClick="But_Conel_Click" />
</div>
