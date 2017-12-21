using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Configuration;
using System.Drawing;
using ERPPlugIn.Class;

namespace ERPPlugIn.GoodsManager
{
    public partial class Goods_Input : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindCustomers();
            }
        }

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnUpload.Text == "预 览")
                {
                    #region  将上传的Excel数据显示在GridView中
                    if (FileUpload1.HasFile == false)
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "error", "<script>alert('请您选择Excel文件!')</script>");
                        //Response.Write("<script>alert('请您选择Excel文件')</script> ");
                        return;//当无文件时,返回
                    }
                    string IsXls = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();
                    if (IsXls != ".xls")
                    {
                        Response.Write("<script>alert('只可以选择Excel文件')</script>");
                        return;//当选择的不是Excel文件时,返回
                    }
                    btnUpload.Text = "确认上传";
                    Button1.Text = "取 消";
                    string path = Server.MapPath("~/UploadExcel/");
                    string strpath = FileUpload1.PostedFile.FileName.ToString();
                    //string filename = FileUpload1.FileName;
                    FileUpload1.PostedFile.SaveAs(path + FileUpload1.FileName);
                    string Filename = path + FileUpload1.FileName;

                    FileStream file = new FileStream(Filename, FileMode.Open, FileAccess.Read);
                    HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                    Sheet sheet = hssfworkbook.GetSheetAt(0);
                    DataTable table = new DataTable();
                    Row headerRow = sheet.GetRow(1);
                    int cellCount = headerRow.LastCellNum;
                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                        table.Columns.Add(column);
                    }
                    int rowCount = sheet.LastRowNum;
                    for (int i = (sheet.FirstRowNum + 2); i < sheet.LastRowNum + 1; i++)
                    {
                        Row row = sheet.GetRow(i);
                        DataRow dataRow = table.NewRow();
                        if (string.IsNullOrEmpty(row.GetCell(0).ToString()))
                        {
                            continue;
                        }
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
                    ViewState["value"] = validate();
                    #endregion
                }
                else
                {
                    #region 确认上传数据
                    if (Convert.ToInt32(ViewState["value"]) != 0)
                    {
                        Response.Write("<script>alert('系统出现以下错误:\\n验证未通过,请修正背景色为红色的数据后再试!')</script>");
                        return;
                    }
                    if (gvExcel.Rows.Count == 0)
                    {
                        Response.Write("<script>alert('数据为空,请重新确认后重试!')</script>");
                        return;
                    }
                    if (ddlCustomer.SelectedItem.Value == "0")
                    {
                        Response.Write("<script>alert('请选择客户名称!')</script>");
                        return;
                    }
                    List<string> sList = getSQLList(this.gvExcel);
                    InsertCommandBuilder insert = new InsertCommandBuilder(constr, "");
                    int count = insert.ExcutTransaction(sList);
                    if (count != 0)
                    {
                        Response.Write("<script>alert('上传成功')</script>");
                        this.gvExcel.DataSource = null;
                        this.gvExcel.DataBind();
                        btnUpload.Text = "预 览";
                    }
                    else
                    {
                        Response.Write("<script>alert('上传失败')</script>");
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('系统出现以下错误:\\n" + ex.Message + "!')</script>");
            }
        }
        protected List<string> getSQLList(GridView dgv)
        {
            int index = 0;
            List<string> sList = new List<string>();
            try
            {
                InsertCommandBuilder insert = new InsertCommandBuilder(constr, "Goods_Up");
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    string goods_name = dgv.Rows[i].Cells[0].Text.ToString().ToUpper();
                    index = i;
                    SelectCommandBuilder s = new SelectCommandBuilder(constr, "goods_tmp");
                    s.SelectColumn("Count(goods_id)");
                    s.ConditionsColumn("goods_name", goods_name.Trim());
                    s.getSelectCommand();
                    int count = Convert.ToInt32(s.ExecuteScalar());
                    if (count == 0)
                    {
                        sList.Add(InsertData(
                            dgv.Rows[i].Cells[0].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[1].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[2].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[3].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[4].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[5].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[6].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[7].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[8].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[9].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[10].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[11].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[12].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[13].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[14].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[15].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[16].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[17].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[18].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[19].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[20].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[21].Text.ToString().ToUpper().IndexOf('%') != -1 ? (Convert.ToDecimal(dgv.Rows[i].Cells[21].Text.ToString().ToUpper().Split('%')[0]) / 100).ToString() : (Convert.ToDecimal(dgv.Rows[i].Cells[21].Text.ToString().ToUpper()) / 100).ToString(),
                            dgv.Rows[i].Cells[22].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[23].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[24].Text.ToString().ToUpper(),
                            dgv.Rows[i].Cells[25].Text.ToString().ToUpper(),
                            ddlCustomer.SelectedItem.Value
                            ));
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('系统出现以下错误:" + ex.Message + "!')</script>");
                gvExcel.Rows[index].BackColor = System.Drawing.Color.Red;
            }
            return sList;
        }
        protected string InsertData(string goods_name, string version, string mjh, string goods_ename, string dw, string Aircraft, string Materail_Number, string Materail_Name, string Materail_Model, string ys, string Materail_Vender_Color, string Materail_Color, string Fire_Retardant_Grade, string Buyer, string Toner_Model, string Toner_Buyer, string Rohs_Certification, string cpdz, string skdz, string Drying_Temperature, string Drying_Time, string sk_scale, string cxzq, string qs, string modeType, string remark, string khdm)
        {
            string id = CommadMethod.getNextId("GD" + DateTime.Now.ToString("yyyyMMdd"), "");
            InsertCommandBuilder ins = new InsertCommandBuilder(constr, "goods_tmp");
            ins.InsertColumn("goods_id", id.ToString());
            ins.InsertColumn("goods_no", goods_name);
            ins.InsertColumn("goods_name", goods_name);
            ins.InsertColumn("IsNew", "Y");
            if (!"&NBSP;".Equals(goods_ename))
            {
                ins.InsertColumn("goods_ename", goods_ename);
            }
            if (!"&NBSP;".Equals(version))
            {
                ins.InsertColumn("Version", version);
            }
            if (!"&NBSP;".Equals(mjh))
            {
                ins.InsertColumn("mjh", mjh);
            }
            if (!"&NBSP;".Equals(dw))
            {
                ins.InsertColumn("dw", dw);
            }
            if (!"&NBSP;".Equals(qs))
            {
                ins.InsertColumn("qs", qs);
            }
            if (!"&NBSP;".Equals(Materail_Number))
            {
                ins.InsertColumn("Materail_Number", Materail_Number);
            }
            if (!"&NBSP;".Equals(Materail_Name))
            {
                ins.InsertColumn("Materail_Name", Materail_Name);
            }
            if (!"&NBSP;".Equals(ys))
            {
                ins.InsertColumn("ys", ys);
            }
            if (!"&NBSP;".Equals(Materail_Model))
            {
                ins.InsertColumn("Materail_Model", Materail_Model);
            }
            if (!"&NBSP;".Equals(Materail_Vender_Color))
            {
                ins.InsertColumn("Materail_Vender_Color", Materail_Vender_Color);
            }
            if (!"&NBSP;".Equals(Materail_Color))
            {
                ins.InsertColumn("Materail_Color", Materail_Color);
            }
            if (!"&NBSP;".Equals(cpdz))
            {
                ins.InsertColumn("cpdz", cpdz);
            }
            if (!"&NBSP;".Equals(skdz))
            {
                ins.InsertColumn("skdz", skdz);
            }
            if (!"&NBSP;".Equals(Drying_Temperature))
            {
                ins.InsertColumn("Drying_Temperature", Drying_Temperature);
            }
            if (!"&NBSP;".Equals(Drying_Time))
            {
                ins.InsertColumn("Drying_Time", Drying_Time);
            }
            if (!"&NBSP;".Equals(sk_scale))
            {
                ins.InsertColumn("sk_scale", sk_scale);
            }
            if (!"&NBSP;".Equals(cxzq))
            {
                ins.InsertColumn("cxzq", cxzq);
            }
            if (!"&NBSP;".Equals(khdm))
            {
                ins.InsertColumn("khdm", khdm);
            }
            if (!"&NBSP;".Equals(remark))
            {
                ins.InsertColumn("remark", remark);
            }
            if (!"&NBSP;".Equals(modeType))
            {
                if (modeType == "贩卖")
                {
                    ins.InsertColumn("Model_Type", 1);
                }
                else if (modeType == "量产")
                {
                    ins.InsertColumn("Model_Type", 0);
                }
            }
            if (!"&NBSP;".Equals(Fire_Retardant_Grade))
            {
                ins.InsertColumn("Fire_Retardant_Grade", Fire_Retardant_Grade);
            }
            if (!"&NBSP;".Equals(Buyer))
            {
                ins.InsertColumn("Buyer", Buyer);
            }
            if (!"&NBSP;".Equals(Toner_Model))
            {
                ins.InsertColumn("Toner_Model", Toner_Model);
            }
            if (!"&NBSP;".Equals(Toner_Buyer))
            {
                ins.InsertColumn("Toner_Buyer", Toner_Buyer);
            }
            if (!"&NBSP;".Equals(Aircraft))
            {
                ins.InsertColumn("Aircraft", Aircraft);
            }
            if (!"&NBSP;".Equals(Rohs_Certification))
            {
                ins.InsertColumn("Rohs_Certification", Rohs_Certification);
            }
            return ins.getInsertCommand();
        }
        protected string UpdateData(string goods_name, string mjh, string goods_ename, string dw, string Aircraft, string Materail_Number, string Materail_Name, string Materail_Model, string ys, string Materail_Vender_Color, string Materail_Color, string Fire_Retardant_Grade, string Buyer, string Toner_Model, string Toner_Buyer, string Rohs_Certification, string cpdz, string skdz, string Drying_Temperature, string Drying_Time, string sk_scale, string cxzq, string qs, string modeType, string remark, string khdm)
        {
            UpdateCommandBuilder u = new UpdateCommandBuilder(constr, "goods_tmp");
            if (!"&NBSP;".Equals(mjh))
            {
                u.UpdateColumn("mjh", mjh);
            }
            if (!"&NBSP;".Equals(goods_ename))
            {
                u.UpdateColumn("goods_ename", goods_ename);
            }
            if (!"&NBSP;".Equals(dw))
            {
                u.UpdateColumn("dw", dw);
            }
            if (!"&NBSP;".Equals(qs))
            {
                u.UpdateColumn("qs", qs);
            }
            if (!"&NBSP;".Equals(Materail_Number))
            {
                u.UpdateColumn("Materail_Number", Materail_Number);
            }
            if (!"&NBSP;".Equals(Materail_Name))
            {
                u.UpdateColumn("Materail_Name", Materail_Name);
            }
            if (!"&NBSP;".Equals(ys))
            {
                u.UpdateColumn("ys", ys);
            }
            if (!"&NBSP;".Equals(Materail_Model))
            {
                u.UpdateColumn("Materail_Model", Materail_Model);
            }
            if (!"&NBSP;".Equals(Materail_Vender_Color))
            {
                u.UpdateColumn("Materail_Vender_Color", Materail_Vender_Color);
            }
            if (!"&NBSP;".Equals(Materail_Color))
            {
                u.UpdateColumn("Materail_Color", Materail_Color);
            }
            if (!"&NBSP;".Equals(cpdz))
            {
                u.UpdateColumn("cpdz", cpdz);
            }
            if (!"&NBSP;".Equals(skdz))
            {
                u.UpdateColumn("skdz", skdz);
            }
            if (!"&NBSP;".Equals(Drying_Temperature))
            {
                u.UpdateColumn("Drying_Temperature", Drying_Temperature);
            }
            if (!"&NBSP;".Equals(Drying_Time))
            {
                u.UpdateColumn("Drying_Time", Drying_Time);
            }
            if (!"&NBSP;".Equals(sk_scale))
            {
                u.UpdateColumn("sk_scale", sk_scale);
            }
            if (!"&NBSP;".Equals(cxzq))
            {
                u.UpdateColumn("cxzq", cxzq);
            }
            if (!"&NBSP;".Equals(khdm))
            {
                u.UpdateColumn("khdm", khdm);
            }
            if (!"&NBSP;".Equals(remark))
            {
                u.UpdateColumn("remark", remark);
            }
            if (!"&NBSP;".Equals(Fire_Retardant_Grade))
            {
                u.UpdateColumn("Fire_Retardant_Grade", Fire_Retardant_Grade);
            }
            if (!"&NBSP;".Equals(Buyer))
            {
                u.UpdateColumn("Buyer", Buyer);
            }
            if (!"&NBSP;".Equals(Toner_Model))
            {
                u.UpdateColumn("Toner_Model", Toner_Model);
            }
            if (!"&NBSP;".Equals(Toner_Buyer))
            {
                u.UpdateColumn("Toner_Buyer", Toner_Buyer);
            }
            if (!"&NBSP;".Equals(Aircraft))
            {
                u.UpdateColumn("Aircraft", Aircraft);
            }
            if (!"&NBSP;".Equals(Rohs_Certification))
            {
                u.UpdateColumn("Rohs_Certification", Rohs_Certification);
            }
            if (!"&NBSP;".Equals(modeType))
            {
                if (modeType == "贩卖")
                {
                    u.UpdateColumn("Model_Type", 1);
                }
                else if (modeType == "量产")
                {
                    u.UpdateColumn("Model_Type", 0);
                }
            }
            u.ConditionsColumn("goods_name", goods_name);
            return u.getUpdateCommand();
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (Button1.Text == "取 消")
            {
                btnUpload.Text = "预 览";
                this.gvExcel.DataSource = null;
                this.gvExcel.DataBind();
                Button1.Text = "返 回";
            }
            else
            {
                Button1.Text = "取 消";
                Response.Redirect("Goods_List.aspx");
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
        public void DownloadFile(string path, string name)
        {
            try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(name));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度
                Response.AddHeader("Content-Length", file.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/ms-excel";
                // 把文件流发送到客户端
                Response.WriteFile(file.FullName);
                // 停止页面的执行
                //Response.End();     
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('系统出现以下错误://n" + ex.Message + "!//n请尽快与管理员联系.')</script>");
            }
        }

        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            string strPath = Server.MapPath("/") + "DownLoadFile//部品上传模板.xls";
            DownloadFile(strPath, "部品上传模板.xls");
        }

        protected int validate()
        {
            int reslut = 0;
            double value = 0;
            for (int i = 0; i < gvExcel.Rows.Count; i++)
            {
                //if (!double.TryParse(gvExcel.Rows[i].Cells[1].Text.ToString(), out value))
                //{
                //    gvExcel.Rows[i].Cells[1].BackColor = Color.Red;
                //    reslut++;
                //}
                if (!double.TryParse(gvExcel.Rows[i].Cells[17].Text.ToString(), out value))
                {
                    gvExcel.Rows[i].Cells[17].BackColor = Color.Red;
                    reslut++;
                }
                if (!double.TryParse(gvExcel.Rows[i].Cells[18].Text.ToString(), out value))
                {
                    gvExcel.Rows[i].Cells[18].BackColor = Color.Red;
                    reslut++;
                }
                if (!double.TryParse(gvExcel.Rows[i].Cells[19].Text.ToString(), out value))
                {
                    gvExcel.Rows[i].Cells[19].BackColor = Color.Red;
                    reslut++;
                }
                if (!double.TryParse(gvExcel.Rows[i].Cells[20].Text.ToString(), out value))
                {
                    gvExcel.Rows[i].Cells[20].BackColor = Color.Red;
                    reslut++;
                }
                if (!double.TryParse(gvExcel.Rows[i].Cells[21].Text.ToString().ToUpper().IndexOf('%') != -1 ? ((gvExcel.Rows[i].Cells[21].Text.ToString().ToUpper().Split('%')[0])).ToString() : gvExcel.Rows[i].Cells[21].Text.ToString().ToUpper(), out value))
                {
                    gvExcel.Rows[i].Cells[21].BackColor = Color.Red;
                    reslut++;
                }
                if (!double.TryParse(gvExcel.Rows[i].Cells[22].Text.ToString(), out value))
                {
                    gvExcel.Rows[i].Cells[22].BackColor = Color.Red;
                    reslut++;
                }
                if (!double.TryParse(gvExcel.Rows[i].Cells[23].Text.ToString(), out value))
                {
                    gvExcel.Rows[i].Cells[23].BackColor = Color.Red;
                    reslut++;
                }
                if (gvExcel.Rows[i].Cells[24].Text.ToString() != "量产" && gvExcel.Rows[i].Cells[24].Text.ToString() != "贩卖")
                {
                    gvExcel.Rows[i].Cells[24].BackColor = Color.Red;
                    reslut++;
                }
            }
            if (reslut == 0)
            {
                Response.Write("<script>alert('验证通过')</script>");
            }
            else
            {
                Response.Write("<script>alert('验证未通过,请修正背景色为红色的数据后再试!')</script>");
            }
            return reslut;
        }

    }
}