<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="packageAdd.aspx.cs" Inherits="GGGETSAdmin.PackageManage.packageAdd" Theme="logisitc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">      

    <div class="FunctionBar">
        <table class="DataView">
            <tr class="Row">
                <td class="FieldHeader" style="width:120px">
                    <asp:Label ID="lbl_BagNumber" runat="server" Text="包号:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="Txt_BagNumber" runat="server" Width="250"></asp:TextBox>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txt_CreateTime" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_UpdateTime" runat="server" Text="修改时间:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txt_UpdateTime" runat="server" CssClass="TextBox"></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_Region" runat="server" Text="目的地三字码:"></asp:Label>
                </td>
                <td style="width:80px">
                    <asp:TextBox ID="txt_Destination" runat="server" Width="50"></asp:TextBox>
                </td>
                <td class="FieldHeader" style="width:50px">
                    <asp:Label ID="lbl_Pice" runat="server" Text="件数:"></asp:Label>
                </td>
                <td style="width:80px">
                    <asp:Label ID="txt_Pice" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td class="FieldHeader">
                    <asp:Label ID="lbl_TotalWeight" runat="server" Text="总重量:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Txt_TotalWeight" runat="server" CssClass="TextBox"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btn_Save" runat="server" CssClass="InputBtn" Text="保 存" 
                        onclick="btn_Save_Click" />
                </td>
                <td>
                    <asp:Button ID="btn_SaveAndClose" runat="server" CssClass="InputBtn" 
                        Text="保存并锁定" onclick="btn_SaveAndClose_Click" />
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader">
                    <asp:Label ID="lbl_BarCode" runat="server" Text="运单号:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txt_BarCode" runat="server" Width="250"></asp:TextBox>
                </td>
                <td colspan="2">
                    <input id="Button1" type="button" name="Button1" value="按钮" onclick="javascript:__doPostBack('Button1','')">
                    <asp:Button ID="btn_Add" runat="server" CssClass="InputBtn" Text="添 加" 
                        onclick="btn_Add_Click"/>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:GridView ID="gv_HAWB" runat="server" AutoGenerateColumns="False" 
                        CssClass="DataView" DataKeyNames="BarCode">
                        <Columns>
                            <asp:TemplateField HeaderText="编号">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Number" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="运单号">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BarCoder" runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="重量">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Weight" runat="server" Text='<%# Eval("TotalWeight") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="Ck_ID" runat="server" onclick="checkAll(this,'chkId')" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr class="Row">
                <td align="right" class="FieldHeader" colspan="5">
                    <asp:Button ID="btn_Close" runat="server" CssClass="InputBtn" Text="删 除" 
                        onclick="btn_Close_Click" OnClientClick="return confirm('是否确认删除？');" />
                </td>
            </tr>
        </table>
    </div>
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
    </script>
</asp:Content>
