using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.Data.SqlClient;
using ERPPlugIn.Class;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ERPPlugIn.MoldManager.M_materailsApply
{
    public partial class M_materails_Apply_Form : System.Web.UI.Page
    {
        public List<AddGoodsInfo> AddList
        {
            get { return ViewState["AddList"] as List<AddGoodsInfo>; }
            set { ViewState["AddList"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AddList = new List<AddGoodsInfo>();
                dgv.Disabled = true;
                btnFind.Enabled = false;
            }
        }

        protected void cbxSpec_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSpec.Checked == true)
            {
                cbxNomal.Checked = false;
                txtModeNo.Enabled = true;
                txtModeNo.Focus();
                btnFind.Enabled = false;
            }
        }

        protected void cbxNomal_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxNomal.Checked == true)
            {
                cbxSpec.Checked = false;
                txtModeNo.Text = string.Empty;
                txtModeNo.Enabled = false;
                txtInternalNo.Focus();
                btnFind.Enabled = true;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtM_name.Text))
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('名称为空')</script>", false);
                txtM_name.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtSpec.Text))
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('规格为空')</script>", false);
                txtSpec.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtCz.Text))
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('材质为空')</script>", false);
                txtCz.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtQty.Text))
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('数量为空')</script>", false);
                txtCz.Focus();
                return;
            }
            AddGoodsInfo info = new AddGoodsInfo() { name = txtM_name.Text.Trim(), spec = txtSpec.Text.Trim(), cz = txtCz.Text.Trim(), qty = int.Parse(txtQty.Text.Trim()) };
            AddList.Add(info);
            gvAddData.DataSource = AddList;
            gvAddData.DataBind();
            txtM_name.Text = string.Empty;
            txtSpec.Text = string.Empty;
            txtCz.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtM_name.Focus();
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            dgv.Disabled = false;
            SelectCommandBuilder select = new SelectCommandBuilder();
            string sql = "SELECT name, spec, texture FROM m_materials";
            if (txtM_name.Text != string.Empty)
            {
                sql += " where name like '" + txtM_name.Text.Trim() + "%'";
            }
            SqlDataReader dr = select.ExecuteReader(sql);
            List<AddGoodsInfo> addinfo = new List<AddGoodsInfo>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    AddGoodsInfo info = new AddGoodsInfo()
                    {
                        name = dr.GetString(0),
                        spec = dr.IsDBNull(1) ? "" : dr.GetString(1),
                        cz = dr.IsDBNull(2) ? "" : dr.GetString(2)
                    };
                    addinfo.Add(info);
                }
            }
            gvShowData.DataSource = addinfo;
            gvShowData.DataBind();
        }

        protected void gvShowData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //----------鼠标移动到每项时颜色交替效果  
                e.Row.Attributes["onmouseover"] = "e=this.style.backgroundColor;this.style.backgroundColor='#FFFF99'";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=e";
                //----------设置悬浮鼠标指针形状为小手"  
                e.Row.Attributes["style"] = "Cursor:hand";
                String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString());
                e.Row.Attributes.Add("onclick", evt);
            }
        }

        protected void gvShowData_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtM_name.Text = (this.gvShowData.SelectedRow.Cells[1].FindControl("Label1") as Label).Text;
            txtSpec.Text = (this.gvShowData.SelectedRow.Cells[2].FindControl("Label2") as Label).Text;
            txtCz.Text = (this.gvShowData.SelectedRow.Cells[3].FindControl("Label3") as Label).Text;
            gvShowData.DataSource = null;
            gvShowData.DataBind();
            dgv.Disabled = true;
            txtQty.Focus();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddList.Count == 0)
                {
                    ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('无数据')</script>", false);
                    return;
                }
                if (string.IsNullOrEmpty(txtInternalNo.Text))
                {
                    ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('输入社内编号')</script>", false);
                    txtInternalNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDate.Text))
                {
                    ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('输入声请日期')</script>", false);
                    txtDate.Focus();
                    return;
                }
                List<string> sqlList = new List<string>();
                InsertCommandBuilder ins = new InsertCommandBuilder("m_materails_apply");
                string applyNo = CommadMethod.getNextId("APN" + DateTime.Now.ToString("yyyyMMdd"), "").Trim();
                ins.InsertColumn("apply_no", applyNo);
                if (!string.IsNullOrEmpty(txtModeNo.Text))
                {
                    ins.InsertColumn("mode_no", txtModeNo.Text);
                }
                ins.InsertColumn("internal_no", txtInternalNo.Text);
                ins.InsertColumn("type", cbxNomal.Checked == true ? 1 : 2);
                ins.InsertColumn("apply_date", "getdate()");
                ins.InsertColumn("apply_by", "0000");
                if (!string.IsNullOrEmpty(txtRemark.Text))
                {
                    ins.InsertColumn("remark", txtRemark.Text);
                }
                ins.InsertColumn("is_confirm", "N");
                for (int i = 0; i < AddList.Count; i++)
                {
                    InsertCommandBuilder insDetail = new InsertCommandBuilder("m_materails_apply_detail");
                    insDetail.InsertColumn("apply_no", applyNo);
                    insDetail.InsertColumn("name", AddList[i].name);
                    insDetail.InsertColumn("texture", AddList[i].cz);
                    insDetail.InsertColumn("spec", AddList[i].spec);
                    insDetail.InsertColumn("qty", AddList[i].qty);
                    insDetail.InsertColumn("is_check", 'N');
                    sqlList.Add(insDetail.getInsertCommand());
                }
                sqlList.Add(ins.getInsertCommand());
                int count = ins.ExcutTransaction(sqlList);
                if (count != 0)
                {
                    ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('保存成功')</script>", false);
                    txtModeNo.Text = string.Empty;
                    txtDate.Text = string.Empty;
                    txtInternalNo.Text = string.Empty;
                    txtRemark.Text = string.Empty;
                    cbxNomal.Checked = false;
                    cbxSpec.Checked = false;
                    AddList.Clear();
                    gvAddData.DataSource = null;
                    gvAddData.DataBind();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('保存失败')</script>", false);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "alert", "<script>alert('" + ex.Message + "')</script>", false);
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == false)
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "error", "<script>alert('" + Resources.Resource.alertxzwj + "!')</script>");
                return;
            }
            string IsXls = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();
            if (IsXls != ".xls")
            {
                Response.Write("<script>alert('" + Resources.Resource.alertxzexcel + "')</script>");
                return;
            }
            cbxSpec.Checked = true;
            string path = Server.MapPath("~/UploadExcel/");
            string strpath = FileUpload1.PostedFile.FileName.ToString();
            //string filename = FileUpload1.FileName;
            FileUpload1.PostedFile.SaveAs(path + FileUpload1.FileName);
            string Filename = path + FileUpload1.FileName;

            FileStream file = new FileStream(Filename, FileMode.Open, FileAccess.Read);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            Sheet sheet = hssfworkbook.GetSheetAt(1);
            DataTable table = new DataTable();
            Row headerRow = sheet.GetRow(4);
            int cellCount = headerRow.LastCellNum;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            int rowCount = sheet.LastRowNum;
            for (int i = (sheet.FirstRowNum + 6); i < sheet.LastRowNum + 1; i++)
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
            this.gvdata.DataSource = table;
            this.gvdata.DataBind();
            File.Delete(Filename);
        }

        protected void btnSure_Click(object sender, EventArgs e)
        {
            //List<AddGoodsInfo> aList = new List<AddGoodsInfo>();
            if (gvdata != null && gvdata.Rows.Count != 0)
            {
                for (int i = 0; i < gvdata.Rows.Count; i++)
                {
                    if (gvdata.Rows[i].Cells[2].Text == "&nbsp;")
                    {
                        continue;
                    }
                    AddGoodsInfo a = new AddGoodsInfo();
                    a.name = gvdata.Rows[i].Cells[2].Text;
                    a.cz = gvdata.Rows[i].Cells[3].Text;
                    a.qty = gvdata.Rows[i].Cells[4].Text == "&nbsp;" ? 0 : int.Parse(gvdata.Rows[i].Cells[4].Text);
                    a.spec = gvdata.Rows[i].Cells[6].Text;
                    AddList.Add(a);
                }
            }
            gvAddData.DataSource = AddList;
            gvAddData.DataBind();
            gvdata.DataSource = null;
            gvdata.DataBind();
        }

        protected void gvAddData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAddData.EditIndex = e.NewEditIndex;
            gvAddData.DataSource = AddList;
            gvAddData.DataBind();
        }

        protected void gvAddData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAddData.EditIndex = -1;
            gvAddData.DataSource = AddList;
            gvAddData.DataBind();
        }

        protected void gvAddData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            AddList[e.RowIndex].name = (gvAddData.Rows[e.RowIndex].Cells[1].FindControl("TextBox1") as TextBox).Text;
            AddList[e.RowIndex].spec = (gvAddData.Rows[e.RowIndex].Cells[2].FindControl("TextBox2") as TextBox).Text;
            AddList[e.RowIndex].cz = (gvAddData.Rows[e.RowIndex].Cells[3].FindControl("TextBox3") as TextBox).Text;
            AddList[e.RowIndex].qty = int.Parse((gvAddData.Rows[e.RowIndex].Cells[4].FindControl("TextBox4") as TextBox).Text);
            gvAddData.EditIndex = -1;
            gvAddData.DataSource = AddList;
            gvAddData.DataBind();
        }
    }
    [Serializable]
    public class AddGoodsInfo
    {
        public string name { get; set; }
        public string spec { get; set; }
        public string cz { get; set; }
        public int qty { get; set; }
    }
}