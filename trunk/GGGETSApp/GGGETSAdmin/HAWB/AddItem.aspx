<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddItem.aspx.cs" Inherits="GGGETSAdmin.HAWB.AddItem" Theme="logisitc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="width:500px; height:500px">
    <div>
        <table class="DataView" cellspacing="0" cellpadding="5">
            <thead>
                <tr class="Header">
                    <th colspan="2">
                        货物详细信息
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr class="Row">
                    <td class="FieldHeader">
                        物品重量:
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="Txt_ItemWeight" runat="server"  Width="100"></asp:TextBox>
                 <asp:RequiredFieldValidator
                    ID="Rfv_weifh" runat="server" ErrorMessage="*" 
                    ForeColor="Red" ControlToValidate="Txt_ItemWeight" ValidationGroup="1"></asp:RequiredFieldValidator> 
                     <asp:RegularExpressionValidator ID="Rev_weifh" runat="server" 
                         ControlToValidate="Txt_ItemWeight" ErrorMessage="只能输入整数或小数，且小数点最多两位！" 
                         ValidationExpression="^[0]\.?[0-9]{0,2}$|[1-9]+\.?[0-9]{0,2}$" ForeColor="Red" 
                         ValidationGroup="1"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader">
                        物品长度:
                    </td>
                    <td style="text-align: left">
                       <asp:TextBox ID="Txt_ItemLength" runat="server"  Width="100"></asp:TextBox>
                 <asp:RequiredFieldValidator
                    ID="Rfv_Length" runat="server" ErrorMessage="*" 
                    ForeColor="Red" ControlToValidate="Txt_ItemLength" ValidationGroup="1"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="Rev_Length" runat="server" 
                         ControlToValidate="Txt_ItemLength" ErrorMessage="只能输入整数或小数，且小数点最多两位！" 
                         ValidationExpression="^[1-9]+\.?[0-9]{0,2}$" ForeColor="Red" 
                         ValidationGroup="1"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        物品宽度:
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="Txt_ItemWidth" runat="server"  Width="100"></asp:TextBox>
                  <asp:RequiredFieldValidator
                    ID="Rfv_Width" runat="server" ErrorMessage="*" 
                    ForeColor="Red" ControlToValidate="Txt_ItemWidth" ValidationGroup="1"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="Rev_ItemWidth" runat="server" 
                          ControlToValidate="Txt_ItemWidth" ErrorMessage="只能输入整数或小数，且小数点最多两位！" 
                          ValidationExpression="^[1-9]+\.?[0-9]{0,2}$" ForeColor="Red" 
                          ValidationGroup="1"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr class="EditRow">
                    <td class="FieldHeader">
                        物品高度:
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="Txt_ItemHeight" runat="server"  Width="100"></asp:TextBox>
                  <b style="color:Red">*</b>
                  <asp:RequiredFieldValidator
                    ID="Rfv_Height" runat="server" ErrorMessage="*" 
                    ForeColor="Red" ControlToValidate="Txt_ItemHeight" ValidationGroup="1"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="Rev_height" runat="server" 
                          ControlToValidate="Txt_ItemHeight" ErrorMessage="只能输入整数或小数，且小数点最多两位！" 
                          ValidationExpression="^[1-9]+\.?[0-9]{0,2}$" ForeColor="Red" 
                          ValidationGroup="1"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr class="SelectedRow">
                    <td class="FieldHeader">
                        运送支付费用:
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="Txt_ItemTransPays" runat="server"  Width="100"></asp:TextBox>
                      <asp:RegularExpressionValidator ID="Rev_TransPays" runat="server" 
                          ErrorMessage="只能输入整数或小数，且小数点最多两位！" 
                          ValidationExpression="^[1-9]+\.?[0-9]{0,2}$" 
                          ControlToValidate="Txt_ItemTransPays" ForeColor="Red" ValidationGroup="1"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr class="Pager">
                    <td class="FieldHeader">
                        运送支付货币:
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="Txt_ItemTransCurrency" runat="server"  Width="100"></asp:TextBox>
                      <asp:RegularExpressionValidator ID="Rev_TransCurrency" runat="server" 
                          ControlToValidate="Txt_ItemTransCurrency" 
                          ErrorMessage="只能输入整数或小数，且小数点最多两位！" 
                          ValidationExpression="^[1-9]+\.?[0-9]{0,2}$" ForeColor="Red" 
                          ValidationGroup="1"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="FooterBtnBar">
            <asp:Button ID="But_AddItem" runat="server" Text="提 交" ValidationGroup="1" 
                CssClass="InputBtn" onclick="But_AddItem_Click" />
            <asp:Button ID="But_Rurnet" runat="server" Text="返 回" CssClass="InputBtn" 
                onclick="But_Rurnet_Click" />
        </div>
    </div>
    </form>
</body>
</html>
