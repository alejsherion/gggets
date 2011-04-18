<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClearanceManage.aspx.cs" Inherits="GGGETSAdmin.CustomsClearance.ClearanceManage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>报关人员选择航班主界面</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    <fieldset>
    <legend>航班查询</legend>
        <table style="width: 100%;">
            <tr>
                <td width="16%" align="center"><asp:Label ID="lblFlightNo" runat="server" Text="航班号:" /></td>
                <td width="50%">
                    <asp:TextBox ID="txtFlightNo" runat="server" Width="300px"></asp:TextBox>
                    <!-- 过滤，只能输入2个字母，其他特殊符号，中文，数字都过滤掉-->
                    <asp:FilteredTextBoxExtender ID="ftbeCountry" runat="server" TargetControlID="txtFlightNo" FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" " />
                </td>
                <td align="center">
                    <asp:Button ID="btnQuery" runat="server" Text="查 询" onclick="btnQuery_Click" />
                </td>
            </tr>
            </table>
    </fieldset>
    </div>
    <div align="center">
    <!--GRID-->
        <telerik:RadGrid ID="RGMAWB" runat="server" AutoGenerateColumns="False" 
            GridLines="None">
        <MasterTableView NoMasterRecordsText="无记录">
        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>

        <RowIndicatorColumn>
        <HeaderStyle Width="20px"></HeaderStyle>
        </RowIndicatorColumn>

        <ExpandCollapseColumn>
        <HeaderStyle Width="20px"></HeaderStyle>
        </ExpandCollapseColumn>
            <Columns>
                <telerik:GridTemplateColumn HeaderText="航班号">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("FlightNo") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="总运单号">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="状态">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# GetStatusStr(Convert.ToString(Eval("Status"))) %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="column" HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbExport" runat="server" onclick="lbExport_Click">导出报关文件</asp:LinkButton>
                        &nbsp;|&nbsp;
                        <asp:LinkButton ID="lbImport" runat="server" PostBackUrl='<%# "~/CustomsClearance/ClearanceImport.aspx?FlightNo="+Eval("FlightNo")+"&MAWBCode"+Eval("BarCode") %>'>导入报关文件</asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        </telerik:RadGrid>
    </div>
    </form>
</body>
</html>
