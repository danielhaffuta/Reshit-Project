
using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

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
            dtCourses = DBConnection.Instance.GetDataTableForDisplay("courses");
            dtGroups = DBConnection.Instance.GetDataTableForDisplay("groups");
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
                    strCourseName = dtGroups.Select("group_id = " + drStudentScheduleRows[0]["group_id"])[0]["name"] + "<br>(" + 
                                    dtGroups.Select("group_id = " + drStudentScheduleRows[0]["group_id"])[0]["teacher_name"] + ")" +
                                                    "?GroupID=" + drStudentScheduleRows[0]["group_id"];
                        
                }
                else
                {
                    strCourseName = dtCourses.Select("course_id = " + drCurrentHour["course_id"])[0]["name"] + "<br>(" +
                                    dtCourses.Select("course_id = " + drCurrentHour["course_id"])[0]["teacher_name"] + ")" +
                                    "?CourseID=" + drCurrentHour["course_id"];
                                    
                                       //dtCourses.Select("course_id = " + drCurrentHour["course_id"].ToString())[0]["teacher_name"].ToString() +")";
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
                string strCourseName = dtCourses.Select("course_id = " + drCurrentHour["course_id"].ToString())[0]["name"].ToString()/*Course Name*/ +"<br>("+
                                       dtCourses.Select("course_id = " + drCurrentHour["course_id"].ToString())[0]["teacher_name"].ToString() /*Teacher Name*/+
                                       ") *" + drCurrentHour["hour_id"] + "-" + drCurrentHour["day_id"];
                DataRow[] drStudentScheduleRows = dtStudentsSchedule.Select("hour_id = " + drCurrentHour["hour_id"].ToString() + " and day_id = " + drCurrentHour["day_id"].ToString());
                if (drStudentScheduleRows.Count() > 0)// there are groups in this time
                {
                    strCourseName += "$"; // Mark the cell as having groups
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

        public static void FillTeacherSchedule(int nTeacherID, DataTable dtScheduleTable)
        {
            DataTable dtTeacherSchedule = DBConnection.Instance.GetDataTableByQuery(
                " select * from classes_schedule "+
                " inner join courses on courses.id = classes_schedule.course_id "+
                " where courses.teacher_id = " + nTeacherID);
            DataTable dtStudentsSchedule = DBConnection.Instance.GetDataTableByQuery(
                " select distinct groups.id as group_id,day_id,hour_id,group_id from students_schedule " +
                " inner join groups on groups.id = students_schedule.group_id " +
                " where groups.teacher_id = " + nTeacherID);
            foreach (DataRow drCurrentHour in dtTeacherSchedule.Rows)
            {
                dtScheduleTable.Select("hour_id = '" + drCurrentHour["hour_id"].ToString().Replace("*", "") + "'")[0][drCurrentHour["day_id"].ToString()] +=
                    dtCourses.Select("course_id = " + drCurrentHour["course_id"].ToString())[0]["name"].ToString();
            }
            foreach (DataRow drCurrentHour in dtStudentsSchedule.Rows)
            {
                dtScheduleTable.Select("hour_id = '" + drCurrentHour["hour_id"].ToString().Replace("*", "") + "'")[0][drCurrentHour["day_id"].ToString()] +=
                    dtGroups.Select("group_id = " + drCurrentHour["group_id"].ToString())[0]["name"].ToString();
            }
        }

        public static GridView FillTeacherGrid(int nTeacherID, DataTable dtScheduleTable, bool bForPrint = false)
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
                    }
                }
                gvrCurrentRow.Cells[0].Visible = false;
            }
            return gvScheduleView;
        }


   
        public static void GetResponseSMS()
        {
            String url = "https://019sms.co.il/api";
            DataTable dtLastDateSent = DBConnection.Instance.GetDataTableByQuery("select value from prefernces where name = last send date");
            string strLastDateSent = (string)dtLastDateSent.Rows[0]["value"];

            //String testUrl = "https://www.019sms.co.il:8090/api/test";
            string xml = @"<?xml version='1.0' encoding='UTF-8'?><incoming><user><username>019sms</username><password>050618</password></user>
                            <from>"+strLastDateSent+"</from><to>"+DateTime.Now.ToString("dd/mm/yy hh:mm")+"</to></incoming>";

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Method = "POST";
            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            webRequest.ContentType = "application/xml";
            webRequest.ContentLength = (long)bytes.Length;
            Stream requestStream = webRequest.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            WebResponse response = webRequest.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream);
            string result = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            response.Close();
            checkResult(result);
            
        }

        private static void checkResult(string result)
        {
            XmlDocument xmlResponse = new XmlDocument();
            xmlResponse.LoadXml(result);
            XmlNodeList allResponse = xmlResponse.GetElementsByTagName("transaction");
            foreach (XmlElement item in allResponse)
            {
                string phoneNumber = item.ChildNodes.Item(0).InnerText.Replace("972","0");
                string messege = item.ChildNodes.Item(2).InnerText;
                if (messege.Contains("כן"))
                {
                    
                    string updateQuery = "select id from students where replace(father_cellphone,'-','')='" + phoneNumber + "' or replace(mother_cellphone,'','')='" + phoneNumber + "'";

                }
            }
        }

    }
}