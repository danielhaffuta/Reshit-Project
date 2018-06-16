<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Prioritization.aspx.cs" Inherits="ReshitScheduler.prioritization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="navbar" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="navbar_extra" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainForm" runat="server">

    <div>
        <asp:ListBox runat="server" ID="lbPrioritization"></asp:ListBox><br />
        <asp:ImageButton runat="server"   OnClick="btnUp_Click" src="./media/up.png" />
        <asp:ImageButton runat="server"   OnClick="btnDown_Click" src="./media/down.png" />
            <asp:Button Text="שמור" runat="server" ID="Save" OnClick="Save_Click" />

        <%--<i class="far fa-arrow-alt-circle-up" runat="server"  id="Up" onclick="btnUp_Click"></i>--%>
        <%--<asp:Button Text="למעלה" runat="server" id="btnUp" OnClick="btnUp_Click"/>--%>
<%--        <asp:Button Text="down" runat="server" id="btnDown" OnClick="btnDown_Click"/>--%>
        
    </div>
  
</asp:Content>
