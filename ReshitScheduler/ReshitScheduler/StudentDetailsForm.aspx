<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDetailsForm.aspx.cs" Inherits="ReshitScheduler.StudentDetailsForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta charset="UTF-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <meta http-equiv="x-ua-compatible" content="ie=edge"/>
    
    <link rel="stylesheet" href="/css/bootstrap.min.css"/>
    <title></title>
</head>
<body dir="rtl">
    <form id="form1" runat="server" class="container text-center">
        <img src="<%=strPicturePath %>" width="400"/><br />
        <h1><%=strStudentName %></h1>
        <h2><%=strClass %><br /></h2>
        
        <div class="form-row justify-content-center">
                <button  runat="server"  onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
            </div>
    </form>
    <script src="/js/jquery.slim.min.js"></script>
    <script src="/js/popper.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>
</body>
</html>
