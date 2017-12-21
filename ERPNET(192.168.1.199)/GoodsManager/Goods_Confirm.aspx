<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Goods_Confirm.aspx.cs"
    Inherits="ERPPlugIn.GoodsManager.Goods_Confirm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>部品构成</title>
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
    </style>
    <script type="text/javascript">
        function submitTest(btn) {
            btn.value = "处理中...";
            btn.onclick = onDealing;
        }
        function onDealing() {
            alert('系统处理中,请稍候...');
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellspacing="0" cellpadding="6">
            <tr>
                <td>
                    客户
                    <label>
                        <asp:DropDownList runat="server" ID="ddlCustomers">
                        </asp:DropDownList>
                    </label>
                    部番
                    <label>
                        <input type="text" id="tags" runat="server" />
                        <asp:Button Text="搜 寻" runat="server" value="搜寻" ID="btnSearch" OnClick="btnSearch_Click"
                            OnClientClick="submitTest(this)" />
                    </label>
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" BorderWidth="1px" BorderStyle="Dashed" Height="400px" ScrollBars="Auto">
            <table width="100%" border="0" align="center" cellpadding="6" cellspacing="0">
                <tr>
                    <td height="30" align="center" style="font-size: 16px; font-weight: bold;">
                        待审核部番
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView runat="server" ID="dgvList" border="1" align="center" CssClass="gv"
                            CellPadding="4" BorderColor="#7E7E7E" AutoGenerateColumns="False" EnableModelValidation="True">
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox Text="" ID="cboCheckItem" runat="server" />
                                        <asp:HiddenField runat="server" ID="HfId" Value='<%# Eval("goodsId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="序列">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
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
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%#Eval("mjh") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="部品名称">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("goods_ename") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%#Eval("goods_ename") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="机种">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Aircraft") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%#Eval("Aircraft") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="编号">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Materail_Number") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%#Eval("Materail_Number") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="名称">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("Materail_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%#Eval("Materail_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="型号">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("Materail_Model") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%#Eval("Materail_Model") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="色番">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("ys") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox7" runat="server" Text='<%#Eval("ys") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="材料供应商色番">
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%#Eval("Materail_Vender_Color") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox8" runat="server" Text='<%#Eval("Materail_Vender_Color") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="颜色">
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%#Eval("Materail_Color") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox9" runat="server" Text='<%#Eval("Materail_Color") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="部品重量">
                                    <ItemTemplate>
                                        <asp:Label ID="Label10" runat="server" Text='<%#Eval("cpdz") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox10" runat="server" Text='<%#Eval("cpdz") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="水口重量">
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%#Eval("skdz") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox11" runat="server" Text='<%#Eval("skdz") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="干燥温度">
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" runat="server" Text='<%#Eval("Drying_Temperature") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox12" runat="server" Text='<%#Eval("Drying_Temperature") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="干燥时间">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server" Text='<%#Eval("Drying_Time") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox13" runat="server" Text='<%#Eval("Drying_Time") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="粉碎料比例">
                                    <ItemTemplate>
                                        <asp:Label ID="Label14" runat="server" Text='<%#Eval("sk_scale") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox14" runat="server" Text='<%#Eval("sk_scale") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="难燃等级">
                                    <ItemTemplate>
                                        <asp:Label ID="Label15" runat="server" Text='<%#Eval("Fire_Retardant_Grade") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox15" runat="server" Text='<%#Eval("Fire_Retardant_Grade") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="购买商">
                                    <ItemTemplate>
                                        <asp:Label ID="Label16" runat="server" Text='<%#Eval("Buyer") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox16" runat="server" Text='<%#Eval("Buyer") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="周期">
                                    <ItemTemplate>
                                        <asp:Label ID="Label17" runat="server" Text='<%#Eval("cxzq") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox17" runat="server" Text='<%#Eval("cxzq") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="色粉型号">
                                    <ItemTemplate>
                                        <asp:Label ID="Label18" runat="server" Text='<%#Eval("Toner_Model") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox18" runat="server" Text='<%#Eval("Toner_Model") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="色粉购买商">
                                    <ItemTemplate>
                                        <asp:Label ID="Label19" runat="server" Text='<%#Eval("Toner_Buyer") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox19" runat="server" Text='<%#Eval("Toner_Buyer") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="取数">
                                    <ItemTemplate>
                                        <asp:Label ID="Label20" runat="server" Text='<%#Eval("qs") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox20" runat="server" Text='<%#Eval("qs") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="机型">
                                    <ItemTemplate>
                                        <asp:Label ID="Label21" runat="server" Text='<%#Eval("dw") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox21" runat="server" Text='<%#Eval("dw") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="客户">
                                    <ItemTemplate>
                                        <asp:Label ID="Label22" runat="server" Text='<%#Eval("khdm") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox22" runat="server" Text='<%#Eval("khdm") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ROHS认证">
                                    <ItemTemplate>
                                        <asp:Label ID="Label23" runat="server" Text='<%#Eval("Rohs_Certification") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox23" runat="server" Text='<%#Eval("Rohs_Certification") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="模具类型">
                                    <ItemTemplate>
                                        <asp:Label ID="Label26" runat="server" Text='<%#Eval("Model_Type") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox26" runat="server" Text='<%#Eval("Model_Type") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="担当人">
                                    <ItemTemplate>
                                        <asp:Label ID="Label24" runat="server" Text='<%#Eval("Model_Abrasives") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox24" runat="server" Text='<%#Eval("Model_Abrasives") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注">
                                    <ItemTemplate>
                                        <asp:Label ID="Label25" runat="server" Text='<%#Eval("remark") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox25" runat="server" Text='<%#Eval("remark") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table width="800" border="1" align="center" style="border-color: #7E7E7E" cellpadding="0"
            cellspacing="0">
            <tr>
                <td align="center">
                    请输入去向地:<asp:TextBox runat="server" ID="txtGoArea" />
                </td>
                <td align="center">
                    <asp:Button Text="添 加" ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click" />
                    <asp:Button Text="清 除" ID="btnClear" runat="server" CssClass="button" OnClick="btnClear_Click" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    部门：
                    <asp:DropDownList runat="server" ID="ddldept" Enabled="False">
                        <asp:ListItem Text="生管" Value="0" />
                        <asp:ListItem Text="营业" Value="1" />
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Button Text="量 产" ID="btnConfirm" runat="server" CssClass="button" OnClick="btnConfirm_Click"
                        OnClientClick="return confirm('确认无误,开始量产吗？');" />
                </td>
            </tr>
            <%
                for (int i = 0; i < sList.Count; i++)
                {
            %>
            <tr>
                <td align="center">
                    去向地：
                </td>
                <td align="center">
                    <% Response.Write(sList[i].ToString()); %>
                </td>
            </tr>
            <%
                }
                
            %>
        </table>
    </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>







































































































