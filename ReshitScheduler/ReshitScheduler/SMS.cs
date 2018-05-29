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
        private string strParentName;
        private string strPhoneNumber;
        private string strStudentName;
        private string strCoureName;
        private string strGroupName;
        private string strDay;

        public string phoneNumber
        {
            set { strPhoneNumber = value; }
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
        public string CoureName
        {
            set { strCoureName = value; }
            get { return strCoureName; }
        }
        public string GroupName
        {
            set { strGroupName = value; }
            get { return strGroupName; }
        }
        public string Day
        {
            set { strDay = value; }
            get { return strDay; }
        }

        public bool send()
        {
            string messegeForamt = "שלום <<parentName>>, האם אתה מוכן שבנך <<studentName>> יעבור משיעור <<courseName>> אשר נלמד ביום <<day>> לקבוצה <<groupName>> השב כן או לא";
            //String testUrl = "https://www.019sms.co.il:8090/api/test";
            string messege;
            messege = messegeForamt.Replace("<<parentName>>", this.ParentName).Replace("<<studentName>>", this.StudentName).Replace("<<courseName>>", this.CoureName).Replace("<<day>>", this.Day).Replace("<<groupName>>", this.GroupName);
            return GenereteSMS(messege, this.phoneNumber);
        }
        private bool GenereteSMS(string messege, string phoneNumber)
        {
            string xml = @"<?xml version='1.0' encoding='UTF-8'?><sms><user><username>019sms</username><password>050618</password>
            </user><source>Reshit</source><destinations><phone>" + phoneNumber + "</phone></destinations><message>" + messege + "</message><response>1</response></sms>";
            String url = "https://www.019sms.co.il:8090/api/test";
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
            return checkResult(result);


        }
        private static bool checkResult(string result)
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