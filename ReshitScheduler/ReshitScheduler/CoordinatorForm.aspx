<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CoordinatorForm.aspx.cs" Inherits="ReshitScheduler.CoordinatorForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <h2 > <asp:Literal ID="hTeacherName" runat="server" /></h2>

    <asp:Panel ID="pnlClasses" runat="server" CssClass="list-group">
        <h3>הכיתות שלך</h3>

    </asp:Panel>
    <asp:Panel ID="pnlGroups" runat="server" CssClass="list-group">
        <h3>הקבוצות שלך</h3>

    </asp:Panel>
     <div class="form-row justify-content-center mt-3">
        <button  runat="server"  onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
        <button  runat="server"  onserverclick="BtnLogout_Click" class="btn btn-outline-dark">התנתק</button>
    </div>
</asp:Content>


