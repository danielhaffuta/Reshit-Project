<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ClassPage.aspx.cs" Inherits="ReshitScheduler.ClassPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <%if(false) { %>
    <div class="form-row justify-content-center btn-group sticky-top bg-success" dir="ltr">
        <button  runat="server" onserverclick="GotoCoursesAndGroupsForm" class="btn btn-outline-dark">ניהול קבוצות וקורסים</button>
        <button  runat="server"  onserverclick="BtnPrintSchedule_Click" class="btn btn-outline-dark">הדפס מערכת</button>
        <button  runat="server"  onserverclick="BtnPrintScheduleForAllStudents_Click" class="btn btn-outline-dark">הדפס מערכות של כל התלמידים</button>
        <button  runat="server" onserverclick="BtnAddStudent_Click" class="btn btn-outline-dark">הוספת תלמיד חדש</button>
        <button  runat="server" onserverclick="BtnLogout_Click" class="btn btn-outline-dark">התנתק</button>
        <button  runat="server" onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
    </div>
    <%} %>
    <h2><%=strClassName %></h2>
    <div class="row mt-5">
        <asp:Panel runat="server" ID="pnlSchedule" CssClass="table-responsive col-12">
    
        </asp:Panel>
    </div>
    
    
    <script src="/js/general.js"></script>

    <!--Reload on get focus-->
    <script>
        var blurred = false;
        window.onblur = function () { blurred = true; };
        window.onfocus = function () { blurred && (location.reload()); };
    </script>
</asp:Content>

