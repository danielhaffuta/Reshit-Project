<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="ReshitScheduler.LoginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link rel = "stylesheet" href = "css/login.css" />
    <script src="js/jquery.min.js"></script>
    <link href="css/page.css" rel="stylesheet" />
    <title>Reshit Scheduler</title>

</head>
<body>
       
         <header>
          
              <img src="./media/reshitLogo.gif" alt="Alternate Text" id="logo"/>
              <span id="title"> 
                  <b>מערכת שעות - בית הספר ראשית</b>
                    <hr />
               </span>
            
               
        </header>
        <!-- Slideshow container -->
        <div class="slideshow-container">

          <!-- Full-width images with number and caption text -->
          <div class="mySlides fade">
            <div class="numbertext">1 /2</div>
            <img src="./media/1.jpg" style="width:100%; height:100%;" />
            <div class="text">ילדות בדשא</div>
          </div>

          <div class="mySlides fade">
            <div class="numbertext">2 / 2</div>
            <img src="./media/2.jpg" style="width:100%; height:100%;"/>
            <div class="text">השביל הזה מתחיל כאן</div>
          </div>
          <!-- Next and previous buttons -->
     <%--     <a class="next" onclick="plusSlides(-1)">&#10094;</a>
          <a class="prev" onclick="plusSlides(1)">&#10095;</a>--%>
        </div>
        <br/>

        <!-- The dots/circles -->
   <%--     <div style="text-align:center">
          <span class="dot" onclick="currentSlide(1)"></span> 
          <span class="dot" onclick="currentSlide(2)"></span> 
        </div>--%>
     
        <form id="form1" runat="server">
      
            <!--User Name:<input id="Text1" type="text" /><br />
            Password:<input id="Password1" type="password" />-->
        
    
              <asp:TextBox ID="Username" runat="server" placeholder="שם משתמש"></asp:TextBox>
              <asp:TextBox ID="Password" runat="server" placeholder="סיסמא" TextMode="Password"></asp:TextBox>
              <asp:Button ID="Button1" CssClass="button" runat="server" OnClick="Button1_Click" Text="התחבר" />
       
    </form>
    <footer>
        <p>
           <img src="./media/arr_logo.png" alt="Alternate Text" />  <b>אתר זה פותח ועוצב ע"י חפוטה ושותפיו</b>
        </p>
    </footer>
     
</body>
</html>

  <script type="text/javascript" src="js/login.js"></script>