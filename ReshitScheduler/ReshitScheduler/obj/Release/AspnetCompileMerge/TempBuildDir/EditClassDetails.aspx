<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditClassDetails.aspx.cs" Inherits="ReshitScheduler.EditClassDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("האם אתה בטוח שברצונך למחוק כיתה זאת? \n לא ניתן לבטל את הפעולה!")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <div class="row justify-content-center mt-3 ">
        <div class="col col-sm-6  border mb-2 pl-4" >
            <h2>עריכת פרטי כיתה </h2>


            <div class="form-group form-inline row">
                <asp:Label ID="ClassSelection" runat="server" Text="בחר כיתה" class="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlClasses" CssClass="form-control col col-sm-9 col-md-8" 
                    AutoPostBack="true" OnSelectedIndexChanged="ddlClasses_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <div class="form-group form-inline row">
                <asp:Label ID="Grade" runat="server" Text="שכבה:" class="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlGrades" CssClass="form-control col col-sm-9 col-md-8"></asp:DropDownList>
            </div>
            <div class="form-group form-inline row">
                <asp:Label ID="Number" runat="server" Text="מספר כיתה:" class="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:TextBox id="ClassNum" TextMode="Number"  min="1" max="10" step="1"  runat="server" 
                             CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox>
            </div>
            <div class="form-group form-inline row">
                <asp:Label ID="Educator" runat="server" Text="מחנך:" class="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:DropDownList ID="ddlTeachers" runat="server" CssClass="form-control col col-sm-9 col-md-8" 
                    OnSelectedIndexChanged ="ddlTeachers_SelectedIndexChanged" AutoPostBack ="true"></asp:DropDownList>
            
            </div>
            <asp:Panel runat="server" ID="pnlChangeEducator" CssClass="table-responsive col-12" >
                <div class="form-group  row">
                    <asp:Label ID="ChangeLessons" runat="server" Text="האם להעביר למורה זה גם את השיעורים והקבוצות?"
                        CssClass="col-form-label col-sm-3 col-md-4"></asp:Label>
                    <div class="form-check form-check-inline">
                        <label class="form-check-label">
                            <asp:RadioButton ID="Change" runat="server" Text="כן" CssClass="form-check-input"
                                GroupName="IfChangeLessons" Checked="true"/>
                        </label>
                        <label class="form-check-label">
                            <asp:RadioButton ID="DontChange" runat="server" Text="לא" CssClass="form-check-input"
                                GroupName="IfChangeLessons" />
                        </label>
                    </div>
                </div>
            </asp:Panel>
            <div class="form-row justify-content-center btn-group-vertical">
                <button id="BtnUpdateClass" runat="server" onserverclick="BtnUpdateClass_Click" class="btn btn-outline-dark">שמור</button>
                <asp:Button ID="btnDelete" CssClass="btn btn-outline-dark" runat="server" OnClick = "BtnDeleteClass" Text = "מחק כיתה" OnClientClick = "Confirm()"/>
            </div>
        </div>
    </div>
         
</asp:Content>
