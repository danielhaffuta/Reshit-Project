
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class TeacherClassesForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void FillClasses()
        {
            DataTable dtClasses = LoggedInTeacher.Type=="מנהל" ? DBConnection.Instance.GetThisYearClasses():
                                                DBConnection.Instance.GetTeacherClasses(LoggedInTeacher.ID);



            if (dtClasses.Rows.Count == 0)
            {
                pnlClassesPanel.Visible = false;
                return;
            }

            List<int> lstGradesIDs = dtClasses.Select("")
                                    .Select(cls => Convert.ToInt32(cls["grade_id"]))
                                    .Distinct()
                                    .OrderBy(grade_id => grade_id)
                                    .ToList();

            foreach (int nCurrentGradeID in lstGradesIDs)
            {
                Panel pnlGrade = new Panel()
                {
                    CssClass = "col-1 align-self-start btn-group-vertical"
                };
                foreach (DataRow drCurrentRow in dtClasses.Select("grade_id = " + nCurrentGradeID))
                {
                    Button btnClassButton = new Button()
                    {
                        ID = drCurrentRow["class_id"].ToString(),
                        Text = drCurrentRow["name"].ToString(),
                        CssClass = "btn btn-outline-dark",
                    };
                    btnClassButton.Click += btnClass_Click;
                    pnlGrade.Controls.Add(btnClassButton);
                }
                pnlClassesPanel.Controls.Add(pnlGrade);

            }
        }
        protected void btnClass_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassPage.aspx?ClassID=" + (sender as Button).ID);
        }

    }
}