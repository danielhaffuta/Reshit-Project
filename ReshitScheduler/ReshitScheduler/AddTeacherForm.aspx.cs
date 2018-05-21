using Data;
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
        private const string SCRIPT_DOFOCUS =
              @"window.setTimeout('DoFocus()', 1);
            function DoFocus()
            {
                try {
                    document.getElementById('REQUEST_LASTFOCUS').focus();
                } catch (ex) {}
            }";

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
            Page.ClientScript.RegisterStartupScript(typeof(LessonForm), "ScriptDoFocus",
                                                    SCRIPT_DOFOCUS.Replace("REQUEST_LASTFOCUS", Request["__LASTFOCUS"]), true);
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }

        protected void BtnAddTeacher_Click(object sender, EventArgs e)
        {
            string values = "'"+txtTeacherFirstName.Text+"','"
                            + txtTeacherLastName.Text + "',"
                            + ddlTeacherType.SelectedValue + ",'"
                            + txtUserName.Text + "','"
                            + txtPassword.Text + "',"
                            + "(select value from preferences where name = 'current_year_id')";
            string fields = "first_name,last_name,teacher_type_id,user_name,password,year_id";

            bool bInsertSucceeded = DBConnection.Instance.InsertTableRow("teachers", fields, values);
            if (!bInsertSucceeded)
            {
                Helper.ShowMessage(ClientScript, "error saving");
            }
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