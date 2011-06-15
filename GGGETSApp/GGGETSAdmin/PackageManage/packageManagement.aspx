<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="packageManagement.aspx.cs" Inherits="GGGETSAdmin.PackageManage.packageManagement"
    Theme="logisitc" %>

<%@ Register Assembly="AutoCompleteExtra" Namespace="AutoCompleteExtra" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table class="DataView" cellspacing="0" cellpadding="3">
                    <tbody>
                        <tr class="AlternatingRow">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_BagBarCode" runat="server" Text="包号:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_BagBarCode" runat="server" TabIndex="1" Width="250" Style="text-transform: uppercase"
                                    CssClass="TextBox"></asp:TextBox>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_UpCreateTime" TabIndex="2" runat="server" Width="75px" onfocusin="WdatePicker()"
                                    Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>-
                                <asp:TextBox ID="txt_ToCreateTime" TabIndex="3" runat="server" Width="75px" onfocusin="WdatePicker()"
                                    Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                            </td>
                            <td class="FieldHeader" style="width: 200px">
                                <asp:Label ID="lbl_Destination" runat="server" Text="起/终地三字码:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_OriginalRegionCode" TabIndex="4" runat="server" Width="50" MaxLength="3"
                                    Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>-
                                    <asp:TextBox ID="Txt_DestinationRegionCode" TabIndex="4" runat="server" Width="50" MaxLength="3"
                                    Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                                <cc1:AutoCompleteExtraExtender runat="server" ID="autocomplete" ServiceMethod="GetCountryList"
                                    TargetControlID="Txt_OriginalRegionCode" AsyncPostback="false" AutoPostback="true" MinimumPrefixLength="1"
                                    CompletionSetCount="10" OnItemSelected="autocomplete_ItemSelected">
                                </cc1:AutoCompleteExtraExtender>
                            </td>
                            <td>
                                <asp:Button ID="btn_Demand" TabIndex="5" runat="server" Text="查 询" CssClass="InputBtn"
                                    OnClick="btn_Demand_Click" />
                                <asp:Button runat="server" Text="提 交" ID="btnSubmit" CssClass="InputBtn" 
                                    onclick="btnSubmit_Click1"></asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div style="height: 350px; overflow-x: auto; overflow-y: auto;">
                    <asp:GridView ID="gv_HAWB" runat="server" CssClass="DataView" AutoGenerateColumns="False"
                        PageSize="36" onrowcommand="gv_HAWB_RowCommand" 
                        onrowdatabound="gv_HAWB_RowDataBound" DataKeyNames="BarCode">
                        <Columns>
                            <asp:TemplateField HeaderText="请选择">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="ckSelect" Visible='<%# IsSubmitDisplay(Convert.ToString(Eval("IsSubmit"))) %>'></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="行号">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Number" runat="server" Text='<%# N() %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="包号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_BagBarCoder" runat="server" CommandName="Eidt" CommandArgument='<%# Eval("BarCode") %>'
                                        Text='<%# Eval("BarCode") %>' ></asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Width="120px" />
                                <controlstyle width="120px" />
                                <controlstyle width="120px" />
                                <controlstyle width="120px" />
                                <controlstyle width="120px" />
                                <controlstyle width="120px" />
                                <controlstyle width="120px" />
                                <controlstyle width="120px" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="起始地三字码">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_OriginalRegionCode" runat="server" Text='<%# Eval("OriginalRegionCode") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="目的地三字码">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_DestinationRegionCode" runat="server" Text='<%# Eval("DestinationRegionCode") %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="提交状态">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsSubmit" runat="server" Text='<%# IsSubmitStr(Convert.ToString(Eval("IsSubmit"))) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操 作">
                                <ItemTemplate>
                                    <asp:Button runat="server" Text="拆 包" CssClass="InputBtn" ID="btnSplitPackage" 
                                        CommandArgument='<%# Eval("BarCode") %>' onclick="btnSplitPackage_Click"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div id="FenYe" runat="server" visible="false" class="DataView">
                <div style="text-align: center">
                    <asp:Button ID="btn_homepage" runat="server" Text="首页" CssClass="InputBtn" OnClick="btn_homepage_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Up" runat="server" Text="上一页" OnClick="btn_Up_Click" CssClass="InputBtn" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lbl_nuber" runat="server" ForeColor="Red"></asp:Label><b style="color: Red">/</b>
                    <asp:Label ID="lbl_sumnuber" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Jumpto" runat="server" Text="跳转到" CssClass="InputBtn" OnClick="btn_Jumpto_Click" />
                    <asp:TextBox ID="Txt_Jumpto" runat="server" Width="30" CssClass="TextBox" onblur="NumberCheck(this)"></asp:TextBox>
                    <asp:Button ID="btn_down" runat="server" Text="下一页" OnClick="btn_down_Click" CssClass="InputBtn" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_lastpage" runat="server" Text="末页" CssClass="InputBtn" OnClick="btn_lastpage_Click" />
                    <asp:Button runat="server" Text="Button" ID="btnRefresh" Visible="False"></asp:Button>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
