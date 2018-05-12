using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class PrincipalPage : System.Web.UI.Page
    {
        private readonly int SCHEDULE = 1, STUDENTS_CONTACT = 2, TEACHERS_CONTACT = 3;
        public static Teacher LoggedInTeacher;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["teacherLoggedIn"] == null)
            {
                Response.Redirect("LoginForm.aspx");
                return;
            }
            LoggedInTeacher = Session["teacherLoggedIn"] as Teacher;

            //===show class schedule===//
            ShowAllClasses(SCHEDULE);
            //=========================//

            //===show class Contact list===//
            LinkButton lbClassContactListButton = new LinkButton()
            {
                ID = "ClassContactList",
                Text = "דף קשר לפי כיתה",
            };
            lbClassContactListButton.Click += lbEvaluationsButton_Click;
            editOptionsPanel.Controls.Add(lbClassContactListButton);
            editOptionsPanel.Controls.Add(new LiteralControl("<br />"));
            //=============================//

            //===show teachers Contact list===//
            LinkButton lbTeachersContactListButton = new LinkButton()
            {
                ID = "TeachersContactList",
                Text = "דף קשר של המורים",
            };
            lbTeachersContactListButton.Click += lbTeachersContactListButton_Click;
            editOptionsPanel.Controls.Add(lbTeachersContactListButton);
            editOptionsPanel.Controls.Add(new LiteralControl("<br />"));
            //================================//

            //===show courses list===//
            LinkButton lbCoursesListButton = new LinkButton()
            {
                ID = "TeachersContactList",
                Text = "רשימת מקצועות לימוד לשנה נוכחית",
            };
            lbCoursesListButton.Click += lbCoursesListButton_Click;
            editOptionsPanel.Controls.Add(lbCoursesListButton);
            editOptionsPanel.Controls.Add(new LiteralControl("<br />"));
            //=======================//

            //===show class schedule===//
            LinkButton lbClassClassContactListButton = new LinkButton()
            {
                ID = "AddStudent",
                Text = "דף קשר לפי כיתה",
            };
            lbClassClassContactListButton.Click += lbEvaluationsButton_Click;
            editOptionsPanel.Controls.Add(lbClassClassContactListButton);
            editOptionsPanel.Controls.Add(new LiteralControl("<br />"));
            //=========================//

            //===add new student===//
            LinkButton lbStudentButton = new LinkButton()
            {
                ID = "AddStudent",
                Text = "הוספת תלמיד חדש",
            };
            lbStudentButton.Click += LbStudentButton_Click;
            editOptionsPanel.Controls.Add(lbStudentButton);
            editOptionsPanel.Controls.Add(new LiteralControl("<br />"));
            //=====================//

            //===add new teacher===//
            LinkButton lbTeacherButton = new LinkButton()
            {
                ID = "AddTeacher",
                Text = "הוספת מורה חדש",
            };
            lbTeacherButton.Click += lbTeacherButton_Click;
            editOptionsPanel.Controls.Add(lbTeacherButton);
            editOptionsPanel.Controls.Add(new LiteralControl("<br />"));
            //=====================//

            //===add new coordinator===//
            LinkButton lbCoordinatorrButton = new LinkButton()
            {
                ID = "AddCoordinator",
                Text = "הוספת רכז חדש",
            };
            lbCoordinatorrButton.Click += lbCoordinatorrButton_Click;
            editOptionsPanel.Controls.Add(lbCoordinatorrButton);
            editOptionsPanel.Controls.Add(new LiteralControl("<br />"));
            //=========================//

            //===add new evaluations===//
            LinkButton lbEvaluationsButton = new LinkButton()
            {
                ID = "AddEvaluation",
                Text = "הערכות תלמידים",
            };
            lbEvaluationsButton.Click += lbEvaluationsButton_Click;
            editOptionsPanel.Controls.Add(lbEvaluationsButton);
            editOptionsPanel.Controls.Add(new LiteralControl("<br />"));
            //=========================//

        }

        private void LbStudentButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddStudentForm.aspx");
        }

        private void lbTeacherButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddTeacherForm.aspx");
        }

        private void lbCoordinatorrButton_Click(object sender, EventArgs e)
        {
            //make a current teacher also a coordinator.
        }

        private void lbEvaluationsButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("EvaluationForm.aspx");
        }

        private void lbClassScheduleButton_Click(object sender, EventArgs e)
        {
            //make a list of classes, choose one and make a query for that class schedule
            //and add phone number column to table.
        }

        private void lbTeachersContactListButton_Click(object sender, EventArgs e)
        {
            //add phone number column to table.
        }

        private void ShowAllClasses(int type)
        {
            DataTable dtType = null;
            if (type == SCHEDULE)
                dtType = DBConnection.Instance.GetDataTableByQuery(
                "select concat(grades.grade_name,classes.class_number) as name,classes.id " +
                "from teacher_class_access " +
                "inner join classes on classes.id = teacher_class_access.class_id " +
                "inner join grades on grades.id = classes.grade_id " +
                "where teacher_class_access.teacher_id = " + LoggedInTeacher.Id);

            HtmlGenericControl olClassesSchedule = FindControl("olClassesSchedule") as HtmlGenericControl;

            foreach (DataRow drCurrentClass in dtType.Rows)
            {
                olClassesSchedule.InnerHtml += "<li><a href=ClassPage.aspx?ClassID=" + drCurrentClass["id"] + ">" + drCurrentClass["name"] + "</a></li>";

            }
        }

        private void lbCoursesListButton_Click(object sender, EventArgs e)
        {
            DataTable dtCourses;
            dtCourses = DBConnection.Instance.GetDataTableByQuery(
            "select C.course_name, concat(T.first_name, T.last_name) as teacher " +
            "from teachers T, courses C " +
            "where T.id = C.teacher_id"
             );

            GridView gvCourses = new GridView()
            {
                ShowHeader = false,
                CssClass = "table table-bordered table-sm",
                DataSource = dtCourses
            };
            gvCourses.DataBind();
            editOptionsPanel.Controls.Add(gvCourses);
        }


    }
}