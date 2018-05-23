
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
    public partial class ClassPage :BasePage
    {

        private DataTable dtScheduleTable;


        private int nClassID;
        protected string strClassName;


        protected void Page_Load(object sender, EventArgs e)
        {
            LoadClassSchedule();
        }

        private void LoadClassSchedule()
        {
            nClassID = Convert.ToInt32( Request.QueryString["ClassID"]?.ToString() ?? "5");
            strClassName = DBConnection.Instance.GetConstraintData("classes", nClassID);

            dtScheduleTable = FormsUtilities.BuildEmptySchedule();
            FormsUtilities.FillClassSchedule(nClassID,  dtScheduleTable);
            pnlSchedule.Controls.Add(FormsUtilities.FillClassGrid(nClassID,dtScheduleTable));

        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
            //GoBack();
        }
        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginForm.aspx");
            //GoBack();
        }
        protected void BtnPrintSchedule_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrintScheduleForm.aspx?ClassID=" + nClassID);
        }
        protected void BtnPrintScheduleForAllStudents_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrintScheduleForm.aspx?ClassID=" + nClassID + "&AllStudents=true");
        }
        protected void GotoCoursesAndGroupsForm(object sender, EventArgs e)
        {
            Response.Redirect("TeacherCoursesAndGroupsForm.aspx?TeacherID=" + LoggedInTeacher.ID + "&ClassID=" + nClassID);

        }
        protected void BtnAddStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddStudentForm.aspx?ClassID=" + nClassID);
        }
        private void GoBack()
        {
            Response.Redirect(strPreviousPage);
        }
    }
}