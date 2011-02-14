<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HAWBItemAdd.aspx.cs" Inherits="GGGETSAdmin.HAWBManage.HAWBItemAdd" Theme="logisitc" %>
<%@ Register src="../Control/HAWBItem.ascx" tagname="HAWBItem" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:HAWBItem ID="HAWBItem1" runat="server" />
</asp:Content>
