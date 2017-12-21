using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Configuration;
using ERPPlugIn.Class;

namespace ERPPlugIn.GoodsManager
{
    public partial class Goods_AddNew : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindCustomers();
                bindEquip();
                getlb7();
                txtgoodsName.Focus();
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtgoodsName.Text.Trim()))
                {
                    Response.Write("<script>alert('请输入部番!')</script>");
                    return;
                }
                SelectCommandBuilder s = new SelectCommandBuilder(constr, "goods_tmp");
                s.SelectColumn("Count(goods_name)");
                s.ConditionsColumn("goods_name", txtgoodsName.Text.Trim());
                if (txtVersion.Text != string.Empty)
                {
                    s.ConditionsColumn("Version", txtVersion.Text.Trim());
                }
                s.getSelectCommand();
                int i = Convert.ToInt32(s.ExecuteScalar());
                int count = 0;
                if (i == 0)
                {
                    count = InsertData();
                }
                else
                {
                    count = UpdateData();
                }
                if (count != 0)
                {
                    ClearTextBox();
                    btnAdd.Enabled = false;
                }
                else
                {
                    Response.Write("<script>alert('添加失败!')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "!')</script>");
            }

        }
        protected void bindCustomers()
        {
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "customer");
            s.SelectColumn("customer_id");
            s.SelectColumn("customer_aname");
            s.ConditionsColumn("1", "1");
            s.getSelectCommand();
            DataTable d = s.ExecuteDataTable();
            ddlCustomer.Items.Clear();
            ddlCustomer.Items.Add(new ListItem("请选择", "0"));
            for (int i = 0; i < d.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(d.Rows[i]["customer_aname"].ToString().Trim()))
                {
                    ddlCustomer.Items.Add(new ListItem(d.Rows[i]["customer_aname"].ToString(), d.Rows[i]["customer_id"].ToString()));
                }
            }
            ddlCustomer.DataTextField = "customer_aname";
        }
        protected void bindEquip()
        {
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "Equip");
            s.SelectColumn("Equip_no");
            s.SelectColumn("Equip_name");
            s.ConditionsColumn("1", "1");
            s.getSelectCommand();
            DataTable d = s.ExecuteDataTable();
            ddlMachine.Items.Clear();
            ddlMachine.Items.Add(new ListItem("请选择", "0"));
            for (int i = 0; i < d.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(d.Rows[i]["Equip_name"].ToString().Trim()))
                {
                    ddlMachine.Items.Add(new ListItem(d.Rows[i]["Equip_name"].ToString(), d.Rows[i]["Equip_no"].ToString()));
                }
            }
            ddlMachine.DataTextField = "Equip_name";
        }

        protected int InsertData()
        {
            string id = CommadMethod.getNextId("GD" + DateTime.Now.ToString("yyyyMMdd"), "");
            InsertCommandBuilder ins = new InsertCommandBuilder(constr, "goods_tmp");
            ins.InsertColumn("goods_id", id.ToString());
            ins.InsertColumn("goods_name", txtgoodsName.Text.Trim().ToUpper());
            ins.InsertColumn("goods_no", txtgoodsName.Text.Trim().ToUpper());
            if (!string.IsNullOrEmpty(txtgoodsEName.Text.Trim()))
            {
                ins.InsertColumn("goods_ename", txtgoodsEName.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtVersion.Text.Trim()))
            {
                ins.InsertColumn("Version", txtVersion.Text.Trim().ToUpper());
            }
            if (!string.IsNullOrEmpty(txtjigNo.Text.Trim()))
            {
                ins.InsertColumn("mjh", txtjigNo.Text.Trim());
            }
            if (!ddlMachine.SelectedItem.Value.Equals("0"))
            {
                ins.InsertColumn("dw", ddlMachine.SelectedItem.Value);
            }
            if (!string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                ins.InsertColumn("qs", txtQty.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtMNo.Text.Trim()))
            {
                ins.InsertColumn("Materail_Number", txtMNo.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtMName.Text.Trim()))
            {
                ins.InsertColumn("Materail_Name", txtMName.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtMColor.Text.Trim()))
            {
                ins.InsertColumn("ys", txtMColor.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtMModel.Text.Trim()))
            {
                ins.InsertColumn("Materail_Model", txtMModel.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtMVenderColor.Text.Trim()))
            {
                ins.InsertColumn("Materail_Vender_Color", txtMVenderColor.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtColor.Text.Trim()))
            {
                ins.InsertColumn("Materail_Color", txtColor.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtWeight.Text.Trim()))
            {
                ins.InsertColumn("cpdz", txtWeight.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtSKWeight.Text.Trim()))
            {
                ins.InsertColumn("skdz", txtSKWeight.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtTemp.Text.Trim()))
            {
                ins.InsertColumn("Drying_Temperature", txtTemp.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtTime.Text.Trim()))
            {
                ins.InsertColumn("Drying_Time", txtTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtCrushedscale.Text.Trim()))
            {
                ins.InsertColumn("sk_scale", txtCrushedscale.Text.Trim().IndexOf('%') != -1 ? (Convert.ToDecimal(txtCrushedscale.Text.Trim().Split('%')[0]) / 100).ToString() : (Convert.ToDecimal(txtCrushedscale.Text.Trim()) / 100).ToString());
            }
            if (!string.IsNullOrEmpty(txtCycle.Text.Trim()))
            {
                ins.InsertColumn("cxzq", txtCycle.Text.Trim());
            }
            if (!ddlCustomer.SelectedItem.Value.Equals("0"))
            {
                ins.InsertColumn("khdm", ddlCustomer.SelectedItem.Value);
            }
            if (!string.IsNullOrEmpty(txtRemark.Text.Trim()))
            {
                ins.InsertColumn("remark", txtRemark.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtLevel.Text.Trim()))
            {
                ins.InsertColumn("Fire_Retardant_Grade", txtLevel.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBuyer.Text.Trim()))
            {
                ins.InsertColumn("Buyer", txtBuyer.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtCModel.Text.Trim()))
            {
                ins.InsertColumn("Toner_Model", txtCModel.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtCBuyer.Text.Trim()))
            {
                ins.InsertColumn("Toner_Buyer", txtCBuyer.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtAircraft.Text.Trim()))
            {
                ins.InsertColumn("Aircraft", txtAircraft.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtCharge.Text.Trim()))
            {
                ins.InsertColumn("Model_Abrasives", txtCharge.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtlb7.Text))
            {
                ins.InsertColumn("Semi_Product_Goods", txtlb7.Text.Trim());
            }
            if (ddlLb7.SelectedItem.Value != "00")
            {
                ins.InsertColumn("Semi_Product_Type", ddlLb7.SelectedItem.Value);
            }
            ins.InsertColumn("crt_date", "getdate()");
            ins.InsertColumn("Model_Type", ddlModeType.SelectedItem.Value);
            ins.InsertColumn("Rohs_Certification", ddlROHS.SelectedItem.Text);
            ins.getInsertCommand();
            return ins.ExecuteNonQuery();
        }

        protected int UpdateData()
        {
            UpdateCommandBuilder u = new UpdateCommandBuilder(constr, "goods_tmp");
            u.UpdateColumn("goods_ename", txtgoodsEName.Text.Trim());
            u.UpdateColumn("Version", txtVersion.Text.Trim().ToUpper());
            u.UpdateColumn("mjh", txtjigNo.Text.Trim());
            u.UpdateColumn("dw", ddlMachine.SelectedItem.Value);
            u.UpdateColumn("qs", txtQty.Text.Trim());
            u.UpdateColumn("Materail_Number", txtMNo.Text.Trim());
            u.UpdateColumn("Materail_Name", txtMName.Text.Trim());
            u.UpdateColumn("ys", txtMColor.Text.Trim());
            u.UpdateColumn("Materail_Model", txtMModel.Text.Trim());
            u.UpdateColumn("Materail_Vender_Color", txtMVenderColor.Text.Trim());
            u.UpdateColumn("Materail_Color", txtColor.Text.Trim());
            u.UpdateColumn("cpdz", txtWeight.Text.Trim());
            u.UpdateColumn("skdz", txtSKWeight.Text.Trim());
            u.UpdateColumn("Drying_Temperature", txtTemp.Text.Trim());
            u.UpdateColumn("Drying_Time", txtTime.Text.Trim());
            u.UpdateColumn("sk_scale", txtCrushedscale.Text.Trim());
            u.UpdateColumn("cxzq", txtCycle.Text.Trim());
            u.UpdateColumn("khdm", ddlCustomer.SelectedItem.Value);
            u.UpdateColumn("remark", txtRemark.Text.Trim());
            u.UpdateColumn("Fire_Retardant_Grade", txtLevel.Text.Trim());
            u.UpdateColumn("Buyer", txtBuyer.Text.Trim());
            u.UpdateColumn("Toner_Model", txtCModel.Text.Trim());
            u.UpdateColumn("Toner_Buyer", txtCBuyer.Text.Trim());
            u.UpdateColumn("Aircraft", txtAircraft.Text.Trim());
            u.UpdateColumn("Rohs_Certification", ddlROHS.SelectedItem.Text);
            u.UpdateColumn("Model_Abrasives", txtCharge.Text.Trim());
            u.UpdateColumn("Model_Type", ddlModeType.SelectedItem.Value);
            u.UpdateColumn("modi_date", "getdate()");
            if (!string.IsNullOrEmpty(txtlb7.Text))
            {
                u.UpdateColumn("Semi_Product_Goods", txtlb7.Text.Trim());
            }
            if (ddlLb7.SelectedItem.Value != "00")
            {
                u.UpdateColumn("Semi_Product_Type", ddlLb7.SelectedItem.Value);
            }
            u.ConditionsColumn("goods_name", txtgoodsName.Text.Trim() + txtVersion.Text.Trim());
            u.getUpdateCommand();
            return u.ExecuteNonQuery();
        }

        protected void ClearTextBox()
        {
            foreach (Control cl in this.Page.FindControl("Form1").Controls)
            {
                if (cl.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                {
                    ((TextBox)cl).Text = "";
                }
            }
            ddlCustomer.SelectedIndex = 0;
            ddlMachine.SelectedIndex = 0;
            ddlROHS.SelectedIndex = 0;
            txtgoodsName.Focus();
        }

        protected void txtMNo_TextChanged(object sender, EventArgs e)
        {
            SelectCommandBuilder s = new SelectCommandBuilder("materials");
            s.SelectColumn("texture");
            s.SelectColumn("spec");
            s.SelectColumn("color");
            s.ConditionsColumn("name", txtMNo.Text.Trim().ToUpper());
            s.getSelectCommand();
            DataTable td = s.ExecuteDataTable();
            if (td != null && td.Rows.Count != 0)
            {
                txtMName.Text = td.Rows[0]["texture"].ToString();
                txtMModel.Text = td.Rows[0]["spec"].ToString();
                txtMVenderColor.Text = td.Rows[0]["color"].ToString();
                txtWeight.Focus();
                btnAdd.Enabled = true;
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('材料未建档'); history.back();</script>", false);
                txtMName.Focus();
            }
        }
        protected void getlb7()
        {
            string sql = "SELECT lb7_id, lb7_name FROM lb7";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            ddlLb7.Items.Clear();
            ddlLb7.Items.Add(new ListItem("请选择", "00"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlLb7.Items.Add(new ListItem(dt.Rows[i]["lb7_name"].ToString(), dt.Rows[i]["lb7_id"].ToString()));
            }
        }
    }
}