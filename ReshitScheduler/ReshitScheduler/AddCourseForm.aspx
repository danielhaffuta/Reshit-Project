<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddCourseForm.aspx.cs" Inherits="ReshitScheduler.AddCourseForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel = "stylesheet" href = "/css/Course.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <asp:Label ID="Course" runat="server" Text="שם הקורס:"></asp:Label><asp:TextBox ID="CourseName" runat="server" Type="text"></asp:TextBox><br /><br />
        <asp:Label ID="Teacher" runat="server" Text="Label">שם המורה:</asp:Label><asp:DropDownList ID="TeachersList" runat="server"></asp:DropDownList><br /><br />
        <asp:Button ID="Save" runat="server" OnClick ="SaveClick" Text="שמור" /><br /><br />
        <asp:Button ID="Back" runat="server" OnClick ="BackClick" Text="חזרה" />
</asp:Content>
