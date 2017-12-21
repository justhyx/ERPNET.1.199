<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock_Output_Detail.aspx.cs"
    Inherits="ERPPlugIn.StockManager.Stock_Output_Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="content-type" content="application/ms-excel; charset=UTF-8" />
<head runat="server">
    <title>备货指示号数据明细</title>
    <style type="text/css" media="print">
        .button
        {
            display: none;
        }
        .notPrint
        {
            display: none;
        }
    </style>
    <style type="text/css">
        .button
        {
            background-image: url(../images/button1_n.jpg);
            font-weight: bold;
            height: 30px;
            width: 80px;
        }
        .td
        {
            text-align: center;
        }
        .ody
        {
            font-size: 15px;
            color: #444444;
        }
    </style>
    <%--<link href="../../css/css.css" rel="stylesheet" type="text/css" />--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="1100" border="0" align="center" cellspacing="0" cellpadding="6">
            <tr>
                <td align="right">
                    <input type="button" name="print" value="预览" onclick="preview()" class="button" />
                    <asp:Button Text="导出" runat="server" ID="btnExport" CssClass="button" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <!--startprint-->
        <table align="center" border="1" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E"
            width="1100" runat="server" id="tb">
            <tr>
                <td align="center" colspan="4" style="font-size: 18px; font-weight: bold;">
                    备货指示号数据明细
                </td>
            </tr>
            <tr>
                <td>
                    客户:
                    <asp:Label ID="lblkhdm" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp; 备料指示号:
                    <asp:Label ID="lblPid" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp; 送货日期:
                    <asp:Label ID="txtDate" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblprintDate" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:GridView ID="gvExcel" runat="server" Width="1100" AutoGenerateColumns="False"
                        EnableModelValidation="True" OnPreRender="gvExcel_PreRender" OnRowDataBound="gvExcel_RowDataBound"
                        ShowFooter="True" CssClass="ody">
                        <Columns>
                            <asp:TemplateField HeaderText="NO.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="部番">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("goodsName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="交货数">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="库存">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("stockSumQty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="订单余数">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("xsht_detailqty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="计划余数" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("plan_qty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="货况">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%#Eval("isOk") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="箱型">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("bzxx") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="箱入数">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("jzl") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="箱数">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("jzlqty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="区域">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("area") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NGArea">
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("NgQty") %>'>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="库存">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("stockQty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备货日期">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="75px" />
                                <HeaderStyle Width="75px" />
                                <ItemStyle Width="75px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="扣数日期">
                                <ItemTemplate>
                                    <asp:Label ID="Label14" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="75px" />
                                <HeaderStyle Width="75px" />
                                <ItemStyle Width="75px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注">
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="75px" />
                                <HeaderStyle Width="75px" />
                                <ItemStyle Width="75px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <!--endprint-->
    </div>
    </form>
</body>
<script type="text/javascript" language="Javascript">
    function preview() {
        bdhtml = window.document.body.innerHTML;
        sprnstr = "<!--startprint-->";
        eprnstr = "<!--endprint-->";
        prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
        prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
        var oWin = window.open("PrintPage.htm", "_blank");
        if (oWin != null) {
            oWin.document.body.innerHTML = prnhtml;
            oWin.document.body.innerHTML = prnhtml + "<div style='text-align:center'><button onclick='window.print()' class='button' style='padding-left:4px;padding-right:4px;'>打 印</button><button onclick='window.opener=null;window.close();'class='button' style='padding-left:4px;padding-right:4px;'>关 闭</button></div>\n";
            //window.document.body.innerHTML = prnhtml;   
        }


    }
    function startPrint(obj) {
        var oWin = window.open("", "_blank");
        var strPrint = "<h4 style='font-size:18px; text-align:center;'>打印预览区</h4>\n";
        strPrint = strPrint + "<script type=\"text/javascript\">\n";
        strPrint = strPrint + "function printWin()\n";
        strPrint = strPrint + "{";
        strPrint = strPrint + "var oWin=window.open(\"\",\"_blank\");\n";
        strPrint = strPrint + "oWin.document.write(document.getElementById(\"content\").innerHTML);\n";
        strPrint = strPrint + "oWin.focus();\n";
        strPrint = strPrint + "oWin.document.close();\n";
        strPrint = strPrint + "oWin.print()\n";
        strPrint = strPrint + "oWin.close()\n";
        strPrint = strPrint + "}\n";
        strPrint = strPrint + "<\/script>\n";
        strPrint = strPrint + "<hr size='1′ />\n";
        strPrint = strPrint + "<div id=\"content\">\n";
        bdhtml = window.document.body.innerHTML;
        sprnstr = "<!--startprint-->";
        eprnstr = "<!--endprint-->";
        prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
        prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
        strPrint = strPrint + prnhtml + "\n";
        strPrint = strPrint + "</div>\n";
        strPrint = strPrint + "<hr size='1′ />\n";
        strPrint = strPrint + "<div style='text-align:center'><button onclick='printWin()' style='padding-left:4px;padding-right:4px;'>打 印</button><button onclick='window.opener=null;window.close();' style='padding-left:4px;padding-right:4px;'>关 闭</button></div>\n";
        oWin.document.write(strPrint);
        oWin.focus();
        oWin.document.close();
    } 

</script>
</html>
