﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SetBellSystem.aspx.cs" Inherits="ReshitScheduler.SetBellSystem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel = "stylesheet" href = "/css/BellSystem.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainForm" runat="server">
    <asp:Panel runat="server" ID="pnlHours" CssClass="row justify-content-center mt-3">
        <div class="col-12 col-sm-6 text-center" >
            <asp:GridView ID="gvHours" runat="server" AutoGenerateColumns ="False" CssClass="table table-striped table-bordered table-sm">
                <Columns>
                    <asp:TemplateField HeaderText="שעה ביום">
                    <HeaderStyle Font-Bold="True" />
                        <ItemTemplate>
                            <asp:TextBox ID="hour_in_day" runat="server"
                                CssClass="form-control" AutoPostBack ="true"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="שעת התחלה" >
                       <ItemTemplate>
                        <asp:TextBox ID="tblStartTime" runat="server" Type="time" Text='<%# Bind("start_time") %>'
                            CssClass="form-control" AutoPostBack ="true"></asp:TextBox>
                       </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="שעת סיום">
                       <ItemTemplate>
                        <asp:TextBox ID="tblFinishTime" runat="server" Type="time" Text='<%# Bind("finish_time") %>'
                            CssClass="form-control" AutoPostBack ="true"></asp:TextBox>
                       </ItemTemplate>
                     </asp:TemplateField>
                    <asp:BoundField DataField="is_break" HeaderText="הפסקה" Visible="true">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="האם הפסקה?">
                       <ItemTemplate>
                           <asp:CheckBox ID="CheckIfBreak" runat="server" CssClass="form-control" AutoPostBack ="true" Text="כן"/>
                       </ItemTemplate>
                     </asp:TemplateField>
                        
                </Columns>
            </asp:GridView>
             <button  runat="server"  onserverclick="BtnSaveBellSystem_Click" class="btn btn-outline-dark">שמור מערכת צלצולים</button>
        </div>

    </asp:Panel>

    <div class="row justify-content-center mt-3 ">
        <div class="col col-sm-6  border mb-2 pl-4" >
            <h2>הוספת שעה למערכת צלצולים:</h2>
            <div class="form-group form-inline row">
                <asp:Label ID="Start" runat="server" Text="התחלה:" class="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:TextBox ID="StartTime" runat="server" Type="time"
                    CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox><br /><br />
            </div>
            <div class="form-group form-inline row">
                <asp:Label ID="End" runat="server" Text="סיום:" class="col-form-label col-sm-3 col-md-4"></asp:Label>
                <asp:TextBox ID="EndTime" runat="server" Type="time"
                    CssClass="form-control col col-sm-9 col-md-8"></asp:TextBox><br /><br />
            </div>
            <div class="form-group  row">
                <asp:Label ID="CheckGroup" runat="server" Text="האם זאת הפסקה?"
                    CssClass="col-form-label col-sm-3 col-md-4"></asp:Label>
                <div class="form-check form-check-inline">
                    <label class="form-check-label">
                        <asp:RadioButton ID="IsBreak" runat="server" Text="כן" CssClass="form-check-input" GroupName="IfBreak"  OnCheckedChanged="IsBreak_changed"/>
                    </label>
                    <label class="form-check-label">
                        <asp:RadioButton ID="NotBreak" runat="server" Text="לא" CssClass="form-check-input" GroupName="IfBreak" OnCheckedChanged="NotBreak_changed" />
                    </label>
                </div>
            </div>
            <div class="row justify-content-center btn-group-vertical">
                <button  runat="server"  onserverclick="BtnSave_Click" class="btn btn-outline-dark">שמור</button>
                <button  runat="server"  onserverclick="BtnBack_Click" class="btn btn-outline-dark">חזור</button>
            </div>
        </div>
    </div>
</asp:Content>
