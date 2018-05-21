﻿using Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class AddCourseForm : BasePage
    {
        private bool IsGroup;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["IsGroup"] == null)
            {
                Session["IsGroup"] = IsGroup = true;
            }
            if (Session["IsGroup"].ToString().Equals("true"))
            {
                IsGroup = true;
                Course.Text = "שם הקבוצה:";
            }
            else
            {
                IsGroup = false;
            }
            if (!IsPostBack)
            {
                DataTable dtTeacherTable = DBConnection.Instance.GetThisYearTeachers();

                ddlTeachers.DataSource = dtTeacherTable;
                ddlTeachers.DataValueField = "id";
                ddlTeachers.DataTextField = "name";
                ddlTeachers.AutoPostBack = true;
                ddlTeachers.DataBind();

               
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string values = "'" + CourseName.Text + "' ,"
                            + ddlTeachers.SelectedValue ;
            string fields = "";
            string tableName = "";
            if (IsGroup)
            {
                tableName = "groups";
                fields = "group_name,teacher_id";
            }
            else
            {
                fields = "course_name,teacher_id";
                tableName = "courses";
            }
            bool bInsertSucceeded = DBConnection.Instance.InsertTableRow(tableName, fields, values);
            if (!bInsertSucceeded)
            {
                Helper.ShowMessage(ClientScript,  "error saving lesson information");
            }
            CourseName.Text = "";
            ddlTeachers.SelectedIndex = 0;
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}