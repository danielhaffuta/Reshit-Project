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
    public partial class EditClassDetails : System.Web.UI.Page
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
                string strDisplayQuert = DBConnection.Instance.GetDisplayQuery("classes");
                DataTable dtClassesTable = DBConnection.Instance.GetDataTableByQuery(strDisplayQuert);
                ddlClasses.DataSource = dtClassesTable;
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
        }

        protected void ddlClasses_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        }
    }
}