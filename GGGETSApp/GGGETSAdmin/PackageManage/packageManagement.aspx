<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="packageManagement.aspx.cs" Inherits="GGGETSAdmin.PackageManage.packageManagement" Theme="logisitc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="FunctionBar">
        <table class="DataView">
            <tbody>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_BagBarCode" runat="server" Text="包号:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_BagBarCode" runat="server" Width="250"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width: 80px">
                        <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                    </td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txt_UpCreateTime" runat="server" Width="75px"></asp:TextBox>-
                        <asp:TextBox ID="txt_ToCreateTime" runat="server" Width="75px"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_Region" runat="server" Text="目的地三字码:"></asp:Label>
                    </td>
                    <td style="width: 80px">
                        <asp:TextBox ID="Txt_Region" runat="server" Width="50"></asp:TextBox>
                    </td>
                    <td style="width: 50px">
                        <asp:Button ID="btn_Demand" runat="server" Text="查 询" CssClass="InputBtn" OnClick="btn_Demand_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:GridView ID="gv_HAWB" runat="server" CssClass="DataView" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="编号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Number" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="运单号">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_BarCoder" runat="server" PostBackUrl='<%# "~/HAWBDetails.aspx?BarCode="+Eval("BarCode") %>'
                            Text='<%# Eval("BarCode") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Width="220px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="重量">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Weight" runat="server" Text='<%# Eval("Weight") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="总运单号">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_SumNumber" PostBackUrl='<%# "~/packageAdd.aspx?BarCode="+Eval("BarCode") %>'
                            runat="server" Text='<%# Eval("BarCode") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Width="220px" />
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
    </div>
    <div class="FooterBtnBar">
        <asp:Button ID="btn_Close" runat="server" CssClass="InputBtn" Text="删 除" 
                        onclick="btn_Close_Click" OnClientClick="return confirm('是否确认删除？');" />
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
