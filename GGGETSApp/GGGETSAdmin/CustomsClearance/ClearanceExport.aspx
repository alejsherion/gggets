<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClearanceExport.aspx.cs" Inherits="GGGETSAdmin.CustomsClearance.ClearanceExport" Theme="logisitc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入报关文件</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <table style="width: 100%;" class="DataView">
            <tr class="Row">
                <td width="16%" class="FieldHeader"><asp:Label ID="lblFlightNo" runat="server" Text="航班号："></asp:Label></td>
                <td>
                    <asp:Label ID="lblFlightNoData" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="Row">
                <td class="FieldHeader"><asp:Label ID="lblPackageNum" runat="server" Text="包数量："></asp:Label></td>
                <td>
                   <asp:Label ID="lblPackageNumData" runat="server" Text=""></asp:Label>&nbsp;(包)
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <!--GRID-->
                    <telerik:RadGrid ID="RGMAWB" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" AllowPaging="True" CssClass="DataView"
                        onpageindexchanged="RGMAWB_PageIndexChanged" PageSize="60">
                    <MasterTableView NoMasterRecordsText="无记录" DataKeyNames="BarCode">
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>

                    <RowIndicatorColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>

                    <ExpandCollapseColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="请选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CBSelect" runat="server" Checked="true" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="运单号">
                                <ItemTemplate>
                                    <asp:Label ID="lbHAWBCode" runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="btnExport" runat="server" Text="导出报关文件" 
                        onclick="btnExport_Click" CssClass="InputBtn" />
                    &nbsp;
                    <asp:Button ID="btnReturn" runat="server" Text="返 回" CssClass="InputBtn" 
                        onclick="btnReturn_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
