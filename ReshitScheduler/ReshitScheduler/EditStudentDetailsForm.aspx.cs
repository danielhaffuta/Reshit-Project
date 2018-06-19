
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class EditStudentDetailsForm : BasePage
    {
        protected DataRow drStudentDetails;
        private int nStudentID;

        protected void Page_Load(object sender, EventArgs e)
        {

            nStudentID = Convert.ToInt32(Request.QueryString["StudentID"]?.ToString() ?? "5");
            drStudentDetails = DBConnection.Instance.GetStudentDetails(nStudentID);
            if (!IsPostBack)
            {
                FillStudentInformation();
            }

        }
        private void FillStudentInformation()
        {
            string[] name = drStudentDetails["name"].ToString().Split(' ');
            student_first_name.Value = name[0];
            student_last_name.Value = name[1];
            mother_full_name.Value = drStudentDetails["mother_full_name"].ToString();
            father_full_name.Value = drStudentDetails["father_full_name"].ToString();
            mother_cellphone.Value = drStudentDetails["mother_cellphone"].ToString();
            father_cellphone.Value = drStudentDetails["father_cellphone"].ToString();
            home_phone.Value = drStudentDetails["home_phone"].ToString();
            parents_email.Value = drStudentDetails["parents_email"].ToString();
            settlement.Value = drStudentDetails["settlement"].ToString();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            ReplaceApostrophe();
            string strFields = "first_name: last_name: mother_full_name: father_full_name: " +
                                                 "mother_cellphone:father_cellphone:" +
                                                 "home_phone:parents_email:" +
                                                 "settlement";
            string strValues = "'" + student_first_name.Value + "':'" + student_last_name.Value + "':'" +
                                                 mother_full_name.Value + "':'" + father_full_name.Value + "':'" +
                                                 mother_cellphone.Value + "':'" + father_cellphone.Value + "':'" +
                                                 home_phone.Value + "':'" + parents_email.Value + "':'" +
                                                 settlement.Value + "'";
            string fileName = string.Empty;
            if (FileUpload1.HasFile)
            {
                fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/pictures/") + fileName);
                strFields += ":picture_path";
                strValues += ":'pictures/" + fileName + "'";
            }
            DBConnection.Instance.UpdateTableRow("students", nStudentID,strFields,strValues);

            GoBack();

        }

        private void ReplaceApostrophe()
        {
            student_first_name.Value = student_first_name.Value.Replace("'", "''");
            student_last_name.Value = student_last_name.Value.Replace("'", "''");
            mother_full_name.Value = mother_full_name.Value.Replace("'", "''");
            father_full_name.Value = father_full_name.Value.Replace("'", "''");
            settlement.Value = settlement.Value.Replace("'", "''");
        }

        protected void BtnDeleteStudent(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                DeleteStudent();
            }
        }

        private void DeleteStudent()
        {
            int nCurrentYearID = DBConnection.Instance.GetCurrentYearID();
            string strDeleteQuery = "delete from students_schedule where student_id = " + nStudentID +
                 " and hour_id in(select id from hours_in_day where year_id = " + nCurrentYearID + ");";
            strDeleteQuery += " delete from groups_evaluations where student_id = " + nStudentID +
                     " and group_id in(select id from groups where teacher_id in(select id from teachers where year_id = " + nCurrentYearID + "));";
            strDeleteQuery += " delete from courses_evaluations where student_id = " + nStudentID +
                     " and course_id in(select id from courses where teacher_id in(select id from teachers where year_id = " + nCurrentYearID + "));";
            strDeleteQuery += " delete from students_classes where student_id = " + nStudentID +
                    " and class_id in(select id from classes where teacher_id in(select id from teachers where year_id = " + nCurrentYearID + "));";
            DBConnection.Instance.ExecuteNonQuery(strDeleteQuery);
            Response.Redirect("ClassPage.aspx?ClassId=" + nClassID);
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }
        private void GoBack()
        {
            Response.Redirect("StudentDetailsForm.aspx?StudentID=" + nStudentID );
        }
    }
}