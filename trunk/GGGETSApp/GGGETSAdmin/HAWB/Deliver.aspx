<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Deliver.aspx.cs" Inherits="GGGETSAdmin.HAWB.Deliver" Theme="logisitc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:PlaceHolder ID="phDeliver1" runat="server">
            <table class="DataView">
                <tbody>
                    <tr class="EditRow">
                        <td colspan="6" align="left">
                            <asp:Label ID="lbl_Deliver1" runat="server" Text="交付人信息"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            <asp:Label ID="lbl_DeliverName1" runat="server" Text="公司："></asp:Label>
                        </td>
                        <td align="left" colspan="5">
                            <asp:TextBox ID="Txt_DeliverName1" runat="server" Width="630" TabIndex="22"></asp:TextBox>
                            <b style="color: Red">*</b>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            <asp:Label ID="lbl_DeliverAddress1" runat="server" Text="地址："></asp:Label>
                        </td>
                        <td align="left" colspan="5">
                            <asp:TextBox ID="Txt_DeliverAddress1" runat="server" Width="630" TabIndex="23" TextMode="MultiLine"></asp:TextBox>
                            <b style="color: Red">*</b>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            <asp:Label ID="lbl_DeliverCountry1" runat="server" Text="国家："></asp:Label>
                        </td>
                        <td align="left" class="style2">
                            <asp:TextBox ID="Txt_DeliverCountry1" runat="server" Width="80" TabIndex="24"></asp:TextBox>
                        </td>
                        <td class="FieldHeader">
                            <asp:Label ID="lbl_DeliverRegion1" runat="server" Text="省份："></asp:Label>
                        </td>
                        <td align="left" class="style1">
                            <asp:TextBox ID="Txt_DeliverRegion1" runat="server" Width="80" TabIndex="25"></asp:TextBox>
                        </td>
                        <td class="FieldHeader">
                            <asp:Label ID="lbl_DeliverCity1" runat="server" Text="城市："></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_DeliverCity1" runat="server" Width="80" TabIndex="26"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            <asp:Label ID="lbl_DeliverZipCode1" runat="server" Text="邮编："></asp:Label>
                        </td>
                        <td align="left" class="style2">
                            <asp:TextBox ID="Txt_DeliverZipCode1" runat="server" Width="80" TabIndex="27"></asp:TextBox>
                        </td>
                        <td class="FieldHeader">
                            <asp:Label ID="lbl_DeliverContactor1" runat="server" Text="姓名："></asp:Label>
                        </td>
                        <td align="left" class="style1">
                            <asp:TextBox ID="Txt_DeliverContactor1" runat="server" Width="80" TabIndex="28"></asp:TextBox>
                            <b style="color: Red">*</b>
                        </td>
                        <td class="FieldHeader">
                            <asp:Label ID="lbl_DeliverTel1" runat="server" Text="电话："></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Txt_DeliverTel1" runat="server" Width="80" TabIndex="29"></asp:TextBox>
                            <b style="color: Red">*</b>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td align="center" colspan="6">
                            <asp:LinkButton ID="lbtn_AddDeliver" runat="server" CssClass="LinkBtn" Text="添 加"></asp:LinkButton>
                            <a href="javascript:close()" class="LinkBtn" >取 消</a>
                            
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
