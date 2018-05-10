<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetBellSystem.aspx.cs" Inherits="ReshitScheduler.SetBellSystem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel = "stylesheet" href = "/css/BellSystem.css" />
    <title></title>
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="BellTable" runat="server">
        <asp:TableRow><asp:TableCell><asp:Label ID="First" runat="server" Text="Label">שעה ראשונה</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="Start1" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="End1" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Second" runat="server" Text="Label">שעה שניה</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="Start2" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="End2" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="BF" runat="server" Text="Label">ארוחת בוקר</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="StartBF" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="EndBF" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Third" runat="server" Text="Label">שעה שלישית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="Start3" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="End3" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="FirstBrake" runat="server" Text="Label">הפסקה</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="StartFBR" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="EndFBR" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Forth" runat="server" Text="Label">שעה רביעית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="Start4" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="End4" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Fifth" runat="server" Text="Label">שעה חמישית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="Start5" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="End5" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="SecondBrake" runat="server" Text="Label">הפסקה</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="StartSBR" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="EndSBR" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Sixth" runat="server" Text="Label">שעה שישית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="Start6" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="End6" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="LanchBrake" runat="server" Text="Label">ארוחת צהריים</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="StartLB" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="EndLB" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Seventh" runat="server" Text="Label">שעה שביעית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="Start7" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="End7" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Eighth" runat="server" Text="Label">שעה שמינית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="Start8" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="End8" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="ThirdBrake" runat="server" Text="Label">הפסקה</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="StartTBR" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="EndTBR" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Ninth" runat="server" Text="Label">שעה תשיעית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="Start9" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="End9" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell><asp:Label ID="Tenth" runat="server" Text="Label">שעה עשירית</asp:Label></asp:TableCell><asp:TableCell>התחלה:<asp:TextBox ID="Start10" runat="server" type="time"></asp:TextBox></asp:TableCell><asp:TableCell>סיום:<asp:TextBox ID="End10" runat="server" type="time"></asp:TextBox></asp:TableCell></asp:TableRow>
        </asp:Table><br /><br />
        <asp:Label ID="LabelYear" runat="server" Text="Label">שנה:</asp:Label><asp:DropDownList ID="JoinYear" runat="server"></asp:DropDownList><br /><br />
        <asp:Button ID="Save" runat="server" OnClick ="SaveClick" Text="שמור" /><br /><br />
        <asp:Button ID="Back" runat="server" OnClick ="BackClick" Text="חזרה" />
    </div>
    </form>
</body>
</html>
