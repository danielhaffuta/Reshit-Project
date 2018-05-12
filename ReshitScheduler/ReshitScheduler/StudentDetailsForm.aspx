<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StudentDetailsForm.aspx.cs" Inherits="ReshitScheduler.StudentDetailsForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <img src="<%=drStudentDetails["picture_path"] %>" width="350"/><br />
    <h1><%=drStudentDetails["name"]%></h1>
    <h2><%=drStudentDetails["class"] %><br /></h2>
        
    <div class="row justify-content-center">
        <div class="col-12 col-lg-8">
            <div class="row h4">
                <div class="col-6 border rounded">
                    שם האם : <%= drStudentDetails["mother_full_name"]%>
                </div>
                <div class="col-6 border rounded">
                    שם האב : <%= drStudentDetails["father_full_name"]%>
                </div>
                <div class="col-6 border rounded">
                    נייד אם : <%= drStudentDetails["mother_cellphone"]%>
                </div>
                <div class="col-6 border rounded">
                    נייד אב : <%= drStudentDetails["father_cellphone"]%>
                </div>
                <div class="col-6 border rounded">
                    טלפון : <%= drStudentDetails["home_phone"]%>
                </div>
                <div class="col-6 border rounded">
                   אי-מייל : <%= drStudentDetails["parents_email"]%>
                </div>
                <div class="col-6 border rounded">
                    יישוב : <%= drStudentDetails["settlement"]%>
                </div>
                
            </div>
        </div>
    </div>

    <asp:Panel runat="server" ID="pnlSchedule" >

    </asp:Panel>

    <div class="form-row justify-content-center">
        <button  runat="server"  onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
    </div>
    <script src="/js/general.js"></script>
</asp:Content>

