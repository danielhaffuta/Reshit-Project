using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ReshitScheduler
{
    public class Helper
    {
        public static void ShowMessage(ClientScriptManager ClientScript,string strMessage)
        {
            ClientScript.RegisterStartupScript(typeof(BasePage), "myalert", "alert('" + strMessage + "');", true);

        }


    }
}