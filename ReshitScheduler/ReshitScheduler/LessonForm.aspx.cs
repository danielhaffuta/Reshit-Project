
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace ReshitScheduler
{
    public partial class LessonForm : BasePage
    {




        protected DataRow drLessonDetails;
        protected DataTable dtClasses;
        protected DataTable dtStudents;

        private int nLessonID;

        private bool IsGroup;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.SetFocus(this);
            if (Request.QueryString["GroupID"] != null)
            {
                nLessonID = Convert.ToInt32(Request.QueryString["GroupID"]?.ToString() ?? "12");
                IsGroup = true;
            }
            else
            {
                nLessonID = Convert.ToInt32(Request.QueryString["CourseID"]?.ToString() ?? "14");
                IsGroup = false;
            }
            //nClassID = Convert.ToInt32(Request.QueryString["ClassID"]?.ToString() ?? "5");
            if (IsGroup)
            {
                drLessonDetails = DBConnection.Instance.GetDataTableByQuery("select id,group_name,teacher_id from groups where id = " + nLessonID).Rows[0];
            }
            else
            {
                drLessonDetails = DBConnection.Instance.GetDataTableByQuery("select id,course_name,teacher_id from courses where id = " + nLessonID).Rows[0];
            }
            if (!IsPostBack)
            {
                FillClasses();
            }
            if(Request.Params.Get("__EVENTTARGET") == null || !Request.Params.Get("__EVENTTARGET").Contains("txtEvaluation"))
            {
                FillStudentsEvaluations();

            }



        }


        private void FillClasses()
        {
            LoadStudents();
            if (dtStudents.Rows.Count == 0)
            {
                pnlNoStudentsMsg.Visible = true;
                divClasses.Visible = false;
                return;
            }
            dtClasses = new DataView(dtStudents).ToTable(true, "class_id","class");
            ddlClassesList.DataSource = dtClasses;
            ddlClassesList.DataBind();
        }

        private void LoadStudents()
        {
            if (IsGroup)
            {
                dtStudents = DBConnection.Instance.GetGroupEvaluations(nLessonID);
            }
            else
            {
                dtStudents = DBConnection.Instance.GetCourseEvaluations(nLessonID);
            }

        }


        private void FillStudentsEvaluations()
        {
            LoadStudents();
            if (dtStudents.Rows.Count == 0)
            {
                pnlNoStudentsMsg.Visible = true;
                divClasses.Visible = false;
                return;
            }
            gvStudents.DataSource = dtStudents.Select("class_id = " + ddlClassesList.SelectedValue).CopyToDataTable();
            gvStudents.DataBind();
            if(gvStudents.Rows.Count==0)
            {
                pnlNoStudentsMsg.Visible = true;
                return;
            }
            pnlNoStudentsMsg.Visible = false;
            gvStudents.HeaderRow.Cells[0].Visible = false;
            gvStudents.HeaderRow.Cells[1].Visible = false;
            foreach (GridViewRow gvrCurrentRow in gvStudents.Rows)
            {
                TextBox txtStudentEvaluation = (gvrCurrentRow.Cells[4].Controls[1] as TextBox);
                txtStudentEvaluation.Text = dtStudents.Select("student_id = " + gvrCurrentRow.Cells[0].Text)[0]["evaluation"].ToString();
                //txtStudentEvaluation.Attributes.Add("onfocus", "try{document.getElementById('__LASTFOCUS').value=this.id} catch(e) {}");
                gvrCurrentRow.Cells[0].Visible = false;
                gvrCurrentRow.Cells[1].Visible = false;
            }

        }



        /*private void LoadLesson1()
        {
            if (IsGroup)
            {
                drLessonDetails = DBConnection.Instance.GetDataTableByQuery("select id,group_name,teacher_id from groups where id = " + nLessonID).Rows[0];
            }
            else
            {
                drLessonDetails = DBConnection.Instance.GetDataTableByQuery("select id,course_name,teacher_id from courses where id = " + nLessonID).Rows[0];
            }
            string strQuery;
            if (IsGroup)
            {
                strQuery =
                    " select  distinct (students.id) as student_id," +
                        " students_classes.class_id as class_id," +
                        " concat(grades.grade_name, classes.class_number) as class," +
                        " groups.group_name as lesson_name," +
                        " concat(students.first_name, ' ', students.last_name) as student_name," +
                        " students.picture_path" +
                    " from students_schedule" +
                    " inner join groups on groups.id = students_schedule.group_id" +
                    " inner join students_classes on students_classes.student_id = students_schedule.student_id" +
                    " inner join students on students.id = students_classes.student_id" +
                    " inner join classes on classes.id = students_classes.class_id" +
                    " inner join grades on grades.id = classes.grade_id" +
                    " where group_id = " + nLessonID;
            }
            else
            {
                strQuery =
                    " select  distinct (students.id) as student_id," +
                        " students_classes.class_id as class_id," +
                        " concat(grades.grade_name, classes.class_number) as class," +
                        " courses.course_name as lesson_name," +
                        " concat(students.first_name, ' ', students.last_name) as student_name," +
                        " students.picture_path" +
                    " from classes_schedule" +
                    " inner join courses on courses.id = classes_schedule.course_id" +
                    " inner join classes on classes.id = classes_schedule.class_id" +
                    " inner join grades on grades.id = classes.grade_id" +
                    " inner join students_classes on students_classes.class_id = classes_schedule.class_id " +
                    " inner join students on students.id = students_classes.student_id" +
                    " where classes_schedule.course_id = " + nLessonID +
                    " and students.id not in (select student_id from students_schedule where students_schedule.day_id = classes_schedule.day_id" +
                                                                                       " and students_schedule.hour_id = classes_schedule.hour_id)";
            }
            DataTable dtStudents = DBConnection.Instance.GetDataTableByQuery(strQuery);


            DataTable dtDistinctClasses = new DataView(dtStudents).ToTable(true, "class", "class_id");

            foreach (DataRow drCurrentClass in dtDistinctClasses.Rows)
            {

                Panel pnlClassPanel = new Panel()
                {
                    //GroupingText = drCurrentClass["class"].ToString(),
                    ID = drCurrentClass["class_id"].ToString(),
                    CssClass = "row"
                };
                pnlClassesPanel.Controls.Add(new LiteralControl("<h2>" + drCurrentClass["class"] + "</h2>"));

                DataRow[] draStudentsInClass = dtStudents.Select("class_id = " + drCurrentClass["class_id"]);
                foreach (DataRow drCurrentStudent in draStudentsInClass)
                {
                    string strFigure = "<a href=\"EvaluationForm.aspx?StudentID=" + drCurrentStudent["student_id"] +
                                                                    (IsGroup ? "&GroupID=" : "&CourseID") + nLessonID + "\" class=\"col-12 col-md-2 col-sm-4\">" +
                              "<figure > <img src=\"" + drCurrentStudent["picture_path"] + "\" width=\"100\">" +
                              "<figcaption>" + drCurrentStudent["student_name"] + "</figcaption></figure></a>";
                    pnlClassPanel.Controls.Add(new LiteralControl(strFigure));
                }
                pnlClassesPanel.Controls.Add(pnlClassPanel);
            }

        }*/
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            if (strPreviousPage.Contains("Coordinator"))
            {
                Response.Redirect("CoordinatorForm.aspx");
            }
            else
            {
                Response.Redirect("TeacherCoursesAndGroupsForm.aspx?TeacherID=" + drLessonDetails["teacher_id"] + "&ClassID=" + nClassID);
            }
        }

        protected void txtEvaluation_TextChanged(object sender, EventArgs e)
        {
            TextBox txtChangedEvaluation = sender as TextBox;
            GridViewRow gvrChangedRow = txtChangedEvaluation.Parent.Parent as GridViewRow;
            ((TextBox)gvrChangedRow.FindControl("txtEvaluation")).ReadOnly = true;
            int nEvaluationID;
            if (int.TryParse(gvrChangedRow.Cells[1].Text, out nEvaluationID))
            {
                DBConnection.Instance.UpdateTableRow(IsGroup ? "groups_evaluations" : "courses_evaluations",
                                           nEvaluationID,
                                          "evaluation",
                                          "'" + (gvrChangedRow.Cells[4].Controls[1] as TextBox).Text.Replace("'", "''") + "'");
            }
            else
            {
                string[] strFields = { IsGroup ? "group_id" : "course_id", "evaluation","student_id" };
                string[] strValues = { Convert.ToString(nLessonID), (gvrChangedRow.Cells[4].Controls[1] as TextBox).Text, gvrChangedRow.Cells[0].Text };
                DBConnection.Instance.InsertTableRow(IsGroup ? "groups_evaluations" : "courses_evaluations",
                                                                    strFields, strValues);

            }
            ((TextBox)gvrChangedRow.FindControl("txtEvaluation")).ReadOnly = false;
            return;
        }

        protected void ddlClassesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStudentsEvaluations();

        }
    }
}