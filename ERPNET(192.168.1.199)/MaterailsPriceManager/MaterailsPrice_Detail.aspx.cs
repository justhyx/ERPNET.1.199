using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using HUDSON.ERP.DBCommandDAL;

namespace ERPPlugIn.MaterailsPriceManager
{
    public partial class MaterailsPrice_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string name = Request.QueryString["name"];
                SelectCommandBuilder s = new SelectCommandBuilder("materials_newest_price");
                s.SelectColumn("name as '部番'");
                s.SelectColumn("price as '价格'");
                s.SelectColumn("wb_name as '币种'");
                s.SelectColumn("exchange_rate as '汇率'");
                s.SelectColumn("CONVERT(varchar(100),update_date,20) as '更改日期'");
                s.ConditionsColumn("name", name);
                s.OrderColumn("update_date");
                s.getSelectCommand(OrderType.desc);
                dgvDataDetail.DataSource = s.ExecuteDataTable();
                dgvDataDetail.DataBind();

            }
        }
    }
}