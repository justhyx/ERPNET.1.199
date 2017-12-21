using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data.SqlClient;

namespace ERPPlugIn.MoldManager.M_materailsManager
{
    public partial class M_materails_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dgvVenderList.DataSource = getAllData("");
                dgvVenderList.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<m> mList = getAllData(txtName.Value);
            dgvVenderList.DataSource = mList;
            dgvVenderList.DataBind();
        }

        private List<m> getAllData(string con)
        {
            List<m> mList = new List<m>();
            SelectCommandBuilder s = new SelectCommandBuilder("m_materials");
            s.SelectColumn("id");
            s.SelectColumn("name");
            s.SelectColumn("spec");
            s.SelectColumn("sccj_id");
            s.SelectColumn("new_price");
            s.SelectColumn("wb_name");
            s.SelectColumn("lb1_id");
            s.SelectColumn("texture");
            s.SelectColumn("color");
            s.SelectColumn("dm");
            s.ConditionsColumn("1", 1);
            string sql = s.getSelectCommand();
            if (txtName.Value != string.Empty)
            {
                sql += " and name like '" + txtName.Value + "%'";
            }
            SqlDataReader dr = s.ExecuteReader(sql);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    m m = new m()
                    {
                        id = dr.GetInt64(0),
                        mc = dr.IsDBNull(1) ? "" : dr.GetString(1),
                        gg = dr.IsDBNull(2) ? "" : dr.GetString(2),
                        gys = dr.IsDBNull(3) ? "" : dr.GetString(3),
                        dj = dr.IsDBNull(4) ? "0.00" : dr.GetDouble(4).ToString("0.00#"),
                        bz = dr.IsDBNull(5) ? "" : dr.GetString(5),
                        lx = dr.IsDBNull(6) ? "" : dr.GetString(6),
                        cz = dr.IsDBNull(7) ? "" : dr.GetString(7),
                        ys = dr.IsDBNull(8) ? "" : dr.GetString(8),
                        jc = dr.IsDBNull(9) ? "" : dr.GetString(9)
                    };
                    mList.Add(m);
                }
            }
            return mList;
        }
        protected void dgvVenderList_DataBound(object sender, EventArgs e)
        {

        }

        protected void dgvVenderList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvVenderList.PageIndex = e.NewPageIndex;
            dgvVenderList.DataSource = getAllData(string.Empty);
            dgvVenderList.DataBind();
        }

        protected void dgvVenderList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = "delete from m_materials where id= " + ((Label)dgvVenderList.Rows[e.RowIndex].Cells[0].FindControl("Label1")).Text + "";
            UpdateCommandBuilder u = new UpdateCommandBuilder();
            u.ExecuteNonQuery(sql);
            dgvVenderList.DataSource = getAllData("");
            dgvVenderList.DataBind();
        }
    }
    public class m
    {
        public long id { get; set; }
        public string mc { get; set; }
        public string gg { get; set; }
        public string gys { get; set; }
        public string dj { get; set; }
        public string bz { get; set; }
        public string lx { get; set; }
        public string cz { get; set; }
        public string ys { get; set; }
        public string jc { get; set; }
    }
}