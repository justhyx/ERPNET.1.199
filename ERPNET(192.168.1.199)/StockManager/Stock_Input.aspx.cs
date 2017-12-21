using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Drawing;
using ERPPlugIn.Class;

namespace ERPPlugIn.StockManager
{
    public partial class Stock_Input : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCustomer();

            }
        }

        private void BindCustomer()
        {
            DataTable d = getCustomers();
            ddlCustomers.Items.Clear();
            ddlCustomers.Items.Add(new ListItem(Resources.Resource.qxz, "0"));
            for (int i = 0; i < d.Rows.Count; i++)
            {
                ddlCustomers.Items.Add(new ListItem(d.Rows[i]["customer_aname"].ToString(), d.Rows[i]["customer_id"].ToString()));
            }
            ddlCustomers.DataTextField = "customer_aname";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
        protected DataTable getCustomers()
        {
            SelectCommandBuilder s = new SelectCommandBuilder("customer");
            s.SelectColumn("customer_id");
            s.SelectColumn("customer_aname");
            s.ConditionsColumn("1", "1");
            s.getSelectCommand();
            return s.ExecuteDataTable();
        }

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            if (btnUpload.Text == Resources.Resource.yl)
            {
                #region  将上传的Excel数据显示在GridView中
                if (FileUpload1.HasFile == false)
                {
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "error", "<script>alert('" + Resources.Resource.alertxzwj + "!')</script>");
                    //Response.Write("<script>alert('请您选择Excel文件')</script> ");
                    return;//当无文件时,返回
                }
                string IsXls = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();
                if (IsXls != ".xls")
                {
                    Response.Write("<script>alert('" + Resources.Resource.alertxzexcel + "')</script>");
                    return;//当选择的不是Excel文件时,返回
                }

                btnUpload.Text = Resources.Resource.qr;
                Button1.Text = Resources.Resource.qx;
                string path = Server.MapPath("~/UploadExcel/");
                string strpath = FileUpload1.PostedFile.FileName.ToString();
                //string filename = FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(path + FileUpload1.FileName);
                string Filename = path + FileUpload1.FileName;

                FileStream file = new FileStream(Filename, FileMode.Open, FileAccess.Read);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                Sheet sheet = hssfworkbook.GetSheetAt(0);
                DataTable table = new DataTable();
                Row headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                }
                int rowCount = sheet.LastRowNum;
                for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum + 1; i++)
                {
                    Row row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }
                    table.Rows.Add(dataRow);
                }
                hssfworkbook = null;
                sheet = null;
                this.gvExcel.DataSource = table;
                this.gvExcel.DataBind();
                File.Delete(Filename);
                #endregion
            }
            else
            {
                #region 确认上传数据
                if (gvExcel.Rows.Count != 0 && checkInput())
                {
                    List<string> sList = getSQLList(this.gvExcel);
                    InsertCommandBuilder insert = new InsertCommandBuilder();
                    int count = insert.ExcutTransaction(sList);
                    if (count != 0)
                    {
                        Response.Write("<script>alert('" + Resources.Resource.alterOk + "')</script>");
                        this.gvExcel.DataSource = null;
                        this.gvExcel.DataBind();
                        btnUpload.Text = Resources.Resource.yl;
                    }
                }
                #endregion

            }

        }
        protected List<string> getSQLList(GridView dgv)
        {
            List<string> sList = new List<string>();
            InsertCommandBuilder insert = new InsertCommandBuilder("Goods_Up");
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                string goods_name = dgv.Rows[i].Cells[0].Text.ToString();
                string prapareId = txtPrapareID.Text.Trim().ToUpper();
                if (!checkCustomer(goods_name))
                {
                    int j = i + 1;
                    dgv.Rows[i].BackColor = Color.Pink;
                    Response.Write("<script>alert('第" + j + "行客户代码错误')</script>");
                    btnUpload.Text = Resources.Resource.yl;
                    sList.Clear();
                    break;

                }
                else if (checkIsUpload(goods_name, prapareId))
                {
                    int j = i + 1;
                    dgv.Rows[i].BackColor = Color.Pink;
                    Response.Write("<script>alert('第" + j + "行已上传至数据库，请检查数据是否重复!')</script>");
                    btnUpload.Text = Resources.Resource.yl;
                    sList.Clear();
                    break;
                }
                else
                {
                    insert.InsertColumn("goods_name", goods_name);
                    insert.InsertColumn("qty", dgv.Rows[i].Cells[1].Text.ToString());
                    insert.InsertColumn("khdm", ddlCustomers.SelectedItem.Value);
                    insert.InsertColumn("Prepare_goods_Id", prapareId);
                    insert.InsertColumn("delivery_date", txtdelivery_date.Text);
                    string sql = insert.getInsertCommand();
                    insert.CommandClear();
                    sList.Add(sql);
                }
            }
            return sList;
        }
        protected bool checkCustomer(string goods_name)
        {
            bool flag = false;
            SelectCommandBuilder s = new SelectCommandBuilder("goods");
            s.SelectColumn("khdm");
            s.ConditionsColumn("goods_name", goods_name);
            s.getSelectCommand();
            DataTable d = s.ExecuteDataTable();
            if (d != null && d.Rows.Count != 0)
            {
                if (d.Rows[0][0].ToString() == ddlCustomers.SelectedItem.Value.Trim())
                {
                    flag = true;
                }
            }
            return flag;

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (Button1.Text == Resources.Resource.qx)
            {
                btnUpload.Text = Resources.Resource.yl;
                this.gvExcel.DataSource = null;
                this.gvExcel.DataBind();
                Button1.Text = Resources.Resource.fh;
            }
            else
            {
                Button1.Text = Resources.Resource.qx;
                Response.Redirect("Stock_Output.aspx");
            }

        }
        public bool checkIsUpload(string goods_name, string prapareId)
        {
            bool flag = false;
            SelectCommandBuilder s = new SelectCommandBuilder("Goods_Up");
            s.SelectColumn("count(goods_name)");
            s.ConditionsColumn("Prepare_goods_Id", prapareId);
            s.ConditionsColumn("goods_name", goods_name);
            s.getSelectCommand();
            int count = Convert.ToInt32(s.ExecuteScalar());
            if (count != 0)
            {
                flag = true;
            }
            return flag;
        }
        public bool checkInput()
        {
            bool flag = true;
            if (ddlCustomers.SelectedItem.Value.ToString() == "0")
            {
                flag = false;
                Response.Write("<script>alert('请选择客户!')</script>");
                ddlCustomers.Focus();
            }
            else if (string.IsNullOrEmpty(txtdelivery_date.Text))
            {
                flag = false;
                Response.Write("<script>alert('请选择送货日期!')</script>");
                txtdelivery_date.Focus();
            }
            else if (string.IsNullOrEmpty(txtPrapareID.Text))
            {
                flag = false;
                Response.Write("<script>alert('请输入备料指示号!')</script>");
                txtPrapareID.Focus();
            }

            return flag;
        }

    }
}