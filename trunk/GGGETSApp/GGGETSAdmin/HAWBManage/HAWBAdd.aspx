<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="HAWBAdd.aspx.cs" Inherits="GGGETSAdmin.HAWBManage.HAWBAdd" Theme="logisitc" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AutoCompleteExtra" Namespace="AutoCompleteExtra" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
        CombineScripts="True">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table class="DataView">
                    <thead>
                        <tr class="Header">
                            <th colspan="6">
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_BarCode" runat="server" Text="运单号：" meta:resourcekey="lbl_BarCode"></asp:Label>
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="Txt_BarCode" runat="server" MaxLength="15" Width="300px" TabIndex="1" OnTextChanged="Txt_BarCode_TextChanged"
                                    AutoPostBack="True" Style="text-transform: uppercase" 
                                    meta:resourcekey="Txt_BarCodeResource1"></asp:TextBox><b style="color: Red">*</b>
                            </td>
                        </tr>
                        <tr class="AlternatingRow">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_Account" runat="server" Text="客户帐号：" Width="80px" 
                                    meta:resourcekey="lbl_AccountResource1"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_Account1" runat="server" Width="80px" TabIndex="2" Style="text-transform: uppercase"
                                    AutoPostBack="True" MaxLength="45" OnTextChanged="Txt_Account1_TextChanged" 
                                    meta:resourcekey="Txt_Account1Resource1"></asp:TextBox>
                                -
                                <asp:TextBox ID="Txt_Account2" runat="server" Width="50px" TabIndex="3" Style="text-transform: uppercase"
                                    AutoPostBack="True" MaxLength="45" OnTextChanged="Txt_Account2_TextChanged" 
                                    meta:resourcekey="Txt_Account2Resource1"></asp:TextBox>
                                <b style="color: Red">*</b>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_SettleType" runat="server" Text="结算方式：" Width="80px" 
                                    meta:resourcekey="lbl_SettleTypeResource1"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DDl_SettleType" runat="server" 
                                    meta:resourcekey="DDl_SettleTypeResource1">
                                    <asp:ListItem Value="0" Text="预付月结" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="预付现结" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="到付月结" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="到付现结" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_Status" runat="server" Text="状态：" 
                                    meta:resourcekey="lbl_StatusResource1"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DDl_Status" runat="server" 
                                    meta:resourcekey="DDl_StatusResource1">
                                    <asp:ListItem Value="0" Text="待审核" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="取货" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="核单" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="派送" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="in包" meta:resourcekey="ListItemResource9"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
    <div>
        <table class="DataView">
            <tbody>
                <tr class="EditRow">
                    <td colspan="6" align="left">
                        <asp:Label ID="lbl_Shipper" runat="server" Text="发件人信息" 
                            meta:resourcekey="lbl_ShipperResource1"></asp:Label>
                        <asp:LinkButton ID="lbtn_Shipperhistory" runat="server" Text="[历史]" 
                            OnClick="lbtn_Shipperhistory_Click" 
                            meta:resourcekey="lbtn_ShipperhistoryResource1"></asp:LinkButton>
                        <asp:Label ID="lbl_ShipperAddressAid" runat="server" Visible="False" 
                            meta:resourcekey="lbl_ShipperAddressAidResource1"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
            <div>
                <table class="DataView">
                    <tbody>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ShipperName" runat="server" Text="公司：" 
                                    meta:resourcekey="lbl_ShipperNameResource1"></asp:Label>
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="Txt_ShipperName" MaxLength="180" runat="server" Width="800px" TabIndex="5" Style="text-transform: uppercase"
                                    AutoPostBack="True" OnTextChanged="Txt_ShipperName_TextChanged" 
                                    meta:resourcekey="Txt_ShipperNameResource1"></asp:TextBox><b
                                        style="color: Red">*</b>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ShipperAddress" runat="server" Text="地址：" 
                                    meta:resourcekey="lbl_ShipperAddressResource1"></asp:Label>
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="Txt_ShipperAddress" runat="server" MaxLength="400" Width="800px" TabIndex="6" TextMode="MultiLine"
                                    Style="text-transform: uppercase" OnTextChanged="Txt_ShipperAddress_TextChanged"
                                    AutoPostBack="True" meta:resourcekey="Txt_ShipperAddressResource1"></asp:TextBox><b style="color: Red">*</b>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ShipperCountry" runat="server" Text="国家：" 
                                    meta:resourcekey="lbl_ShipperCountryResource1"></asp:Label>
                            </td>
                            <td align="left" class="style2">
                                <asp:TextBox ID="Txt_ShipperCountry" runat="server" Width="80px" AutoPostBack="True"
                                    OnTextChanged="Txt_ShipperCountry_TextChanged" TabIndex="7" 
                                    Style="text-transform: uppercase" 
                                    meta:resourcekey="Txt_ShipperCountryResource1"></asp:TextBox>
                                <b style="color: Red">*</b>
                                <cc1:AutoCompleteExtraExtender runat="server" ID="autocomplete" ServiceMethod="GetCountryList"
                                    TargetControlID="Txt_ShipperCountry" AutoPostback="True"
                                    MinimumPrefixLength="1" OnItemSelected="autocomplete_ItemSelected" 
                                    DelimiterCharacters="" Enabled="True" ServicePath="">
                                </cc1:AutoCompleteExtraExtender>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ShipperProvince" runat="server" Text="省份：" 
                                    meta:resourcekey="lbl_ShipperProvinceResource1"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_ShipperProvince" runat="server" Width="80px" TabIndex="8" Style="text-transform: uppercase"
                                    OnTextChanged="Txt_ShipperProvince_TextChanged" AutoPostBack="True" 
                                    meta:resourcekey="Txt_ShipperProvinceResource1"></asp:TextBox>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ShipperRegion" runat="server" Text="城市：" 
                                    meta:resourcekey="lbl_ShipperRegionResource1"></asp:Label>
                            </td>
                            <td align="left" class="style1">
                                <asp:TextBox ID="Txt_ShipperRegion" runat="server" Width="80px" TabIndex="9" Style="text-transform: uppercase"
                                    AutoPostBack="True" OnTextChanged="Txt_ShipperRegion_TextChanged" 
                                    meta:resourcekey="Txt_ShipperRegionResource1"></asp:TextBox>
                                <b style="color: Red">*</b>
                                <cc1:AutoCompleteExtraExtender runat="server" ID="autoRegion" ServiceMethod="GetRegionList"
                                    TargetControlID="Txt_ShipperRegion" MinimumPrefixLength="1" 
                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="">
                                </cc1:AutoCompleteExtraExtender>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ShipperZipCode" runat="server" Text="邮编：" 
                                    meta:resourcekey="lbl_ShipperZipCodeResource1"></asp:Label>
                            </td>
                            <td align="left" class="style2">
                                <asp:TextBox ID="Txt_ShipperZipCode" runat="server" Width="80px" TabIndex="10" Style="text-transform: uppercase"
                                    OnTextChanged="Txt_ShipperZipCode_TextChanged" MaxLength="15" AutoPostBack="True" 
                                    meta:resourcekey="Txt_ShipperZipCodeResource1"></asp:TextBox>
                                <b style="color: Red">*</b>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ShipperContactor" runat="server" Text="姓名：" 
                                    meta:resourcekey="lbl_ShipperContactorResource1"></asp:Label>
                            </td>
                            <td align="left" class="style1">
                                <asp:TextBox ID="Txt_ShipperContactor" runat="server" Width="80px" 
                                    TabIndex="11" Style="text-transform: uppercase"
                                    OnTextChanged="Txt_ShipperContactor_TextChanged" MaxLength="40" AutoPostBack="True" 
                                    meta:resourcekey="Txt_ShipperContactorResource1"></asp:TextBox>
                                <b style="color: Red">*</b>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ShipperTel" runat="server" Text="电话：" 
                                    meta:resourcekey="lbl_ShipperTelResource1"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_ShipperTel" AutoPostBack="True" runat="server" 
                                    Width="80px" MaxLength="20" TabIndex="12"
                                    Style="text-transform: uppercase" 
                                    OnTextChanged="Txt_ShipperTel_TextChanged" 
                                    meta:resourcekey="Txt_ShipperTelResource1"></asp:TextBox>
                                <b style="color: Red">*</b>
                                <asp:Button ID="btn_Addressbox" runat="server" Text="保存新地址" ForeColor="Red" Visible="False"
                                    CssClass="InputBtn" OnClick="btn_Addressbox_Click" 
                                    meta:resourcekey="btn_AddressboxResource1" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div id="Deliver" runat="server" visible="False">
                <table class="DataView">
                    <tbody>
                        <tr class="EditRow">
                    <td colspan="6" align="left">
                        <asp:Label ID="lbl_Deliver" runat="server" Text="交付人信息" 
                            meta:resourcekey="lbl_DeliverResource1"></asp:Label>
                        <asp:LinkButton ID="lbtn_Deliverhistory" runat="server" Text="[历史]" 
                            OnClick="lbtn_Deliverhistory_Click" 
                            meta:resourcekey="lbtn_DeliverhistoryResource1"></asp:LinkButton>
                    </td>
                </tr>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_DeliverName" runat="server" Text="公司:" 
                                    meta:resourcekey="lbl_DeliverNameResource1"></asp:Label>
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="Txt_DeliverName" runat="server" MaxLength="180" Width="800px" TabIndex="13" Style="text-transform: uppercase"
                                    OnTextChanged="Txt_DeliverName_TextChanged" AutoPostBack="True" 
                                    meta:resourcekey="Txt_DeliverNameResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_DeliverAddress" runat="server" Text="地址：" 
                                    meta:resourcekey="lbl_DeliverAddressResource1"></asp:Label>
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="Txt_DeliverAddress" runat="server" MaxLength="400" Width="800px" TabIndex="14" TextMode="MultiLine"
                                    Style="text-transform: uppercase" OnTextChanged="Txt_DeliverAddress_TextChanged"
                                    AutoPostBack="True" meta:resourcekey="Txt_DeliverAddressResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_DeliverCountry" runat="server" Text="国家：" 
                                    meta:resourcekey="lbl_DeliverCountryResource1"></asp:Label>
                            </td>
                            <td align="left" class="style2">
                                <asp:TextBox ID="Txt_DeliverCountry" runat="server" AutoPostBack="True" OnTextChanged="Txt_DeliverCountry_TextChanged"
                                    Width="80px" TabIndex="24" Style="text-transform: uppercase" 
                                    meta:resourcekey="Txt_DeliverCountryResource1"></asp:TextBox>
                                <cc1:AutoCompleteExtraExtender runat="server" ID="autoDeliveCountry" ServiceMethod="GetCountryList"
                                    TargetControlID="Txt_DeliverCountry" AutoPostback="True"
                                    MinimumPrefixLength="1" OnItemSelected="autoDeliveCountry_ItemSelected" 
                                    DelimiterCharacters="" Enabled="True" ServicePath="">
                                </cc1:AutoCompleteExtraExtender>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_DeliverProvince" runat="server" Text="省份：" 
                                    meta:resourcekey="lbl_DeliverProvinceResource1"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_DeliverProvince" runat="server" Width="80px" TabIndex="15" Style="text-transform: uppercase"
                                    OnTextChanged="Txt_DeliverProvince_TextChanged" AutoPostBack="True" 
                                    meta:resourcekey="Txt_DeliverProvinceResource1"></asp:TextBox>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_DeliverRegion" runat="server" Text="城市：" 
                                    meta:resourcekey="lbl_DeliverRegionResource1"></asp:Label>
                            </td>
                            <td align="left" class="style1">
                                <asp:TextBox ID="Txt_DeliverRegion" runat="server" Width="80px" TabIndex="16" Style="text-transform: uppercase"
                                    OnTextChanged="Txt_DeliverRegion_TextChanged" AutoPostBack="True" 
                                    meta:resourcekey="Txt_DeliverRegionResource1"></asp:TextBox>
                                <cc1:AutoCompleteExtraExtender runat="server" ID="autoDeliverRegion" ServiceMethod="GetRegionList"
                                    TargetControlID="Txt_DeliverRegion" MinimumPrefixLength="1" 
                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="">
                                </cc1:AutoCompleteExtraExtender>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_DeliverZipCode" runat="server" Text="邮编：" 
                                    meta:resourcekey="lbl_DeliverZipCodeResource1"></asp:Label>
                            </td>
                            <td align="left" class="style2">
                                <asp:TextBox ID="Txt_DeliverZipCode" runat="server" MaxLength="15" Width="80px" TabIndex="17" Style="text-transform: uppercase"
                                    OnTextChanged="Txt_DeliverZipCode_TextChanged" AutoPostBack="True" 
                                    meta:resourcekey="Txt_DeliverZipCodeResource1"></asp:TextBox>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_DeliverContactor" runat="server" Text="姓名：" 
                                    meta:resourcekey="lbl_DeliverContactorResource1"></asp:Label>
                            </td>
                            <td align="left" class="style1">
                                <asp:TextBox ID="Txt_DeliverContactor" runat="server" MaxLength="40" Width="80px" 
                                    TabIndex="18" Style="text-transform: uppercase"
                                    OnTextChanged="Txt_DeliverContactor_TextChanged" AutoPostBack="True" 
                                    meta:resourcekey="Txt_DeliverContactorResource1"></asp:TextBox>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_DeliverTel" runat="server" Text="电话：" 
                                    meta:resourcekey="lbl_DeliverTelResource1"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_DeliverTel" runat="server" Width="80px" MaxLength="20" TabIndex="19" Style="text-transform: uppercase"
                                    OnTextChanged="Txt_DeliverTel_TextChanged" AutoPostBack="True" 
                                    meta:resourcekey="Txt_DeliverTelResource1"></asp:TextBox>
                                <asp:Button ID="btn_DeliverAddress" runat="server" Text="保存新地址" ForeColor="Red" Visible="False"
                                    CssClass="InputBtn" OnClick="btn_DeliverAddress_Click" 
                                    meta:resourcekey="btn_DeliverAddressResource1" />
                            </td>
                        </tr>
                        <tr class="Row">
                            <td colspan="6">
                                <asp:Button ID="but_cancel" runat="server" Text="清除交付人信息" CssClass="InputBtn" 
                                    OnClick="but_cancel_Click" meta:resourcekey="but_cancelResource1" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
    <div>
        <table class="DataView">
            <tbody>
                
            </tbody>
        </table>
    </div>
            <div>
                <table class="DataView">
                    <tbody>
                        <tr class="EditRow">
                    <td colspan="6" align="left">
                        <asp:Label ID="lbl_Consignee" runat="server" Text="收件人信息" 
                            meta:resourcekey="lbl_ConsigneeResource1"></asp:Label>
                        <asp:LinkButton ID="lbtn_Consigneehistory" runat="server" Text="[历史]" 
                            OnClick="lbtn_Consigneehistory_Click" 
                            meta:resourcekey="lbtn_ConsigneehistoryResource1"></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtn_AddConsignee" runat="server" Text="添加交付人信息" CssClass="LinkBtn"
                            OnClick="lbtn_AddConsignee_Click" 
                            meta:resourcekey="lbtn_AddConsigneeResource1"></asp:LinkButton>
                    </td>
                </tr>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ConsigneeName" runat="server" Text="公司：" 
                                    meta:resourcekey="lbl_ConsigneeNameResource1"></asp:Label>
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="Txt_ConsigneeName" runat="server" Width="800px" 
                                    onchange="Context()" TabIndex="20" MaxLength="180" Style="text-transform: uppercase" 
                                    meta:resourcekey="Txt_ConsigneeNameResource1"></asp:TextBox>
                                <asp:Button ID="btn_ConsigneeName" runat="server" 
                                    onclick="btn_ConsigneeName_Click" BackColor="White" BorderStyle="None" 
                                    EnableTheming="False" meta:resourcekey="btn_ConsigneeNameResource1"/>
                                <b style="color: Red">*</b>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ConsigneeAddress" runat="server" Text="地址：" 
                                    meta:resourcekey="lbl_ConsigneeAddressResource1"></asp:Label>
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="Txt_ConsigneeAddress" runat="server" Width="800px" 
                                    TabIndex="21" TextMode="MultiLine" MaxLength="400"
                                    Style="text-transform: uppercase" OnTextChanged="Txt_ConsigneeAddress_TextChanged"
                                    AutoPostBack="True" meta:resourcekey="Txt_ConsigneeAddressResource1"></asp:TextBox><b style="color: Red">*</b>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ConsigneeCountry" runat="server" Text="国家：" 
                                    meta:resourcekey="lbl_ConsigneeCountryResource1"></asp:Label>
                            </td>
                            <td align="left" class="style2">
                                <asp:TextBox ID="Txt_ConsigneeCountry" runat="server" Width="80px" OnTextChanged="Txt_ConsigneeCountry_TextChanged"
                                    AutoPostBack="True" TabIndex="22" Style="text-transform: uppercase" 
                                    meta:resourcekey="Txt_ConsigneeCountryResource1"></asp:TextBox>
                                <b style="color: Red">*</b>
                                <cc1:AutoCompleteExtraExtender runat="server" ID="autoConsigneeCountry" ServiceMethod="GetCountryList"
                                    TargetControlID="Txt_ConsigneeCountry" AutoPostback="True"
                                    MinimumPrefixLength="1" OnItemSelected="autoConsigneeCountry_ItemSelected" 
                                    DelimiterCharacters="" Enabled="True" ServicePath="">
                                </cc1:AutoCompleteExtraExtender>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ConsigneeProvince" runat="server" Text="省份：" 
                                    meta:resourcekey="lbl_ConsigneeProvinceResource1"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_ConsigneeProvince" runat="server" Width="80px" 
                                    TabIndex="23" Style="text-transform: uppercase"
                                    OnTextChanged="Txt_ConsigneeProvince_TextChanged" AutoPostBack="True" 
                                    meta:resourcekey="Txt_ConsigneeProvinceResource1"></asp:TextBox>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ConsigneeRegion" runat="server" Text="城市：" 
                                    meta:resourcekey="lbl_ConsigneeRegionResource1"></asp:Label>
                            </td>
                            <td align="left" class="style1">
                                <asp:TextBox ID="Txt_ConsigneeRegion" runat="server" Width="80px" TabIndex="24" Style="text-transform: uppercase"
                                    AutoPostBack="True" OnTextChanged="Txt_ConsigneeRegion_TextChanged" 
                                    meta:resourcekey="Txt_ConsigneeRegionResource1"></asp:TextBox>
                                <b style="color: Red">*</b>
                                <cc1:AutoCompleteExtraExtender runat="server" ID="autoConsigneeRegion" ServiceMethod="GetRegionList"
                                    TargetControlID="Txt_ConsigneeRegion" MinimumPrefixLength="1" 
                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="">
                                </cc1:AutoCompleteExtraExtender>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ConsigneeZipCode" runat="server" Text="邮编：" 
                                    meta:resourcekey="lbl_ConsigneeZipCodeResource1"></asp:Label>
                            </td>
                            <td align="left" class="style2">
                                <asp:TextBox ID="Txt_ConsigneeZipCode" runat="server" Width="80px" 
                                    TabIndex="25" Style="text-transform: uppercase"
                                    OnTextChanged="Txt_ConsigneeZipCode_TextChanged" MaxLength="15" AutoPostBack="True" 
                                    meta:resourcekey="Txt_ConsigneeZipCodeResource1"></asp:TextBox>
                                <b style="color: Red">*</b>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ConsigneeContactor" runat="server" Text="姓名：" 
                                    meta:resourcekey="lbl_ConsigneeContactorResource1"></asp:Label>
                            </td>
                            <td align="left" class="style1">
                                <asp:TextBox ID="Txt_ConsigneeContactor" runat="server" MaxLength="40" Width="80px" TabIndex="26"
                                    Style="text-transform: uppercase" OnTextChanged="Txt_ConsigneeContactor_TextChanged"
                                    AutoPostBack="True" meta:resourcekey="Txt_ConsigneeContactorResource1"></asp:TextBox>
                                <b style="color: Red">*</b>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_ConsigneeTel" runat="server" Text="电话：" 
                                    meta:resourcekey="lbl_ConsigneeTelResource1"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_ConsigneeTel" runat="server" Width="80px" TabIndex="27" Style="text-transform: uppercase"
                                    OnTextChanged="Txt_ConsigneeTel_TextChanged" AutoPostBack="True" 
                                    meta:resourcekey="Txt_ConsigneeTelResource1" MaxLength="20"></asp:TextBox>
                                <b style="color: Red">*</b>
                                <asp:Button ID="btn_ConsigneeAddress" runat="server" Text="保存新地址" ForeColor="Red"
                                    Visible="False" CssClass="InputBtn" OnClick="btn_ConsigneeAddress_Click" 
                                    meta:resourcekey="btn_ConsigneeAddressResource1" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="FooterBtnBar">
        <asp:Button ID="But_Next" runat="server" Text="下一页" CssClass="InputBtn" TabIndex="28"
            OnClick="But_Next_Click" meta:resourcekey="But_NextResource1" />
        <asp:Button ID="But_Conel" runat="server" Text="重  填" CssClass="InputBtn" TabIndex="29"
            OnClick="But_Conel_Click" OnClientClick="return confirm('是否确认重填？');" 
                    meta:resourcekey="But_ConelResource1" />
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <div id="sg" class="sg">
    </div>
    <div id="branch_select" class="ps">
        <iframe frameborder="0" id="jiaofuframe1" scrolling="no" width="650" height="250">
        </iframe>
    </div>
    <div id="lishi" class="sg">
    </div>
    <div id="lishi_select" class="ps">
        <iframe frameborder="0" id="lishiframe1" scrolling="no" width="650" height="250">
        </iframe>
    </div>
    <style type="text/css">
        div.ps
        {
            position: absolute;
            display: none;
            z-index: 999;
            background-color: #FFFFFF;
            border: thin solid #CCCCCC;
            left: 150px;
            top: 100px;
            opacity: 1;
            filter: alpha(opacity=100);
        }
        
        div.sg
        {
            position: absolute;
            display: none;
            z-index: 200;
            height: auto;
            width: auto;
            background-color: #666666;
            left: 0px;
            top: 0px;
            border-top-style: none;
            border-right-style: none;
            border-bottom-style: none;
            border-left-style: none;
            opacity: 0.2;
            filter: alpha(opacity=20);
        }
    </style>
    <script type="text/javascript">
        function changeScope() {
            var width = document.body.clientWidth; //浏览器相对宽度
            var height = document.body.clientHeight; //浏览器相对高度
            var background = document.getElementById("sg"); //获取第一层玻璃
            var supplierPicIframe = document.getElementById("branch_select"); //获取第二层玻璃
            background.style.width = width;
            background.style.height = "900";
            background.style.display = "block";
            supplierPicIframe.style.display = "block";
            //ar supplierCode = document.getElementById("SupplierCode").value;
            //document.getElementById("frame1").src = "./DeliverAdd.aspx.aspx?SupplierCode=" + supplierCode;
            document.getElementById("jiaofuframe1").src = "./DeliverAdd.aspx";
        }
        function OpenShipperhistory() {
            var width = document.body.clientWidth; //浏览器相对宽度
            var height = document.body.clientHeight; //浏览器相对高度
            var background = document.getElementById("lishi"); //获取第一层玻璃
            var supplierPicIframe = document.getElementById("lishi_select"); //获取第二层玻璃
            background.style.width = width;
            background.style.height = "900";
            background.style.display = "block";
            supplierPicIframe.style.display = "block";
            //ar supplierCode = document.getElementById("SupplierCode").value;
            //document.getElementById("frame1").src = "./DeliverAdd.aspx.aspx?SupplierCode=" + supplierCode;
            document.getElementById("lishiframe1").src = "./Addresshistory.aspx";
        }
        function Context()//响应Enter事件
        {
//            if (event.keyCode == 13) {
            document.all("ContentPlaceHolder1_btn_ConsigneeName").click(); //设置要响应的的button
////                event.returnValue = false;
////            }
////            else
//            //                event.returnValue = true;
//            alert("恭喜成功~");
        }
    </script>
</asp:Content>
