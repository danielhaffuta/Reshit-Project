
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class GroupsForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strIDs;
            if (Request.QueryString["IDs"] != null)
            {
                strIDs = Request.QueryString["IDs"];
                nClassID = Convert.ToInt32(strIDs.Split('-')[0]);
                nHourId = Convert.ToInt32(strIDs.Split('-')[1]);
                nDayId = Convert.ToInt32(strIDs.Split('-')[2]);
            }

            string strSelectQuery = DBConnection.Instance.GetDisplayQuery("groups");

            DataTable dtGroupsCoursesIDs = DBConnection.Instance.GetDataTableByQuery(strSelectQuery + " where groups.id in(select distinct(group_id) from students_schedule " +
                                                                                        //" inner join students on students.id = students_schedule.student_id " +
                                                                                        " inner join students_classes on students_classes.student_id = students_schedule.student_id" +
                                                                                        " where students_classes.class_id = " + nClassID +
                                                                                        " and hour_id = " + nHourId +
                                                                                        " and day_id = " + nDayId + ")");

            LinkButton lbGroup = null;

            foreach (DataRow drCurrentRow in dtGroupsCoursesIDs.Rows)
            {
                Panel pnlStudents = new Panel();
                Label lblStudent = null,lblComma =null; ;
                //string strGroupStudents = "<br>";
                DataTable dtGroupStudents = DBConnection.Instance.GetDataTableByQuery("select concat(first_name,' ',last_name) as name, approval_status_id from students " +
                                                                                      " inner join students_schedule on students_schedule.student_id = students.id " +
                                                                                      " inner join students_classes on students_classes.student_id = students.id" +
                                                                                      " where students_schedule.group_id = " + drCurrentRow["group_id"] +
                                                                                      " and students_schedule.hour_id = " + nHourId +
                                                                                      " and students_schedule.day_id = " + nDayId +
                                                                                      " and students_classes.class_id = " + nClassID);

                foreach (DataRow drCurrentStudent in dtGroupStudents.Rows)
                {
                    lblStudent = new Label() { Text = drCurrentStudent["name"].ToString()};
                    switch (drCurrentStudent["approval_status_id"].ToString())
                    {
                        case "2":
                            lblStudent.CssClass = "bg-success";
                            break;
                        case "3":
                            lblStudent.CssClass = "bg-danger";
                            break;
                        default:
                            break;
                    }

                    pnlStudents.Controls.Add(lblStudent);
                    lblComma = new Label() { Text = " , "};
                    pnlStudents.Controls.Add(lblComma);

                }
                pnlStudents.Controls.Remove(lblComma);

                lbGroup = new LinkButton()
                {
                    ID = nClassID + "-" + nHourId + "-" + nDayId + "-" + drCurrentRow["group_id"].ToString(),
                    CssClass = "form-control mb-2"
                };
                lbGroup.Controls.Add(new Label(){ Text = drCurrentRow["name"].ToString(), });

                lbGroup.Controls.Add(pnlStudents);
                lbGroup.Click += LbGroup_Click;
                container.Controls.Add(lbGroup);
                container.Controls.Add(new Literal() { Text = "" });
            }
            lbGroup = new LinkButton()
            {
                Text = "קבוצה חדשה",
                ID = nClassID + "-" + nHourId + "-" + nDayId + "-0",
                CssClass = "form-control mb-2"
            };
            lbGroup.Click += LbGroup_Click;
            container.Controls.Add(lbGroup);

            LinkButton lbBack = new LinkButton()
            {
                Text = "חזור",
                CssClass = "form-control mb-2"
            };
            lbBack.Click += LbBack_Click;
            container.Controls.Add(lbBack);
            HideNavBar();
        }



        private void LbBack_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }

        private void LbGroup_Click(object sender, EventArgs e)
        {
            TableEdit.strTableName ="students_schedule";
            Session["IDs"] = (sender as LinkButton).ID;
            Response.Redirect("AddGroupToClass.aspx");
        }
    }
}