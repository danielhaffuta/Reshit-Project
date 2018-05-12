<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TeacherCoursesAndGroupsForm.aspx.cs" Inherits="ReshitScheduler.TeacherCoursesAndGroupsForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <h1><%=drTeacherDetails["name"] %></h1>
        <div class="row justify-content-center" >
            <div class="col-6 col-sm-3">
                <div class="h4">הקורסים שלך:</div>
                <asp:Panel runat="server" ID="pnlCourses" CssClass="btn-group-vertical"> 

                </asp:Panel>
            </div>
            <div class="col-6 col-sm-3">
                <div class="h4">הקבוצות שלך:</div>
            
                <asp:Panel runat="server" ID="pnlGroups" CssClass="btn-group-vertical"> 
                </asp:Panel>
                <br />
            </div>
        </div>
        <div class="form-row justify-content-center">
            <button  runat="server"  onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
        </div>
</asp:Content>
