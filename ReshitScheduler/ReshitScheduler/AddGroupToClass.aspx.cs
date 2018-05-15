using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class AddGroupToClass : BasePage
    {
        private int nClassID, nHourId, nDayId, nGroupId;

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["IDs"] == null)
            {
                nClassID = 5;
                nHourId = 1;
                nDayId = 1;
                nGroupId = 2;
            }
            else
            {
                string[] strIDs = Request.QueryString["IDs"].Split('-');
                nClassID = Convert.ToInt32(strIDs[0]);
                nHourId = Convert.ToInt32(strIDs[1]);
                nDayId = Convert.ToInt32(strIDs[2]);
                nGroupId = Convert.ToInt32(strIDs[3]);
            }
            if (!IsPostBack)
            {

                string strGroupsQuery = DBConnection.Instance.GetDisplayQuery("Groups");
                //strGroupsQuery += " inner join students_schedule on students_schedule.group_id = groups.id" +
                //        " inner join students on students.id = students_schedule.student_id" +
                //      " where students.class_id = " + nClassID;

                strGroupsQuery += " where groups.id not in" +
                                      " (select distinct(groups.id) from groups" +
                                      " inner join teachers on teachers.id = groups.teacher_id " +
                                      " inner join students_schedule on students_schedule.group_id = groups.id" +
                                      //" inner join students on students.id = students_schedule.student_id " +
                                      " inner join students_classes on students_classes.student_id = students_schedule.student_id" +
                                      " where students_classes.class_id = " + nClassID +
                                      " and students_schedule.day_id = " + nDayId +
                                      " and students_schedule.hour_id = " + nHourId + ")";
                if (nGroupId != 0)
                {
                    strGroupsQuery += " or  groups.id =" + nGroupId;


                }

                strGroupsQuery += " and teachers.year_id = " + nYearID + ";";

                //string TeacherQuery = "SELECT CONCAT(first_name, ' ',last_name) AS full_name, id FROM teachers";
                DataTable dtGroupsTable = DBConnection.Instance.GetDataTableByQuery(strGroupsQuery);
                if(dtGroupsTable.Rows.Count == 0)
                {
                    Response.Write("<script language='javascript'>window.alert('אין קבוצות לבחירה');window.location='"+ strPreviousPage + "';</script>");

                    return;
                }
                GroupsList.DataSource = dtGroupsTable;
                GroupsList.DataValueField = "id";
                GroupsList.DataTextField = "name";
                GroupsList.AutoPostBack = true;
                GroupsList.DataBind();


                foreach (ListItem liCurrentItem in GroupsList.Items)
                {
                    if(nGroupId!=0 && Convert.ToInt32(liCurrentItem.Value) == nGroupId)
                    {
                        GroupsList.SelectedIndex = GroupsList.Items.IndexOf(liCurrentItem);
                    }
                    liCurrentItem.Text = liCurrentItem.Text.Replace("<br>", "");
                }
                //string test = "SELECT * FROM groups";
                //DataTable testTable = DBConnection.Instance.GetDataTableByQuery(test);
            }
                this.AddStudents();
        }

        private void AddStudents()
        {
            string strStudentsDisplayQuery = DBConnection.Instance.GetDisplayQuery("students");
            strStudentsDisplayQuery += " inner join students_classes on students_classes.student_id = students.id";

            DataTable dtClassStudent = DBConnection.Instance.GetDataTableByQuery(strStudentsDisplayQuery + " where students_classes.class_id = " + nClassID);
            DataTable dtSelectedStudents = DBConnection.Instance.GetDataTableByQuery(strStudentsDisplayQuery +
                " inner join students_schedule on students_schedule.student_id = students.id" +
                " where students_classes.class_id = " + nClassID +
                " and students_schedule.group_id = " + nGroupId+
                " and students_schedule.hour_id = " + nHourId+
                " and students_schedule.day_id = " + nDayId);

            DataTable dtDisabledStudents = DBConnection.Instance.GetDataTableByQuery(strStudentsDisplayQuery +
                " inner join students_schedule on students_schedule.student_id = students.id" +
                " where students_classes.class_id = " + nClassID +
                " and students_schedule.group_id <> " + nGroupId +
                " and students_schedule.hour_id = " + nHourId +
                " and students_schedule.day_id = " + nDayId);

            int nCurrentStudentNumber = 0;
            //Panel pNewRow = null;
            
            foreach (DataRow drCurrentStudent in dtClassStudent.Rows)
            {
                //if(nCurrentStudentNumber%6 == 0)
                //{
                //    pNewRow = new Panel() { CssClass = "form-control row justify-content-around bg-success m-0" };
                //    StudentsCol.Controls.Add(pNewRow);

                //}
                Panel p = new Panel() { CssClass = "form-control" };
                CheckBox cbStudentCheckBox = new CheckBox()
                {
                    Text = drCurrentStudent["name"].ToString(),
                    ID = drCurrentStudent["id"].ToString(),
                    //CssClass = "form-control col-2 d-inline-block text-right  bg-info  ",
                    CssClass = "form-control col col-sm-6 col-md-4 col-lg-3 col-xl-2  d-inline-flex   justify-content-center align-items-center",
                    
                    AutoPostBack = true
                };
                if(nGroupId!=0 && dtSelectedStudents.Select("id = " + drCurrentStudent["id"]).Count()>0)
                {
                    cbStudentCheckBox.Checked = true;
                }
                if (dtDisabledStudents.Select("id = " + drCurrentStudent["id"]).Count() > 0)
                {
                    cbStudentCheckBox.Enabled = false;
                    cbStudentCheckBox.ToolTip = "נמצא בקבוצה אחרת";
                }

                p.Controls.Add(cbStudentCheckBox);
                StudentsCol.Controls.Add(cbStudentCheckBox);
                nCurrentStudentNumber++;
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strDeleteCommand = " delete students_schedule from students_schedule" +
                                    " inner join students_classes on students_classes.student_id = students_schedule.student_id" + 
                                    //" inner join students on students.id = students_schedule.student_id " + 
                                    " where day_id = " + nDayId +
                                    " and hour_id = " + nHourId +
                                    " and group_id = " + nGroupId+
                                    " and students_classes.class_id = " + nClassID;
            DBConnection.Instance.ExecuteNonQuery(strDeleteCommand);

            string strInsertCommand = "insert into students_schedule(day_id,hour_id,group_id,student_id) values ";
            bool bIsGroupEmpty = true;

            foreach (Control ctrlCurrentControl in StudentsCol.Controls)
            {
                if (ctrlCurrentControl is CheckBox && (ctrlCurrentControl as CheckBox).Checked)
                {
                    bIsGroupEmpty = false;
                    strInsertCommand += "(" + nDayId + "," + nHourId + "," + GroupsList.SelectedValue + "," + ctrlCurrentControl.ID + "),";
                }
            }
            if (!bIsGroupEmpty)
            {
                strInsertCommand = strInsertCommand.Remove(strInsertCommand.Length - 1);
                DBConnection.Instance.ExecuteNonQuery(strInsertCommand);
            }
            GoBack();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        protected void GoBack()
        {
            Response.Redirect("GroupsForm.aspx?IDs=" + nClassID + "-" + nHourId + "-" + nDayId);
        }

    }
}