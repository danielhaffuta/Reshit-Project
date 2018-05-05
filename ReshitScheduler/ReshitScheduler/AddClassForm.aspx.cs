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
    public partial class AddClassForm : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // grade dropdown list
            string gradeQuery = "SELECT * FROM grades";
            DataTable table = DBConnection.Instance.GetDataTableByQuery(gradeQuery);

            GradeList.DataSource = table;
            GradeList.DataValueField = "id";
            GradeList.DataTextField = "grade_name";
            GradeList.AutoPostBack = true;
            GradeList.DataBind();

            // Educators List
            string EducatorsQuery = "SELECT CONCAT(first_name, ' ', last_name) AS full_name, id FROM teachers";
            DataTable EducatorsTable = DBConnection.Instance.GetDataTableByQuery(EducatorsQuery);

            EducatorsList.DataSource = EducatorsTable;
            EducatorsList.DataValueField = "id";
            EducatorsList.DataTextField = "full_name";
            EducatorsList.AutoPostBack = true;
            EducatorsList.DataBind();

            // Join Year List
            string JoinYearQuery = "SELECT * FROM years";
            DataTable JoinYearTable = DBConnection.Instance.GetDataTableByQuery(JoinYearQuery);

            JoinYear.DataSource = JoinYearTable;
            JoinYear.DataValueField = "id";
            JoinYear.DataTextField = "hebrew_year";
            JoinYear.AutoPostBack = true;
            JoinYear.DataBind();
        }

        protected void SaveClick(object sender, EventArgs e)
        {
            string values = GradeList.SelectedValue + ",'"
                            + ClassNum.Text + "',"
                            + EducatorsList.SelectedValue + ","
                            + JoinYear.SelectedValue;
            string fields = "grade_id, class_number, teacher_id, year_id";

            bool res = DBConnection.Instance.InsertTableRow("classes", fields, values);

            cleanFields(sender, e);            
        }

        private void cleanFields(object sender, EventArgs e)
        {
            GradeList.SelectedIndex = 0;
            ClassNum.Text = "";
            EducatorsList.SelectedIndex = 0;
            JoinYear.SelectedIndex = 0;
        }
    }
}