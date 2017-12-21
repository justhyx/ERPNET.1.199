using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Configuration;
using System.Drawing;
using ERPPlugIn.Class;

namespace ERPPlugIn.GoodsManager
{
    public partial class Goods_Modify : System.Web.UI.Page
    {
        string smtpIp = ConfigurationManager.AppSettings["smtpIP"].ToString();
        Decimal Port = Convert.ToDecimal(ConfigurationManager.AppSettings["smtpPort"].ToString());
        string fromUser = ConfigurationManager.AppSettings["fromUser"].ToString();
        string fromPwd = ConfigurationManager.AppSettings["fromPassword"].ToString();
        string mailList = ConfigurationManager.AppSettings["mailList"].ToString();
        string ccList = ConfigurationManager.AppSettings["ccList"].ToString();
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        public string Id { get { return ViewState["Id"].ToString(); } set { ViewState["Id"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnRun.Attributes["onclick"] = this.GetPostBackEventReference(this.btnRun) + ";this.disabled=true;";
                btnAdd.Enabled = false;
                string goodsId = Request.QueryString["goodsId"];
                ViewState["id"] = goodsId;
                bindEquip();
                getlb7();
                bindCustomers();
                List<goods> drt = getConfirmGoodsList(goodsId);
                if (drt.Count != 0)
                {
                    dgvList.DataSource = drt;
                    dgvList.DataBind();
                }
                else
                {
                    fieldgoods.InnerHtml = "<legend>部品变更履历</legend><h3>无变更履历</h3>";
                }
                DataTable d = getGoodsDetail(goodsId);
                if (d != null && d.Rows.Count != 0)
                {
                    for (int i = 0; i < d.Rows.Count; i++)
                    {
                        hifId.Value = d.Rows[i]["goods_id"].ToString().Trim();
                        Id = d.Rows[i]["FileName"].ToString().Trim();
                        txtgoodsName.Text = d.Rows[i]["goods_name"].ToString().Trim();
                        txtVersion.Text = d.Rows[i]["version"].ToString().Trim();
                        ViewState["name"] = txtgoodsName.Text.Trim();
                        txtjigNo.Text = d.Rows[i]["mjh"].ToString().Trim();
                        txtgoodsEName.Text = d.Rows[i]["goods_ename"].ToString().Trim();
                        txtMNo.Text = d.Rows[i]["Materail_Number"].ToString().Trim();
                        txtMName.Text = d.Rows[i]["Materail_Name"].ToString().Trim();
                        txtMModel.Text = d.Rows[i]["Materail_Model"].ToString().Trim();
                        txtMColor.Text = d.Rows[i]["ys"].ToString().Trim();
                        txtMVenderColor.Text = d.Rows[i]["Materail_Vender_Color"].ToString().Trim();
                        txtColor.Text = d.Rows[i]["Materail_Color"].ToString().Trim();
                        txtWeight.Text = d.Rows[i]["cpdz"].ToString() == "" ? "" : Convert.ToDouble(d.Rows[i]["cpdz"]).ToString().Trim();
                        txtSKWeight.Text = d.Rows[i]["skdz"].ToString() == "" ? "" : Convert.ToDouble(d.Rows[i]["skdz"]).ToString().Trim();
                        txtTemp.Text = d.Rows[i]["Drying_Temperature"].ToString().Trim();
                        txtTime.Text = d.Rows[i]["Drying_Time"].ToString().Trim();
                        txtCrushedscale.Text = string.IsNullOrEmpty(d.Rows[i]["sk_scale"].ToString()) ? "" : Convert.ToDouble(d.Rows[i]["sk_scale"]).ToString("0.##%").Trim();
                        txtCycle.Text = string.IsNullOrEmpty(d.Rows[i]["cxzq"].ToString()) ? "" : Convert.ToDouble(d.Rows[i]["cxzq"]).ToString("0.##").Trim();
                        txtQty.Text = string.IsNullOrEmpty(d.Rows[i]["qs"].ToString()) ? "" : Convert.ToDouble(d.Rows[i]["qs"]).ToString("0.##").Trim();
                        txtRemark.Text = d.Rows[i]["remark"].ToString().Trim().ToString();
                        txtLevel.Text = d.Rows[i]["Fire_Retardant_Grade"].ToString().Trim().ToString();
                        txtBuyer.Text = d.Rows[i]["Buyer"].ToString().Trim().ToString();
                        txtCModel.Text = d.Rows[i]["Toner_Model"].ToString().Trim().ToString();
                        txtCBuyer.Text = d.Rows[i]["Toner_Buyer"].ToString().Trim().ToString();
                        txtNo.Text = d.Rows[i]["TNumber"].ToString().Trim().ToString();
                        txtContent.Text = d.Rows[i]["ChangeContent"].ToString().Trim().ToString();
                        ddlROHS.SelectedIndex = d.Rows[i]["Rohs_Certification"].ToString().Trim().ToString() == "有" ? 0 : 1;
                        txtAircraft.Text = d.Rows[i]["Aircraft"].ToString().Trim().ToString();
                        ddlModeType.SelectedIndex = Convert.ToInt32(d.Rows[i]["Model_type"]);
                        txtlb7.Text = d.Rows[i]["Semi_Product_Goods"].ToString().Trim().ToString();
                        for (int j = 0; j < ddlCustomer.Items.Count; j++)
                        {
                            if (ddlCustomer.Items[j].Value.Trim() == d.Rows[i]["khdm"].ToString().Trim())
                            {
                                ddlCustomer.SelectedIndex = j;
                                break;
                            }
                        }
                        for (int j = 0; j < ddlMachine.Items.Count; j++)
                        {
                            if (ddlMachine.Items[j].Value.Trim() == d.Rows[i]["dw"].ToString().Trim())
                            {
                                ddlMachine.SelectedIndex = j;
                                break;
                            }
                        }
                        for (int j = 0; j < ddlLb7.Items.Count; j++)
                        {
                            if (d.Rows[i]["Semi_Product_Type"].ToString().Trim() == ddlLb7.Items[j].Value.Trim())
                            {
                                ddlLb7.SelectedIndex = j;
                                break;
                            }
                        }
                    }
                }
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
                    ddlCustomer.Items.Add(new ListItem(d.Rows[i]["customer_aname"].ToString().Trim(), d.Rows[i]["customer_id"].ToString().Trim()));
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
                    ddlMachine.Items.Add(new ListItem(d.Rows[i]["Equip_name"].ToString().Trim(), d.Rows[i]["Equip_no"].ToString().Trim()));
                }
            }
            ddlMachine.DataTextField = "Equip_name";
        }
        protected DataTable getGoodsDetail(string goodsId)
        {
            string sql = "select goods_id, goods_name,version,mjh,goods_ename,dw,qs,Materail_Number, Materail_Name,ys, Materail_Model, Materail_Vender_Color,Materail_Color,cpdz,skdz, Drying_Temperature, Drying_Time,sk_scale,cxzq,khdm,remark,Fire_Retardant_Grade,Buyer,Toner_Model,Toner_Buyer,Rohs_Certification,Aircraft,Model_type, Semi_Product_Type,Semi_Product_Goods,Semi_Product_Type,TNumber,ChangeContent,FileName from goods_tmp where goods_id='" + goodsId + "'";
            return new SelectCommandBuilder(constr, "").ExecuteDataTable(sql);
        }
        protected string getUpdateDataSQL(string tableName, string Id)
        {
            UpdateCommandBuilder u = new UpdateCommandBuilder(constr, tableName);
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
            if (!string.IsNullOrEmpty(txtCrushedscale.Text.Trim()))
            {
                u.UpdateColumn("sk_scale", Convert.ToDecimal(txtCrushedscale.Text.Trim().Split('%')[0]) / 100);
            }
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
            u.UpdateColumn("Semi_Product_Goods", txtlb7.Text.Trim());
            u.UpdateColumn("Semi_Product_Type", ddlLb7.SelectedItem.Value);
            u.UpdateColumn("modi_date", "getdate()");
            u.ConditionsColumn("goods_id", Id);
            return u.getUpdateCommand();
        }
        protected string getUpdateDataSQL(string tableName, string Goods_name, string Verson)
        {
            UpdateCommandBuilder u = new UpdateCommandBuilder(constr, tableName);
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
            if (!string.IsNullOrEmpty(txtCrushedscale.Text.Trim()))
            {
                u.UpdateColumn("sk_scale", Convert.ToDecimal(txtCrushedscale.Text.Trim().Split('%')[0]) / 100);
            }
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
            u.UpdateColumn("Semi_Product_Goods", txtlb7.Text.Trim());
            u.UpdateColumn("Semi_Product_Type", ddlLb7.SelectedItem.Value);
            u.UpdateColumn("modi_date", "getdate()");
            u.ConditionsColumn("goods_name", Goods_name);
            u.ConditionsColumn("version", Verson);
            return u.getUpdateCommand();
        }
        protected int InsertData(string tableName, string id)
        {
            InsertCommandBuilder ins = new InsertCommandBuilder(constr, tableName);
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
                ins.InsertColumn("sk_scale", Convert.ToDecimal(txtCrushedscale.Text.Trim().Split('%')[0]) / 100);
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["name"].ToString() != txtgoodsName.Text.Trim())
                {
                    Response.Write("<script>alert('添加记录请点击新增按钮!')</script>");
                    return;
                }
                List<string> SqlList = new List<string>();
                string Usql = getUpdateDataSQL("goods_tmp", ViewState["id"].ToString());
                int count = 0;
                if (Rbt4M.Checked == true)
                {
                    string Tsql = getUpdateDataSQL("goods_tran", hifId.Value);
                    string Csql = getUpdateDataSQL("goods_chage_record", txtgoodsName.Text.Trim().ToUpper(), txtVersion.Text.Trim().ToUpper().ToUpper());
                    SqlList.Add(Tsql);
                    SqlList.Add(Csql);
                }
                SqlList.Add(Usql);
                InsertCommandBuilder up = new InsertCommandBuilder(constr, "");
                count = up.ExcutTransaction(SqlList);
                if (count != 0)
                {
                    dgvList.DataSource = getConfirmGoodsList(ViewState["id"].ToString());
                    dgvList.DataBind();
                    Response.Write("<script>alert('保存成功!')</script>");
                }
                else
                {
                    Response.Write("<script>alert('保存失败!')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "!')</script>");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string sql1 = "select count(*) from goods_tmp where goods_name = '" + txtgoodsName.Text.Trim().ToUpper() + "' and version = '" + txtVersion.Text.Trim().ToUpper().ToUpper() + "'";
                int count = Convert.ToInt32(new SelectCommandBuilder(constr, "").ExecuteScalar(sql1));
                if (count != 0)
                {
                    Response.Write("<script>alert('资料重复，请确认后再试!')</script>");
                    return;
                }
                string id = CommadMethod.getNextId("GID" + DateTime.Now.ToString("yyyyMMdd"), "");
                if (InsertData("goods_tmp", id) != 0)
                {
                    Response.Write("<script>alert('新增成功!')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "!')</script>");
            }
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            btnRun.Attributes["onclick"] = this.GetPostBackEventReference(this.btnRun) + ";this.disabled=true;this.value='处理中...'";
            string id = CommadMethod.getNextId("GID" + DateTime.Now.ToString("yyyyMMdd"), "");
            txtMNo.BackColor = Color.White;
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "");
            string sql = "select count(*) from goods_tran where goods_no=(select DISTINCT  goods_name from goods_tmp where goods_id = '" + ViewState["id"].ToString() + "')";
            string sql1 = "select count(*) from goods_tmp where goods_name = '" + txtgoodsName.Text.Trim().ToUpper() + "' and version = '" + txtVersion.Text.Trim().ToUpper().ToUpper() + "'";
            string sql2 = "select count(*) from materials where name = '" + txtMNo.Text.Trim().ToUpper() + "'";
            string sql3 = "select count(*) from goods_chage_record where goods_name='" + txtgoodsName.Text.Trim().ToUpper() + "' and version = '" + txtVersion.Text.Trim().ToUpper().ToUpper() + "'";
            int count = Convert.ToInt32(s.ExecuteScalar(sql));
            int count1 = Convert.ToInt32(s.ExecuteScalar(sql1));
            int count2 = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(sql2));
            int count3 = Convert.ToInt32(s.ExecuteScalar(sql3));
            int i = 0;
            if (count2 == 0)
            {
                Response.Write("<script>alert('材料编号不存在,请确认后再试!')</script>");
                txtMNo.BackColor = Color.Red;
                txtMNo.Focus();
                return;
            }
            if (count3 != 0)
            {
                Response.Write("<script>alert('该部品已经被发行!')</script>");
                return;
            }
            if (count1 == 0)
            {
                InsertData("goods_tmp", id);
                hifId.Value = id;
            }
            if (count != 0)
            {
                //UpdateData("goods_tran");
                new DeleteCommandBuilder(constr, "").ExecuteNonQuery("delete from goods_tran where goods_no = '" + txtgoodsName.Text.Trim().ToUpper() + "'");
                InsertData("goods_tran", hifId.Value.Trim());
            }
            else
            {
                InsertData("goods_tran", hifId.Value.Trim());
            }
            UpdateCommandBuilder u = new UpdateCommandBuilder(constr, "goods_tran");
            u.UpdateColumn("isConfirm", "Waiting");
            u.ConditionsColumn("goods_no", txtgoodsName.Text.Trim().ToUpper());
            u.getUpdateCommand();
            i = Convert.ToInt32(u.ExecuteNonQuery());
            if (i != 0)
            {
                InsertData("goods_chage_record", hifId.Value.Trim());
                new UpdateCommandBuilder(constr, "").ExecuteNonQuery("update goods_tmp set lb8_id = '1'");
                Response.Write("<script>alert('发行成功')</script>");
                dgvList.DataSource = getConfirmGoodsList(ViewState["id"].ToString());
                dgvList.DataBind();
                string _body = "部番:" + txtgoodsName.Text + " 版本：" + txtVersion.Text.Trim() + " 已发行成功," + "<a href='http://192.168.1.199:81/GoodsManager/Goods_Aprove.aspx'>点击此链接快速审核...</a>";
                SendMail.ExecuteSendMail(smtpIp, Port, fromUser, fromPwd, mailList, ccList, "部品发行成功通知", _body, "", "");
            }
            else
            {
                Response.Write("<script>alert('发行失败')</script>");
            }
        }

        protected void txtgoodsName_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            txtVersion.Focus();
        }
        protected List<goods> getConfirmGoodsList(string goodsId)
        {
            List<goods> gList = new List<goods>();
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "");
            string sql = "select goods_Id, RTRIM(goods_name) + '(' + RTRIM(Version) + ')' AS goods_name,mjh,goods_ename,dw,qs,Materail_Number, Materail_Name,ys, Materail_Model, Materail_Vender_Color,Materail_Color,cpdz,skdz, Drying_Temperature, Drying_Time,sk_scale,cxzq,khdm = (select customer_name from customer where customer_id = khdm),remark,Fire_Retardant_Grade,Buyer,Toner_Model,Toner_Buyer,Rohs_Certification,Aircraft,Model_Abrasives,Model_Type from goods_chage_record where goods_no =(Select DISTINCT goods_name from goods_tmp where goods_id= '" + goodsId + "')";
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
                    };
                    gList.Add(g);
                }
            }
            return gList;

        }

        protected void dgvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (dgvList.Rows.Count > 1)
            {
                for (int i = 0; i < dgvList.Rows.Count - 1; i++)
                {
                    for (int j = 1; j < dgvList.Rows[i].Cells.Count - 1; j++)
                    {
                        if ((dgvList.Rows[i].Cells[j].FindControl("Label" + j) as Label).Text.Trim().ToUpper() != (dgvList.Rows[i + 1].Cells[j].FindControl("Label" + j) as Label).Text.Trim().ToUpper())
                        {
                            (dgvList.Rows[i + 1].Cells[j].FindControl("Label" + j) as Label).ForeColor = Color.Red;
                        }
                    }
                }
            }
        }

        protected void txtVersion_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            txtjigNo.Focus();
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Goods_List.aspx");
        }

        protected void txtMColor_TextChanged(object sender, EventArgs e)
        {
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "materials");
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
            }
            else
            {
                txtMName.Focus();
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int c = new DeleteCommandBuilder(constr, "").ExecuteNonQuery("delete from goods_tmp where goods_id = '" + ViewState["id"].ToString() + "'");
            if (c != 0)
            {
                Response.Write("<script>alert('删除成功!')</script>");
                dgvList.DataSource = getConfirmGoodsList(ViewState["id"].ToString());
                dgvList.DataBind();
            }
            else
            {
                Response.Write("<script>alert('删除失败!')</script>");
            }
        }
        protected void getlb7()
        {
            string sql = "SELECT lb7_id, lb7_name FROM lb7";
            DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql);
            ddlLb7.Items.Clear();
            ddlLb7.Items.Add(new ListItem("无选择", "00"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlLb7.Items.Add(new ListItem(dt.Rows[i]["lb7_name"].ToString(), dt.Rows[i]["lb7_id"].ToString()));
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("Goods_List.aspx");
        }
    }
}