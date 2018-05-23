using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class TeacherForm : BasePage
    {
        private DataTable dtTeacherScheduleTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTeacherSchedule();
        }

        private void LoadTeacherSchedule()
        {
            dtTeacherScheduleTable = FormsUtilities.BuildEmptySchedule();
            FormsUtilities.FillTeacherSchedule(LoggedInTeacher.ID, dtTeacherScheduleTable);
            pnlTeacherSchedule.Controls.Add(FormsUtilities.FillTeacherGrid(LoggedInTeacher.ID, dtTeacherScheduleTable));
        }
    }
}