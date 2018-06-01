
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

        private List<int> lstOldSelectedStudentsIDs = new List<int>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string strGroupsQuery = DBConnection.Instance.GetDisplayQuery("Groups");
                string strGroupsQuery =
                " select groups.id as group_id, groups.group_name as name, " +
                " concat(teachers.first_name, ' ', teachers.last_name) as teacher_name" +
                " from groups " +
                " inner join teachers on teachers.id = groups.teacher_id " +
                " inner join years on years.id = teachers.year_id";
                strGroupsQuery += " where groups.teacher_id not in " +
                                        " (select distinct(teacher_id) from classes_schedule " +
                                        " inner join courses on courses.id = classes_schedule.course_id "+
                                        " where classes_schedule.day_id = " + nDayId +
                                        " and classes_schedule.hour_id = " + nHourId + ")"+
                                    " and groups.teacher_id not in" +
                                        " (select distinct(teacher_id) from students_schedule " +
                                        " inner join groups on groups.id = students_schedule.group_id " +
                                        " where students_schedule.day_id = " + nDayId +
                                        " and students_schedule.hour_id = " + nHourId + ")" +
                                    " and groups.id not in" +
                                        " (select distinct(groups.id) from groups" +
                                        " inner join teachers on teachers.id = groups.teacher_id " +
                                        " inner join students_schedule on students_schedule.group_id = groups.id" +
                                        " inner join students_classes on students_classes.student_id = students_schedule.student_id" +
                                        " where students_classes.class_id = " + nClassID +
                                        " and students_schedule.day_id = " + nDayId +
                                        " and students_schedule.hour_id = " + nHourId + ")";
                if (nGroupId != 0)
                {
                    strGroupsQuery += " or groups.id =" + nGroupId;
                    strGroupsQuery += " or groups.teacher_id =(select teacher_id from groups where id = " + nGroupId +")";
                    GroupsList.Enabled = false;
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
                GroupsList.DataValueField = "group_id";
                GroupsList.DataTextField = "name";
                GroupsList.AutoPostBack = true;
                GroupsList.DataBind();


                foreach (ListItem liCurrentItem in GroupsList.Items)
                {
                    liCurrentItem.Text += " - " + dtGroupsTable.Select("group_id = " + liCurrentItem.Value).Select(group => group["teacher_name"]).FirstOrDefault();
                }
                GroupsList.SelectedValue = Math.Max(nGroupId,1).ToString();

                //string test = "SELECT * FROM groups";
                //DataTable testTable = DBConnection.Instance.GetDataTableByQuery(test);
            }
            this.AddStudents();
            HideNavBar();
        }

        private void AddStudents()
        {
            string strStudentsDisplayQuery = DBConnection.Instance.GetDisplayQuery("students");
            strStudentsDisplayQuery += " inner join students_classes on students_classes.student_id = students.id";

            DataTable dtClassStudent = DBConnection.Instance.GetDataTableByQuery(strStudentsDisplayQuery + " where students_classes.class_id = " + nClassID +
                                                                                " order by name");
            DataTable dtSelectedStudents = null;
            if (nGroupId != 0)
            {
                dtSelectedStudents = DBConnection.Instance.GetDataTableByQuery(
                      " select  distinct(students.id), approval_status_id, concat(first_name, ' ', last_name) as name" +
                      " from students " +
                      " inner join students_classes on students_classes.student_id = students.id " +
                      " inner join students_schedule on students_schedule.student_id = students.id" +
                      " where students_classes.class_id = " + nClassID +
                      " and students_schedule.group_id = " + nGroupId +
                      " and students_schedule.hour_id = " + nHourId +
                      " and students_schedule.day_id = " + nDayId);
            }
            DataTable dtDisabledStudents = DBConnection.Instance.GetDataTableByQuery(strStudentsDisplayQuery +
                " inner join students_schedule on students_schedule.student_id = students.id" +
                " where students_classes.class_id = " + nClassID +
                " and students_schedule.group_id <> " + nGroupId +
                " and students_schedule.hour_id = " + nHourId +
                " and students_schedule.day_id = " + nDayId);

            // need to ask idan,probably because sms
            //+" or (students_schedule.approval_status_id = 1" +
            //        " and(students_schedule.hour_id <> " + nHourId +
            //            " or students_schedule.day_id <> " + nDayId + ")) group by students.id");

            int nCurrentStudentNumber = 0;
            
            foreach (DataRow drCurrentStudent in dtClassStudent.Rows)
            {
                CheckBox cbStudentCheckBox = new CheckBox()
                {
                    Text = drCurrentStudent["name"].ToString(),
                    ID = drCurrentStudent["id"].ToString(),
                    CssClass = "form-control col col-sm-6 col-md-4 col-lg-3 col-xl-2 d-inline-flex justify-content-start align-items-right",
                    AutoPostBack = true
                };
                
                if (nGroupId != 0 && dtSelectedStudents.Select("id = " + drCurrentStudent["id"]).Count() > 0)
                {
                    lstOldSelectedStudentsIDs.Add(Convert.ToInt32(drCurrentStudent["id"]));
                    cbStudentCheckBox.Checked = true;
                    switch (dtSelectedStudents.Select("id = " + drCurrentStudent["id"])[0]["approval_status_id"].ToString())
                    {
                        case "2":
                            cbStudentCheckBox.CssClass += " bg-success";
                            break;
                        case "3":
                            cbStudentCheckBox.CssClass += " bg-danger";
                            break;
                        default:
                            break;
                    }
                }
                if (dtDisabledStudents.Select("id = " + drCurrentStudent["id"]).Count() > 0)
                {
                    cbStudentCheckBox.ToolTip = "נמצא בקבוצה אחרת";
                    cbStudentCheckBox.Enabled = false;
                }
                
                StudentsCol.Controls.Add(cbStudentCheckBox);
                nCurrentStudentNumber++;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataRow drScheduleDetails = DBConnection.Instance.GetScheduleDetails(nDayId, nHourId, nClassID, nGroupId);
            string strInsertCommand = "insert into students_schedule(day_id, hour_id, group_id, student_id, approval_status_id,group_purpose) values ";
            bool bIsGroupEmpty = true;
            //List<int> lstNewSelectedStudentsIDs = new List<int>();
            List<int> lstCurrentlySelectedStudentsIDs = new List<int>();
            List<SMS> lstStudentsSMS = new List<SMS>();
            string strGroupPurpose = groupPurpose.Value ?? "";
            strGroupPurpose = "'" + strGroupPurpose + "'";
            foreach (Control ctrlCurrentControl in StudentsCol.Controls)
            {
                if (ctrlCurrentControl is CheckBox && (ctrlCurrentControl as CheckBox).Checked)
                {
                    lstCurrentlySelectedStudentsIDs.Add(Convert.ToInt32(ctrlCurrentControl.ID));
                    if (!lstOldSelectedStudentsIDs.Contains(Convert.ToInt32(ctrlCurrentControl.ID)))
                    {
                        bIsGroupEmpty = false;
                        //lstNewSelectedStudentsIDs.Add(Convert.ToInt32(ctrlCurrentControl.ID));
                        SMS sms = new SMS()
                        {
                            StudentID = Convert.ToInt32(ctrlCurrentControl.ID),
                            Day = nDayId,
                            GroupName = GroupsList.SelectedItem.Text,
                            CourseName = drScheduleDetails["course_name"].ToString(),
                            StudentName = (ctrlCurrentControl as CheckBox).Text,
                            GroupId = Convert.ToInt32(GroupsList.SelectedValue),
                            GroupPurpose = strGroupPurpose.Substring(1, strGroupPurpose.Length - 2) //remove the ' from the string
                        };
                        lstStudentsSMS.Add(sms);
                        strInsertCommand += "(" + nDayId + "," + nHourId + "," + GroupsList.SelectedValue + "," + ctrlCurrentControl.ID + ",1,"+strGroupPurpose+"),";
                    }
                }
            }
            if (nGroupId != 0)
            {
                string strDeleteCommand = " delete students_schedule from students_schedule" +
                                        " where day_id = " + nDayId +
                                        " and hour_id = " + nHourId +
                                        " and group_id = " + nGroupId +
                                        " and student_id in (" + string.Join(",", lstOldSelectedStudentsIDs) + ")" +
                                         (lstCurrentlySelectedStudentsIDs.Count >0? " and student_id not in (" + string.Join(",", lstCurrentlySelectedStudentsIDs) + ")":"");
                DBConnection.Instance.ExecuteNonQuery(strDeleteCommand);
            }
            if (!bIsGroupEmpty)
            {

                strInsertCommand = strInsertCommand.Remove(strInsertCommand.Length - 1);
                DBConnection.Instance.ExecuteNonQuery(strInsertCommand);
                Session["lstSelectedStudents"] = lstStudentsSMS;
                Response.Redirect("SelectParentsForSmsForm.aspx");
            }
            GoBack();
        }



        protected void btnSave_Click1(object sender, EventArgs e)
        {
            string strDeleteCommand = " delete students_schedule from students_schedule" +
                                      " inner join students_classes on students_classes.student_id = students_schedule.student_id" + 
                                      " where day_id = " + nDayId +
                                      " and hour_id = " + nHourId +
                                      " and group_id = " + nGroupId+
                                      " and students_classes.class_id = " + nClassID;
            DBConnection.Instance.ExecuteNonQuery(strDeleteCommand);

            string strInsertCommand = "insert into students_schedule(day_id, hour_id, group_id, student_id, approval_status_id) values ";
            bool bIsGroupEmpty = true;
            List<int> lstSelectedStudentsIDs = new List<int>();
            foreach (Control ctrlCurrentControl in StudentsCol.Controls)
            {
                if (ctrlCurrentControl is CheckBox && (ctrlCurrentControl as CheckBox).Checked)
                {
                    bIsGroupEmpty = false;
                    lstSelectedStudentsIDs.Add(Convert.ToInt32(ctrlCurrentControl.ID));
                    strInsertCommand += "(" + nDayId + "," + nHourId + "," + GroupsList.SelectedValue + "," + ctrlCurrentControl.ID + ",1),";
                }
            }
            if (!bIsGroupEmpty)
            {
                
                strInsertCommand = strInsertCommand.Remove(strInsertCommand.Length - 1);
                DBConnection.Instance.ExecuteNonQuery(strInsertCommand);
                Session["lstSelectedStudentsIDs"] = lstSelectedStudentsIDs;
                Response.Redirect("SelectParentsForSmsForm.aspx");
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