<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStudentForm.aspx.cs" Inherits="ReshitScheduler.AddStudentForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel = "stylesheet" href = "css/Student.css" />
    <title></title>
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="NameStudent" runat="server" Text="Label">שם התלמיד:</asp:Label><input id="StudentName" type="text" /><br />
        <asp:Label ID="LastNameStudent" runat="server" Text="Label">שם משפחה:</asp:Label><input id="StudentLastName" type="text" /><br />
        <asp:Label ID="Class" runat="server" Text="Label">כיתה:</asp:Label><asp:DropDownList ID="ClassesList" runat="server"></asp:DropDownList><br />
        <asp:Label ID="Picture" runat="server" Text="Label">תמונה:</asp:Label><input id="StudentPic" type="file" accept="image/*" /><br />
        <asp:Label ID="LabelYear" runat="server" Text="Label">שנה:</asp:Label><asp:DropDownList ID="JoinYear" runat="server"></asp:DropDownList><br />
        <asp:Button ID="Save" runat="server" Text="שמור" />
    </div>
    </form>
</body>
</html>
