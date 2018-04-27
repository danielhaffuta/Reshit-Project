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
    public partial class AddCourseForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string TeacherQuery = "SELECT CONCAT(first_name, ' ',last_name) AS full_name, id FROM teachers";
            DataTable TeacherTable = DBConnection.Instance().GetDataTableByQuery(TeacherQuery);

            TeachersList.DataSource = TeacherTable;
            TeachersList.DataValueField = "id";
            TeachersList.DataTextField = "full_name";
            TeachersList.AutoPostBack = true;
            TeachersList.DataBind();

            string YearsQuery = "SELECT id,hebrew_year FROM years";
            DataTable YearTable = DBConnection.Instance().GetDataTableByQuery(YearsQuery);

            JoinYear.DataSource = YearTable;
            JoinYear.DataValueField = "id";
            JoinYear.DataTextField = "hebrew_year";
            JoinYear.AutoPostBack = true;
            JoinYear.DataBind();
        }
    }
}