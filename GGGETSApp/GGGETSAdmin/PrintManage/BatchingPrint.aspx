<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchingPrint.aspx.cs" Inherits="GGGETSAdmin.PrintManage.BatchingPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="demo" Namespace="DanLudwig.Controls.Web"
	Assembly="DanLudwig.Controls.AspAjax.ListBox" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批处理套打</title>
    <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
		.clearfix:after
		{
			content: ".";
			display: block;
			height: 0;
			clear: both;
			visibility: hidden;
		}

		/* all borwsers see this, IE-mac included */
		.clearfix
		{
			display: inline-block;
		}

		/* Hide this style from IE-mac \*/
		* html .clearfix
		{
			height: 1%; /* apply to IE-win*/
		}
		/* End hide from IE-mac */
		
		body
		{
			font-family: Arial;
		}
		a:link, a:visited
		{
			color: Blue;
			text-decoration: underline;
		}
		a:hover
		{
			text-decoration: none;
		}
	</style>
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript" src="../Scripts/LodopFuncs.js"></script>
    <object id="LODOP" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width=0 height=0> 
	    <embed id="LODOP_EM" TYPE="application/x-print-lodop" width=0 height=0 PLUGINSPAGE="install_lodop.exe"></embed>
    </object>
    <script language="javascript">
        var LODOP; //声明为全局变量 
        function CreatePage(direction, width, height, pageName) {
            LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
            LODOP.PRINT_INIT("批量连打");
            LODOP.SET_PRINT_PAGESIZE(direction, width, height, pageName);
        };

        function DisplayDesign() {
            LODOP.SET_PREVIEW_WINDOW(0, 1, 1, 800, 600, "打印预览.开始打印");
            LODOP.PREVIEW();
        };

        function closeList() {
            var background = parent.document.getElementById("sg");
            var supplierPicIframe = parent.document.getElementById("branch_select");
            background.style.display = "none";
            supplierPicIframe.style.display = "none";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <div class="clearfix" style="padding: 10px; border: solid 1px #000000; width: 575px; background-color: #FBEDBB; margin-bottom: 1em;">
    <div style="text-align: right">
        <asp:LinkButton ID="lbReturn" runat="server" OnClientClick="closeList()">返回主界面</asp:LinkButton></div>
    <h2 style="margin: 0 0 1em;">运单批量套打</h2>
    <div>
    <fieldset>
    <legend>
    <asp:Label ID="lblHAWBQuery" runat="server" Text="运单查询:"/>
    </legend>
    <asp:Label ID="lblHAWBBarcode" runat="server" Text="运单号:"/>
    <asp:TextBox ID="txtHAWBBarcode" runat="server"></asp:TextBox>
    <asp:Label ID="lblCreateDate" runat="server" Text="创建时间：" />
    <asp:TextBox ID="txtBeginDate" runat="server" Width="100" onClick="WdatePicker()" style="text-transform:uppercase"></asp:TextBox>-
    <asp:TextBox ID="txtEndDate" runat="server" Width="100" onClick="WdatePicker()" style="text-transform:uppercase"></asp:TextBox>
    <asp:Button ID="btnQuery" CssClass="bluebuttoncss" runat="server" Text="查询" 
            onclick="btnQuery_Click" />
    </fieldset>
	</div>
    <div class="clearfix" style="margin-bottom: 1em;">
		<asp:Panel ID="Panel1" runat="server" DefaultButton="LinkButton1"
			Style="float: left">
			<asp:UpdatePanel ID="UpdatePanel1" runat="server">
				<ContentTemplate>
					<p style="margin: 0; padding: 0;">
						可选运单</p>
					<demo:ListBox ID="lbLeft" runat="server" Rows="9"
						SelectionMode="Multiple" HorizontalScrollEnabled="true"
						Style="width: 200px; border: solid 1px #000000;"
						ScrollStateEnabled="true">
					</demo:ListBox>
				</ContentTemplate>
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
					<asp:AsyncPostBackTrigger ControlID="LinkButton2" EventName="Click" />
					<asp:AsyncPostBackTrigger ControlID="LinkButton3" EventName="Click" />
					<asp:AsyncPostBackTrigger ControlID="LinkButton4" EventName="Click" />
				</Triggers>
			</asp:UpdatePanel>
		</asp:Panel>
		<div style="float: left; margin: 0.5em; width: 138px;
			font-size: 0.9em; text-align: center;">
			<p>
				<asp:LinkButton ID="LinkButton1" runat="server" Text="添加 >"
					OnClick="LinkButton1_Click" /></p>
                    <br />
			<p>
				<asp:LinkButton ID="LinkButton2" runat="server" Text="< 移除"
					OnClick="LinkButton2_Click" /></p>
                    <br />
			<p>
				<asp:LinkButton ID="LinkButton3" runat="server" Text="添加所有 >>"
					OnClick="LinkButton3_Click" /></p>
                    <br />
			<p>
				<asp:LinkButton ID="LinkButton4" runat="server" Text="<< 移除所有"
					OnClick="LinkButton4_Click" /></p>
                    <br />
		</div>
		<asp:Panel ID="Panel2" runat="server" DefaultButton="LinkButton2"
			Style="float: left">
			<asp:UpdatePanel ID="UpdatePanel2" runat="server">
				<ContentTemplate>
					<p style="margin: 0; padding: 0;">
						已选运单</p>
					<demo:ListBox ID="lbRight" runat="server" Rows="9"
						SelectionMode="Multiple" HorizontalScrollEnabled="true"
						Width="200px" BorderStyle="Solid" BorderWidth="1px"
						BorderColor="#000000" ScrollStateEnabled="true">
					</demo:ListBox>
				</ContentTemplate>
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
					<asp:AsyncPostBackTrigger ControlID="LinkButton2" EventName="Click" />
					<asp:AsyncPostBackTrigger ControlID="LinkButton3" EventName="Click" />
					<asp:AsyncPostBackTrigger ControlID="LinkButton4" EventName="Click" />
				</Triggers>
			</asp:UpdatePanel>
		</asp:Panel>
    </div>
    <p align="center">
        <asp:Button ID="btnBatchPrint" CssClass="bluebuttoncss" runat="server" 
            Text="批量套打" onclick="btnBatchPrint_Click" />
    </p>
    </div>
    </form>
</body>
</html>
