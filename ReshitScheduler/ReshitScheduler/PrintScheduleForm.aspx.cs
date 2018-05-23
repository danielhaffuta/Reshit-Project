
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class PrintScheduleForm : BasePage
    {
        protected string strTitle;

        private int nClassID;
        private int nStudentID;
        private DataTable dtScheduleTable;
        protected DataRow drStudentDetails;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["StudentID"] != null)
            {
                nStudentID = Convert.ToInt32(Request.QueryString["StudentID"]?.ToString() ?? "5");
                drStudentDetails = DBConnection.Instance.GetStudentDetails(nStudentID);
                strTitle = drStudentDetails["name"].ToString();

                dtScheduleTable = FormsUtilities.BuildEmptySchedule();
                FormsUtilities.FillStudentSchedule(nStudentID, Convert.ToInt32(drStudentDetails["class_id"]), dtScheduleTable);
                pnlSchedule.Controls.Add(FormsUtilities.FillStudentGrid(dtScheduleTable, null));
            }
            else if (Request.QueryString["ClassID"] != null)
            {
                if (Request.QueryString["AllStudents"] != null)
                {
                    nClassID = Convert.ToInt32(Request.QueryString["ClassID"]?.ToString() ?? "5");

                    DataTable dtStudents = DBConnection.Instance.GetDataTableByQuery("select students.id,concat(first_name,' ' ,last_name) as name,"+
                                                                                     " picture_path from students" +
                                                                                     " inner join students_classes on students_classes.student_id = students.id" +
                                                                                     " where students_classes.class_id = " + nClassID);
                    

                    foreach (DataRow drCurrentStudent in dtStudents.Rows)
                    {
                        int nCurrentStudentID = Convert.ToInt32(drCurrentStudent["id"]);

                        dtScheduleTable = FormsUtilities.BuildEmptySchedule();
                        FormsUtilities.FillStudentSchedule(nCurrentStudentID, nClassID, dtScheduleTable);
                        pnlSchedule.Controls.Add(new LiteralControl("<h2 class=\"mt-5\">" + drCurrentStudent["name"] + "</h2>"));
                        GridView gvStudentSchedule = FormsUtilities.FillStudentGrid(dtScheduleTable, null);
                        gvStudentSchedule.Style.Add("page-break-after", "always");
                        pnlSchedule.Controls.Add(gvStudentSchedule);
                    }

                }
                else
                {
                    nClassID = Convert.ToInt32(Request.QueryString["ClassID"]?.ToString() ?? "5");
                    strTitle = DBConnection.Instance.GetConstraintData("classes", nClassID);

                    dtScheduleTable = FormsUtilities.BuildEmptySchedule();
                    FormsUtilities.FillClassSchedule(nClassID, dtScheduleTable);
                    pnlSchedule.Controls.Add(FormsUtilities.FillClassGrid(nClassID, dtScheduleTable, true));
                }
            }
            if (!IsPostBack)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);
            }
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["StudentID"] != null)
                Response.Redirect("StudentDetailsForm.aspx?StudentID=" + nStudentID);
            else if(Request.QueryString["ClassID"] != null)
                Response.Redirect("ClassPage.aspx?ClassID = " + nClassID);
            //GoBack();
        }
    }
}