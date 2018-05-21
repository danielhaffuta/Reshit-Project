<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddClassForm.aspx.cs" Inherits="ReshitScheduler.AddClassForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel = "stylesheet" href = "/css/Class.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">

    <asp:Panel runat="server" ID="pnlStudents" CssClass="row justify-content-center mt-3">
        <div class="col-12 col-sm-6 text-center" >
            <asp:GridView ID="gvClasses" runat="server" AutoGenerateColumns ="False" CssClass="table table-striped table-bordered table-sm">
                <Columns>
                    <asp:BoundField DataField="name" HeaderText="כיתה">
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
            <h2>הוספת כיתה חדשה</h2>
            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">שכבה:</label>
                <asp:DropDownList ID="ddlGrades" runat="server" CssClass="form-control col col-sm-9 col-md-8"></asp:DropDownList>
            </div>
            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">מספר כיתה:</label>
                <asp:TextBox id="txtStudentFirstName" TextMode="Number"  min="1" max="10" step="1"  runat="server" 
                             CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox>
            </div>
            <div class="form-group form-inline row">
                <label class="col-form-label col-sm-3 col-md-4">מחנך:</label>
                <asp:DropDownList runat="server" ID="ddlTeachers" CssClass="form-control col col-sm-9 col-md-8" >
                </asp:DropDownList>
            </div>
            <div class="form-row justify-content-center btn-group-vertical">
                <button  runat="server" onserverclick="BtnAddClass_Click" class="btn btn-outline-dark">הוסף כיתה</button>
                <button  runat="server" onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
             </div>
        </div>
    </div>
</asp:Content>


