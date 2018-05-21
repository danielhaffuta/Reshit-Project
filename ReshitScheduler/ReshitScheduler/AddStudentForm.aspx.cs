using Data;
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
        private int nClassID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["ClassID"] != null)
            {
                nClassID = Convert.ToInt32(Request.QueryString["ClassID"]?.ToString());
                divClasses.Visible = false;
            }
            if (!IsPostBack)
            {
                if (nClassID == 0)
                {
                    string strDisplayQuert = DBConnection.Instance.GetDisplayQuery("classes");
                    string strSelectClassesQuery = strDisplayQuert + " where year_id = " + nYearID;
                    dtClassesTable = DBConnection.Instance.GetDataTableByQuery(strSelectClassesQuery);
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

        private void ValidateFields()
        {
            
        }

        protected void BtnAddStudent_Click(object sender, EventArgs e)
        {
            ValidateFields();
            int nSelectedClassID = nClassID != 0 ? nClassID : Convert.ToInt32(ddlClassesList.SelectedValue);
            string fileName = string.Empty;
            if (FileUpload1.HasFile)
            {
                fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/pictures/") + fileName);
            }

            string fields = "first_name,last_name,father_full_name,mother_full_name,father_cellphone,mother_cellphone,home_phone,parents_email,settlement,picture_path";

            string values = "'" + txtStudentFirstName.Text + "','" + txtStudentLastName.Text + "','" + txtFather_full_name.Text + "'," +
                            "'" + txtMother_full_name.Text + "','" + txtFather_cellphone.Text + "','" + txtMother_cellphone.Text + "'," +
                            "'" + txtHome_phone.Text + "','" + txtParents_email.Text + "','" + txtSettlement.Text + "','" + "pictures/" + fileName + "'";

            int nNewStudentIS;

            bool bSuccess = DBConnection.Instance.InsertTableRow("students", fields, values,out nNewStudentIS);
            if (!bSuccess)
            {
                Helper.ShowMessage(ClientScript, "error saving student information");
            }
            else
            {
                bSuccess = DBConnection.Instance.InsertTableRow("students_classes", "student_id,class_id", nNewStudentIS+","+ nSelectedClassID);
                if (!bSuccess)
                {
                    Helper.ShowMessage(ClientScript,  "error connecting student to class");
                }
            }


            LoadClassStudents();


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