<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateLodopManage.aspx.cs" Inherits="GGGETSAdmin.PrintManage.TemplateLodopManage" Theme="logisitc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模板打印维护界面</title>
    <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script language="javascript" src="../Scripts/LodopFuncs.js"></script>
    <script language="javascript">
        var LODOP; //声明为全局变量 
        function CreatePage(direction, width, height, pageName) {
            LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
            //设定打印模板大小
            LODOP.PRINT_INITA(0, 0, 1000, 700, "打印模板制作区域");
        };
        function DisplayDesign(operateType) {
            LODOP.SET_SHOW_MODE("DESIGN_IN_BROWSE", 1); //当前模式为内嵌式
            //LODOP.SET_SHOW_MODE("SETUP_ENABLESS", "11111110111111");//控制属性权限代码
            LODOP.PRINT_DESIGN();
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <fieldset>
    <legend style="font-size: large; font-weight: bold;">模板维护</legend>
        <table style="width: 100%;">
            <tr>
                <td align="center">
                    <object id="LODOP" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width=780 height=407> 
                        <param name="Caption" value="打印模板制作区域">
                        <param name="Border" value="1">
                        <param name="Color" value="#C0C0C0">
                        <embed id="LODOP_EM" TYPE="application/x-print-lodop" width=1000 height=700 PLUGINSPAGE="install_lodop.exe">
                    </object>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnMaintain" runat="server" Text="开始维护" 
                        CssClass="InputBtn" onclick="btnMaintain_Click" />&nbsp;
                    <input id="btnSave" type="button" value="保存状态" class="InputBtn" onclick="SaveState()" />
                </td>
            </tr>
        </table>
    </fieldset>
    <script language="javascript">
        function SaveState() {
            if (LODOP == null) {
                alert("您还没有进行维护！");
                return;
            }
            var count = LODOP.GET_VALUE('ItemCount', 0); //获取打印参数个数，用于遍历

            for (var i = 1; i <= count; i++) {
                var ItemTop = LODOP.GET_VALUE('ItemTop', i); //获取对象上边距
                var ItemLeft = LODOP.GET_VALUE('ItemLeft', i); //获取对象左边距
                var ItemWidth = LODOP.GET_VALUE('ItemWidth', i); //获取对象宽度
                var ItemHeight = LODOP.GET_VALUE('ItemHeight', i); //获取对象高度
                var ItemContent = LODOP.GET_VALUE('ItemContent', i); //获取对象内容
                var ItemFontName = LODOP.GET_VALUE('ItemFontName', i); //获取对象字体名称
                var ItemFontSize = LODOP.GET_VALUE('ItemFontSize', i); //获取对象字体大小
                var ItemAlignment = LODOP.GET_VALUE('ItemAlignment', i); //获取对象靠齐方式
                var Itembold = LODOP.GET_VALUE('Itembold', i); //获取对象是否粗体
                var ItemItalic = LODOP.GET_VALUE('ItemItalic', i); //获取对象是否斜体
                var ItemUnderline = LODOP.GET_VALUE('ItemUnderline', i); //获取对象是否下划线
                var ItemClassName = LODOP.GET_VALUE('ItemClassName', i); //获取对象类型
                var requestParam = GetQueryString("TID");//获取传递过来的参数 

                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "TemplateLodopManage.aspx/GetItemClassName",
                    data: "{ItemTop:'" + ItemTop + "',ItemLeft:'" + ItemLeft + "',ItemWidth:'" + ItemWidth + "',ItemHeight:'" + ItemHeight + "',ItemFontName:'" + ItemFontName + "',ItemFontSize:'" + ItemFontSize + "',ItemAlignment:'" + ItemAlignment + "',Itembold:'" + Itembold + "',ItemItalic:'" + ItemItalic + "',ItemUnderline:'" + ItemUnderline + "',Tag:" + i + ",RequestParam:'" + requestParam + "'}",
                    dataType: 'json',
                    async: false,      //ajax同步
                    success: function (result) {
                        temp = result.d;
                    }
                });
            }
            alert("保存成功！");
            location.href("./TemplateMaintain.aspx");
        }

        //获取URL参数
        function GetQueryString(name) {

            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");

            var r = window.location.search.substr(1).match(reg);

            if (r != null) return unescape(r[2]); return null;

        }
    </script>
    </div>
    </form>
</body>
</html>
