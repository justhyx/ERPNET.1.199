using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Configuration;
using ERPPlugIn.Class;

namespace ERPPlugIn.MaterialControlManager
{
    public partial class Stock_out_page : System.Web.UI.Page
    {
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
        private string str_bill_id
        {
            set { ViewState["str_bill_id"] = value; }
            get { return ViewState["str_bill_id"].ToString(); }
        }
        public string barcode
        {
            set { ViewState["barcode"] = value; }
            get { return ViewState["barcode"].ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                sList = new List<string>();
                sList.Insert(0, AppendDateTime("货位号???"));
                alertMsg();
                txtInput.BackColor = Color.LightGreen;
                step = 1;
            }
        }
        public string AppendDateTime(string msg)
        {
            string Msg = "";
            Msg = msg += " " + DateTime.Now.ToString("HH:mm:ss");
            return Msg;
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
        public bool getUser(string UserId)
        {
            SelectCommandBuilder select = new SelectCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "HUDSON_User");
            select.SelectColumn("Count(*)");
            select.ConditionsColumn("UserId", UserId);
            select.getSelectCommand();
            bool flag = Convert.ToInt32(select.ExecuteScalar()) != 0 ? true : false;
            return flag;
        }
        public string[] getBarCode(string barcode)
        {
            string[] info = new string[] { };
            if (barcode.Length == 49)
            {
                string goodsName = barcode.Substring(0, 16);
                string Qty = barcode.Substring(16, 14).Trim().Split('.')[0];
                string Lot = barcode.Substring(30, 19);
                string name = goodsName.Substring(0, 8);
                string go = goodsName.Substring(12, 1);
                string customerId = barcode.Substring(30, 8);
                if (go != " ")
                {
                    goodsName = name + go + "-+-" + goodsName.Substring(goodsName.Length - 2, 1);
                }
                else
                {
                    if (customerId.ToUpper().Trim() == "CA03140")
                    {
                        goodsName = name + "-+-" + goodsName.Substring(goodsName.Length - 2, 1);
                    }
                }
                string PDate = Lot.Substring(9, 6);
                string No = Lot.Substring(Lot.Length - 5, 5);
                info = new string[] { goodsName.Trim(), Qty.Trim(), PDate.Trim(), No.Trim(), customerId.Trim() };

            }
            else if (barcode.Length == 57)
            {
                string goodsName = barcode.Substring(0, 16);
                string Qty = barcode.Substring(16, 14).Trim().Split('.')[0];
                string customerId = barcode.Substring(30, 8);
                string PDate = DateTime.Now.Year.ToString().Substring(0, 2) + barcode.Substring(38, 6);
                string Lot = barcode.Substring(44, 5);
                string Machine = barcode.Substring(49, 7);
                string Class = barcode.Substring(56, 1);
                info = new string[] { goodsName.Trim(), Qty.Trim(), PDate.Trim(), Lot.Trim(), customerId.Trim(), Machine.Trim(), Class.Trim() };
            }
            return info;
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
                    case 1:
                        hwh = txtInput.Text.Trim().ToLower().Split(' ')[0];
                        store_id = txtInput.Text.Trim().ToLower().Split(' ')[1];
                        sList.Insert(0, AppendDateTime(hwh));
                        sList.Insert(0, AppendDateTime("OK,条码???"));
                        alertMsg();
                        step += 1;
                        break;
                    case 2:
                        if (txtInput.Text.ToLower() == "end")
                        {
                            sList.Insert(0, AppendDateTime("OK,结束指令，需要操作请重新刷读指令"));
                            alertMsg();
                            step = 0;
                            return;
                        }
                        else if (txtInput.Text.ToLower() == "reenter")
                        {
                            sList.Insert(0, AppendDateTime("OK,货位号???"));
                            step = 1;
                            alertMsg();
                            return;
                        }
                        this.barcode = txtInput.Text;
                        string[] aList = getBarCode(this.barcode);
                        goodsName = aList[0];
                        Qty = int.Parse(aList[1]);
                        string sql = "select count(str_in_bill_id) from pre_str_in_bill where str_in_bill_no = '" + aList[0].Trim() + aList[2].Trim().Substring(2, 6) + Convert.ToInt32(aList[3].Trim()).ToString() + "'";
                        int hasCount = Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(sql));
                        if (hasCount != 0)
                        {
                            sList.Insert(0, AppendDateTime("NG,二维码重复,如需继续写入数据.请确认! 1:写入  2:取消"));
                            step += 1;
                            alertMsg();
                            break;
                        }
                        DoMain(aList);
                        step = 2;
                        sList.Insert(0, AppendDateTime("OK,二维码???"));
                        step = 2;
                        txtInput.Text = string.Empty;
                        alertMsg();
                        break;
                    case 3:
                        if (txtInput.Text == "2")
                        {
                            sList.Insert(0, AppendDateTime("OK,已取消!!!"));
                            sList.Insert(0, AppendDateTime("OK,二维码???"));
                            step = 2;
                            txtInput.Text = string.Empty;
                            alertMsg();
                            return;
                        }
                        else if (txtInput.Text == "1")
                        {
                            string[] aList1 = getBarCode(this.barcode);
                            goodsName = aList1[0];
                            Qty = int.Parse(aList1[1]);
                            DoMain(aList1);
                            sList.Insert(0, AppendDateTime("OK,二维码???"));
                            step = 2;
                            txtInput.Text = string.Empty;
                            alertMsg();
                        }
                        else
                        {
                            sList.Insert(0, AppendDateTime("NG,输入错误!输入提示: 1:写入  2:取消"));
                            step = 3;
                            alertMsg();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                sList.Insert(0, AppendDateTime("NG," + ex.Message + "\r\n请重新输入!"));
                alertMsg();
            }

        }
        private void DoMain(string[] aList)
        {
            List<string> Alist = new List<string>();
            str_bill_id = CommadMethod.getNextId("00000000", "");
            InsertCommandBuilder insert3 = new InsertCommandBuilder("pre_str_in_bill");
            insert3.InsertColumn("str_in_bill_id", str_bill_id);
            insert3.InsertColumn("str_in_type_id", "C");
            insert3.InsertColumn("str_in_bill_no", aList[0].Trim() + aList[2].Trim().Substring(2, 6) + Convert.ToInt32(aList[3].Trim()).ToString());
            insert3.InsertColumn("operator_date", "getdate()");
            insert3.InsertColumn("str_in_date", "getdate()");
            insert3.InsertColumn("store_id", "03");//store_id);//暂时固定为03
            insert3.InsertColumn("create_user", "0000");
            insert3.InsertColumn("operator_id", "0000");
            //insert3.InsertColumn("come_from", aList[1]);
            insert3.InsertColumn("islocal", "y");
            //insert.InsertColumn("is_state", "N");
            Alist.Add(insert3.getInsertCommand());
            InsertCommandBuilder ins = new InsertCommandBuilder("pre_str_in_bill_detail");
            ins.InsertColumn("batch_id", CommadMethod.getNextId("00000000", ""));
            ins.InsertColumn("str_in_bill_id", str_bill_id);
            ins.InsertColumn("goods_id", new SelectCommandBuilder().ExecuteDataTable("select goods_id from goods where goods_name='" + goodsName + "'").Rows[0][0].ToString());
            ins.InsertColumn("qty", Qty);
            ins.InsertColumn("hwh", hwh);
            ins.InsertColumn("pch", aList[2]);
            Alist.Add(ins.getInsertCommand());
            ins.ExcutTransaction(Alist);
            sList.Insert(0, AppendDateTime("部番：" + goodsName + "  数量：" + Qty));
            sList.Insert(0, AppendDateTime("OK,条码???"));
        }
    }
}