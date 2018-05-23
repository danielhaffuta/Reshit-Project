
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class SelectParentsForSmsForm : BasePage
    {
        private DataTable dtStudents;
        private List<int> lstSelectedStudentsIDs;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lstSelectedStudentsIDs"] !=null)
            {
                lstSelectedStudentsIDs = Session["lstSelectedStudentsIDs"] as List<int>;
            }
            FillStudents();
        }

        private void FillStudents()
        {
            dtStudents = DBConnection.Instance.GetStudentsDetails(lstSelectedStudentsIDs);
            foreach (DataRow drCurrentStudent in dtStudents.Rows)
            {
                Label lblStudentName = new Label() { Text = drCurrentStudent["name"].ToString() + ":" };
                Label lblMotherName = new Label() { Text = drCurrentStudent["mother_full_name"].ToString() };
                Label lblFatherName = new Label() { Text = drCurrentStudent["father_full_name"].ToString() };
                RadioButtonList rblParents = new RadioButtonList();
                rblParents.Controls.Add(new RadioButton() { Text = drCurrentStudent["mother_full_name"].ToString() ,Checked = true});
                rblParents.Controls.Add(new RadioButton() { Text = drCurrentStudent["father_full_name"].ToString(), Checked = false });
                pnlStudents.Controls.Add(rblParents);
            }
        }
    }
}