<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SpecialFunctionsForm.aspx.cs" Inherits="ReshitScheduler.SpecialFunctionsForm" %>
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

<asp:Content ID="Content3" ContentPlaceHolderID="navbar_extra" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainForm" runat="server">
    <div class="form-row justify-content-center mt-3">
        <button  runat="server"  onserverclick="BtnIncreaseSemester_Click" class="btn btn-outline-dark">מעבר מחצית</button>
        <asp:Button ID="btnConfirm" CssClass="btn btn-outline-dark" runat="server" OnClick = "BtnIncreaseYear" Text = "מעבר שנה" OnClientClick = "Confirm()"/>
    </div>
</asp:Content>
