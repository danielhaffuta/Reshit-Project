<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ClassPage.aspx.cs" Inherits="ReshitScheduler.ClassPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <h2><%=strClassName %></h2>
    <div class="row mt-5">
        <asp:Panel runat="server" ID="pnlSchedule" CssClass="table-responsive col-12">
    
        </asp:Panel>
    </div>
    <asp:Panel runat="server" ID="pnlStudents" CssClass="row">

    </asp:Panel>
    <div class="form-row justify-content-center">
        <button  runat="server" onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
        <button  runat="server" onserverclick="BtnLogout_Click" class="btn btn-outline-dark">התנתק</button>
        <button  runat="server" onserverclick="GotoCoursesAndGroupsForm" class="btn btn-outline-dark">ניהול קבוצות וקורסים</button>
    </div>
    <script src="/js/general.js"></script>
    <script>
        var blurred = false;
        window.onblur = function () { blurred = true; };
        window.onfocus = function () { blurred && (location.reload()); };
    </script>
</asp:Content>

