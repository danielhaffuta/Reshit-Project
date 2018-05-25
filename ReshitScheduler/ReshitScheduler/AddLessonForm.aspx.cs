﻿
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
        protected bool IsGroup = false;
        private DataTable dtCourses;
        private DataTable dtGroups;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["IsGroup"] == null || Request.QueryString["IsGroup"].Equals("true"))
            {
                IsGroup = true;
                AddLesson.InnerText = "הוספת קבוצה חדשה:";
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

                FillLessons();
            }
        }

        private void FillLessons()
        {
            if (IsGroup)
            {
                dtGroups = DBConnection.Instance.GetThisYearGroups();
                gvLessons.DataSource = dtGroups;
            }
            else
            {
                dtCourses = DBConnection.Instance.GetThisYearCourses();
                gvLessons.DataSource = dtCourses;
            }
            gvLessons.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string values = "'" + CourseName.Text + "',"
                            + ddlTeachers.SelectedValue ;
            string fields = "";
            string tableName = "";
            if (IsGroup)
            {
                tableName = "groups";
                fields = "group_name,teacher_id,group_goal";
                values += ",'" + GroupGoal.Text + "'";
            }
            else
            {
                fields = "course_name,teacher_id,also_group";
                tableName = "courses";
                if(Yes.Checked)
                {
                    values += ",'1'";
                }
                if(No.Checked)
                {
                    values += ",'0'";
                }
            }
            bool bInsertSucceeded = DBConnection.Instance.InsertTableRow(tableName, fields, values);
            if (!bInsertSucceeded)
            {
                Helper.ShowMessage(ClientScript,  "error saving lesson information");
            }
            CourseName.Text = "";
            ddlTeachers.SelectedIndex = 0;
            GroupGoal.Text = "";
            FillLessons();
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}