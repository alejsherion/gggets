<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HAWBAdd.aspx.cs" Inherits="GGGETSAdmin.HAWB.HAWBAdd" Theme="logisitc" %>
<%@ Register src="../Control/HAWB.ascx" tagname="HAWB" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:HAWB ID="HAWB1" runat="server" />
</asp:Content>
