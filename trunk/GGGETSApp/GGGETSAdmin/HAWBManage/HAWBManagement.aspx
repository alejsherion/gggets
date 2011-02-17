<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HAWBManagement.aspx.cs" Inherits="GGGETSAdmin.HAWBManage.HAWBManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../Scripts/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:PlaceHolder ID="phHawb" runat="server">
        <table class="DataView">
            <thead>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_BarCode" runat="server" Text="运单号："></asp:Label>
                    </td>
                    <td colspan="5" align="left">
                        <asp:TextBox ID="Txt_BarCode" runat="server" Width="500" TabIndex="1"></asp:TextBox>
                    </td>                    
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="Country" runat="server" Text="国家二字码："></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="Txt_Country" runat="server" Width="80" MaxLength="2"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Region" runat="server" Text="地区三字码："></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="Txt_Region" runat="server" Width="80" MaxLength="3"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Account" runat="server" Text="客户帐号：" Width="80"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Account1" runat="server" Width="80" TabIndex="2"></asp:TextBox>-
                        <asp:TextBox ID="Txt_Account2" runat="server" Width="50" TabIndex="3"></asp:TextBox><b
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_CorporationName" runat="server" Text="公司名称："></asp:Label>
                    </td>
                    <td colspan="5" align="left">
                        <asp:TextBox ID="Txt_corporationName" runat="server" Width="500"></asp:TextBox>
                    </td> 
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Contactor" runat="server" Text="联系人："></asp:Label>                        
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="Txt_Contactor" runat="server" Width="80"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="Label1" runat="server" Text="联系电话："></asp:Label>                        
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="Txt_phone" runat="server" Width="80"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间："></asp:Label>                        
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_GetUpTime" runat="server" Width="80" onfocusin="calendar()"></asp:TextBox>-
                        <asp:TextBox ID="Txt_StopTime" runat="server" Width="80" onfocusin="calendar()"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td align="right" class="FieldHeader">
                        <asp:Label ID="lbl_SettleType" runat="server" Text="结算方式：" Width="80"></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:DropDownList ID="DDl_SettleType" runat="server">
                            <asp:ListItem Value="-1" Text="请选择"></asp:ListItem>
                            <asp:ListItem Value="0" Text="预付月结"></asp:ListItem>
                            <asp:ListItem Value="1" Text="预付现结"></asp:ListItem>
                            <asp:ListItem Value="2" Text="到付月结"></asp:ListItem>
                            <asp:ListItem Value="3" Text="到付现结"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_BoxType" runat="server" Text="包裹类型：" Width="80"></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:DropDownList ID="ddl_BoxType" runat="server">
                            <asp:ListItem Value="-1" Text="请选择"></asp:ListItem>
                            <asp:ListItem Value="0" Text="文件"></asp:ListItem>
                            <asp:ListItem Value="1" Text="小包裹"></asp:ListItem>
                            <asp:ListItem Value="2" Text="普货"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="FieldHeader"><asp:Label ID="lbl_HAWBType" runat="server" Text="运单类型"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_HAWBType" runat="server">
                            <asp:ListItem Value="0" Text="国外"></asp:ListItem>
                            <asp:ListItem Value="1" Text="国内"></asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_Demand" runat="server" Text="查 询" CssClass="InputBtn" 
                            onclick="btn_Demand_Click" />
                        <asp:Button ID="btn_Add" runat="server" Text="添 加" CssClass="InputBtn" 
                            onclick="btn_Add_Click" />
                    </td>
                    
                </tr>                
            </thead>
        </table>
    </asp:PlaceHolder>
    <div>
        <asp:GridView ID="Gv_HAWB" runat="server" AutoGenerateColumns="False" 
            onrowcommand="Gv_HAWB_RowCommand" DataKeyNames="BarCode" >
            <Columns>
                <asp:TemplateField HeaderText="运单号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_BarCode" runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发件公司">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ShipperName" runat="server" Text='<%# Eval("ShipperName") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="收件公司">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ConsigneeName" runat="server" Text='<%# Eval("ConsigneeName") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发件时间">
                    <ItemTemplate>
                        <asp:Label ID="lbl_CreateTime" runat="server" Text='<%# Eval("CreateTime","{0:yyyy-MM-dd} ")) %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                </asp:TemplateField>
                <asp:ButtonField CommandName="Eidt" ButtonType="Link" Text="详细" />
                <asp:ButtonField CommandName="Updata" ButtonType="Link" Text="修改" />
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Delete" CommandArgument='<%# Eval("BarCode") %>' CommandName="Del" runat="server" Text="删除" OnClientClick="javascript:return confirm('确定删除该条运单吗?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>
    </form>
</body>
</html>
