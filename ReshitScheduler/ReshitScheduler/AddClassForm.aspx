<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddClassForm.aspx.cs" Inherits="ReshitScheduler.AddClassForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Grade" runat="server" Text="Label">שכבה:</asp:Label><asp:DropDownList ID="GradeList" runat="server"></asp:DropDownList><br />
        <asp:Label ID="NumberOfClass" runat="server" Text="Label">מספר כיתה:</asp:Label><input id="ClassNum" type="number" /><br />
        <asp:Label ID="Educator" runat="server" Text="Label">שם המחנך:</asp:Label><asp:DropDownList ID="EducatorsList" runat="server"></asp:DropDownList><br />
    </div>
    </form>
</body>
</html>
