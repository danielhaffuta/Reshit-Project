using System;
using System.Collections.Generic;
using System.Data;
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
            if (bpCurrentPage.LoggedInTeacher.Type == "רכז")
            {
                DataTable dtClasses = DBConnection.Instance.GetTeacherClasses(bpCurrentPage.LoggedInTeacher.ID);

                string strDropDownHtml = "<div class=\"dropdown\">";
                strDropDownHtml += " <a class=\"nav-item nav-link dropdown-toggle\"";
                strDropDownHtml += " data-toggle=\"dropdown\" id=\"classesManageDropDown\"";
                strDropDownHtml += " aria-haspopup=\"true\" aria-expanded=\"false\"";
                strDropDownHtml += " href = \"#\">ניהול כיתות</a>";
                strDropDownHtml += " <div class=\"dropdown-menu dropdown-menu-right text-right\" aria-labelledby=\"classManageDropDown\">";
                
                foreach (DataRow drCurrentClass in dtClasses.Rows)
                {
                    strDropDownHtml += " <a class=\"nav-item nav-link dropdown-toggle\"";
                    strDropDownHtml += " data-toggle=\"dropdown\" id=\"class" + drCurrentClass["class_id"] + "ManageDropDown\"";
                    strDropDownHtml += " aria-haspopup=\"true\" aria-expanded=\"false\"";
                    strDropDownHtml += " href = \"#\">" + drCurrentClass["name"] + "</a>";

                    strDropDownHtml += " <div class=\"dropdown-submenu\">";
                    

                    
                    strDropDownHtml += " <a class=\"dropdown-item\" href=\"ClassPage.aspx?ClassID="+ drCurrentClass["class_id"] +"\"> מערכת כיתה</a>";
                    strDropDownHtml += " <a class=\"dropdown-item\" href=\"ClassStudents.aspx?ClassID=" + drCurrentClass["class_id"] + "\"> תלמידים</a>";
                    strDropDownHtml += " <a class=\"dropdown-item\" href=\"AddStudentForm.aspx?ClassID=" + drCurrentClass["class_id"] + "\"> הוספת תלמיד חדש</a>";
                    strDropDownHtml += " </div>";


                }
                strDropDownHtml += "</div>";
                strDropDownHtml += "</div>";
                navbar_extra.Controls.Add(new LiteralControl(strDropDownHtml));
            }
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginForm.aspx");
        }
    }
}