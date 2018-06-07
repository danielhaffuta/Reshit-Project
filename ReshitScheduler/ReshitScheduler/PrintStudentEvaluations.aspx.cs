
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xceed.Words.NET;

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

                System.Data.DataTable dtStudentEvaluations = DBConnection.Instance.GetStudentEvaluations(nStudentID);

                System.Web.UI.WebControls.Table tblStudentEvaluations = new System.Web.UI.WebControls.Table() { CssClass = "table table-striped table-bordered  text-right" };
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

        protected void btnWordPrint_ServerClick(object sender, EventArgs e)
        {
            var docEveluation = DocX.Load(@"D:\ReshitProject\Evaluations\example.docx");
            docEveluation.SetDirection(Direction.RightToLeft);
            string strCurrentSemester = DBConnection.Instance.GetCurrentSemester();
            insertHeader(docEveluation, strCurrentSemester);

            Paragraph studentName = docEveluation.InsertParagraph();
            studentName.Alignment = Alignment.left;
            studentName.Append("שם התלמיד:" + strTitle).FontSize(12).Direction=Direction.RightToLeft;
            insertEvaluatiomToTable(pnlEvaluations, docEveluation);
            insertSignature(docEveluation);

            string strFilePath = DBConnection.Instance.GetRootDirectory()+"הערכות\\"+DBConnection.Instance.GetYearName()+"\\"+DBConnection.Instance.GetCurrentSemester();
            saveFile(docEveluation, strFilePath, strTitle);
            //docEveluation.SaveAs(@"D:\ReshitProject\Evaluations\"+ strTitle+".docx");
            
        }
        private void insertHeader(DocX docEveluation, string strCurrentSemester)
        {
            //docEveluation.AddHeaders();
            //docEveluation.DifferentOddAndEvenPages = false;
            CultureInfo culture = new CultureInfo("he-IL");
            culture.DateTimeFormat.Calendar = new HebrewCalendar();
         
            var dateAndTime = DateTime.Now;
            var date2 = dateAndTime.Date;
            string hb =date2.ToString(culture);
            hb = hb.Substring(0, hb.IndexOf("00:00:00"));
            //string hb = value.ToString(culture);
            //Xceed.Words.NET.Image img = docEveluation.AddImage(@"D:\ReshitProject\ReshitScheduler\ReshitScheduler\media\reshitLogo.gif");
            //Picture pic = img.CreatePicture(100, 100);
            //Paragraph picArea = docEveluation.Headers.Odd.InsertParagraph();
            //picArea.Alignment = Alignment.left;
            //picArea.AppendPicture(pic);
            var headerText = docEveluation.Headers.Odd.InsertParagraph();
            headerText.Alignment = Alignment.center;
            headerText.Append(strCurrentSemester);
            headerText.Direction = Direction.RightToLeft;

            var dateText = docEveluation.Headers.Odd.InsertParagraph();
            dateText.Alignment = Alignment.left ;
            dateText.Append(hb);
            dateText.Direction = Direction.RightToLeft;
        }
        private void insertEvaluatiomToTable(Panel pnlEvaluations, DocX docEveluation)
        {
            for(int currentEvelaution = 1; currentEvelaution < pnlEvaluations.Controls[2].Controls.Count;currentEvelaution++)
            {

                var tblEvaluation = docEveluation.AddTable(1, 2);
                tblEvaluation.SetDirection(Direction.RightToLeft);
                tblEvaluation.Alignment = Alignment.center;
                string strCourseName = ((System.Web.UI.WebControls.TableCell)(pnlEvaluations.Controls[2].Controls[currentEvelaution].Controls[0])).Text;
                string strEvaluation = ((System.Web.UI.WebControls.TableCell)(pnlEvaluations.Controls[2].Controls[currentEvelaution].Controls[1])).Text;
                tblEvaluation.Rows[0].Cells[0].Paragraphs.First().Append(strCourseName).FontSize(15).Font(new Font("David")).Bold();
                tblEvaluation.Rows[0].Cells[1].Paragraphs.First().Append(strEvaluation).FontSize(10).Font(new Font("David"));
                tblEvaluation.SetWidthsPercentage(new float[] { 20, 70 },null)
                tblEvaluation.Rows[0].Height = 50;
                docEveluation.InsertTable(tblEvaluation);
                docEveluation.InsertParagraph(" ");
            }       
        }

        private void insertSignature(DocX docEveluation)
        {
            
            var p = docEveluation.InsertParagraph();
            p.Direction = Direction.RightToLeft;
            p.Append("                 חתימת הצוות_________________________________________").FontSize(15); ;
            p.Append("");
            p = docEveluation.InsertParagraph();
            p.Direction = Direction.RightToLeft;
            p.Alignment = Alignment.center;
            p.Append("                 חתימת המנהלת__________________").FontSize(15);
            p.Append("");
            p = docEveluation.InsertParagraph();
            p.Direction = Direction.RightToLeft;
            p.Append("                  חתימת התלמיד_______________  חתימת ההורה_______________").FontSize(15); ;
        }

        private void saveFile(DocX docEveluation,string strFilePath,string strFileName)
        {
            strFileName = strFilePath + "\\" + strFileName + ".docx";
            if (!System.IO.Directory.Exists(strFilePath))
            {
                System.IO.Directory.CreateDirectory(strFilePath);
            }
       
            docEveluation.SaveAs(strFileName);
        }
    }

   

}