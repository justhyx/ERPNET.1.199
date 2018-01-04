using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace ERPPlugIn.Class
{
    public class ConnectionFactory
    {
        public static string ConnectionString_hudsonwwwroot = ConfigurationManager.ConnectionStrings["hudsonwwwroot"].ConnectionString;
    }
}