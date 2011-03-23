<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="FlightManagement.aspx.cs" Inherits="GGGETSAdmin.FlightManage.FlightManagement" Theme="logisitc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register Assembly="AutoCompleteExtra" Namespace="AutoCompleteExtra" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc2:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc2:ToolkitScriptManager>
    <div>
        
        <table class="DataView">
            <tbody>
                <tr class="Row">
                    <td class="FieldHeader">
                        <asp:Label ID="lbl_FlightNo" runat="server" Text="航班号:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_FlightNo" Style="text-transform: uppercase" TabIndex="1" runat="server" Width="250" CssClass="TextBox"></asp:TextBox>
                    </td>
                     <td class="FieldHeader" style="width:160px">
                        <asp:Label ID="lbl_From" runat="server" Text="起飞三字码:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_From" TabIndex="2" runat="server" Style="text-transform: uppercase" Width="50" CssClass="TextBox"></asp:TextBox>
                        <cc1:AutoCompleteExtraExtender runat="server" ID="autoFrom" ServiceMethod="GetCountryList"
                                        TargetControlID="Txt_From" AsyncPostback="false" AutoPostback="true"
                                        MinimumPrefixLength="1" CompletionSetCount="10" onitemselected="autoFrom_ItemSelected">
                                    </cc1:AutoCompleteExtraExtender>
                    </td>
                     <td class="FieldHeader" style="width:160px">
                        <asp:Label ID="lbl_To" runat="server" Text="到达三字码:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_To" TabIndex="3" Style="text-transform: uppercase" runat="server" Width="50" CssClass="TextBox"></asp:TextBox>
                        <cc1:AutoCompleteExtraExtender runat="server" ID="autoTo" ServiceMethod="GetCountryList"
                                        TargetControlID="Txt_To" AsyncPostback="false" AutoPostback="true"
                                        MinimumPrefixLength="1" CompletionSetCount="10" onitemselected="autoTo_ItemSelected">
                                    </cc1:AutoCompleteExtraExtender>
                    </td>
                    <td>
                        <asp:Button ID="btn_Demand" TabIndex="6" runat="server" Text="查 询" CssClass="InputBtn" OnClick="btn_Demand_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="height: 350px;overflow-x:auto;overflow-y:auto;">
        <asp:GridView ID="gv_HAWB" runat="server" CssClass="DataView" AutoGenerateColumns="False"
            OnRowEditing="gv_HAWB_RowEditing" onrowupdating="gv_HAWB_RowUpdating" 
            DataKeyNames="MID" onrowcancelingedit="gv_HAWB_RowCancelingEdit" 
            onrowcommand="gv_HAWB_RowCommand" onrowdeleting="gv_HAWB_RowDeleting" PageSize="5000">
            <Columns>
                <asp:TemplateField HeaderText="行号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Number" runat="server" Text='<%# N() %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="航班号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_FlightNo" runat="server" Text='<%# Eval("FlightNo")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="总运单号">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_BarCode" runat="server" CommandName="Select" PostBackUrl='<%# "../MawbManage/MawbDetails.aspx?BarCode="+Eval("BarCode") %>' Text='<%# Eval("BarCode") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Width="220px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="起飞三字码">
                    <ItemTemplate>
                        <asp:Label ID="lbl_from" runat="server" Text='<%# Eval("From") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="Txt_from" runat="server" TabIndex="3" Text='<%# Eval("From") %>' style="text-transform:uppercase" CssClass="TextBox"></asp:TextBox>
                        <cc1:AutoCompleteExtraExtender runat="server" ID="autoFrom" ServiceMethod="GetCountryList"
                                        TargetControlID="Txt_From" AsyncPostback="false" AutoPostback="true"
                                        MinimumPrefixLength="1" CompletionSetCount="10" onitemselected="autoFrom_ItemSelected">
                                    </cc1:AutoCompleteExtraExtender>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="起飞三字码">
                    <ItemTemplate>
                        <asp:Label ID="lbl_To" runat="server" Text='<%# Eval("To") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="Txt_To" runat="server" MaxLength="3" Text='<%# Eval("To") %>' style="text-transform:uppercase" CssClass="TextBox"></asp:TextBox>
                        <cc1:AutoCompleteExtraExtender runat="server" ID="autoTo" ServiceMethod="GetCountryList"
                                        TargetControlID="Txt_To" AsyncPostback="false" AutoPostback="true"
                                        MinimumPrefixLength="1" CompletionSetCount="10" onitemselected="autoTo_ItemSelected">
                                    </cc1:AutoCompleteExtraExtender>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <%--<asp:CommandField ShowEditButton="True" />--%>
                <asp:ButtonField CommandName="Delete" ButtonType="Link" Text="删除" />
                <%-- <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="Ck_ID" runat="server" onclick="checkAll(this,'chkId')" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        </div>
    </div>
</asp:Content>
