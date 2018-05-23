<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddLessonForm.aspx.cs" Inherits="ReshitScheduler.AddCourseForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel = "stylesheet" href = "/css/Course.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <asp:Panel runat="server" ID="pnlCourses" CssClass="row justify-content-center mt-3">
        <div class="col-12 col-sm-6 text-center" >
            <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns ="False" CssClass="table table-striped table-bordered table-sm">
                <Columns>
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
    <div class="row justify-content-center mt-3 ">
        <div class="col col-sm-6  border mb-2 pl-4" >
            <h2>הוספת שיעור חדש:</h2>
            <div class="form-group form-inline row">
                <asp:Label ID="Course" runat="server" Text="שם השיעור:" class="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:TextBox ID="CourseName" runat="server" Type="text"
                    CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox><br /><br />
            </div>
            <div class="form-group form-inline row">
                <asp:Label ID="Teacher" runat="server" Text="שם המורה:" class="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:DropDownList ID="ddlTeachers" runat="server" CssClass="form-control col col-sm-9 col-md-8"></asp:DropDownList><br /><br />
            </div>
            <div class="row justify-content-center btn-group-vertical">
                <button  runat="server"  onserverclick="BtnSave_Click" class="btn btn-outline-dark">שמור</button>
                <button  runat="server"  onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
            </div>
        </div>
    </div>
</asp:Content>
