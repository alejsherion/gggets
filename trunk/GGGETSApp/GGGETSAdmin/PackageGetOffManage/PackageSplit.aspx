<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PackageSplit.aspx.cs" Inherits="GGGETSAdmin.PackageGetOffManage.PackageSplit" Theme="logisitc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>包裹拆分</title>
    <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .a1
        {
            list-style: none;
            padding-top:10px;
            padding-bottom:10px;
            margin-left:10px;
        }
     </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <table style="width: 100%;" class="DataView">
            <tr class="Row" height="40px">
                <td width="16%" class="FieldHeader"><asp:Label ID="lblPackageCode" runat="server" Text="包裹编号:"></asp:Label></td>
                <td width="50%">
                    <asp:TextBox ID="txtPackageCode" runat="server" Width="300px"></asp:TextBox>
                </td>
                <td>
                    <asp:LinkButton ID="lbSearch" runat="server" onclick="lbSearch_Click" CssClass="InputBtn">查询并锁定</asp:LinkButton></td>
            </tr>
            <tr class="Row" height="40px">
                <td class="FieldHeader"><asp:Label ID="lblHAWBCode" runat="server" Text="运单编号:"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtScanner" runat="server" Width="300px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnCertain" runat="server" Text="确 认" 
                        onclick="btnCertain_Click" Visible="false" CssClass="InputBtn" /></td>
            </tr>
            <tr>
                <td colspan="3">
                <asp:Panel runat="server" ID="Filepanel" BorderColor="Red" BorderWidth="1" BackColor="#FFFFB9"
                    Width="100%">
                    <ul class="a1">
                        <li>状态说明: <img src="../Images/c01.gif" /> - 通过 <img src="../Images/c02.gif" /> - 不通过</li>
                    </ul>
                 </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <!--GRID-->
                    <telerik:RadGrid ID="RGHAWBs" runat="server" AutoGenerateColumns="False" 
                        GridLines="None">
                    <MasterTableView NoMasterRecordsText="无记录" DataKeyNames="BarCode">
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>

                    <RowIndicatorColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>

                    <ExpandCollapseColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="运单编号">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="状态">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="报关状态">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("CustomsClearanceState") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="right">
                    <asp:LinkButton ID="lbComplete" runat="server" onclick="lbComplete_Click" CssClass="InputBtn">拆包完成</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
