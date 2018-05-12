<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LessonForm.aspx.cs" Inherits="ReshitScheduler.LessonForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <asp:Panel runat="server" ID="pnlClassesPanel">
       
    </asp:Panel>
    <div class="form-row justify-content-center">
        <button  runat="server"  onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
    </div>
</asp:Content>
