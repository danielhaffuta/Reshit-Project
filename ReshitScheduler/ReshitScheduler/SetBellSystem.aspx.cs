
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
            string[] strFields = { "hour_of_school_day","start_time","finish_time","is_break","year_id" };
            int hour_of_school_day = Convert.ToInt32(DBConnection.Instance.GetMaxHourOfSchoolDay()) + 1;
            string[] strValues = new String[5];
            strValues[0] = Convert.ToString(hour_of_school_day);
            strValues[1] = StartTime.Text;
            strValues[2] = EndTime.Text;
            if(CheckIfBreakNewHour)
            {
                strValues[3] = "1";
                IsBreak.Checked = false;
            }
            else
            {
                strValues[3] = "0";
                NotBreak.Checked = false;
            }
            strValues[4] = "(select value from preferences where name = 'current_year_id')";
            int nInsertSucceeded = DBConnection.Instance.InsertTableRow("hours_in_day", strFields, strValues);
            if (nInsertSucceeded == 0)
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
                if (((CheckBox)grCurrentRow.FindControl("CheckIfBreak")).Checked)
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
            Helper.ShowMessage(ClientScript, "מערכת צלצולים נשמרה");
            showHours();
        }

        protected void BtnDeleteHour(object sender, EventArgs e)
        {
            nHourId = Convert.ToInt32((((sender as Button).Parent as DataControlFieldCell).Parent as GridViewRow).Cells[5].Text);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                DeleteHour(nHourId);
            }
        }

        private void DeleteHour(int nHourID)
        {
            DataTable dtCheckTables;
            int nCurrentYearID = DBConnection.Instance.GetCurrentYearID();
            dtCheckTables = DBConnection.Instance.GetDataTableByQuery("select students_schedule.hour_id from students_schedule" + 
                                                                    " inner join hours_in_day on students_schedule.hour_id = hours_in_day.id" +
                                                                    " where students_schedule.hour_id = " + nHourID +
                                                                    " and hours_in_day.year_id = " + nCurrentYearID);
            if (dtCheckTables.Rows.Count != 0)
            {
                Helper.ShowMessage(ClientScript, "לא ניתן למחוק שעה בשימוש");
                showHours();
                return;
            }
            dtCheckTables = DBConnection.Instance.GetDataTableByQuery("select classes_schedule.hour_id from classes_schedule" +
                                                                    " inner join hours_in_day on classes_schedule.hour_id = hours_in_day.id" +
                                                                    " where classes_schedule.hour_id = " + nHourID +
                                                                    " and hours_in_day.year_id = " + nCurrentYearID);

            if (dtCheckTables.Rows.Count != 0)
            {
                Helper.ShowMessage(ClientScript, "לא ניתן למחוק שעה בשימוש");
                showHours();
                return;
            }
            string strDeleteQuery = " delete from hours_in_day where id = " + nHourID +
                " and year_id = " + nCurrentYearID + ";";
            DBConnection.Instance.ExecuteNonQuery(strDeleteQuery);
            showHours();
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }


    }
}