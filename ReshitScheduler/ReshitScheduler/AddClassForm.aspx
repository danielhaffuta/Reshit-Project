<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddClassForm.aspx.cs" Inherits="ReshitScheduler.AddClassForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel = "stylesheet" href = "/css/Class.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <asp:Label ID="Grade" runat="server" Text="Label">שכבה:</asp:Label><asp:DropDownList ID="GradeList" runat="server"></asp:DropDownList><br /><br />
        <asp:Label ID="NumberOfClass" runat="server" Text="Label">מספר כיתה:</asp:Label><asp:TextBox ID="ClassNum" runat="server" Type="number"></asp:TextBox><br /><br />
        <asp:Label ID="Educator" runat="server" Text="Label">שם המחנך:</asp:Label><asp:DropDownList ID="EducatorsList" runat="server"></asp:DropDownList><br /><br />
        <asp:Label ID="LabelYear" runat="server" Text="Label">שנה:</asp:Label><asp:DropDownList ID="JoinYear" runat="server"></asp:DropDownList><br /><br />
        <asp:Button ID="Save" runat="server" OnClick ="SaveClick" Text="שמור" /><br />
</asp:Content>


