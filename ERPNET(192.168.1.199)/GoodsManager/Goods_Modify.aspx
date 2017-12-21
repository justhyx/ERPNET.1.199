<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Goods_Modify.aspx.cs" Inherits="ERPPlugIn.GoodsManager.Goods_Modify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>部番详细</title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .button
        {
            background-image: url(../images/button1_n.jpg);
            font-weight: bold;
            height: 30px;
            width: 80px;
        }
        .Divcenter
        {
            margin-right: auto;
            margin-left: auto;
            width: 98%;
            border-style: ridge;
            border-width: 2px;
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
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td height="30" align="center" style="font-size: 16px; font-weight: bold;">
                    商品档案
                    <asp:HiddenField runat="server" ID="hifId" />
                </td>
            </tr>
        </table>
        <div style="display: none">
            <table width="800" border="0" style="border-style: solid; border-color: #7E7E7E"
                align="center" cellpadding="6" cellspacing="0">
                <tr>
                    <td>
                        部番
                    </td>
                    <td>
                        版本
                    </td>
                    <td>
                        模具编号
                    </td>
                </tr>
                <tr>
                    <td>
                        部品名称
                    </td>
                    <td>
                        机种
                    </td>
                    <td>
                        编号
                    </td>
                </tr>
                <tr>
                    <td>
                        名称
                    </td>
                    <td>
                        型号
                    </td>
                    <td>
                        色番
                    </td>
                </tr>
                <tr>
                    <td>
                        材料供应商色番
                    </td>
                    <td>
                        颜色
                    </td>
                    <td>
                        部品重量
                    </td>
                </tr>
                <tr>
                    <td>
                        水口重量
                    </td>
                    <td>
                        干燥温度
                    </td>
                    <td>
                        干燥时间
                    </td>
                </tr>
                <tr>
                    <td>
                        粉碎料比例
                    </td>
                    <td>
                        难燃等级
                        <asp:TextBox runat="server" ID="txtLevel" />
                    </td>
                    <td>
                        购买商
                        <asp:TextBox runat="server" ID="txtBuyer" />
                    </td>
                </tr>
                <tr>
                    <td>
                        周期
                    </td>
                    <td>
                        色粉型号
                        <asp:TextBox runat="server" ID="txtCModel" />
                    </td>
                    <td>
                        色粉购买商
                        <asp:TextBox runat="server" ID="txtCBuyer" />
                    </td>
                </tr>
                <tr>
                    <td>
                        取数
                    </td>
                    <td>
                        担当 &nbsp;
                    </td>
                    <td>
                        客户
                    </td>
                </tr>
                <tr>
                    <td>
                        ROHS认证
                        <asp:DropDownList ID="ddlROHS" runat="server">
                            <asp:ListItem Text="有" Value="0" />
                            <asp:ListItem Text="无" Value="1" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        机型
                    </td>
                    <td>
                        模具类型
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        备注
                        <asp:TextBox runat="server" ID="txtRemark" Width="750px" />
                    </td>
                </tr>
            </table>
            <table width="800" border="0" align="center" cellpadding="0" cellspacing="0" style="border-color: #7E7E7E">
                <tr>
                    <td align="center">
                        <asp:Button Text="修 改" ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click"
                            OnClientClick="return confirm('确定修改？');" />
                    </td>
                    <td align="center">
                        <asp:Button Text="新 增" ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click"
                            OnClientClick="return confirm('确定新增？');" />
                    </td>
                    <td align="center">
                        <asp:Button Text="发 行" ID="btnRun" runat="server" CssClass="button" OnClick="btnRun_Click"
                            OnClientClick="submitTest(this)" />
                    </td>
                    <td align="center">
                        <asp:Button Text="删 除" runat="server" CssClass="button" ID="btnDelete" OnClick="btnDelete_Click"
                            OnClientClick="return confirm('是否删除？');" />
                    </td>
                    <td align="center">
                        <asp:Button Text="返 回" ID="btnReturn" runat="server" CssClass="button" OnClick="btnReturn_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-bottom: 20px; margin-top: 20px">
            <fieldset class="Divcenter">
                <legend>商品信息</legend>
                <table width="98%" border="0" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                    <tr>
                        <td align="right">
                            部番
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtgoodsName" Enabled="False" />
                        </td>
                        <td align="right">
                            版本
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtVersion" AutoPostBack="True" OnTextChanged="txtVersion_TextChanged"
                                Enabled="False" />
                        </td>
                        <td align="right">
                            模具编号
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtjigNo" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            部品名称
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtgoodsEName" />
                        </td>
                        <td align="right">
                            机种
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAircraft" />
                        </td>
                        <td align="right">
                            半成品类型
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlLb7">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            半成品对应成品
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtlb7" />
                        </td>
                        <td align="right">
                            技通号
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNo" Enabled="False" />
                        </td>
                        <td colspan="2" align="left">
                            <%if (Id != string.Empty)
                              {
                            %>
                            <a target="_blank" href="../UploadFile/<%=Id%>">附件浏览</a>
                            <%
                                }
                              if (Id == string.Empty)
                              {
                            %>无附件
                            <% 
                                }
                            %>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            设变内容
                        </td>
                        <td colspan="7">
                            <asp:TextBox runat="server" ID="txtContent" Width="99%" Enabled="False" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div style="margin-bottom: 20px">
            <fieldset class="Divcenter">
                <legend>材料信息</legend>
                <table width="98%" border="0" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                    <tr>
                        <td align="right">
                            材料编号
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMNo" AutoPostBack="True" OnTextChanged="txtMColor_TextChanged" />
                        </td>
                        <td align="right">
                            名称
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMName" Enabled="false" />
                        </td>
                        <td align="right">
                            型号
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMModel" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            色番
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMColor" Enabled="false" />
                        </td>
                        <td align="right">
                            材料供应商色番
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMVenderColor" Enabled="false" />
                        </td>
                        <td align="right">
                            颜色
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtColor" Enabled="false" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <fieldset class="Divcenter">
            <legend>成型信息</legend>
            <table width="98%" border="0" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                <tr>
                    <td align="right">
                        部品重量
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtWeight" />
                    </td>
                    <td align="right">
                        水口重量
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSKWeight" />
                    </td>
                    <td align="right">
                        干燥温度
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTemp" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        干燥时间
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTime" />&nbsp;h
                    </td>
                    <td align="right">
                        粉碎料比例
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCrushedscale" />
                    </td>
                    <td align="right">
                        担当
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCharge" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        周期
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCycle" />
                    </td>
                    <td align="right">
                        取数
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtQty" />
                    </td>
                    <td align="right">
                        &nbsp;模具类型
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlModeType" runat="server">
                            <asp:ListItem Text="量产" Value="0" />
                            <asp:ListItem Text="贩卖" Value="1" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        客户
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustomer" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        机型
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMachine" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        4M变更
                    </td>
                    <td>
                        <asp:CheckBox Text="" runat="server" ID="Rbt4M" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        备注
                    </td>
                    <td colspan="5">
                        <asp:TextBox runat="server" ID="TextBox20" Width="99%" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <div style="margin-top: 20px; margin-bottom: 20px">
            <fieldset class="Divcenter">
                <legend>操作按钮</legend>
                <table border="0" cellpadding="10px" cellspacing="6px" align="center">
                    <tr>
                        <td align="center">
                            <asp:Button Text="修 改" ID="Button1" runat="server" CssClass="button" OnClick="btnSave_Click"
                                OnClientClick="return confirm('确定修改？');" />
                        </td>
                        <asp:Button Text="新 增" ID="Button2" runat="server" CssClass="button" OnClick="btnAdd_Click"
                            OnClientClick="return confirm('确定新增？');" Visible="False" />
                        <td align="center">
                            <asp:Button Text="发 行" ID="Button3" runat="server" CssClass="button" OnClick="btnRun_Click"
                                OnClientClick="submitTest(this)" />
                        </td>
                        <td align="center">
                            <asp:Button Text="删 除" runat="server" CssClass="button" ID="Button4" OnClick="btnDelete_Click"
                                OnClientClick="return confirm('是否删除？');" />
                        </td>
                        <td align="center">
                            <asp:Button Text="返回主页" runat="server" CssClass="button" ID="Button5" OnClick="Button5_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <fieldset class="Divcenter" runat="server" id="fieldgoods" style="text-align: center">
            <legend>部品变更履历</legend>
            <table width="100%" border="0" align="center" cellpadding="6" cellspacing="0">
                <tr>
                    <td>
                        <asp:GridView runat="server" ID="dgvList" border="1" align="center" CssClass="gv"
                            CellPadding="4" BorderColor="#7E7E7E" AutoGenerateColumns="False" EnableModelValidation="True"
                            OnRowDataBound="dgvList_RowDataBound" Width="100%" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:TemplateField HeaderText="序列">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                        <asp:HiddenField runat="server" ID="HfId" Value='<%# Eval("goodsId") %>' />
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
                                        <asp:Label ID="Label14" runat="server" Text='<%#Eval("sk_scale") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--                            <asp:TemplateField HeaderText="难燃等级">
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text='<%#Eval("Fire_Retardant_Grade") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="购买商">
                                    <ItemTemplate>
                                        <asp:Label ID="Label15" runat="server" Text='<%#Eval("Buyer") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="周期">
                                    <ItemTemplate>
                                        <asp:Label ID="Label16" runat="server" Text='<%#Eval("cxzq") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--                            <asp:TemplateField HeaderText="色粉型号">
                                <ItemTemplate>
                                    <asp:Label ID="Label18" runat="server" Text='<%#Eval("Toner_Model") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="色粉购买商">
                                <ItemTemplate>
                                    <asp:Label ID="Label19" runat="server" Text='<%#Eval("Toner_Buyer") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="取数">
                                    <ItemTemplate>
                                        <asp:Label ID="Label17" runat="server" Text='<%#Eval("qs") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="机型">
                                    <ItemTemplate>
                                        <asp:Label ID="Label18" runat="server" Text='<%#Eval("dw") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="客户">
                                    <ItemTemplate>
                                        <asp:Label ID="Label19" runat="server" Text='<%#Eval("khdm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--                            <asp:TemplateField HeaderText="ROHS认证">
                                <ItemTemplate>
                                    <asp:Label ID="Label23" runat="server" Text='<%#Eval("Rohs_Certification") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="担当人">
                                    <ItemTemplate>
                                        <asp:Label ID="Label20" runat="server" Text='<%#Eval("Model_Abrasives") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注">
                                    <ItemTemplate>
                                        <asp:Label ID="Label21" runat="server" Text='<%#Eval("remark") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    </form>
</body>
</html>
