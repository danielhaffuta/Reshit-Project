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
    public partial class AddStudentForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ClassQuery = "SELECT classes.id, CONCAT(grades.grade_name,classes.class_number) as class"+
                                " FROM classes INNER JOIN grades on grades.id= classes.grade_id";
            DataTable ClassTable = DBConnection.Instance().GetDataTableByQuery(ClassQuery);

            ClassesList.DataSource = ClassTable;
            ClassesList.DataValueField = "id";
            ClassesList.DataTextField = "class";
            ClassesList.AutoPostBack = true;
            ClassesList.DataBind();

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