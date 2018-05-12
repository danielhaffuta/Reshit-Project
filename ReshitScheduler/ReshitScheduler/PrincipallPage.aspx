<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrincipallPage.aspx.cs" Inherits="ReshitScheduler.PrincipallPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel = "stylesheet" href = "/css/page.css" />
    <link rel = "stylesheet" href = "/css/AdminForm.css" />
    <script src="/js/jquery.min.js"></script>
    <title>Principal Page</title>
</head>
<body dir="rtl">
    <header>          
        <img src="/media/reshitLogo.gif" alt="Alternate Text" id="logo"/>
        <span id="title"> 
            שלום 
            <asp:Label ID="PrincipalName" runat="server" Text="מנהל\ת"></asp:Label>
        </span>            
    </header> 

    <form id="principalPage" runat="server">
        <asp:Panel runat="server" CssClass="form-group" ID="editOptionsPanel" >
           <br />אפשרויות עריכה:<br />
            <ol id="olClassesSchedule" runat="server">

            </ol>
        </asp:Panel>
    </form>

    <div class="form-row justify-content-center">
      <button  runat="server"  onserverclick="BtnLogout_Click" class="btn btn-outline-dark">התנתק</button>
    </div>
</body>
</html>
