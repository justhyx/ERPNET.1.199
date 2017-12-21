using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Configuration;
using System.Collections;
using ERPPlugIn.Class;
namespace ERPPlugIn
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["UserCount"] = 0;
            CommadMethod.count = 0;
            Application.UnLock();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            //Application["UserCount"] = Int32.Parse(Application["UserCount"].ToString()) + 1;
            CommadMethod.count += 1;
            Application.UnLock();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            //Application["UserCount"] = Int32.Parse(Application["UserCount"].ToString()) - 1;
            CommadMethod.count -= 1;
            Application.UnLock();
        }
        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}