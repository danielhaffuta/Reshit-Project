<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditTeacherDetails.aspx.cs" Inherits="ReshitScheduler.EditTeacherDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <div class="row justify-content-center mt-3 ">
        <div class="col col-sm-6  border mb-2 pl-4" >
            <h2>עריכת פרטי מורה </h2>


            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">בחר מורה</label>
                <asp:DropDownList runat="server" ID="ddlTeachers" CssClass="form-control col col-sm-9 col-md-8" 
                    AutoPostBack="true" OnSelectedIndexChanged="ddlTeachers_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

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
                <asp:DropDownList ID="ddlTeacherTypes" runat="server" CssClass="form-control col col-sm-9 col-md-8" ></asp:DropDownList>
            </div>
            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">שם משתמש:</label>
                <asp:TextBox id="txtUserName" runat="server" CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox>
            </div>
            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">סיסמה:</label>
                <asp:TextBox id="txtPassword" runat="server" CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox>
            </div>

            <asp:Panel runat="server" ID="pnlClasses" CssClass="row mx-3">
                <h2 class="col-12">הרשאות לכיתות</h2>


            </asp:Panel>


            <div class="form-row justify-content-center btn-group-vertical">
                <button id="BtnUpdateTeacher" runat="server" onserverclick="BtnUpdateTeacher_Click" class="btn btn-outline-dark">שמור</button>
                <button  runat="server" onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
            </div>
         </div>
    </div>
</asp:Content>
