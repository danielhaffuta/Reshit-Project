using Data;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;

namespace ReshitScheduler
{
    
    public partial class MainForm : System.Web.UI.Page
    {
        public static Teacher LoggedInTeacher;
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
                case "מנהל":
                    //Response.Redirect("PrincipalPage.aspx");
                    bIsPrincipal = true;
                    break;
                default:
                    break;
            }

            FillClasses();
            FillGroups();
        }

        private void FillClasses()
        {
            DataTable dtClasses = bIsPrincipal? DBConnection.Instance.GetThisYearClasses():
                                                DBConnection.Instance.GetTeacherClasses(LoggedInTeacher.Id);
            
            

            if (dtClasses.Rows.Count == 0)
            {
                pnlClassesPanel.Visible = false;
                return;
            }

            List<int> lstGradesIDs = dtClasses.Select("")
                                    .Select(cls => Convert.ToInt32(cls["grade_id"]))
                                    .Distinct()
                                    .OrderBy(grade_id=>grade_id)
                                    .ToList();

            foreach (int nCurrentGradeID in lstGradesIDs)
            {
                Panel pnlGrade = new Panel()
                {
                    CssClass = "col-1 align-self-start btn-group-vertical"
                };
                foreach (DataRow drCurrentRow in dtClasses.Select("grade_id = "+ nCurrentGradeID))
                {
                    Button btnClassButton = new Button()
                    {
                        ID = drCurrentRow["class_id"].ToString(),
                        Text = drCurrentRow["name"].ToString(),
                        CssClass = "btn btn-outline-dark",
                    };
                    btnClassButton.Click += btnClass_Click;
                    pnlGrade.Controls.Add(btnClassButton);
                }
                    pnlClassesPanel.Controls.Add(pnlGrade);
                
            }
        }

        private void FillGroups()
        {
            DataTable dtGroups = DBConnection.Instance.GetDataTableByQuery(
                " select id as group_id,group_name as name" +
                " from groups " +
                " where groups.teacher_id = " + LoggedInTeacher.Id);

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

        private void BtnGroup_Click(object sender, EventArgs e)
        {
            Response.Redirect("LessonForm.aspx?GroupID=" + (sender as Button).ID);
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session["LoggedInTeacher"] = null;
            Response.Redirect("LoginForm.aspx");
        }

        protected void btnClass_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassPage.aspx?ClassID="+(sender as Button).ID);
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

    }
}