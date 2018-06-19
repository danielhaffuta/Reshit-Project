
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class AddStudentForm : BasePage
    {
        protected DataTable dtClassesTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(nClassID != 0)
            {
                divClasses.Visible = false;
            }
            if (!IsPostBack)
            {
                if (nClassID == 0)
                {
                    if (LoggedInTeacher.Type == "מנהל")
                    {
                        dtClassesTable = DBConnection.Instance.GetThisYearClasses();
                    }
                    else
                    {
                        dtClassesTable = DBConnection.Instance.GetAllTeacherClasses(LoggedInTeacher.ID);
                        
                    }
                    ddlClassesList.DataSource = dtClassesTable;
                    ddlClassesList.DataBind();
                }
                LoadClassStudents();
            }
        }

        protected void ddlClassesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadClassStudents();
        }

        private void LoadClassStudents()
        {
            int nSelectedClassID = nClassID != 0 ? nClassID : Convert.ToInt32(ddlClassesList.SelectedValue);
            DataTable dtStudentsInformation = DBConnection.Instance.GetClassStudents(nSelectedClassID);
            gvStudents.DataSource = dtStudentsInformation;
            gvStudents.DataBind();
            pnlNoStudentsMsg.Visible = gvStudents.Rows.Count == 0;
        }

        private bool ValidateFields()
        {
            if(txtStudentFirstName.Text == "" || txtStudentFirstName.Text == null)
            {
                Helper.ShowMessage(ClientScript, "חובה להכניס שם פרטי של התלמיד");
                return false;
            }
            if (txtStudentLastName.Text == "" || txtStudentLastName.Text == null)
            {
                Helper.ShowMessage(ClientScript, "חובה להכניס שם משפחה של התלמיד");
                return false;
            }
            return true;
        }

        protected void BtnAddStudent_Click(object sender, EventArgs e)
        {
            bool bValid = ValidateFields();
            if (!bValid)
                return;
            int nSelectedClassID = nClassID != 0 ? nClassID : Convert.ToInt32(ddlClassesList.SelectedValue);
            string fileName = string.Empty;
            bool saveFile = false;
            if (FileUpload1.HasFile)
            {
                fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                saveFile = true;
            }
            string[] strFields = { "first_name","last_name","father_full_name",
                            "mother_full_name","father_cellphone","mother_cellphone","home_phone",
                            "parents_email","settlement","picture_path" };
            string[] strValues = { txtStudentFirstName.Text, txtStudentLastName.Text, txtFather_full_name.Text,
                                txtMother_full_name.Text, txtFather_cellphone.Text, txtMother_cellphone.Text,
                                txtHome_phone.Text, txtParents_email.Text, txtSettlement.Text, "pictures/" + fileName};

            int nNewStudentIS = DBConnection.Instance.InsertTableRow("students", strFields, strValues);
            if(nNewStudentIS == 0)
            {
                Helper.ShowMessage(ClientScript, "שגיאה בשמירת פרטי תלמיד");
                Helper.ShowMessage(ClientScript, "תמונה לא הועלתה, נא לטעון תמונה מחדש");
                return;
            }
            else
            {
                string[] strFieldsClass = { "student_id","class_id" };
                string[] strValuesClass = { Convert.ToString(nNewStudentIS), Convert.ToString(nSelectedClassID) };
                int nSuccess = DBConnection.Instance.InsertTableRow("students_classes", strFieldsClass, strValuesClass);
                if (nSuccess == 0)
                {
                    Helper.ShowMessage(ClientScript,  "שגיאה בצירוף סטודנט לכיתה, פרטים לא נשמרו");
                    string strDeleteQuery = "delete from students where id = " + nNewStudentIS;
                    DBConnection.Instance.ExecuteNonQuery(strDeleteQuery);
                    return;
                }
            }
            if (saveFile)
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/pictures/") + fileName);
            }
            Helper.ShowMessage(ClientScript, "תלמיד נשמר");
            ResetFields();
            LoadClassStudents();


        }
        

        private void ResetFields()
        {
            txtStudentFirstName.Text = "";
            txtStudentLastName.Text = "";
            txtFather_full_name.Text = "";
            txtMother_full_name.Text = "";
            txtFather_cellphone.Text = "";
            txtMother_cellphone.Text = "";
            txtHome_phone.Text = "";
            txtParents_email.Text = "";
            txtSettlement.Text = "";
        }

        protected void BtnChoosePicture_Click(object sender, EventArgs e)
        {
            
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}