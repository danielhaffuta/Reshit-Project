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
        private string strParentName;
        private string strPhoneNumber;
        private string strStudentName;
        private string strCourseName;
        private string strGroupName;
        private int nDayID;
        private int nHourID;
        private int nStudentScheduleID;

        public string PhoneNumber
        {
            set { strPhoneNumber = value.Replace("-",""); }
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

        public bool Send()
        {
            string messegeForamt = "שלום <<parentName>>, האם אתה מוכן שבנך <<studentName>> יעבור משיעור <<courseName>> אשר נלמד ביום <<day>> לקבוצה <<groupName>> השב כן או לא";
            //String testUrl = "https://www.019sms.co.il:8090/api/test";
            string messege;
            messege = messegeForamt.Replace("<<parentName>>", this.ParentName).Replace("<<studentName>>", this.StudentName).Replace("<<courseName>>", this.CourseName).Replace("<<day>>", this.Day.ToString()).Replace("<<groupName>>", this.GroupName);
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