<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetBellSystem.aspx.cs" Inherits="ReshitScheduler.SetBellSystem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel = "stylesheet" href = "css/BellSystem.css" />
    <title></title>
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="Table1" runat="server">
        <asp:TableRow><asp:TableCell><asp:Label ID="First" runat="server" Text="Label">שעה ראשונה</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="Start1" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="End1" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Second" runat="server" Text="Label">שעה שניה</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="Start2" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="End2" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="BF" runat="server" Text="Label">ארוחת בוקר</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="StartBF" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="EndBF" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Third" runat="server" Text="Label">שעה שלישית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="Start3" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="End3" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="FirstBrake" runat="server" Text="Label">הפסקה</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="StartFBR" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="EndFBR" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Forth" runat="server" Text="Label">שעה רביעית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="Start4" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="End4" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Fifth" runat="server" Text="Label">שעה חמישית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="Start5" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="End5" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="SecondBrake" runat="server" Text="Label">הפסקה</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="StartSBR" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="EndSBR" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Sixth" runat="server" Text="Label">שעה שישית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="Start6" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="End6" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="LanchBrake" runat="server" Text="Label">ארוחת צהריים</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="StartLB" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="EndLB" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Seventh" runat="server" Text="Label">שעה שביעית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="Start7" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="End7" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Eighth" runat="server" Text="Label">שעה שמינית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="Start8" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="End8" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="ThirdBrake" runat="server" Text="Label">הפסקה</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="StartTBR" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="EndTBR" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Ninth" runat="server" Text="Label">שעה תשיעית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="Start9" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="End9" type="time" /></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Tenth" runat="server" Text="Label">שעה עשירית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<input id="Start10" type="time" /></asp:TableCell><asp:TableCell>סיום:<input id="End10" type="time" /></asp:TableCell></asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
