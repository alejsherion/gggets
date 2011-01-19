<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="HAWBManagement.aspx.cs" Inherits="GGGETSAdmin.HAWB1.HAWBdemand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
<asp:Content ID="Content4" runat="server" 
    contentplaceholderid="CPH_UserControl">
            <div style="width:64%; float:left">
                <div>
                   <asp:Label ID="Lbl_BarCode" runat="server" Text="条形码:"></asp:Label>
                   <asp:TextBox ID="Txt_BarCode" runat="server"></asp:TextBox>
                   <asp:Button ID="But_Demand" runat="server" Text="查 询" 
                        onclick="But_Demand_Click" />
                </div>
               <div>
                    <asp:GridView ID="GV_Bdemand" runat="server" AutoGenerateColumns="False" 
                        onrowcommand="GV_Bdemand_RowCommand" DataKeyNames="BarCode">
                        <Columns>
                            <asp:TemplateField HeaderText="条形码">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BarCode" runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="承运公司">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Carrier" runat="server" Text='<%# Eval("Carrier") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="发件联系人">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ShipperContactor" runat="server" Text='<%# Eval("ShipperContactor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="收件联系人">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ConsigneeContactor" runat="server" Text='<%# Eval("ConsigneeContactor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="运单截止日期">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_DeadlineTime" runat="server" Text='<%# Eval("DeadlienTime") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" HeaderText="查看详细信息">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" Text="选择"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Content>

