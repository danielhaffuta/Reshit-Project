
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;

namespace ReshitScheduler
{
    
    public partial class MainForm : BasePage
    {
        protected bool bIsPrincipal = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInTeacher"] == null)
            {
                Response.Redirect("LoginForm.aspx");
                return;
            }
            LoggedInTeacher = Session["LoggedInTeacher"] as Teacher;
            
            switch (LoggedInTeacher.Type)
            {
                case "admin":
                    Response.Redirect("AdminForm.aspx");
                    break;
                case "רכז":
                    Response.Redirect("CoordinatorForm.aspx");
                    break;
                case "מחנך":
                    Response.Redirect("EducatorForm.aspx");
                    break;
                case "מורה":
                    Response.Redirect("TeacherForm.aspx");
                    break;
                case "מנהל":
                    bIsPrincipal = true;
                    break;
                default:
                    break;
            }

            //FillClasses();
            FillCourses();
            FillGroups();
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
                Button btnCourse = new Button()
                {
                    CssClass = "btn btn-outline-dark",
                    Text = drCurrentCourse["name"].ToString(),
                    ID = drCurrentCourse["course_id"].ToString()
                };

                btnCourse.Click += BtnCourse_Click;
                pnlCourses.Controls.Add(btnCourse);

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
                h3Groups.Visible = false;
            }
            foreach (DataRow drCurrentGroup in dtGroups.Rows)
            {
                Button btnGroup = new Button()
                {
                    CssClass= "btn btn-outline-dark",
                    Text = drCurrentGroup["name"].ToString(),
                    ID = drCurrentGroup["group_id"].ToString()
                };

                btnGroup.Click += BtnGroup_Click;
                pnlGroups.Controls.Add(btnGroup);

            }

        }

        private void BtnCourse_Click(object sender, EventArgs e)
        {
            Response.Redirect("LessonForm.aspx?CourseID=" + (sender as Button).ID);
        }

        private void BtnGroup_Click(object sender, EventArgs e)
        {
            Response.Redirect("LessonForm.aspx?GroupID=" + (sender as Button).ID);
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session["LoggedInTeacher"] = null;
            Response.Redirect("LoginForm.aspx");
        }

        protected void BtnAddStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddStudentForm.aspx");
        }
        protected void BtnBellSystem_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetBellSystem.aspx");
        }
        protected void BtnAddTeacher_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddTeacherForm.aspx");
        }

        protected void BtnAddCourse_Click(object sender, EventArgs e)
        {
            Session["IsGroup"] = false;
            Response.Redirect("AddLessonForm.aspx");
        }
        protected void BtnAddClass_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddClassForm.aspx");
        }

        protected void BtnAddGroup_Click(object sender, EventArgs e)
        {
            Session["IsGroup"] = true;
            Response.Redirect("AddLessonForm.aspx");
        }
        protected void BtnEditTeacher_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditTeacherDetails.aspx");

        }
        protected void BtnEditClass_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditClassDetails.aspx");
        }

    }
}