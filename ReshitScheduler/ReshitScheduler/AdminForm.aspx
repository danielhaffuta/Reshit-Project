<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminForm.aspx.cs" Inherits="ReshitScheduler.AdminForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel = "stylesheet" href = "css/page.css" />
     <link rel = "stylesheet" href = "css/AdminForm.css" />
    <title>admin page</title>
</head>
<body dir ="rtl">
        


       <header>
          
              <img src="./media/reshitLogo.gif" alt="Alternate Text" id="logo"/>
              <span id="title"> 
                  <b>שלום</b>
                  <br />
                   <asp:Label ID="AdminName" runat="server" Text="Label"></asp:Label>
               </span>
              
            
               
     </header> 
     <form id="form1" runat="server">
            <div dir ="rtl">
    
 
              <asp:Label runat="server" >בחר טבלה לעריכה:</asp:Label>
                 <br />
                  <asp:dropdownlist ID="courseEdit" runat="server" AutoPostBack="True" 
                 onselectedindexchanged="itemSelected">
                </asp:dropdownlist>
            </div>
        </form>
        <footer>
        <p>
           <img src="./media/arr_logo.png" alt="Alternate Text" />  <b>אתר זה פותח ועוצב ע"י חפוטה ושותפיו</b>
        </p>
    </footer>
</body>
</html>
