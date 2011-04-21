<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClearanceImport.aspx.cs" Inherits="GGGETSAdmin.CustomsClearance.ClearanceImport" %>

<%@ Register src="../SWFUpload/UC_SWFUpload.ascx" tagname="UC_SWFUpload" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入报关文件</title>
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.cookie.min.js" type="text/javascript"></script>
    <script>
        //判断状态是否存在于cookie中
        function IsExistCookieStatus() {
            //debugger;
            if (getCookie("uploadStatus") == '200') {
                //删除cookie
                alert("报关文件已经上传到服务器，请点击获取数据！");
                //GGGETSAdmin.CustomsClearance.ClearanceImport.DeleteCookie();
                $.cookie("uploadStatus", null, { path: '/' });
                document.getElementById("divBtn").style.display = "block";
                document.getElementById("divSwf").style.display = "none";
                clearInterval(id);
            }
        }

        //得到cookie
        function getCookie(objName) {//获取指定名称的cookie的值
            var arrStr = document.cookie.split("; ");
            for (var i = 0; i < arrStr.length; i++) {
                var temp = arrStr[i].split("=");
                if (temp[0] == objName) return unescape(temp[1]);
            }
        }
        var id = setInterval(IsExistCookieStatus, 200);
    </script>
    <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../SWFUpload/css/swfupload.css" rel="stylesheet" type="text/css" />
    <script src="../SWFUpload/js/swfupload.js" type="text/javascript"></script>
    <script src="../SWFUpload/js/handlers.js" type="text/javascript"></script>
    <script src="../SWFUpload/js/json2.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="table3">
  <tr>
    <td class="title">导入报关文件</td>
  </tr>
  <tr>
    <td colspan="4" bgcolor="#FFFFFF" class="STYLE1">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="margin-top:10px;">
      <tr>
        <td class="lf">Excel文件上传：</td>
           <td nowrap="nowrap">
               <div id="divSwf">
                    <uc1:uc_swfupload ID="UC_SWFUpload1" runat="server" />
               </div>
               <div id="divBtn" style="display:none;">
                    <asp:Button ID="btn" runat="server" Text="获取上传文件数据" onclick="btn_Click" />
               </div>
           </td>
      </tr>
      <tr>
        <td class="lf">航班编号：</td>
           <td nowrap="nowrap">
              <asp:Label ID="lblFlightNo" runat="server" Text=""></asp:Label></td>
      </tr>
      <tr>
        <td class="lf">总运单编号：</td>
           <td nowrap="nowrap">
              <asp:Label ID="lblMAWBCode" runat="server" Text=""></asp:Label></td>
      </tr>
      <tr>
        <td class="lf">运单数量：</td>
           <td nowrap="nowrap" align="left"><asp:Label ID="lblHAWBNum" runat="server" Text=""></asp:Label></td>
      </tr>
      <tr id="trMessage" runat="server">
        <td nowrap="nowrap" style=" height:30px;" colspan="2">
            <!--GRID-->
            <telerik:RadGrid ID="RGGetData" runat="server">
            </telerik:RadGrid>
        </td>
      </tr>
      <tr>
        <td nowrap="nowrap" style=" width:20%;height:30px;">
          </td>
           <td height="50" nowrap="nowrap">
                <asp:Button ID="btnConfirm" runat="server" Text="返回" class="butt" onclick="btnConfirm_Click" />&nbsp;
                <div id="result" runat="server" style=" color:Red;" />
          </td>
      </tr>
      </table>
    </td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>
