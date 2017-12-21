using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;

namespace ERPPlugIn
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //new UpdateCommandBuilder().ExecuteNonQuery("Update UserInfo set online ='N' where operator_id = '" + HttpContext.Current.Request.Cookies["cookie"].Values["id"] + "'");
                Session.Abandon();
            }
        }
    }
}