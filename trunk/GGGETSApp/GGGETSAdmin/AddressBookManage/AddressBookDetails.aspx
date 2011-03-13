<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="AddressBookDetails.aspx.cs" Inherits="GGGETSAdmin.AddressBookManage.AddressBookDetails"
    Theme="logisitc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="DataView">
            <tbody id="tbDeliver" runat="server">
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_AddressType" runat="server" Text="地址类型:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_AddressType" runat="server"></asp:Label>
                     </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_CompanyCode" runat="server" Text="部门账号:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Txt_CompanyCode" runat="server" TabIndex="2" Width="70" Style="text-transform: uppercase"></asp:Label>-
                        <asp:Label ID="Txt_Code" runat="server" Width="70" TabIndex="3" Style="text-transform: uppercase"></asp:Label>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_LoginName" runat="server" Text="用户名:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Txt_LoginName" runat="server" Style="text-transform: uppercase" TabIndex="3"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_DeliverName" runat="server" Text="公司:"></asp:Label>
                    </td>
                    <td align="left" colspan="5">
                        <asp:Label ID="Txt_DeliverName" runat="server" Width="500" TabIndex="1" Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_DeliverAddress" runat="server" Text="地址:"></asp:Label>
                    </td>
                    <td align="left" colspan="5">
                        <asp:Label ID="Txt_DeliverAddress" runat="server" Enabled="False" Width="500" 
                            TabIndex="2" TextMode="MultiLine"
                            Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_DeliverCountry" runat="server" Text="国家:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_DeliverCountry" runat="server" Width="80" OnTextChanged="Txt_DeliverCountry_TextChanged" TabIndex="3" Style="text-transform: uppercase"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverProvince" runat="server" Text="省份:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_DeliverProvince" runat="server" Width="80" TabIndex="4" Style="text-transform: uppercase"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverRegion" runat="server" Text="城市:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_DeliverRegion" runat="server" Width="80"
                            TabIndex="5" Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_DeliverZipCode" runat="server" Text="邮编:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_DeliverZipCode" runat="server" Width="80" TabIndex="6" Style="text-transform: uppercase"></asp:Label>
                        &nbsp;
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverContactor" runat="server" Text="姓名:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_DeliverContactor" runat="server" Width="80" TabIndex="7" Style="text-transform: uppercase"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverTel" runat="server" Text="电话:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_DeliverTel" runat="server" Width="80" TabIndex="8" Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td align="center" colspan="6">
                        <asp:Button ID="But_Update" runat="server" Text="修 改" CssClass="InputBtn" TabIndex="30"
                            OnClick="But_Update_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="But_Conel" runat="server" Text="返 回" CssClass="InputBtn" TabIndex="31"
                            OnClick="But_Conel_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
