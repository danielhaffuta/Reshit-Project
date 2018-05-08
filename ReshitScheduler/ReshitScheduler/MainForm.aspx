<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="ReshitScheduler.MainForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <meta http-equiv="x-ua-compatible" content="ie=edge"/>
    
    <link rel="stylesheet" href="css/AddGroupToClass.css"/>
    <link rel="stylesheet" href="css/bootstrap.min.css"/>
    <title></title>
</head>
<body dir = "rtl">
    <form id="form1" runat="server" class="form text-center">
        <asp:Panel runat="server" ID="container" class="container">
                שלום
                <asp:Label CssClass="form-text" ID="TeacherName" runat="server" Text="Label"></asp:Label>
                <br />
                הכיתות שלך:
                <asp:Panel runat="server" CssClass="form-group" ID="ClassesPanel" >

                </asp:Panel>
                <asp:Panel runat="server" CssClass="form-group" ID="editOptionsPanel" >
                    <br />אפשרויות עריכה:<br />
                </asp:Panel>
                <div class="form-row justify-content-center">
                    <button  runat="server"  onserverclick="BtnLogout_Click" class="btn btn-outline-dark">התנתק</button>
                </div>
        </asp:Panel>
    </form>

    <script src="js/jquery.slim.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
