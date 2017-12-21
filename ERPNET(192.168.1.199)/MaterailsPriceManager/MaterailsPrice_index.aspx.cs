using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.IO;

namespace ERPPlugIn.MaterailsPriceManager
{
    public partial class MaterailsPrice_index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dgvMaterailList.DataSource = getIndexPageData();
                dgvMaterailList.DataBind();
                ViewState["GridView_ReferedDataDetail_SortDirection"] = "ASC";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Value.Trim()) && ddlVendorList.SelectedItem.Value.Trim() == "0")
            {
                dgvMaterailList.DataSource = getIndexPageData();
            }
            else
            {
                dgvMaterailList.DataSource = getIndexPageData(txtName.Value.Trim(), ddlVendorList.SelectedItem.Value.Trim());
            }
            dgvMaterailList.DataBind();
        }

        protected void btnA_Click(object sender, EventArgs e)
        {

        }

        protected void btnB_Click(object sender, EventArgs e)
        {

        }

        public List<material> getIndexPageData()
        {
            List<material> mList = new List<material>();
            SelectCommandBuilder select = new SelectCommandBuilder("materials");
            select.SelectColumn("name");
            select.SelectColumn("sccj_id");
            select.SelectColumn("new_price");
            select.SelectColumn("wb_name");
            select.ConditionsColumn("1", 1);
            select.getSelectCommand();
            SqlDataReader dr = select.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    material m = new material()
                    {
                        name = dr.IsDBNull(0) ? "NULL" : dr.GetString(0),
                        sccj = dr.IsDBNull(1) ? "NULL" : getVendorById(dr.GetString(1)),
                        price = dr.IsDBNull(2) ? 0.0 : dr.GetDouble(2),
                        currency = dr.IsDBNull(3) ? "N" : dr.GetString(3),
                        exchangeRate = getexchangeRate(dr.IsDBNull(3) ? "N" : dr.GetString(3))
                    };
                    mList.Add(m);
                }
            }
            ViewState["DataTable_GridView_ReferedDataDetail"] = ListToDataaTable(mList);
            return mList;

        }
        public string getVendorById(string vendor_id)
        {
            string venderName = "NULL";
            SelectCommandBuilder s = new SelectCommandBuilder("vendor");
            s.SelectColumn("vendor_name");
            s.ConditionsColumn("vendor_id", vendor_id);
            s.getSelectCommand();
            DataTable dt = s.ExecuteDataTable();
            if (dt != null && dt.Rows.Count != 0)
            {
                venderName = dt.Rows[0]["vendor_name"].ToString();
            }
            return venderName;
        }


        public List<material> getIndexPageData(string name, string sccj)
        {
            List<material> mList = new List<material>();
            SelectCommandBuilder select = new SelectCommandBuilder("materials");
            select.SelectColumn("name");
            select.SelectColumn("sccj_id");
            select.SelectColumn("new_price");
            select.SelectColumn("wb_name");
            select.ConditionsColumn("1", 1);
            string sql = select.getSelectCommand();
            if (!string.IsNullOrEmpty(name))
            {
                sql += "and name like '%" + name + "%'";
            }
            if (!string.IsNullOrEmpty(sccj))
            {
                select.ConditionsColumn("sccj_id", sccj);
                sql += "and sccj_id = '" + sccj + "'";
            }
            SqlDataReader dr = select.ExecuteReader(sql);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    material m = new material()
                    {
                        name = dr.GetString(0),
                        sccj = getVendorById(dr.GetString(1)),
                        price = dr.IsDBNull(2) ? 0.00 : dr.GetDouble(2),
                        currency = dr.IsDBNull(3) ? "RMB" : dr.GetString(3),
                        exchangeRate = getexchangeRate(dr.IsDBNull(3) ? "RMB" : dr.GetString(3))
                    };
                    mList.Add(m);
                }
            }
            ViewState["DataTable_GridView_ReferedDataDetail"] = ListToDataaTable(mList);
            return mList;

        }

        public double getexchangeRate(string currency)
        {
            SelectCommandBuilder s = new SelectCommandBuilder("prd_dictate_wb");
            double exchangeRate = 0.00;
            s.SelectColumn("wb_hl");
            s.ConditionsColumn("wb_name", currency);
            s.getSelectCommand();
            DataTable dt = s.ExecuteDataTable();
            if (dt != null && dt.Rows.Count != 0)
            {
                exchangeRate = Convert.ToDouble(dt.Rows[0]["wb_hl"]);
            }
            return exchangeRate;
        }

        protected void dgvMaterailList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvMaterailList.PageIndex = e.NewPageIndex;
            this.dgvMaterailList.DataSource = (DataTable)ViewState["DataTable_GridView_ReferedDataDetail"];
            this.dgvMaterailList.DataBind();
        }

        //public static string Obj2Json<T>(T data)
        //{
        //    try
        //    {
        //        System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(data.GetType());
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            serializer.WriteObject(ms, data);
        //            return Encoding.UTF8.GetString(ms.ToArray());
        //        }
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public static string ObjectToJson<T>(string jsonName, IList<T> IL)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"" + jsonName + "\":[");
            if (IL.Count > 0)
            {
                for (int i = 0; i < IL.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    Type type = obj.GetType();
                    PropertyInfo[] pis = type.GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        Json.Append("\"" + pis[j].Name.ToString() + "\":\"" + pis[j].GetValue(IL[i], null) + "\"");
                        if (j < pis.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < IL.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

        public string getJson()
        {
            return ObjectToJson("table", getIndexPageData());
        }

        protected void dgvMaterailList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "message", " <script language='javascript' >if(confirm('确认需要编辑?'))document.getElementById('Hf').value='1'; else document.getElementById('Hf').value='0'; </script>");
            string selectValue = (dgvMaterailList.Rows[e.NewEditIndex].FindControl("Label4") as Label).Text;
            this.dgvMaterailList.EditIndex = e.NewEditIndex;
            btnSearch_Click(sender, e as EventArgs);
            DropDownList d = (dgvMaterailList.Rows[e.NewEditIndex].FindControl("ddlcurrency") as DropDownList);
            List<string> slist = getCurrency();
            for (int i = 0; i < slist.Count; i++)
            {
                d.Items.Add(new ListItem(slist[i], i.ToString()));
            }
            int selectIndex = 0;
            for (int i = 0; i < d.Items.Count; i++)
            {
                if (selectValue == d.Items[i].Text)
                {
                    selectIndex = i;
                }
            }
            d.SelectedIndex = selectIndex;

        }

        protected void dgvMaterailList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.dgvMaterailList.EditIndex = -1;
            btnSearch_Click(sender, e as EventArgs);


        }

        protected void dgvMaterailList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string name = (dgvMaterailList.Rows[e.RowIndex].FindControl("Label1") as Label).Text;
            double price = Convert.ToDouble((dgvMaterailList.Rows[e.RowIndex].FindControl("txtPrice") as TextBox).Text);
            string currency = (dgvMaterailList.Rows[e.RowIndex].FindControl("ddlcurrency") as DropDownList).SelectedItem.Text;
            double exchangeRate = getexchangeRate(currency);

            UpdateCommandBuilder u = new UpdateCommandBuilder("materials");
            u.UpdateColumn("new_price", price);
            u.UpdateColumn("wb_name", currency);
            u.ConditionsColumn("name", name);
            string mainSql = u.getUpdateCommand();

            InsertCommandBuilder insert = new InsertCommandBuilder("materials_newest_price");
            insert.InsertColumn("name", name);
            insert.InsertColumn("price", price);
            insert.InsertColumn("wb_name", currency);
            insert.InsertColumn("exchange_rate", exchangeRate);
            insert.InsertColumn("update_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string insertSql = insert.getInsertCommand();

            List<string> sList = new List<string>();
            sList.Add(mainSql);
            sList.Add(insertSql);
            int i = insert.ExcutTransaction(sList);
            if (i != 0)
            {
                dgvMaterailList.EditIndex = -1;
                if (string.IsNullOrEmpty(txtName.Value.Trim()) && ddlVendorList.SelectedItem.Value.Trim() == "0")
                {
                    dgvMaterailList.DataSource = getIndexPageData();
                }
                else
                {
                    dgvMaterailList.DataSource = getIndexPageData(txtName.Value.Trim(), ddlVendorList.SelectedItem.Value.Trim());
                }
                dgvMaterailList.DataBind();
            }
        }

        public List<string> getCurrency()
        {
            List<string> sList = new List<string>();
            SelectCommandBuilder s = new SelectCommandBuilder("prd_dictate_wb");
            s.SelectColumn("wb_name");
            s.ConditionsColumn("1", 1);
            s.getSelectCommand();
            DataTable d = s.ExecuteDataTable();
            for (int i = 0; i < d.Rows.Count; i++)
            {
                sList.Add(d.Rows[i][0].ToString());
            }
            return sList;
        }

        protected void dgvMaterailList_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable DataTable_GridView_ReferedDataDetail = (DataTable)ViewState["DataTable_GridView_ReferedDataDetail"];
            DataView DataViewRefereDataDetail = DataTable_GridView_ReferedDataDetail.DefaultView;

            if (ViewState["DataTable_GridView_ReferedDataDetail"] != null)
            {
                if (ViewState["GridView_ReferedDataDetail_SortDirection"].ToString() == "Asc")
                {
                    ViewState["GridView_ReferedDataDetail_SortDirection"] = "Desc";
                    string MySortDirection = "Desc";
                    string MySortExpression = e.SortExpression + " " + MySortDirection;
                    DataViewRefereDataDetail.Sort = MySortExpression;
                }
                else
                {
                    ViewState["GridView_ReferedDataDetail_SortDirection"] = "Asc";
                    string MySortDirection = "Asc";
                    string MySortExpression = e.SortExpression + " " + MySortDirection;
                    DataViewRefereDataDetail.Sort = MySortExpression;
                }
            }
            ViewState["DataTable_GridView_ReferedDataDetail"] = DataViewRefereDataDetail.ToTable();
            dgvMaterailList.DataSource = DataViewRefereDataDetail;
            dgvMaterailList.DataBind();

        }

        protected DataTable ListToDataaTable(List<material> List)
        {
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口
            Type type = typeof(material);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列    
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in List)
            { 
                DataRow row = dt.NewRow();
                //给row 赋值    
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable    
                dt.Rows.Add(row);
            }
            return dt;
        }
        
        protected void btnSearchVendor_Click(object sender, EventArgs e)
        {
            ddlVendorList.Items.Clear();
            if (!string.IsNullOrEmpty(txtSccj.Value))
            {
                List<Vendor> vList = getAllVendor(txtSccj.Value);
                for (int i = 0; i < vList.Count; i++)
                {
                    ddlVendorList.Items.Add(new ListItem(vList[i].VenderName, vList[i].VendorID));
                }
            }
        }
        public List<Vendor> getAllVendor(string VendorName)
        {
            List<Vendor> vList = new List<Vendor>();
            string sql = "select vendor_id,vendor_name ,vendor_aname from Vendor where CheckFirst = 'Y'";
            if (!string.IsNullOrEmpty(VendorName))
            {
                sql += " and vendor_name like '%" + VendorName + "%'";
            }
            SelectCommandBuilder select = new SelectCommandBuilder();
            SqlDataReader reader = select.ExecuteReader(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Vendor v = new Vendor() { VendorID = reader.GetString(0), VenderName = reader.GetString(1), VenderShortName = reader.IsDBNull(2) ? "null" : reader.GetString(2) };
                    vList.Add(v);
                }
            }
            return vList;
        }
    }
    public class material
    { 
        public string sccj { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string currency { get; set; }
        public double exchangeRate { get; set; } 
    }
}
/* kl;lk;lk
lock;kl;kjjj;lock invoke kljjjdfjijjqiuwj if if (true)
	{
		if you no three no four i give you color see see
        if  ou n 3 n 4 love is back 
 *      mingyue jishi you ba jiu wen qing tian bu zhi tian shang gong que jinxi shi he nian.
 *      wo yu cheng feng gui qu ,wei kong qiong lou yu yu .gao chu bu sheng hai,qi wu nong qing ying,he shi zai ren jian.
 *      zhuan zhu ge,di qi fu , zhao wu mian. bu ying you heng ,he shi chang xiang bie shi yuan .ren you bei huan li he ,
 *      yue you ying qing yuan que,ci shi gu nan quan ,dan yuan ren chang jiu . qian li gong chang juan 
 *      ni ta ma de zai fang pi o ni niwe jjf fjiw
	}
*/