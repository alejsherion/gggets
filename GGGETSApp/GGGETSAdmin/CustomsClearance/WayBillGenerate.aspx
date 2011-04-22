<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WayBillGenerate.aspx.cs" Inherits="GGGETSAdmin.CustomsClearance.WayBillGenerate" Theme="logisitc" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WayBill Generate</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%;" class="DataView">
        <tr class="Row" height="40px">
            <td width="15%" class="FieldHeader">&nbsp;<asp:Label ID="lblWayBill" runat="server" Text="路单编号:"></asp:Label></td>
            <td width="50%"><asp:TextBox ID="txtWayBill" runat="server" Width="300px" 
                    AutoPostBack="True" ontextchanged="txtWayBill_TextChanged"></asp:TextBox></td>
            <td align="center">
                
            </td>
        </tr>
        <tr class="Row" height="40px">
            <td class="FieldHeader">&nbsp;<asp:Label ID="lblBarCode" runat="server" Text="运单编号:"></asp:Label></td>
            <td><asp:TextBox ID="txtBarCode" runat="server" Width="300px" /></td>
            <td>
                <asp:Button ID="btnCertain" runat="server" Text="确 认" 
                    onclick="btnCertain_Click" CssClass="InputBtn" />
            </td>
        </tr>
    </table>
    
    <div>
        <!--ReportViewer-->
        <rsweb:ReportViewer ID="RVWayBills" runat="server" Width="100%" Height="100%">
        </rsweb:ReportViewer>
    </div>
    <div align="center" style="padding-top: 20px">
        <asp:LinkButton ID="lbSave" runat="server" CssClass="InputBtn" 
            onclick="lbSave_Click">保 存</asp:LinkButton>
    </div>
    </form>
</body>
</html>
