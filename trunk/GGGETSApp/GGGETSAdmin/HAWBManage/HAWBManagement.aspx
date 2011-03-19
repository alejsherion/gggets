<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HAWBManagement.aspx.cs" Inherits="GGGETSAdmin.HAWBManage.HAWBManagement" Theme="logisitc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../Scripts/calendar.js"></script>
    <script type="text/javascript">

        function NumberCheck(name) {
            var s = name.value;
            var regu = /^[0-9]*$/;
            var re = new RegExp(regu);
            if (s.search(re) == -1) {
                name.select();
                alert("页数只能输入整数")
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: right">
          <asp:LinkButton ID="lbtn_Navigation" runat="server" Text="主页" PostBackUrl="~/Navigation.aspx" CssClass="LinkBtn"></asp:LinkButton>
     </div>
    <div>
        <asp:PlaceHolder ID="phHawb" runat="server">
        <table class="DataView">
            <thead>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_BarCode" runat="server" Text="运单号："></asp:Label>
                    </td>
                    <td colspan="5" align="left">
                        <asp:TextBox ID="Txt_BarCode" runat="server" Width="500" TabIndex="1" style="text-transform:uppercase"></asp:TextBox>
                    </td>                    
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="Country" runat="server" Text="国家二字码："></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="Txt_Country" runat="server" Width="80" MaxLength="2" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Region" runat="server" Text="地区三字码："></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="Txt_Region" runat="server" Width="80" MaxLength="3" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Account" runat="server" Text="客户帐号：" Width="80"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Account1" runat="server" Width="80" TabIndex="2" style="text-transform:uppercase"></asp:TextBox>-
                        <asp:TextBox ID="Txt_Account2" runat="server" Width="50" TabIndex="3" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_CorporationName" runat="server" Text="承运公司名称："></asp:Label>
                    </td>
                    <td colspan="5" align="left">
                        <asp:TextBox ID="Txt_corporationName" runat="server" Width="500" style="text-transform:uppercase"></asp:TextBox>
                    </td> 
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_LoginName" runat="server" Text="操作人："></asp:Label> 
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_LoginName" runat="server" Width="80"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_Contactor" runat="server" Text="联系人姓名："></asp:Label>                        
                    </td>
                    <td align="left" style="width:80px">
                        <asp:TextBox ID="Txt_Contactor" runat="server" Width="80" style="text-transform:uppercase"></asp:TextBox>
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
                    <td class="FieldHeader" align="right">
                        <asp:Label ID="lbl_HAWBType" runat="server" Text="运单类型："></asp:Label>
                    </td>
                    <td align="left" style="width:80px">
                        <asp:DropDownList ID="ddl_HAWBType" runat="server">
                            <asp:ListItem Value="0" Text="国外"></asp:ListItem>
                            <asp:ListItem Value="1" Text="国内"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="FieldHeader" align="right">
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
                        <asp:Button ID="btn_Demand" runat="server" Text="查 询" CssClass="InputBtn" 
                            onclick="btn_Demand_Click" />
                    </td>
                </tr>                
            </thead>
        </table>
    </asp:PlaceHolder>
    <div style="height: 350px;overflow-x:auto;overflow-y:auto;">
        <asp:GridView ID="Gv_HAWB" runat="server" AutoGenerateColumns="False" 
            onrowcommand="Gv_HAWB_RowCommand" DataKeyNames="BarCode" PageSize="36">
            <Columns>
            <asp:TemplateField HeaderText="行号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Number" runat="server" Text='<%# N() %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="运单号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_BarCode" runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发件公司">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ShipperName" runat="server" Text='<%# Eval("ShipperName").ToString().Length>10?Eval("ShipperName").ToString().Substring(0,10)+"":Eval("ShipperName") %>' ToolTip='<%# Eval("ShipperName")%>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="150px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="收件公司">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ConsigneeName" runat="server" Text='<%# Eval("ConsigneeName").ToString().Length>10?Eval("ConsigneeName").ToString().Substring(0,10)+"":Eval("ConsigneeName") %>' ToolTip='<%# Eval("ConsigneeName")%>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="150px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发件时间">
                    <ItemTemplate>
                        <asp:Label ID="lbl_CreateTime" runat="server" Text='<%# Eval("CreateTime","{0:yyyy-MM-dd HH:mm} ") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="120px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:ButtonField CommandName="Eidt" ButtonType="Link" Text="详细" >
                <ItemStyle Width="50px" />
                </asp:ButtonField>
                <asp:ButtonField CommandName="Updata" ButtonType="Link" Text="修改" >
                <ItemStyle Width="50px" />
                </asp:ButtonField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Delete" CommandArgument='<%# Eval("BarCode") %>' CommandName="Del" runat="server" Text="删除" OnClientClick="javascript:return confirm('确定删除该条运单吗?');"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Derive" CommandArgument='<%# Eval("BarCode") %>' CommandName="Derive" runat="server" Text="导出运单发票"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_DeriveAccept" CommandArgument='<%# Eval("CarrierHAWBBarCode") %>' CommandName="DeriveAccept" runat="server" Text="导出承运发票"></asp:LinkButton>
                        
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="FenYe" runat="server" visible="false" class="DataView">
        <asp:Button ID="btn_homepage" runat="server" Text="首页" CssClass="InputBtn" 
            onclick="btn_homepage_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Up" runat="server" Text="上一页" onclick="btn_Up_Click" CssClass="InputBtn" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lbl_nuber" runat="server" ForeColor="Red"></asp:Label><b style="color: Red">/</b>
        <asp:Label ID="lbl_sumnuber" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Jumpto" runat="server" Text="跳转到" CssClass="InputBtn" 
            onclick="btn_Jumpto_Click" /><asp:TextBox ID="Txt_Jumpto"
            runat="server" Width="30" CssClass="TextBox" onblur="NumberCheck(this)"></asp:TextBox>
        <asp:Button ID="btn_down" runat="server" Text="下一页" onclick="btn_down_Click" CssClass="InputBtn" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_lastpage" runat="server" Text="末页" CssClass="InputBtn" 
            onclick="btn_lastpage_Click" />
    </div>
</div>
    </form>
</body>
</html>
