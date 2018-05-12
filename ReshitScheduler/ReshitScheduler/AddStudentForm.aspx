<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddStudentForm.aspx.cs" Inherits="ReshitScheduler.AddStudentForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel = "stylesheet" href = "/css/Student.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <asp:Label ID="NameStudent" runat="server" Text="Label">שם התלמיד:</asp:Label><asp:TextBox ID="StudentName" runat="server"></asp:TextBox><br /><br />
        <asp:Label ID="LastNameStudent" runat="server" Text="Label">שם משפחה:</asp:Label><asp:TextBox ID="StudentLastName" runat="server"></asp:TextBox><br /><br />
        <asp:Label ID="Class" runat="server" Text="Label">כיתה:</asp:Label><asp:DropDownList ID="ClassesList" runat="server"></asp:DropDownList><br /><br />
       <!-- <asp:Label ID="Picture" runat="server" Text="Label">תמונה:</asp:Label><asp:FileUpload ID="StudentPic" runat="server" accept ="image/*" /><br /><br />-->
        <asp:Button ID="Save" runat="server" OnClick ="SaveClick" Text="שמור" /><br /><br />
        <asp:Button ID="Back" runat="server" OnClick ="BackClick" Text="חזרה" /><br />
</asp:Content>

