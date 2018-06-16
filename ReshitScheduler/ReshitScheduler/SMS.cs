using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ReshitScheduler
{
    public class SMS
    {
        private int nStudentID;
        private int nGroupId;
        private string strParentName;
        private string strPhoneNumber;
        private string strStudentName;
        private string strCourseName;
        private string strGroupName;
        private string strGroupPurpose;
        private int nDayID;
        private int nHourID;
        private int nStudentScheduleID;
        static int ALLOW_WAITING_SMS=7;
        public string PhoneNumber
        {
            set { strPhoneNumber = value.Replace("-", ""); }
            get { return strPhoneNumber; }
        }

        public string ParentName
        {
            set { strParentName = value; }
            get { return strParentName; }
        }
        public string StudentName
        {
            set { strStudentName = value; }
            get { return strStudentName; }
        }
        public string CourseName
        {
            set { strCourseName = value; }
            get { return strCourseName; }
        }
        public string GroupName
        {
            set { strGroupName = value; }
            get { return strGroupName; }
        }
        public int Day
        {
            set { nDayID = value; }
            get { return nDayID; }
        }

        public int StudentScheduleID
        {
            get
            {
                return nStudentScheduleID;
            }

            set
            {
                nStudentScheduleID = value;
            }
        }

        public int HourID
        {
            get
            {
                return nHourID;
            }

            set
            {
                nHourID = value;
            }
        }

        public int StudentID
        {
            get
            {
                return nStudentID;
            }

            set
            {
                nStudentID = value;
            }
        }
        public int GroupId
        {
            get { return nGroupId; }
            set { nGroupId = value; }
        }
        public string GroupPurpose
        {
            get { return strGroupPurpose; }
            set { strGroupPurpose = value; }
        }

        public bool Send()
        {
            int? nConfarmationNumber = 1;
            DataTable dtConfatmationNumber = DBConnection.Instance.GetDataTableByQuery(@"select confarmation_number from students_schedule where student_id in
                                             (select student_id from students where mother_cellphone='" + this.PhoneNumber+"' or father_cellphone='"+this.PhoneNumber+"') " + " order by confarmation_number desc limit 1");
            if(dtConfatmationNumber.Rows.Count== ALLOW_WAITING_SMS)
            {
                // need to alert to the techer??
            }
            if (Convert.IsDBNull(dtConfatmationNumber.Rows[0]["confarmation_number"]))
            {
                nConfarmationNumber = 1;
            }
            else
            {
                nConfarmationNumber = (Convert.ToInt32(dtConfatmationNumber.Rows[0]["confarmation_number"]) + 2);// % ALLOW_WAITING_SMS;
            }
            DBConnection.Instance.ExecuteNonQuery("update students_schedule set confarmation_number=" + nConfarmationNumber+" where group_id="+this.nGroupId+" and student_id="+this.nStudentID);
            string messegeForamt = (string)DBConnection.Instance.GetDataTableByQuery("select value from preferences where name ='sms messege format'").Rows[0]["value"];
            string messege;
            if(this.GroupPurpose=="")
            {
                messegeForamt = messegeForamt.Replace("מטרת הקבוצה: <<gP>>", "");
            }
            messege = messegeForamt.Replace("<<pN>>", this.ParentName).Replace("<<sN>>", this.StudentName).Replace("<<gN>>", this.GroupName).Replace("<<cN>>", this.CourseName).Replace("<<gP>>", this.GroupPurpose).Replace("<<nC>>", nConfarmationNumber.ToString()).Replace("<<nD>>", (nConfarmationNumber + 1).ToString());
            return GenereteSMS(messege, this.PhoneNumber);
        }
        private bool GenereteSMS(string messege, string phoneNumber)
        {
            String url = DBConnection.Instance.GetSmsUrl();
            string strSendXml = @"<?xml version='1.0' encoding='UTF-8'?><sms><user><username>resheet</username><password>batya123</password>
            </user><source>Reshit</source><destinations><phone>" + phoneNumber + "</phone></destinations><message>" + messege + "</message><response>1</response></sms>";

            string strResult = FormsUtilities.GetResultFromSmsServer(url, strSendXml);
            return CheckResult(strResult);


        }
        private static bool CheckResult(string strResult)
        {
            strResult = strResult.Substring(strResult.IndexOf("<status>"));
            strResult = strResult.Substring(0, strResult.IndexOf("</status>"));
            if (strResult.Contains("0"))
            {
                return true;
            }
            return false;

        }
    }
}