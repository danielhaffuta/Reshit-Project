<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTeacherForm.aspx.cs" Inherits="ReshitScheduler.AddTeacherForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="LabelName" runat="server" Text="Label">שם:</asp:Label><input id="TeacherName" type="text" /><br />
        <asp:Label ID="LabelLastName" runat="server" Text="Label">שם משפחה:</asp:Label><input id="TeacherLastName" type="text" /><br />
        <asp:Label ID="LabelPos" runat="server" Text="Label">תפקיד:</asp:Label><asp:DropDownList ID="JobDescription" runat="server"></asp:DropDownList><br />
        <asp:Label ID="LabelUser" runat="server" Text="Label">שם משתמש:</asp:Label><input id="UserName" type="text" /><br />
        <asp:Label ID="LabelPass" runat="server" Text="Label">סיסמה:</asp:Label><input id="Password" type="text" /><br />
        <asp:Label ID="LabelYear" runat="server" Text="Label">שנה:</asp:Label><asp:DropDownList ID="JoinYear" runat="server"></asp:DropDownList><br />
        <asp:Button ID="Save" runat="server" Text="שמור" />
    </div>
    </form>
</body>
</html>
