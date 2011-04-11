<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Switch.aspx.cs" Inherits="GGGETSAdmin.Switch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="gb2312" content="text/html;" http-equiv="Content-Type"/>
    <script language="javascript">

        function switchSysBar() {
            if (parent.document.getElementById('attachucp').cols == "210,10,*") {
                document.getElementById('leftbar').style.display = "";
                parent.document.getElementById('attachucp').cols = "0,10,*";
            }
            else {
                parent.document.getElementById('attachucp').cols = "210,10,*";
                document.getElementById('leftbar').style.display = "none"
            }
        }

        function load() {
            if (parent.document.getElementById('attachucp').cols == "0,10,*") {
                document.getElementById('leftbar').style.display = "";
            }
        }

    </script>
</head>
<body marginwidth="0" marginheight="0" bgcolor="#f5f4f4" onload="load()" topmargin="0" leftmargin="0">
<center>
<table height="450" cellspacing="0" cellpadding="0" border="0" width="100%">
<tbody>
<tr>
<td id="leftbar" bgcolor="#f5f4f4" style="display: none;" valign="middle">
<a onclick="switchSysBar()" href="javascript:void(0);">
<img height="90" border="0" width="9" alt="展开左侧菜单" src="Images/Out.gif"/>
</a>
</td>
<td id="rightbar" bgcolor="#f5f4f4" valign="middle">
<a onclick="switchSysBar()" href="javascript:void(0);">
<img height="90" border="0" width="9" alt="隐藏左侧菜单" src="Images/In.gif"/>
</a>
</td>
</tr>
</tbody>
</table>
</center>
</body>
</html>
