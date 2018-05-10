using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class StudentDetailsForm : System.Web.UI.Page
    {
        private string strStudentID;
        protected string strStudentName;
        protected string strPicturePath;
        protected string strClass;

        private static string strPreviousPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strPreviousPage = Request.UrlReferrer?.ToString() ?? "LoginForm.aspx";

            }
            strStudentID = Request.QueryString["StudentID"]?.ToString() ?? "5";

            DataTable dtStudent = DBConnection.Instance.GetDataTableByQuery(" select concat(first_name,' ' ,last_name) as name,"+
                                                                            " picture_path,concat(grades.grade_name,classes.class_number) as class" +
                                                                            " from students "+
                                                                            " inner join classes on classes.id = students.class_id"+
                                                                            " inner join grades on grades.id = classes.grade_id" +
                                                                            " where students.id = " + strStudentID);
            strStudentName = dtStudent.Rows[0]["name"].ToString();
            strPicturePath= dtStudent.Rows[0]["picture_path"].ToString();
            strClass= dtStudent.Rows[0]["class"].ToString();


        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }
        private void GoBack()
        {
            Response.Redirect(strPreviousPage);
        }
    }
}