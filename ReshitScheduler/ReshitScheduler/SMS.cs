using System;
using System.Collections.Generic;
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
            int nConfarmationNumber = Convert.ToInt32(DBConnection.Instance.GetDataTableByQuery("select count(*) as nStudentGroups,student_id from students_schedule where approval_status_id = 1  and student_id = "+this.StudentID).Rows[0]["nStudentGroups"]);
            nConfarmationNumber = nConfarmationNumber + nConfarmationNumber - 1;
            DBConnection.Instance.ExecuteNonQuery("update students_schedule set confarmation_number=" + nConfarmationNumber+" where group_id="+this.nGroupId+" and student_id="+this.nStudentID);
            string messegeForamt = (string)DBConnection.Instance.GetDataTableByQuery("select value from preferences where name ='sms messege format'").Rows[0]["value"];
            string messege;
            if(this.GroupPurpose=="")
            {
                messegeForamt = messegeForamt.Replace("מטרת הקבוצה: <<gP>>", "");
            }
            messege = messegeForamt.Replace("<<pN>>", this.ParentName).Replace("<<sN>>", this.StudentName).Replace("<<gN>>", this.GroupName).Replace("<<cN>>", this.CourseName).Replace("<<gP>>", this.GroupPurpose).Replace("<<nC>>", nConfarmationNumber.ToString()).Replace("<<nD>>", (nConfarmationNumber + 1).ToString());
            //messege+= messegeForamt
            return GenereteSMS(messege, this.PhoneNumber);
        }
        private bool GenereteSMS(string messege, string phoneNumber)
        {
            string xml = @"<?xml version='1.0' encoding='UTF-8'?><sms><user><username>019sms</username><password>050618</password>
            </user><source>Reshit</source><destinations><phone>" + phoneNumber + "</phone></destinations><message>" + messege + "</message><response>1</response></sms>";
            String url = "https://www.019sms.co.il:8090/api/test";
          // url= "https://www.019sms.co.il/api";
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
            return CheckResult(result);


        }
        private static bool CheckResult(string result)
        {
            result = result.Substring(result.IndexOf("<status>"));
            result = result.Substring(0, result.IndexOf("</status>"));
            if (result.Contains("0"))
            {
                return true;
            }
            return false;

        }
    }
}