<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HOME.aspx.cs" Inherits="GGGETSAdmin.HOME" %>
<%@ Register src="TopMenu.ascx" tagname="TopMenu" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Navigation" runat="server">
    
    <uc1:TopMenu ID="TopMenu1" runat="server" />
    
</asp:Content>
