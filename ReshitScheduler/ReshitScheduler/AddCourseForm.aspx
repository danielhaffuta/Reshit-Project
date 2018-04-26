<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCourseForm.aspx.cs" Inherits="ReshitScheduler.AddCourseForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Course" runat="server" Text="Label">שם הקורס:</asp:Label><input id="CourseName" type="text" /><br />
        <asp:Label ID="Teacher" runat="server" Text="Label">שם המורה:</asp:Label><asp:DropDownList ID="TeachersList" runat="server"></asp:DropDownList><br />
        <asp:Label ID="Group" runat="server" Text="Label">האם זו קבוצה?</asp:Label><asp:RadioButton ID="IsGroup" Text="כן" runat="server" /><asp:RadioButton ID="NotGtoup" Text="לא" runat="server" />
    </div>
    </form>
</body>
</html>
