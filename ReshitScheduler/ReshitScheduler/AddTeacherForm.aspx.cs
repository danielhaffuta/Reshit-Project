using Data;
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
    public partial class AddTeacherForm : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string TTQuery = "SELECT * FROM teacher_types where teacher_type_name <> 'admin'";
                DataTable TTtable = DBConnection.Instance().GetDataTableByQuery(TTQuery);

                JobDescription.DataSource = TTtable;
                JobDescription.DataValueField = "id";
                JobDescription.DataTextField = "teacher_type_name";
                JobDescription.AutoPostBack = true;
                JobDescription.DataBind();

                string YearsQuery = "SELECT id,hebrew_year FROM years";
                DataTable YearTable = DBConnection.Instance().GetDataTableByQuery(YearsQuery);

                JoinYear.DataSource = YearTable;
                JoinYear.DataValueField = "id";
                JoinYear.DataTextField = "hebrew_year";
                JoinYear.AutoPostBack = true;
                JoinYear.DataBind();
//              string test = "SELECT * FROM teachers";
//              DataTable testTable = DBConnection.Instance().GetDataTableByQuery(test);
            }
        }

        protected void SaveClick(object sender, EventArgs e)
        {
            string values = "'"+TeacherName.Text+"','"
                            + TeacherLastName.Text + "',"
                            + JobDescription.SelectedValue + ",'"
                            + UserName.Text + "','"
                            + Password.Text + "',"
                            + JoinYear.SelectedValue;
            string fields = "first_name,last_name,teacher_type_id,user_name,password,year_id";

            bool res = DBConnection.Instance().InsertTableRow("teachers", fields, values);

            cleanFields(sender, e);

        }

        private void cleanFields(object sender, EventArgs e)
        {
            TeacherName.Text = "";
            TeacherLastName.Text = "";
            JobDescription.SelectedIndex = 0;
            UserName.Text = "";
            JoinYear.SelectedIndex = 0;
        }
    }
}