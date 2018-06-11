<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditLessonDetails.aspx.cs" Inherits="ReshitScheduler.EditLessonDetails" %>

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
            <h2 id="LessonEdit" runat="server">עריכת פרטי שיעור </h2>


            <div class="form-group form-inline row">
                <asp:Label ID="LessonSelection" runat="server" Text="בחר שיעור" class="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlLessons" CssClass="form-control col col-sm-9 col-md-8" 
                    AutoPostBack="true" OnSelectedIndexChanged="ddlLessons_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <div class="form-group form-inline row">
                <asp:Label ID="Course" runat="server" Text="שם השיעור:" class="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:TextBox ID="CourseName" runat="server" Type="text"
                    CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox><br /><br />
            </div>
            <div class="form-group form-inline row">
                <asp:Label ID="TeacherName" runat="server" Text="שם המורה:" class="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:DropDownList ID="ddlTeachers" runat="server" CssClass="form-control col col-sm-9 col-md-8" ></asp:DropDownList>
            
            </div>
            <div class="form-group  row">
                <asp:Label ID="CheckGroup" runat="server" Text="האם זה שיעור עם הערכה?"
                    CssClass="col-form-label col-sm-3 col-md-4"></asp:Label>
                <div class="form-check form-check-inline">
                    <label class="form-check-label">
                        <asp:RadioButton ID="HasEvaluation" runat="server" Text="כן" CssClass="form-check-input" 
                            GroupName="IfHasEvaluation"/>
                    </label>
                    <label class="form-check-label">
                        <asp:RadioButton ID="NotHaveEvaluation" runat="server" Text="לא" CssClass="form-check-input"
                             GroupName="IfHasEvaluation"/>
                    </label>
                </div>
            </div>
            <div class="form-row justify-content-center btn-group-vertical">
                <button id="BtnUpdateLesson" runat="server" onserverclick="BtnUpdateLesson_Click" class="btn btn-outline-dark">שמור</button>
                <asp:Button ID="btnDelete" CssClass="btn btn-outline-dark" runat="server" OnClick = "BtnDeleteLesson" Text = "מחק שיעור" OnClientClick = "Confirm()"/>
            </div>
        </div>
    </div>
         
</asp:Content>
