<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompanyAdd.aspx.cs" Inherits="GGGETSAdmin.PersonnelManage.CompanyManage.CompanyAdd" Theme="logisitc" %>
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
                        <asp:TextBox ID="Txt_CompanyCode" runat="server" TabIndex="1" 
                            CssClass="TextBox" AutoPostBack="True" MaxLength="35"
                            ontextchanged="Txt_CompanyCode_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
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
                        <asp:TextBox ID="Txt_Address" runat="server" MaxLength="190" CssClass="TextBox" TabIndex="8" Width="300" TextMode="MultiLine" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_OrganizationCode" runat="server" Text="组织机构代码:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_OrganizationCode" runat="server" CssClass="TextBox" MaxLength="35" TabIndex="9" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Remark" runat="server" Text="备注:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Remark" runat="server" CssClass="TextBox" MaxLength="120" TabIndex="10" TextMode="MultiLine" Width="300" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="FooterBtnBar">
        <asp:Button ID="btn_Add" runat="server" Text="添 加" CssClass="InputBtn" 
            onclick="btn_AddCompany_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Cancel" runat="server" Text="取 消" CssClass="InputBtn" 
            onclick="btn_Cancel_Click" />
    </div>
</asp:Content>
