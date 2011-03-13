<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="GGGETSAdmin.PersonnelManage.UserManage.UserDetails" Theme="logisitc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="DataView">
            <tbody>
                <tr class="AlternatingRow" id="tdCompanyCode" runat="server"> 
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_CompanyCode" runat="server" Text="公司账号:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_CompanyCode" runat="server" TabIndex="1" 
                            AutoPostBack="True" 
                            ontextchanged="Txt_CompanyCode_TextChanged" Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
                <tr class="Row" id="tdDepCode" runat="server">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_DepCode" runat="server" Text="部门账号:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_DepCode" runat="server" 
                            AutoPostBack="true" TabIndex="2" Style="text-transform: uppercase" 
                            ontextchanged="Txt_DepCode_TextChanged"></asp:Label>
                    </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_LoginName" runat="server" Text="用 户 名:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_LoginName" runat="server" TabIndex="3" 
                            AutoPostBack="True" ontextchanged="Txt_LoginName_TextChanged" Style="text-transform: uppercase"></asp:Label>
                        </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_RealName" runat="server" Text="真实姓名:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_RealName" runat="server" TabIndex="6" Style="text-transform: uppercase"></asp:Label>
                        </td>
                </tr>
                <tr class="AlternatingRow">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Email" runat="server" Text="邮箱地址:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_Email" runat="server" TabIndex="7"></asp:Label>
                        </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Phone" runat="server" Text="联系电话:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_Phone" runat="server" TabIndex="8"></asp:Label>
                        </td>
                </tr>
                    <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_FeeDiscountType" runat="server" Text="费用折扣类型:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_FeeDiscountType" runat="server"></asp:Label>
                    </td>
                </tr>
                </tr>
                <tr class="AlternatingRow"> 
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_FeeDiscountRate" runat="server" Text="费用折扣率:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_FeeDiscountRate" runat="server" TabIndex="10" 
                            Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_WeightDiscountType" runat="server" Text="重量折扣类型:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_WeightDiscountType" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="AlternatingRow"> 
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_WeightDiscountRate" runat="server" Text="重量折扣率:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_WeightDiscountRate" runat="server" TabIndex="12" 
                            Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_SettleType" runat="server" Text="结算方式:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_SettleType" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="AlternatingRow"> 
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_WeightCalType" runat="server" Text="计重方式:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_WeightCalType" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Remark" runat="server" Text="备注:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_Remark" runat="server" TabIndex="15" TextMode="MultiLine" Width="300" Style="text-transform: uppercase"></asp:Label>
                    </td>
                </tr>
                <tr class="AlternatingRow"> 
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Status" runat="server" Text="当前状态:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_Status" runat="server"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="FooterBtnBar">
        <asp:Button ID="But_Update" runat="server" Text="修 改" CssClass="InputBtn" 
        TabIndex="30" onclick="But_Next_Click" />
    <asp:Button ID="But_Conel" runat="server" Text="返 回" CssClass="InputBtn" 
        TabIndex="31" onclick="But_Conel_Click" />
    </div>
</asp:Content>
