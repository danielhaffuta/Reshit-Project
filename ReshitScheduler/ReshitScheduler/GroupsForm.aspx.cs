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
    public partial class GroupsForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strIDs = Request.QueryString["IDs"];
            int nClassID = Convert.ToInt32(strIDs.Split('-')[0]);
            int nHourId = Convert.ToInt32(strIDs.Split('-')[1]);
            int nDayId = Convert.ToInt32(strIDs.Split('-')[2]);

            string strSelectQuery = DBConnection.Instance.GetDisplayQuery("groups");

            DataTable dtGroupsCoursesIDs = DBConnection.Instance.GetDataTableByQuery(strSelectQuery + " where groups.id in(select distinct(group_id) from students_schedule "+
                                                                                        " inner join students on students.id = students_schedule.student_id " +
                                                                                        " where students.class_id = " + nClassID +
                                                                                        " and hour_id = " + nHourId +
                                                                                        " and day_id = " + nDayId+")");

            LinkButton lbGroup = null;

            foreach (DataRow drCurrentRow in dtGroupsCoursesIDs.Rows)
            {
                string strGroupStudents = "-(";
                DataTable dtGroupStudents = DBConnection.Instance.GetDataTableByQuery("select concat(first_name,' ',last_name) as name from students " +
                                                                                      " inner join students_schedule on students_schedule.student_id = students.id " +
                                                                                      " where students_schedule.group_id = " + drCurrentRow["id"] +
                                                                                      " and students_schedule.hour_id = " + nHourId +
                                                                                      " and students_schedule.day_id = " + nDayId +
                                                                                      " and students.class_id = " + nClassID);
                foreach (DataRow drCurrentStudent in dtGroupStudents.Rows)
                {
                    strGroupStudents += drCurrentStudent["name"].ToString() + ",";
                }
                strGroupStudents = strGroupStudents.Remove(strGroupStudents.Length - 1) + ")";
                lbGroup = new LinkButton()
                {
                    Text = drCurrentRow["name"].ToString()  + strGroupStudents,
                    ID = strIDs + "-"+ drCurrentRow["id"].ToString(),
                };
                lbGroup.Click += LbGroup_Click;
                form1.Controls.Add(lbGroup);
                form1.Controls.Add(new Literal() { Text = "<br><br>" });
            }
            lbGroup = new LinkButton()
            {
                Text = "קבוצה חדשה",
                ID = strIDs + "-0"
            };
            lbGroup.Click += LbGroup_Click;
            form1.Controls.Add(lbGroup);

        }

        private void LbGroup_Click(object sender, EventArgs e)
        {
            TableEdit.strTableName ="students_schedule";
            Response.Redirect("AddGroupToClass.aspx?IDs=" + (sender as LinkButton).ID);
        }
    }
}