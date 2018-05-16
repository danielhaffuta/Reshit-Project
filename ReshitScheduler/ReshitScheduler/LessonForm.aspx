<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LessonForm.aspx.cs" Inherits="ReshitScheduler.LessonForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    
    <asp:Panel runat="server" ID="pnlClassesPanel">
       
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlStudents" CssClass="row justify-content-center mt-3">
        <div class="col text-center" >
            <asp:Panel runat="server" CssClass="border" ID="pnlNoStudentsMsg"><h2>אין תלמידים בכיתה</h2></asp:Panel>
            <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns ="false" CssClass="table table-striped table-bordered">
                <Columns>
                    <asp:BoundField DataField="student_id" Visible="true">
                    </asp:BoundField>
                    <asp:BoundField DataField="evaluation_id" Visible="true">
                    </asp:BoundField>
                    <asp:BoundField DataField="student_name" HeaderText="שם תלמיד">
                    <HeaderStyle Font-Bold="True" />
                    </asp:BoundField>
                    <asp:ImageField DataImageUrlField="picture_path" HeaderText="תמונה">
                        <ControlStyle Width="100px" />
                    </asp:ImageField>
                    <asp:TemplateField HeaderText="הערכה">
                       <ItemTemplate>
                        <asp:TextBox ID="txtEvaluation" TextMode="MultiLine" Columns="25" 
                            rows="5" runat="server" CssClass="form-control" AutoPostBack ="true" OnTextChanged="txtEvaluation_TextChanged" 
                            ></asp:TextBox>
                       </ItemTemplate>
                      </asp:TemplateField>

                        
                </Columns>
            </asp:GridView>
        </div>

    </asp:Panel>


    <div class="form-row justify-content-center">
        <button  runat="server"  onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
    </div>

</asp:Content>
