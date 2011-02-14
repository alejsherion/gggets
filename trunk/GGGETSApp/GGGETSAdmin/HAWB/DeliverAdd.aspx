<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliverAdd.aspx.cs" Inherits="GGGETSAdmin.HAWB.DeliverAdd" Theme="logisitc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table class="DataView">
            <tbody id="tbDeliver" runat="server">
                <tr class="EditRow">
                    <td colspan="6" align="left">
                        <asp:Label ID="lbl_Deliver" runat="server" Text="交付人信息"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverName" runat="server" Text="公司:"></asp:Label>
                    </td>
                    <td align="left" colspan="5">
                        <asp:TextBox ID="Txt_DeliverName" runat="server" Width="500" TabIndex="22"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverAddress" runat="server" Text="地址:"></asp:Label>
                    </td>
                    <td align="left" colspan="5">
                        <asp:TextBox ID="Txt_DeliverAddress" runat="server" Width="500" TabIndex="23" TextMode="MultiLine"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverCountry" runat="server" Text="国家:"></asp:Label>
                    </td>
                    <td align="left" class="style2">
                        <asp:TextBox ID="Txt_DeliverCountry" runat="server" Width="80" TabIndex="24" 
                            MaxLength="2"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverRegion" runat="server" Text="省份:"></asp:Label>
                    </td>
                    <td align="left" class="style1">
                        <asp:TextBox ID="Txt_DeliverRegion" runat="server" Width="80" TabIndex="25" 
                            MaxLength="3"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverCity" runat="server" Text="城市:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_DeliverCity" runat="server" Width="80" TabIndex="26"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverZipCode" runat="server" Text="邮编:"></asp:Label>
                    </td>
                    <td align="left" class="style2">
                        <asp:TextBox ID="Txt_DeliverZipCode" runat="server" Width="80" TabIndex="27"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverContactor" runat="server" Text="姓名:"></asp:Label>
                    </td>
                    <td align="left" class="style1">
                        <asp:TextBox ID="Txt_DeliverContactor" runat="server" Width="80" TabIndex="28"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverTel" runat="server" Text="电话:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_DeliverTel" runat="server" Width="80" TabIndex="29"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="Row">
                    <td align="center" colspan="6">
                        <asp:Button ID="btn_AddDeliver" runat="server" CssClass="InputBtn" Text="添 加" 
                            onclick="btn_AddDeliver_Click" />
                        <asp:Button ID="btn_Close" runat="server" CssClass="InputBtn" Text="取 消" OnClientClick="javascript:window.close()" />
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>