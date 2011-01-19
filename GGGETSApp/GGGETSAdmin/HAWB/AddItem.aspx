<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AddItem.aspx.cs" Inherits="GGGETSAdmin.HAWB1.AddItem1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
<asp:Content ID="Content4" runat="server" 
    contentplaceholderid="CPH_UserControl">
            <div style="width:64%; float:left">
                <div style="width:64%; float:left">
                <div>
                <div>
                 <table>
                    <tr>
                        <td>物品重量:</td>
                        <td>
                            <asp:TextBox ID="Txt_ItemWeight" runat="server"></asp:TextBox></td>
                        <td>物品长度:</td>
                        <td><asp:TextBox ID="Txt_ItemLength" runat="server"></asp:TextBox></td>                    </tr>
                    <tr>
                        <td>物品宽度:</td>
                        <td><asp:TextBox ID="Txt_ItemWidth" runat="server"></asp:TextBox></td>
                        <td>物品高度:</td>
                        <td><asp:TextBox ID="Txt_ItemHeight" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>运送支付费用:</td>
                        <td><asp:TextBox ID="Txt_ItemTransPays" runat="server"></asp:TextBox></td>
                        <td>运送支付货币:</td>
                        <td><asp:TextBox ID="Txt_ItemTransCurrency" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="But_AddItme" runat="server" Text="提交" 
                                onclick="But_AddItme_Click" />
                        </td>
                    </tr>
                 </table>
                <div id="Div1" runat="server" visible="false">
                    <asp:GridView ID="Gv_BaleXinXi" runat="server" EnableModelValidation="True" 
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="编号">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Order" runat="server" Text='<%# Eval("cTel") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="货物名称">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("cTel") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="重量">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("cTel") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="高度">
                                <EditItemTemplate>
                                    <asp:TextBox ID="Txt_Hight" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Hight" runat="server" Text="1111"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改">
                                <ItemTemplate>
                                    <asp:Button ID="But_Edit" runat="server" Text="修改"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_delete" runat="server" Text="删除"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                    </asp:GridView>
                    <div style="text-align:center"><asp:Button ID="But_Add" runat="server" Text="提交" /></div>
                </div>
           </div>
            </div>
            </div>
            </div>
        </asp:Content>

