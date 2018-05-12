<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EvaluationForm.aspx.cs" Inherits="ReshitScheduler.EvaluationForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <img src="<%=drStudentDetails["picture_path"] %>" width="350"/><br />
    <h1><%=drStudentDetails["name"]%></h1>
    <h2><%=drStudentDetails["class"] %><br /></h2>
    <h3> הערכה עבור
    <% if (IsGroup){ %> קבוצת <%}else{%> קורס <%} %> <%=strLessonName %>
        </h3>
    <asp:TextBox  runat ="server" ID="txtEvaluation" TextMode="MultiLine"  Columns="50" rows="10"></asp:TextBox>

    <div class="form-row justify-content-center">
        <button  runat="server"  onserverclick="BtnSave_Click" class="btn btn-outline-dark">שמור</button>
        <button  runat="server"  onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
    </div>
</asp:Content>

