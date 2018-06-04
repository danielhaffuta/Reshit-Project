using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReshitScheduler
{
    public partial class p : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataTable dtCourses = DBConnection.Instance.GetDataTableByQuery("select id,course_name,priority from courses order by priority");
                DataTable dtGroups = DBConnection.Instance.GetDataTableByQuery("select id,group_name,priority from groups order by priority");

                foreach (DataRow currentCourse in dtCourses.Rows)
                {
                    string strValue = "course" + currentCourse["id"].ToString() + "id" + currentCourse["priority"].ToString();
                    ListItem liNewListItem = new ListItem() { Text = (string)currentCourse["course_name"], Value = strValue};
                    lbPrioritization.Items.Add(liNewListItem);
                }
                foreach (DataRow currentGroup in dtGroups.Rows)
                {
                    string strValue = "groups" + currentGroup["id"].ToString() + "id" + currentGroup["priority"].ToString();
                    ListItem liNewListItem = new ListItem() { Text = (string)currentGroup["group_name"], Value = strValue };
                    liNewListItem.Attributes.Add("class", "group");
                    liNewListItem.Attributes.Add("id", currentGroup["id"].ToString());
                    lbPrioritization.Items.Add(liNewListItem);
                }
                List<ListItem> list = new List<ListItem>(lbPrioritization.Items.Cast<ListItem>());
                list = list.OrderBy(x => Convert.ToInt32(x.Value.Substring(x.Value.IndexOf("id")+2))).ToList<ListItem>();
                lbPrioritization.Items.Clear();
                lbPrioritization.Items.AddRange(list.ToArray<ListItem>());


            }


        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            int nCurrentIndex = lbPrioritization.SelectedIndex;
            if (nCurrentIndex > 0)
            {
                ListItem liTemp = lbPrioritization.Items[nCurrentIndex-1];
                ListItem currentItem = lbPrioritization.Items[nCurrentIndex];
                lbPrioritization.Items.RemoveAt(nCurrentIndex - 1);
                lbPrioritization.Items.Insert(nCurrentIndex - 1, currentItem);
                lbPrioritization.Items.RemoveAt(nCurrentIndex);
                lbPrioritization.Items.Insert(nCurrentIndex, liTemp);
            }
            
        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            int nCurrentIndex = lbPrioritization.SelectedIndex;
            if (nCurrentIndex < lbPrioritization.Items.Count-1)
            {
                ListItem liTemp = lbPrioritization.Items[nCurrentIndex + 1];
                ListItem currentItem = lbPrioritization.Items[nCurrentIndex];
                lbPrioritization.Items.RemoveAt(nCurrentIndex + 1);
                lbPrioritization.Items.Insert(nCurrentIndex + 1, currentItem);
                lbPrioritization.Items.RemoveAt(nCurrentIndex);
                lbPrioritization.Items.Insert(nCurrentIndex, liTemp);
            }

        }

        protected void Save_Click(object sender, EventArgs e)
        {
            string strUpdateQuery = "";
            string strCurrentValue = "";
            for (int currentItem = 0; currentItem <lbPrioritization.Items.Count; currentItem++)
            {
                strCurrentValue = lbPrioritization.Items[currentItem].Value;
                string nItemID = strCurrentValue.Substring(6,strCurrentValue.IndexOf("id")-6);
                if (lbPrioritization.Items[currentItem].Value.Contains("course"))
                {

                    strUpdateQuery += " update courses set priority = " + (currentItem + 1) + " where id=" + nItemID+";";
                }
                else if (lbPrioritization.Items[currentItem].Value.Contains("groups"))
                {
                    strUpdateQuery += " update groups set priority = " + (currentItem+1) + " where id=" + nItemID+";";
                }
            }

            int x = 2;
            DBConnection.Instance.ExecuteNonQuery(strUpdateQuery);
          //  DBConnection.Instance.ExecuteNonQuery(strUpdateGroups);



        }
    }
    
}