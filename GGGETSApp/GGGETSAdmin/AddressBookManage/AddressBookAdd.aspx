﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="AddressBookAdd.aspx.cs" Inherits="GGGETSAdmin.AddressBookManage.AddressBookAdd"
    Theme="logisitc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="AutoCompleteExtra" Namespace="AutoCompleteExtra" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc2:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc2:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table class="DataView">
                    <tbody>
                        <tr class="AlternatingRow">
                            <td class="FieldHeader" align="right">
                                <asp:Label ID="lbl_AddressType" runat="server" Text="地址类型:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_AddressBookType" runat="server" TabIndex="1">
                                    <asp:ListItem Text="发件人地址" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="收件人地址" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="交付人地址" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_CompanyCode" runat="server" Text="部门账号:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Txt_CompanyCode" runat="server" TabIndex="2" AutoPostBack="True"
                                    OnTextChanged="Txt_CompanyCode_TextChanged" Width="70" Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>-
                                <asp:TextBox ID="Txt_Code" runat="server" OnTextChanged="Txt_Code_TextChanged" AutoPostBack="True"
                                    Width="70" TabIndex="3" Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                            </td>
                            <td class="FieldHeader" align="right">
                                <asp:Label ID="lbl_LoginName" runat="server" Text="用户名:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Txt_LoginName" runat="server" AutoPostBack="True" Style="text-transform: uppercase"
                                    OnTextChanged="Txt_LoginName_TextChanged" TabIndex="3" CssClass="TextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader" align="right">
                                <asp:Label ID="lbl_DeliverName" runat="server" Text="公司:"></asp:Label>
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="Txt_DeliverName" runat="server" MaxLength="180" Width="500" TabIndex="4" Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                                <b style="color: Red">*</b>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader" align="right">
                                <asp:Label ID="lbl_DeliverAddress" runat="server" Text="地址:"></asp:Label>
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="Txt_DeliverAddress" runat="server" MaxLength="400" Width="500" TabIndex="5" TextMode="MultiLine"
                                    Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                                <b style="color: Red">*</b>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader" align="right">
                                <asp:Label ID="lbl_DeliverCountry" runat="server" Text="国家:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_DeliverCountry" runat="server" Width="80" OnTextChanged="Txt_DeliverCountry_TextChanged"
                                    AutoPostBack="true" TabIndex="6" Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                                <b style="color: Red">*</b>
                                <cc1:AutoCompleteExtraExtender runat="server" ID="autoDeliveCountry" ServiceMethod="GetCountryList"
                                    TargetControlID="Txt_DeliverCountry" AsyncPostback="false" AutoPostback="true"
                                    MinimumPrefixLength="1" CompletionSetCount="10" OnItemSelected="autoDeliveCountry_ItemSelected">
                                </cc1:AutoCompleteExtraExtender>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_DeliverProvince" runat="server" Text="省份:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_DeliverProvince" runat="server" Width="80" TabIndex="7" Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_DeliverRegion" runat="server" Text="城市:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_DeliverRegion" runat="server" Width="80" OnTextChanged="Txt_DeliverRegion_TextChanged"
                                    TabIndex="8" Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                                <b style="color: Red">*</b>
                                <cc1:AutoCompleteExtraExtender runat="server" ID="autoDeliverRegion" ServiceMethod="GetRegionList"
                                    TargetControlID="Txt_DeliverRegion" AsyncPostback="false" MinimumPrefixLength="1"
                                    CompletionSetCount="10" UseContextKey="True">
                                </cc1:AutoCompleteExtraExtender>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader" align="right">
                                <asp:Label ID="lbl_DeliverZipCode" runat="server" Text="邮编:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_DeliverZipCode" runat="server" MaxLength="15" Width="80" TabIndex="9" Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                                <b style="color: Red">*</b>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_DeliverContactor" runat="server" Text="姓名:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_DeliverContactor" runat="server" MaxLength="40" Width="80" TabIndex="10" Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                                <b style="color: Red">*</b>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_DeliverTel" runat="server" Text="电话:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_DeliverTel" runat="server" MaxLength="20" Width="80" TabIndex="11" Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                                <b style="color: Red">*</b>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td align="center" colspan="6">
                                <asp:Button ID="btn_AddDeliver" runat="server" CssClass="InputBtn" TabIndex="12"
                                    Text="添 加" OnClick="btn_AddDeliver_Click" />
                                <asp:Button ID="btn_Close" runat="server" CssClass="InputBtn" TabIndex="13" Text="取 消" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
