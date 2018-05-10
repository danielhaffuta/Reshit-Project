using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class CoordinatorForm : System.Web.UI.Page
    {

        public static Teacher LoggedInTeacher;
        private static string strPreviousPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInTeacher"] == null)
            {
                LoggedInTeacher = new Teacher()
                {
                    FirstName = "אדוני",
                    LastName = "הרכז",
                    Id = 10,
                    Type = "רכז",
                };
            }
            else
            {
                LoggedInTeacher = Session["LoggedInTeacher"] as Teacher;
            }
            FillClasses();
            if(!IsPostBack)
            {
                strPreviousPage = Request.UrlReferrer?.ToString() ?? "LoginForm.aspx";
            }
            
            hTeacherName.Text= "שלום " + LoggedInTeacher.FirstName + " " + LoggedInTeacher.LastName;


        }


        private void FillClasses()
        {


            DataTable dtClasses = DBConnection.Instance.GetDataTableByQuery(
                "select concat(grades.grade_name,classes.class_number) as name,classes.id " +
                "from teacher_class_access " +
                "inner join classes on classes.id = teacher_class_access.class_id " +
                "inner join grades on grades.id = classes.grade_id " +
                "where teacher_class_access.teacher_id = " + LoggedInTeacher.Id);

            HtmlGenericControl olClasses = FindControl("olClasses") as HtmlGenericControl;

            foreach (DataRow drCurrentClass in dtClasses.Rows)
            {
                olClasses.InnerHtml += "<li><a href=ClassPage.aspx?ClassID=" + drCurrentClass["id"] + ">" + drCurrentClass["name"] + "</a></li>";

            }

        }
    }
}