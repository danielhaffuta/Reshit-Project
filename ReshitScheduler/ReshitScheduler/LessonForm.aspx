<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LessonForm.aspx.cs" Inherits="ReshitScheduler.LessonForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel = "stylesheet" href = "/css/LessonForm.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">

    <div class="row justify-content-center mt-5" runat="server" id="divClasses">
        <div class="col col-sm-6" >
            <div class="form-group form-inline row">
                <label id="lblClasses" for="ddlClassesList" class="col-form-label col-sm-3  col-md-4">כיתה:</label>
                <asp:DropDownList ID="ddlClassesList" runat="server" OnSelectedIndexChanged="ddlClassesList_SelectedIndexChanged"   
                     CssClass="form-control col col-sm-9 col-md-8" DataValueField="class_id"
                     DataTextField="class" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
    </div>

    <asp:Panel runat="server" ID="pnlStudents" CssClass="row justify-content-center mt-3">
        <div class="col text-center" >
            <asp:Panel runat="server" CssClass="border" ID="pnlNoStudentsMsg"><h2>אין תלמידים בכיתה</h2></asp:Panel>
            <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns ="false" 
                CssClass="table table-striped table-bordered table-sm">
                <Columns>
                    <asp:BoundField DataField="student_id" Visible="true">
                    </asp:BoundField>
                    <asp:BoundField DataField="evaluation_id" Visible="true">
                    </asp:BoundField>
                    <asp:BoundField DataField="student_name" HeaderText="שם תלמיד"  ItemStyle-Width="10%">
                    <HeaderStyle Font-Bold="True" />
                    </asp:BoundField>
                    <asp:ImageField DataImageUrlField="picture_path" HeaderText="תמונה" ItemStyle-Width="10%" >
                    </asp:ImageField>
                    <asp:TemplateField HeaderText="הערכה" ItemStyle-Width="80%">
                       <ItemTemplate>
                        <asp:TextBox ID="txtEvaluation" TextMode="MultiLine" Columns="25" 
                            rows="3" runat="server" CssClass="form-control" AutoPostBack ="true" OnTextChanged="txtEvaluation_TextChanged" 
                            
                            ></asp:TextBox>
                       </ItemTemplate>
                      </asp:TemplateField>

                        
                </Columns>
            </asp:GridView>
        </div>

    </asp:Panel>


    <div class="form-row justify-content-center">
    </div>

</asp:Content>
