using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class LessonForm : BasePage
    {
        protected DataRow drLessonDetails;

        private string strLessonID;
        private bool IsGroup;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["GroupID"] != null)
            {
                strLessonID = Request.QueryString["GroupID"].ToString();
                IsGroup = true;
            }
            else
            {
                strLessonID = Request.QueryString["CourseID"]?.ToString() ?? "14";
                IsGroup = false;
            }
            LoadLesson();
        }

        private void LoadLesson()
        {
            if (IsGroup)
            {
                drLessonDetails = DBConnection.Instance.GetDataTableByQuery("select id,group_name,teacher_id as name from groups where id = " + strLessonID).Rows[0];
            }
            else
            {
                drLessonDetails = DBConnection.Instance.GetDataTableByQuery("select id,course_name,teacher_id as name from courses where id = " + strLessonID).Rows[0];
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
                    " where group_id = " + strLessonID;
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
                    " where classes_schedule.course_id = " + strLessonID +
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
                                                                    (IsGroup?"&GroupID=":"&CourseID")+strLessonID + "\" class=\"col-12 col-md-2 col-sm-4\">" +
                              "<figure > <img src=\"" + drCurrentStudent["picture_path"] + "\" width=\"100\">" +
                              "<figcaption>" + drCurrentStudent["student_name"] + "</figcaption></figure></a>";
                    pnlClassPanel.Controls.Add(new LiteralControl(strFigure));
                }
                pnlClassesPanel.Controls.Add(pnlClassPanel);
            }

        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            if (strPreviousPage.Contains("Coordinator"))
            {
                Response.Redirect("MainForm.aspx");
            }
            else
            {
                Response.Redirect("MainForm.aspx");
                // Response.Redirect("TeacherCoursesAndGroupsForm.aspx?TeacherID=" + drLessonDetails["teacher_id"] + " & ClassID=" + strClassID");
            }
            //GoBack();
        }
    }
}