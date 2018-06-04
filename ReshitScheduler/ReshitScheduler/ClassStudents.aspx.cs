
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class ClassStudents : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillStudents();


        }
        private void FillStudents()
        {
            DataTable dtStudents = DBConnection.Instance.GetDataTableByQuery("select students.id,concat(last_name,' ' ,first_name) as name,picture_path from students" +
                                                                            " inner join students_classes on students_classes.student_id = students.id" +
                                                                            " where students_classes.class_id = " + nClassID);

            foreach (DataRow drCurrentStudent in dtStudents.Rows)
            {
                string strFigure = "<a href=\"StudentDetailsForm.aspx?StudentID="+ drCurrentStudent["id"] + "\" class=\"col-6 col-md-2 col-sm-4\">"+
                                   "<figure > <img src=\"" + drCurrentStudent["picture_path"] + "\" style=\"max-height:75px;\"  >" +
                                   "<figcaption>" + drCurrentStudent["name"] + "</figcaption></figure></a>";
                pnlStudents.Controls.Add(new LiteralControl(strFigure));
            }
        }
    }
}