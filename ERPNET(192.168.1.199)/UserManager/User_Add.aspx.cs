using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERPPlugIn.Class;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;


namespace ERPPlugIn.UserManager
{
    public partial class User_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            UpdateCommandBuilder u = new UpdateCommandBuilder("UserInfo");
            Random rnd = new Random();
            int key = rnd.Next(1000, 9999);
            string pwd = ClassDES.Encrypt("HUDSON.", key.ToString());
            u.UpdateColumn("password", pwd);
            u.UpdateColumn("PassWordKey", key.ToString());
            u.ConditionsColumn("operator_id", "0000");
            u.getUpdateCommand();
            u.ExecuteNonQuery();
        }
    }
}