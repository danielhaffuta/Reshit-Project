
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
        private bool bLessonDeleted = false;
        private int nLessonID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["IsGroup"] == null || Request.QueryString["IsGroup"].Equals("true"))
            {
                IsGroup = true;
                LessonEdit.InnerText = "עריכת פרטי קבוצה";
                LessonSelection.Text = "בחר קבוצה";
                Course.Text = "שם הקבוצה:";
                btnDelete.Text = "מחק קבוצה";
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

        protected void BtnDeleteLesson(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                DeleteLesson();
            }
        }
        private void DeleteLesson()
        {
            string strDeleteQuery;
            DataTable dtTableCheck;
            int nCurrentYearID = DBConnection.Instance.GetCurrentYearID();
            if (IsGroup)
            {
                dtTableCheck = DBConnection.Instance.GetDataTableByQuery("select group_id from students_schedule where group_id = " + nLessonID +
                                                                        " and hour_id in(select id from hours_in_day where year_id = " + nCurrentYearID + ")");
                if (dtTableCheck.Rows.Count != 0)
                {
                    Helper.ShowMessage(ClientScript, "לא ניתן למחוק קבוצה שמשובצים בה תלמידים");
                    ResetLessonDetails();
                    return;
                }
                dtTableCheck = DBConnection.Instance.GetDataTableByQuery("select group_id from groups_evaluations where group_id = " + nLessonID +
                                                                        " and group_id in(select id from groups where teacher_id in(select id from teachers where year_id = " + nCurrentYearID + "))");
                if (dtTableCheck.Rows.Count != 0)
                {
                    Helper.ShowMessage(ClientScript, "לא ניתן למחוק קבוצה שיש בה הערכות");
                    ResetLessonDetails();
                    return;
                }
                strDeleteQuery = " delete from groups where id = " + nLessonID +
                     " and teacher_id in(select id from teachers where year_id = " + nCurrentYearID + ");";
            }
            else
            {
                dtTableCheck = DBConnection.Instance.GetDataTableByQuery("select course_id from classes_schedule where course_id = " + nLessonID +
                                                                        " and hour_id in(select id from hours_in_day where year_id = " + nCurrentYearID + ")");
                if (dtTableCheck.Rows.Count != 0)
                {
                    Helper.ShowMessage(ClientScript, "לא ניתן למחוק שיעור שמשובצים בו תלמידים");
                    ResetLessonDetails();
                    return;
                }
                dtTableCheck = DBConnection.Instance.GetDataTableByQuery("select course_id from courses_evaluations where course_id = " + nLessonID +
                                                                        " and course_id in(select id from courses where teacher_id in(select id from teachers where year_id = " + nCurrentYearID + "))");
                if (dtTableCheck.Rows.Count != 0)
                {
                    Helper.ShowMessage(ClientScript, "לא ניתן למחוק שיעור שיש בו הערכות");
                    ResetLessonDetails();
                    return;
                }
                strDeleteQuery = " delete from courses where id = " + nLessonID +
                     " and teacher_id in(select id from teachers where year_id = " + nCurrentYearID + ");";
            }
            
            DBConnection.Instance.ExecuteNonQuery(strDeleteQuery);
            Helper.ShowMessage(ClientScript, "נמחק");
            bLessonDeleted = true;
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
            if (bLessonDeleted)
            {
                if (IsGroup)
                {
                    dtGroups = DBConnection.Instance.GetThisYearGroups();
                    ddlLessons.DataSource = dtGroups;
                    ddlLessons.DataValueField = "group_id";
                }
                else
                {
                    dtCourses = DBConnection.Instance.GetThisYearCourses();
                    ddlLessons.DataSource = dtCourses;
                    ddlLessons.DataValueField = "course_id";
                }
                ddlLessons.DataTextField = "name";
                ddlLessons.AutoPostBack = true;
                ddlLessons.DataBind();
            }
            ddlLessons.SelectedIndex = 0;
            nLessonID = Convert.ToInt32(ddlLessons.SelectedValue);
            FillLessonDetails();
        }
    }
}