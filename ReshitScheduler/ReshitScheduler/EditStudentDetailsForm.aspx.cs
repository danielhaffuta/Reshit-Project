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
            DBConnection.Instance.UpdateTableRow("students", nStudentID,
                                                 "mother_full_name:father_full_name:" +
                                                 "mother_cellphone:father_cellphone:" +
                                                 "home_phone:parents_email:" +
                                                 "settlement",
                                                 "'" + mother_full_name.Value + "':'" + father_full_name.Value + "':'" +
                                                 mother_cellphone.Value + "':'" + father_cellphone.Value + "':'" +
                                                 home_phone.Value + "':'" + parents_email.Value + "':'" +
                                                 settlement.Value + "'");

            GoBack();

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