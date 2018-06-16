<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddTeacherForm.aspx.cs" Inherits="ReshitScheduler.AddTeacherForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel = "stylesheet" href = "/css/Teacher.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <div class="row justify-content-center mt-3 ">
        <div class="col col-sm-6  border mb-2 pl-4" >
            <h2>הוספת מורה חדש</h2>

            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">שם פרטי:</label>
                <asp:TextBox id="txtTeacherFirstName" runat="server" CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox>
            </div>
            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">שם משפחה:</label>
                <asp:TextBox id="txtTeacherLastName" runat="server" CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox>
            </div>
            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">תפקיד:</label>
                <asp:DropDownList ID="ddlTeacherType" runat="server"></asp:DropDownList>
            </div>
            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">שם משתמש:</label>
                <asp:TextBox id="txtUserName" runat="server" CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox>
            </div>
            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">סיסמה:</label>
                <asp:TextBox id="txtPassword" runat="server" CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox>
            </div>


            <div class="form-row justify-content-center btn-group-vertical">
                <button  runat="server" onserverclick="BtnAddTeacher_Click" class="btn btn-outline-dark">הוסף מורה</button>
            </div>
         </div>
    </div>



</asp:Content>

