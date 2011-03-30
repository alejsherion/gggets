<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="packageAdd.aspx.cs" Inherits="GGGETSAdmin.PackageManage.packageAdd" Theme="logisitc" %>

<%@ Register Assembly="AutoCompleteExtra" Namespace="AutoCompleteExtra" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table class="DataView">
                    <tr class="Row">
                        <td class="FieldHeader" style="width: 120px">
                            <asp:Label ID="lbl_BagBarCode" runat="server" Text="包号:"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="Txt_BagBarCode" runat="server" Width="250" TabIndex="1" Style="text-transform: uppercase"
                                OnTextChanged="Txt_BagBarCode_TextChanged" MaxLength="45" AutoPostBack="True" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader">
                            <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txt_CreateTime" runat="server"></asp:Label>
                        </td>
                        <td class="FieldHeader">
                            <asp:Label ID="lbl_PackageType" runat="server" Text="是否混包:"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbtn_PackageType" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0" Text="否"></asp:ListItem>
                                <asp:ListItem Value="1" Text="是"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader" style="width:150px">
                            <asp:Label ID="lbl_Region" runat="server" Text="起/终三字码:"></asp:Label>
                        </td>
                        <td style="width: 80px">
                            <asp:TextBox ID="Txt_OriginalRegionCode" runat="server" TabIndex="2" Width="50" 
                                MaxLength="3" AutoPostBack="true" OnTextChanged="Txt_Region_TextChanged" Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                                -
                            <asp:TextBox ID="Txt_DestinationRegionCode" runat="server" TabIndex="2" Width="50" 
                                MaxLength="3" AutoPostBack="true" Style="text-transform: uppercase" 
                                CssClass="TextBox" ontextchanged="Txt_DestinationRegionCode_TextChanged"></asp:TextBox>
                           
                            <%--<cc1:AutoCompleteExtraExtender runat="server" ID="autocomplete" ServiceMethod="GetCountryList"
                                TargetControlID="txt_Destination" AsyncPostback="false" AutoPostback="true" MinimumPrefixLength="1"
                                CompletionSetCount="10" OnItemSelected="autocomplete_ItemSelected">
                            </cc1:AutoCompleteExtraExtender>--%>
                        </td>
                        <td class="FieldHeader" style="width: 30px">
                            <asp:Label ID="lbl_Pice" runat="server" Text="件数:"></asp:Label>
                        </td>
                        <td style="width: 80px">
                            <asp:Label ID="txt_Pice" runat="server"></asp:Label>
                        </td>
                        <td class="FieldHeader">
                            <asp:Label ID="lbl_TotalWeight" runat="server" Text="总重量:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Txt_TotalWeight" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btn_Save" runat="server" CssClass="InputBtn" Text="保 存" OnClick="btn_Save_Click"
                                TabIndex="5" UseSubmitBehavior="false" />
                        </td>
                        <td>
                            <asp:Button ID="btn_SaveAndClose" runat="server" CssClass="InputBtn" Text="保存并锁定"
                                OnClick="btn_SaveAndClose_Click" TabIndex="6" />
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader">
                            <asp:Label ID="lbl_BarCode" runat="server" Text="运单号:"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txt_BarCode" runat="server" Width="250" TabIndex="3" Style="text-transform: uppercase" onkeydown="Context()" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <asp:Button ID="btn_Add" runat="server" CssClass="InputBtn" Text="添 加" OnClick="btn_Add_Click"
                                TabIndex="4" />
                        </td>
                    </tr>
                    <tr class="FieldHeader">
                        <td colspan="8">
                            <asp:GridView ID="gv_HAWB" runat="server" AutoGenerateColumns="False" CssClass="DataView"
                                DataKeyNames="BarCode" PageSize="500">
                                <Columns>
                                    <asp:TemplateField HeaderText="行号">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Number" runat="server" Text='<%# N() %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="运单号">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_BarCoder" runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="120px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="重量">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Weight" runat="server" Text='<%# Eval("TotalWeight") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <input id="CheckedAll" name="CheckedAll" type="checkbox" onclick="CheckAll(this,'<%=gv_HAWB.ClientID %>',3)"/>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkId" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td align="right" class="FieldHeader" colspan="5">
                            <asp:Button ID="btn_Close" runat="server" CssClass="InputBtn" Text="移 除" OnClick="btn_Close_Click" OnClientClick="return confirm('是否确认删除？');"  />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        function CheckAll(checkBox, gvid, index) {
            checkBox.checked = checkBox.checked ? false : true;
            var gridview = document.getElementById(gvid);
            var rowlength = gridview.rows.length;
            for (var i = 0; i < rowlength; i++) {
                var input = gridview.rows[i].cells[index].getElementsByTagName("input");
                if (input[0].type == "checkbox") {
                    input[0].checked = input[0].checked ? false : true;
                }
            }
        }
        function Context()//响应Enter事件
        {
            if (event.keyCode == 13) {
                if (document.getElementById("ContentPlaceHolder1_btn_Add").disabled) {
                    document.all("ContentPlaceHolder1_btn_Add").click(); //设置要响应的的button
                    event.returnValue = false;
                }
            }
            else
                event.returnValue = true;
        }
    </script>
</asp:Content>
