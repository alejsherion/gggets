<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProductModify.aspx.cs" Inherits="GGGETSAdmin.ProductManage.ProductModify" Theme="logisitc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table class="DataView">
                    <tr class="Row">
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_HSCode" runat="server" Text="HS编码："></asp:Label>
                        </td>
                        <td style="width: 200px">
                            <asp:TextBox ID="Txt_HSCode" runat="server" TabIndex="1" Width="200" MaxLength="20" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_HSName" runat="server" Text="商品名称："></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="Txt_HSName" runat="server" TabIndex="2" Width="400" MaxLength="30" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_DiscountTax" runat="server" Text="优惠税率："></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_DiscountTax" runat="server" TabIndex="3" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_GeneralTax" runat="server" Text="普通税率："></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_GeneralTax" runat="server" TabIndex="4" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_ExportTax" runat="server" Text="出口税率："></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_ExportTax" runat="server" TabIndex="5" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="AlternatingRow">
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_ConsumeTax" runat="server" Text="消费税率："></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_ConsumeTax" runat="server" TabIndex="6" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_RiseTax" runat="server" Text="增值税率："></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_RiseTax" runat="server" TabIndex="7" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader" align="right" style="width: 200px">
                            <asp:Label ID="lbl_CertificateSign" runat="server" Text="所需证件标志："></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_CertificateSign" runat="server" TabIndex="8" MaxLength="20" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader" align="right" style="width: 250px">
                            <asp:Label ID="lbl_PricingSign" runat="server" Text="重点审价标志："></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_PricingSign" runat="server" TabIndex="9" MaxLength="20" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader" align="right" style="width: 250px">
                            <asp:Label ID="lbl_TaxDemandSign" runat="server" Text="征税要求标记："></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_TaxDemandSign" runat="server" TabIndex="10" MaxLength="20" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr class="Row">
                        <td class="FieldHeader" align="right">
                            <asp:Label ID="lbl_Remark" runat="server" Text="备注："></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="Txt_Remark" runat="server" TabIndex="11" TextMode="MultiLine" Height="50"
                                Width="400" MaxLength="130" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td class="FieldHeader" align="center">
                            <asp:Button ID="btn_Addparoduct" runat="server" TabIndex="12" CssClass="InputBtn" Text="修 改"
                                OnClick="btn_Addparoduct_Click" />
                        </td>
                        <td align="left">
                            <asp:Button ID="But_Conel" runat="server" Text="返 回" CssClass="InputBtn" TabIndex="13"
                                OnClick="But_Conel_Click" />
                        </td>
                    </tr>
                </table>
                 <table class="DataView">
                        <tr class="Row">
                            <td class="FieldHeader">
                                <asp:Label ID="lbl_Property" runat="server" Text="属性名:"></asp:Label>
                            </td>
                            <td style="width:100px">
                                <asp:TextBox ID="Txt_Property" runat="server" MaxLength="20" CssClass="TextBox"></asp:TextBox>
                            </td>
                            <td class="FieldHeader" style="width:100px">
                                <asp:Label ID="lbl_ChineseRemark" runat="server" Text="中文备注:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Txt_ChineseRemark" runat="server" Width="500" MaxLength="50" onkeydown="Context()" CssClass="TextBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btn_Add" runat="server" CssClass="InputBtn" Text="添 加" 
                                    onclick="btn_Add_Click" />
                            </td>
                        </tr>
                    </table>
                <asp:GridView ID="Gv_Productiy" runat="server" AutoGenerateColumns="False" PageSize="100"
                    DataKeyNames="HSPID" onrowcancelingedit="Gv_Productiy_RowCancelingEdit" 
                    onrowediting="Gv_Productiy_RowEditing" onrowupdating="Gv_Productiy_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="行号">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nuber" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="属性名称">
                            <ItemTemplate>
                                <asp:Label ID="lbl_PropertyName" runat="server" Text='<%# Eval("PropertyName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="Txt_PropertyName" runat="server" Text='<%# Eval("PropertyName") %>' CssClass="TextBox"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="中文备注">
                            <ItemTemplate>
                                <asp:Label ID="lbl_ChineseRemark" runat="server" Text='<%# Eval("ChineseRemark") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="Txt_ChineseRemark" runat="server" Width="550" Text='<%# Eval("ChineseRemark") %>' CssClass="TextBox"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="550px" />
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <input id="CheckedAll" name="CheckedAll" type="checkbox" onclick="CheckAll(this,'<%=Gv_Productiy.ClientID %>',4)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkId" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div id="DVProperty" runat="server">
                <asp:Button ID="btn_Close" runat="server" CssClass="InputBtn" Text="移 除" OnClick="btn_Close_Click"
                    OnClientClick="return confirm('是否确认删除？');" />
                    </div>
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
                document.all("ContentPlaceHolder1_btn_Add").click(); //设置要响应的的button
                event.returnValue = false;
            }
            else
                event.returnValue = true;
        }
        function Url() {
            alert("修改成功！");
            top.location = 'ProductManagemnet.aspx';
        }
    </script>
</asp:Content>
