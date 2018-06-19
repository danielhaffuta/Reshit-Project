
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class AddClassForm : BasePage
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
            string nGradeID = ddlGrades.SelectedValue;
            string nClassNumber = txtClassNumber.Text;
            string nTeacherID = ddlTeachers.SelectedValue;
            DataTable dtClassCheck = DBConnection.Instance.GetThisYearClassesDetails();
            if (dtClassCheck.Select("grade_id = " + nGradeID + " AND class_number = " + nClassNumber).Count() > 0)
            {
                Helper.ShowMessage(ClientScript, "כיתה כבר קיימת - לא ניתן להוסיף");
                return;
            }
            string[] strFields = { "grade_id","class_number","teacher_id" };
            string[] strValues = { nGradeID, nClassNumber, nTeacherID };
            int nInsertSucceeded = DBConnection.Instance.InsertTableRow("classes", strFields, strValues);
            if(nInsertSucceeded == 0)
            {
                Helper.ShowMessage(ClientScript, "error saving");
                return;
            }
            Helper.ShowMessage(ClientScript, "כיתה נשמרה");
            dtClasses = DBConnection.Instance.GetThisYearClasses();
            gvClasses.DataSource = dtClasses;
            gvClasses.DataBind();
        }
    }
}