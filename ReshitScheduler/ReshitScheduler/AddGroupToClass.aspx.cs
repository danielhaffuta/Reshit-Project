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
    public partial class AddGroupToClass : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int nClassID, nHourId, nDayId,nGroupId;

            if (Request.QueryString["IDs"] == null)
            {
                nClassID = 5;
                nHourId = 1;
                nDayId = 3;
                nGroupId = 1;
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


                string strYearID = DBConnection.Instance.GetCurrentYearID();
                string strGroupsQuery = DBConnection.Instance.GetDisplayQuery("Groups");
                //strGroupsQuery += " inner join students_schedule on students_schedule.group_id = groups.id" +
                //        " inner join students on students.id = students_schedule.student_id" +
                //      " where students.class_id = " + nClassID;
                if (nGroupId == 0)
                {
                    strGroupsQuery += " where groups.id not in" +
                                      " (select distinct(groups.id) from groups" +
                                      " inner join teachers on teachers.id = groups.teacher_id " +
                                      " inner join students_schedule on students_schedule.group_id = groups.id" +
                                      " inner join students on students.id = students_schedule.student_id " +
                                      " where students.class_id = " + nClassID +
                                      " and students_schedule.day_id = " + nDayId +
                                      " and students_schedule.hour_id = " + nHourId + ")";
                    
                }

                strGroupsQuery += " and teachers.year_id = " + strYearID + ";";

                //string TeacherQuery = "SELECT CONCAT(first_name, ' ',last_name) AS full_name, id FROM teachers";
                DataTable dtGroupsTable = DBConnection.Instance.GetDataTableByQuery(strGroupsQuery);

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
        }
    }
}