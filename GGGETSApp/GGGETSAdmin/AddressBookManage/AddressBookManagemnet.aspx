<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddressBookManagemnet.aspx.cs" Inherits="GGGETSAdmin.AddressBookManage.AddressBookManagemnet" Theme="logisitc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../Scripts/calendar.js"></script>
    <div>
        <table class="DataView">
            <tbody>
                <tr class="AlternatingRow">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_CompanyCode" runat="server" Text="公司账号:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_CompanyCode" runat="server" CssClass="TextBox" Width="70"></asp:TextBox>-
                        <asp:TextBox ID="Txt_DepCode" runat="server" CssClass="TextBox" Width="50"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_LoginName" runat="server" Text="用户名:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_LoginName" CssClass="TextBox" runat="server"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_GetUpTime" runat="server" CssClass="TextBox" TabIndex="2" onfocusin="calendar()" Width="70"></asp:TextBox>-
                        <asp:TextBox ID="Txt_StopTime" runat="server" TabIndex="3" 
                            CssClass="TextBox" onfocusin="calendar()" Width="70"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Button ID="btn_Demand" runat="server" Text="查 询" CssClass="InputBtn" 
                            onclick="btn_Demand_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="height: 350px;overflow-x:auto;overflow-y:auto;">
        <asp:GridView ID="gv_AddressBook" runat="server" AutoGenerateColumns="false" 
            onrowcommand="gv_AddressBook_RowCommand" PageSize="5000">
            <Columns>
                <asp:TemplateField HeaderText="行号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Number" runat="server" Text='<%# N() %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="地址类型">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Addresstype" runat="server" Text='<%# Eval("AddressType").ToString().Replace("0","发件地址").Replace("1","收件地址").Replace("1","交付地址") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="公司名称">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="地址">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Address" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="联系人">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ContactorName" runat="server" Text='<%# Eval("ContactorName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="联系电话">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Phone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Eidt" CommandName="Eidt" runat="server" Text="详细" CommandArgument='<%# Eval("AID") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Updata" CommandName="Updata" runat="server" Text="修改" CommandArgument='<%# Eval("AID") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Delete" CommandName="Del" runat="server" Text="删除" CommandArgument='<%# Eval("AID") %>' OnClientClick="javascript:return confirm('确定删除该条记录吗?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
    </div>
</asp:Content>
