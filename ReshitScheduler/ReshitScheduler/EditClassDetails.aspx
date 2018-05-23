<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditClassDetails.aspx.cs" Inherits="ReshitScheduler.EditClassDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <div class="row justify-content-center mt-3 ">
        <div class="col col-sm-6  border mb-2 pl-4" >
            <h2>עריכת פרטי כיתה </h2>


            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">בחר כיתה</label>
                <asp:DropDownList runat="server" ID="ddlClasses" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClasses_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">שכבה:</label>
                <asp:DropDownList runat="server" ID="ddlGrades" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">מספר כיתה:</label>
                <asp:TextBox id="ClassNum" TextMode="Number"  min="1" max="10" step="1"  runat="server" 
                             CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox>
            </div>
            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">מחנך:</label>
                <asp:DropDownList ID="ddlTeachers" runat="server" CssClass="form-control" ></asp:DropDownList>
            
            </div>
            <div class="form-row justify-content-center btn-group-vertical">
                <button id="BtnUpdateClass" runat="server" onserverclick="BtnUpdateClass_Click" class="btn btn-outline-dark">שמור</button>
                <button  runat="server" onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
            </div>
        </div>
    </div>
         
</asp:Content>
