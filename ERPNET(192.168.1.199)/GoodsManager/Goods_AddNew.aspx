<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Goods_AddNew.aspx.cs" Inherits="ERPPlugIn.GoodsManager.Goods_AddNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>部品添加</title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .button
        {
            background-image: url(../images/button1_n.jpg);
            font-weight: bold;
            height: 30px;
            width: 80px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: 1px solid black">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td height="30" align="center" style="font-size: 16px; font-weight: bold;">
                    商品档案
                </td>
            </tr>
        </table>
        <div style="display: none">
            <table width="800" border="0" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
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
                        担当
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
        </div>
        <div style="margin-bottom: 20px; margin-top: 20px">
            商品信息
            <table width="98%" border="1" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                <tr>
                    <td align="right">
                        部番
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtgoodsName" />
                    </td>
                    <td align="right">
                        版本
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtVersion" />
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
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-bottom: 20px">
            材料信息
            <table width="98%" border="1" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                <tr>
                    <td align="right">
                        材料编号
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMNo" AutoPostBack="True" OnTextChanged="txtMNo_TextChanged" /><label>--回车键带出材料信息</label>
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
        </div>
        成型信息
        <table width="98%" border="1" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
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
                    <asp:TextBox ID="txtCrushedscale" runat="server" />
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
                    模具类型
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlModeType">
                        <asp:ListItem Text="量产" Value="0" />
                        <asp:ListItem Text="贩卖" Value="1" Selected="True" />
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
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    备注
                    <asp:TextBox runat="server" ID="TextBox1" Width="1284px" />
                </td>
            </tr>
        </table>
        <div style="margin-top: 20px; margin-bottom: 20px">
            <table border="0" cellpadding="0" cellspacing="0" align="center">
                <tr>
                    <td align="center">
                        <asp:Button Text="添 加" ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click"
                            Enabled="False" />
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="center">
                        <input type="button" name="Submit22" onclick="javascript:history.back();" style="background-image: url(../images/button1_n.jpg);
                            font-weight: bold; height: 30px; width: 80px;" value="返 回" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
