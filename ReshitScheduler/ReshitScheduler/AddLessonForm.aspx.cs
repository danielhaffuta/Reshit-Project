
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
        private DataTable dtCourses;
        private DataTable dtGroups;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["IsGroup"] == null || Request.QueryString["IsGroup"].Equals("true"))
            {
                IsGroup = true;
                AddLesson.InnerText = "הוספת קבוצה חדשה:";
                Course.Text = "שם הקבוצה:";
                CheckGroup.Text = "האם זו קבוצה עם הערכה?";
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
                gvLessons.DataSource = dtGroups;
            }
            else
            {
                dtCourses = DBConnection.Instance.GetThisYearCourses();
                gvLessons.DataSource = dtCourses;
            }
            gvLessons.DataBind();
            foreach(GridViewRow grCurrentRow in gvLessons.Rows )
            {
                if(grCurrentRow.Cells[2].Text.Equals("0"))
                {
                    grCurrentRow.Cells[2].Text = "כן";
                }
                else
                {
                    grCurrentRow.Cells[2].Text = "לא";
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string tableName = "";
            string tableFirstField = "";
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
            string[] strValues = new string[4];
            strValues[0] = CourseName.Text;
            strValues[1] = ddlTeachers.SelectedValue;
            strValues[2] = Convert.ToString(priority);
            if (HasEvaluation.Checked)
            {
                strValues[3] = "0";
            }
            else
            {
                strValues[3] = "1";
            }
            if (IsGroup)
            {
                tableName = "groups";
                tableFirstField = "group_name";
            }
            else
            {
                tableName = "courses";
                tableFirstField = "course_name";
            }
            string[] strFields = { tableFirstField, "teacher_id", "priority", "has_evaluation" };
            int nInsertSucceeded = DBConnection.Instance.InsertTableRow(tableName, strFields, strValues);
            if (nInsertSucceeded == 0)
            {
                Helper.ShowMessage(ClientScript,  "error saving lesson information");
            }
            Helper.ShowMessage(ClientScript, "נשמר");
            CourseName.Text = "";
            ddlTeachers.SelectedIndex = 0;
            FillLessons();
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}