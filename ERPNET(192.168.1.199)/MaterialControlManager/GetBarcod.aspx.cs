using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;

namespace ERPPlugIn.MaterialControlManager
{
    public partial class GetBarcod : System.Web.UI.Page
    {
        private List<string> sList
        {
            set { ViewState["sList"] = value; }
            get { return ViewState["sList"] as List<string>; }
        }
        public int step { set { ViewState["a"] = value; } get { return Convert.ToInt32(ViewState["a"]); } }
        public string barcode { set { ViewState["barcode"] = value; } get { return ViewState["barcode"].ToString(); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                sList = new List<string>();
                step = 0;
                barcode = string.Empty;
                sList.Insert(0, AppendDateTime("OK,二维码???"));
                alertMsg();
            }
        }

        protected void sr_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInput.Text.Equals(string.Empty))
                {
                    txtInput.Focus();
                    return;
                }
                switch (step)
                {
                    case 0:
                        string fullB = txtInput.Text.Trim();
                        //if (fullB.Length != 49)
                        //{
                        //    sList.Insert(0, AppendDateTime("NG,二维码格式错误!"));
                        //    alertMsg();
                        //    return;
                        //}
                        this.barcode = fullB;
                        string sql = " select count(*) from BarCodeRecords where Barcode = '" + fullB + "'";
                        if (Convert.ToInt32(new SelectCommandBuilder().ExecuteScalar(sql)) != 0)
                        {
                            sList.Insert(0, AppendDateTime("NG,二维码重复,如需继续写入数据.请确认! 1:写入  2:取消"));
                            step += 1;
                            alertMsg();
                            break;
                        }
                        string[] barcode = getBarCode(fullB);
                        int count = 0;
                        if (fullB.Length == 49)
                        {
                            count = excutInsert(fullB, barcode[0], int.Parse(barcode[1]), barcode[2], barcode[3], barcode[4]);
                        }
                        else if (fullB.Length == 57)
                        {
                            count = excutInsert(fullB, barcode[0], int.Parse(barcode[1]), barcode[2], barcode[3], barcode[4], barcode[5], barcode[6]);
                        }

                        if (count != 0)
                        {
                            sList.Insert(0, AppendDateTime("部番:" + barcode[0] + "数量:" + barcode[1]));
                            sList.Insert(0, AppendDateTime("OK,二维码???"));
                        }
                        else
                        {
                            sList.Insert(0, AppendDateTime("NG,写入失败!!!"));
                        }
                        alertMsg();
                        break;
                    case 1:
                        if (txtInput.Text == "2")
                        {
                            sList.Insert(0, AppendDateTime("OK,已取消!!!"));
                            sList.Insert(0, AppendDateTime("OK,二维码???"));
                            step = 0;
                            alertMsg();
                            return;
                        }
                        else if (txtInput.Text == "1")
                        {
                            string[] barcode1 = getBarCode(this.barcode);
                            int count1 = 0;
                            if (this.barcode.Length == 49)
                            {
                                count1 = excutInsert(this.barcode, barcode1[0], int.Parse(barcode1[1]), barcode1[2], barcode1[3], barcode1[4]);
                            }
                            else if (this.barcode.Length == 57)
                            {
                                count1 = excutInsert(this.barcode, barcode1[0], int.Parse(barcode1[1]), barcode1[2], barcode1[3], barcode1[4], barcode1[5], barcode1[6]);
                            }
                            if (count1 != 0)
                            {
                                sList.Insert(0, AppendDateTime("部番:" + barcode1[0] + "数量:" + barcode1[1]));
                                sList.Insert(0, AppendDateTime("OK,二维码???"));
                                step = 0;
                            }
                            else
                            {
                                sList.Insert(0, AppendDateTime("NG,写入失败!!!"));
                            }
                            alertMsg();
                        }
                        else
                        {
                            sList.Insert(0, AppendDateTime("NG,输入错误!输入提示: 1:写入  2:取消"));
                            step = 1;
                            alertMsg();
                        }

                        break;
                    case 2:
                        break;
                }

            }
            catch (Exception ex)
            {
                sList.Insert(0, AppendDateTime(ex.Message));
                alertMsg();
            }

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
                string PDate = barcode.Substring(38, 6);
                string Lot = barcode.Substring(44, 5);
                string Machine = barcode.Substring(49, 7);
                string Class = barcode.Substring(56, 1);
                info = new string[] { goodsName.Trim(), Qty.Trim(), PDate.Trim(), Lot.Trim(), customerId.Trim(), Machine.Trim(), Class.Trim() };
            }
            return info;
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
        public int excutInsert(string Barcode, string goods_name, int qty, string pdate, string sn, string customerID)
        {
            InsertCommandBuilder insert = new InsertCommandBuilder("BarCodeRecords");
            insert.InsertColumn("Barcode", Barcode);
            insert.InsertColumn("goods_name", goods_name);
            insert.InsertColumn("qty", qty);
            insert.InsertColumn("pdate", pdate);
            insert.InsertColumn("sn", sn);
            insert.InsertColumn("customerId", customerID);
            insert.InsertColumn("create_date", "getdate()");
            insert.getInsertCommand();
            return insert.ExecuteNonQuery();
        }
        public int excutInsert(string Barcode, string goods_name, int qty, string pdate, string sn, string customerID, string machine, string classid)
        {
            InsertCommandBuilder insert = new InsertCommandBuilder("BarCodeRecords");
            insert.InsertColumn("Barcode", Barcode);
            insert.InsertColumn("goods_name", goods_name);
            insert.InsertColumn("qty", qty);
            insert.InsertColumn("pdate", pdate);
            insert.InsertColumn("sn", sn);
            insert.InsertColumn("customerId", customerID);
            insert.InsertColumn("equip", machine);
            insert.InsertColumn("classid", classid);
            insert.InsertColumn("create_date", "getdate()");
            insert.getInsertCommand();
            return insert.ExecuteNonQuery();
        }
    }
}