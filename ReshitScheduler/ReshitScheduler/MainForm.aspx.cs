using Data;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    
    public partial class MainForm : System.Web.UI.Page
    {
        public static Teacher LoggedInTeacher;
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
                case "מנהל":
                    Response.Redirect("PrincipalPage.aspx");
                    break;
                default:
                    break;
            }

            

            DataTable dtClasses = DBConnection.Instance.GetDataTableByQuery(
                "select concat(grades.grade_name,classes.class_number) ,classes.id " +
                "from teachers " +
                "inner join classes on classes.teacher_id = teachers.id " +
                "inner join grades on grades.id = classes.grade_id " +
                "where teachers.id = " + LoggedInTeacher.Id);

            if(dtClasses.Rows.Count == 0 )
            {
                pnlClassesPanel.Visible = false;
            }
            foreach (DataRow drCurrentRow in dtClasses.Rows)
            {
                Button btnClassButton = new Button()
                {
                    ID = drCurrentRow[1].ToString(),
                    Text = drCurrentRow[0].ToString(),
                    CssClass= "btn btn-outline-dark",
                };
                btnClassButton.Click += btnClass_Click;
                pnlClassesPanel.Controls.Add(btnClassButton);
            }



        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session["LoggedInTeacher"] = null;
            Response.Redirect("LoginForm.aspx");
        }

        protected void btnClass_Click(object sender, EventArgs e)
        {
            Session["ClassID"] = (sender as LinkButton).ID;
            Response.Redirect("ClassPage.aspx?ClassID="+(sender as LinkButton).ID);
        }
        protected void BtnAddStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddStudentForm.aspx");
        }
        protected void BtnBellSystem_Click(object sender, EventArgs e)
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
    }
}