<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompanyModify.aspx.cs" Inherits="GGGETSAdmin.PersonnelManage.CompanyManage.CompanyModify" Theme="logisitc" %>
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
                            CssClass="TextBox" Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_FullName" runat="server" Text="公司全称:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_FullName" runat="server" CssClass="TextBox" MaxLength="70" TabIndex="2" Width="300" Style="text-transform: uppercase"></asp:TextBox><b style="color: Red">*</b> 
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_ShortName" runat="server" Text="公司简称:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_ShortName" runat="server" CssClass="TextBox" MaxLength="50" TabIndex="3" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Contactor" runat="server" Text="联系人:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Contactor" runat="server" CssClass="TextBox" MaxLength="25" TabIndex="4" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_ContactorPhone" runat="server" Text="联系电话:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_ContactorPhone" runat="server" MaxLength="20" CssClass="TextBox" TabIndex="5"></asp:TextBox> 
                        <b style="color: Red">*</b> 
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Phone" runat="server" Text="公司电话:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Phone" runat="server" CssClass="TextBox" MaxLength="20" TabIndex="6"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Fax" runat="server" Text="公司传真:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Fax" runat="server" CssClass="TextBox" MaxLength="20" TabIndex="6"></asp:TextBox>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_PostCode" runat="server" Text="邮编:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_PostCode" runat="server" CssClass="TextBox" MaxLength="20" TabIndex="7"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Address" runat="server" Text="地址:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Address" runat="server" MaxLength="400" CssClass="TextBox" TabIndex="8" Width="300" TextMode="MultiLine" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_OrganizationCode" runat="server" Text="组织机构代码:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_OrganizationCode" runat="server" MaxLength="35" CssClass="TextBox" TabIndex="9" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Remark" runat="server" Text="备注:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Remark" runat="server" MaxLength="120" CssClass="TextBox" TabIndex="10" TextMode="MultiLine" Width="300" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                     <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Status" runat="server" Text="状态:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DDL_Status" runat="server">
                            <asp:ListItem Value="0" Text="可用"></asp:ListItem>
                            <asp:ListItem Value="1" Text="不可用"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="FooterBtnBar">
        <asp:Button ID="btn_Update" runat="server" Text="修 改" CssClass="InputBtn" 
            onclick="btn_UpCompany_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Cancel" runat="server" Text="取 消" CssClass="InputBtn" 
            onclick="btn_Cancel_Click" />
    </div>
</asp:Content>
