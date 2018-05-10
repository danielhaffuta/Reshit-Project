<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CoordinatorForm.aspx.cs" Inherits="ReshitScheduler.CoordinatorForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <meta http-equiv="x-ua-compatible" content="ie=edge"/>
    
    <link rel="stylesheet" href="/css/bootstrap.min.css"/>
    <title></title>
</head>
<body dir="rtl">
    <div class="container text-right">
        <form id="form1" runat="server">
            <h2 > <asp:Literal ID="hTeacherName" runat="server" /></h2>

            <h3>הכיתות שלך</h3>
            <ol id="olClasses" runat="server">

            </ol>
        </form>
    </div>

    <script src="/js/jquery.slim.min.js"></script>
    <script src="/js/popper.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>
</body>
</html>
