<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliverAdd.aspx.cs" Inherits="GGGETSAdmin.HAWBManage.DeliverAdd" Theme="logisitc"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="AutoCompleteExtra" Namespace="AutoCompleteExtra" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <style type="text/css">
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
        div.ps1
        {
            position: absolute;
            display: none;
            z-index: 999;
            background-color: #FFFFFF;
            border: thin solid #CCCCCC;
            left: 0px;
            top: 0px;
            opacity: 1;
            filter: alpha(opacity=100);
        }
    </style>
    <script type="text/javascript">
        function closeList2() {   
            var background = parent.document.getElementById("sg");
            var supplierPicIframe = parent.document.getElementById("branch_select");
            background.style.display = "none";
            supplierPicIframe.style.display = "none";
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <cc2:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc2:ToolkitScriptManager>
        <table class="DataView">
            <tbody id="tbDeliver" runat="server">
                <tr class="EditRow">
                    <td colspan="6" align="left">
                        <asp:Label ID="lbl_Deliver" runat="server" Text="交付人信息"></asp:Label>
                        <%--<asp:LinkButton ID="lbtn_Deliverhistory" runat="server" Text="[历史]" 
                            onclick="lbtn_Deliverhistory_Click"></asp:LinkButton>--%>
                        <asp:LinkButton ID="lbtn_Deliverhistory"
                               runat="server" Text="[历史]" OnClick="lbtn_Deliverhistory_Click1"></asp:LinkButton> 

                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverName" runat="server" Text="公司:"></asp:Label>
                    </td>
                    <td align="left" colspan="5">
                        <asp:TextBox ID="Txt_DeliverName" runat="server" MaxLength="180" Width="500" TabIndex="1" 
                            Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverAddress" runat="server" Text="地址:"></asp:Label>
                    </td>
                    <td align="left" colspan="5">
                        <asp:TextBox ID="Txt_DeliverAddress" runat="server" Width="500" MaxLength="400" TabIndex="2" TextMode="MultiLine"
                            Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverCountry" runat="server" Text="国家:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_DeliverCountry" runat="server" Width="80" OnTextChanged="Txt_DeliverCountry_TextChanged"
                            AutoPostBack="true" TabIndex="3" Style="text-transform: uppercase"></asp:TextBox>
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
                        <asp:TextBox ID="Txt_DeliverProvince" runat="server" Width="80" TabIndex="4" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverRegion" runat="server" Text="城市:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_DeliverRegion" runat="server" Width="80" OnTextChanged="Txt_DeliverRegion_TextChanged" TabIndex="5" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
                        <cc1:AutoCompleteExtraExtender runat="server" ID="autoDeliverRegion" ServiceMethod="GetRegionList"
                            TargetControlID="Txt_DeliverRegion" AsyncPostback="false" 
                            MinimumPrefixLength="1" CompletionSetCount="10"
                            UseContextKey="True">
                        </cc1:AutoCompleteExtraExtender>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverZipCode" runat="server" Text="邮编:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_DeliverZipCode" runat="server" MaxLength="15" Width="80" TabIndex="6"
                            Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverContactor" runat="server" Text="姓名:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_DeliverContactor" runat="server" MaxLength="40" Width="80" TabIndex="7" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_DeliverTel" runat="server" Text="电话:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_DeliverTel" runat="server" Width="80" MaxLength="20" TabIndex="8" Style="text-transform: uppercase"></asp:TextBox>
                        <b style="color: Red">*</b>
                    </td>
                </tr>
                <tr class="Row">
                    <td align="center" colspan="6">
                        <asp:Button ID="btn_AddDeliver" runat="server" CssClass="InputBtn" Text="添 加" OnClick="btn_AddDeliver_Click1"/>
                        <asp:Button ID="btn_Close" runat="server" CssClass="InputBtn" Text="取 消" OnClientClick="closeList2()" />
                    </td>
                </tr>
            </tbody>
        </table>
    <div id="lishi" class="sg">
    </div>
    <div id="lishi_select" class="ps1">
        <iframe frameborder="0" id="lishiframe1" scrolling="no" width="650" height="250">
        </iframe>
    </div>
    </form>
</body>
</html>
