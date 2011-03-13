<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserModify.aspx.cs" Inherits="GGGETSAdmin.PersonnelManage.UserManage.UserModify" Theme="logisitc" %>
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
                        <asp:TextBox ID="Txt_CompanyCode" runat="server" TabIndex="1" 
                            CssClass="TextBox" AutoPostBack="True" 
                            ontextchanged="Txt_CompanyCode_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                        &nbsp;</td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_DepCode" runat="server" Text="部门账号:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_DepCode" runat="server" CssClass="TextBox" 
                            AutoPostBack="true" TabIndex="2" Style="text-transform: uppercase" 
                            ontextchanged="Txt_DepCode_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_LoginName" runat="server" Text="用 户 名:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_LoginName" runat="server" CssClass="TextBox" TabIndex="3" 
                            AutoPostBack="True" ontextchanged="Txt_LoginName_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b></td>
                </tr>                
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_RealName" runat="server" Text="真实姓名:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_RealName" runat="server" CssClass="TextBox" TabIndex="6" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b></td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Email" runat="server" Text="邮箱地址:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Email" runat="server" CssClass="TextBox" TabIndex="7"></asp:TextBox>
                        </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Phone" runat="server" Text="联系电话:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Phone" runat="server" CssClass="TextBox" TabIndex="8"></asp:TextBox>
                        </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_FeeDiscountType" runat="server" Text="费用折扣类型:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_FeeDiscountType" runat="server" TabIndex="9">
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
                        <asp:TextBox ID="Txt_FeeDiscountRate" runat="server" TabIndex="10" 
                            CssClass="TextBox" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_WeightDiscountType" runat="server" Text="重量折扣类型:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_WeightDiscountType" runat="server" TabIndex="11">
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
                        <asp:TextBox ID="Txt_WeightDiscountRate" runat="server" TabIndex="12" 
                            CssClass="TextBox" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_SettleType" runat="server" Text="结算方式:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_SettleType" runat="server" TabIndex="13">
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
                        <asp:DropDownList ID="ddl_WeightCalType" runat="server" TabIndex="14">
                            <asp:ListItem Value="0" Text="按照0.5KG标准"></asp:ListItem>
                            <asp:ListItem Value="1" Text="按照分段标准"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Remark" runat="server" Text="备注:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Remark" runat="server" CssClass="TextBox" TabIndex="15" TextMode="MultiLine" Width="300" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                </tr>
                 <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Status" runat="server" Text="状态:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DDL_Status" runat="server">
                            <asp:ListItem Value="0" Text="可用"></asp:ListItem>
                            <asp:ListItem Value="1" Text="不可用"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
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
