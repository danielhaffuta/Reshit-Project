
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
        private List<SMS> lstSelectedStudentsIDs;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lstSelectedStudents"] !=null)
            {
                lstSelectedStudentsIDs = Session["lstSelectedStudents"] as List<SMS>;
            }
            if (!IsPostBack)
            {
            }
                FillStudents();
            HideNavBar();
        }

        private void FillStudents()
        {
            dtStudents = DBConnection.Instance.GetStudentsDetails(lstSelectedStudentsIDs.Select(student=> student.StudentID).ToList());
            foreach (DataRow drCurrentStudent in dtStudents.Rows)
            {
                Panel pnlStudent = new Panel() { CssClass = "col-18 col-md-6 col-lg-4 col-xl-2 border rounded" };
                Panel pnlRow = new Panel() { CssClass = "row" };
                Label lblStudentName = new Label() { Text = drCurrentStudent["name"].ToString() + ":" + Environment.NewLine,CssClass= "col-6 text-right" };
                pnlRow.Controls.Add(lblStudentName);
                RadioButtonList rblParents = new RadioButtonList() { RepeatDirection = RepeatDirection.Vertical,RepeatLayout= RepeatLayout.Flow ,CssClass = "col-12",CellSpacing=3};
                rblParents.Attributes.Add("student_id", drCurrentStudent["student_id"].ToString());
                rblParents.Items.Add(new ListItem("אמא", drCurrentStudent["mother_cellphone"].ToString()) { Selected = true });
                //rblParents.Items[0].Selected = IsPostBack ? rblParents.Items[0].Selected : true;
                rblParents.Items.Add(new ListItem("אבא", drCurrentStudent["father_cellphone"].ToString()));
                rblParents.Items.Add(new ListItem("אישור בעל פה"));
                pnlRow.Controls.Add(rblParents);
                pnlStudent.Controls.Add(pnlRow);
                pnlStudents.Controls.Add(pnlStudent);
            }
        }



        protected void btnSend_Click(object sender, EventArgs e)
        {
            DataRow drScheduleDetails = DBConnection.Instance.GetScheduleDetails(nDayId, nHourId, nClassID, nGroupId);
            
            List<SMS> lstFailedSMS = new List<SMS>();
            foreach (Control ctrlCurrentStudent in pnlStudents.Controls)
            {
                if (ctrlCurrentStudent is Panel)
                {
                    RadioButtonList tblCurrentStudent = (ctrlCurrentStudent.Controls[0].Controls[1] as RadioButtonList);
                    lstSelectedStudentsIDs.FirstOrDefault(student => student.StudentID == Convert.ToInt32(tblCurrentStudent.Attributes["student_id"])).PhoneNumber =
                        tblCurrentStudent.SelectedItem.Value;
                }
            }
            foreach (SMS CurrentSMS in lstSelectedStudentsIDs)
            {
                if(CurrentSMS.PhoneNumber.Equals("אישור בעל פה"))
                {
                    DBConnection.Instance.ExecuteNonQuery("update students_schedule set approval_status_id=2 where group_id=" + CurrentSMS.GroupId + " and student_id=" + CurrentSMS.StudentID);
                }
                else if(!CurrentSMS.Send())
                {
                    lstFailedSMS.Add(CurrentSMS);
                }
            }

            //FormsUtilities.GenereteSMS();
            string strFailedMsg = "שליחת SMS נכשלה עבור הורי התלמידים:" + Environment.NewLine;
            foreach (SMS smsCurrentSMS in lstFailedSMS)
            {
                strFailedMsg += smsCurrentSMS.StudentName + Environment.NewLine;
            }

            Response.Redirect("GroupsForm.aspx");

        }
    }
}