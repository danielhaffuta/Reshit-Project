
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class EditClassDetails : BasePage
    {
        private DataTable dtTeachers;
        private DataTable dtClasses;
        private DataTable dtGrades;
        private bool EducatorChanged = false;
        private bool bClassDeleted = false;
        private static int nCurrentEducatorID;

        protected void Page_Load(object sender, EventArgs e)
        {
            dtTeachers = DBConnection.Instance.GetThisYearTeachers();
            dtClasses = DBConnection.Instance.GetThisYearClasses();
            dtGrades = DBConnection.Instance.GetGrades();


            if (!IsPostBack)
            {
                ddlClasses.DataSource = dtClasses;
                ddlClasses.DataValueField = "class_id";
                ddlClasses.DataTextField = "name";
                ddlClasses.DataBind();

                ddlTeachers.DataSource = dtTeachers;
                ddlTeachers.DataValueField = "id";
                ddlTeachers.DataTextField = "name";
                ddlTeachers.DataBind();

                ddlGrades.DataSource = dtGrades;
                ddlGrades.DataValueField = "id";
                ddlGrades.DataTextField = "name";
                ddlGrades.DataBind();

            }
            nClassID = Convert.ToInt32(ddlClasses.SelectedValue);
            if (!IsPostBack)
            {
                FillClassDetails();

            }
        }

        private void FillClassDetails()
        {
            DataTable dtClassesDetails = DBConnection.Instance.GetThisYearClassesDetails();
            DataRow drClassDetails = dtClassesDetails.Select("class_id = " + nClassID)[0];
            ddlGrades.SelectedValue = drClassDetails["grade_id"].ToString();
            ClassNum.Text = drClassDetails["class_number"].ToString();
            ddlTeachers.SelectedValue = drClassDetails["teacher_id"].ToString();
            nCurrentEducatorID = Convert.ToInt32(drClassDetails["teacher_id"]);
        }

        protected void ddlClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillClassDetails();
        }

        protected void ddlTeachers_SelectedIndexChanged(object sender, EventArgs e)
        {
            EducatorChanged = true;
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }
        private void GoBack()
        {
            Response.Redirect("MainForm.aspx");
        }
        protected void BtnUpdateClass_Click(object sender, EventArgs e)
        {
            int nGradeID = Convert.ToInt32(ddlGrades.SelectedValue);
            int nClassNumber = Convert.ToInt32(ClassNum.Text);
            DataTable dtClassCheck = DBConnection.Instance.GetThisYearClassesDetails();
            if (dtClassCheck.Select("grade_id = " + nGradeID +
                                    " AND class_number = " + nClassNumber +
                                    " AND class_id <> " + nClassID).Count() > 0)
            {
                Helper.ShowMessage(ClientScript, "כיתה כבר קיימת - לא ניתן לערוך כיתה");
                return;
            }
            if(EducatorChanged)
            {
                int nNewEducatorID = Convert.ToInt32(ddlTeachers.SelectedValue);
                string newTecherType = DBConnection.Instance.GetStringByQuery("select teacher_type_id from teachers where id = " + nNewEducatorID);
                if (newTecherType.Equals("4") || newTecherType.Equals("6"))
                {
                    bool bUpdateTypeSucceeded = DBConnection.Instance.UpdateTableRow("teachers", nNewEducatorID, "teacher_type_id", "2");
                    if (!bUpdateTypeSucceeded)
                    {
                        Helper.ShowMessage(ClientScript, "error saving");
                        return;
                    }
                }
                DataTable dtCourses = DBConnection.Instance.GetDataTableByQuery("select * from courses where teacher_id = " + nCurrentEducatorID);
                foreach(DataRow drCurrentRow in dtCourses.Rows)
                {
                    bool bUpdateCoursesSucceeded = DBConnection.Instance.UpdateTableRow("courses", Convert.ToInt32(drCurrentRow[0]), "teacher_id", Convert.ToString(nNewEducatorID));
                    if (!bUpdateCoursesSucceeded)
                    {
                        Helper.ShowMessage(ClientScript, "error saving");
                        return;
                    }
                }
                DataTable dtGroups = DBConnection.Instance.GetDataTableByQuery("select * from groups where teacher_id = " + nCurrentEducatorID);
                foreach (DataRow drCurrentRow in dtGroups.Rows)
                {
                    bool bUpdateGroupsSucceeded = DBConnection.Instance.UpdateTableRow("groups", Convert.ToInt32(drCurrentRow[0]), "teacher_id", Convert.ToString(nNewEducatorID));
                    if (!bUpdateGroupsSucceeded)
                    {
                        Helper.ShowMessage(ClientScript, "error saving");
                        return;
                    }
                }
            }
            string strFields = "grade_id:class_number:teacher_id";
            string strValues = ddlGrades.SelectedValue + ":'" +
                               ClassNum.Text + "':" +
                               ddlTeachers.SelectedValue;

            bool bUpdateSucceeded = DBConnection.Instance.UpdateTableRow("classes", nClassID, strFields, strValues);
            if (!bUpdateSucceeded)
            {
                Helper.ShowMessage(ClientScript, "error saving");
            }
            Helper.ShowMessage(ClientScript, "נשמר");
            ResetClassDetails();

        }

        protected void BtnDeleteClass_Click(object sender, EventArgs e)
        {
            bClassDeleted = true;
            string strDeleteQuery = "delete from classes where id = " + nClassID +
                         " and teacher_id in(select id from teachers where year_id = " + DBConnection.Instance.GetCurrentYearID() + ");";
            DBConnection.Instance.ExecuteNonQuery(strDeleteQuery);
            Helper.ShowMessage(ClientScript, "כיתה נמחקה");
            ResetClassDetails();
        }

        private void ResetClassDetails()
        {
            if (bClassDeleted)
            {
                dtClasses = DBConnection.Instance.GetThisYearClasses();
                ddlClasses.DataSource = dtClasses;
                ddlClasses.DataValueField = "class_id";
                ddlClasses.DataTextField = "name";
                ddlClasses.DataBind();
            }
            ddlClasses.SelectedIndex = 0;
            nClassID = Convert.ToInt32(ddlClasses.SelectedValue);
            FillClassDetails();
        }
    }
}