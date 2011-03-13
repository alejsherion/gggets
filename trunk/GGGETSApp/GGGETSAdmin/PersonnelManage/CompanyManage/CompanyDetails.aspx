<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompanyDetails.aspx.cs" Inherits="GGGETSAdmin.PersonnelManage.CompanyManage.CompanyDetails" Theme="logisitc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="DataView">
            <tbody>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_CompanyCode" runat="server" Text="公司账号:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_CompanyCode" runat="server" TabIndex="1" 
                             AutoPostBack="True" 
                            ontextchanged="Txt_CompanyCode_TextChanged" Style="text-transform: uppercase"></asp:Label>
                        &nbsp;</td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_FullName" runat="server" Text="公司全称:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_FullName" runat="server"  TabIndex="2" Width="300" Style="text-transform: uppercase"></asp:Label>&nbsp;</td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_ShortName" runat="server" Text="公司简称:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_ShortName" runat="server"  TabIndex="3" Style="text-transform: uppercase"></asp:Label>
                        &nbsp;</td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Contactor" runat="server" Text="联系人:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_Contactor" runat="server"  TabIndex="4" Style="text-transform: uppercase"></asp:Label>
                        &nbsp;</td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_ContactorPhone" runat="server" Text="联系电话:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_ContactorPhone" runat="server"  TabIndex="5"></asp:Label> 
                        &nbsp;</td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Phone" runat="server" Text="公司电话:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_Phone" runat="server"  TabIndex="6"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Fax" runat="server" Text="公司传真:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_Fax" runat="server"  TabIndex="6"></asp:Label>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_PostCode" runat="server" Text="邮编:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_PostCode" runat="server"  TabIndex="7"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Address" runat="server" Text="地址:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_Address" runat="server"  TabIndex="8" Width="300" TextMode="MultiLine" Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_OrganizationCode" runat="server" Text="组织机构代码:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_OrganizationCode" runat="server"  TabIndex="9" Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Remark" runat="server" Text="备注:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_Remark" runat="server"  TabIndex="10" TextMode="MultiLine" Width="300" Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Status" runat="server" Text="状态:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_Status" runat="server"  TabIndex="9" Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="FooterBtnBar">
        <asp:Button ID="But_Update" runat="server" Text="修 改" CssClass="InputBtn" 
        TabIndex="30" onclick="But_Next_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="But_Conel" runat="server" Text="返 回" CssClass="InputBtn" 
        TabIndex="31" onclick="But_Conel_Click" />
    </div>
</asp:Content>
