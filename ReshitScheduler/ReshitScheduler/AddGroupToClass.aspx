<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddGroupToClass.aspx.cs" Inherits="ReshitScheduler.AddGroupToClass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/css/AddGroupToClass.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <div class="form">
        <div class="form-row justify-content-around">
            <div class="col-12 col-sm-8 col-md-6 col-lg-4 col-xl-3 mb-2">
                <asp:DropDownList CssClass="form-control" ID="GroupsList" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="form-row" >
            <asp:Panel  runat="server" ID="StudentsCol" >
            </asp:Panel>
            </div>
        <div>
            הכנס את מטרת הקבוצה:<input type="text" name="groupPurpose" placeholder="שדה זה אינו חובה" runat="server" id="groupPurpose" />
        </div>
        
        <div class="form-row justify-content-center">
            <button  runat="server"  onserverclick="btnSave_Click" class="btn btn-outline-dark">שמור</button>
            <button  runat="server"  onserverclick="btnCancel_Click" class="btn btn-outline-dark">בטל</button>
        </div>
    </div>
</asp:Content>

