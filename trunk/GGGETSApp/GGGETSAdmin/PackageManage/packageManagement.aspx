﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="packageManagement.aspx.cs" Inherits="GGGETSAdmin.PackageManage.packageManagement"
    Theme="logisitc"%>

<%@ Register Assembly="AutoCompleteExtra" Namespace="AutoCompleteExtra" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../Scripts/calendar.js"></script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <table class="DataView" cellspacing="0" cellpadding="3">
            <tbody>
                <tr class="AlternatingRow">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_BagBarCode" runat="server" Text="包号:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_BagBarCode" runat="server" TabIndex="1" Width="250" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_UpCreateTime" TabIndex="2" runat="server" Width="75px" onfocusin="calendar()" Style="text-transform: uppercase"></asp:TextBox>-
                        <asp:TextBox ID="txt_ToCreateTime" TabIndex="3" runat="server" Width="75px" onfocusin="calendar()" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_Destination" runat="server" Text="目的地三字码:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_Destination" TabIndex="4" runat="server" Width="50" MaxLength="3" Style="text-transform: uppercase"></asp:TextBox>
                        <cc1:AutoCompleteExtraExtender runat="server" ID="autocomplete" ServiceMethod="GetCountryList"
                            TargetControlID="txt_Destination" AsyncPostback="false" AutoPostback="true" MinimumPrefixLength="1"
                            CompletionSetCount="10" OnItemSelected="autocomplete_ItemSelected">
                        </cc1:AutoCompleteExtraExtender>
                    </td>
                    <td>
                        <asp:Button ID="btn_Demand" TabIndex="5" runat="server" Text="查 询" CssClass="InputBtn"
                            OnClick="btn_Demand_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="height: 350px;overflow-x:auto;overflow-y:auto;">
        <asp:GridView ID="gv_HAWB" runat="server" CssClass="DataView" 
            AutoGenerateColumns="False" PageSize="36">
            <Columns>
                <asp:TemplateField HeaderText="行号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Number" runat="server" Text='<%# N() %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="包号">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_BagBarCoder" runat="server" PostBackUrl='<%# "PackageDetails.aspx?BarCode="+Eval("BarCode") %>'
                            Text='<%# Eval("BarCode") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Width="120px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="目的地三字码">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RegionCode" runat="server" Text='<%# Eval("RegionCode") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="重量">
                    <ItemTemplate>
                        <asp:Label ID="lbl_TotalWeight" runat="server" Text='<%# Eval("TotalWeight") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Status").ToString().Replace("0","打开").Replace("1","关闭")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="是否混包">
                    <ItemTemplate>
                        <asp:Label ID="lbl_IsMixed" runat="server" Text='<%# Eval("Status").ToString().Replace("1","是").Replace("0","否")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <%--<asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="Ck_ID" runat="server" onclick="checkAll(this,'chkId')" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkId" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        </div>
    </div>
    <div id="FenYe" runat="server" visible="false" class="DataView">
        <asp:Button ID="btn_homepage" runat="server" Text="首页" CssClass="InputBtn" 
            onclick="btn_homepage_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Up" runat="server" Text="上一页" onclick="btn_Up_Click" CssClass="InputBtn" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lbl_nuber" runat="server" ForeColor="Red"></asp:Label><b style="color: Red">/</b>
        <asp:Label ID="lbl_sumnuber" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Jumpto" runat="server" Text="跳转到" CssClass="InputBtn" 
            onclick="btn_Jumpto_Click" />
        <asp:TextBox ID="Txt_Jumpto" runat="server" Width="30" CssClass="TextBox" onblur="NumberCheck(this)"></asp:TextBox>
        <asp:Button ID="btn_down" runat="server" Text="下一页" onclick="btn_down_Click" CssClass="InputBtn" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_lastpage" runat="server" Text="末页" CssClass="InputBtn" 
            onclick="btn_lastpage_Click" />
    </div>
    <script type="text/javascript">

        function NumberCheck(name) {
            var s = name.value;
            var regu = /^[0-9]*$/;
            var re = new RegExp(regu);
            if (s.search(re) == -1) {
                name.select();
                alert("页数只能输入整数")
            }
        }
    </script>
</asp:Content>
