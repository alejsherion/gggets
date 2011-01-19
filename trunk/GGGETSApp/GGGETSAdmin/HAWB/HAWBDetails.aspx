<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="HAWBDetails.aspx.cs" Inherits="GGGETSAdmin.HAWB1.HAWBSetAndEit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
<asp:Content ID="Content4" runat="server" 
    contentplaceholderid="CPH_UserControl">
            <div style="width:64%; float:left">
            <script type="text/javascript" src="../Scripts/calendar.js"></script>
                <div>
                    <table>
                        <tr>
                            <td>条形码:</td>
                            <td><asp:TextBox ID="Txt_BarCode" runat="server"></asp:TextBox></td>
                            <td>承运公司名称:</td>
                            <td><asp:TextBox ID="Txt_Carrier" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>承运公司运单号:</td>
                            <td><asp:TextBox ID="Txt_CarrierHAWBID" runat="server"></asp:TextBox></td>
                            <td>客户帐号:</td>
                            <td><asp:TextBox ID="Txt_Account" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>结算方式:</td>
                            <td><asp:DropDownList ID="DDl_SettleType" runat="server">
                                <asp:ListItem>先付</asp:ListItem>
                                <asp:ListItem>到付</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>服务方式:</td>
                            <td><asp:DropDownList ID="DDl_ServiceType" runat="server">
                                <asp:ListItem>空运</asp:ListItem>
                                <asp:ListItem>快件</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>运单截止日期:</td>
                            <td><asp:TextBox ID="Txt_DeadlineTime" runat="server" onfocusin="calendar()"></asp:TextBox></td>
                            <td>负责人:</td>
                            <td><asp:TextBox ID="Txt_Owner" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>发件人编号:</td>
                            <td><asp:DropDownList ID="DDl_ShipperID" runat="server"></asp:DropDownList></td>
                            <td>发件联系人:</td>
                            <td><asp:TextBox ID="Txt_ShipperContactor" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>发件国家二字码:</td>
                            <td><asp:TextBox ID="Txt_ShipperCountry" runat="server"></asp:TextBox></td>
                            <td>发件地区三字码:</td>
                            <td><asp:TextBox ID="Txt_ShipperRegion" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>发件地址:</td>
                            <td><asp:TextBox ID="Txt_ShipperAddress" runat="server"></asp:TextBox></td>
                            <td>发件联系电话:</td>
                            <td><asp:TextBox ID="Txt_ShipperTel" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>发件邮编:</td>
                            <td><asp:TextBox ID="Txt_ShipperZipCode" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>收件人编号:</td>
                            <td><asp:DropDownList ID="DDl_ConsigneeID" runat="server"></asp:DropDownList></td>
                            <td>收件联系人:</td>
                            <td><asp:TextBox ID="Txt_ConsigneeContactor" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>收件国家二字码:</td>
                            <td><asp:TextBox ID="Txt_ConsigneeCountry" runat="server"></asp:TextBox></td>
                            <td>收件地区三字码:</td>
                            <td><asp:TextBox ID="Txt_ConsigneeRegion" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>收件地址:</td>
                            <td><asp:TextBox ID="Txt_ConsigneeAddress" runat="server"></asp:TextBox></td>
                            <td>收件联系电话:</td>
                            <td><asp:TextBox ID="Txt_ConsigneeTel" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>收件邮编:</td>
                            <td><asp:TextBox ID="Txt_ConsigneeZipCode" runat="server"></asp:TextBox></td>
                            <td>添加货物信息</td>
                            <td>
                                <asp:Button ID="But_AddItme" runat="server" Text="添加货物信息"  /></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>重量类型:</td>
                            <td><asp:DropDownList ID="DDl_WeightType" runat="server">
                                    <asp:ListItem>实重</asp:ListItem>
                                    <asp:ListItem>泡重</asp:ListItem>
                                    <asp:ListItem>加权</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>总重量:</td>
                            <td><asp:TextBox ID="Txt_Weight" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>件数:</td>
                            <td><asp:TextBox ID="Txt_Piece" runat="server"></asp:TextBox></td>
                            <td>国际/国内运单:</td>
                            <td><asp:TextBox ID="Txt_IsInternational" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>特别通关指示:</td>
                            <td><asp:TextBox ID="Txt_SpecialInstruction" runat="server"></asp:TextBox></td>
                            <td>关税结算方式:</td>
                            <td><asp:TextBox ID="Txt_Taxes" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>运单内容：</td>
                            <td colspan="3"><asp:TextBox ID="Txt_Description" runat="server" 
                                    TextMode="MultiLine" Width="331px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>运单备注：</td>
                            <td colspan="3"><asp:TextBox ID="Txt_Remark" runat="server" 
                                    TextMode="MultiLine" Width="331px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="But_AddHAWB" runat="server" Text="提 交" 
                                    onclick="But_AddHAWB_Click" /></td>
                                    <td align="center" colspan="2">
                                <asp:Button ID="But_Conel" runat="server" Text="返 回" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Content>

