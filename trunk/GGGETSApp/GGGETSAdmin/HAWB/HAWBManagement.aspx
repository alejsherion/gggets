<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="HAWBManagement.aspx.cs" Inherits="GGGETSAdmin.HAWB.HAWBManagement"
    Theme="logisitc" %>

<%@ Register Src="~/Control/MyPanel.ascx" TagName="MyPanel" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style>
        #login
        {
            position: relative;
            display: none;
            top: 20px;
            left: 30px;
            background-color: #ffffff;
            text-align: center;
            border: solid 1px;
            padding: 10px;
            z-index: 1;
        }
        
        body
        {
            background-color: #0099CC;
        }
        .STYLE1
        {
            color: #FFFF00;
        }
    </style>
    <script type="text/javascript">


        var W = screen.width; //取得屏幕分辨率宽度
        var H = screen.height; //取得屏幕分辨率高度

        function M(id) {
            return document.getElementById(id); //用M()方法代替document.getElementById(id)
        }
        function MC(t) {
            return document.createElement(t); //用MC()方法代替document.createElement(t)
        };
        //判断浏览器是否为IE
        function isIE() {
            return (document.all && window.ActiveXObject && !window.opera) ? true : false;
        }
        //取得页面的高宽
        function getBodySize() {
            var bodySize = [];
            with (document.documentElement) {
                bodySize[0] = (scrollWidth > clientWidth) ? scrollWidth : clientWidth; //如果滚动条的宽度大于页面的宽度，取得滚动条的宽度，否则取页面宽度
                bodySize[1] = (scrollHeight > clientHeight) ? scrollHeight : clientHeight; //如果滚动条的高度大于页面的高度，取得滚动条的高度，否则取高度
            }
            return bodySize;
        }
        //创建遮盖层
        function popCoverDiv() {
            if (M("cover_div")) {
                //如果存在遮盖层，则让其显示 
                M("cover_div").style.display = 'block';
            } else {
                //否则创建遮盖层
                var coverDiv = MC('div');
                document.body.appendChild(coverDiv);
                coverDiv.id = 'cover_div';
                with (coverDiv.style) {
                    position = 'absolute';
                    background = '#CCCCCC';
                    left = '0px';
                    top = '0px';
                    var bodySize = getBodySize();
                    width = bodySize[0] + 'px'
                    height = bodySize[1] + 'px';
                    zIndex = 0;
                    if (isIE()) {
                        filter = "Alpha(Opacity=60)"; //IE逆境
                    } else {
                        opacity = 0.6;
                    }
                }
            }
        }



        //让登陆层显示为块 
        function showLogin() {
            var login = M("login");
            login.style.display = "block";
        }

        //设置DIV层的样式
        function change() {
            var login = M("login");
            login.style.position = "absolute";
            login.style.border = "1px solid #CCCCCC";
            login.style.background = "#F6F6F6";
            var i = 0;
            var bodySize = getBodySize();
            login.style.left = (bodySize[0] - i * i * 4) / 2 + "px";
            login.style.top = (bodySize[1] / 2 - 100 - i * i) + "px";
            login.style.width = i * i * 4 + "px";
            login.style.height = i * i * 1.5 + "px";

            popChange(i);
        }
        //让DIV层大小循环增大
        function popChange(i) {
            var login = M("login");
            var bodySize = getBodySize();
            login.style.left = (bodySize[0] - i * i * 4) / 2 + "px";
            login.style.top = (bodySize[1] / 2 - 100 - i * i) + "px";
            login.style.width = i * i * 2.5 + "px";
            login.style.height = i * i * 1.3 + "px";
            if (i <= 10) {
                i++;
                setTimeout("popChange(" + i + ")", 10); //设置超时10毫秒
            }
        }
        //打开DIV层
        function open() {
            change();
            showLogin();
            popCoverDiv()
            void (0); //不进行任何操作,如：<a href="#">aaa</a>
        }
        //关闭DIV层
        function close() {
            M('login').style.display = 'none';
            M("cover_div").style.display = 'none';
            void (0);
        }

    </script>
    <uc1:MyPanel ID="MyPanel1" runat="server" />
    <fieldset>
        <div class="FunctionBar">
            <div class="DivFloatLeft">
                <a href="#">订单管理</a> -> <a href="#">订单详细信息</a></div>
            <div class="DivFloatRight">
                <asp:Label ID="Lbl_BarCode" runat="server" Text="条形码:"></asp:Label>
                <asp:TextBox ID="Txt_BarCode" runat="server"></asp:TextBox>
                <asp:Button ID="But_Demand" CssClass="InputBtn" runat="server" Text="查 询" OnClick="But_Demand_Click" />
                <a href="javascript:open();" class="LinkBtn">新增</a>
            </div>
            <div class="Clear">
            </div>
        </div>
        <div id="login">
            <table border="0" width="300" id="table109" cellpadding="0">
                <tr>
                    <td valign="top">
                        <table border="0" width="100%" id="table110" cellpadding="0" class="white">
                            <tr>
                                <td>
                                    <table class="white" width="100%" id="table111">
                                        <tr>
                                            <td bgcolor="#FFB64B">
                                                <p align="center">
                                                    <font color="#FFFFFF"><b>温馨提示</b></font>
                                                    </p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="80">
                                    <p align="left">
                                        <b>尊敬的用户：</b><br>
                                        请选择: <a href="AddHAWB.aspx" class="LinkBtn">国际快递业务 </a>/<a href="AddHAWBInernal.aspx" class="LinkBtn">国内快递业务</a>
                                    </p>
                                </td>
                                
                            </tr>
                            <tr>
                                <td align="center">
                                    <a class="LinkBtn" href="javascript:close();">关闭</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:GridView ID="GV_Bdemand" runat="server" OnRowCommand="GV_Bdemand_RowCommand"
                DataKeyNames="BarCode" OnRowDeleting="GV_Bdemand_RowDeleting" PageSize="10">
                <Columns>
                    <asp:TemplateField HeaderText="条形码">
                        <ItemTemplate>
                            <asp:Label ID="lbl_BarCode" runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                            <asp:Label ID="lbl_Guid" runat="server" Text='<%# Eval("Hid") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="承运公司">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Carrier" runat="server" Text='<%# Eval("Carrier") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发件联系人">
                        <ItemTemplate>
                            <asp:Label ID="lbl_ShipperContactor" runat="server" Text='<%# Eval("ShipperContactor") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收件联系人">
                        <ItemTemplate>
                            <asp:Label ID="lbl_ConsigneeContactor" runat="server" Text='<%# Eval("ConsigneeContactor") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="运单截止日期">
                        <ItemTemplate>
                            <asp:Label ID="lbl_DeadlineTime" runat="server" Text='<%# Eval("DeadlienTime","{0:yyyy-MM-dd} ") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="查看信息">
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_Select" runat="server" CommandName="Select" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                Text="详细" CssClass="LinkBtn"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="修改信息">
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_Eit" runat="server" CommandName="Eit" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                Text="修改" CssClass="LinkBtn"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="删除信息">
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_Colse" runat="server" CommandName="Close" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                Text="删除" CssClass="LinkBtn" Enabled="false"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="FooterBtnBar">
            <asp:Button ID="But_Goup" runat="server" CssClass="InputBtn" Text="上一页" OnClick="But_Goup_Click" />
            <asp:Button ID="But_Down" runat="server" CssClass="InputBtn" Text="下一页" OnClick="But_Down_Click" />
        </div>
    </fieldset>
</asp:Content>
