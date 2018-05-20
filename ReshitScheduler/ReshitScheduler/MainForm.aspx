<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="ReshitScheduler.MainForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <h2 >
שלום <%=LoggedInTeacher.FirstName + " " +LoggedInTeacher.LastName %>
    </h2>        

    <asp:Panel runat="server" CssClass="row justify-content-center btn-group-vertical" ID="pnlClassesPanel" >
        הכיתות שלך:

    </asp:Panel>
                
    <h2>אפשרויות עריכה:</h2>

    <div class="row justify-content-center btn-group-vertical">
        <button  runat="server" onserverclick="BtnLogout_Click" class="btn btn-outline-dark">התנתק</button>
        <button  runat="server" onserverclick="BtnAddStudent_Click" class="btn btn-outline-dark">הוספת תלמיד חדש</button>
        <button  runat="server" onserverclick="BtnAddClass_Click" class="btn btn-outline-dark">הוספת כיתה חדשה</button>
        <button  runat="server" onserverclick="BtnAddCourse_Click" class="btn btn-outline-dark">הוספת קורס</button>
        <button  runat="server" onserverclick="BtnAddGroup_Click" class="btn btn-outline-dark">הוספת קבוצה</button>
        <button  runat="server" onserverclick="BtnBellSystem_Click" class="btn btn-outline-dark">אתחול מערכת צלצולים</button>
        <button  runat="server" onserverclick="BtnLogout_Click" class="btn btn-outline-dark">הוספת מורה</button>
    </div>
</asp:Content>
