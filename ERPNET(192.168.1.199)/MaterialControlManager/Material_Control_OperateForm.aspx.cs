using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Configuration;
using System.Drawing;
using ERPPlugIn.Class;
using System.Data;

namespace ERPPlugIn.MaterialControlManager
{
    public partial class Material_Control_OperateForm : System.Web.UI.Page
    {
        //private List<string> sList = new List<string>();
        private List<string> sList
        {
            set { ViewState["sList"] = value; }
            get { return ViewState["sList"] as List<string>; }
        }
        private int step
        {
            set { ViewState["step"] = value; }
            get { return Convert.ToInt32(ViewState["step"]); }
        }
        private string area
        {
            set { ViewState["area"] = value; }
            get { return ViewState["area"].ToString(); }
        }
        private string goodsName
        {
            set { ViewState["goodsName"] = value; }
            get { return ViewState["goodsName"].ToString(); }
        }
        private int Qty
        {
            set { ViewState["Qty"] = value; }
            get { return Convert.ToInt32(ViewState["Qty"]); }
        }
        private string dfdh
        {
            set { ViewState["dfdh"] = value; }
            get { return ViewState["dfdh"].ToString(); }
        }
        private string fromArea
        {
            set { ViewState["fromArea"] = value; }
            get { return ViewState["fromArea"].ToString(); }
        }
        private int total
        {
            set { ViewState["total"] = value; }
            get { return Convert.ToInt32(ViewState["total"]); }
        }
        private string str_bill_id
        {
            set { ViewState["str_bill_id"] = value; }
            get { return ViewState["str_bill_id"].ToString(); }
        }
        private string hwh
        {
            set { ViewState["hwh"] = value; }
            get { return ViewState["hwh"].ToString(); }
        }
        private string store_id
        {
            set { ViewState["store_id"] = value; }
            get { return ViewState["store_id"].ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                sList = new List<string>();
                sList.Insert(0, AppendDateTime("开始指令???"));
                alertMsg();
                txtInput.BackColor = Color.LightGreen;
                total = 0;
            }
        }

        protected void sr_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInput.Text))
                {
                    txtInput.Focus();
                    return;
                }
                switch (step)
                {
                    case 0:
                        if (txtInput.Text.ToLower() != "begin")
                        {
                            sList.Insert(0, AppendDateTime("NG,指令错误"));
                            sList.Insert(0, AppendDateTime("开始指令???"));
                            alertMsg();
                            return;
                        }
                        step += 1;
                        sList.Insert(0, AppendDateTime("OK,工号???"));
                        alertMsg();
                        break;
                    case 1:
                        if (!getUser(txtInput.Text.Trim().ToUpper()))
                        {
                            sList.Insert(0, AppendDateTime("NG,用户不存在"));
                            alertMsg();
                            return;
                        }
                        step += 1;
                        ViewState["UserId"] = txtInput.Text.Trim().ToUpper();
                        sList.Insert(0, AppendDateTime(ViewState["UserId"].ToString()));
                        sList.Insert(0, AppendDateTime("OK,请扫入区域???"));
                        alertMsg();
                        break;
                    case 2:
                        if (txtInput.Text.ToLower() == "end")
                        {
                            sList.Insert(0, AppendDateTime("OK,结束指令，需要操作请重新刷读指令"));
                            alertMsg();
                            step = 0;
                            return;
                        }
                        area = txtInput.Text;
                        if (area.Length > 11)
                        {
                            area = area.Substring(area.Length - 6, 6);

                        }
                        else
                        {
                            area = area;
                        }
                        step += 1;
                        switch (area.ToLower())
                        {
                            case "waitstorge":
                                sList.Insert(0, AppendDateTime("待入库区"));
                                sList.Insert(0, AppendDateTime("OK,入库票???"));
                                alertMsg();
                                break;
                            case "storge":
                                hwh = txtInput.Text.Trim().ToLower().Split(' ')[0];
                                store_id = txtInput.Text.Trim().ToLower().Split(' ')[1];
                                sList.Insert(0, AppendDateTime("仓库"));
                                sList.Insert(0, AppendDateTime("OK,请扫入库票"));
                                alertMsg();
                                step += 1;
                                break;
                            case "pickarea":
                                sList.Insert(0, AppendDateTime("选别区"));
                                sList.Insert(0, AppendDateTime("OK,请扫入库票???"));
                                alertMsg();
                                break;
                            case "print":
                                sList.Insert(0, AppendDateTime("丝印区"));
                                sList.Insert(0, AppendDateTime("OK,请扫入库票???"));
                                alertMsg();
                                break;
                            case "z_area":
                                sList.Insert(0, AppendDateTime("暂放区域"));
                                sList.Insert(0, AppendDateTime("OK,请扫入库票???"));
                                alertMsg();
                                break;
                            default:
                                sList.Insert(0, AppendDateTime("NG,区域错误"));
                                sList.Insert(0, AppendDateTime("请重新扫描区域???"));
                                alertMsg();
                                step = 2;
                                break;
                        }
                        break;
                    case 3:
                        switch (area.ToLower())
                        {
                            case "waitstorge":
                                if (txtInput.Text.ToLower() == "end")
                                {
                                    sList.Insert(0, AppendDateTime("OK,结束指令，需要操作请重新刷读指令"));
                                    alertMsg();
                                    step = 0;
                                    return;
                                }
                                string[] List = getBarCode(txtInput.Text);
                                if (List.Length < 7)
                                {
                                    sList.Insert(0, AppendDateTime("NG,条码格式错误!"));
                                    sList.Insert(0, AppendDateTime("NG,重新入库票"));
                                    step = 3;
                                    alertMsg();
                                    return;
                                }
                                goodsName = List[2];
                                Qty = int.Parse(List[6]);
                                int wCount = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("SELECT count(*) FROM Material_control WHERE (label = '" + List[0] + "' and process_id = '" + 1 + "')"));
                                if (wCount != 0)
                                {
                                    sList.Insert(0, AppendDateTime("入库票重复"));
                                    sList.Insert(0, AppendDateTime("NG,请重新入库票???"));
                                    step = 3;
                                    alertMsg();
                                    return;
                                }
                                waitStorge(List[0], goodsName, Qty, ViewState["UserId"].ToString());
                                sList.Insert(0, AppendDateTime("部番：" + goodsName + "  数量：" + Qty));
                                sList.Insert(0, AppendDateTime("OK,请扫入区域"));
                                step = 2;
                                alertMsg();
                                break;
                            case "z_area":
                                if (txtInput.Text.ToLower() == "end")
                                {
                                    sList.Insert(0, AppendDateTime("OK,结束指令，需要操作请重新刷读指令"));
                                    alertMsg();
                                    step = 0;
                                    return;
                                }
                                string[] zList = getBarCode(txtInput.Text);
                                if (zList.Length < 7)
                                {
                                    sList.Insert(0, AppendDateTime("NG,入库票格式错误!"));
                                    sList.Insert(0, AppendDateTime("NG,重新刷入库票"));
                                    return;
                                }
                                goodsName = zList[2];
                                Qty = int.Parse(zList[6]);
                                int zCount = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("select count(*) from material_control where label='" + zList[0] + "' and process_id= 5 "));
                                if (zCount != 0)
                                {
                                    sList.Insert(0, AppendDateTime("入库票重复"));
                                    sList.Insert(0, AppendDateTime("入库票重复"));
                                    step = 3;
                                    alertMsg();
                                    return;
                                }
                                string sqlz = "SELECT TOP 1 process_id FROM Material_control where label = '" + zList[0] + "' ORDER BY operate_time DESC";
                                DataTable dtz = new SelectCommandBuilder().ExecuteDataTable(sqlz);
                                if (dtz == null || dtz.Rows.Count == 0)
                                {
                                    sList.Insert(0, AppendDateTime("NG,未入待入库区,请确认扫描后再入库!"));
                                    sList.Insert(0, AppendDateTime("NG,重新入库票"));
                                    step = 3;
                                    alertMsg();
                                    return;
                                }
                                int z_areaId = int.Parse(dtz.Rows[0][0].ToString());
                                string z_QtySql = "SELECT ISNULL(SUM(CurrQty), 0) AS CurrQty FROM Material_control_process WHERE (label = '" + zList[0] + "' and process_id = 1)";
                                int z_Result = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(z_QtySql));
                                if (z_Result < Qty)
                                {
                                    sList.Insert(0, AppendDateTime("NG,数量超出待入库区数量，无法入库!"));
                                    sList.Insert(0, AppendDateTime("NG,重新入库票"));
                                    step = 4;
                                }
                                else
                                {
                                    z_area(zList[0], goodsName, Qty, ViewState["UserId"].ToString());
                                    sList.Insert(0, AppendDateTime("部番：" + goodsName + "  数量：" + Qty));
                                    sList.Insert(0, AppendDateTime("OK,请扫入区域"));
                                    step = 2;
                                }
                                alertMsg();
                                break;
                            case "pickarea":
                                if (txtInput.Text.ToLower() == "end")
                                {
                                    sList.Insert(0, AppendDateTime("OK,结束指令，需要操作请重新刷读指令"));
                                    alertMsg();
                                    step = 0;
                                    return;
                                }
                                string[] pList = getBarCode(txtInput.Text);
                                if (pList.Length < 7)
                                {
                                    sList.Insert(0, AppendDateTime("NG,条码格式错误!"));
                                    sList.Insert(0, AppendDateTime("NG,重新入库票"));
                                    step = 3;
                                    alertMsg();
                                    return;
                                }
                                goodsName = pList[2];
                                Qty = int.Parse(pList[6]);
                                int pCount = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("SELECT count(*) FROM Material_control WHERE (label = '" + pList[0] + "' and process_id = '" + 2 + "')"));
                                if (pCount != 0)
                                {
                                    sList.Insert(0, AppendDateTime("入库票重复"));
                                    sList.Insert(0, AppendDateTime("NG,请重新入库票???"));
                                    step = 3;
                                    alertMsg();
                                    return;
                                }
                                List<string> paList = new List<string>();
                                string sql1 = "select count(str_in_bill_id) from pre_str_in_bill where str_in_bill_no = '" + pList[0] + "'";
                                int hasCount1 = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(sql1));
                                if (hasCount1 != 0)
                                {
                                    sList.Insert(0, AppendDateTime("NG,该入库票已入库???"));
                                    sList.Insert(0, AppendDateTime("条码???"));
                                    step = 5;
                                    alertMsg();
                                    return;
                                }
                                str_bill_id = CommadMethod.getNextId("Q");
                                InsertCommandBuilder insert = new InsertCommandBuilder("pre_str_in_bill");
                                insert.InsertColumn("str_in_bill_id", str_bill_id);
                                insert.InsertColumn("str_in_type_id", "C");
                                insert.InsertColumn("str_in_bill_no", pList[0]);
                                insert.InsertColumn("operator_date", "getdate()");
                                insert.InsertColumn("str_in_date", "getdate()");
                                insert.InsertColumn("store_id", "12");
                                insert.InsertColumn("create_user", ViewState["UserId"]);
                                insert.InsertColumn("operator_id", "0000");
                                insert.InsertColumn("dfdh", pList[1]);
                                insert.InsertColumn("come_from", "生产加工入库");
                                insert.InsertColumn("islocal", "Y");
                                insert.InsertColumn("verifier", "0024");
                                insert.InsertColumn("bill_num", "0");
                                insert.InsertColumn("paydate", "1900/1/1");
                                //insert.InsertColumn("is_state", "N");
                                paList.Add(insert.getInsertCommand());
                                //入库操作
                                InsertCommandBuilder ins1 = new InsertCommandBuilder("pre_str_in_bill_detail");
                                ins1.InsertColumn("batch_id", CommadMethod.getNextId("Q"));
                                ins1.InsertColumn("str_in_bill_id", str_bill_id);
                                ins1.InsertColumn("goods_id", new SelectCommandBuilder().ExecuteDataTable("select goods_id from goods where goods_name='" + goodsName + "'").Rows[0][0].ToString());
                                ins1.InsertColumn("qty", Qty);
                                ins1.InsertColumn("pch", pList[0].Substring(2, 6));
                                ins1.InsertColumn("hwh", "NG01");
                                ins1.InsertColumn("piece", "0");
                                ins1.InsertColumn("price", "0");
                                ins1.InsertColumn("inqty", Qty);
                                ins1.InsertColumn("exam", " ");
                                ins1.InsertColumn("yxq", "1900/1/1");
                                ins1.InsertColumn("producedate", "1900/1/1");
                                paList.Add(ins1.getInsertCommand());
                                string QtySql1 = "SELECT ISNULL(SUM(CurrQty), 0) AS CurrQty FROM Material_control_process WHERE (label = '" + pList[0] + "' and process_id = '1')";
                                int Result1 = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(QtySql1));
                                if (Result1 < Qty)
                                {
                                    sList.Insert(0, AppendDateTime("NG,数量不足，无法入库3!"));
                                    sList.Insert(0, AppendDateTime("NG,请注明手动处理，重扫区域???"));
                                    step = 2;
                                    alertMsg();
                                    return;
                                }
                                else
                                {
                                    PickArea(pList[0], goodsName, Qty, ViewState["UserId"].ToString());
                                    ins1.ExcutTransaction(paList);
                                    sList.Insert(0, AppendDateTime("部番：" + goodsName + "  数量：" + Qty));
                                    sList.Insert(0, AppendDateTime("OK,请扫描区域???"));
                                    step = 2;
                                    alertMsg();
                                    break;
                                }
                            case "print":
                                if (txtInput.Text.ToLower() == "end")
                                {
                                    sList.Insert(0, AppendDateTime("OK,结束指令，需要操作请重新刷读指令"));
                                    alertMsg();
                                    step = 0;
                                    return;
                                }
                                string[] prList = getBarCode(txtInput.Text);
                                if (prList.Length < 7)
                                {
                                    sList.Insert(0, AppendDateTime("NG,条码格式错误!"));
                                    sList.Insert(0, AppendDateTime("NG,重新入库票"));
                                    step = 3;
                                    alertMsg();
                                    return;
                                }
                                goodsName = prList[2];
                                Qty = int.Parse(prList[6]);
                                int prCount = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar("SELECT count(*) FROM Material_control WHERE (label = '" + prList[0] + "' and process_id = '" + 3 + "')"));
                                if (prCount != 0)
                                {
                                    sList.Insert(0, AppendDateTime("入库票重复"));
                                    sList.Insert(0, AppendDateTime("NG,请重新入库票???"));
                                    step = 3;
                                    alertMsg();
                                    return;
                                }
                                string QtySql2 = "SELECT ISNULL(SUM(CurrQty), 0) AS CurrQty FROM Material_control_process WHERE (label = '" + prList[0] + "' and process_id = '" + 1 + "')";
                                int Result2 = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(QtySql2));
                                if (Result2 < Qty)
                                {
                                    sList.Insert(0, AppendDateTime("NG,数量不足，无法入库1!"));
                                    sList.Insert(0, AppendDateTime("NG,重新入库票???"));
                                    step = 2;
                                    alertMsg();
                                    return;
                                }
                                else
                                {
                                    print(prList[0], goodsName, Qty, ViewState["UserId"].ToString());
                                    sList.Insert(0, AppendDateTime("部番：" + goodsName + "  数量：" + Qty));
                                    sList.Insert(0, AppendDateTime("OK,请扫描区域???"));
                                    List<string> printList = new List<string>();
                                    str_bill_id = CommadMethod.getNextId("Q");
                                    InsertCommandBuilder pInser = new InsertCommandBuilder("pre_str_in_bill");
                                    pInser.InsertColumn("str_in_bill_id", str_bill_id);
                                    pInser.InsertColumn("str_in_type_id", "C");
                                    pInser.InsertColumn("str_in_bill_no", prList[0]);
                                    pInser.InsertColumn("operator_date", "getdate()");
                                    pInser.InsertColumn("str_in_date", "getdate()");
                                    pInser.InsertColumn("store_id", "11");
                                    pInser.InsertColumn("create_user", ViewState["UserId"]);
                                    pInser.InsertColumn("operator_id", "0000");
                                    pInser.InsertColumn("dfdh", prList[1]);
                                    pInser.InsertColumn("come_from", "生产加工入库");
                                    pInser.InsertColumn("islocal", "Y");
                                    pInser.InsertColumn("verifier", "0024");
                                    pInser.InsertColumn("bill_num", "0");
                                    pInser.InsertColumn("paydate", "1900/1/1");
                                    //insert.InsertColumn("is_state", "N");
                                    printList.Add(pInser.getInsertCommand());
                                    //入库操作
                                    InsertCommandBuilder pIinserd = new InsertCommandBuilder("pre_str_in_bill_detail");
                                    pIinserd.InsertColumn("batch_id", CommadMethod.getNextId("Q"));
                                    pIinserd.InsertColumn("str_in_bill_id", str_bill_id);
                                    pIinserd.InsertColumn("goods_id", new SelectCommandBuilder().ExecuteDataTable("select goods_id from goods where goods_name='" + goodsName + "'").Rows[0][0].ToString());
                                    pIinserd.InsertColumn("qty", Qty);
                                    pIinserd.InsertColumn("pch", prList[0].Substring(2, 6));
                                    pIinserd.InsertColumn("hwh", "SY");
                                    pIinserd.InsertColumn("piece", "0");
                                    pIinserd.InsertColumn("price", "0");
                                    pIinserd.InsertColumn("inqty", Qty);
                                    pIinserd.InsertColumn("exam", " ");
                                    pIinserd.InsertColumn("yxq", "1900/1/1");
                                    pIinserd.InsertColumn("producedate", "1900/1/1");
                                    printList.Add(pIinserd.getInsertCommand());
                                    pIinserd.ExcutTransaction(printList);
                                }
                                step = 2;
                                alertMsg();
                                break;
                        }
                        break;
                    case 4:
                        if (txtInput.Text.ToLower() == "end")
                        {
                            sList.Insert(0, AppendDateTime("OK,结束指令，需要操作请重新刷读指令"));
                            str_bill_id = string.Empty;
                            alertMsg();
                            step = 0;
                            return;
                        }
                        else if (txtInput.Text.ToLower() == "reenter")
                        {
                            sList.Insert(0, AppendDateTime("OK,货位号???"));
                            step = 3;
                            alertMsg();
                            return;
                        }

                        string[] aList = getBarCode(txtInput.Text);
                        if (aList.Length < 7)
                        {
                            sList.Insert(0, AppendDateTime("NG,条码格式错误!"));
                            sList.Insert(0, AppendDateTime("NG,重新入库票"));
                            step = 4;
                            alertMsg();
                            return;
                        }
                        goodsName = aList[2];
                        Qty = int.Parse(aList[6]);
                        string goods_id = new SelectCommandBuilder().ExecuteDataTable("select goods_id from goods where goods_name='" + goodsName + "'").Rows[0][0].ToString();
                        List<string> Alist = new List<string>();
                        string sql = "select count(str_in_bill_id) from pre_str_in_bill where str_in_bill_no = '" + aList[0] + "'";
                        int hasCount = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(sql));
                        if (hasCount != 0)
                        {
                            sList.Insert(0, AppendDateTime("NG,该入库票已入库???"));
                            sList.Insert(0, AppendDateTime("条码???"));
                            step = 4;
                            alertMsg();
                            return;
                        }
                        string lbel1 = aList[0].Substring(0, 2).ToString();
                        if (lbel1 == "XB")
                        {
                            List<string> olist = new List<string>();
                            string str_out_bill_id = CommadMethod.getNextId("Q");
                            string dfdh = CommadMethod.getNextId("Z");
                            InsertCommandBuilder insertout = new InsertCommandBuilder("pre_str_out_bill");
                            insertout.InsertColumn("str_out_bill_id", str_out_bill_id);
                            insertout.InsertColumn("str_out_bill_no", aList[0]);
                            insertout.InsertColumn("dfdh", dfdh);
                            insertout.InsertColumn("str_out_type_id", "2");
                            insertout.InsertColumn("str_out_date", "getdate()");
                            insertout.InsertColumn("store_id", "12");
                            insertout.InsertColumn("operator_id", "0000");
                            insertout.InsertColumn("transactor", "曾松林");
                            insertout.InsertColumn("verifier", "曾松林");
                            insertout.InsertColumn("come_to", "成品");
                            insertout.InsertColumn("bill_num", "1");
                            insertout.InsertColumn("operator_date", "getdate()");
                            insertout.InsertColumn("islocal", "Y");
                            insertout.InsertColumn("gsptypeid", "2");
                            olist.Add(insertout.getInsertCommand());
                            string Dsql = @"SELECT stock_remain.goods_id, stock_remain.qty, batch.batch_id,batch.hwh, stock_remain.store_id FROM 
                            stock_remain INNER JOIN batch ON stock_remain.batch_id = batch.batch_id 
                            WHERE (stock_remain.goods_id = '" + goods_id + "') AND (batch.hwh = 'NG01') and stock_remain.store_id='12' order by right(rtrim(batch.pch),6)";
                            SelectCommandBuilder s = new SelectCommandBuilder();
                            int count = Convert.ToInt32(s.ExecuteScalar(Dsql));
                            if (count == 0)
                            {
                                sList.Insert(0, "选别区数量不足");
                                sList.Insert(0, "NG,请扫描区域");
                                step = 2;
                                alertMsg();
                                return;
                            }
                            DataTable Dqty = s.ExecuteDataTable(Dsql);
                            int dbqty = 0;
                            int xQty = Qty;
                            for (int i = 0; i < Dqty.Rows.Count; i++)
                            {
                                dbqty = xQty - Convert.ToInt32(Dqty.Rows[i]["qty"]);
                                if (dbqty == 0)
                                {
                                    InsertCommandBuilder insd = new InsertCommandBuilder("pre_str_out_bill_detail");
                                    insd.InsertColumn("str_out_bill_id", str_out_bill_id);
                                    insd.InsertColumn("goods_id", goods_id);
                                    insd.InsertColumn("batch_id", Dqty.Rows[i]["batch_id"]);
                                    insd.InsertColumn("qty", xQty);
                                    insd.InsertColumn("exam", " ");
                                    insd.InsertColumn("price", "0");
                                    insd.InsertColumn("Can_sale", "Y");
                                    insd.InsertColumn("DSort", "1");
                                    insd.InsertColumn("CostPrice", "0");
                                    insd.InsertColumn("hwh", hwh);
                                    olist.Add(insd.getInsertCommand());
                                    break;
                                }
                                else if (dbqty > 0)
                                {
                                    InsertCommandBuilder insd = new InsertCommandBuilder("pre_str_out_bill_detail");
                                    insd.InsertColumn("str_out_bill_id", str_out_bill_id);
                                    insd.InsertColumn("goods_id", goods_id);
                                    insd.InsertColumn("batch_id", Dqty.Rows[i]["batch_id"]);
                                    insd.InsertColumn("qty", Dqty.Rows[i]["qty"]);
                                    insd.InsertColumn("exam", " ");
                                    insd.InsertColumn("price", "0");
                                    insd.InsertColumn("Can_sale", "Y");
                                    insd.InsertColumn("DSort", "1");
                                    insd.InsertColumn("CostPrice", "0");
                                    insd.InsertColumn("hwh", hwh);
                                    xQty = dbqty;
                                    olist.Add(insd.getInsertCommand());
                                    continue;
                                }
                                else if (dbqty < 0)
                                {
                                    InsertCommandBuilder insd = new InsertCommandBuilder("pre_str_out_bill_detail");
                                    insd.InsertColumn("str_out_bill_id", str_out_bill_id);
                                    insd.InsertColumn("goods_id", goods_id);
                                    insd.InsertColumn("batch_id", Dqty.Rows[i]["batch_id"]);
                                    insd.InsertColumn("qty", xQty);
                                    insd.InsertColumn("exam", " ");
                                    insd.InsertColumn("price", "0");
                                    insd.InsertColumn("Can_sale", "Y");
                                    insd.InsertColumn("DSort", "1");
                                    insd.InsertColumn("CostPrice", "0");
                                    insd.InsertColumn("hwh", hwh);
                                    olist.Add(insd.getInsertCommand());
                                    break;
                                }
                            }
                            new InsertCommandBuilder().ExcutTransaction(olist);
                            Storge(aList[0], goodsName, Qty, 2, ViewState["UserId"].ToString());//1:待入库 2：选别
                            sList.Insert(0, AppendDateTime("部番：" + goodsName + "  数量：" + Qty));
                            sList.Insert(0, AppendDateTime("OK,请扫描区域"));
                            step = 2;
                            alertMsg();
                            return;
                        }
                        str_bill_id = CommadMethod.getNextId("Q");
                        InsertCommandBuilder insert3 = new InsertCommandBuilder("pre_str_in_bill");
                        insert3.InsertColumn("str_in_bill_id", str_bill_id);
                        insert3.InsertColumn("dfdh", aList[1]);
                        insert3.InsertColumn("str_in_type_id", "C");
                        insert3.InsertColumn("verifier", "0024");
                        insert3.InsertColumn("bill_num", "0");
                        insert3.InsertColumn("paydate", "1900/1/1");
                        insert3.InsertColumn("str_in_bill_no", aList[0]);
                        insert3.InsertColumn("operator_date", "getdate()");
                        insert3.InsertColumn("str_in_date", "getdate()");
                        insert3.InsertColumn("store_id", "03");//store_id);//暂时固定为03
                        insert3.InsertColumn("create_user", ViewState["UserId"]);
                        insert3.InsertColumn("operator_id", "0000");
                        insert3.InsertColumn("come_from", "生产加工入库");
                        insert3.InsertColumn("islocal", "Y");
                        Alist.Add(insert3.getInsertCommand());
                        InsertCommandBuilder ins = new InsertCommandBuilder("pre_str_in_bill_detail");
                        ins.InsertColumn("batch_id", CommadMethod.getNextId("Q"));
                        ins.InsertColumn("str_in_bill_id", str_bill_id);
                        ins.InsertColumn("goods_id", new SelectCommandBuilder().ExecuteDataTable("select goods_id from goods where goods_name='" + goodsName + "'").Rows[0][0].ToString());
                        ins.InsertColumn("qty", Qty);
                        ins.InsertColumn("exam", " ");
                        ins.InsertColumn("yxq", "1900/1/1");
                        ins.InsertColumn("Producedate", "1900/1/1");
                        ins.InsertColumn("inqty", Qty);
                        ins.InsertColumn("notin", "1");
                        ins.InsertColumn("tax_rate", "17");
                        ins.InsertColumn("piece", "0");
                        ins.InsertColumn("price", "0");
                        ins.InsertColumn("hwh", hwh);
                        ins.InsertColumn("pch", aList[0].Substring(2, 6));
                        Alist.Add(ins.getInsertCommand());
                        string sql2 = "SELECT TOP 1 process_id FROM Material_control where label = '" + aList[0] + "' ORDER BY operate_time DESC";
                        DataTable dt = new SelectCommandBuilder().ExecuteDataTable(sql2);
                        if (dt == null || dt.Rows.Count == 0)
                        {
                            sList.Insert(0, AppendDateTime("NG,前端未扫描,请确认扫描后再入库!"));
                            sList.Insert(0, AppendDateTime("NG,重新入库票"));
                            step = 4;
                            alertMsg();
                            return;
                        }
                        int areaId = int.Parse(dt.Rows[0][0].ToString());
                        string QtySql = "SELECT ISNULL(SUM(CurrQty), 0) AS CurrQty FROM Material_control_process WHERE (label = '" + aList[0] + "' and process_id = '" + areaId + "')";
                        int Result = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(QtySql));
                        if (Result < Qty)
                        {
                            sList.Insert(0, AppendDateTime("NG,数量不足，无法入库2!"));
                            sList.Insert(0, AppendDateTime("NG,重新入库票"));
                            step = 4;
                        }
                        else
                        {
                            Storge(aList[0], goodsName, Qty, areaId, ViewState["UserId"].ToString());//1:待入库 2：选别
                            ins.ExcutTransaction(Alist);
                            sList.Insert(0, AppendDateTime("部番：" + goodsName + "  数量：" + Qty));
                            sList.Insert(0, AppendDateTime("OK,请扫描区域"));
                            step = 2;
                        }
                        alertMsg();
                        break;
                }
            }

            catch (Exception ex)
            {
                sList.Insert(0, AppendDateTime(ex.Message));
                sList.Insert(0, AppendDateTime("NG,出现错误,请重新刷读"));
                alertMsg();
            }
        }
        public void alertMsg()
        {
            txtAlertMsg.Text = null;
            txtInput.Text = string.Empty;
            for (int i = 0; i < sList.Count; i++)
            {
                txtAlertMsg.Text += sList[i] + "\r\n";
                txtInput.Focus();
            }
            if (sList[0].Substring(0, 2) == "OK")
            {
                txtInput.BackColor = Color.LightGreen;
            }
            else
            {
                txtInput.BackColor = Color.Red;
            }
        }
        public string AppendDateTime(string msg)
        {
            string Msg = "";
            Msg = msg += " " + DateTime.Now.ToString("HH:mm:ss");
            return Msg;
        }
        public void waitStorge(string label, string goodsName, int Qty, string userId)
        {
            InsertData(label, goodsName, Qty, 1, userId);
            SelectCommandBuilder s = new SelectCommandBuilder();
            int count = Convert.ToInt32(s.ExecuteScalar("select count(*) from Material_control_process where label = '" + label + "' and process_id = 1"));
            string sql = string.Empty;
            if (count != 0)
            {
                UpdateCommandBuilder up = new UpdateCommandBuilder();
                sql = "update Material_control_process set InQty = InQty + " + Qty + " ,CurrQty = CurrQty + " + Qty + " where label = '" + label + "' and process_id = 1";
            }
            else
            {
                InsertCommandBuilder ins = new InsertCommandBuilder("Material_control_process");
                ins.InsertColumn("label", label);
                ins.InsertColumn("goods_name", goodsName);
                ins.InsertColumn("process_id", 1);
                ins.InsertColumn("InQty", Qty);
                ins.InsertColumn("OutQty", 0);
                ins.InsertColumn("CurrQty", Qty);
                ins.InsertColumn("in_date", "getDate()");
                sql = ins.getInsertCommand();
            }
            new InsertCommandBuilder().ExecuteNonQuery(sql);

        }
        public void z_area(string label, string goodsName, int Qty, string userId)
        {
            InsertData(label, goodsName, Qty, 5, userId);
            SelectCommandBuilder s = new SelectCommandBuilder();
            int count = Convert.ToInt32(s.ExecuteScalar("select count(*) from Material_control_process where label='" + label + "' and process_id = 5 "));
            string sql = string.Empty;
            if (count != 0)
            {
                UpdateCommandBuilder up = new UpdateCommandBuilder();
                sql = "update Material_control_process set InQty = InQty + " + Qty + " ,CurrQty = CurrQty + " + Qty + " where label = '" + label + "' and process_id = 5";
            }
            else
            {
                InsertCommandBuilder ins = new InsertCommandBuilder("Material_control_process");
                ins.InsertColumn("label", label);
                ins.InsertColumn("goods_name", goodsName);
                ins.InsertColumn("process_id", 5);
                ins.InsertColumn("InQty", Qty);
                ins.InsertColumn("OutQty", 0);
                ins.InsertColumn("CurrQty", Qty);
                ins.InsertColumn("in_date", "getDate()");
                sql = ins.getInsertCommand();
            }
            string z_sql = "update Material_control_process set OutQty = OutQty + " + Qty + " ,CurrQty = CurrQty - " + Qty + " where label = '" + label + "' and process_id = 1";
            List<string> list = new List<string>();
            list.Add(sql);
            list.Add(z_sql);
            new InsertCommandBuilder().ExcutTransaction(list);
        }
        public void Storge(string label, string goodsName, int Qty, int area, string userId)
        {
            InsertData(label, goodsName, Qty, 4, userId);
            SelectCommandBuilder s = new SelectCommandBuilder();
            int count = Convert.ToInt32(s.ExecuteScalar("select count(*) from Material_control_process where label = '" + label + "' and process_id = 4"));
            string sql = string.Empty;
            if (count != 0)
            {
                UpdateCommandBuilder up = new UpdateCommandBuilder();
                sql = "update Material_control_process set InQty = InQty + " + Qty + " ,CurrQty = CurrQty + " + Qty + " where label = '" + label + "' and process_id = 4 ";
            }
            else
            {
                InsertCommandBuilder ins = new InsertCommandBuilder("Material_control_process");
                ins.InsertColumn("label", label);
                ins.InsertColumn("goods_name", goodsName);
                ins.InsertColumn("process_id", 4);
                ins.InsertColumn("InQty", Qty);
                ins.InsertColumn("OutQty", 0);
                ins.InsertColumn("CurrQty", Qty);
                ins.InsertColumn("in_date", "getDate()");
                sql = ins.getInsertCommand();
            }
            string sql1 = "update Material_control_process set OutQty = OutQty + " + Qty + " ,CurrQty = CurrQty - " + Qty + " where label = '" + label + "' and process_id = " + area + " ";
            List<string> list = new List<string>();
            list.Add(sql);
            list.Add(sql1);
            new InsertCommandBuilder().ExcutTransaction(list);
        }
        public void PickArea(string label, string goodsName, int Qty, string userId)
        {
            InsertData(label, goodsName, Qty, 2, userId);
            SelectCommandBuilder s = new SelectCommandBuilder();
            int count = Convert.ToInt32(s.ExecuteScalar("select count(*) from Material_control_process where label = '" + label + "' and process_id = 2"));
            string sql = string.Empty;
            if (count != 0)
            {
                sql = "update Material_control_process set OutQty = InQty + " + Qty + " ,CurrQty = CurrQty + " + Qty + " where label = '" + label + "' and process_id = 2 ";
            }
            else
            {
                InsertCommandBuilder ins = new InsertCommandBuilder("Material_control_process");
                ins.InsertColumn("label", label);
                ins.InsertColumn("goods_name", goodsName);
                ins.InsertColumn("process_id", 2);
                ins.InsertColumn("InQty", Qty);
                ins.InsertColumn("OutQty", 0);
                ins.InsertColumn("CurrQty", Qty);
                ins.InsertColumn("in_date", "getDate()");
                sql = ins.getInsertCommand();
            }
            string sql1 = "update Material_control_process set OutQty = OutQty + " + Qty + " ,CurrQty = CurrQty - " + Qty + " where label = '" + label + "' and process_id = 1 ";
            List<string> list = new List<string>();
            list.Add(sql);
            list.Add(sql1);
            new InsertCommandBuilder().ExcutTransaction(list);
        }
        public void print(string label, string goodsName, int Qty, string userId)
        {
            InsertData(label, goodsName, Qty, 3, userId);
            SelectCommandBuilder s = new SelectCommandBuilder();
            int count = Convert.ToInt32(s.ExecuteScalar("select count(*) from Material_control_process where label = '" + label + "' and process_id = 3"));
            string sql = string.Empty;
            if (count != 0)
            {
                sql = "update Material_control_process set OutQty = InQty + " + Qty + " ,CurrQty = CurrQty + " + Qty + " where label = '" + label + "' and process_id = 3 ";
            }
            else
            {
                InsertCommandBuilder ins = new InsertCommandBuilder("Material_control_process");
                ins.InsertColumn("label", label);
                ins.InsertColumn("goods_name", goodsName);
                ins.InsertColumn("process_id", 3);
                ins.InsertColumn("InQty", Qty);
                ins.InsertColumn("OutQty", 0);
                ins.InsertColumn("CurrQty", Qty);
                ins.InsertColumn("in_date", "getDate()");
                sql = ins.getInsertCommand();
            }
            string sql1 = "update Material_control_process set OutQty = OutQty + " + Qty + " ,CurrQty = CurrQty - " + Qty + " where label = '" + label + "' and process_id = 1 ";
            List<string> list = new List<string>();
            list.Add(sql);
            list.Add(sql1);
            new InsertCommandBuilder().ExcutTransaction(list);
        }

        private static void InsertData(string label, string goodsName, int Qty, int areaId, string userId)
        {
            InsertCommandBuilder ins = new InsertCommandBuilder("Material_control");
            ins.InsertColumn("label", label);
            ins.InsertColumn("goods_name", goodsName);
            ins.InsertColumn("process_id", areaId);
            ins.InsertColumn("qty", Qty);
            ins.InsertColumn("operate_time", "getDate()");
            ins.InsertColumn("operator", userId);
            ins.getInsertCommand();
            ins.ExecuteNonQuery();
        }
        public bool getUser(string UserId)
        {
            SelectCommandBuilder select = new SelectCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "HUDSON_User");
            select.SelectColumn("Count(*)");
            select.ConditionsColumn("UserId", UserId);
            select.getSelectCommand();
            bool flag = Convert.ToInt32(select.ExecuteScalar()) != 0 ? true : false;
            return flag;
        }
        public void stockin(string id, string type_id, string goods_id, int qty, string hwh)
        {
            List<string> sList = new List<string>();
            InsertCommandBuilder insert = new InsertCommandBuilder("pre_str_in_bill");
            insert.InsertColumn("str_in_bill_id", id);
            insert.InsertColumn("str_in_type_id", type_id);
            insert.InsertColumn("operator_date", "getdate()");
            insert.InsertColumn("str_in_date", "getdate");
            insert.InsertColumn("store_id", "12");
            insert.InsertColumn("create_user", ViewState["UserId"]);
            insert.InsertColumn("come_from", "生产入库");
            insert.InsertColumn("is_local", "y");
            insert.InsertColumn("is_state", "N");
            sList.Add(insert.getInsertCommand());
            insert.CommandClear();
            insert = new InsertCommandBuilder("pre_str_in_bill_detail");
            insert.InsertColumn("batch_id", id);
            insert.InsertColumn("str_in_bill_id", id);
            insert.InsertColumn("goods_id", goods_id);
            insert.InsertColumn("qty", qty);
            insert.InsertColumn("hwh", hwh);
            sList.Add(insert.getInsertCommand());
        }
        public string[] getBarCode(string barcode)
        {
            string[] c = barcode.Split(' ');//PS141201001091 PD14112610394 D1496061P-1 48 21 20 1028 入库单号0 计划号1 部番2 箱入数3 整箱数4 尾数5 总数量6
            return c;
        }
    }
}