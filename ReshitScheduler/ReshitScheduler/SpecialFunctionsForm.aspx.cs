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

        protected void BtnIncreaseSemester_Click(object sender, EventArgs e)
        {
            Helper.ShowMessage(ClientScript, "מבצע מעבר למחצית שניה של השנה");
            DBConnection.Instance.IncreaseSemester();
        }
    }
}