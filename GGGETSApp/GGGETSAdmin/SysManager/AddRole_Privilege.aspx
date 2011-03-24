<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRole_Privilege.aspx.cs" Inherits="GGGETSAdmin.SysManager.AddRole_Privilege" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色权限管理</title>
    <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    <telerik:RadTreeView ID="trvwAppModule" runat="server" CheckBoxes="True" Height="280px"
                    TriStateCheckBoxes="true" 
            CheckChildNodes="true"                                                   
            DataFieldID="ModuleID"   DataFieldParentID="ParentId" 
            onnodedatabound="trvwAppModule_NodeDataBound">
                     <DataBindings>
                <telerik:RadTreeNodeBinding TextField="Description" ValueField="ModuleID"/>
                <telerik:RadTreeNodeBinding Depth="0"  TextField="Description" Expanded="true"
                    CssClass="rootNode"/>
            </DataBindings>
        </telerik:RadTreeView>
        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="确 认" />
        <br />
    </div>
    </form>
</body>
</html>
