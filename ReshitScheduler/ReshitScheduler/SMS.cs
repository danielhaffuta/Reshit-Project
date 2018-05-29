using System;
using System.Collections.Generic;
using System.Linq;
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
        private int nStudentScheduleID;

        public string PhoneNumber
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
    }
}