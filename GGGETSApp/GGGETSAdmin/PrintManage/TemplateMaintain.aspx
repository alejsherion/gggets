<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateMaintain.aspx.cs" Inherits="GGGETSAdmin.PrintManage.TemplateMaintain" Theme="logisitc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>打印模板维护</title>
    <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />
</head>
<body>
 <form id="form1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
 </asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <ContentTemplate>
 <fieldset>
 <legend style="font-size: large; font-weight: bold;">模板管理</legend>
     <asp:GridView ID="gvTemplate" runat="server" AutoGenerateColumns="False" 
         Width="100%" onrowupdating="gvTemplate_RowUpdating" 
         onrowediting="gvTemplate_RowEditing" 
         onrowcancelingedit="gvTemplate_RowCancelingEdit" 
         onrowcommand="gvTemplate_RowCommand" DataKeyNames="TemplateCode" 
         AllowPaging="True" onrowdeleting="gvTemplate_RowDeleting" 
         onrowdatabound="gvTemplate_RowDataBound">
         <Columns>
             <asp:TemplateField HeaderText="模板编号" ItemStyle-Width="10%">
                 <EditItemTemplate>
                     <asp:Label ID="lblTemplateCode" runat="server" Text='<%# Eval("TemplateCode") %>'></asp:Label>
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Label ID="lblTemplateCode" runat="server" Text='<%# Eval("TemplateCode") %>'></asp:Label>
                 </ItemTemplate>

<ItemStyle Width="10%"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="模板名称" ItemStyle-Width="15%">
                 <EditItemTemplate>
                     <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                 </ItemTemplate>

<ItemStyle Width="9%"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="模板描述" ItemStyle-Width="28%">
                 <EditItemTemplate>
                     <asp:TextBox ID="txtDesc" runat="server" Text='<%# Eval("Desc") %>' 
                         TextMode="MultiLine" Width="300px"></asp:TextBox>
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Desc") %>'></asp:Label>
                 </ItemTemplate>

<ItemStyle Width="28%"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="打印方向" ItemStyle-Width="13%">
                 <EditItemTemplate>
                     <asp:DropDownList ID="ddlPrintDirection" runat="server">
                         <asp:ListItem Value="0">由操作人员自行选择</asp:ListItem>
                         <asp:ListItem Value="1">纵向打印,固定纸张</asp:ListItem>
                         <asp:ListItem Value="2">横向打印,固定纸张</asp:ListItem>
                         <asp:ListItem Value="3">纵向,宽度固定,高度自由</asp:ListItem>
                     </asp:DropDownList>
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Label ID="lblPrintDirection" runat="server" Text='<%# PrintDirection(Convert.ToInt32(Eval("PrintDirection"))) %>'></asp:Label>
                 </ItemTemplate>

<ItemStyle Width="13%"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="纸张宽度(mm)">
                 <EditItemTemplate>
                     <asp:TextBox ID="txtWidth" runat="server" Text='<%# Eval("PagerWidth") %>' Width="30px"></asp:TextBox>
                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtWidth">
                     </asp:FilteredTextBoxExtender>
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("PagerWidth") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="纸张高度(mm)">
                 <EditItemTemplate>
                     <asp:TextBox ID="txtHeight" runat="server" Text='<%# Eval("PagerHeight") %>' Width="30px"></asp:TextBox>
                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtHeight">
                     </asp:FilteredTextBoxExtender>
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Label ID="lblHeight" runat="server" Text='<%# Eval("PagerHeight") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="批量套打高度(mm)">
                 <EditItemTemplate>
                     <asp:TextBox ID="txtBatchHeight" runat="server" Text='<%# Eval("BatchHeight") %>' width="30px"></asp:TextBox>
                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtBatchHeight">
                     </asp:FilteredTextBoxExtender>
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Label ID="lblBatchHeight" runat="server" Text='<%# Eval("BatchHeight") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="操作" ItemStyle-Width="12%">
                 <EditItemTemplate>
                     <asp:LinkButton ID="lbModify" runat="server" CommandName="Update">更新</asp:LinkButton>
                     <asp:LinkButton ID="lbCancel" runat="server" CommandName="Cancel">取消</asp:LinkButton>
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Edit" CommandArgument='<%# Eval("PrintDirection") %>'>修改</asp:LinkButton>
                     <asp:LinkButton ID="lbConfig" runat="server" CommandName="ParamConfig" PostBackUrl='<%# "~/PrintManage/TemplateParamManage.aspx?TID="+Eval("TID") %>'>参数配置</asp:LinkButton>
                     <asp:LinkButton ID="lbMaintain" runat="server" PostBackUrl='<%# "~/PrintManage/TemplateLodopManage.aspx?TID="+Eval("TID") %>'>维护模板</asp:LinkButton>
                     <asp:LinkButton ID="lbDelete" runat="server" CommandName="Delete">删除</asp:LinkButton>
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>
     </asp:GridView>
     <div align="right">
         <asp:Button ID="btnAdd" runat="server" CssClass="InputBtn" Text="模板添加" 
             onclick="btnAdd_Click" /></div>
 </fieldset>
 </ContentTemplate>
 </asp:UpdatePanel>
 </form>
</body>
</html>
