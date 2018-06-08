
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
        protected const string SCRIPT_DOFOCUS =
              @"window.setTimeout('DoFocus()', 1);
            function DoFocus()
            {
                try {
                    document.getElementById('REQUEST_LASTFOCUS').focus();
                } catch (ex) {}
            }";

        protected int nYearID;
        public Teacher LoggedInTeacher;
        protected static string strPreviousPage;
        protected int nClassID, nHourId, nDayId, nGroupId;



        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                strPreviousPage = Request.UrlReferrer?.ToString() ?? "LoginForm.aspx";
            }
            FillTeacher();
            nYearID = DBConnection.Instance.GetCurrentYearID();
            FillIDs();
            base.OnLoad(e);
            AddOnFocusAttribute(this.Controls);

            Page.ClientScript.RegisterStartupScript(typeof(LessonForm), "ScriptDoFocus",
                                                    SCRIPT_DOFOCUS.Replace("REQUEST_LASTFOCUS", Request["__LASTFOCUS"]), true);
        }

        private void AddOnFocusAttribute(ControlCollection ccControls)
        {
            foreach (Control ctrlControl in ccControls)
            {
                if (ctrlControl is WebControl)
                {
                    (ctrlControl as WebControl).Attributes.Add("onfocus", "try{document.getElementById('__LASTFOCUS').value=this.id} catch(e) {}");
                }
                AddOnFocusAttribute(ctrlControl.Controls);
            }
        }

        private void FillTeacher()
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
        }
        private void FillIDs()
        {
            if (Session["IDs"] != null)
            {
                string[] strIDs = Session["IDs"].ToString().Split('-');
                nClassID = strIDs.Length > 0 ? Convert.ToInt32(strIDs[0]) : 0;
                nHourId = strIDs.Length > 1 ? Convert.ToInt32(strIDs[1]) : 0;
                nDayId = strIDs.Length > 2 ? Convert.ToInt32(strIDs[2]) : 0;
                nGroupId = strIDs.Length > 3 ? Convert.ToInt32(strIDs[3]) : 0;
            }
            else
            {
                nClassID = Convert.ToInt32( Request.QueryString["ClassID"]?.ToString() ?? LoggedInTeacher.ClassID.ToString());

            }
        }

        protected void HideNavBar()
        {
            Page.Master.FindControl("navbar").Visible = false;
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