using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class BasePage : System.Web.UI.Page
    {
        protected int nYearID;
        public Teacher LoggedInTeacher;
        protected static string strPreviousPage;
        protected override void OnLoad(EventArgs e)
        {
            if (Session["LoggedInTeacher"] == null)
            {
                LoggedInTeacher = new Teacher()
                {
                    FirstName = "אדוני",
                    LastName = "הרכז",
                    ID = 10,
                    ClassID = 5,
                    Type = "רכז",
                };
            }
            else
            {
                LoggedInTeacher = Session["LoggedInTeacher"] as Teacher;
            }
            if (!IsPostBack)
            {
                strPreviousPage = Request.UrlReferrer?.ToString() ?? "LoginForm.aspx";

            }

            nYearID = DBConnection.Instance.GetCurrentYearID();

            base.OnLoad(e);
        }

        //protected void BtnBack_Click(object sender, EventArgs e)
        //{
        //    GoBack();
        //}

        //protected void GoBack()
        //{
        //    Response.Redirect(strPreviousPage ?? "LoginForm.aspx");
        //}
    }
}