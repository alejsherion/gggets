<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateParamManage.aspx.cs" Inherits="GGGETSAdmin.PrintManage.TemplateParamManage" Theme="logisitc"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模板参数管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <fieldset>
        <legend>模板参数维护</legend>
        <asp:GridView ID="gbTemplateParams" runat="server" AutoGenerateColumns="False" 
                AllowPaging="True" 
                onpageindexchanging="gbTemplateParams_PageIndexChanging" 
                onrowcommand="gbTemplateParams_RowCommand" 
                   onrowdatabound="gbTemplateParams_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="标签">
                    <ItemTemplate>
                        <asp:Label ID="lblTag" runat="server" Text='<%# Eval("Tag") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="参数名">
                    <ItemTemplate>
                        <asp:Label ID="lblKey" runat="server" Text='<%# Eval("Key") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="参数类型">
                    <ItemTemplate>
                        <asp:Label ID="lblParamType" runat="server" Text='<%# ReturnParamType(Convert.ToString(Eval("ParamType"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操 作">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDelete" runat="server" CommandName="DeleteNew" CommandArgument='<%# Eval("Tag") %>'>删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings FirstPageText="首页" LastPageText="末页" Mode="NextPrevious" 
                NextPageText="下一页" PreviousPageText="上一页" />
            <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
        <br />
               <asp:Button ID="btnReturn" runat="server" Text="返回上一目录" 
                   onclick="btnReturn_Click" CssClass="InputBtn" />
        </fieldset>
        <br />
        <fieldset>
        <legend>参数新增</legend>
            <table style="width: 100%;">
                <tr>
                    <td width="15%">对应表：</td>
                    <td><asp:DropDownList ID="ddlTable" runat="server" Width="150px" 
                            AutoPostBack="True" onselectedindexchanged="ddlTable_SelectedIndexChanged" /></td>
                </tr>
                <tr>
                    <td>对应列：</td>
                    <td><asp:DropDownList ID="ddlColume" runat="server" Width="150px" 
                            AutoPostBack="True" onselectedindexchanged="ddlColume_SelectedIndexChanged" /></td>
                </tr>
                <tr>
                    <td>参数名称：</td>
                    <td><asp:TextBox ID="txtKey" runat="server" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtKey" ErrorMessage="参数名称不能为空!" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>参数类型：</td>
                    <td>
                        <asp:DropDownList ID="ddlParamType" runat="server" Width="150px" AutoPostBack="true" 
                            onselectedindexchanged="ddlParamType_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="Text">普通文本</asp:ListItem>
                            <asp:ListItem Value="Textarea">长文本</asp:ListItem>
                            <asp:ListItem Value="BarCode">条形码</asp:ListItem>
                            <asp:ListItem Value="RadioBox">单选按钮</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trStatistic" runat="server" visible="false">
                    <td>统计(可选)：</td>
                    <td>
                        <asp:RadioButtonList ID="rblStatistic" runat="server" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="sum">总和</asp:ListItem>
                            <asp:ListItem Value="avg">平均数</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAdd" runat="server" Text="新 增" onclick="btnAdd_Click" CssClass="InputBtn" /></td>
                </tr>
            </table>
        </fieldset>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlTable" EventName="SelectedIndexChanged" />
			<asp:AsyncPostBackTrigger ControlID="ddlColume" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlParamType" EventName="SelectedIndexChanged" />
        </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
