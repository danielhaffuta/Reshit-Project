
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class EvaluationForm : BasePage
    {
        protected bool IsGroup;
        protected bool IsNew;
        private string strLessonID;
        protected string strLessonName;
        private string strStudentID;
        protected DataTable dtEvaluationDetails;
        protected DataRow drStudentDetails;
        protected bool CheckIfHaveEvaluation;


        protected void Page_Load(object sender, EventArgs e)
        {
            strStudentID = Request.QueryString["StudentID"]?.ToString() ?? "7";
            if (Request.QueryString["GroupID"] != null)
            {
                strLessonID = Request.QueryString["GroupID"].ToString();
                CheckIfHaveEvaluation = DBConnection.Instance.CheckIfHaveEvaluation(Convert.ToInt32(strLessonID), "groups");
                if(!CheckIfHaveEvaluation)
                {
                    Helper.ShowMessage(ClientScript, "לקבוצה זאת אין אפשרות להכניס הערכה");
                }
                strLessonName = DBConnection.Instance.GetStringByQuery("select group_name from groups where id = " + strLessonID);
                dtEvaluationDetails = DBConnection.Instance.GetDataTableByQuery("select id,evaluation from groups_evaluations" +
                                                                                " where group_id = " + strLessonID +
                                                                                " and student_id = " + strStudentID);
                IsGroup = true;
            }
            else
            {
                strLessonID = Request.QueryString["CourseID"]?.ToString() ?? "14";
                CheckIfHaveEvaluation = DBConnection.Instance.CheckIfHaveEvaluation(Convert.ToInt32(strLessonID), "courses");
                if (!CheckIfHaveEvaluation)
                {
                    Helper.ShowMessage(ClientScript, "לשיעור זה אין אפשרות להכניס הערכה");
                }
                strLessonName = DBConnection.Instance.GetStringByQuery("select course_name from courses where id = " + strLessonID);
                dtEvaluationDetails = DBConnection.Instance.GetDataTableByQuery("select id,evaluation from courses_evaluations" +
                                                                                " where course_id = " + strLessonID +
                                                                                " and student_id = " + strStudentID);
                IsGroup = false;
            }

            drStudentDetails = DBConnection.Instance.GetDataTableByQuery(" select concat(first_name,' ' ,last_name) as name," +
                                                                            " picture_path,concat(grades.grade_name,classes.class_number) as class," +
                                                                            " classes.id as class_id, students.id as student_id" +
                                                                            " from students " +
                                                                            " inner join students_classes on students_classes.student_id = students.id" +
                                                                            " inner join classes on classes.id = students_classes.class_id" +
                                                                            " inner join grades on grades.id = classes.grade_id" +
                                                                            " where students.id = " + strStudentID).Rows[0];

            IsNew = dtEvaluationDetails.Rows.Count == 0;
            if(!IsNew && !IsPostBack)
            {
                txtEvaluation.Text = dtEvaluationDetails.Rows[0]["evaluation"].ToString();
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (IsNew)
            {
                DBConnection.Instance.InsertTableRow(IsGroup ? "groups_evaluations" : "courses_evaluations",
                                                  "evaluation,student_id," + (IsGroup ? "group_id" : "course_id"),
                                                  "'" + txtEvaluation.Text.Replace("'", "''") + "'," + strStudentID + "," + strLessonID);
            }
            else
            {
                DBConnection.Instance.UpdateTableRow(IsGroup ? "groups_evaluations" : "courses_evaluations",
                                                     Convert.ToInt32(dtEvaluationDetails.Rows[0]["id"]),
                                                    "evaluation:student_id:" + (IsGroup ? "group_id" : "course_id"),
                                                    "'" + txtEvaluation.Text.Replace("'", "''") + "':" + strStudentID + ":" + strLessonID);
            }
            GoBack();
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        protected void GoBack()
        {
            Response.Redirect(strPreviousPage ?? "LoginForm.aspx");
        }
    }
}