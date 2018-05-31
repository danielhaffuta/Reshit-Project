<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ClassPage.aspx.cs" Inherits="ReshitScheduler.ClassPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <h2><%=strClassName %></h2>
    <div class="row mt-5">
        <asp:Panel runat="server" ID="pnlSchedule" CssClass="table-responsive col-12">
    
        </asp:Panel>
    </div>
    
    
    <script src="/js/general.js"></script>

    <!--Reload on get focus-->
    <script>
        var blurred = false;
        window.onblur = function () { blurred = true; };
        window.onfocus = function () { blurred && (location.reload()); };
    </script>
</asp:Content>

