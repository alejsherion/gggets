<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="PackageDetails.aspx.cs" Inherits="GGGETSAdmin.PackageManage.PackageDetails" Theme="logisitc" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="DataView" cellspacing="0" cellpadding="3">
            <thead>
            </thead>
            <tbody>
                <tr class="AlternatingRow">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_BagBarCode" runat="server" Text="包号:" 
                            meta:resourcekey="lbl_BagBarCodeResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="txt_BagBarCode" runat="server" Width="230px" 
                            meta:resourcekey="txt_BagBarCodeResource1"></asp:Label>
                    </td>
                    <td class="FieldHeader" style="width:120px">
                        <asp:Label ID="lbl_CreateTime" runat="server" Text="创建时间:" 
                            meta:resourcekey="lbl_CreateTimeResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="txt_CreateTime" runat="server" 
                            meta:resourcekey="txt_CreateTimeResource1"></asp:Label>
                    </td>
                    <td class="FieldHeader" style="width:120px">
                        <asp:Label ID="lbl_UpdateTime" runat="server" Text="修改时间:" 
                            meta:resourcekey="lbl_UpdateTimeResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="txt_UpdateTime" runat="server" 
                            meta:resourcekey="txt_UpdateTimeResource1"></asp:Label>
                    </td>
                    <td class="FieldHeader" style="width:200px">
                        <asp:Label ID="lbl_Region" runat="server" Text="目的地三字码:" 
                            meta:resourcekey="lbl_RegionResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="txt_Destination" runat="server" Width="50px" 
                            meta:resourcekey="txt_DestinationResource1"></asp:Label>
                    </td>
                </tr>
                <tr class="Row">
                    <td class="FieldHeader" style="width:120px">
                        <asp:Label ID="lbl_MHAWb" runat="server" Text="总运单号:" 
                            meta:resourcekey="lbl_MHAWbResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="lbtn_MHAWb" runat="server" OnClick="lbtn_MHAWb_Click" 
                            Text='<%# Eval("TotalWeight") %>' meta:resourcekey="lbtn_MHAWbResource1"></asp:LinkButton>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_Pice" runat="server" Text="件数:" 
                            meta:resourcekey="lbl_PiceResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="txt_Pice" runat="server" meta:resourcekey="txt_PiceResource1"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_TotalWeight" runat="server" Text="总重量:" 
                            meta:resourcekey="lbl_TotalWeightResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Txt_TotalWeight" runat="server" 
                            meta:resourcekey="Txt_TotalWeightResource1"></asp:Label>
                    </td>
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_Status" runat="server" Text="当前状态:" 
                            meta:resourcekey="lbl_StatusResource1"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="txt_Status" runat="server" 
                            meta:resourcekey="txt_StatusResource1"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="height: 350px;overflow-x:auto;overflow-y:auto;">
        <asp:GridView ID="gv_HAWB" runat="server" CssClass="DataView" 
            AutoGenerateColumns="False" meta:resourcekey="gv_HAWBResource1" 
            PageSize="35">
            <Columns>
                <asp:TemplateField HeaderText="行号" meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Number" runat="server" Text='<%# N() %>' 
                            meta:resourcekey="lbl_NumberResource1"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="运单号" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_BarCoder" runat="server" PostBackUrl='<%# "../HAWBManage/HAWBDetails.aspx?BarCode="+Eval("BarCode") %>'
                            Text='<%# Eval("BarCode") %>' meta:resourcekey="lbtn_BarCoderResource1"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发件公司">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ShipperName" runat="server" Text='<%# Eval("ShipperName") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="150px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="收件公司">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ConsigneeName" runat="server" Text='<%# Eval("ConsigneeName") %>'></asp:Label>
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
                <asp:TemplateField HeaderText="重量" meta:resourcekey="TemplateFieldResource3">
                    <ItemTemplate>
                        <asp:Label ID="lbl_TotalWeight" runat="server" 
                            Text='<%# Eval("TotalWeight") %>' meta:resourcekey="lbl_TotalWeightResource2"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <%-- <asp:TemplateField>
                                <HeaderTemplate><asp:CheckBox ID="Ck_Sum" runat="server" /></HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Ck" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        </div>
        <div class="AlternatingRow">
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="But_Update" runat="server" Text="修 改" CssClass="InputBtn" 
                    OnClick="But_Next_Click" meta:resourcekey="But_UpdateResource1" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="But_Conel" runat="server" Text="返 回" CssClass="InputBtn" 
                    OnClick="But_Conel_Click" meta:resourcekey="But_ConelResource1" />
        </div>
    </div>
</asp:Content>
