using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler.Forms
{
    public partial class EducatorForm : System.Web.UI.Page
    {
        public static Teacher LoggedInTeacher;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInTeacher"] == null)
            {
                Response.Redirect("LoginForm.aspx");
                return;
            }
            LoggedInTeacher = Session["LoggedInTeacher"] as Teacher;
            DataTable dtClasses = DBConnection.Instance.GetDataTableByQuery(
                " select classes.id " +
                " from classes " +
                " inner join teachers on teachers.id = classes.teacher_id " +
                " where teachers.id = " + LoggedInTeacher.Id);
            Response.Redirect("ClassPage.aspx?ClassId=" + dtClasses.Rows[0]["id"]);
        }
    }
}