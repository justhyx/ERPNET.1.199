<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Goods_Print.aspx.cs" Inherits="ERPPlugIn.GoodsManager.Goods_Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<object id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0"
    width="0">
</object>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>构成配布</title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .button
        {
            background-image: url(../images/button1_n.jpg);
            font-weight: bold;
            height: 30px;
            width: 80px;
        }
        .gv
        {
            width: 100%;
        }
        .lbhide
        {
            display: none;
        }
    </style>
    <style type="text/css" media="print">
        .notPrint
        {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--startprint-->
        <table width="100%" border="0" align="center" cellpadding="6" cellspacing="0">
            <tr>
                <td colspan="2" height="30" align="center" style="font-size: 16px; font-weight: bold;">
                    构成配布
                </td>
            </tr>
            <tr class="notPrint">
                <td>
                    客户:
                    <asp:DropDownList runat="server" ID="ddlCustomer">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button Text="搜寻" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                </td>
                <td align="right">
                    <input type="button" value="页面设置" onclick="document.all.WebBrowser.ExecWB(8,1)" />
                    <input type="button" value="打印预览" onclick="document.all.WebBrowser.ExecWB(7,1)" />
                    <input type="button" value="打印" onclick="preview()" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView runat="server" ID="gvData" border="1" align="center" CssClass="gv"
                        CellPadding="4" BorderColor="#7E7E7E" AutoGenerateColumns="False" EnableModelValidation="True">
                        <Columns>
                            <asp:TemplateField HeaderText="序列">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1%>
                                    <asp:HiddenField runat="server" ID="HfId" Value='<%# Eval("goods_Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="部番">
                                <ItemTemplate>
                                    <asp:Label ID="Label0" runat="server" Text='<%#Eval("goods_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="磨具编号">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("mjh") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="部品名称">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("goods_ename") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="机种">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("Aircraft") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="编号">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("Materail_Number") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="名称">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("Materail_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="型号">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%#Eval("Materail_Model") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="色番">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("ys") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="材料供应商色番">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%#Eval("Materail_Vender_Color") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="颜色">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%#Eval("Materail_Color") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="部品重量">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%#Eval("cpdz") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="水口重量">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%#Eval("skdz") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="干燥温度">
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Text='<%#Eval("Drying_Temperature") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="干燥时间">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Text='<%#Eval("Drying_Time") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="粉碎料比例">
                                <ItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Text='<%#string.Format("{0:0.%}",Eval("sk_scale")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="难燃等级">
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text='<%#Eval("Fire_Retardant_Grade") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="购买商">
                                <ItemTemplate>
                                    <asp:Label ID="Label16" runat="server" Text='<%#Eval("Buyer") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="周期">
                                <ItemTemplate>
                                    <asp:Label ID="Label17" runat="server" Text='<%#string.Format("{0:0.#}",Eval("cxzq")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="色粉型号">
                                <ItemTemplate>
                                    <asp:Label ID="Label18" runat="server" Text='<%#Eval("Toner_Model") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="色粉购买商">
                                <ItemTemplate>
                                    <asp:Label ID="Label19" runat="server" Text='<%#Eval("Toner_Buyer") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="取数">
                                <ItemTemplate>
                                    <asp:Label ID="Label20" runat="server" Text='<%# string.Format("{0:0.#}",Eval("qs")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="机型">
                                <ItemTemplate>
                                    <asp:Label ID="Label21" runat="server" Text='<%#Eval("dw") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="客户">
                                <ItemTemplate>
                                    <asp:Label ID="Label22" runat="server" Text='<%#Eval("khdm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ROHS认证">
                                <ItemTemplate>
                                    <asp:Label ID="Label23" runat="server" Text='<%#Eval("Rohs_Certification") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="担当人">
                                <ItemTemplate>
                                    <asp:Label ID="Label24" runat="server" Text='<%#Eval("Model_Abrasives") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注">
                                <ItemTemplate>
                                    <asp:Label ID="Label25" runat="server" Text='<%#Eval("remark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <!--endprint-->
        </table>
    </div>
    </form>
    <script type="text/javascript">
        function preview() {
            window.print();
        }
    </script>
</body>
</html>



























































































