﻿using System;
using System.Collections.Generic;
using System.Web;
using HUDSON.ERP.DBCommandDAL.SQLServerClient;
using System.Data;

namespace ERPPlugIn.CrushedMaterialManager
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class ServerData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string keyword = context.Request.QueryString["keyword"];
            if (keyword != null)
            {
                string sql = "SELECT DISTINCT TOP 5 goods_name FROM goods WHERE (goods_name LIKE '" + keyword + "%')";
                SelectCommandBuilder s = new SelectCommandBuilder();
                DataTable d = s.ExecuteDataTable(sql);
                string jsonString = "";
                if (d != null && d.Rows.Count != 0)
                {
                    jsonString += "[";
                    for (int i = 0; i < d.Rows.Count; i++)
                    {
                        if (i != d.Rows.Count - 1)
                        {
                            jsonString += '"' + d.Rows[i][0].ToString().Trim() + '"' + ",";
                        }
                        else
                        {
                            jsonString += '"' + d.Rows[i][0].ToString().Trim() + '"' + "]";
                        }

                    }

                }
                context.Response.Write(jsonString);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}