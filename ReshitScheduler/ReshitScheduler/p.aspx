<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p.aspx.cs" Inherits="ReshitScheduler.p" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ListBox runat="server" ID="lbPrioritization"></asp:ListBox>
        <asp:Button Text="up" runat="server" id="btnUp" OnClick="btnUp_Click"/>
        <asp:Button Text="down" runat="server" id="btnDown" OnClick="btnDown_Click"/>
        <asp:Button Text="save" runat="server" ID="Save" OnClick="Save_Click" />
        
    </div>
    </form>
</body>
</html>
