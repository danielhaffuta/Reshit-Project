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
        protected static string strPreviousPage;
        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                strPreviousPage = Request.UrlReferrer?.ToString() ?? "LoginForm.aspx";

            }
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