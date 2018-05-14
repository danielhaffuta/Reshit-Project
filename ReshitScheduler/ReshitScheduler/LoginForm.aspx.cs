using Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ReshitScheduler
{
    public partial class LoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["LoggedInTeacher"] = null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            DBConnection dbcConnection = DBConnection.Instance;
            
            string strQuery = "SELECT teachers.id,first_name,last_name,teacher_types.teacher_type_name " +
                               "FROM teachers " +
                               "inner join teacher_types on teacher_types.id = teachers.teacher_type_id " +
                               "where user_name ='" + Username.Text + "' " +
                               "and password = '" + Password.Text + "'"+
                               "and teachers.year_id = (select value from preferences where name = 'current_year_id' )";

            DataTable dtLoginData = dbcConnection.GetDataTableByQuery(strQuery);
            if (dtLoginData != null && dtLoginData.Rows.Count > 0)
            {
                Teacher LoggedInTeacher = new Teacher()
                {
                    Id = int.Parse(dtLoginData.Rows[0][0].ToString()),
                    FirstName = dtLoginData.Rows[0][1].ToString(),
                    LastName = dtLoginData.Rows[0][2].ToString(),
                    Type = dtLoginData.Rows[0][3].ToString()
                };
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Hello " + LoggedInTeacher.FirstName + " " + LoggedInTeacher.LastName + "');", true);

                Session["LoggedInTeacher"] = LoggedInTeacher;
                Response.Redirect("MainForm.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('wrong username or password');", true);
            }



        }
    }
}