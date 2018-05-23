
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class PrintStudentEvaluations : BasePage
    {
        
        private int nStudentID;
        protected DataRow drStudentDetails;

        protected string strTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["StudentID"] != null)
            {
                nStudentID = Convert.ToInt32(Request.QueryString["StudentID"]?.ToString() ?? "5");
                drStudentDetails = DBConnection.Instance.GetStudentDetails(nStudentID);
                strTitle = drStudentDetails["name"].ToString();

                DataTable dtStudentEvaluations = DBConnection.Instance.GetStudentEvaluations(nStudentID);

                Table tblStudentEvaluations = new Table() { CssClass = "table table-striped table-bordered  text-right" };
                TableRow trHeaderRow = new TableRow() { CssClass = "thead-light " };
                TableHeaderCell thcLesson = new TableHeaderCell() { Text = "שיעור" };
                thcLesson.Style.Add("width", "25%");
                trHeaderRow.Cells.Add(thcLesson);
                TableHeaderCell thcEvaluation = new TableHeaderCell() { Text = "הערכה" };
                thcEvaluation.Style.Add("width", "70%");
                trHeaderRow.Cells.Add(thcEvaluation);

                //trHeaderRow.Cells.Add(new TableHeaderCell() { Text = "הערכה" });
                tblStudentEvaluations.Rows.Add(trHeaderRow);


                string strHtml = "";
                strHtml += "<div class\"row\">";
                strHtml += "<div class=\" bg-info col-4 \">שיעור</div>";
                strHtml += "<div class=\"bg-danger col-8 \">הערכה</div>";
                strHtml += "</div>";

                foreach (DataRow drCurrentEvaluation in dtStudentEvaluations.Rows)
                {
                    TableRow trEvaluationRow = new TableRow() { };
                    TableCell tcLesson = new TableCell() { Text = drCurrentEvaluation["lesson_name"].ToString()};
                    tcLesson.Style.Add("width", "25%");
                    trEvaluationRow.Cells.Add(tcLesson);
                    TableCell tcEvaluatuon = new TableCell() { Text = drCurrentEvaluation["evaluation"].ToString()};
                    tcEvaluatuon.Style.Add("width", "75%");
                    trEvaluationRow.Cells.Add(tcEvaluatuon);
                    tblStudentEvaluations.Rows.Add(trEvaluationRow);
                }
                pnlEvaluations.Controls.Add(new LiteralControl("<div class=\"row\">"));
                pnlEvaluations.Controls.Add(tblStudentEvaluations);
                pnlEvaluations.Controls.Add(new LiteralControl("</div>"));

            }
        }

        

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["StudentID"] != null)
                Response.Redirect("StudentDetailsForm.aspx?StudentID=" + nStudentID);
        }
        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);
            
        }
    }
    
}