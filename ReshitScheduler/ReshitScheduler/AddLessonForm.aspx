<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddLessonForm.aspx.cs" Inherits="ReshitScheduler.AddCourseForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel = "stylesheet" href = "/css/Course.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <div class="row justify-content-center mt-3 ">
        <div class="col col-sm-6  border mb-2 pl-4" >
            <h2 id="AddLesson" runat="server">הוספת שיעור חדש:</h2>
            <div class="form-group form-inline row">
                <asp:Label ID="Course" runat="server" Text="שם השיעור:" CssClass="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:TextBox ID="CourseName" runat="server"
                    CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox><br /><br />
            </div>
            <div class="form-group form-inline row">
                <asp:Label ID="Teacher" runat="server" Text="שם המורה:" CssClass="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:DropDownList ID="ddlTeachers" runat="server" CssClass="form-control col col-sm-9 col-md-8"></asp:DropDownList><br /><br />
            </div>
            <%if (IsGroup)
                { %>
            <div class="form-group form-inline row">
                <asp:Label ID="Goal" runat="server" Text="מטרת הקבוצה:" CssClass="col-form-label col-sm-3 col-md-4"></asp:Label>
                    <asp:TextBox ID="GroupGoal" runat="server"  MaxLength ="40"
                        CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox><br /><br />
            </div>
            <%} else
                { %>
            <div class="form-group  row">
                <asp:Label ID="CheckGroup" runat="server" Text="האם השיעור הוא גם קבוצה?"
                    CssClass="col-form-label col-sm-3 col-md-4"></asp:Label>
                <div class="form-check form-check-inline">
                    <label class="form-check-label">
                        <asp:RadioButton ID="IsAlsoGroup" runat="server" Text="כן" CssClass="form-check-input" GroupName="IfAlsoGroup"  OnCheckedChanged="Yes_changed"/>
                    </label>
                    <label class="form-check-label">
                        <asp:RadioButton ID="NotAlsoGroup" runat="server" Text="לא" CssClass="form-check-input" GroupName="IfAlsoGroup" OnCheckedChanged="No_changed" />
                    </label>
                </div>
            </div>
            <%} %>
            <div class="row justify-content-center btn-group-vertical">
                <button  runat="server"  onserverclick="BtnSave_Click" class="btn btn-outline-dark">שמור</button>
                <button  runat="server"  onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
            </div>
        </div>
    </div>

    <asp:Panel runat="server" ID="pnlCourses" CssClass="row justify-content-center mt-3">
        <div class="col-12 col-sm-6 text-center" >
            <asp:GridView ID="gvLessons" runat="server" AutoGenerateColumns ="False" 
                CssClass="table table-striped table-bordered table-sm"
                >
                <Columns >
                    <asp:BoundField DataField="name" HeaderText="שם השיעור">
                        <HeaderStyle Font-Bold="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="teacher_name" HeaderText="מורה">
                        <HeaderStyle Font-Bold="True" />
                    </asp:BoundField>

                </Columns>
            </asp:GridView>
        </div>

    </asp:Panel>

</asp:Content>
