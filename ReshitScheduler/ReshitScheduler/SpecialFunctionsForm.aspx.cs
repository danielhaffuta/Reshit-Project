using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class SpecialFunctionsForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnIncreaseYear(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                IncreaseYear();
            }
        }

        private void IncreaseYear()
        {
            DBConnection.Instance.IncreaseCurrentYearID();
            Response.Redirect("LoginForm.aspx");

        }

        protected void BtnChangeSemester_Click(object sender, EventArgs e)
        {
            int semesterNum;
            if (FirstSemester.Checked)
                semesterNum = 1;
            else
                semesterNum = 2;
            string message;
            if (semesterNum == 1)
                message = "מבצע מעבר למחצית הראשונה של השנה";
            else
                message = "מבצע מעבר למחצית השניה של השנה";
            Helper.ShowMessage(ClientScript, message);
            DBConnection.Instance.ChangeSemester(semesterNum);
        }
    }
}