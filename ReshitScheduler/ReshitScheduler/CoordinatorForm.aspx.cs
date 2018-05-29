
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
    public partial class CoordinatorForm : BasePage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            
            FillClasses();
            FillCourses();
            FillGroups();

            hTeacherName.Text = "שלום " + LoggedInTeacher.FirstName + " " + LoggedInTeacher.LastName;
        }

        private void FillClasses()
        {
            DataTable dtClasses = DBConnection.Instance.GetTeacherClasses(LoggedInTeacher.ID);

            if (dtClasses.Rows.Count == 0)
            {
                pnlClasses.Visible = false;
            }
            foreach (DataRow drCurrentClass in dtClasses.Rows)
            {
                pnlClasses.Controls.Add(new LiteralControl("<a class=\"list-group-item list-group-item-action d-block\" " +
                                       "href=ClassPage.aspx?ClassID=" + drCurrentClass["class_id"] + ">" + drCurrentClass["name"] + "</a></li>"));
            }
        }

        private void FillCourses()
        {
            DataTable dtCourses = DBConnection.Instance.GetDataTableByQuery(
                " select id as course_id,course_name as name" +
                " from courses " +
                " where courses.teacher_id = " + LoggedInTeacher.ID);

            if (dtCourses.Rows.Count == 0)
            {
                h3Courses.Visible = false;
            }
            foreach (DataRow drCurrentCourse in dtCourses.Rows)
            {
                pnlCourses.Controls.Add(new LiteralControl("<a class=\"list-group-item list-group-item-action d-block\" " +
                                      "href=LessonForm.aspx?CourseID=" + drCurrentCourse["course_id"] + ">" + drCurrentCourse["name"] + "</a></li>"));

            }

        }

        private void FillGroups()
        {
            DataTable dtGroups = DBConnection.Instance.GetDataTableByQuery(
                " select id as group_id,group_name as name" +
                " from groups " +
                " where groups.teacher_id = " + LoggedInTeacher.ID);

            if (dtGroups.Rows.Count == 0)
            {
                pnlGroups.Visible = false;
            }
            foreach (DataRow drCurrentGroup in dtGroups.Rows)
            {
                pnlGroups.Controls.Add(new LiteralControl("<a class=\"list-group-item list-group-item-action d-block\" " +
                                      "href=LessonForm.aspx?GroupID=" + drCurrentGroup["group_id"] + ">" + drCurrentGroup["name"] + "</a></li>"));

            }

        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }
        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginForm.aspx");
        }

        protected void GoBack()
        {
            Response.Redirect("MainForm.aspx");
        }

        protected void BtnIncreaseYear(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                IncreaseYear();
            }
        }

        private void IncreaseYear()
        {
            DBConnection.Instance.IncreaseCurrentYearID();
            Response.Redirect("LoginForm.aspx");

        }

        protected void BtnIncreaseSemester_Click(object sender, EventArgs e)
        {
            Helper.ShowMessage(ClientScript, "מבצע מעבר למחצית שניה של השנה");
            DBConnection.Instance.IncreaseSemester();
        }
    }

}