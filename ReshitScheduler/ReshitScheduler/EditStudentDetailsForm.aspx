<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditStudentDetailsForm.aspx.cs" Inherits="ReshitScheduler.EditStudentDetailsForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="navbar_extra" runat="server">
    <a class="nav-item nav-link" runat="server" onserverclick="BtnSave_Click">שמור</a>
    <a class="nav-item nav-link" runat="server" onserverclick="BtnDelete_Click">מחק תלמיד</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <img class="figure-img img-fluid" src="<%=drStudentDetails["picture_path"] %>" width="350"/><br />
    <h1><%=drStudentDetails["name"]%></h1>
    <h2><%=drStudentDetails["class"] %><br /></h2>
        
    <div class="row justify-content-center">
        <div class="col-12 col-lg-8">
            <div class="row h4">
                <div class="col-12 col-sm-6 border rounded form-group">
                    <label for="mother_full_name" class="form-control-label">שם האם : </label>
                    <input id="mother_full_name" runat="server" type="text" class ="form-control mb-1"/>
                </div>
                <div class="col-12 col-sm-6 border rounded form-group">
                    <label for="father_full_name" class="form-control-label">שם האב : </label>
                    <input id="father_full_name" runat="server" type="text" class ="form-control mb-1"/>
                </div>
                <div class="col-12 col-sm-6 border rounded form-group">
                    <label for="mother_cellphone" class="form-control-label">נייד אם : </label>
                    <input id="mother_cellphone" runat="server" type="text" class ="form-control mb-1"/>
                </div>
                <div class="col-12 col-sm-6 border rounded form-group">
                    <label for="father_cellphone" class="form-control-label">נייד אב : </label>
                    <input id="father_cellphone" runat="server" type="text" class ="form-control mb-1"/>
                </div>
                <div class="col-12 col-sm-6 border rounded form-group">
                    <label for="home_phone" class="form-control-label">טלפון  : </label>
                    <input id="home_phone" runat="server" type="text" class ="form-control mb-1"/>
                </div>
                <div class="col-12 col-sm-6 border rounded form-group">
                    <label for="parents_email" class="form-control-label">אי-מייל : </label>
                    <textarea id="parents_email" runat="server" rows="2"  class ="form-control mb-1"></textarea>
                </div>
                <div class="col-12 col-sm-6 border rounded form-group">
                    <label for="settlement" class="form-control-label">יישוב : </label>
                    <input id="settlement" runat="server" type="text" class ="form-control mb-1"/>
                </div>
                
            </div>
        </div>
    </div>

</asp:Content>
