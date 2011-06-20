<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="GGGETSAdmin.Account.AddUser" Theme="logisitc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register Assembly="AutoCompleteExtra" Namespace="AutoCompleteExtra" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户管理</title>
    <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <cc2:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc2:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align:center">
        <table class="DataView" width="98%">
            <tbody>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_LoginName" runat="server" Text="登录名："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLoginName" runat="server" style="text-transform:uppercase" 
                            MaxLength="20" CssClass="TextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valrLoginName" runat="server" ErrorMessage="必填" ControlToValidate="txtLoginName">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_LoginPwd" runat="server" Text="登录密码："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" 
                            MaxLength="100" CssClass="TextBox">
                           
                            </asp:TextBox>
                             <asp:RequiredFieldValidator ID="valrPassword" runat="server" ErrorMessage="必填" ControlToValidate="txtPassword">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="Row" id="trPassword" runat="server">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_ConfirmPwd" runat="server" Text="确认密码："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password" 
                            MaxLength="100" CssClass="TextBox"></asp:TextBox>
                        <asp:CompareValidator ID="valcConfirmPwd" runat="server" ControlToCompare="txtPassword"
                           ControlToValidate="txtConfirmPwd" 
                             ErrorMessage="密码不正确">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_RealName" runat="server" Text="真实姓名："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRealName" runat="server" MaxLength="50" CssClass="TextBox"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="valrRealName" runat="server" ErrorMessage="必填" ControlToValidate="txtRealName">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Tel" runat="server" Text="联系电话："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPhone" runat="server" MaxLength="50" CssClass="TextBox"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="valePhone" runat="server" 
                                        ControlToValidate="txtPhone" ErrorMessage="联系电话格式不正确" 
                                        ValidationExpression="((\d{11})|^(\d{2,4}[-_－—]?)?\d{3,8}([-_－—]?\d{3,8})?([-_－—]?\d{1,7})?$)|(^0?1[35]\d{9}$)"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Email" runat="server" Text="邮箱："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="TextBox"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="valeEmail" runat="server" 
                                        ControlToValidate="txtEmail" ErrorMessage="电子邮件格式不对" 
                                        
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_CountryCode" runat="server" Text="国家二字码："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtCountryCode" runat="server" MaxLength="2" 
                            AutoPostBack="true" ontextchanged="txtCountryCode_TextChanged" CssClass="TextBox"></asp:TextBox>
                        <cc1:AutoCompleteExtraExtender runat="server" ID="autoCountry" ServiceMethod="GetCountryList"
                            TargetControlID="txtCountryCode" AsyncPostback="false" AutoPostback="true"
                            MinimumPrefixLength="1" CompletionSetCount="10" OnItemSelected="autoCountry_ItemSelected">
                        </cc1:AutoCompleteExtraExtender>
                         <asp:RequiredFieldValidator ID="valeCountryCode" runat="server" ErrorMessage="必填" ControlToValidate="txtCountryCode">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_RegionCode" runat="server" Text="地区三字码："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRegionCode" runat="server" 
                            ontextchanged="txtRegionCode_TextChanged" AutoPostBack="true" CssClass="TextBox"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="valeRegionCode" runat="server" ErrorMessage="必填" ControlToValidate="txtRegionCode">
                        </asp:RequiredFieldValidator>
                        <cc1:AutoCompleteExtraExtender runat="server" ID="autoRegion" ServiceMethod="GetRegionList"
                            TargetControlID="txtRegionCode" AsyncPostback="false" 
                            MinimumPrefixLength="1" CompletionSetCount="10"
                            UseContextKey="True">
                        </cc1:AutoCompleteExtraExtender>
                    </td>
                </tr>
                <tr class="Row">
                <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Status" runat="server" Text="会员状态:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                           
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                       <asp:Label ID="Label1" runat="server" Text="角色:"></asp:Label></td>
                    <td align="left">
                        <asp:CheckBoxList ID="chkRoleID" runat="server" RepeatColumns="3" 
                              RepeatDirection="Horizontal">
                        </asp:CheckBoxList></td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Remark" runat="server" Text="备注:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Remark" runat="server" CssClass="TextBox" TabIndex="10" 
                            TextMode="MultiLine" Width="300" Style="text-transform: uppercase" 
                            MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
            <div>
        <asp:Button ID="btn_login" runat="server" Text="设 置" 
            onclick="btn_login_Click" CssClass="InputBtn" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_cancel" runat="server" Text="返 回" 
            onclick="btn_cancel_Click" CausesValidation="false" CssClass="InputBtn"/>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
