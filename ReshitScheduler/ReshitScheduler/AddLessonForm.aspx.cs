
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
        protected bool IsGroup = false;
        private bool YesChecked = false;
        private DataTable dtCourses;
        private DataTable dtGroups;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["IsGroup"] == null || Request.QueryString["IsGroup"].Equals("true"))
            {
                IsGroup = true;
                AddLesson.InnerText = "הוספת קבוצה חדשה:";
                Course.Text = "שם הקבוצה:";
            }
            else
            {
                IsGroup = false;
            }

            if (!IsPostBack)
            {
                DataTable dtTeacherTable = DBConnection.Instance.GetThisYearTeachers();

                ddlTeachers.DataSource = dtTeacherTable;
                ddlTeachers.DataValueField = "id";
                ddlTeachers.DataTextField = "name";
                ddlTeachers.AutoPostBack = true;
                ddlTeachers.DataBind();

                FillLessons();
            }
        }

        private void FillLessons()
        {
            if (IsGroup)
            {
                dtGroups = DBConnection.Instance.GetThisYearGroups();
                BoundField bfGoal = new BoundField();
                bfGoal.HeaderText = "מטרת הקבוצה";
                bfGoal.DataField = "group_goal";
                bfGoal.HeaderStyle.Font.Bold = true;

                gvLessons.Columns.Add(bfGoal);
                gvLessons.DataSource = dtGroups;
            }
            else
            {
                dtCourses = DBConnection.Instance.GetThisYearCourses();
                gvLessons.DataSource = dtCourses;
            }
            gvLessons.DataBind();
        }

        protected void Yes_changed(object sender, EventArgs e)
        {
            YesChecked = true;
        }

        protected void No_changed(object sender, EventArgs e)
        {
            YesChecked = false;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string values = "'" + CourseName.Text + "',"
                            + ddlTeachers.SelectedValue ;
            string fields = "";
            string tableName = "";
            string strPriorityGroup = DBConnection.Instance.GetPriority("groups");
            string strPriorityCourse = DBConnection.Instance.GetPriority("courses");
            int groupP = Convert.ToInt32(strPriorityGroup);
            int courseP = Convert.ToInt32(strPriorityCourse);
            int priority;
            if(groupP > courseP)
            {
                priority = groupP+1;
            }
            else
            {
                priority = courseP+1;
            }
            if (IsGroup)
            {
                tableName = "groups";
                fields = "group_name,teacher_id,priority,group_goal";
                values += "," + priority + ",'" + GroupGoal.Text + "'";
            }
            else
            {
                fields = "course_name,teacher_id,also_group,priority";
                tableName = "courses";
                values += ","+ Convert.ToInt32(YesChecked) + "," + priority;
            }
            bool bInsertSucceeded = DBConnection.Instance.InsertTableRow(tableName, fields, values);
            if (!bInsertSucceeded)
            {
                Helper.ShowMessage(ClientScript,  "error saving lesson information");
            }
            CourseName.Text = "";
            ddlTeachers.SelectedIndex = 0;
            GroupGoal.Text = "";
            FillLessons();
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}