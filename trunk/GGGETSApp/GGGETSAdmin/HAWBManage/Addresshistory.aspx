<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Addresshistory.aspx.cs" Inherits="GGGETSAdmin.HAWBManage.Addresshistory" Theme="logisitc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function closeList2() {

            var background = parent.document.getElementById("lishi");
            var supplierPicIframe = parent.document.getElementById("lishi_select");
            background.style.display = "none";
            supplierPicIframe.style.display = "none";
        }
    </script>
    <div style="height: 200px;width: 650px;overflow-x:auto;overflow-y:auto;">

                    <asp:GridView ID="gv_Shipper" runat="server" OnRowCommand="gv_Shipper_RowCommand"
                        AutoGenerateColumns="False" PageSize="500">
                        <Columns>
                            <asp:TemplateField HeaderText="公司名称">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_name" runat="server" Text='<%# Eval("Name") %>' CommandName="Select"
                                        CommandArgument='<%# Eval("AID") %>' OnClientClick="closeList2()"></asp:LinkButton>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="联系电话">
                                <ItemTemplate>                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("Phone") %>'
                                        CommandName="Select" CommandArgument='<%# Eval("AID") %>' OnClientClick="closeList2()"></asp:LinkButton>
                            </ItemTemplate>

                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                
    </div>
    <asp:Button ID="btn_Close" runat="server" CssClass="InputBtn" Text="取 消" OnClientClick="closeList2()" />
    </form>
</body>
</html>
