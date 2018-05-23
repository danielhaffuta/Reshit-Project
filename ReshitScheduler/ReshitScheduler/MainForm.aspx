<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="ReshitScheduler.MainForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <h2 >
שלום <%=LoggedInTeacher.FirstName + " " +LoggedInTeacher.LastName %>
    </h2>        



    <h3 runat="server" id="h3Courses">השיעורים שלך</h3>
    <asp:Panel ID="pnlCourses" runat="server" CssClass="row justify-content-center btn-group-vertical">

    </asp:Panel>     
     
    <h3 runat="server" id="h3Groups">הקבוצות שלך</h3>
    <asp:Panel ID="pnlGroups" runat="server" CssClass="row justify-content-center btn-group-vertical">

    </asp:Panel>

    <h3>אפשרויות עריכה:</h3>

    <div class="row justify-content-center btn-group-vertical">
        <button  runat="server" onserverclick="BtnLogout_Click" class="btn btn-outline-dark">התנתק</button>
        <button  runat="server" onserverclick="BtnAddStudent_Click" class="btn btn-outline-dark">הוספת תלמיד חדש</button>
        <%if (bIsPrincipal)
            {%>
        <button  runat="server" onserverclick="BtnAddClass_Click" class="btn btn-outline-dark" >הוספת כיתה חדשה</button>
        <button  runat="server" onserverclick="BtnAddCourse_Click" class="btn btn-outline-dark">הוספת קורס</button>
        <button  runat="server" onserverclick="BtnAddGroup_Click" class="btn btn-outline-dark">הוספת קבוצה</button>
        <button  runat="server" onserverclick="BtnBellSystem_Click" class="btn btn-outline-dark">אתחול מערכת צלצולים</button>
        <button  runat="server" onserverclick="BtnAddTeacher_Click" class="btn btn-outline-dark">הוספת מורה</button>
        <button  runat="server" onserverclick="BtnEditTeacher_Click" class="btn btn-outline-dark">ערוך פרטי מורה</button>
        <button  runat="server" onserverclick="BtnEditClass_Click" class="btn btn-outline-dark">ערוך פרטי כיתה</button>
        <%} %>
    </div>
</asp:Content>
