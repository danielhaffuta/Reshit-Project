<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvaluationForm.aspx.cs" Inherits="ReshitScheduler.EvaluationForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel = "stylesheet" href = "css/Evaluation.css" />
    <title></title>
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="EvalLabel" runat="server" Text="Label">הערכה:</asp:Label><textarea id="Evaluation" cols="50" rows="10"></textarea><br /><br />
        <asp:Label ID="LabelYear" runat="server" Text="Label">שנה:</asp:Label><asp:DropDownList ID="JoinYear" runat="server"></asp:DropDownList><br /><br />
        <asp:Button ID="Save" runat="server" Text="שמור" />  <br />
    </div>
    </form>
</body>
</html>