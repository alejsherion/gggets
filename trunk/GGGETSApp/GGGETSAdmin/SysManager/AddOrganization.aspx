<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddOrganization.aspx.cs" Inherits="GGGETSAdmin.SysManager.AddOrganization" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>组织架构</title>
     <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
   

</head>
<body>
    <form id="form1" runat="server">
      <script type="text/javascript">
          function nodeClicking(sender, args) {
              var comboBox = $find("<%= dropParentDID.ClientID %>");

              var node = args.get_node()

              comboBox.set_text(node.get_text());
              comboBox.set_value(node.get_value());
              comboBox.trackChanges();
              comboBox.get_items().getItem(0).set_text(node.get_text());
              comboBox.get_items().getItem(0).set_value(node.get_value());
              comboBox.commitChanges();

              comboBox.hideDropDown();
          }

          function StopPropagation(e) {
              if (!e) {
                  e = window.event;
              }

              e.cancelBubble = true;
          }

          function OnClientDropDownOpenedHandler(sender, eventArgs) {
              var tree = sender.get_items().getItem(0).findControl("trvwOrganization");
              var selectedNode = tree.get_selectedNode();
              if (selectedNode) {
                  selectedNode.scrollIntoView();
              }
          }
        </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div align="center">
        <table class="DataView" width="98%">
            <thead>
                <tr class="Row">
                    <td class="FieldHeader" align="right" width="20%">
                       <asp:Label ID="lblIsLeft" runat="server" Text="组织类型："></asp:Label>
                    </td>
                    <td class="FieldHeader" align="left">
                        <asp:RadioButtonList ID="rbtnOrganizationType" runat="server" 
                            RepeatDirection="Horizontal" 
                            AutoPostBack="true" 
                            onselectedindexchanged="rbtnOrganizationType_SelectedIndexChanged">
                        <asp:ListItem Text="顶级组织" Value="0" Selected="True"/>
                         <asp:ListItem Text="子级组织" Value="1"  />
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr class="Row" runat="server" id="trParentDirectory" visible="false">
                    <td id="Td1" class="FieldHeader" align="right" width="20%" runat="server">
                         <asp:Label ID="Label1" runat="server" Text="上级组织："></asp:Label></td>
                    <td id="Td2" class="FieldHeader" align="left" runat="server"> 
                       
                        <telerik:RadComboBox ID="dropParentDID" Runat="server" Height="140px" Width="215px" 
                        ShowToggleImage="True" 
                            Style="vertical-align: middle;"                            OnClientDropDownOpened="OnClientDropDownOpenedHandler"
                EmptyMessage="请选择部门" ExpandAnimation-Type="None" CollapseAnimation-Type="None" 
                           >
                <ItemTemplate>
                <div id="div1">
                      <telerik:RadTreeView ID="trvwOrganization" runat="server" 
                          OnClientNodeClicking="nodeClicking" Height="138px" Width="212px"                                                 
            DataFieldID="DID"   DataFieldParentID="ParentDID" 
                          >
                     <DataBindings>
                <telerik:RadTreeNodeBinding TextField="OrganizationName" ValueField="DID"/>
                <telerik:RadTreeNodeBinding Depth="0"  TextField="OrganizationName" ValueField="DID" Expanded="true"
                    CssClass="rootNode" />
            </DataBindings>
        </telerik:RadTreeView>
                    </div>
                </ItemTemplate>
                 <Items>
                    <telerik:RadComboBoxItem Text="" />
                </Items>
                        </telerik:RadComboBox>
                       
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_OrganizationCode" runat="server" Text="组织编号："></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtOrganizationCode" runat="server" style="text-transform:uppercase" 
                            MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valrLoginName" runat="server" ErrorMessage="必填" ControlToValidate="txtOrganizationCode">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right" width="20%">
                         <asp:Label ID="lblOrganizationName" runat="server" Text="组织名：" style="text-transform:uppercase"></asp:Label></td>
                    <td class="FieldHeader" align="left">
                        <asp:TextBox ID="txtOrganizationName" runat="server" MaxLength="50" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valrDescription" runat="server" ErrorMessage="必填项" ControlToValidate="txtOrganizationName"></asp:RequiredFieldValidator>
                        </td>
                </tr>
               
                <tr class="Row">
                    <td class="FieldHeader" align="right" width="20%">
                         <asp:Label ID="lblRemark" runat="server" Text="备注：" style="text-transform:uppercase"></asp:Label></td>
                    <td class="FieldHeader" align="left">
                        <asp:TextBox ID="txtRemark" runat="server" Rows="5" TextMode="MultiLine" Width="300" MaxLength="200" style="text-transform:uppercase"></asp:TextBox></td>
                </tr>
                <tr align="center">
                    <td align="center" colspan="2">
                        &nbsp;
                            <asp:Button ID="btnAdd" runat="server" Text="设    置" CssClass="bluebuttoncss" 
                            Width="150px" onclick="btnAdd_Click"   />&nbsp;
                            <asp:Button ID="btnComeBack" runat="server" Text="返    回" CssClass="bluebuttoncss" 
                            Width="150px" onclick="btnComeBack_Click"   CausesValidation="false"/>
                        <asp:Label ID="errorDesc" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                </tr>           
            </thead>
        </table>
         <script type="text/javascript">
//             var div1 = document.getElementById("div1");
//             div1.onclick = StopPropagation;
            </script>
    </div>
    
    </form>
</body>
</html>
