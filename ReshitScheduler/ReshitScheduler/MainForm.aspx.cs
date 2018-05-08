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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["LoggedInTeacher"] == null)
            {
                Response.Redirect("LoginForm.aspx");
                return;
            }
            LoggedInTeacher = Session["LoggedInTeacher"] as Teacher;

            TeacherName.Text = LoggedInTeacher.FirstName + " " + LoggedInTeacher.LastName;
            DataTable dtClasses = DBConnection.Instance.GetDataTableByQuery(
                "select concat(grades.grade_name,classes.class_number) ,classes.id " +
                "from teachers " +
                "inner join classes on classes.teacher_id = teachers.id " +
                "inner join grades on grades.id = classes.grade_id " +
                "where teachers.id = " + LoggedInTeacher.Id);

            foreach (DataRow drCurrentRow in dtClasses.Rows)
            {
                LinkButton lbClassButton = new LinkButton()
                {
                    ID = drCurrentRow[1].ToString(),
                    Text = drCurrentRow[0].ToString(),
                    
                };
                lbClassButton.Click += LbClassButton_Click;
                ClassesPanel.Controls.Add(lbClassButton);
                ClassesPanel.Controls.Add(new LiteralControl("<br />"));
            }
            //LoggedInTeacher = new Teacher();

            LinkButton lbStudentButton = new LinkButton()
            {
                ID = "AddStudent",
                Text = "הוספת תלמיד חדש",
            };
            lbStudentButton.Click += LbStudentButton_Click;
            editOptionsPanel.Controls.Add(lbStudentButton);
            editOptionsPanel.Controls.Add(new LiteralControl("<br />"));

            LinkButton lbAddClasslButton = new LinkButton()
            {
                ID = "AddNewClass",
                Text = "הוספת כיתה חדשה",
            };
            lbAddClasslButton.Click += lbAddClasslButton_Click;
            editOptionsPanel.Controls.Add(lbAddClasslButton);
            editOptionsPanel.Controls.Add(new LiteralControl("<br />"));

            LinkButton lbCourseButton = new LinkButton()
            {
                ID = "AddCourse",
                Text = "הוספת קורס",
            };
            lbCourseButton.Click += LbCourseButton_Click;
            editOptionsPanel.Controls.Add(lbCourseButton);
            editOptionsPanel.Controls.Add(new LiteralControl("<br />"));

            LinkButton lbGroupButton = new LinkButton()
            {
                ID = "AddGroup",
                Text = "הוספת קבוצה",
            };
            lbGroupButton.Click += LbCourseButton_Click;
            editOptionsPanel.Controls.Add(lbGroupButton);
            editOptionsPanel.Controls.Add(new LiteralControl("<br />"));

            LinkButton lbBellSystemButton = new LinkButton()
            {
                ID = "SetBellSystem",
                Text = "אתחול מערכת צלצולים",
            };
            lbBellSystemButton.Click += LbBellSystemButton_Click;
            editOptionsPanel.Controls.Add(lbBellSystemButton);
            editOptionsPanel.Controls.Add(new LiteralControl("<br />"));


        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session["LoggedInTeacher"] = null;
            Response.Redirect("LoginForm.aspx");
        }

        private void LbClassButton_Click(object sender, EventArgs e)
        {
            Session["ClassID"] = (sender as LinkButton).ID;
            Response.Redirect("ClassPage.aspx");
        }
        private void LbStudentButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddStudentForm.aspx");
        }
        private void LbBellSystemButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetBellSystem.aspx");
        }
        private void LbCourseButton_Click(object sender, EventArgs e)
        {
            Session["CourseID"] = (sender as LinkButton).ID;
            Response.Redirect("AddCourseForm.aspx");
        }
        private void lbAddClasslButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddClassForm.aspx");
        }
    }
}