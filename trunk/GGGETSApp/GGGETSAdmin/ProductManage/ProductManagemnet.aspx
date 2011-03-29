<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProductManagemnet.aspx.cs" Inherits="GGGETSAdmin.ProductManage.ProductManagemnet" Theme="logisitc" %>

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
            <tbody>
                <tr class="AlternatingRow">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_HSCode" runat="server" Text="HS编码:" style="text-transform:uppercase"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_HSCode" runat="server" TabIndex="1" 
                            CssClass="TextBox" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_HSName" runat="server" Text="商品名称:" style="text-transform:uppercase"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_HSName" runat="server" CssClass="TextBox" TabIndex="2" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Button ID="btn_Demand" runat="server" Text="查 询" CssClass="InputBtn" 
                            onclick="btn_Demand_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="height: 350px;overflow-x:auto;overflow-y:auto;">
        <asp:GridView ID="gv_HS" runat="server" AutoGenerateColumns="False" PageSize="36" 
                onrowcommand="gv_HS_RowCommand" onrowdeleting="gv_HS_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="行号">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nuber" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                <asp:TemplateField HeaderText="HS编号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Code" runat="server" Text='<%# Eval("HSCode") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品名称">
                    <ItemTemplate>
                        <asp:Label ID="lbl_HSName" runat="server" Text='<%# Eval("HSName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="1200px" />
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="优惠税率">
                    <ItemTemplate>
                        <asp:Label ID="lbl_DiscountTax" runat="server" Text='<%# Eval("DiscountTax") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="普通税率">
                    <ItemTemplate>
                        <asp:Label ID="lbl_GeneralTax" runat="server" Text='<%# Eval("GeneralTax") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="出口税率">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ExportTax" runat="server" Text='<%# Eval("ExportTax") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="消费税率">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ConsumeTax" runat="server" Text='<%# Eval("ConsumeTax") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="增值税率">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RiseTax" runat="server" Text='<%# Eval("RiseTax") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Eidt" CommandName="Eidt" runat="server" Text="详细" PostBackUrl='<%# "ProductDetails.aspx?HSCode="+Eval("HSCode") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Updata" CommandName="Updata" runat="server" Text="修改" PostBackUrl='<%# "ProductModify.aspx?HSCode="+Eval("HSCode") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Delete" CommandName="Del" runat="server" Text="删除" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" OnClientClick="javascript:return confirm('确定删除该条运单吗?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
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
