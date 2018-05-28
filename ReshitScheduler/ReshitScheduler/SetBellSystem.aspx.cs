
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
                //string YearsQuery = "SELECT id,hebrew_year FROM years";
                //DataTable YearTable = DBConnection.Instance.GetDataTableByQuery(YearsQuery);

                //JoinYear.DataSource = YearTable;
                //JoinYear.DataValueField = "id";
                //JoinYear.DataTextField = "hebrew_year";
                //JoinYear.AutoPostBack = true;
                //JoinYear.DataBind();

                //string test = "SELECT * FROM hours_in_day";
                //DataTable testTable = DBConnection.Instance.GetDataTableByQuery(test);
            }
        }
        private void showHours()
        {
            DataTable dtHours = DBConnection.Instance.GetDataTableByQuery("select * from hours_in_day " +
                "where year_id = (select value from preferences where name='current_year_id')" +
                "order by start_time");
            gvHours.DataSource = dtHours;
            gvHours.DataBind();
            int nCurrentHour = 1;
            int isBreak;
            foreach (GridViewRow grCurrentRow in gvHours.Rows)
            {
                int.TryParse(grCurrentRow.Cells[3].Text, out isBreak);
                if (isBreak == 1)
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

        }

        protected void IsBreak_changed(object sender, EventArgs e)
        {
            CheckIfBreakNewHour = true;
        }

        protected void NotBreak_changed(object sender, EventArgs e)
        {
            CheckIfBreakNewHour = false;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {

            //string fields = "hour_of_school_day,start_time,finish_time,is_break,year_id";
            //int hour_in_day = 1;
            //bool isFilled = false;
            //foreach (TableRow row in BellTable.Rows)
            //{
            //    string values = hour_in_day.ToString() + ",";
            //    isFilled = false;
            //    foreach (TableCell cell in row.Cells)
            //    {
            //        foreach (Control ctrl in cell.Controls)
            //        {
            //            //CONTROL IS TEXBOXT: EXTRACT VALUES//
            //            if (ctrl is TextBox)
            //            {
            //                TextBox txt = (TextBox)ctrl;
            //                if (txt.Text != "" && txt.Text != null)
            //                {
            //                    values += "'" + txt.Text + "',";
            //                    txt.Text = "";
            //                    isFilled = true;
            //                }
            //            }
            //        }
            //    }
            //    if (isFilled)
            //    {
            //        if(hour_in_day == 3 || hour_in_day == 5 || hour_in_day == 8 || hour_in_day == 10 || hour_in_day == 13)
            //        {
            //            values += 1 + ",";
            //        }
            //        else
            //            values += 0 + ",";
            //        hour_in_day++;
            //        values += JoinYear.SelectedValue;
            //        bool res = DBConnection.Instance.InsertTableRow("hours_in_day", fields, values);
            //        if (!res)
            //        {
            //            Helper.ShowMessage(ClientScript, "error saving bell system information");
            //        }
            //    }
            //}
            //JoinYear.SelectedIndex = 0;
        }

        protected void BtnSaveBellSystem_Click(object sender, EventArgs e)
        {

        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}