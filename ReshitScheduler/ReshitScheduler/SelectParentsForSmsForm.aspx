<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SelectParentsForSmsForm.aspx.cs" Inherits="ReshitScheduler.SelectParentsForSmsForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">

    <fieldset >
        <legend>בחר הורה לשליחת SMS</legend>
        <div class="form-group form-row" runat="server" id="pnlStudents">    
            
        </div>
    </fieldset>
    <div class="form-row justify-content-center">
        <button  runat="server"  onserverclick="btnSend_Click" class="btn btn-outline-dark">שלח</button>
    </div>
    <label id="smsWarning"> שים לב אישור הורים מתקבל רק בכניסה מחדש למערכת</label>
</asp:Content>
