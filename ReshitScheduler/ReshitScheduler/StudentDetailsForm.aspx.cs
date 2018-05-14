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
        private DataTable dtDays;
        private DataTable dtHours;
        private DataTable dtCourses;
        private DataTable dtGroups;
        private DataTable dtScheduleTable;
        //protected DataTable dtStudentDetails;
        protected DataRow drStudentDetails;


        private string strStudentID;

        private static string strPreviousPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strPreviousPage = Request.UrlReferrer?.ToString() ?? "LoginForm.aspx";

            }
            strStudentID = Request.QueryString["StudentID"]?.ToString() ?? "5";

            drStudentDetails = DBConnection.Instance.GetDataTableByQuery(" select concat(students.first_name,' ' ,students.last_name) as name," +
                                                                            " picture_path,concat(grades.grade_name,classes.class_number) as class," +
                                                                            " classes.id as class_id, students.id as student_id," +
                                                                            " students.mother_cellphone, students.mother_full_name," +
                                                                            " students.father_cellphone, students.father_full_name," +
                                                                            " students.home_phone, students.parents_email," +
                                                                            " students.settlement" +
                                                                            " from students " +
                                                                            " inner join students_classes on students_classes.student_id = students.id" +
                                                                            " inner join classes on classes.id = students_classes.class_id" +
                                                                            " inner join teachers on teachers.id = classes.teacher_id and teachers.year_id = " + nYearId +
                                                                            " inner join grades on grades.id = classes.grade_id" +
                                                                            " where students.id = " + strStudentID).Rows[0];

            dtDays = DBConnection.Instance.GetAllDataFromTable("days", string.Empty);
            dtHours = DBConnection.Instance.GetConstraintDataTable("hours_in_day", "where hours_in_day.year_id = " + nYearId, "order by hour_of_school_day");
            dtCourses = DBConnection.Instance.GetConstraintDataTable("courses");
            dtGroups = DBConnection.Instance.GetConstraintDataTable("groups");

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
                if (drCurrentHour["is_break"].ToString().Equals("1"))
                {
                    drNewRow["hour_id"] += "*";
                }
                dtScheduleTable.Rows.Add(drNewRow);

            }
            //////////////////////////////////////////////////
        }
        private void FillSchedule()
        {
            DataTable dtClassSchedule = DBConnection.Instance.GetDataTableByQuery(
                "select * from classes_schedule where class_id = " + drStudentDetails["class_id"]);
            DataTable dtStudentsSchedule = DBConnection.Instance.GetDataTableByQuery(
                "select * from students_schedule where student_id = " + strStudentID);
            foreach (DataRow drCurrentHour in dtClassSchedule.Rows)
            {
                string strCourseName;

                DataRow[] drStudentScheduleRows = dtStudentsSchedule.Select("hour_id = " + drCurrentHour["hour_id"].ToString() + " and day_id = " + drCurrentHour["day_id"].ToString());
                if (drStudentScheduleRows.Count() > 0)
                {
                    strCourseName = dtGroups.Select("id = " + drStudentScheduleRows[0]["group_id"])[0]["name"] + "?GroupID=" + drStudentScheduleRows[0]["group_id"]; 
                }
                else
                {
                    strCourseName = dtCourses.Select("id = " + drCurrentHour["course_id"])[0]["name"] + "?CourseID=" + drCurrentHour["course_id"];
                }
                dtScheduleTable.Select(("hour_id = '" + drCurrentHour["hour_id"]).Replace("*","")+"'")[0][drCurrentHour["day_id"].ToString()] = strCourseName + "-" + drCurrentHour["day_id"] + drCurrentHour["hour_id"];
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
                if (gvrCurrentRow.Cells[0].Text.Contains("*"))
                {
                    gvrCurrentRow.Cells[0].Text.Replace("*", "");
                    gvrCurrentRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    gvrCurrentRow.CssClass = "bg-light";
                    gvrCurrentRow.Cells[2].ColumnSpan = 6;
                    gvrCurrentRow.Cells[2].Text = "הפסקה";
                    gvrCurrentRow.Cells[2].CssClass = "h5 text-center";

                    gvrCurrentRow.Cells.RemoveAt(7);
                    gvrCurrentRow.Cells.RemoveAt(6);
                    gvrCurrentRow.Cells.RemoveAt(5);
                    gvrCurrentRow.Cells.RemoveAt(4);
                    gvrCurrentRow.Cells.RemoveAt(3);
                }
                else
                {
                    foreach (TableCell tcCurrentCell in gvrCurrentRow.Cells)
                    {
                        tcCurrentCell.Text = tcCurrentCell.Text.Replace("&lt;br&gt;", "<br>");
                        tcCurrentCell.HorizontalAlign = HorizontalAlign.Center;
                        if (tcCurrentCell.Text.Contains("?"))
                        {
                            LinkButton lbEvaluationLinkButton = new LinkButton()
                            {
                                Text = tcCurrentCell.Text.Split('?')[0],
                                ID = tcCurrentCell.Text.Split('?')[1],
                                //OnClientClick = "genericPopup(\"GroupsForm.aspx?IDs=" + strClassID + "-" + tcCurrentCell.Text.Split('*')[1].Replace("$", "") + "\",1200,900,\"yes\")"
                            };
                            lbEvaluationLinkButton.Click += LbEvaluationLinkButton_Click;
                            tcCurrentCell.Controls.Add(lbEvaluationLinkButton);
                        }
                    }
                    
                }
                gvrCurrentRow.Cells[0].Visible = false;
                pnlSchedule.Controls.Add(gvScheduleView);
            }
        }

        private void LbEvaluationLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("EvaluationForm.aspx?StudentID=" + strStudentID + "&" + (sender as LinkButton).ID.Split('-')[0]);
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }
        private void GoBack()
        {
            Response.Redirect("ClassPage.aspx?StudentID=" + strStudentID+ "&ClassID=" + drStudentDetails["class_id"]);
        }
    }
}