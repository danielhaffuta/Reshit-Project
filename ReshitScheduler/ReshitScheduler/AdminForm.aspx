<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminForm.aspx.cs" Inherits="ReshitScheduler.AdminForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel = "stylesheet" href = "css/page.css" />
     <link rel = "stylesheet" href = "css/AdminForm.css" />
     <script src="js/jquery.min.js"></script>
    <title>admin page</title>
   
</head>
<body dir ="rtl">
          
         
       <header>
          
              <img src="./media/reshitLogo.gif" alt="Alternate Text" id="logo"/>
              <span id="title"> 
                    שלום - 
                    <asp:Label ID="AdminName" runat="server" Text="Label"></asp:Label>
                    <button class="hamburger">&#9776;</button>
                   <button class="cross">&#735;</button>
               </span>
              
             
     </header> 

      <div class="menu">
      <ul>
          <a href="#"><li onclick="form1()"> ערוך טבלאות</li></a>
          <a href="#"><li onclick="classShow()">מערכת שעות</li></a>
          <a href="#"><li onclick="disconnect()">התנתק</li></a>
          
      </ul>
    </div> 

     <form id="allOptions" runat="server">
           <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>   
         <div class="options" id="editTables" dir ="rtl">
    
 
              <asp:Label runat="server" >בחר טבלה לעריכה:</asp:Label>
                 <br />
                  <asp:dropdownlist ID="courseEdit" runat="server" AutoPostBack="True" 
                 onselectedindexchanged="itemSelected">
                </asp:dropdownlist>
            </div>
         <div class="options" id="classShow">
             <label>בחר כיתה להצגה</label>

         </div>
     </form>
        <footer>
        <p>
           <img src="./media/arr_logo.png" alt="Alternate Text" />  <b>אתר זה פותח ועוצב ע"י חפוטה ושותפיו</b>
        </p>
    </footer>
</body>
</html>
     <script type="text/javascript" src="js/AdminForm.js"></script>
