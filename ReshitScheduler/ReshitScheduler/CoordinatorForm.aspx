<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CoordinatorForm.aspx.cs" Inherits="ReshitScheduler.CoordinatorForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("האם אתה בטוח שברצונך לעבור לשנה הבאה? \n לא ניתן לבטל את הפעולה!")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <h2 > <asp:Literal ID="hTeacherName" runat="server" /></h2>

    <asp:Panel ID="pnlClasses" runat="server" CssClass="list-group">
        <h3>הכיתות שלך</h3>

    </asp:Panel>
    <asp:Panel ID="pnlGroups" runat="server" CssClass="list-group">
        <h3>הקבוצות שלך</h3>

    </asp:Panel>
     <div class="form-row justify-content-center mt-3">
        <button  runat="server"  onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
        <button  runat="server"  onserverclick="BtnLogout_Click" class="btn btn-outline-dark">התנתק</button>
        <asp:Button ID="btnConfirm" CssClass="btn btn-outline-dark" runat="server" OnClick = "BtnIncreaseYear" Text = "מעבר שנה" OnClientClick = "Confirm()"/>
    </div>
</asp:Content>


