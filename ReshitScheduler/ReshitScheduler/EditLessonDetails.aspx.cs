
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
                BtnDeleteLesson.InnerText = "מחק קבוצה";
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
            }
            else
            {
                dtCourseDetails = DBConnection.Instance.GetThisYearLessons("courses");
                drLessonDetails = dtCourseDetails.Select("course_id = " + nLessonID)[0];
            }
            if (drLessonDetails["has_evaluation"].ToString().Equals("0"))
            {
                HasEvaluation.Checked = true;
                NotHaveEvaluation.Checked = false;
            }
            else
            {
                HasEvaluation.Checked = false;
                NotHaveEvaluation.Checked = true;
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
                strFields = "group_name:teacher_id:has_evaluation";
            }
            else
            {
                tableName = "courses";
                strFields = "course_name:teacher_id:has_evaluation";
            }
            if (HasEvaluation.Checked)
            {
                strValues += ":0";
            }
            if (NotHaveEvaluation.Checked)
            {
                strValues += ":1";
            }


            bool bUpdateSucceeded = DBConnection.Instance.UpdateTableRow(tableName, nLessonID, strFields, strValues);
            if (!bUpdateSucceeded)
            {
                Helper.ShowMessage(ClientScript, "error saving");
            }
            Helper.ShowMessage(ClientScript, "נשמר");
            ResetLessonDetails();

        }
        protected void BtnDeleteLesson_Click(object sender, EventArgs e)
        {
            string strTableName;
            if (IsGroup)
            {
                strTableName = "groups";
                DBConnection.Instance.DeleteRowFromTable("groups_evaluation", nLessonID, "group_id");
                DBConnection.Instance.DeleteRowFromTable("students_schedule", nLessonID, "group_id");
            }
            else
            {
                strTableName = "courses";
                DBConnection.Instance.DeleteRowFromTable("courses_evaluation", nLessonID, "course_id");
                DBConnection.Instance.DeleteRowFromTable("classes_schedule", nLessonID, "course_id");
            }
            DBConnection.Instance.DeleteRowFromTable(strTableName, nLessonID);
            Helper.ShowMessage(ClientScript, "נמחק");
            ResetLessonDetails();
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }
        private void GoBack()
        {
            Response.Redirect("MainForm.aspx");
        }

        private void ResetLessonDetails()
        {
            ddlLessons.SelectedIndex = 0;
            nLessonID = Convert.ToInt32(ddlLessons.SelectedValue);
            FillLessonDetails();
        }
    }
}