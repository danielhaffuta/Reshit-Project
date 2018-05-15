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
    public partial class AddCourseForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["CourseID"] == null)
            {
                Session["CourseID"] = "AddGroup";
            }
            if (Session["CourseID"].ToString().Equals("AddGroup"))
            {
                Course.Text = "שם הקבוצה:";
            }
            if (!IsPostBack)
            {
                string strTeacherQuery = DBConnection.Instance.GetDisplayQuery("teachers");
                strTeacherQuery += " where year_id = " + nYearID;
                //string TeacherQuery = "SELECT CONCAT(first_name, ' ',last_name) AS full_name, id FROM teachers";
                DataTable dtTeacherTable = DBConnection.Instance.GetDataTableByQuery(strTeacherQuery);

                TeachersList.DataSource = dtTeacherTable;
                TeachersList.DataValueField = "id";
                TeachersList.DataTextField = "name";
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