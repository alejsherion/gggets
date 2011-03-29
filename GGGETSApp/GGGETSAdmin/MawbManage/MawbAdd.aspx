<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="MawbAdd.aspx.cs" Inherits="GGGETSAdmin.MawbManage.MawbAdd" Theme="logisitc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table class="DataView">
                    <tbody>
                        <tr class="AlternatingRow">
                            <td class="FieldHeader" align="left" style="width: 80px">
                                <asp:Label ID="lbl_MAWBBarCode" runat="server" Text="总运单号:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <%--<input id="Txt_MAWBBarCode" name="Txt_MAWBBarCode" type="text" class="TextBox" runat="server" onblur="MAWBBarCode(this)" style="width:250px" />--%>
                                <asp:TextBox ID="Txt_MAWBBarCode" runat="server" Width="250" CssClass="TextBox" TabIndex="1"
                                    Style="text-transform: uppercase" OnTextChanged="Txt_MAWBBarCode_TextChanged"
                                    AutoPostBack="True" MaxLength="45"></asp:TextBox>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txt_CreateTime" runat="server"></asp:Label>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_UpdateTime" runat="server" Text="修改时间:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txt_UpdateTime" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader" align="left">
                                <asp:Label ID="lbl_FLTNo" runat="server" Text="航班号:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txt_FLTNo" runat="server" MaxLength="45" Width="250" CssClass="TextBox" TabIndex="2"
                                    Style="text-transform: uppercase"></asp:TextBox>
                            </td>
                            <td class="FieldHeader" style="width:150px">
                                <asp:Label ID="lbl_AirportRegion" runat="server" Text="机场三字码:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_From" runat="server" Width="50" MaxLength="3" CssClass="TextBox"
                                    TabIndex="3" Style="text-transform: uppercase"></asp:TextBox>To
                                <asp:TextBox ID="txt_To" runat="server" Width="50" MaxLength="3" CssClass="TextBox"
                                    TabIndex="4" Style="text-transform: uppercase"></asp:TextBox>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_Pice" runat="server" Text="件数:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txt_Pice" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlternatingRow">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_TotalWeight" runat="server" Text="总重量:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Txt_TotalWeight" runat="server"></asp:Label>
                            </td>
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_TotalVolume" runat="server" Text="总体积:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txt_TotalVolume" runat="server"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Button ID="btn_Save" runat="server" Text="保 存" CssClass="InputBtn" OnClick="btn_Save_Click"
                                    TabIndex="7" />
                            </td>
                            <td colspan="2">
                                <asp:Button ID="btn_SaveAndClose" runat="server" Text="保存并锁定" CssClass="InputBtn"
                                    OnClick="btn_SaveAndClose_Click" TabIndex="8" />
                            </td>
                        </tr>
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_BagBarCode" runat="server" Text="包号:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="Txt_BagBarCode" runat="server" Width="250" CssClass="TextBox" TabIndex="5"
                                    Style="text-transform: uppercase" onkeydown="Context()"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:Button ID="btn_Add" runat="server" Text="添 加" CssClass="InputBtn" OnClick="btn_Add_Click"
                                    TabIndex="6" />
                            </td>
                        </tr>
                        <tr class="AlternatingRow">
                            <td colspan="8">
                                <asp:GridView ID="gv_Bag" runat="server" CssClass="DataView" AutoGenerateColumns="False"
                                    DataKeyNames="BarCode" PageSize="50">
                                    <Columns>
                                        <asp:TemplateField HeaderText="行号">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Number" runat="server" Text='<%# N() %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="包号">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_BagBarCode" runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="300px" />
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
                            </td>
                        </tr>
                        <tr class="Row">
                            <td align="right" class="FieldHeader" colspan="5">
                                <asp:Button ID="btn_Close" runat="server" CssClass="InputBtn" Text="移 除" OnClick="btn_Close_Click"
                                    OnClientClick="return confirm('是否确认删除？');" />
                            </td>
                        </tr>
                    </tbody>
                </table>
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
                document.all("ContentPlaceHolder1_btn_Add").click(); //设置要响应的的button
                event.returnValue = false;
            }
            else
                event.returnValue = true;
        }
    </script>
</asp:Content>
