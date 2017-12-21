using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ERPPlugIn
{
    public partial class LoginRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string userId = Request.QueryString["id"];
                string languea = Request.QueryString["langue"];
                HttpCookie cook = new HttpCookie("cookie");
                cook.Values.Add("id", userId);
                //Session["Culture"] = languea.ToLower() == "english" ? "en-us" : "zh-cn";
                string l = "en-us";
                switch (languea.ToLower())
                {
                    case "thailand":
                        l = "th-th";
                        break;
                    case "chinese":
                        l = "zh-cn";
                        break;
                    default:
                        l = "en-us";
                        break;
                }
                cook.Values.Add("langue", l);
                HttpContext.Current.Response.Cookies.Add(cook);
                Response.Redirect(ConfigurationManager.AppSettings["responseUrl"]);
            }
        }
    }
}