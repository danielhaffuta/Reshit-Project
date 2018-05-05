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
            if (Session["CourseID"].ToString().Equals("AddGroup"))
            {
                Course.Text = "שם הקבוצה:";
            }
            if (!IsPostBack)
            {
                string TeacherQuery = "SELECT CONCAT(first_name, ' ',last_name) AS full_name, id FROM teachers";
                DataTable TeacherTable = DBConnection.Instance.GetDataTableByQuery(TeacherQuery);

                TeachersList.DataSource = TeacherTable;
                TeachersList.DataValueField = "id";
                TeachersList.DataTextField = "full_name";
                TeachersList.AutoPostBack = true;
                TeachersList.DataBind();

                //string test = "SELECT * FROM groups";
                //DataTable testTable = DBConnection.Instance.GetDataTableByQuery(test);
            }
        }
        protected void SaveClick(object sender, EventArgs e)
        {
            string values = "'" + CourseName.Text + "' ,"
                            + TeachersList.SelectedValue ;
            string fields = "";
            string tableName = "";
            if (Session["CourseID"].ToString().Equals("AddGroup"))
            {
                tableName = "groups";
                fields = "group_name,teacher_id";
            }
            else
            {
                fields = "course_name,teacher_id";
                tableName = "courses";
            }
            bool res = DBConnection.Instance.InsertTableRow(tableName, fields, values);
            if (!res)
            {
                Helper.ShowMessage(ClientScript, GetType(), "error saving course information");
            }
            CourseName.Text = "";
            TeachersList.SelectedIndex = 0;
        }
        protected void BackClick(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}