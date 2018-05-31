<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditLessonDetails.aspx.cs" Inherits="ReshitScheduler.EditLessonDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <%if (IsGroup)
                { %>
            <div class="form-group form-inline row">
                <asp:Label ID="Goal" runat="server" Text="מטרת הקבוצה:" class="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:TextBox ID="GroupGoal" runat="server" Type="text" MaxLength ="40"
                    CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox><br /><br />
            </div>
            <%} %>
            <%if (!IsGroup)
                { %>
            <div class="form-group form-inline row">
                <asp:Label ID="CheckGroup" runat="server" Text="האם השיעור הוא גם קבוצה?" 
                    class="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:RadioButton ID="Yes" runat="server" Text="כן" GroupName ="IfGroup" />
                <asp:RadioButton ID="No" runat="server" Text="לא" GroupName ="IfGroup" />
            </div>
            <%} %>
            <div class="form-row justify-content-center btn-group-vertical">
                <button id="BtnUpdateLesson" runat="server" onserverclick="BtnUpdateLesson_Click" class="btn btn-outline-dark">שמור</button>
            </div>
        </div>
    </div>
         
</asp:Content>
