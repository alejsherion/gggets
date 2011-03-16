﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="MawbManagement.aspx.cs" Inherits="GGGETSAdmin.MawbManage.MawbManagement" Theme="logisitc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../Scripts/calendar.js"></script>
    <div>
        <table class="DataView">
            <tbody>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_MAWBBarCode" runat="server" Text="总运单号:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_MAWBBarCode" TabIndex="1" runat="server" Width="250" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:"></asp:Label>
                    </td>
                    <td style="width:75px">
                        <asp:TextBox ID="txt_UpCreateTime" TabIndex="2" runat="server" Width="75px" onfocusin="calendar()" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td class="FieldHeader" style="width:5px">
                        <asp:Label ID="lbl_Status" runat="server" Text="-"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ToCreateTime" TabIndex="3" runat="server" Width="75px" onfocusin="calendar()" style="text-transform:uppercase"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btn_Demand" TabIndex="4" runat="server" Text="查 询" CssClass="InputBtn" OnClick="btn_Demand_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="height: 350px;overflow-x:auto;overflow-y:auto;">
        <asp:GridView ID="gv_HAWB" runat="server" CssClass="DataView" 
            AutoGenerateColumns="False" onrowcommand="gv_HAWB_RowCommand" 
            PageSize="36">
            <Columns>
                <asp:TemplateField HeaderText="行号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Number" runat="server" Text='<%# N() %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="航班号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_FlightNo" runat="server" Text='<%# Eval("FlightNo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="总运单号">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_FLTNo" runat="server" PostBackUrl='<%# "MawbDetails.aspx?BarCode="+Eval("BarCode") %>'
                            Text='<%# Eval("BarCode") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Width="150px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="重量">
                    <ItemTemplate>
                        <asp:Label ID="lbl_BarCoder" runat="server" Text='<%# Eval("TotalWeight") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Status").ToString().Replace("0","打开").Replace("1","关闭")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Derive" CommandArgument='<%# Eval("BarCode") %>' CommandName="Derive" runat="server" Text="导出总运单清单"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_DeriveAccept" CommandArgument='<%# Eval("BarCode") %>' CommandName="DeriveAccept" runat="server" Text="导出承运清单"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
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
    <script type="text/javascript">

        function checkAll(obj, form_id) {
            var idstr = "";
            var objs;
            if (obj.checked) {
                objs = document.forms[0].elements;
                for (i = 0; i < objs.length; i++) {
                    objs[i].checked = true;

                }
            }
            else {
                objs = document.forms[0].elements;
                for (i = 0; i < objs.length; i++) {
                    objs[i].checked = false;

                }

            }
        }
        function NumberCheck(name) {
            var s = document.getElementById('ContentPlaceHolder1_Txt_Jumpto').value;
            var regu = /^[0-9]*$/;
            var re = new RegExp(regu);
            if (s.search(re) == -1) {
                name.select();
                alert("页数只能输入整数")
            }
        }
    </script>
</asp:Content>
