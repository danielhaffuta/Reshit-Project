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
    public partial class ClassPage :BasePage
    {

        private DataTable dtScheduleTable;


        private int nClassID;
        protected string strClassName;
        private static string strPreviousPage;
        public static Teacher LoggedInTeacher;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CurrentYearID"] == null)
            {
                Session["CurrentYearID"] = nYearID;
            }
            if (Session["LoggedInTeacher"] == null)
            {
                //Response.Redirect("LoginForm.aspx");
                //return;
            }
            else
            {
                LoggedInTeacher = Session["LoggedInTeacher"] as Teacher;
            }
            /*if (Request.QueryString["ClassID"] == null /* Session["ClassID"] == null)
            {
                Session["ClassID"] = 1;
                //Response.Redirect("MainForm.aspx");
                //return;
            }*/
            if (!IsPostBack)
            {
                strPreviousPage = Request.UrlReferrer?.ToString() ?? "LoginForm.aspx";

            }
            LoadClassSchedule();
            FillStudents();
        }

        private void LoadClassSchedule()
        {
            nClassID = Convert.ToInt32( Request.QueryString["ClassID"]?.ToString() ?? "5");
            strClassName = DBConnection.Instance.GetConstraintData("classes", nClassID);

            dtScheduleTable = FormsUtilities.BuildEmptySchedule();
            FormsUtilities.FillClassSchedule(nClassID,  dtScheduleTable);
            pnlSchedule.Controls.Add(FormsUtilities.FillClassGrid(nClassID,dtScheduleTable));

        }




        private void FillStudents()
        {
            DataTable dtStudents = DBConnection.Instance.GetDataTableByQuery("select students.id,concat(first_name,' ' ,last_name) as name,picture_path from students" +
                                                                            " inner join students_classes on students_classes.student_id = students.id" +
                                                                            " where students_classes.class_id = " + nClassID);

            foreach (DataRow drCurrentStudent in dtStudents.Rows)
            {
                string strFigure = "<a href=\"StudentDetailsForm.aspx?StudentID="+ drCurrentStudent["id"] + "\" class=\"col-6 col-md-2 col-sm-4\">"+
                                   "<figure > <img src=\"" + drCurrentStudent["picture_path"] + "\" width=\"100\">"+
                                   "<figcaption>" + drCurrentStudent["name"] + "</figcaption></figure></a>";
                pnlStudents.Controls.Add(new LiteralControl(strFigure));
            }
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
            Response.Redirect("TeacherCoursesAndGroupsForm.aspx?TeacherID=" + LoggedInTeacher.Id + "&ClassID=" + nClassID);

        }

        private void GoBack()
        {
            Response.Redirect(strPreviousPage);
        }
    }
}