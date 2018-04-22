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
    public partial class ClassPage : System.Web.UI.Page
    {
        public static Teacher LoggedInTeacher;
        private int nYearId;


        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["LoggedInTeacher"] == null)
            {
                Response.Redirect("LoginForm.aspx");
                return;
            }
            if (Session["ClassID"] == null)
            {
                Response.Redirect("MainForm.aspx");
                return;
            }
            LoadClassSchedule();
        }

        private void LoadClassSchedule()
        {
            string strClassID = Session["ClassID"].ToString();
            nYearId = Convert.ToInt32(DBConnection.Instance().GetStringByQuery("select year_id from classes where classes.id = " + strClassID));
            DBConnection.Instance().GetDataTableByQuery(
                "select * from schedule where class_id = " + strClassID);
            BuildEmptySchedule();
        }

        private void BuildEmptySchedule()
        {
            DataTable dtDays = DBConnection.Instance().GetAllDataFromTable("days");
            string strGetDisplayHoursQuery = "SELECT display_column_query " +
                                          "FROM tables_information " +
                                           "where table_name = 'hours_in_day'";
            string strDisplayQuery = DBConnection.Instance().GetStringByQuery(strGetDisplayHoursQuery);
            DataTable dtHours = DBConnection.Instance().GetDataTableByQuery(strDisplayQuery);
            DataTable dtCourses = DBConnection.Instance().GetConstraintDataTable("courses");

            DataTable dtSchedule = new DataTable("Schedule");
            dtSchedule.Columns.Add("hour_id", typeof(string));
            dtSchedule.Columns.Add("days_hours", typeof(string));
            
            foreach (DataRow drCurrentDay in dtDays.Rows)
            {
                dtSchedule.Columns.Add(drCurrentDay["id"].ToString(), typeof(string));
            }
            DataRow drNewRow = dtSchedule.NewRow();
            drNewRow["days_hours"] = "ימים/שעות";

            foreach (DataRow drCurrentDay in dtDays.Rows)
            {
                drNewRow[drCurrentDay["id"].ToString()] = drCurrentDay["day_name"].ToString();
            }


            dtSchedule.Rows.Add(drNewRow);
            foreach (DataRow drCurrentHour in dtHours.Rows)
            {
                drNewRow = dtSchedule.NewRow();
                drNewRow["hour_id"] = drCurrentHour["id"].ToString();
                drNewRow["days_hours"] = drCurrentHour["name"].ToString();
                dtSchedule.Rows.Add(drNewRow);

            }
            string strClassID = Session["ClassID"].ToString();
            strClassID = "3";
            DataTable dtSchedule1 = DBConnection.Instance().GetDataTableByQuery(
                "select * from schedule where class_id = " + strClassID);
            foreach (DataRow drCurrentHour in dtSchedule1.Rows)
            {
                dtSchedule.Select("hour_id = " + drCurrentHour["hour_id"].ToString())[0][drCurrentHour["day_id"].ToString()] = 
                    dtCourses.Select("id = "+ drCurrentHour["course_id"].ToString())[0]["name"].ToString();
            }
            GridView gvScheduleView = new GridView()
            {
                ShowHeader = false,
                CssClass = "myGridClass"
            };
            gvScheduleView.DataSource = dtSchedule;
            gvScheduleView.DataBind();
            foreach (GridViewRow gvrCurrentRow in gvScheduleView.Rows)
            {
                gvrCurrentRow.Cells[0].Visible = false;
            }

            this.form1.Controls.Add(gvScheduleView);
            gvScheduleView.CssClass = "myGridClass";

            Button btnBack = new Button() { Text = "Back" };
            btnBack.Click += BtnBack_Click;
            form1.Controls.Add(new LiteralControl("<br />"));
            form1.Controls.Add(btnBack);

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}