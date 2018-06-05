using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class EditStudentEvaluations : BasePage
    {
        private int nStudentID;
        protected DataRow drStudentDetails;
        private DataTable dtStudentEvaluations;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["StudentID"] != null)
            {
                nStudentID = Convert.ToInt32(Request.QueryString["StudentID"]?.ToString() ?? "5");
                drStudentDetails = DBConnection.Instance.GetStudentDetails(nStudentID);
                name.InnerText = drStudentDetails["name"].ToString();
                
                if(!IsPostBack)
                {
                    string strStudentQuery = "select students.id as id, concat(students.last_name,' ' ,students.first_name) as name" + 
                                        " from students" +
                                        " inner join students_classes on students_classes.student_id = students.id" +
                                        " where students_classes.class_id = " + nClassID +
                                        " order by last_name";
                    DataTable dtStudents = DBConnection.Instance.GetDataTableByQuery(strStudentQuery);
                    ddlStudents.DataSource = dtStudents;
                    ddlStudents.DataValueField = "id";
                    ddlStudents.DataTextField = "name";
                    ddlStudents.AutoPostBack = true;
                    ddlStudents.DataBind();
                    FillEvaluations();
                }
            }
        }
        private void FillEvaluations()
        {
            dtStudentEvaluations = DBConnection.Instance.GetStudentEvaluationsForEdit(nStudentID);
            gvEvaluations.DataSource = dtStudentEvaluations;
            gvEvaluations.DataBind();
            gvEvaluations.Columns[2].Visible = false;
            gvEvaluations.Columns[3].Visible = false;
            gvEvaluations.Columns[4].Visible = false;
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["StudentID"] != null)
                Response.Redirect("StudentDetailsForm.aspx?StudentID=" + nStudentID);
        }

        protected void ddlStudents_changed(object sender, EventArgs e)
        {
            nStudentID = Convert.ToInt32(ddlStudents.SelectedValue);
            Response.Redirect("EditStudentEvaluations.aspx?StudentID=" + nStudentID);
        }

        protected void txtEvaluation_TextChanged(object sender, EventArgs e)
        {
            TextBox txtChangedEvaluation = sender as TextBox;
            GridViewRow gvrChangedRow = txtChangedEvaluation.Parent.Parent as GridViewRow;
            int nLessonID;
            int.TryParse(gvrChangedRow.Cells[4].Text, out nLessonID);
            int buffer;
            int.TryParse(gvrChangedRow.Cells[2].Text, out buffer);
            bool IsGroup = Convert.ToBoolean(buffer);
            int nEvaluationID;
            if (int.TryParse(gvrChangedRow.Cells[3].Text, out nEvaluationID))
            {
                DBConnection.Instance.UpdateTableRow(IsGroup ? "groups_evaluations" : "courses_evaluations",
                                           nEvaluationID,
                                          "evaluation",
                                          "'" + (gvrChangedRow.Cells[1].Controls[1] as TextBox).Text.Replace("'", "''") + "'");
            }
            else
            {
                int semester = Convert.ToInt32(DBConnection.Instance.GetSemester());
                DBConnection.Instance.InsertTableRow(IsGroup ? "groups_evaluations" : "courses_evaluations",
                                      "evaluation,student_id," + (IsGroup ? "group_id" : "course_id")+",semester_number",
                                      "'" + (gvrChangedRow.Cells[1].Controls[1] as TextBox).Text.Replace("'", "''") + "'," +
                                      nStudentID + "," + nLessonID + "," + semester);
            }
            return;
        }
    }
}