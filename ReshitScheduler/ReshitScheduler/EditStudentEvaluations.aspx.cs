using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class EditStudentEvaluations : BasePage
    {
        private int nStudentID;
        protected DataRow drStudentDetails;
        private DataTable dtStudentEvaluations;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["StudentID"] != null)
            {
                nStudentID = Convert.ToInt32(Request.QueryString["StudentID"]?.ToString() ?? "5");
                drStudentDetails = DBConnection.Instance.GetStudentDetails(nStudentID);
                name.InnerText = drStudentDetails["name"].ToString();
                
                if(!IsPostBack)
                {
                    FillEvaluations();
                }
            }
        }
        private void FillEvaluations()
        {
            dtStudentEvaluations = DBConnection.Instance.GetStudentEvaluations(nStudentID);
            gvEvaluations.DataSource = dtStudentEvaluations;
            gvEvaluations.DataBind();
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["StudentID"] != null)
                Response.Redirect("StudentDetailsForm.aspx?StudentID=" + nStudentID);
        }

        protected void txtEvaluation_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}