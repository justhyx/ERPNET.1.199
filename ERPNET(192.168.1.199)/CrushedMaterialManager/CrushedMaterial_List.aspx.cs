using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data.SqlClient;
using ERPPlugIn.Class;
using System.Configuration;
using System.Data;

namespace ERPPlugIn.CrushedMaterialManager
{
    public partial class CrushedMaterial_List : System.Web.UI.Page
    {
        string smtpIp = ConfigurationManager.AppSettings["smtpIP"].ToString();
        Decimal Port = Convert.ToDecimal(ConfigurationManager.AppSettings["smtpPort"].ToString());
        string fromUser = ConfigurationManager.AppSettings["fromUser"].ToString();
        string fromPwd = ConfigurationManager.AppSettings["fromPassword"].ToString();
        string mailList = ConfigurationManager.AppSettings["mailList"].ToString();
        string ccList = ConfigurationManager.AppSettings["ccList"].ToString();
        string url = ConfigurationManager.AppSettings["url"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dgvList.DataSource = getAllCrushedMaterial();
                dgvList.DataBind();
            }
        }
        public List<CrushedMaterial> getAllCrushedMaterial()
        {
            List<CrushedMaterial> cList = new List<CrushedMaterial>();
            SelectCommandBuilder s = new SelectCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "trymolde");
            string sql = "SELECT id, 品番, 材料, 材料编号, 模具担当, addtime, PO处理 FROM trymolde WHERE (PO处理 = '粉碎') AND(isApprove is null) ORDER BY addtime";
            SqlDataReader dr = s.ExecuteReader(sql);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CrushedMaterial c = new CrushedMaterial()
                    {
                        id = dr.GetInt32(0),
                        Name = dr.GetString(1),
                        Material = dr.GetString(2),
                        MaterialNo = dr.GetString(3),
                        JigLeader = dr.GetString(4),
                        addtime = dr.GetString(5)
                    };
                    cList.Add(c);
                }
            }
            return cList;
        }
        protected void dgvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvList.PageIndex = e.NewPageIndex;
            dgvList.DataSource = getAllCrushedMaterial();
            dgvList.DataBind();
        }
        protected void dgvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //----------鼠标移动到每项时颜色交替效果  
                e.Row.Attributes["onmouseover"] = "e=this.style.backgroundColor;this.style.backgroundColor='#FFFF99'";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=e";
                //----------设置悬浮鼠标指针形状为小手"  
                e.Row.Attributes["style"] = "Cursor:hand";
                //String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString()); e.Row.Attributes.Add("onclick", evt);
            }
        }
        protected void dgvList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CheckBox cb = (dgvList.SelectedRow.Cells[0].FindControl("cboCheckItem") as CheckBox);
            //if (cb.Checked == true)
            //{
            //    cb.Checked = false;
            //}
            //else
            //{
            //    cb.Checked = true;
            //}
        }
        protected void ddlVerify_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtReason.Text = string.Empty;
            if (ddlVerify.SelectedItem.Value == "1")
            {
                txtReason.Enabled = true;
                txtReason.Focus();
            }
            else
            {
                txtReason.Enabled = false;
            }
        }
        protected void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                List<CrushedMaterial> cmList = new List<CrushedMaterial>();
                List<string> sqlList = new List<string>();
                if (dgvList.Rows.Count != 0 && checkVerifyInput())
                {
                    for (int i = 0; i < dgvList.Rows.Count; i++)
                    {
                        if ((dgvList.Rows[i].Cells[0].FindControl("cboCheckItem") as CheckBox).Checked == true)
                        {
                            CrushedMaterial c = new CrushedMaterial()
                            {
                                id = int.Parse((dgvList.Rows[i].Cells[0].FindControl("Hfid") as HiddenField).Value),
                                Name = (dgvList.Rows[i].Cells[1].FindControl("Label1") as Label).Text,
                                MaterialNo = (dgvList.Rows[i].Cells[2].FindControl("Label2") as Label).Text,
                                Material = (dgvList.Rows[i].Cells[3].FindControl("Label3") as Label).Text,
                                JigLeader = (dgvList.Rows[i].Cells[4].FindControl("Label4") as Label).Text,
                                addtime = (dgvList.Rows[i].Cells[5].FindControl("Label5") as Label).Text
                            };
                            cmList.Add(c);
                        }
                    }
                    if (cmList.Count == 0)
                    {
                        Response.Write("<script>alert('没有选择任何行!')</script>");
                        return;
                    }
                    string body = "部番：";
                    List<string> bd = new List<string>();
                    string list = "";
                    for (int i = 0; i < cmList.Count; i++)
                    {
                        if (i != cmList.Count - 1)
                        {
                            body += cmList[i].Name + ",";
                            list += cmList[i].JigLeader + ";";
                        }
                        else
                        {
                            body += cmList[i].Name;
                            list += cmList[i].JigLeader;
                        }

                        UpdateCommandBuilder up = new UpdateCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "trymolde");
                        up.UpdateColumn("isApprove", ddlVerify.SelectedItem.Text);
                        up.UpdateColumn("ApprovedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        up.UpdateColumn("Reason", txtReason.Text.Trim());
                        if (string.IsNullOrEmpty(txtReason.Text.Trim()))
                        {
                            up.UpdateColumn("area1style", "Wait");
                            up.UpdateColumn("area2style", "Wait");
                            up.UpdateColumn("area3style", "Wait");
                            up.UpdateColumn("area4style", "Wait");
                        }
                        up.ConditionsColumn("id", cmList[i].id);
                        sqlList.Add(up.getUpdateCommand());
                    }
                    if (ddlVerify.SelectedItem.Text == "NG")
                    {
                        body += "审核未通过，原因为:" + txtReason.Text;
                        mailList = getUserMailAddress(list);
                    }
                    else
                    {
                        bd.Add(body + "审核已通过!" + "   <a href='" + url + "?status=Wait&&area=1'>点击此链接开始进行粉碎...</a>");
                        bd.Add(body + "审核已通过!" + "   <a href='" + url + "?status=Wait&&area=2'>点击此链接开始进行粉碎...</a>");
                        bd.Add(body + "审核已通过!" + "   <a href='" + url + "?status=Wait&&area=3'>点击此链接开始进行粉碎...</a>");
                        bd.Add(body + "审核已通过!" + "   <a href='" + url + "?status=Wait&&area=4'>点击此链接开始进行粉碎...</a>");
                    }
                    InsertCommandBuilder ins = new InsertCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "trymolde");
                    int count = ins.ExcutTransaction(sqlList);
                    if (count != 0)
                    {
                        Response.Write("<script>alert('审核成功!')</script>");
                        if (mailList != "")
                        {
                            if (ddlVerify.SelectedItem.Text == "NG")
                            {
                                SendMail.ExecuteSendMail(smtpIp, Port, fromUser, fromPwd, mailList, "", "试作品粉碎审核通告(系统邮件,请勿回复！)", body, "", "");
                            }
                            else
                            {
                                for (int i = 0; i <= 3; i++)
                                {
                                    SendMail.ExecuteSendMail(smtpIp, Port, fromUser, fromPwd, ConfigurationManager.AppSettings["area" + (i + 1)].ToString(), "", "试作品粉碎审核通告(系统邮件,请勿回复！)", bd[i], "", "");
                                }
                            }

                        }

                        dgvList.DataSource = getAllCrushedMaterial();
                        dgvList.DataBind();
                        //Response.Write("<script>alert('" + s + "!')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('审核失败!')</script>");
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "!')</script>"); ;
            }

        }
        protected string getUserMailAddress(string userName)
        {
            string mail = "";
            string[] nameList = userName.Split(';');
            SelectCommandBuilder s = new SelectCommandBuilder(ConnectionFactory.ConnectionString_hudsonwwwroot, "trymolde");
            string sql = "SELECT email FROM HUDSON_User";
            for (int i = 0; i < nameList.Length; i++)
            {
                if (i == 0)
                {
                    sql += " Where UserName = '" + nameList[i] + "'";
                }
                else
                {
                    sql += " or UserName = '" + nameList[i] + "'";
                }

            }
            DataTable dt = s.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i]["email"].ToString()))
                    {
                        mail += dt.Rows[i]["email"].ToString() + ";";
                    }
                }
            }
            return mail;
        }
        protected bool checkVerifyInput()
        {
            bool flag = true;
            if (ddlVerify.SelectedItem.Value == "1" && txtReason.Text == "")
            {
                Response.Write("<script>alert('请输入原因!')</script>");
                txtReason.Focus();
                flag = false;
            }
            return flag;
        }

        protected void btnWork_Click(object sender, EventArgs e)
        {
            Response.Redirect("CrushedMaterial_Approved.aspx");
        }
    }
    public class CrushedMaterial
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string MaterialNo { get; set; }
        public string Material { get; set; }
        public string JigLeader { get; set; }
        public string addtime { get; set; }
    }
}