using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace ReshitScheduler
{
    public partial class AdminForm : System.Web.UI.Page
    {
        public static Teacher LoggedInTeacher;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInTeacher"] != null)
            {

                LoggedInTeacher = Session["LoggedInTeacher"] as Teacher;
                AdminName.Text = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(LoggedInTeacher.FirstName) + " " + System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(LoggedInTeacher.LastName);

                PopulateMenu();
                //Button btnLogout = new Button() { Text = "Logout" };
                //btnLogout.Click += BtnLogout_Click;
                //form1.Controls.Add(btnLogout);
                //Disconect.Click += BtnLogout_Click;
            }
            else
            {
                Response.Redirect("LoginForm.aspx");
                return;
            }

        }

        [System.Web.Services.WebMethod]
        public void BtnLogout_Click()// not in use
        {
            
            Session["LoggedInTeacher"] = null;
            Response.Redirect("LoginForm.aspx");
            return;
        }
        private void PopulateMenu()
        {
            //DataTable dtTables = DBConnection.Instance.GetDataTableByQuery("select table_name from INFORMATION_SCHEMA.tables where table_schema = 'reshit'");

            DataTable dtTables = DBConnection.Instance.GetDataTableByQuery("select table_name,hebrew_name from tables_information");
            string tableName = "";
            string tableDisplayName="";
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem(tableDisplayName, tableName));
            foreach (DataRow CurrentTable in dtTables.Rows)
            {
                tableName = (string)CurrentTable["table_name"];
                tableDisplayName = (string)CurrentTable["hebrew_name"];
                items.Add(new ListItem(tableDisplayName, tableName));
               
            }
            courseEdit.Items.AddRange(items.ToArray());
        }

        protected void itemSelected(object sender, EventArgs e)
        {
            DropDownList dropDown = (DropDownList)sender;
            TableEdit.strTableName = dropDown.SelectedValue;
            Response.Redirect("TableEdit.aspx");

        }
    }
}