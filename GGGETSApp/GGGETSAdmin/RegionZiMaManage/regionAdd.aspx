<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="regionAdd.aspx.cs" Inherits="GGGETSAdmin.RegionZiMaManage.regionAdd" Theme="logisitc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register Assembly="AutoCompleteExtra" Namespace="AutoCompleteExtra" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc2:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc2:ToolkitScriptManager>
     <div>
        <table class="DataView">
            <tr class="Row">
                <td class="FieldHeader" style="width:120px">
                    <asp:Label ID="lbl_CountryCode" runat="server" Text="国家二字码:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Txt_CountryCode" runat="server" Width="80" TabIndex="1" 
                        style="text-transform:uppercase" MaxLength="2"></asp:TextBox>
                        <cc1:AutoCompleteExtraExtender runat="server" ID="autocomplete" ServiceMethod="GetCountryList"
                                        TargetControlID="Txt_CountryCode" AsyncPostback="false" AutoPostback="true"
                                        MinimumPrefixLength="1" CompletionSetCount="10" onitemselected="autocomplete_ItemSelected">
                                    </cc1:AutoCompleteExtraExtender>
                </td>
            </tr>
            <tr class="AlternatingRow">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_RegionCode" runat="server" Text="地区三字码:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_RegionCode" runat="server"
                        TabIndex="2" MaxLength="3" style="text-transform:uppercase"></asp:TextBox>
                </td>
               
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_RegionName" runat="server" Text="地区全称:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_RegionName" runat="server"
                        TabIndex="3" style="text-transform:uppercase" MaxLength="50"></asp:TextBox>
                </td>
               
            </tr>
        </table>
        <div class="FooterBtnBar">
            <asp:Button ID="btn_Add" runat="server" CssClass="InputBtn" Text="添 加" 
                        onclick="btn_Add_Click" TabIndex="4"/>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="But_Conel" runat="server" Text="返 回" CssClass="InputBtn" 
        TabIndex="5" onclick="But_Conel_Click" />
        </div>
    </div>
</asp:Content>
