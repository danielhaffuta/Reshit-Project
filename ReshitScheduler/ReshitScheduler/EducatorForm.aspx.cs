
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class EducatorForm : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dtClasses;
            if (LoggedInTeacher.Type.Equals("מורה לחינוך מיוחד"))
            {
                dtClasses = DBConnection.Instance.GetDataTableByQuery(
                " select teacher_class_access.class_id " +
                " from teacher_class_access " +
                " inner join teachers on teachers.id = teacher_class_access.teacher_id " +
                " where teachers.id = " + LoggedInTeacher.ID);
                LoggedInTeacher.ClassID = Convert.ToInt32(dtClasses.Rows[0]["class_id"]);
                Response.Redirect("ClassPage.aspx?ClassId=" + dtClasses.Rows[0]["class_id"]);
            }
            else
            {
                dtClasses = DBConnection.Instance.GetDataTableByQuery(
                    " select classes.id " +
                    " from classes " +
                    " inner join teachers on teachers.id = classes.teacher_id " +
                    " where teachers.id = " + LoggedInTeacher.ID);
                Response.Redirect("ClassPage.aspx?ClassId=" + dtClasses.Rows[0]["id"]);
            }
            
        }
    }
}