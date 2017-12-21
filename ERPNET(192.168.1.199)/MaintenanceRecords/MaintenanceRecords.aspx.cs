using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;
using System.IO;
using ERPPlugIn.Class;

namespace ERPPlugIn.MaintenanceRecords
{
    public partial class MaintenanceRecords : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString;
        public string Id { get { return ViewState["id"].ToString(); } set { ViewState["id"] = value; } }
        public string imgId { get { return ViewState["imgId"].ToString(); } set { ViewState["imgId"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getPerson();
            }
        }
        public void getPerson()
        {
            SelectCommandBuilder s = new SelectCommandBuilder(constr, "HUDSON_User");
            s.SelectColumn("UserName");
            s.ConditionsColumn("user_l", "19");
            s.getSelectCommand();
            DataTable dt = s.ExecuteDataTable();
            cblPerson.Items.Clear();
            if (dt != null && dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cblPerson.Items.Add(new ListItem(dt.Rows[i]["UserName"].ToString()));
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (checkInput() != true)
            {
                return;
            }
            Id = CommadMethod.getNextId("Fix" + DateTime.Now.ToString("yyyyMMdd"), "").Trim();
            string docType = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();
            imgId = Id + docType;
            InsertCommandBuilder ins = new InsertCommandBuilder("Repair_records");
            ins.InsertColumn("id", Id);
            ins.InsertColumn("eqp_no", tags.Text);
            ins.InsertColumn("rely_date", txtDate.Text);
            ins.InsertColumn("repair_time", Convert.ToDecimal(txtHours.Text));
            ins.InsertColumn("repair_record", txtRecords.Text);
            ins.InsertColumn("type", ddlType.SelectedItem.Text);
            ins.InsertColumn("rely_dept", ddlDept.SelectedItem.Text);
            string person = string.Empty;
            for (int i = 0; i < cblPerson.Items.Count; i++)
            {
                if (cblPerson.Items[i].Selected == true)
                {
                    person += cblPerson.Items[i].Text + ",";
                }
            }
            if (!string.IsNullOrEmpty(txtPerson.Text))
            {
                person += txtPerson.Text;
            }
            else
            {
                person = person.Substring(0, person.Length - 1);
            }
            ins.InsertColumn("repairman", person);
            ins.InsertColumn("create_date", "getdate()");
            ins.InsertColumn("doc_url", imgId);
            ins.getInsertCommand();
            int count = ins.ExecuteNonQuery();
            if (count != 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('添加成功')</script>", false);
                uploadImg();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('添加失败')</script>", false);
            }


        }
        public bool checkInput()
        {
            bool flag = true;
            if (ddlType.SelectedItem.Value == "0")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('请选择内容')</script>", false);
                ddlType.Focus();
                flag = false;
            }
            else if (string.IsNullOrEmpty(tags.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('编号为空')</script>", false);
                tags.Focus();
                flag = false;
            }
            else if (string.IsNullOrEmpty(txtDate.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('依赖时间为空')</script>", false);
                txtDate.Focus();
                flag = false;
            }
            else if (string.IsNullOrEmpty(txtHours.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('维修时间为空')</script>", false);
                txtHours.Focus();
                flag = false;
            }
            else if (string.IsNullOrEmpty(txtRecords.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('处置内容为空')</script>", false);
                txtRecords.Focus();
                flag = false;
            }
            else if (!FileUpload1.HasFile)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('请选择上传图片')</script>", false);
                FileUpload1.Focus();
                flag = false;
            }
            return flag;
        }
        public bool uploadImg()
        {
            bool flag = false;
            string path = Server.MapPath("~/UploadPics/");
            FileUpload1.PostedFile.SaveAs(path + imgId);
            flag = true;
            return flag;
        }
    }
}