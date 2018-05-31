
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class TeacherCoursesAndGroupsForm : BasePage
    {
        protected DataRow drTeacherDetails;


        protected void Page_Load(object sender, EventArgs e)
        {
            drTeacherDetails = DBConnection.Instance.GetDataTableForDisplay("teachers", " where teachers.id = " + LoggedInTeacher.ID).Rows[0];
            FillCourses();
            FillGroups();
            pnlNoLessonsMsg.Visible = !(divCourses.Visible || divGroups.Visible);
        }

        private void FillCourses()
        {
            DataTable dtCourses = DBConnection.Instance.GetDataTableByQuery("select id,course_name from courses where teacher_id = " + LoggedInTeacher.ID);
            divCourses.Visible = (dtCourses.Rows.Count != 0);
            foreach (DataRow drCurrentCourse in dtCourses.Rows)
            {
                Button btnCourseButton = new Button()
                {
                    Text = drCurrentCourse["course_name"].ToString(),
                    ID = "CourseID=" + drCurrentCourse["id"].ToString(),
                    CssClass = "btn btn-outline-dark"
                };
                btnCourseButton.Click += LessonClick;

                pnlCourses.Controls.Add(btnCourseButton);
            }
        }

        private void FillGroups()
        {
            DataTable dtGroups = DBConnection.Instance.GetDataTableByQuery("select id,group_name from groups where teacher_id = " + LoggedInTeacher.ID);
            divGroups.Visible = (dtGroups.Rows.Count != 0);
            foreach (DataRow drCurrentGroup in dtGroups.Rows)
            {
                Button btnGroupButton = new Button()
                {
                    Text = drCurrentGroup["group_name"].ToString(),
                    ID = "GroupID=" + drCurrentGroup["id"].ToString(),
                    CssClass = "btn btn-outline-dark"
                };
                btnGroupButton.Click += LessonClick;

                pnlGroups.Controls.Add(btnGroupButton);
            }
        }

        private void LessonClick(object sender, EventArgs e)
        {
            Response.Redirect("LessonForm.aspx?" + (sender as Button).ID);
        }
    }
}