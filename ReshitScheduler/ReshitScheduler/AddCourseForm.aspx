<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCourseForm.aspx.cs" Inherits="ReshitScheduler.AddCourseForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel = "stylesheet" href = "css/Course.css" />
    <title></title>
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Course" runat="server" Text="Label">שם הקורס:</asp:Label><asp:TextBox ID="CourseName" runat="server" Type="text"></asp:TextBox><br /><br />
        <asp:Label ID="Teacher" runat="server" Text="Label">שם המורה:</asp:Label><asp:DropDownList ID="TeachersList" runat="server"></asp:DropDownList><br /><br />
        <asp:Label ID="Group" runat="server" Text="Label">האם זו קבוצה?</asp:Label><input id="IsGroup" type="radio" />כן <input id="NotGroup" type="radio" />לא<br /><br />
        <asp:Label ID="LabelYear" runat="server" Text="Label">שנה:</asp:Label><asp:DropDownList ID="JoinYear" runat="server"></asp:DropDownList><br /><br />
        <asp:Button ID="Save" runat="server" Text="שמור" /><br />
    </div>
    </form>
</body>
</html>
