<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HAWBPreview.aspx.cs" Inherits="GGGETSAdmin.PrintManage.HAWBPreview" Theme="logisitc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>运单查询</title>
    <%--<link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />--%>
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>
    <object id="LODOP" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width=0 height=0> 
	    <embed id="LODOP_EM" TYPE="application/x-print-lodop" width=0 height=0 PLUGINSPAGE="install_lodop.exe"></embed>
    </object>
    <script language="javascript">
        var LODOP; //声明为全局变量 
        function CreatePage(direction,width,height,pageName) {
            LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
            LODOP.PRINT_INIT("运单打印");//模拟打印的文件名
            LODOP.SET_PRINT_PAGESIZE(direction, width, height, pageName);
        };

        function DisplayDesign(operateType) {
            LODOP.SET_PREVIEW_WINDOW(0, 1, 1, 800, 600, "打印预览.开始打印"); //‘打印预览’为打印预览窗体的头标题；‘开始打印’为内部控件显示
            if(operateType == 1)
                LODOP.PREVIEW();
            if (operateType == 2)
                LODOP.PRINT();
        };

        function changeScope() {
            var width = document.body.clientWidth; //浏览器相对宽度
            var height = document.body.clientHeight; //浏览器相对高度
            var background = document.getElementById("sg"); //获取第一层玻璃
            var supplierPicIframe = document.getElementById("branch_select"); //获取第二层玻璃
            background.style.width = width;
            background.style.height = "900";
            background.style.display = "block";
            supplierPicIframe.style.display = "block";
            document.getElementById("frame1").src = "./BatchingPrint.aspx";
        }
    </script>
    <style type="text/css">
        div.ps {
         position: absolute;
         display:none;
         z-index: 999;
         background-color: #FFFFFF;
         border: thin solid #CCCCCC;
         left: 300px;
         top: 200px;
         opacity:0.8;
         filter:alpha(opacity=80);
        }
        
        div.sg{
         position: absolute;
         display:none;
         z-index: 200;
         height: auto;
         width: auto;
         background-color: #666666;
         left: 0px;
         top: 0px;
         border-top-style: none;
         border-right-style: none;
         border-bottom-style: none;
         border-left-style: none;
         opacity:0.8;
         filter:alpha(opacity=80);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <div class="nav">运单查询</div>
    <div align="center">
        <table class="DataView" width="98%">
            <thead>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_BarCode" runat="server" Text="运单号："></asp:Label>
                    </td>
                    <td colspan="5" align="left">
                        <asp:TextBox ID="Txt_BarCode" runat="server" Width="600" TabIndex="1" style="text-transform:uppercase"></asp:TextBox>
                    </td>                    
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="Country" runat="server" Text="国家名称："></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="Txt_Country" runat="server" Width="150" style="text-transform:uppercase"></asp:TextBox>
                        <!-- 过滤，只能输入2个字母，其他特殊符号，中文，数字都过滤掉-->
                        <asp:FilteredTextBoxExtender ID="ftbeCountry" runat="server" TargetControlID="Txt_Country" FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" " />
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Region" runat="server" Text="地区名称："></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="Txt_Region" runat="server" Width="150" style="text-transform:uppercase"></asp:TextBox>
                        <!-- 过滤，只能输入3个字母，其他特殊符号，中文，数字都过滤掉-->
                        <asp:FilteredTextBoxExtender ID="ftbeRegion" runat="server" TargetControlID="Txt_Region" FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" " />
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Account" runat="server" Text="客户帐号(个人/部门)：" Width="120"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_UserCode" runat="server" Width="200" TabIndex="2" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_CorporationName" runat="server" Text="承运公司："></asp:Label>
                    </td>
                    <td colspan="5" align="left">
                        <asp:TextBox ID="Txt_corporationName" runat="server" Width="500" style="text-transform:uppercase"></asp:TextBox>
                    </td> 
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Contactor" runat="server" Text="个人/公司名称："></asp:Label>                        
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="txt_RealName" runat="server" Width="80" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_HAWBType" runat="server" Text="运单类型"></asp:Label>                       
                    </td>
                    <td align="left" style="width:80px">
                        <asp:DropDownList ID="ddl_HAWBType" runat="server" Width="150px">
                            <asp:ListItem Value="-1" Text="请选择" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="国外"></asp:ListItem>
                            <asp:ListItem Value="1" Text="国内"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间："></asp:Label>                        
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_GetUpTime" runat="server" Width="100" onClick="WdatePicker()" style="text-transform:uppercase"></asp:TextBox>-
                        <asp:TextBox ID="Txt_StopTime" runat="server" Width="100" onClick="WdatePicker()" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td align="right" class="FieldHeader">
                        <asp:Label ID="lbl_SettleType" runat="server" Text="结算方式：" Width="80"></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:DropDownList ID="DDl_SettleType" runat="server">
                            <asp:ListItem Value="-1" Text="请选择" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="预付月结"></asp:ListItem>
                            <asp:ListItem Value="1" Text="预付现结"></asp:ListItem>
                            <asp:ListItem Value="2" Text="到付月结"></asp:ListItem>
                            <asp:ListItem Value="3" Text="到付现结"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_BoxType" runat="server" Text="包裹类型：" Width="80"></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:DropDownList ID="ddl_BoxType" runat="server">
                            <asp:ListItem Value="-1" Text="请选择" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="文件"></asp:ListItem>
                            <asp:ListItem Value="1" Text="小包裹"></asp:ListItem>
                            <asp:ListItem Value="2" Text="普货"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="FieldHeader" align="right">&nbsp;</td>
                    <td align="left">&nbsp;</td>
                </tr>       
                <tr align="center">
                    <td align="center" colspan="6">
                       <asp:Button ID="btn_Demand" runat="server" Text="查 询" CssClass="InputBtn"
                            Width="150px" onclick="btn_Demand_Click" />
                       <input type="button" value="批 量 套 打" class="InputBtn" onclick="changeScope()" />
                    </td>
                </tr>           
            </thead>
        </table>
    </div>
    <div align="center">
        <asp:GridView ID="GVHAWBs" runat="server" AutoGenerateColumns="False" 
            Width="98%" onrowcommand="GVHAWBs_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="运单号">
                    <ItemTemplate>
                        <asp:Label ID="lblHAWBCode" runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="国家">
                    <ItemTemplate>
                        <asp:Label ID="lblCountry" runat="server" Text='<%# GetCountryNameByCode(Convert.ToString(Eval("ConsigneeCountry"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="地区">
                    <ItemTemplate>
                        <asp:Label ID="lblRegion" runat="server" Text='<%# GetRegionNameByCode(Convert.ToString(Eval("ConsigneeRegion"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="承运公司">
                    <ItemTemplate>
                        <asp:Label ID="lblSenderCompany" runat="server" Text='<%# Eval("Carrier") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="运单类型">
                    <ItemTemplate>
                        <asp:Label ID="lblHAWBType" runat="server" Text='<%# GetHAWBType(Convert.ToBoolean(Eval("IsInternational"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="结算方式">
                    <ItemTemplate>
                        <asp:Label ID="lblSettleType" runat="server" Text='<%# GetServiceType(Convert.ToString("SettleType")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="包裹类型">
                    <ItemTemplate>
                        <asp:Label ID="lblPackageType" runat="server" Text='<%# GetPackageType(Convert.ToString("ServiceType")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建日期">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# Eval("CreateTime") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:Button ID="btnPrint" runat="server" Text="打印预览" CssClass="InputBtn" CommandName="Print" CommandArgument='<%# Eval("HID") %>' />
                        <asp:Button ID="btnDirectPrint" runat="server" Text="直接打印" CssClass="InputBtn" CommandName="DirectPrint" CommandArgument='<%# Eval("HID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    <!--modalPopUp效果-->
        <div id="sg" class="sg"></div>
        <div id="branch_select" class="ps" >
            <iframe frameborder="0" id="frame1" scrolling="no" width="630" height="330">
            </iframe>
        </div>
    </form>
</body>
</html>
