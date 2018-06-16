<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TeacherClassesForm.aspx.cs" Inherits="ReshitScheduler.TeacherClassesForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="navbar" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainForm" runat="server">
    <h3>הכיתות שלך:</h3>
    <asp:Panel ID="pnlClassesPanel" runat="server" CssClass="row justify-content-center">
    </asp:Panel>
</asp:Content>
