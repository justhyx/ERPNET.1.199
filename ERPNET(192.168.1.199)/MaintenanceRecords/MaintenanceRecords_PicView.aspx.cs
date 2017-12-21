using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERPPlugIn.MaintenanceRecords
{
    public partial class MaintenanceRecords_PicView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string url = Request.QueryString["picId"];
                this.PicView.ImageUrl = "..\\UploadPics\\" + url;
            }
        }
    }
}