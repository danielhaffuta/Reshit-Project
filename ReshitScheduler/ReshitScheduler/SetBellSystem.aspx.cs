
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class SetBellSystem : BasePage
    {
        private bool CheckIfBreakNewHour = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showHours();
            }
        }

        private void showHours()
        {
            gvHours.Columns[3].Visible = true;
            gvHours.Columns[5].Visible = true;
            DataTable dtHours = DBConnection.Instance.GetHoursDetails();
            gvHours.DataSource = dtHours;
            gvHours.DataBind();
            int nCurrentHour = 1;
            foreach (GridViewRow grCurrentRow in gvHours.Rows)
            {
                if (grCurrentRow.Cells[3].Text.Equals("1"))
                {
                    grCurrentRow.Cells[0].Text = "הפסקה";
                    ((CheckBox)grCurrentRow.FindControl("CheckIfBreak")).Checked = true;
                }
                else
                {
                    grCurrentRow.Cells[0].Text = nCurrentHour.ToString();
                    nCurrentHour++;
                }
            }
            gvHours.Columns[3].Visible = false;
            gvHours.Columns[5].Visible = false;
        }

        protected void IsBreak_changed(object sender, EventArgs e)
        {
            CheckIfBreakNewHour = true;
        }

        protected void NotBreak_changed(object sender, EventArgs e)
        {
            CheckIfBreakNewHour = false;
        }

        //private void CheckIfHourExist(string start_time)
        //{

        //    Helper.ShowMessage(ClientScript, "השעה הזאת מתנגשת עם שעה אחרת במערכתת לא ניתן להוסיף");
        //}

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string fields = "hour_of_school_day,start_time,finish_time,is_break,year_id";
            int hour_of_school_day = Convert.ToInt32(DBConnection.Instance.GetMaxHourOfSchoolDay()) + 1;
            string values = hour_of_school_day + "," +
                            "'" + StartTime.Text + "'," +
                            "'" + EndTime.Text + "',";
            if(CheckIfBreakNewHour)
            {
                values += "1,";
                IsBreak.Checked = false;
            }
            else
            {
                values += "0,";
                NotBreak.Checked = false;
            }
            values += "(select value from preferences where name = 'current_year_id')";
            bool bInsertSucceeded = DBConnection.Instance.InsertTableRow("hours_in_day", fields, values);
            if (!bInsertSucceeded)
            {
                Helper.ShowMessage(ClientScript, "error saving");
            }
            StartTime.Text = "";
            EndTime.Text = "";
            showHours();
            
        }

        protected void BtnSaveBellSystem_Click(object sender, EventArgs e)
        {
            string fields = "start_time,finish_time,is_break";
            string values;
            int nHourID;
            foreach (GridViewRow grCurrentRow in gvHours.Rows)
            {
                int.TryParse(grCurrentRow.Cells[5].Text, out nHourID);
                values = "'" + ((TextBox)grCurrentRow.Cells[1].FindControl("tblStartTime")).Text + "'," +
                            "'" + ((TextBox)grCurrentRow.Cells[2].FindControl("tblFinishTime")).Text + "',";
                if(((CheckBox)grCurrentRow.FindControl("CheckIfBreak")).Checked)
                {
                    values += "1";
                }
                else
                {
                    values += "0";
                }
                bool bInsertSucceeded = DBConnection.Instance.UpdateTableRow1("hours_in_day", nHourID, fields, values);
                if (!bInsertSucceeded)
                {
                    Helper.ShowMessage(ClientScript, "error saving bell system");
                }
            }
            showHours();
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}