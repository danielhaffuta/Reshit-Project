<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminForm.aspx.cs" Inherits="ReshitScheduler.AdminForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel = "stylesheet" href = "css/login.css" />
     <link rel = "stylesheet" href = "css/AdminForm.css" />
    <title>admin page</title>
</head>
<body dir ="rtl">
    
     <form id="form1" runat="server">
            <div dir ="rtl">
    
                <h1>שלום
                    </h1>
                <asp:Label ID="AdminName" runat="server" Text="Label"></asp:Label>
    
                <br />
              
             
              <asp:Label runat="server" >בחר טבלה לעריכה:</asp:Label>
                 <br />
                  <asp:dropdownlist ID="courseEdit" runat="server" AutoPostBack="True" 
                 onselectedindexchanged="itemSelected">
                </asp:dropdownlist>
            </div>
        </form>
</body>
</html>
