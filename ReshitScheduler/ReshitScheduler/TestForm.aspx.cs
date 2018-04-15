using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class TestForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button bb = new Button();
            bb.Click += Bb_Click;
            form1.Controls.Add(bb);
        }

        private void Bb_Click(object sender, EventArgs e)
        {
            int a = 4;
            a++;
        }
    }
}