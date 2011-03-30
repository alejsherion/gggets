<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="packageModify.aspx.cs" Inherits="GGGETSAdmin.PackageManage.packageModify" Theme="logisitc" %>

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
                    <tbody>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_BagBarCode" runat="server" Text="包号:"></asp:Label>
                            </td>
                            <td colspan="3" align="left">
                                <asp:LinkButton ID="lbtn_BagBarCode" runat="server" Width="250" OnClick="lbtn_BagBarCode_Click"></asp:LinkButton>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_FLTNo" runat="server" Text="航班号:"></asp:Label>
                            </td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="txt_FLTNo" TabIndex="1" MaxLength="45" runat="server" Width="250" Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader" align="right">
                                <asp:Label ID="lbl_MAWBNCode" runat="server" Text="总运单号:"></asp:Label>
                            </td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="Txt_MAWBCode" TabIndex="2" MaxLength="45" runat="server" Width="250" 
                                    Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                            </td>
                            <td class="FieldHeader" align="right" style="width:150px">
                                <asp:Label ID="lbl_CreateTime1" runat="server" Text="创建时间:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="txt_CreateTime" runat="server"></asp:Label>
                            </td>
                            <td class="FieldHeader" align="right">
                                <asp:Label ID="lbl_UpdateTime" runat="server" Text="修改时间:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="txt_UpdateTime" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader" align="right" style="width:200px">
                                <asp:Label ID="lbl_Destination" runat="server" Text="目的三字码:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_Destination" runat="server" Width="50" TabIndex="3" MaxLength="3" Style="text-transform: uppercase" CssClass="TextBox"></asp:TextBox>
                                <%--<cc1:AutoCompleteExtraExtender runat="server" ID="autocomplete" ServiceMethod="GetCountryList"
                                    TargetControlID="txt_Destination" AsyncPostback="false" AutoPostback="true" MinimumPrefixLength="1"
                                    CompletionSetCount="10" OnItemSelected="autocomplete_ItemSelected">
                                </cc1:AutoCompleteExtraExtender>--%>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_Pice" runat="server" Text="件数:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="txt_Pice" runat="server"></asp:Label>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_TotalWeight" runat="server" Text="总重量:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="Txt_TotalWeight" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btn_Save" runat="server" Text="修 改" CssClass="InputBtn" OnClick="btn_Save_Click"
                                    TabIndex="6" />
                            </td>
                            <td>
                                <asp:Button ID="btn_SaveAndClose" runat="server" Text="修改并锁定" CssClass="InputBtn"
                                    OnClick="btn_SaveAndClose_Click" TabIndex="7" />
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_BarCode" runat="server" Text="运单号:"></asp:Label>
                            </td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="txt_BarCode" TabIndex="4" runat="server" Width="250" Style="text-transform: uppercase" onkeydown="Context()" CssClass="TextBox"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:Button ID="btn_Add" TabIndex="5" runat="server" Text="添 加" CssClass="InputBtn"
                                    OnClick="btn_Add_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div style="height: 350px;overflow-x:auto;overflow-y:auto;">
                <asp:GridView ID="gv_HAWB" runat="server" CssClass="DataView" AutoGenerateColumns="False"
                    DataKeyNames="BarCode" PageSize="35">
                    <Columns>
                        <asp:TemplateField HeaderText="行号">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Number" runat="server" Text='<%# N() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="运单号">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtn_BarCoder" PostBackUrl='<%# "../HAWBManage/HAWBDetails.aspx?BarCode="+Eval("BarCode") %>'
                                    runat="server" Text='<%# Eval("BarCode") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ControlStyle Width="150px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="重量">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TotalWeight" runat="server" Text='<%# Eval("TotalWeight") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="Ck_ID" runat="server" onclick="checkAll(this,'chkId')" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkId" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
            </div>
            <div class="FooterBtnBar">
                <asp:Button ID="btn_Close" runat="server" CssClass="InputBtn" Text="删 除" OnClick="btn_Close_Click"
                    OnClientClick="return confirm('是否确认删除？');" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        function checkAll(obj, form_id) {
            var idstr = "";
            var objs;
            if (obj.checked) {
                objs = document.forms[0].elements;
                for (i = 0; i < objs.length; i++) {
                    objs[i].checked = true;

                }
            }
            else {
                objs = document.forms[0].elements;
                for (i = 0; i < objs.length; i++) {
                    objs[i].checked = false;

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
        function Url() {
            alert("修改成功！");
            top.location = 'packageManagement.aspx';
        }
    </script>
</asp:Content>
