<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTeacherForm.aspx.cs" Inherits="ReshitScheduler.AddTeacherForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel = "stylesheet" href = "css/Teacher.css" />
    <title></title>
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="LabelName" runat="server" Text="Label">שם:</asp:Label><asp:TextBox ID="TeacherName" runat="server"></asp:TextBox><br /><br />
        <asp:Label ID="LabelLastName" runat="server" Text="Label">שם משפחה:</asp:Label><asp:TextBox ID="TeacherLastName" runat="server"></asp:TextBox><br /><br />
        <asp:Label ID="LabelPos" runat="server" Text="Label">תפקיד:</asp:Label><asp:DropDownList ID="JobDescription" runat="server"></asp:DropDownList><br /><br />
        <asp:Label ID="LabelUser" runat="server" Text="Label">שם משתמש:</asp:Label><asp:TextBox ID="UserName" runat="server"></asp:TextBox><br /><br />
        <asp:Label ID="LabelPass" runat="server" Text="Label">סיסמה:</asp:Label><asp:TextBox ID="Password" runat="server"></asp:TextBox><br /><br />
        <asp:Label ID="LabelYear" runat="server" Text="Label">שנה:</asp:Label><asp:DropDownList ID="JoinYear" runat="server"></asp:DropDownList><br /><br />
        <asp:Button ID="Save" runat="server" OnClick ="SaveClick" Text="שמור" /><br />
    </div>
    </form>
</body>
</html>
