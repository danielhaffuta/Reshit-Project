﻿
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
        private bool bChangeLessons = false;
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
            pnlChangeEducator.Visible = true;
            EducatorChanged = true;
        }

        private void AddRadioButtons()
        {
            Label lbQuestion = new Label()
            {
                ID = "ChangeLessons",
                Text = "האם תרצה להעביר למורה זה גם קורסים וקבוצות?",
                CssClass = "col-form-label col-sm-3 col-md-4",
            };
            pnlChangeEducator.Controls.Add(lbQuestion);
            Label lbChange = new Label()
            {
                CssClass = "form-check-label",
            };
            pnlChangeEducator.Controls.Add(lbChange);
            RadioButton rbChange = new RadioButton()
            {
                ID = "ReplaceLessons",
                Text = "כן",
                CssClass = "form-check-input",
                GroupName = "Replace",
                Checked = true
            };
            rbChange.AutoPostBack = true;
            lbChange.Controls.Add(rbChange);
            Label lbNotChange = new Label()
            {
                CssClass = "form-check-label",
            };
            pnlChangeEducator.Controls.Add(lbNotChange);
            RadioButton rbNotChange = new RadioButton()
            {
                ID = "NotReplaceLessons",
                Text = "לא",
                CssClass = "form-check-input",
                GroupName = "Replace"
            };
            rbNotChange.AutoPostBack = true;
            lbNotChange.Controls.Add(rbNotChange);
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
            if (EducatorChanged)
            {
                int nNewEducatorID = Convert.ToInt32(ddlTeachers.SelectedValue);
                string newTecherType = DBConnection.Instance.GetStringByQuery("select teacher_type_name from teacher_types" +
                                                                                " inner join teachers on teachers.teacher_type_id = teacher_types.id" +
                                                                                " where teachers.id = " + nNewEducatorID);
                if (newTecherType.Equals("מורה") || newTecherType.Equals("מורה לחינוך מיוחד"))
                {
                    string educatorType = DBConnection.Instance.GetStringByQuery("select id from teacher_types where teacher_type_name = 'מחנך'");
                    bool bUpdateTypeSucceeded = DBConnection.Instance.UpdateTableRow("teachers", nNewEducatorID, "teacher_type_id", educatorType);
                    if (!bUpdateTypeSucceeded)
                    {
                        Helper.ShowMessage(ClientScript, "error saving");
                        return;
                    }
                }
                DBConnection.Instance.UptadeTableCol("courses", "teacher_id", Convert.ToString(nCurrentEducatorID), Convert.ToString(nNewEducatorID));
                DBConnection.Instance.UptadeTableCol("groups", "teacher_id", Convert.ToString(nCurrentEducatorID), Convert.ToString(nNewEducatorID));

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
        protected void BtnDeleteClass(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                DeleteClass();
            }
        }

        private void DeleteClass()
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