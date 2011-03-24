<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRole.aspx.cs" Inherits="GGGETSAdmin.SysManager.AddRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色管理</title>
    <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
div.ps {
	position: absolute;
        display:none;
	z-index: 999;
	background-color: #FFFFFF;
	border: thin solid #CCCCCC;
        left: 60px;
	top: 120px;
        opacity:1;
        filter:alpha(opacity=100);

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
        opacity:0.2;
        filter:alpha(opacity=20);
}

</style>
<script type="text/javascript">
    function changeScope(url) {
        var id = document.getElementById("hfId").value;
        if (id == "") return;
        if (url == "AddRolePrivilege.aspx") {
            document.getElementById("frameWin").height = "400";
            document.getElementById("frameWin").width = "300";
        }
        else {
            document.getElementById("frameWin").width = "800";
            document.getElementById("frameWin").height = "600";
        }
        var width = document.body.clientWidth;
        var height = document.body.clientHeight;

        var background = document.getElementById("sg");
        var password = document.getElementById("branch_select");
        background.style.width = width;
        background.style.height = "900";

        background.style.display = "block";
        password.style.display = "block";
        var webUrl = url + "?Id=" + id;
        document.getElementById("frameWin").src = webUrl;
      
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
      <div>
        <table class="DataView"  width="98%">
            <tbody>
                <tr class="AlternatingRow">
                    <td class="FieldHeader"  align="right">
                        <asp:Label ID="lbl_Name" runat="server" Text="名称:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" CssClass="TextBox" runat="server" Width="120px" 
                            MaxLength="20" Text='<%# Eval("Name") %>'></asp:TextBox><asp:RequiredFieldValidator ID="valrName"
                                runat="server" ErrorMessage="必填" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                    </td>
                 </tr> 
                <tr>
                    <td class="FieldHeader"  align="right">
                        <asp:Label ID="lbl_description" runat="server" Text="描述:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtdescription" CssClass="TextBox" runat="server" 
                            Width="150px" MaxLength="200" Text='<%# Eval("Description") %>'></asp:TextBox>
                       
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Remark" runat="server" Text="备注:" ></asp:Label>
                    </td>
                    <td align="left">
                         <asp:TextBox ID="txtRemark" runat="server" Rows="5" TextMode="MultiLine" Width="300" MaxLength="200" style="text-transform:uppercase" Text='<%# Eval("Remark") %>'></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        状态:</td>
                    <td align="left">
                        <asp:RadioButtonList ID="rbtnStatus" runat="server" 
                            RepeatDirection="Horizontal">
                        </asp:RadioButtonList>
                        &nbsp;</td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="center" colspan="2">
                         <asp:Button ID="btn_login" runat="server" Text="设        置" 
                             CssClass="bluebuttoncss" onclick="btn_login_Click"/> &nbsp;
        <asp:Button ID="btn_cancel" runat="server" Text="返        回" CssClass="bluebuttoncss" 
                             onclick="btn_cancel_Click" CausesValidation="false"/>&nbsp;
                             <input type="button" value="操 作 用 户" onclick="changeScope('AddRoleUser.aspx')" class="bluebuttoncss" runat="server" id="btnUser"/>
        &nbsp;
         <input type="button" value="操 作 权 限" onclick="changeScope('AddRolePrivilege.aspx')" class="bluebuttoncss"  runat="server"  id="btnPrivilege"/>
                        <asp:HiddenField ID="hfId" runat="server" />
        </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div id="sg" class="sg"></div>
     <div id="branch_select" class="ps" >
<iframe frameborder="0"  scrolling="no" width="700" height="800" runat="server" id="frameWin">
</iframe>
</div>
    </form>
</body>
</html>
