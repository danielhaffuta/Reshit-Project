
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
    public partial class CoordinatorForm : BasePage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            
            FillClasses();


        }

        private void FillClasses()
        {
            DataTable dtClasses = DBConnection.Instance.GetTeacherClasses(LoggedInTeacher.ID);

            if (dtClasses.Rows.Count == 0)
            {
                pnlClasses.Visible = false;
            }
            foreach (DataRow drCurrentClass in dtClasses.Rows)
            {
                pnlClasses.Controls.Add(new LiteralControl("<a class=\"list-group-item list-group-item-action d-block\" " +
                                       "href=ClassPage.aspx?ClassID=" + drCurrentClass["class_id"] + ">" + drCurrentClass["name"] + "</a></li>"));
            }
        }

    }

}