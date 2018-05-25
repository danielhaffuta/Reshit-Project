﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class EditLessonDetails : BasePage
    {
        private DataTable dtCourses;
        private DataTable dtGroups;
        protected bool IsGroup;
        private int nLessonID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["IsGroup"] == null || Request.QueryString["IsGroup"].Equals("true"))
            {
                IsGroup = true;
                LessonEdit.InnerText = "עריכת פרטי קבוצה";
                LessonSelection.Text = "בחר קבוצה";
                Course.Text = "שם הקבוצה:";
            }
            else
            {
                IsGroup = false;
            }
            dtCourses = DBConnection.Instance.GetThisYearCourses();
            dtGroups = DBConnection.Instance.GetThisYearGroups();
            if (!IsPostBack)
            {
                if(IsGroup)
                {
                    ddlLessons.DataSource = dtGroups;
                    ddlLessons.DataValueField = "group_id";
                }
                else
                {
                    ddlLessons.DataSource = dtCourses;
                    ddlLessons.DataValueField = "course_id";
                }
                ddlLessons.DataTextField = "name";
                ddlLessons.AutoPostBack = true;
                ddlLessons.DataBind();

                DataTable dtTeacherTable = DBConnection.Instance.GetThisYearTeachers();

                ddlTeachers.DataSource = dtTeacherTable;
                ddlTeachers.DataValueField = "id";
                ddlTeachers.DataTextField = "name";
                ddlTeachers.AutoPostBack = true;
                ddlTeachers.DataBind();
            }
            nLessonID = Convert.ToInt32(ddlLessons.SelectedValue);
            if (!IsPostBack)
            {
                FillLessonDetails();

            }
        }

        private void FillLessonDetails()
        {
            DataTable dtCourseDetails;
            DataRow drLessonDetails;
            if (IsGroup)
            {
                dtCourseDetails = DBConnection.Instance.GetThisYearLessons("groups");
                drLessonDetails = dtCourseDetails.Select("group_id = " + nLessonID)[0];
                GroupGoal.Text = drLessonDetails["group_goal"].ToString();
            }
            else
            {
                dtCourseDetails = DBConnection.Instance.GetThisYearLessons("courses");
                drLessonDetails = dtCourseDetails.Select("course_id = " + nLessonID)[0];
                if (drLessonDetails["also_group"].ToString().Equals("1"))
                {
                    Yes.Checked = true;
                    No.Checked = false;
                }
                else
                {
                    Yes.Checked = false;
                    No.Checked = true;
                }
            }
            
            CourseName.Text = drLessonDetails["name"].ToString();
            ddlTeachers.SelectedValue = drLessonDetails["teacher_id"].ToString();
        }

        protected void ddlLessons_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLessonDetails();
        }

        protected void BtnUpdateLesson_Click(object sender, EventArgs e)
        {
            string strFields = "";
            string strValues = "'"+ CourseName.Text + "':" +
                               ddlTeachers.SelectedValue;
            string tableName = "";
            if (IsGroup)
            {
                tableName = "groups";
                strFields = "group_name:teacher_id:group_goal";
                strValues += ":'" + GroupGoal.Text + "'";
            }
            else
            {
                tableName = "courses";
                strFields = "course_name:teacher_id:also_group";
                if (Yes.Checked)
                {
                    strValues += ":'1'";
                }
                if (No.Checked)
                {
                    strValues += ":'0'";
                }
            }
            

            bool bUpdateSucceeded = DBConnection.Instance.UpdateTableRow(tableName, nLessonID, strFields, strValues);
            if (!bUpdateSucceeded)
            {
                Helper.ShowMessage(ClientScript, "error saving");
            }

            GoBack();
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }
        private void GoBack()
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}