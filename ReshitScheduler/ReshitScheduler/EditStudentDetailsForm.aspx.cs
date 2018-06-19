
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
            student_last_name.Value = name[2];
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
            string[] strFields = new string[10];
            strFields[0] = "first_name";
            strFields[1] = "last_name";
            strFields[2] = "father_full_name";
            strFields[3] = "mother_full_name";
            strFields[4] = "father_cellphone";
            strFields[5] = "mother_cellphone";
            strFields[6] = "home_phone";
            strFields[7] = "parents_email";
            strFields[8] = "settlement";
            string[] strValues = new string[10];
            strValues[0] = student_first_name.Value;
            strValues[1] = student_last_name.Value;
            strValues[2] = father_full_name.Value;
            strValues[3] = mother_full_name.Value;
            strValues[4] = father_cellphone.Value;
            strValues[5] = mother_cellphone.Value;
            strValues[6] = home_phone.Value;
            strValues[7] = parents_email.Value;
            strValues[8] = settlement.Value;
            string fileName = string.Empty;
            if (FileUpload1.HasFile)
            {
                fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/pictures/") + fileName);
                strFields[9] = "picture_path";
                strValues[9] = "pictures/" + fileName;
            }
            DBConnection.Instance.UpdateTableRow("students", nStudentID,strFields,strValues);

            GoBack();

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