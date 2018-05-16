using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public class FormsUtilities
    {
        private static DataTable dtDays;
        private static DataTable dtHours;
        private static DataTable dtCourses;
        private static DataTable dtGroups;

        static FormsUtilities()
        {
            dtDays = DBConnection.Instance.GetAllDataFromTable("days");
            dtHours = DBConnection.Instance.GetHours();
            dtCourses = DBConnection.Instance.GetConstraintDataTable("courses");
            dtGroups = DBConnection.Instance.GetConstraintDataTable("groups");
        }

        public static DataTable BuildEmptySchedule()
        {
            DataTable dtScheduleTable = new DataTable("Schedule");
            dtScheduleTable.Columns.Add("hour_id", typeof(string));
            dtScheduleTable.Columns.Add("days_hours", typeof(string));

            //Setting days columns
            foreach (DataRow drCurrentDay in dtDays.Rows)
            {
                dtScheduleTable.Columns.Add(drCurrentDay["id"].ToString(), typeof(string));
            }
            ///////////////////////////////

            //Setting days Rows
            DataRow drNewRow = dtScheduleTable.NewRow();
            drNewRow["days_hours"] = "ימים/שעות";
            foreach (DataRow drCurrentDay in dtDays.Rows)
            {
                drNewRow[drCurrentDay["id"].ToString()] = drCurrentDay["day_name"].ToString();
            }
            dtScheduleTable.Rows.Add(drNewRow);
            ////////////////////////////////////////////////////////

            //Setting Hours Rows
            foreach (DataRow drCurrentHour in dtHours.Rows)
            {
                drNewRow = dtScheduleTable.NewRow();
                drNewRow["hour_id"] = drCurrentHour["id"].ToString();
                drNewRow["days_hours"] = drCurrentHour["name"].ToString();
                if (drCurrentHour["is_break"].ToString().Equals("1"))
                {
                    drNewRow["hour_id"] += "*";
                }
                dtScheduleTable.Rows.Add(drNewRow);

            }
            //////////////////////////////////////////////////

            return dtScheduleTable;
        }


        public static void FillStudentSchedule(int nStudentID, int nClassID, DataTable dtScheduleTable)
        {
            DataTable dtClassSchedule = DBConnection.Instance.GetDataTableByQuery(
                "select * from classes_schedule where class_id = " + nClassID);
            DataTable dtStudentsSchedule = DBConnection.Instance.GetDataTableByQuery(
                "select * from students_schedule where student_id = " + nStudentID);
            foreach (DataRow drCurrentHour in dtClassSchedule.Rows)
            {
                string strCourseName;

                DataRow[] drStudentScheduleRows = dtStudentsSchedule.Select("hour_id = " + drCurrentHour["hour_id"].ToString() + " and day_id = " + drCurrentHour["day_id"].ToString());
                if (drStudentScheduleRows.Count() > 0)
                {
                    strCourseName = dtGroups.Select("id = " + drStudentScheduleRows[0]["group_id"])[0]["name"] + "?GroupID=" + drStudentScheduleRows[0]["group_id"];
                }
                else
                {
                    strCourseName = dtCourses.Select("id = " + drCurrentHour["course_id"])[0]["name"] + "?CourseID=" + drCurrentHour["course_id"];
                }
                dtScheduleTable.Select(("hour_id = '" + drCurrentHour["hour_id"]).Replace("*", "") + "'")[0][drCurrentHour["day_id"].ToString()] = strCourseName + "-" + drCurrentHour["day_id"] + drCurrentHour["hour_id"];
            }
        }


        public static GridView FillStudentGrid(DataTable dtScheduleTable, EventHandler ehButtonClick)
        {
            GridView gvScheduleView = new GridView()
            {
                ShowHeader = false,
                CssClass = "table table-bordered table-sm",
                DataSource = dtScheduleTable
            };
            gvScheduleView.DataBind();

            foreach (GridViewRow gvrCurrentRow in gvScheduleView.Rows)
            {
                if (gvrCurrentRow.Cells[0].Text.Contains("*"))
                {
                    gvrCurrentRow.Cells[0].Text.Replace("*", "");
                    gvrCurrentRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    gvrCurrentRow.CssClass = "bg-light";
                    gvrCurrentRow.Cells[2].ColumnSpan = 6;
                    gvrCurrentRow.Cells[2].Text = "הפסקה";
                    gvrCurrentRow.Cells[2].CssClass = "h5 text-center";

                    gvrCurrentRow.Cells.RemoveAt(7);
                    gvrCurrentRow.Cells.RemoveAt(6);
                    gvrCurrentRow.Cells.RemoveAt(5);
                    gvrCurrentRow.Cells.RemoveAt(4);
                    gvrCurrentRow.Cells.RemoveAt(3);
                }
                else
                {
                    foreach (TableCell tcCurrentCell in gvrCurrentRow.Cells)
                    {
                        tcCurrentCell.Text = tcCurrentCell.Text.Replace("&lt;br&gt;", "<br>");
                        tcCurrentCell.HorizontalAlign = HorizontalAlign.Center;

                        if (tcCurrentCell.Text.Contains("?"))
                        {
                            if (ehButtonClick != null)
                            {
                                LinkButton lbEvaluationLinkButton = new LinkButton()
                                {
                                    Text = tcCurrentCell.Text.Split('?')[0],
                                    ID = tcCurrentCell.Text.Split('?')[1],
                                    //OnClientClick = "genericPopup(\"GroupsForm.aspx?IDs=" + strClassID + "-" + tcCurrentCell.Text.Split('*')[1].Replace("$", "") + "\",1200,900,\"yes\")"
                                };
                                lbEvaluationLinkButton.Click += ehButtonClick;
                                tcCurrentCell.Controls.Add(lbEvaluationLinkButton);
                            }
                            else
                            {
                                tcCurrentCell.Text = tcCurrentCell.Text.Split('?')[0];
                            }
                        }
                    }

                }
                gvrCurrentRow.Cells[0].Visible = false;
            }
            return gvScheduleView;
        }


        public static void FillClassSchedule(int nClassID, DataTable dtScheduleTable)
        {
            DataTable dtClassSchedule = DBConnection.Instance.GetDataTableByQuery(
                "select * from classes_schedule where class_id = " + nClassID);
            DataTable dtStudentsSchedule = DBConnection.Instance.GetDataTableByQuery(
            "SELECT * FROM students_schedule " +
            //            " inner join students on students.id = students_schedule.student_id" +
            " inner join students_classes on students_classes.student_id = students_schedule.student_id" +
            " inner join classes on classes.id = students_classes.class_id" +
            " inner join teachers on teachers.id = classes.teacher_id and teachers.year_id =  (select value from preferences where name = 'current_year_id')"+
            " where students_classes.class_id = " + nClassID);
            foreach (DataRow drCurrentHour in dtClassSchedule.Rows)
            {
                string strCourseName = dtCourses.Select("id = " + drCurrentHour["course_id"].ToString())[0]["name"].ToString() +
                                       "*" + drCurrentHour["hour_id"] + "-" + drCurrentHour["day_id"];
                DataRow[] drStudentScheduleRows = dtStudentsSchedule.Select("hour_id = " + drCurrentHour["hour_id"].ToString() + " and day_id = " + drCurrentHour["day_id"].ToString());
                if (drStudentScheduleRows.Count() > 0)
                {
                    strCourseName += "$";
                }
                dtScheduleTable.Select("hour_id = '" + drCurrentHour["hour_id"].ToString().Replace("*", "") + "'")[0][drCurrentHour["day_id"].ToString()] = strCourseName;
            }
        }

        public static GridView FillClassGrid(int nClassID, DataTable dtScheduleTable,bool bForPrint = false)
        {
            GridView gvScheduleView = new GridView()
            {
                ShowHeader = false,
                CssClass = "table table-bordered table-sm",
                DataSource = dtScheduleTable
            };
            gvScheduleView.DataBind();

            foreach (GridViewRow gvrCurrentRow in gvScheduleView.Rows)
            {
                if (gvrCurrentRow.Cells[0].Text.Contains("*"))
                {
                    gvrCurrentRow.Cells[0].Text.Replace("*", "");
                    gvrCurrentRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    gvrCurrentRow.CssClass = "bg-light";
                    gvrCurrentRow.Cells[2].ColumnSpan = 6;
                    gvrCurrentRow.Cells[2].Text = "הפסקה";
                    gvrCurrentRow.Cells[2].CssClass = "h5 text-center";

                    gvrCurrentRow.Cells.RemoveAt(7);
                    gvrCurrentRow.Cells.RemoveAt(6);
                    gvrCurrentRow.Cells.RemoveAt(5);
                    gvrCurrentRow.Cells.RemoveAt(4);
                    gvrCurrentRow.Cells.RemoveAt(3);
                }
                else
                {
                    foreach (TableCell tcCurrentCell in gvrCurrentRow.Cells)
                    {
                        tcCurrentCell.Text = tcCurrentCell.Text.Replace("&lt;br&gt;", "<br>");
                        tcCurrentCell.HorizontalAlign = HorizontalAlign.Center;
                        if (tcCurrentCell.Text.Contains("*"))
                        {
                            Label lbl = new Label() { Text = tcCurrentCell.Text.Split('*')[0] };
                            tcCurrentCell.Attributes.Add("onClick", "OnClick(\"GroupsForm.aspx?IDs=" + nClassID + "-" + tcCurrentCell.Text.Split('*')[1].Replace("$", "") + "\",1200,900,\"yes\");");
                            tcCurrentCell.Controls.Add(lbl);

                            if (!bForPrint && tcCurrentCell.Text.Contains("$"))
                            {
                                //tcCurrentCell.Text = tcCurrentCell.Text.Replace("$", "");
                                LinkButton lbGroupLinkButton = new LinkButton()
                                {
                                    Text = "<br>קבוצות",
                                    ID = tcCurrentCell.Text.Split('*')[1],
                                    OnClientClick = "genericPopup(\"GroupsForm.aspx?IDs=" + nClassID + "-" + tcCurrentCell.Text.Split('*')[1].Replace("$", "") + "\",1200,900,\"yes\")"
                                };
                                tcCurrentCell.Controls.Add(lbGroupLinkButton);
                            }
                        }
                    }
                }
                gvrCurrentRow.Cells[0].Visible = false;
            }
            return gvScheduleView;
        }

        public static GridView FillClassStudents(GridView gvStudents,DataTable dtStudents)
        {

            gvStudents.DataSource = dtStudents;
            gvStudents.DataBind();



            foreach (GridViewRow gvrCurrentRow in gvStudents.Rows)
            {
                foreach (TableCell tcCurrentCell in gvrCurrentRow.Cells)
                {

                }
                /*gvrCurrentRow.Cells[0].Visible = false;
                gvrCurrentRow.Cells[0].Visible = false;
                gvrCurrentRow.Cells[0].Visible = false;
                gvrCurrentRow.Cells[0].Visible = false;
                gvrCurrentRow.Cells[0].Visible = false;
                gvrCurrentRow.Cells[0].Visible = false;
                gvrCurrentRow.Cells[0].Visible = false;*/
            }


            return gvStudents;
        }

    }
}