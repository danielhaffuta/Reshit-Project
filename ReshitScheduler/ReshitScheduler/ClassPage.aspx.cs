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
    public partial class ClassPage : System.Web.UI.Page
    {

        private DataTable dtDays;
        private DataTable dtHours;
        protected DataTable dtCourses;
        private DataTable dtScheduleTable;


        private string strClassID;
        private static string strPreviousPage;
        public static Teacher LoggedInTeacher;
        private int nYearId;


        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["CurrentYearID"] == null)
            {
                nYearId = Convert.ToInt32(DBConnection.Instance.GetCurrentYearID());
                Session["CurrentYearID"] = nYearId;
            }
            if (Session["LoggedInTeacher"] == null)
            {
                //Response.Redirect("LoginForm.aspx");
                //return;
            }
            /*if (Request.QueryString["ClassID"] == null /* Session["ClassID"] == null)
            {
                Session["ClassID"] = 1;
                //Response.Redirect("MainForm.aspx");
                //return;
            }*/
            if (!IsPostBack)
            {
                strPreviousPage = Request.UrlReferrer?.ToString() ?? "LoginForm.aspx";

            }
            LoadClassSchedule();
            FillStudents();
        }

        private void LoadClassSchedule()
        {
            strClassID = Request.QueryString["ClassID"]?.ToString() ?? "5";
            dtDays = DBConnection.Instance.GetAllDataFromTable("days", string.Empty);
            dtHours = DBConnection.Instance.GetConstraintDataTable("hours_in_day");
            dtCourses = DBConnection.Instance.GetConstraintDataTable("courses");

            BuildEmptySchedule();
            FillSchedule();
            FillAndAddGrid();

        }

        private void BuildEmptySchedule()
        {
            dtScheduleTable = new DataTable("Schedule");
            dtScheduleTable.Columns.Add("hour_id", typeof(string));
            dtScheduleTable.Columns.Add("days_hours", typeof(string));

            //Setting days columns
            foreach (DataRow drCurrentDay in dtDays.Rows)
            {
                dtScheduleTable.Columns.Add(drCurrentDay["id"].ToString(), typeof(string));
            }
            ///////////////////////////////

            //Setting days Rows
            DataRow drNewRow = dtScheduleTable.NewRow();
            drNewRow["days_hours"] = "ימים/שעות";
            foreach (DataRow drCurrentDay in dtDays.Rows)
            {
                drNewRow[drCurrentDay["id"].ToString()] = drCurrentDay["day_name"].ToString();
            }
            dtScheduleTable.Rows.Add(drNewRow);
            ////////////////////////////////////////////////////////

            //Setting Hours Rows
            foreach (DataRow drCurrentHour in dtHours.Rows)
            {
                drNewRow = dtScheduleTable.NewRow();
                drNewRow["hour_id"] = drCurrentHour["id"].ToString();
                drNewRow["days_hours"] = drCurrentHour["name"].ToString();
                dtScheduleTable.Rows.Add(drNewRow);

            }
            //////////////////////////////////////////////////
        }

        private void FillSchedule()
        {
            DataTable dtClassSchedule = DBConnection.Instance.GetDataTableByQuery(
                "select * from classes_schedule where class_id = " + strClassID);
            DataTable dtStudentsSchedule = DBConnection.Instance.GetDataTableByQuery(
            "SELECT * FROM students_schedule " +
            " inner join students on students.id = students_schedule.student_id" +
            " where students.class_id = " + strClassID);
            foreach (DataRow drCurrentHour in dtClassSchedule.Rows)
            {
                string strCourseName = dtCourses.Select("id = " + drCurrentHour["course_id"].ToString())[0]["name"].ToString() +
                                       "*" + drCurrentHour["hour_id"] + "-" + drCurrentHour["day_id"];
                DataRow[] drStudentScheduleRows = dtStudentsSchedule.Select("hour_id = " + drCurrentHour["hour_id"].ToString() + " and day_id = " + drCurrentHour["day_id"].ToString());
                if (drStudentScheduleRows.Count() > 0)
                {

                    strCourseName += "$";
                }
                dtScheduleTable.Select("hour_id = " + drCurrentHour["hour_id"].ToString())[0][drCurrentHour["day_id"].ToString()] = strCourseName;
            }

            

        }

        private void FillAndAddGrid()
        {
            GridView gvScheduleView = new GridView()
            {
                ShowHeader = false,
                CssClass = "table table-bordered table-sm",
                DataSource = dtScheduleTable
            };
            gvScheduleView.DataBind();

            foreach (GridViewRow gvrCurrentRow in gvScheduleView.Rows)
            {
                foreach (TableCell tcCurrentCell in gvrCurrentRow.Cells)
                {
                    tcCurrentCell.Text = tcCurrentCell.Text.Replace("&lt;br&gt;", "<br>");
                    tcCurrentCell.HorizontalAlign = HorizontalAlign.Center;
                    if (tcCurrentCell.Text.Contains("*"))
                    {
                        Label lbl = new Label() { Text = tcCurrentCell.Text.Split('*')[0] };
                        tcCurrentCell.Attributes.Add("onClick", "OnClick(\"GroupsForm.aspx?IDs=" + strClassID + "-" + tcCurrentCell.Text.Split('*')[1].Replace("$", "") + "\",1200,900,\"yes\");");
                        tcCurrentCell.Controls.Add(lbl);

                        if (tcCurrentCell.Text.Contains("$"))
                        {
                            //tcCurrentCell.Text = tcCurrentCell.Text.Replace("$", "");
                            LinkButton lbGroupLinkButton = new LinkButton()
                            {
                                Text = "<br>קבוצות",
                                ID = tcCurrentCell.Text.Split('*')[1],
                                OnClientClick = "genericPopup(\"GroupsForm.aspx?IDs=" + strClassID + "-" + tcCurrentCell.Text.Split('*')[1].Replace("$", "") + "\",1200,900,\"yes\")"
                            };
                            tcCurrentCell.Controls.Add(lbGroupLinkButton);
                        }
                    }
                }
                gvrCurrentRow.Cells[0].Visible = false;
            }
            pnlSchedule.Controls.Add(gvScheduleView);
        }

        private void FillStudents()
        {
            DataTable dtStudents = DBConnection.Instance.GetDataTableByQuery("select id,concat(first_name,' ' ,last_name) as name,picture_path from students where class_id = " + strClassID);

            foreach (DataRow drCurrentStudent in dtStudents.Rows)
            {
                string strFigure = "<a href=\"StudentDetailsForm.aspx?StudentID="+ drCurrentStudent["id"] + "\" class=\"col-12 col-md-2 col-sm-4\">"+
                                   "<figure > <img src=\"" + drCurrentStudent["picture_path"] + "\" width=\"100\">"+
                                   "<figcaption>" + drCurrentStudent["name"] + "</figcaption></figure></a>";
                pnlStudents.Controls.Add(new LiteralControl(strFigure));
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void GoBack()
        {
            Response.Redirect(strPreviousPage);
        }
    }
}