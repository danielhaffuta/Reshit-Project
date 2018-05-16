﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PrintScheduleForm.aspx.cs" Inherits="ReshitScheduler.PrintScheduleForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        @page 
        {
            margin: 0;  /* this affects the margin in the printer settings */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server" >
    <h2 ><%=strTitle %></h2>
    <asp:Panel runat="server" ID="pnlSchedule" >
    </asp:Panel>
    <div class="form-row justify-content-center">
        <button  runat="server" onserverclick="BtnBack_Click" class="btn btn-outline-dark d-print-none">חזור</button>
    </div>
</asp:Content>