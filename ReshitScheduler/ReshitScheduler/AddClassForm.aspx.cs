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
    public partial class AddClassForm : System.Web.UI.Page
    {
        private DataTable dtTeachers;
        private DataTable dtClasses;
        private DataTable dtGrades;

        protected void Page_Load(object sender, EventArgs e)
        {
            dtTeachers = DBConnection.Instance.GetThisYearTeachers();
            dtClasses = DBConnection.Instance.GetThisYearClasses();
            dtGrades = DBConnection.Instance.GetGrades();
            if (!IsPostBack)
            {

                ddlTeachers.DataSource = dtTeachers;
                ddlTeachers.DataValueField = "id";
                ddlTeachers.DataTextField = "name";
                ddlTeachers.DataBind();

                ddlGrades.DataSource = dtGrades;
                ddlGrades.DataValueField = "id";
                ddlGrades.DataTextField = "name";
                ddlGrades.DataBind();

                gvClasses.DataSource = dtClasses;
                gvClasses.DataBind();

            }

        }



        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }
        private void GoBack()
        {
            Response.Redirect("MainForm.aspx");
        }
        protected void BtnAddClass_Click(object sender, EventArgs e)
        {
            int nGradeID = Convert.ToInt32(ddlGrades.SelectedValue);
            int nClassNumber = Convert.ToInt32(txtStudentFirstName.Text);
            int nTeacherID = Convert.ToInt32(ddlTeachers.SelectedValue);
            if (dtClasses.Select("grade_id = " + nGradeID + " AND class_number = " + nClassNumber).Count()>0)
            {
                Helper.ShowMessage(ClientScript, "כיתה כבר קיימת - לא ניתן להוסיף");
                return;
            }
            bool bInsertSucceeded = 
                DBConnection.Instance.InsertTableRow("classes", "grade_id,class_number,teacher_id",
                                                           nGradeID + "," + nClassNumber + "," + nTeacherID);
            if(!bInsertSucceeded)
            {
                Helper.ShowMessage(ClientScript, "error saving");
            }
            dtClasses = DBConnection.Instance.GetThisYearClasses();
            gvClasses.DataSource = dtClasses;
            gvClasses.DataBind();
        }
    }
}