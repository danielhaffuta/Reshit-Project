using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected BasePage bpCurrentPage = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            bpCurrentPage = this.Page as BasePage;

        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginForm.aspx");
        }
    }
}