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
    public partial class StudentDetailsForm : BasePage
    {
        protected global::System.Web.UI.WebControls.ContentPlaceHolder MainForm;

        private DataTable dtScheduleTable;
        protected DataRow drStudentDetails;


        private int nStudentID;


        protected void Page_Load(object sender, EventArgs e)
        {

            nStudentID = Convert.ToInt32(Request.QueryString["StudentID"]?.ToString() ?? "5");
            drStudentDetails = DBConnection.Instance.GetStudentDetails(nStudentID);
            dtScheduleTable = FormsUtilities.BuildEmptySchedule();
            FormsUtilities.FillStudentSchedule(nStudentID,Convert.ToInt32(drStudentDetails["class_id"]),dtScheduleTable);
            pnlSchedule.Controls.Add(FormsUtilities.FillStudentGrid(dtScheduleTable, LbEvaluationLinkButton_Click));

        }
        

        private void LbEvaluationLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("EvaluationForm.aspx?StudentID=" + nStudentID + "&" + (sender as LinkButton).ID.Split('-')[0]);
        }
        protected void BtnEditStudentDetails(object sender, EventArgs e)
        {
            Response.Redirect("EditStudentDetailsForm.aspx?StudentID=" + nStudentID);

        }
        
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }
        private void GoBack()
        {
            if (strPreviousPage.Contains("ClassPage.aspx"))
            {
                Response.Redirect("ClassPage.aspx?StudentID=" + nStudentID + "&ClassID=" + drStudentDetails["class_id"]);
            }
            else if (strPreviousPage.Contains("AddStudentForm.aspx"))
            {
                Response.Redirect("AddStudentForm.aspx");
            }
        }
        protected void BtnPrintSchedule_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrintScheduleForm.aspx?StudentID=" + nStudentID );
        }
        protected void BtnPrintEvaluations_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrintStudentEvaluations.aspx?StudentID=" + nStudentID);
        }
    }
}
