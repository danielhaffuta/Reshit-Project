
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
            
            DataTable dtClasses = DBConnection.Instance.GetDataTableByQuery(
                " select classes.id " +
                " from classes " +
                " inner join teachers on teachers.id = classes.teacher_id " +
                " where teachers.id = " + LoggedInTeacher.ID);
            Response.Redirect("ClassPage.aspx?ClassId=" + dtClasses.Rows[0]["id"]);
        }
    }
}