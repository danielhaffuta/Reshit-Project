<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddLessonForm.aspx.cs" Inherits="ReshitScheduler.AddCourseForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel = "stylesheet" href = "/css/Course.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <asp:Label ID="Course" runat="server" Text="שם הקורס:"></asp:Label><asp:TextBox ID="CourseName" runat="server" Type="text"></asp:TextBox><br /><br />
        <asp:Label ID="Teacher" runat="server" Text="Label">שם המורה:</asp:Label>
        <asp:DropDownList ID="TeachersList" runat="server"></asp:DropDownList><br /><br />
        <div class="row justify-content-center btn-group-vertical">
            <button  runat="server"  onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
            <button  runat="server"  onserverclick="BtnSave_Click" class="btn btn-outline-dark">שמור</button>
        </div>
</asp:Content>
