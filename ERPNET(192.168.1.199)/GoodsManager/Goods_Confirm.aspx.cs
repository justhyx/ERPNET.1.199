using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERPPlugIn.GoodsManager;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Configuration;
using System.Reflection;
using ERPPlugIn.Class;

namespace ERPPlugIn.GoodsManager
{
    public partial class Goods_Confirm : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        public static List<string> sList = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindCustomers(ddlCustomers);
                //dgvList.DataSource = getConfirmGoodsList();
                //dgvList.DataBind();
                ViewState["type"] = Request.QueryString["type"];
                ddldept.SelectedIndex = Convert.ToInt32(ViewState["type"]);
            }
        }
        protected List<goods> getConfirmGoodsList()
        {
            List<goods> gList = new List<goods>();
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "goods_tmp");
            string sql = "select goods_Id, (RTRIM(goods_name) + isnull(Version,'')) as goods_name,mjh,goods_ename,dw,qs,Materail_Number, Materail_Name,ys, Materail_Model, Materail_Vender_Color,Materail_Color,cpdz,skdz, Drying_Temperature, Drying_Time,sk_scale,cxzq,khdm = (select customer_name from customer where customer_id = khdm),remark,Fire_Retardant_Grade,Buyer,Toner_Model,Toner_Buyer,Rohs_Certification,Aircraft,Model_Abrasives from goods_tran where isConfirm = 'Approved'";
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
                        khdm = d.Rows[i]["khdm"].ToString().Trim().ToString()
                    };
                    gList.Add(g);
                }
            }
            return gList;

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            List<goods> List = new List<goods>();
            List<string> sqlList = new List<string>();
            string constr = "";
            if (ddldept.SelectedItem.Value == "0")
            {
                constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            else
            {
                constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
            }
            if (dgvList.Rows.Count != 0)
            {
                for (int i = 0; i < dgvList.Rows.Count; i++)
                {
                    if ((dgvList.Rows[i].Cells[0].FindControl("cboCheckItem") as CheckBox).Checked == true)
                    {
                        goods g = new goods()
                        {
                            goodsId = (dgvList.Rows[i].Cells[0].FindControl("HfId") as HiddenField).Value,
                            goods_name = (dgvList.Rows[i].Cells[2].FindControl("Label0") as Label).Text,
                            mjh = (dgvList.Rows[i].Cells[3].FindControl("Label1") as Label).Text,
                            goods_ename = (dgvList.Rows[i].Cells[4].FindControl("Label2") as Label).Text,
                            Aircraft = (dgvList.Rows[i].Cells[5].FindControl("Label3") as Label).Text,
                            Materail_Number = (dgvList.Rows[i].Cells[6].FindControl("Label4") as Label).Text,
                            Materail_Name = (dgvList.Rows[i].Cells[7].FindControl("Label5") as Label).Text,
                            Materail_Model = (dgvList.Rows[i].Cells[8].FindControl("Label6") as Label).Text,
                            ys = (dgvList.Rows[i].Cells[9].FindControl("Label7") as Label).Text,
                            Materail_Vender_Color = (dgvList.Rows[i].Cells[10].FindControl("Label8") as Label).Text,
                            Materail_Color = (dgvList.Rows[i].Cells[11].FindControl("Label9") as Label).Text,
                            cpdz = (dgvList.Rows[i].Cells[12].FindControl("Label10") as Label).Text,
                            skdz = (dgvList.Rows[i].Cells[13].FindControl("Label11") as Label).Text,
                            Drying_Temperature = (dgvList.Rows[i].Cells[14].FindControl("Label12") as Label).Text,
                            Drying_Time = (dgvList.Rows[i].Cells[15].FindControl("Label13") as Label).Text,
                            sk_scale = (dgvList.Rows[i].Cells[16].FindControl("Label14") as Label).Text,
                            Fire_Retardant_Grade = (dgvList.Rows[i].Cells[17].FindControl("Label15") as Label).Text,
                            Buyer = (dgvList.Rows[i].Cells[18].FindControl("Label16") as Label).Text,
                            cxzq = (dgvList.Rows[i].Cells[19].FindControl("Label17") as Label).Text,
                            Toner_Model = (dgvList.Rows[i].Cells[20].FindControl("Label18") as Label).Text,
                            Toner_Buyer = (dgvList.Rows[i].Cells[21].FindControl("Label19") as Label).Text,
                            qs = (dgvList.Rows[i].Cells[22].FindControl("Label20") as Label).Text,
                            dw = (dgvList.Rows[i].Cells[23].FindControl("Label21") as Label).Text,
                            khdm = (dgvList.Rows[i].Cells[24].FindControl("Label22") as Label).Text,
                            Rohs_Certification = (dgvList.Rows[i].Cells[25].FindControl("Label23") as Label).Text,
                            Model_Abrasives = (dgvList.Rows[i].Cells[26].FindControl("Label24") as Label).Text,
                            remark = (dgvList.Rows[i].Cells[27].FindControl("Label25") as Label).Text
                        };
                        List.Add(g);
                    }
                }
            }
            if (List.Count == 0)
            {
                Response.Write("<script>alert('没有选择任何行')</script>");
                return;
            }
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "");
            string sql = "SELECT MAX(goods_id) FROM goods";
            long id = Convert.ToInt64(s.ExecuteScalar(sql).ToString().Trim());
            string maxId = new SelectCommandBuilder(constr, "").ExecuteScalar("SELECT MAX(goods_ration_id) FROM goods_ration").ToString();
            int rationSId = int.Parse(CommadMethod.getNextId("", ""));
            for (int i = 0; i < List.Count; i++)
            {
                if (sList.Count == 0)
                {
                    id += 1;
                    InsertCommandBuilder ins = new InsertCommandBuilder(constr, "goods");
                    ins.InsertColumn("goods_id", id.ToString());
                    ins.InsertColumn("goods_name", List[i].goods_name);
                    ins.InsertColumn("goods_ename", List[i].goods_ename);
                    ins.InsertColumn("mjh", List[i].mjh);
                    ins.InsertColumn("dw", List[i].dw);
                    ins.InsertColumn("qs", List[i].qs);
                    ins.InsertColumn("Materail_Number", List[i].Materail_Number);
                    ins.InsertColumn("Materail_Name", List[i].Materail_Name);
                    ins.InsertColumn("ys", List[i].ys);
                    ins.InsertColumn("Materail_Model", List[i].Materail_Model);
                    ins.InsertColumn("Materail_Vender_Color", List[i].Materail_Vender_Color);
                    ins.InsertColumn("Materail_Color", List[i].Materail_Color);
                    ins.InsertColumn("cpdz", List[i].cpdz);
                    ins.InsertColumn("skdz", List[i].skdz);
                    ins.InsertColumn("Drying_Temperature", List[i].Drying_Temperature);
                    ins.InsertColumn("Drying_Time", List[i].Drying_Time);
                    string sk = List[i].sk_scale.Trim().IndexOf('%') != -1 ? (Convert.ToDecimal(List[i].sk_scale.Trim().Split('%')[0]) / 100).ToString() : (Convert.ToDecimal(List[i].sk_scale.Trim()) / 100).ToString();
                    ins.InsertColumn("sk_scale", sk);
                    ins.InsertColumn("cxzq", List[i].cxzq);
                    ins.InsertColumn("khdm", List[i].khdm);
                    ins.InsertColumn("remark", List[i].remark);
                    ins.InsertColumn("Fire_Retardant_Grade", List[i].Fire_Retardant_Grade);
                    ins.InsertColumn("Buyer", List[i].Buyer);
                    ins.InsertColumn("Toner_Model", List[i].Toner_Model);
                    ins.InsertColumn("Toner_Buyer", List[i].Toner_Buyer);
                    ins.InsertColumn("Aircraft", List[i].Aircraft);
                    ins.InsertColumn("Rohs_Certification", List[i].Rohs_Certification);
                    decimal qty = (Convert.ToDecimal(List[i].cpdz) + (Convert.ToDecimal(List[i].skdz) / Convert.ToDecimal(List[i].qs))) * (1 - Convert.ToDecimal(sk));
                    decimal skqty = (Convert.ToDecimal(List[i].cpdz) + (Convert.ToDecimal(List[i].skdz) / Convert.ToDecimal(List[i].qs))) * Convert.ToDecimal(sk);
                    List<decimal> dlist = new List<decimal>();
                    dlist.Add(qty);
                    dlist.Add(skqty);
                    for (int k = 0; k < dlist.Count; k++)
                    {
                        InsertCommandBuilder inss = new InsertCommandBuilder(constr, "goods_ration");
                        inss.InsertColumn("goods_ration_id", "AG" + rationSId.ToString().PadLeft(8, '0') + "0101");
                        inss.InsertColumn("goods_id", id);
                        inss.InsertColumn("item_Data", "");
                        inss.InsertColumn("item_type", "01");
                        inss.InsertColumn("qty", dlist[k].ToString("0.00#"));
                        inss.InsertColumn("price", "0");
                        inss.InsertColumn("remark", "");
                        inss.InsertColumn("operator_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        sqlList.Add(inss.getInsertCommand());
                    }
                    CommadMethod.getNextId("", "", dlist.Count);
                    sqlList.Add(ins.getInsertCommand());
                }
                else
                {
                    for (int j = 0; j < sList.Count; j++)
                    {
                        string name = List[i].goods_name.Trim() + sList[j];
                        id += 1;
                        InsertCommandBuilder ins = new InsertCommandBuilder(constr, "goods");
                        ins.InsertColumn("goods_id", id.ToString());
                        ins.InsertColumn("goods_name", name);
                        ins.InsertColumn("goods_ename", List[i].goods_ename);
                        ins.InsertColumn("mjh", List[i].mjh);
                        ins.InsertColumn("dw", List[i].dw);
                        ins.InsertColumn("qs", List[i].qs);
                        ins.InsertColumn("Materail_Number", List[i].Materail_Number);
                        ins.InsertColumn("Materail_Name", List[i].Materail_Name);
                        ins.InsertColumn("ys", List[i].ys);
                        ins.InsertColumn("Materail_Model", List[i].Materail_Model);
                        ins.InsertColumn("Materail_Vender_Color", List[i].Materail_Vender_Color);
                        ins.InsertColumn("Materail_Color", List[i].Materail_Color);
                        ins.InsertColumn("cpdz", List[i].cpdz);
                        ins.InsertColumn("skdz", List[i].skdz);
                        ins.InsertColumn("Drying_Temperature", List[i].Drying_Temperature);
                        ins.InsertColumn("Drying_Time", List[i].Drying_Time);
                        string sk = List[i].sk_scale.Trim().IndexOf('%') != -1 ? (Convert.ToDecimal(List[i].sk_scale.Trim().Split('%')[0]) / 100).ToString() : (Convert.ToDecimal(List[i].sk_scale.Trim()) / 100).ToString();
                        ins.InsertColumn("sk_scale", sk);
                        ins.InsertColumn("cxzq", List[i].cxzq);
                        ins.InsertColumn("khdm", List[i].khdm);
                        ins.InsertColumn("remark", List[i].remark);
                        ins.InsertColumn("Fire_Retardant_Grade", List[i].Fire_Retardant_Grade);
                        ins.InsertColumn("Buyer", List[i].Buyer);
                        ins.InsertColumn("Toner_Model", List[i].Toner_Model);
                        ins.InsertColumn("Toner_Buyer", List[i].Toner_Buyer);
                        ins.InsertColumn("Aircraft", List[i].Aircraft);
                        ins.InsertColumn("Rohs_Certification", List[i].Rohs_Certification);
                        decimal qty = (Convert.ToDecimal(List[i].cpdz) + (Convert.ToDecimal(List[i].skdz) / Convert.ToDecimal(List[i].qs))) * (1 - Convert.ToDecimal(sk));
                        decimal skqty = (Convert.ToDecimal(List[i].cpdz) + (Convert.ToDecimal(List[i].skdz) / Convert.ToDecimal(List[i].qs))) * Convert.ToDecimal(sk);
                        List<decimal> dlist = new List<decimal>();
                        dlist.Add(qty);
                        dlist.Add(skqty);
                        for (int k = 0; k < dlist.Count; k++)
                        {
                            rationSId = int.Parse(CommadMethod.getNextId("", ""));
                            InsertCommandBuilder inss = new InsertCommandBuilder(constr, "goods_ration");
                            inss.InsertColumn("goods_ration_id", "AG" + rationSId.ToString().PadLeft(8, '0') + "0101");
                            inss.InsertColumn("goods_id", id);
                            inss.InsertColumn("item_Data", "");
                            inss.InsertColumn("item_type", "01");
                            inss.InsertColumn("qty", dlist[k].ToString("0.00#"));
                            inss.InsertColumn("price", "0");
                            inss.InsertColumn("remark", "");
                            inss.InsertColumn("operator_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            sqlList.Add(inss.getInsertCommand());
                        }
                        //CommadMethod.getNextId("", "", dlist.Count);
                        sqlList.Add(ins.getInsertCommand());
                    }
                }
            }
            InsertCommandBuilder insert = new InsertCommandBuilder(constr, "");
            for (int i = 0; i < List.Count; i++)
            {
                new UpdateCommandBuilder(constr, "").ExecuteNonQuery("update goods_tran set isConfirm = 'Done' where goods_id = '" + List[i].goodsId + "'");
            }
            int count = insert.ExcutTransaction(sqlList);
            Response.Write("<script>alert('执行成功')</script>");
            btnSearch_Click(sender, e);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtGoArea.Text.Equals(string.Empty))
            {
                Response.Write("<script>alert('去向地为空')</script>");
                txtGoArea.Focus();
                return;
            }
            if (sList.Contains(txtGoArea.Text.ToUpper()))
            {
                Response.Write("<script>alert('去向地重复')</script>");
                txtGoArea.Text = string.Empty;
                txtGoArea.Focus();
                return;
            }
            sList.Add(txtGoArea.Text.Trim().ToUpper());
            txtGoArea.Text = string.Empty;
            txtGoArea.Focus();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            sList.Clear();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<goods> slist = getGoods(ddlCustomers.SelectedItem.Value, this.tags.Value, ViewState["type"].ToString());
            ViewState["DataTable_GridView_ReferedDataDetail"] = ListToDataaTable(slist);
            dgvList.DataSource = (DataTable)ViewState["DataTable_GridView_ReferedDataDetail"];
            dgvList.DataBind();
        }
        protected List<goods> getGoods(string customerId, string goods_name, string type)
        {
            List<goods> gList = new List<goods>();
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "goods");
            string sql = "select goods_Id, (RTRIM(goods_name) + isnull(Version,'')) as goods_name,mjh,goods_ename,dw,qs,Materail_Number, Materail_Name,ys, Materail_Model, Materail_Vender_Color,Materail_Color,cpdz,skdz, Drying_Temperature, Drying_Time,sk_scale,cxzq,khdm = (select customer_name from customer where customer_id = khdm),remark,Fire_Retardant_Grade,Buyer,Toner_Model,Toner_Buyer,Rohs_Certification,Aircraft,Model_Abrasives, RTRIM(Version) as Version ,Model_type from goods_tran where isConfirm = 'Approved'";
            if (!string.IsNullOrEmpty(goods_name))
            {
                sql += " and goods_name like '%" + goods_name + "%'";
            }
            if (!customerId.Equals("0"))
            {
                sql += " and khdm = '" + customerId + "'";
            }
            if (type != "")
            {
                sql += " and Model_Type = '" + type + "'";
            }
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
                        Model_Type = d.Rows[i]["Model_type"].ToString().Trim().ToString()
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
    }
}