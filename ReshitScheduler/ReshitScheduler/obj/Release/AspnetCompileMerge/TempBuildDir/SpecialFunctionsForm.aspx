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
        <asp:Button ID="btnConfirm" CssClass="btn btn-outline-dark" runat="server" OnClick = "BtnIncreaseYear" Text = "מעבר שנה" OnClientClick = "Confirm()"/>
        <button  runat="server"  onserverclick="BtnChangeSemester_Click" class="btn btn-outline-dark">מעבר מחצית</button>
    </div>
    <div class="form-check form-check-inline">
            <label class="form-check-label">
                <asp:RadioButton ID="FirstSemester" runat="server" Text="מחצית ראשונה" CssClass="form-check-input" 
                    GroupName="ChooseSemester"/>
            </label>
            <label class="form-check-label">
                <asp:RadioButton ID="SecondSemester" runat="server" Text="מחצית שניה" CssClass="form-check-input" 
                    GroupName="ChooseSemester" Checked ="true" />
            </label>
        </div>
    <%--<div class="row justify-content-center btn-group-vertical">
        
        
    </div>--%>
</asp:Content>
