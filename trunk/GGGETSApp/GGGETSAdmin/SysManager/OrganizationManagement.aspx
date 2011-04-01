<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrganizationManagement.aspx.cs" Inherits="GGGETSAdmin.SysManager.OrganizationManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组织架构管理</title>
    <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
    <div>
        <table class="DataView" width="89%" id="tbCondtion" runat="server">
            <tbody>
                <tr class="AlternatingRow">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_Name" runat="server" Text="组织名称:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrganizationName" CssClass="TextBox" runat="server" Width="120px" 
                            MaxLength="20"></asp:TextBox>
                    </td>
                    
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartCreateTime" runat="server" CssClass="TextBox" TabIndex="2" onClick="WdatePicker()" Width="90"></asp:TextBox>-
                        <asp:TextBox ID="txtEndCreateTime" runat="server" TabIndex="3" 
                            CssClass="TextBox" onClick="WdatePicker()" Width="90"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td class="FieldHeader">
                          <asp:Label ID="lbl_RoleType" runat="server" Text="父级架构:"></asp:Label>
                    </td>
                    <td>
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
                    <td class="FieldHeader">
                      <asp:Label ID="Label1" runat="server" Text="编号:"></asp:Label>
                    </td>
                    <td>
                      
                       <asp:TextBox ID="txtOrganizationCode" runat="server" CssClass="TextBox"  Width="90" MaxLength="50"></asp:TextBox>&nbsp;
                        <asp:Button ID="btnQuery" runat="server" Text="查 询" CssClass="InputBtn" onclick="btnQuery_Click" 
                            />&nbsp;&nbsp;
                            <asp:Button ID="btnAdd" runat="server" Text="添 加" CssClass="InputBtn" 
                             onclick="btnAdd_Click"  /></td>
                </tr>
            </tbody>
        </table>
        <asp:GridView ID="dgrdOrganization" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="DID" onAllowPaging="true" 
            PageSize="20"  Width="98%" onrowcommand="dgrdOrganization_RowCommand" onpageindexchanging="dgrdOrganization_PageIndexChanging" 
            >
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编号">
                    <ItemTemplate>
                        <asp:Label ID="lblOrganizationCode" runat="server" Text='<%# Eval("OrganizationCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("OrganizationName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="父级节点描述">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Description" runat="server" Text='<%#  GetStatusByCode(Convert.ToString(Eval("ParentDID"))) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="120px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="备注" >
                    <ItemTemplate>
                        <asp:Label ID="lbtn_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建时间">
                    <ItemTemplate>
                        <asp:Label ID="lbl_CreateTime" runat="server" Text='<%# Eval("CreateTime") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" CommandName="Update" runat="server" Text="修改" CommandArgument='<%# Eval("DID") %>'></asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="lbtn_Delete" CommandName="Del" runat="server" Text="删除" CommandArgument='<%# Eval("DID") %>' OnClientClick="javascript:return confirm('确定删除该条记录吗?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
