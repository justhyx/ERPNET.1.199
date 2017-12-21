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
    public partial class Goods_Record : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConXG_ZhuSu"].ConnectionString;
        public string Id { get { return ViewState["Id"].ToString(); } set { ViewState["Id"] = value; } }
        public string NewId { get { return ViewState["NewId"].ToString(); } set { ViewState["NewId"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void btnExcute_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT Version,goods_id
                            FROM goods_tmp
                            WHERE (goods_name = '" + txtgoodsName.Text.Trim().ToUpper() + "' and isNew = 'Y')";
            DataTable dt = new SelectCommandBuilder(constr, "").ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                txtVersion.Text = dt.Rows[0][0].ToString();
                Id = dt.Rows[0][1].ToString();
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Alert", "<script>alert('部番不存在...')</script>", false);
                txtgoodsName.Text = string.Empty;
                txtgoodsName.Focus();
            }
        }

        protected void txtNewVersion_TextChanged(object sender, EventArgs e)
        {
            if (txtVersion.Text.Trim().ToUpper() == txtNewVersion.Text.Trim().ToUpper())
            {
                txtNo.Enabled = true;
                txtNo.Focus();
            }
            else
            {
                txtNo.Enabled = false;
                txtContent.Focus();
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FileUpload1.FileName))
            {
                string doctype = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();
                if (doctype.ToLower() != ".pdf")
                {
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Alert", "<script>alert('上传文档必须是PDF格式!')</script>", false);
                    return;
                }
            }
            NewId = CommadMethod.getNextId("GD" + DateTime.Now.ToString("yyyyMMdd"), "");
            string sql = @"insert into goods_tmp (goods_id, goods_no, goods_name, goods_ename, goods_lname, spec, sccj_id, 
                              goods_unit, pack_unit, szb, min_storage, max_storage, best_storage, is_mx, 
                              is_valid, lb1_id, lb2_id, lb3_id, lb4_id, lb5_id, lb6_id, lb7_id, lb8_id, lb9_id, lb10_id, 
                              dm, py, barcode, yxq, jjfs_id, yzh, km_id, lb11_id, lb12_id, lb13_id, lb14_id, price_id, 
                              szb2, tax_rate, zxbz, exam2, select_code, yjts, rkys, jyfw_id, isother, remark, 
                              sccj_add, Crt_Operator, crt_date, modi_operator, modi_date, CheckFirst, ImportId, 
                              TCBL, StatCode, tag_id, sls_Taxrate, hwh, jfbz, cz, ys, cpdz, skdz, qs, cxzq, mjh, 
                              istm, khdm, sbdm, bysbdm, bzlb, bzxx, jzl, ksscrq, jsscrq, dw, xkh, CostPrice, LrTcxs, 
                              Customs_Goods_id, sk_scale, Customs_Weight, new_price, customs_lab, 仕向地, 
                              外箱箱入数, 内装箱入数, LOT, PO, OPR, 外箱格式文件, 内装格式文件, 尾数格式文件, 
                              xstcbl, tssj, use_days, zzgn, yjsx_id, yzh_yxq, zbz, zczyxq, lb15_id, FT, 
                              Materail_Number, Materail_Name, Materail_Model, Materail_Vender_Color, 
                              Materail_Color, Drying_Temperature, Drying_Time, Fire_Retardant_Grade, Buyer, 
                              Toner_Model, Toner_Buyer, Rohs_Certification, Aircraft, Model_Abrasives, isConfirm, 
                              Version, Model_Type, Semi_Product_Type, Semi_Product_Goods, IsNew,TNumber,ChangeContent ,FileName) select '" + NewId + @"', goods_no, goods_name, goods_ename, goods_lname, spec, sccj_id, 
                              goods_unit, pack_unit, szb, min_storage, max_storage, best_storage, is_mx, 
                              is_valid, lb1_id, lb2_id, lb3_id, lb4_id, lb5_id, lb6_id, lb7_id, lb8_id, lb9_id, lb10_id, 
                              dm, py, barcode, yxq, jjfs_id, yzh, km_id, lb11_id, lb12_id, lb13_id, lb14_id, price_id, 
                              szb2, tax_rate, zxbz, exam2, select_code, yjts, rkys, jyfw_id, isother, remark, 
                              sccj_add, Crt_Operator, getdate(), modi_operator, modi_date, CheckFirst, ImportId, 
                              TCBL, StatCode, tag_id, sls_Taxrate, hwh, jfbz, cz, ys, cpdz, skdz, qs, cxzq, mjh, 
                              istm, khdm, sbdm, bysbdm, bzlb, bzxx, jzl, ksscrq, jsscrq, dw, xkh, CostPrice, LrTcxs, 
                              Customs_Goods_id, sk_scale, Customs_Weight, new_price, customs_lab, 仕向地, 
                              外箱箱入数, 内装箱入数, LOT, PO, OPR, 外箱格式文件, 内装格式文件, 尾数格式文件, 
                              xstcbl, tssj, use_days, zzgn, yjsx_id, yzh_yxq, zbz, zczyxq, lb15_id, FT, 
                              Materail_Number, Materail_Name, Materail_Model, Materail_Vender_Color, 
                              Materail_Color, Drying_Temperature, Drying_Time, Fire_Retardant_Grade, Buyer, 
                              Toner_Model, Toner_Buyer, Rohs_Certification, Aircraft, Model_Abrasives, isConfirm, 
                              '" + txtNewVersion.Text.Trim().ToUpper() + "', Model_Type, Semi_Product_Type, Semi_Product_Goods, 'Y','" + txtNo.Text.Trim() + "','" + txtContent.Text.Trim() + "','" + NewId + ".pdf'" + " from goods_tmp where goods_id = '" + Id + "'";
            string Usql = "update goods_tmp set isNew = 'N' where goods_id = '" + Id + "'";
            string Isql = "insert into Mold_management_accounting (id,Mode_type_id,currency,goods_no)values('" + NewId + "',1,'N','" + txtgoodsName.Text.Trim() + txtNewVersion.Text.Trim().ToUpper() + "')";
            List<string> sqlList = new List<string>();
            sqlList.Add(sql);
            sqlList.Add(Usql);
            sqlList.Add(Isql);
            int count = new InsertCommandBuilder(constr, "").ExcutTransaction(sqlList);
            if (count != 0)
            {
                string path = Server.MapPath("~/UploadFile/");
                string strpath = FileUpload1.PostedFile.FileName.ToString();
                FileUpload1.PostedFile.SaveAs(path + NewId + ".pdf");
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Alert", "<script>alert('操作已完成!')</script>", false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Alert", "<script>alert('操作不成功!')</script>", false);
            }
        }
    }
}