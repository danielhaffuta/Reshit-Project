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
            DataTable dtHours = DBConnection.Instance().GetConstraintDataTable("hours_in_day");
            DataTable dtCourses = DBConnection.Instance().GetConstraintDataTable("courses");

            DataTable dtScheduleTable = new DataTable("Schedule");
            dtScheduleTable.Columns.Add("hour_id", typeof(string));
            dtScheduleTable.Columns.Add("days_hours", typeof(string));
            
            foreach (DataRow drCurrentDay in dtDays.Rows)
            {
                dtScheduleTable.Columns.Add(drCurrentDay["id"].ToString(), typeof(string));
            }
            DataRow drNewRow = dtScheduleTable.NewRow();
            drNewRow["days_hours"] = "ימים/שעות";

            foreach (DataRow drCurrentDay in dtDays.Rows)
            {
                drNewRow[drCurrentDay["id"].ToString()] = drCurrentDay["day_name"].ToString();
            }


            dtScheduleTable.Rows.Add(drNewRow);
            foreach (DataRow drCurrentHour in dtHours.Rows)
            {
                drNewRow = dtScheduleTable.NewRow();
                drNewRow["hour_id"] = drCurrentHour["id"].ToString();
                drNewRow["days_hours"] = drCurrentHour["name"].ToString();
                dtScheduleTable.Rows.Add(drNewRow);

            }
            string strClassID = Session["ClassID"].ToString();
            
            DataTable dtSchedule = DBConnection.Instance().GetDataTableByQuery(
                "select * from schedule where class_id = " + strClassID);
            foreach (DataRow drCurrentHour in dtSchedule.Rows)
            {
                dtScheduleTable.Select("hour_id = " + drCurrentHour["hour_id"].ToString())[0][drCurrentHour["day_id"].ToString()] = 
                    dtCourses.Select("id = "+ drCurrentHour["course_id"].ToString())[0]["name"].ToString();
            }
            GridView gvScheduleView = new GridView()
            {
                ShowHeader = false,
                CssClass = "myGridClass"
            };
            gvScheduleView.DataSource = dtScheduleTable;
            gvScheduleView.DataBind();
            int nCurrentRow = 0;
            foreach (GridViewRow gvrCurrentRow in gvScheduleView.Rows)
            {
                if (nCurrentRow % 2 == 0)
                    gvrCurrentRow.CssClass = "myAltRowClass";
                gvrCurrentRow.Cells[0].Visible = false;
                ++nCurrentRow;
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