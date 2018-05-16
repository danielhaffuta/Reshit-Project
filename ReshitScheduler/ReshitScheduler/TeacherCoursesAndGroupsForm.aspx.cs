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
    public partial class TeacherCoursesAndGroupsForm : BasePage
    {
        protected DataRow drTeacherDetails;


        private int nTeacherID;
        private int nClassID;


        protected void Page_Load(object sender, EventArgs e)
        {

            nTeacherID = Convert.ToInt32(Request.QueryString["TeacherID"]?.ToString() ?? "6");
            nClassID = Convert.ToInt32(Request.QueryString["ClassID"]?.ToString() ?? "5");

            drTeacherDetails = DBConnection.Instance.GetConstraintDataTable("teachers", " where teachers.id = " + nTeacherID).Rows[0];
            /*drTeacherDetails = DBConnection.Instance.GetDataTableByQuery(" select concat(first_name,' ' ,last_name) as name from"+
                                                                         " teachers where teachers.id = " + strTeacherID).Rows[0];*/
            LoadCourses();
            LoadGroups();
        }

        private void LoadCourses()
        {
            DataTable dtCourses = DBConnection.Instance.GetDataTableByQuery("select id,course_name from courses where teacher_id = " + nTeacherID);
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

        private void LoadGroups()
        {
            DataTable dtGroups = DBConnection.Instance.GetDataTableByQuery("select id,group_name from groups where teacher_id = " + nTeacherID);
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
            Response.Redirect("LessonForm.aspx?" + (sender as Button).ID + "&ClassID=" + nClassID);
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }
        private void GoBack()
        {
            Response.Redirect("ClassPage.aspx?ClassID=" + nClassID);
        }

    }
}