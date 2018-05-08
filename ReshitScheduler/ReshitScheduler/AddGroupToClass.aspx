<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddGroupToClass.aspx.cs" Inherits="ReshitScheduler.AddGroupToClass" %>

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
<body dir="rtl">
    <form id="form1" runat="server" class="form">
        <br />
        <div id="divContainer" class ="container">
            <div class="form-row justify-content-around">
                <div class="col-12 col-sm-8 col-md-6 col-lg-4 col-xl-3 mb-2">
                    <asp:DropDownList CssClass="form-control" ID="GroupsList" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="form-row" >
                <asp:Panel  runat="server" ID="StudentsCol" >
                </asp:Panel>
             </div>
        
            <div class="form-row justify-content-center">
                <button  runat="server"  onserverclick="btnSave_Click" class="btn btn-outline-dark">שמור</button>
                <button  runat="server"  onserverclick="btnCancel_Click" class="btn btn-outline-dark">בטל</button>
            </div>
        </div>
    </form>
    <script src="js/jquery.slim.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
