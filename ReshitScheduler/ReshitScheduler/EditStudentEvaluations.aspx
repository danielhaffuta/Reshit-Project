<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditStudentEvaluations.aspx.cs" Inherits="ReshitScheduler.EditStudentEvaluations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <div class="mt-5">
        <h2 id="name" runat="server" ></h2>
        <asp:Panel runat="server" ID="pnlEvaluations" >
            <div class="col text-center" >
            <asp:GridView ID="gvEvaluations" runat="server" AutoGenerateColumns ="False" 
                CssClass="table table-striped table-bordered table-sm">
                <Columns>
                    <asp:BoundField DataField="lesson_name" HeaderText="שם השיעור" ItemStyle-Width="15%">
                    <HeaderStyle Font-Bold="True" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="הערכה" ItemStyle-Width="85%">
                       <ItemTemplate>
                        <asp:TextBox ID="txtEvaluation" runat="server" Text='<%# Bind("evaluation") %>'
                            TextMode="MultiLine"  Columns="25" rows="3" 
                            CssClass="form-control" AutoPostBack ="true" OnTextChanged="txtEvaluation_TextChanged" 
                            ></asp:TextBox>
                       </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="is_group" HeaderText="קבוצה" >
                    </asp:BoundField>
                    <asp:BoundField DataField="evaluation_id" HeaderText="מספר הערכה">
                    </asp:BoundField>
                    <asp:BoundField DataField="lesson_id" HeaderText="מספר שיעור">
                    </asp:BoundField>
                        
                </Columns>
            </asp:GridView>
        </div>
        </asp:Panel>
        <div class="form-row justify-content-center">
        </div>
    </div>
</asp:Content>
