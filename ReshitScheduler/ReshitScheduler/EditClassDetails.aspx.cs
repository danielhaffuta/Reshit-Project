
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class EditClassDetails : BasePage
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
                ddlClasses.DataSource = dtClasses;
                ddlClasses.DataValueField = "class_id";
                ddlClasses.DataTextField = "name";
                ddlClasses.DataBind();

                ddlTeachers.DataSource = dtTeachers;
                ddlTeachers.DataValueField = "id";
                ddlTeachers.DataTextField = "name";
                ddlTeachers.DataBind();

                ddlGrades.DataSource = dtGrades;
                ddlGrades.DataValueField = "id";
                ddlGrades.DataTextField = "name";
                ddlGrades.DataBind();

            }
            nClassID = Convert.ToInt32(ddlClasses.SelectedValue);
            if (!IsPostBack)
            {
                FillClassDetails();

            }
        }

        private void FillClassDetails()
        {
            DataTable dtClassesDetails = DBConnection.Instance.GetThisYearClassesDetails();
            DataRow drClassDetails = dtClassesDetails.Select("class_id = " + nClassID)[0];
            ddlGrades.SelectedValue = drClassDetails["grade_id"].ToString();
            ClassNum.Text = drClassDetails["class_number"].ToString();
            ddlTeachers.SelectedValue = drClassDetails["teacher_id"].ToString();
        }

        protected void ddlClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillClassDetails();
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }
        private void GoBack()
        {
            Response.Redirect("MainForm.aspx");
        }
        protected void BtnUpdateClass_Click(object sender, EventArgs e)
        {
            int nGradeID = Convert.ToInt32(ddlGrades.SelectedValue);
            int nClassNumber = Convert.ToInt32(ClassNum.Text);
            DataTable dtClassCheck = DBConnection.Instance.GetThisYearClassesDetails();
            if (dtClassCheck.Select("grade_id = " + nGradeID +
                                    " AND class_number = " + nClassNumber +
                                    " AND class_id <> " + nClassID).Count() > 0)
            {
                Helper.ShowMessage(ClientScript, "כיתה כבר קיימת - לא ניתן לערוך כיתה");
                return;
            }
            string strFields = "grade_id:class_number:teacher_id";
            string strValues = ddlGrades.SelectedValue + ":'" +
                               ClassNum.Text + "':" +
                               ddlTeachers.SelectedValue;

            bool bUpdateSucceeded = DBConnection.Instance.UpdateTableRow("classes", nClassID, strFields, strValues);
            if (!bUpdateSucceeded)
            {
                Helper.ShowMessage(ClientScript, "error saving");
            }
            Helper.ShowMessage(ClientScript, "נשמר");

        }
    }
}