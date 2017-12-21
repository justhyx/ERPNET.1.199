using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;

namespace ERPPlugIn.Class
{
    public class PageBase : System.Web.UI.Page
    {
        protected string ConnectionString { get; set; }
        public PageBase()
        {
            //
            // TODO: Add constructor logic here
            //
            ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        protected override void InitializeCulture()
        {
            //string currentCulture = (string)Session["Culture"];
            string currentCulture = "zh-cn";//HttpContext.Current.Request.Cookies["cookie"].Values["langue"];
            //string currentCulture = ConfigurationManager.AppSettings["L"].ToString();
            if (string.IsNullOrEmpty(currentCulture))
            {
                currentCulture = "en-us";
                Session["Culture"] = "en-us";
            }

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(currentCulture);
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(currentCulture);
        }
    }
}