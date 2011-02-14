<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HAWBAdd.aspx.cs" Inherits="GGGETSAdmin.HAWBManage.HAWBAdd" Theme="logisitc" %>
<%@ Register src="../Control/HAWBGuoJi.ascx" tagname="HAWBGuoJi" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:HAWBGuoJi ID="HAWBGuoJi1" runat="server" />
    </asp:Content>
