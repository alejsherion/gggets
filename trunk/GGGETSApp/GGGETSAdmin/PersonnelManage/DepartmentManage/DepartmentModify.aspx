<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DepartmentModify.aspx.cs" Inherits="GGGETSAdmin.PersonnelManage.DepartmentManage.DepartmentModify" Theme="logisitc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="DataView">
            <tbody>
                <tr class="AlternatingRow"> 
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_CompanyCode" runat="server" Text="公司账号:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_CompanyCode" runat="server" TabIndex="1" 
                            CssClass="TextBox" AutoPostBack="True" MaxLength="35" 
                            ontextchanged="Txt_CompanyCode_TextChanged" Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_DepCode" runat="server" Text="部门账号:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_DepCode" runat="server" CssClass="TextBox" MaxLength="35" 
                            AutoPostBack="true" TabIndex="2" Style="text-transform: uppercase" 
                            ontextchanged="Txt_DepCode_TextChanged"></asp:Label>
                        &nbsp;</td>
                </tr>
                <tr class="AlternatingRow"> 
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_DepName" runat="server" Text="部门名称:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_DepName" runat="server" TabIndex="3" MaxLength="35"
                            CssClass="TextBox" AutoPostBack="True" Style="text-transform: uppercase" 
                            ontextchanged="Txt_DepName_TextChanged"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_FeeDiscountType" runat="server" Text="费用折扣类型:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_FeeDiscountType" runat="server" TabIndex="4">
                            <asp:ListItem Text="灵活折扣" Value="0"></asp:ListItem>
                            <asp:ListItem Text="固定折扣" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="AlternatingRow"> 
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_FeeDiscountRate" runat="server" Text="费用折扣率:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_FeeDiscountRate" runat="server" TabIndex="5" 
                            CssClass="TextBox" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_WeightDiscountType" runat="server" Text="重量折扣类型:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_WeightDiscountType" runat="server" TabIndex="6">
                            <asp:ListItem Text="灵活折扣" Value="0"></asp:ListItem>
                            <asp:ListItem Text="固定折扣" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="AlternatingRow"> 
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_WeightDiscountRate" runat="server" Text="重量折扣率:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_WeightDiscountRate" runat="server" TabIndex="7" 
                            CssClass="TextBox" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_SettleType" runat="server" Text="结算方式:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_SettleType" runat="server" TabIndex="9">
                            <asp:ListItem Value="0" Text="预付月结"></asp:ListItem>
                            <asp:ListItem Value="1" Text="预付现结"></asp:ListItem>
                            <asp:ListItem Value="2" Text="到付月结"></asp:ListItem>
                            <asp:ListItem Value="3" Text="到付现结"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="AlternatingRow"> 
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_WeightCalType" runat="server" Text="计重方式:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_WeightCalType" runat="server" TabIndex="10">
                            <asp:ListItem Value="0" Text="按照0.5KG标准"></asp:ListItem>
                            <asp:ListItem Value="1" Text="按照分段标准"></asp:ListItem>
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
