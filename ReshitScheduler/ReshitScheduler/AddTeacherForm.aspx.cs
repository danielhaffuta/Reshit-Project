
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
    public partial class AddTeacherForm : BasePage
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strTeacherTypeQuery = "SELECT * FROM teacher_types where teacher_type_name <> 'admin'";
                DataTable dtTeacherTypes = DBConnection.Instance.GetDataTableByQuery(strTeacherTypeQuery);

                ddlTeacherType.DataSource = dtTeacherTypes;
                ddlTeacherType.DataValueField = "id";
                ddlTeacherType.DataTextField = "teacher_type_name";
                ddlTeacherType.AutoPostBack = true;
                ddlTeacherType.Attributes.Add("onfocus", "try{document.getElementById('__LASTFOCUS').value=this.id} catch(e) {}");

                ddlTeacherType.DataBind();


            }
            
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }

        private bool ValidateFields()
        {
            if (txtTeacherFirstName.Text == "" || txtTeacherFirstName.Text == null)
            {
                Helper.ShowMessage(ClientScript, "חובה להכניס שם פרטי של המורה");
                return false;
            }
            if (txtTeacherLastName.Text == "" || txtTeacherLastName.Text == null)
            {
                Helper.ShowMessage(ClientScript, "חובה להכניס שם משפחה של המורה");
                return false;
            }
            if (txtUserName.Text == "" || txtUserName.Text == null)
            {
                Helper.ShowMessage(ClientScript, "חובה להכניס שם משתמש למורה");
                return false;
            }
            if (txtPassword.Text == "" || txtPassword.Text == null)
            {
                Helper.ShowMessage(ClientScript, "חובה להכניס סיסמה למורה");
                return false;
            }
            return true;
        }

        protected void BtnAddTeacher_Click(object sender, EventArgs e)
        {
            bool bValid = ValidateFields();
            if (!bValid)
                return;
            string[] strValues = { txtTeacherFirstName.Text, txtTeacherLastName.Text,
                ddlTeacherType.SelectedValue, txtUserName.Text, txtPassword.Text,
                "(select value from preferences where name = 'current_year_id')" };
            string[] strFields = { "first_name","last_name","teacher_type_id","user_name","password","year_id" };

            int nInsertSucceeded = DBConnection.Instance.InsertTableRow("teachers", strFields, strValues);
            if (nInsertSucceeded == 0)
            {
                Helper.ShowMessage(ClientScript, "error saving");
                return;
            }
            Helper.ShowMessage(ClientScript, "מורה נשמר");
            CleanFields();

        }
        

        private void CleanFields()
        {
            txtTeacherFirstName.Text = "";
            txtTeacherLastName.Text = "";
            ddlTeacherType.SelectedIndex = 0;
            txtUserName.Text = "";
            txtPassword.Text = "";
        }
    }
}