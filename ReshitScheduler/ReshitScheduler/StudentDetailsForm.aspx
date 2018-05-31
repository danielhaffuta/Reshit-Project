<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StudentDetailsForm.aspx.cs" Inherits="ReshitScheduler.StudentDetailsForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="navbar_extra" runat="server">

    <a class="nav-item nav-link" href="EditStudentDetailsForm.aspx?StudentID=<%=nStudentID %>">ערוך פרטי תלמיד</a>
    <div class="dropdown">

        <a class="nav-item nav-link dropdown-toggle "
            data-toggle="dropdown" id="printDropDown"
            aria-haspopup="true" aria-expanded="false"
            href="#">הדפסה</a>
        <div class="dropdown-menu dropdown-menu-right text-right" aria-labelledby="printDropDown">
            <a class="dropdown-item" href="PrintScheduleForm.aspx?StudentID=<%=nStudentID%>">מערכת</a>
            <a class="dropdown-item" href="PrintStudentEvaluations.aspx?StudentID=<%=nStudentID%>">הערכה</a>
        </div>
    </div>
    <a class="nav-item nav-link" href="EditStudentEvaluations.aspx?StudentID=<%=nStudentID %>">צפה בהערכות</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <img class="figure-img img-fluid" src="<%=drStudentDetails["picture_path"] %>" width="350"/><br />
    <h1><%=drStudentDetails["name"]%></h1>
    <h2><%=drStudentDetails["class"] %><br /></h2>
        
    <div class="row justify-content-center">
        <div class="col-12 col-lg-8">
            <div class="row h4">
                <div class="col-12 col-sm-6 border rounded">
                    שם האם : <%= drStudentDetails["mother_full_name"]%>
                </div>
                <div class="col-12 col-sm-6 border rounded">
                    שם האב : <%= drStudentDetails["father_full_name"]%>
                </div>
                <div class="col-12 col-sm-6 border rounded">
                    נייד אם : <%= drStudentDetails["mother_cellphone"]%>
                </div>
                <div class="col-12 col-sm-6 border rounded">
                    נייד אב : <%= drStudentDetails["father_cellphone"]%>
                </div>
                <div class="col-12 col-sm-6 border rounded">
                    טלפון : <%= drStudentDetails["home_phone"]%>
                </div>
                <div class="col-12 col-sm-6 border rounded">
                   אי-מייל : <%= drStudentDetails["parents_email"]%>
                </div>
                <div class="col-12 col-sm-6 border rounded">
                    יישוב : <%= drStudentDetails["settlement"]%>
                </div>
                
            </div>
        </div>
    </div>

    <asp:Panel runat="server" ID="pnlSchedule" >

    </asp:Panel>


    <script src="/js/general.js"></script>
</asp:Content>

