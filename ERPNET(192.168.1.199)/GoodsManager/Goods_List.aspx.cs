using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Reflection;

namespace ERPPlugIn.GoodsManager
{
    public partial class Goods_List : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //dgvList.DataSource = getGoods();
                //dgvList.DataBind();
                bindCustomers(ddlCustomers);
                bindMachineKind();
                bindDanDang();
                //Response.Write("<script>alert('" + HttpContext.Current.Request.Cookies["cookie"].Values["name"] + "')</script>");
                //Response.Write("<script>alert('" + HttpContext.Current.Request.Cookies["cookie"].Values["id"] + "')</script>");
            }
        }

        protected void dgvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvList.PageIndex = e.NewPageIndex;
            dgvList.DataSource = getGoods();
            dgvList.DataBind();
        }

        protected void dgvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "e=this.style.backgroundColor;this.style.backgroundColor='#FFFF99'";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=e";
                e.Row.Attributes["style"] = "Cursor:hand";
            }
        }

        protected void bindCustomers(DropDownList ddl)
        {
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "customer");
            s.SelectColumn("customer_id");
            s.SelectColumn("customer_aname");
            s.ConditionsColumn("1", "1");
            s.getSelectCommand();
            DataTable d = s.ExecuteDataTable();
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("请选择", "0"));
            for (int i = 0; i < d.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(d.Rows[i]["customer_aname"].ToString(), d.Rows[i]["customer_id"].ToString()));
            }
            ddl.DataTextField = "customer_aname";
        }
        protected void bindMachineKind()
        {
            string sql = "SELECT DISTINCT Aircraft  AS MachineKind FROM goods_tmp ";
            DataTable dt = new SelectCommandBuilder(constr, "").ExecuteDataTable(sql);
            ddlMachineKind.Items.Clear();
            ddlMachineKind.Items.Add("请选择");
            if (dt != null && dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlMachineKind.Items.Add(dt.Rows[i]["MachineKind"].ToString());
                }
            }
        }
        protected void bindDanDang()
        {
            string sql = "SELECT DISTINCT LTRIM(Model_Abrasives) AS MachineKind FROM goods_tmp where Model_Abrasives is not null and Model_Abrasives <> ' '";
            DataTable dt = new SelectCommandBuilder(constr, "").ExecuteDataTable(sql);
            ddlDandang.Items.Clear();
            ddlDandang.Items.Add("请选择");
            if (dt != null && dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlDandang.Items.Add(dt.Rows[i]["MachineKind"].ToString());
                }
            }
        }
        protected void getCustomer(DropDownList ddl)
        {
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "customer");
            s.SelectColumn("customer_id");
            s.SelectColumn("customer_name");
            s.ConditionsColumn("1", "1");
            s.getSelectCommand();
            DataTable d = s.ExecuteDataTable();
            for (int i = 0; i < d.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(d.Rows[i]["customer_name"].ToString(), d.Rows[i]["customer_id"].ToString()));
            }
            ddl.DataTextField = "customer_aname";
        }
        protected List<goods> getGoods()
        {
            List<goods> gList = new List<goods>();
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "goods");
            string sql = "select goods_Id, goods_name,mjh,goods_ename,dw,qs,Materail_Number, Materail_Name,ys, Materail_Model, Materail_Vender_Color,Materail_Color,cpdz,skdz, Drying_Temperature, Drying_Time,sk_scale,cxzq,khdm,remark,Fire_Retardant_Grade,Buyer,Toner_Model,Toner_Buyer,Rohs_Certification,Aircraft,Model_Abrasives,Version from goods_tmp";
            DataTable d = s.ExecuteDataTable(sql);
            if (d != null && d.Rows.Count != 0)
            {
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    goods g = new goods()
                    {
                        goodsId = d.Rows[i]["goods_Id"].ToString().Trim(),
                        goods_name = d.Rows[i]["goods_name"].ToString().Trim(),
                        mjh = d.Rows[i]["mjh"].ToString().Trim(),
                        goods_ename = d.Rows[i]["goods_ename"].ToString().Trim(),
                        Materail_Number = d.Rows[i]["Materail_Number"].ToString().Trim(),
                        Materail_Name = d.Rows[i]["Materail_Name"].ToString().Trim(),
                        Materail_Model = d.Rows[i]["Materail_Model"].ToString().Trim(),
                        ys = d.Rows[i]["ys"].ToString().Trim(),
                        Materail_Vender_Color = d.Rows[i]["Materail_Vender_Color"].ToString().Trim(),
                        Materail_Color = d.Rows[i]["Materail_Color"].ToString().Trim(),
                        cpdz = d.Rows[i]["cpdz"].ToString() == "" ? "" : Convert.ToDouble(d.Rows[i]["cpdz"]).ToString().Trim(),
                        skdz = d.Rows[i]["skdz"].ToString() == "" ? "" : Convert.ToDouble(d.Rows[i]["skdz"]).ToString().Trim(),
                        Drying_Temperature = d.Rows[i]["Drying_Temperature"].ToString().Trim(),
                        Drying_Time = d.Rows[i]["Drying_Time"].ToString().Trim(),
                        sk_scale = string.IsNullOrEmpty(d.Rows[i]["sk_scale"].ToString()) ? "" : Convert.ToDouble(d.Rows[i]["sk_scale"]).ToString("0.##%").Trim(),
                        cxzq = string.IsNullOrEmpty(d.Rows[i]["cxzq"].ToString()) ? "" : Convert.ToDouble(d.Rows[i]["cxzq"]).ToString("0.##").Trim(),
                        qs = string.IsNullOrEmpty(d.Rows[i]["qs"].ToString()) ? "" : Convert.ToDouble(d.Rows[i]["qs"]).ToString("0.##").Trim(),
                        remark = d.Rows[i]["remark"].ToString().Trim().ToString(),
                        Fire_Retardant_Grade = d.Rows[i]["Fire_Retardant_Grade"].ToString().Trim().ToString(),
                        Buyer = d.Rows[i]["Buyer"].ToString().Trim().ToString(),
                        Toner_Model = d.Rows[i]["Toner_Model"].ToString().Trim().ToString(),
                        Toner_Buyer = d.Rows[i]["Toner_Buyer"].ToString().Trim().ToString(),
                        Rohs_Certification = d.Rows[i]["Rohs_Certification"].ToString().Trim().ToString() == "有" ? "0" : "1",
                        Aircraft = d.Rows[i]["Aircraft"].ToString().Trim().ToString(),
                        Model_Abrasives = d.Rows[i]["Model_Abrasives"].ToString().Trim().ToString(),
                        dw = d.Rows[i]["dw"].ToString().Trim().ToString(),
                        khdm = d.Rows[i]["khdm"].ToString().Trim().ToString(),
                        Version = d.Rows[i]["Version"].ToString().Trim().ToString()
                    };
                    gList.Add(g);
                }
            }
            return gList;
        }
        protected List<goods> getGoods(string customerId, string goods_name, string kind, string dandang)
        {
            List<goods> gList = new List<goods>();
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "goods");
            string sql = "select goods_Id, goods_name,mjh,goods_ename,dw,qs,Materail_Number, Materail_Name,ys, Materail_Model, Materail_Vender_Color,Materail_Color,cpdz,skdz, Drying_Temperature, Drying_Time,sk_scale,cxzq,khdm = (select customer_name from customer where customer_id = khdm),remark,Fire_Retardant_Grade,Buyer,Toner_Model,Toner_Buyer,Rohs_Certification,Aircraft,Model_Abrasives,'(' + RTRIM(Version) + ') ' as Version,(case when Model_type = '0' then '量产' else '贩卖' end)Model_type,Semi_Product_Type=(select lb7_name from lb7 where lb7_id =Semi_Product_Type ),Semi_Product_Goods from goods_tmp where isnew = 'Y' ";
            if (!string.IsNullOrEmpty(goods_name))
            {
                sql += " and goods_name = '" + goods_name + "'";
            }
            if (!customerId.Equals("0"))
            {
                sql += " and khdm = '" + customerId + "'";
            }
            if (kind != "请选择")
            {
                sql += " and Aircraft = '" + kind + "'";
            }
            if (dandang != "请选择")
            {
                sql += " and Model_Abrasives = '" + dandang + "'";
            }
            sql += " order by goods_name,Version";
            DataTable d = s.ExecuteDataTable(sql);
            if (d != null && d.Rows.Count != 0)
            {
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    goods g = new goods()
                    {
                        goodsId = d.Rows[i]["goods_Id"].ToString().Trim(),
                        goods_name = d.Rows[i]["goods_name"].ToString().Trim(),
                        mjh = d.Rows[i]["mjh"].ToString().Trim(),
                        goods_ename = d.Rows[i]["goods_ename"].ToString().Trim(),
                        Materail_Number = d.Rows[i]["Materail_Number"].ToString().Trim(),
                        Materail_Name = d.Rows[i]["Materail_Name"].ToString().Trim(),
                        Materail_Model = d.Rows[i]["Materail_Model"].ToString().Trim(),
                        ys = d.Rows[i]["ys"].ToString().Trim(),
                        Materail_Vender_Color = d.Rows[i]["Materail_Vender_Color"].ToString().Trim(),
                        Materail_Color = d.Rows[i]["Materail_Color"].ToString().Trim(),
                        cpdz = d.Rows[i]["cpdz"].ToString() == "" ? "" : Convert.ToDouble(d.Rows[i]["cpdz"]).ToString().Trim(),
                        skdz = d.Rows[i]["skdz"].ToString() == "" ? "" : Convert.ToDouble(d.Rows[i]["skdz"]).ToString().Trim(),
                        Drying_Temperature = d.Rows[i]["Drying_Temperature"].ToString().Trim(),
                        Drying_Time = d.Rows[i]["Drying_Time"].ToString().Trim(),
                        sk_scale = string.IsNullOrEmpty(d.Rows[i]["sk_scale"].ToString()) ? "" : Convert.ToDouble(d.Rows[i]["sk_scale"]).ToString("0.##%").Trim(),
                        cxzq = string.IsNullOrEmpty(d.Rows[i]["cxzq"].ToString()) ? "" : Convert.ToDouble(d.Rows[i]["cxzq"]).ToString("0.##").Trim(),
                        qs = string.IsNullOrEmpty(d.Rows[i]["qs"].ToString()) ? "" : Convert.ToDouble(d.Rows[i]["qs"]).ToString("0.##").Trim(),
                        remark = d.Rows[i]["remark"].ToString().Trim().ToString(),
                        Fire_Retardant_Grade = d.Rows[i]["Fire_Retardant_Grade"].ToString().Trim().ToString(),
                        Buyer = d.Rows[i]["Buyer"].ToString().Trim().ToString(),
                        Toner_Model = d.Rows[i]["Toner_Model"].ToString().Trim().ToString(),
                        Toner_Buyer = d.Rows[i]["Toner_Buyer"].ToString().Trim().ToString(),
                        Rohs_Certification = d.Rows[i]["Rohs_Certification"].ToString().Trim().ToString(),// == "有" ? "0" : "1",
                        Aircraft = d.Rows[i]["Aircraft"].ToString().Trim().ToString(),
                        Model_Abrasives = d.Rows[i]["Model_Abrasives"].ToString().Trim().ToString(),
                        dw = d.Rows[i]["dw"].ToString().Trim().ToString(),
                        khdm = d.Rows[i]["khdm"].ToString().Trim().ToString(),
                        Version = d.Rows[i]["Version"].ToString().Trim().ToString(),
                        Model_Type = d.Rows[i]["Model_type"].ToString().Trim().ToString(),
                        Semi_Product_Goods = d.Rows[i]["Semi_Product_Goods"].ToString().Trim().ToString(),
                        Semi_Product_Type = d.Rows[i]["Semi_Product_Type"].ToString().Trim().ToString()
                    };
                    gList.Add(g);
                }
            }
            return gList;

        }
        protected DataTable ListToDataaTable(List<goods> List)
        {
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口
            Type type = typeof(goods);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列    
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in List)
            {
                DataRow row = dt.NewRow();
                //赋值
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                dt.Rows.Add(row);
            }
            return dt;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<goods> slist = getGoods(ddlCustomers.SelectedItem.Value, this.tags.Value, ddlMachineKind.SelectedItem.Text, ddlDandang.SelectedItem.Text);
            ViewState["DataTable_GridView_ReferedDataDetail"] = ListToDataaTable(slist);
            dgvList.DataSource = (DataTable)ViewState["DataTable_GridView_ReferedDataDetail"];
            dgvList.DataBind();
        }

        protected void btnAddUrl_Click(object sender, EventArgs e)
        {
            Response.Redirect("Goods_AddNew.aspx");
        }

        protected void btnInput_Click(object sender, EventArgs e)
        {
            Response.Redirect("Goods_Input.aspx");
        }

        protected void dgvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string selectValue = (dgvList.Rows[e.NewEditIndex].Cells[24].FindControl("Label22") as Label).Text;
            int rohsIndex = (dgvList.Rows[e.NewEditIndex].Cells[25].FindControl("Label23") as Label).Text == "有" ? 0 : 1;
            dgvList.EditIndex = e.NewEditIndex;
            //btnSearch_Click(sender, e as EventArgs);

            dgvList.DataSource = (DataTable)ViewState["DataTable_GridView_ReferedDataDetail"];
            dgvList.DataBind();
            DropDownList d = (dgvList.Rows[e.NewEditIndex].Cells[24].FindControl("ddlKhdm") as DropDownList);
            (dgvList.Rows[e.NewEditIndex].Cells[25].FindControl("ddlRohs") as DropDownList).SelectedIndex = rohsIndex;
            getCustomer(d);
            int selectIndex = 0;
            for (int i = 0; i < d.Items.Count; i++)
            {
                if (selectValue == d.Items[i].Text.Trim())
                {
                    selectIndex = i;
                }
            }
            d.SelectedIndex = selectIndex;
        }

        protected void dgvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgvList.EditIndex = -1;
            dgvList.DataSource = (DataTable)ViewState["DataTable_GridView_ReferedDataDetail"];
            dgvList.DataBind();
        }

        protected void dgvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = (dgvList.Rows[e.RowIndex].Cells[2].FindControl("hfId") as HiddenField).Value;
            UpdateCommandBuilder update = new UpdateCommandBuilder(constr, "goods_tmp");
            update.UpdateColumn("mjh", (dgvList.Rows[e.RowIndex].Cells[3].FindControl("TextBox1") as TextBox).Text);
            update.UpdateColumn("goods_ename", (dgvList.Rows[e.RowIndex].Cells[4].FindControl("TextBox2") as TextBox).Text);
            update.UpdateColumn("Aircraft", (dgvList.Rows[e.RowIndex].Cells[5].FindControl("TextBox3") as TextBox).Text);
            update.UpdateColumn("Materail_Number", (dgvList.Rows[e.RowIndex].Cells[6].FindControl("TextBox4") as TextBox).Text);
            update.UpdateColumn("Materail_Name", (dgvList.Rows[e.RowIndex].Cells[7].FindControl("TextBox5") as TextBox).Text);
            update.UpdateColumn("Materail_Model", (dgvList.Rows[e.RowIndex].Cells[8].FindControl("TextBox6") as TextBox).Text);
            update.UpdateColumn("ys", (dgvList.Rows[e.RowIndex].Cells[9].FindControl("TextBox7") as TextBox).Text);
            update.UpdateColumn("Materail_Vender_Color", (dgvList.Rows[e.RowIndex].Cells[10].FindControl("TextBox8") as TextBox).Text);
            update.UpdateColumn("Materail_Color", (dgvList.Rows[e.RowIndex].Cells[11].FindControl("TextBox9") as TextBox).Text);
            update.UpdateColumn("cpdz", (dgvList.Rows[e.RowIndex].Cells[12].FindControl("TextBox10") as TextBox).Text);
            update.UpdateColumn("skdz", (dgvList.Rows[e.RowIndex].Cells[13].FindControl("TextBox11") as TextBox).Text);
            update.UpdateColumn("Drying_Temperature", (dgvList.Rows[e.RowIndex].Cells[14].FindControl("TextBox12") as TextBox).Text);
            update.UpdateColumn("Drying_Time", (dgvList.Rows[e.RowIndex].Cells[15].FindControl("TextBox13") as TextBox).Text);
            string sk = (dgvList.Rows[e.RowIndex].Cells[16].FindControl("TextBox14") as TextBox).Text;
            string sk_scale = sk.Trim().IndexOf('%') != -1 ? (Convert.ToDecimal(sk.Trim().Split('%')[0]) / 100).ToString() : (Convert.ToDecimal(sk.Trim()) / 100).ToString();
            update.UpdateColumn("sk_scale", sk_scale);
            update.UpdateColumn("Fire_Retardant_Grade", (dgvList.Rows[e.RowIndex].Cells[17].FindControl("TextBox15") as TextBox).Text);
            update.UpdateColumn("Buyer", (dgvList.Rows[e.RowIndex].Cells[18].FindControl("TextBox16") as TextBox).Text);
            update.UpdateColumn("cxzq", (dgvList.Rows[e.RowIndex].Cells[19].FindControl("TextBox17") as TextBox).Text);
            update.UpdateColumn("Toner_Model", (dgvList.Rows[e.RowIndex].Cells[20].FindControl("TextBox18") as TextBox).Text);
            update.UpdateColumn("Toner_Buyer", (dgvList.Rows[e.RowIndex].Cells[21].FindControl("TextBox19") as TextBox).Text);
            update.UpdateColumn("qs", (dgvList.Rows[e.RowIndex].Cells[22].FindControl("TextBox20") as TextBox).Text);
            update.UpdateColumn("dw", (dgvList.Rows[e.RowIndex].Cells[23].FindControl("TextBox21") as TextBox).Text);
            update.UpdateColumn("khdm", (dgvList.Rows[e.RowIndex].Cells[24].FindControl("ddlKhdm") as DropDownList).SelectedItem.Value);
            update.UpdateColumn("Rohs_Certification", (dgvList.Rows[e.RowIndex].Cells[25].FindControl("ddlRohs") as DropDownList).SelectedItem.Text);
            update.UpdateColumn("Model_Abrasives", (dgvList.Rows[e.RowIndex].Cells[26].FindControl("TextBox24") as TextBox).Text);
            update.UpdateColumn("remark", (dgvList.Rows[e.RowIndex].Cells[27].FindControl("TextBox25") as TextBox).Text);
            update.ConditionsColumn("goods_id", id);
            update.getUpdateCommand();
            int i = update.ExecuteNonQuery();
            if (i != 0)
            {
                dgvList.EditIndex = -1;
                Response.Write("<script>alert('更新成功')</script>");
                btnSearch_Click(sender, e);
            }
        }

    }
    public class goods
    {
        public string goodsId { get; set; }
        public string goods_name { get; set; }
        public string mjh { get; set; }
        public string goods_ename { get; set; }
        public string dw { get; set; }
        public string qs { get; set; }
        public string Materail_Number { get; set; }
        public string Materail_Name { get; set; }
        public string ys { get; set; }
        public string Materail_Model { get; set; }
        public string Materail_Vender_Color { get; set; }
        public string Materail_Color { get; set; }
        public string cpdz { get; set; }
        public string skdz { get; set; }
        public string Drying_Temperature { get; set; }
        public string Drying_Time { get; set; }
        public string sk_scale { get; set; }
        public string cxzq { get; set; }
        public string khdm { get; set; }
        public string remark { get; set; }
        public string Fire_Retardant_Grade { get; set; }
        public string Buyer { get; set; }
        public string Toner_Model { get; set; }
        public string Toner_Buyer { get; set; }
        public string Rohs_Certification { get; set; }
        public string Aircraft { get; set; }
        public string Model_Abrasives { get; set; }
        public string Model_Type { get; set; }
        public string Version { get; set; }
        public string Semi_Product_Type { get; set; }
        public string Semi_Product_Goods { get; set; }
    }
}