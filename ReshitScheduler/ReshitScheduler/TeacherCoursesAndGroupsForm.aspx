<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TeacherCoursesAndGroupsForm.aspx.cs" Inherits="ReshitScheduler.TeacherCoursesAndGroupsForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
        <asp:Panel runat="server" CssClass="border" ID="pnlNoLessonsMsg"><h2>אינך מלמד אף שיעור</h2></asp:Panel>
        <div class="row justify-content-center" >
            <div class="col-6 col-sm-3"  runat="server" id="divCourses">
                <div class="h4">הקורסים שלך:</div>
                <asp:Panel runat="server" ID="pnlCourses" CssClass="btn-group-vertical"> 

                </asp:Panel>
            </div>
            <div class="col-6 col-sm-3" runat="server" id ="divGroups">
                <div class="h4">הקבוצות שלך:</div>
            
                <asp:Panel runat="server" ID="pnlGroups" CssClass="btn-group-vertical"> 
                </asp:Panel>
                <br />
            </div>
        </div>
</asp:Content>
