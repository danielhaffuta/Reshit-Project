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
        <asp:Label ID="Course" runat="server" Text="שם הקורס:"></asp:Label><asp:TextBox ID="CourseName" runat="server" Type="text"></asp:TextBox><br /><br />
        <asp:Label ID="Teacher" runat="server" Text="Label">שם המורה:</asp:Label><asp:DropDownList ID="TeachersList" runat="server"></asp:DropDownList><br /><br />
        <asp:Button ID="Save" runat="server" OnClick ="SaveClick" Text="שמור" /><br /><br />
        <asp:Button ID="Back" runat="server" OnClick ="BackClick" Text="חזרה" />
    </div>
    </form>
</body>
</html>
