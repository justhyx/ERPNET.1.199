using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using ERPPlugIn.Class;

namespace ERPPlugIn
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitializeCulture();
                if (Session["UserName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                //UserCount.Text = Application["UserCount"].ToString();
                UserCount.Text = getCount();
                //new UpdateCommandBuilder().ExecuteNonQuery("Update UserInfo set online ='Y' where operator_id = '" + HttpContext.Current.Request.Cookies["cookie"].Values["id"] + "'");
            }
        }
        protected string getCount()
        {
            return CommadMethod.count.ToString();
        }
        protected override void InitializeCulture()
        {
            string currentCulture = (string)Session["Culture"];
            if (string.IsNullOrEmpty(currentCulture))
            {
                currentCulture = "en-US";
                Session["Culture"] = "en-US";
            }
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(currentCulture);
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(currentCulture);

        }
    }
}