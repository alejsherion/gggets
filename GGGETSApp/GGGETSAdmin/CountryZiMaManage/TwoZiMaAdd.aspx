<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TwoZiMaAdd.aspx.cs" Inherits="GGGETSAdmin.CountryZiMaManage.TwoZiMaAdd" Theme="logisitc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="">
        <table class="DataView">
            <tr class="Row">
                <td class="FieldHeader" align="left">
                    <asp:Label ID="lbl_CountryCode" runat="server" Text="国家二字码:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Txt_CountryCode" runat="server" Width="80" TabIndex="1" 
                        style="text-transform:uppercase" MaxLength="2"></asp:TextBox>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader" align="left">
                    <asp:Label ID="lbl_CountryName" runat="server" Text="国家全称:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_CountryName" runat="server"
                        TabIndex="2" style="text-transform:uppercase" MaxLength="50"></asp:TextBox>
                </td>
               
            </tr>
        </table>
        <div>
            <asp:Button ID="btn_Add" runat="server" CssClass="InputBtn" Text="添 加" 
                        onclick="btn_Add_Click" TabIndex="3"/>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="But_Conel" runat="server" Text="返 回" CssClass="InputBtn" 
        TabIndex="4" onclick="But_Conel_Click" />
        </div>
    </div>
</asp:Content>
