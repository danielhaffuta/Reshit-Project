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
    public partial class CoordinatorForm : BasePage
    {

        public static Teacher LoggedInTeacher;
        //private static string strPreviousPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInTeacher"] == null)
            {
                LoggedInTeacher = new Teacher()
                {
                    FirstName = "אדוני",
                    LastName = "הרכז",
                    Id = 10,
                    Type = "רכז",
                };
            }
            else
            {
                LoggedInTeacher = Session["LoggedInTeacher"] as Teacher;
            }
            FillClasses();
            FillGroups();

            if (!IsPostBack)
            {
                strPreviousPage = Request.UrlReferrer?.ToString() ?? "LoginForm.aspx";
            }
            hTeacherName.Text = "שלום " + LoggedInTeacher.FirstName + " " + LoggedInTeacher.LastName;
        }

        private void FillClasses()
        {
            DataTable dtClasses = DBConnection.Instance.GetDataTableByQuery(
                " select concat(grades.grade_name,classes.class_number) as name,classes.id " +
                " from teacher_class_access " +
                " inner join classes on classes.id = teacher_class_access.class_id " +
                " inner join grades on grades.id = classes.grade_id " +
                " where teacher_class_access.teacher_id = " + LoggedInTeacher.Id);

            if (dtClasses.Rows.Count == 0)
            {
                pnlClasses.Visible = false;
            }
            foreach (DataRow drCurrentClass in dtClasses.Rows)
            {
                pnlClasses.Controls.Add(new LiteralControl("<a class=\"list-group-item list-group-item-action d-block\" " +
                                       "href=ClassPage.aspx?ClassID=" + drCurrentClass["id"] + ">" + drCurrentClass["name"] + "</a></li>"));
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
    }

}