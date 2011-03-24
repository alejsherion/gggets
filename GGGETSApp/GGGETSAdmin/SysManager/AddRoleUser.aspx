<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRoleUser.aspx.cs" Inherits="GGGETSAdmin.SysManager.AddRoleUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加角色用户</title>
    <link href="../Styles/sub.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/xtab.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function closeList() {

            var background = parent.document.getElementById("sg");
            var password = parent.document.getElementById("branch_select");
            background.style.display = "none";
            password.style.display = "none";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <div align="center">
        <table class="DataView" width="98%">
            <thead>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="Country" runat="server" Text="邮件："></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="txtEmail" runat="server" Width="120" style="text-transform:uppercase"></asp:TextBox>
                        <!-- 过滤，只能输入2个字母，其他特殊符号，中文，数字都过滤掉-->
                        <asp:RegularExpressionValidator ID="valeEmail" runat="server" 
                            ErrorMessage="邮件格式不对" ControlToValidate="txtEmail" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>  
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Region" runat="server" Text="联系电话："></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="txtTel" runat="server" Width="150" style="text-transform:uppercase"></asp:TextBox>
                        <!-- 过滤，只能输入3个字母，其他特殊符号，中文，数字都过滤掉-->
                       <asp:RegularExpressionValidator ID="valeTel" runat="server" 
                            ErrorMessage="电话格式不对" ControlToValidate="txtTel" 
                            ValidationExpression="((\d{11})|^(\d{2,4}[-_－—]?)?\d{3,8}([-_－—]?\d{3,8})?([-_－—]?\d{1,7})?$)|(^0?1[35]\d{9}$)"></asp:RegularExpressionValidator>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Account" runat="server" Text="登录名：" Width="100"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLoginName" runat="server" Width="120" TabIndex="2" 
                         style="text-transform:uppercase"  MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                         <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间："></asp:Label>           
                    </td>
                    <td align="left" colspan="5">
                        <asp:TextBox ID="txtStartCreateTime" runat="server" Width="100" onClick="WdatePicker()" style="text-transform:uppercase"></asp:TextBox>-
                        <asp:TextBox ID="txtEndCreateTime" runat="server" Width="100" onClick="WdatePicker()" style="text-transform:uppercase"></asp:TextBox>
                       </td>
                </tr>
                <tr align="center">
                    <td align="center" colspan="6">
                       <asp:Button ID="btnQuery" runat="server" Text="查 询 用 户" CssClass="bluebuttoncss" 
                            Width="150px" onclick="btnQuery_Click"   />&nbsp;
                            
                    </td>
                </tr>           
            </thead>
        </table>
    </div>
      <div align="center" >
         <table border="0" cellpadding="0" cellspacing="0" width="98%">
         <thead>
         <tr>
         <th>已选择用户</th>
         <th>用户列表</th>
         </tr>
         </thead>
         <tr valign="top">
         <td>
         <telerik:RadListView ID="lvwUser" runat="server" 
                 ItemPlaceholderID="ListViewContainer" 
                 DataKeyNames="UID" 
                 onitemcommand="lvwUser_ItemCommand">
          <ItemTemplate>
            <asp:LinkButton ID="LinkButton1" CssClass="selectedButtons" runat="server" CommandName="Select" CommandArgument='<%# Eval("UID") %>'>
           <fieldset style="float: left; width: 350px; height: 150px;">
                    <legend><b>真实姓名</b>:
                        <asp:Label ID="lblRealName" runat="server" Text='<%# Eval("RealName") %>'></asp:Label></legend>
                    <div class="details">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr align="left">
                        <td> <label>
                                        登录名:</label>
                                    <%#Eval("LoginName")%></td>
                        </tr>
                        <tr align="left">
                        <td> <label>
                                        联系电话:</label>
                                    <%#Eval("Phone")%></td>
                        </tr>
                        <tr align="left">
                        <td> <label>
                                        电子邮件:</label>
                                    <%#Eval("Email")%></td>
                        </tr>
                        </table>
                    </div>
                </fieldset>
                </asp:LinkButton>

          </ItemTemplate>
          <LayoutTemplate>
           <table>
           <tr>
           <td>
           <asp:PlaceHolder ID="ListViewContainer" runat="server"></asp:PlaceHolder>
           </td>
           </tr>
           <tr>
           <td>
           </td>
           </tr>
           </table>
          </LayoutTemplate>
           <EmptyDataTemplate>
                    <fieldset>
                        <legend>选择用户信息</legend>无
                    </fieldset>
            </EmptyDataTemplate>

          </telerik:RadListView> 
         </td>
         <td width="50%">
              <telerik:RadListView ID="lvwRoleUser" runat="server" 
                 ItemPlaceholderID="ListViewContainer1" 
                 DataKeyNames="UID" onitemcommand="lvwRoleUser_ItemCommand" 
                >
          <ItemTemplate>
            <asp:LinkButton ID="LinkButton1" CssClass="selectedButtons" runat="server" CommandName="Select" CommandArgument='<%# Eval("UID") %>'>
           <fieldset style="float: left; width: 350px; height: 150px;">
                    <legend><b>真实姓名</b>:
                        <asp:Label ID="lblRealName" runat="server" Text='<%# Eval("RealName") %>'></asp:Label></legend>
                    <div class="details">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr align="left">
                        <td> <label>
                                        登录名:</label>
                                    <%#Eval("LoginName")%></td>
                        </tr>
                        <tr align="left">
                        <td> <label>
                                        联系电话:</label>
                                    <%#Eval("Phone")%></td>
                        </tr>
                        <tr align="left">
                        <td> <label>
                                        电子邮件:</label>
                                    <%#Eval("Email")%></td>
                        </tr>
                        </table>
                    </div>
                </fieldset>
                </asp:LinkButton>

          </ItemTemplate>
          <LayoutTemplate>
             <table>
           <tr>
           <td>
           <asp:PlaceHolder ID="ListViewContainer1" runat="server"></asp:PlaceHolder>
           </td>
           </tr>
           <tr>
           <td>
           <telerik:RadDataPager ID="RadDataPager1" runat="server" Style="border: none;">
                            <Fields>
                                <telerik:RadDataPagerButtonField FieldType="FirstPrev" />
                                <telerik:RadDataPagerButtonField FieldType="Numeric" />
                                <telerik:RadDataPagerButtonField FieldType="NextLast" />
                                <telerik:RadDataPagerGoToPageField />
                            </Fields>
                        </telerik:RadDataPager>
           </td>
           </tr>
           </table>
          </LayoutTemplate>
           <EmptyDataTemplate>
                    <fieldset>
                        <legend>选择用户信息</legend>无
                    </fieldset>
            </EmptyDataTemplate>

          </telerik:RadListView> 
         </td>
         </tr>
         <tr>
         <td colspan="2">
              <asp:Button ID="btnsave" runat="server" Text="确   认" CssClass="bluebuttoncss" 
                            Width="150px" onclick="btnsave_Click"    />&nbsp;
                            <input type="button" value="关闭"  class="bluebuttoncss"  onclick="closeList()" style="width:150px"/>
                            </td>
         </tr>
         </table>
    </div>
    </form>
</body>
</html>
