<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mode_Modify.aspx.cs" Inherits="ERPPlugIn.Mold_Management_Accounting.Mode_Modify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>模具管理</title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
    var ra =<%=rate %>;
        function getTotal() {
            var price = document.getElementById("txtPrice").value;
            var rate = document.getElementById("txtRate");
            var toatl = document.getElementById("txtTotal");
            var count = document.getElementById("txtCount").value;
            var value = Math.round(price * ra * count * 100) / 100;
            rate.value = value;
            toatl.value = (Math.round(price * count * 100) / 100) + value;
        }
    </script>
    <style type="text/css">
        .fontstyle
        {
            font-size: larger;
        }
        .botton_c
        {
            height: 60px;
            width: 120px;
            color: #FFFFFF;
            font-size: 24px;
            font-weight: bold;
            border-color: #FF0000;
            border: 2px;
        }
        .btn
        {
            border-left: #ff9966 1px solid;
            border-right: #ff9966 1px solid;
            border-top: #ff9966 1px solid;
            border-bottom: #ff9966 1px solid;
            padding-left: 2px;
            padding-right: 2px;
            padding-top: 2px;
            padding-bottom: 2px;
            font-size: 20px;
            cursor: hand;
            color: #000000;
        }
        .label
        {
            padding-left: 2px;
            padding-right: 2px;
            padding-top: 2px;
            padding-bottom: 2px;
            font-size: 48px;
            cursor: hand;
            color: #000000;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: 1px solid black">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td height="30" align="center" style="font-size: 16px; font-weight: bold;">
                    模具管理
                </td>
            </tr>
        </table>
        <div style="margin-bottom: 20px; margin-top: 20px">
            <table width="98%" border="1" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                <tr>
                    <td align="right">
                        类型
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlMode_Type">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        模具型号
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMode" />
                    </td>
                    <td align="right">
                        部品名称
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtGoods_Name" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        部品号
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtGoods_No" />
                    </td>
                    <td align="right">
                        数量
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCount" onblur="getTotal()" onkeypress="if((event.keyCode<48 ||event.keyCode>57) &&event.keyCode!=46)event.returnValue=false;" />
                    </td>
                    <td align="right">
                        报价单番号
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtQuotation_number" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        单价(未税)
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPrice" onblur="getTotal()" onkeypress="if((event.keyCode<48 ||event.keyCode>57) &&event.keyCode!=46)event.returnValue=false;" />
                    </td>
                    <td align="right">
                        <asp:Label Text="text" runat="server" ID="lblRate" />
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtRate" readonly="readonly" />
                    </td>
                    <td align="right">
                        <asp:Label Text="text" runat="server" ID="lblTotal" />
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtTotal" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        币种
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlCurrency">
                            <asp:ListItem Text="RMB" />
                            <asp:ListItem Text="USD" />
                            <asp:ListItem Text="HKD" />
                            <asp:ListItem Text="N" />
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        客户订单接收日期
                    </td>
                    <td>
                        <asp:TextBox ID="txtRecive_Date" runat="server" onclick="WdatePicker();" />
                    </td>
                    <td align="right">
                        送货日期
                    </td>
                    <td>
                        <asp:TextBox ID="txtDelivery_Date" runat="server" onclick="WdatePicker();" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        内容备注
                    </td>
                    <td colspan="5">
                        <asp:TextBox runat="server" ID="txtContent_Remark" Height="58px" TextMode="MultiLine"
                            Width="99.5%" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-bottom: 20px; margin-top: 20px">
            <table width="98%" border="1" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                <tr>
                    <td align="right">
                        送货单号
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtdelivery_no" />
                    </td>
                    <td align="right">
                        客户订单号
                    </td>
                    <td>
                        <asp:TextBox ID="txtcustomer_order_no" runat="server" />
                    </td>
                    <td align="right">
                        营业收入时间
                    </td>
                    <td>
                        <asp:TextBox ID="txtrevenue_time" runat="server" onclick="WdatePicker({dateFmt:'yyyy年MM月'})" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        熔接对账时间
                    </td>
                    <td>
                        <asp:TextBox ID="txtreconciliation_time" runat="server" onclick="WdatePicker()" />
                    </td>
                    <td align="right">
                        收款日期
                    </td>
                    <td>
                        <asp:TextBox ID="txtreceipt_date" runat="server" onclick="WdatePicker()" />
                    </td>
                    <td align="right">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        备注
                    </td>
                    <td colspan="5">
                        <asp:TextBox runat="server" ID="txtRemark" Height="58px" TextMode="MultiLine" Width="99.5%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                    </td>
                    <td align="center" style="width: 25%">
                        已收:<asp:Label Text="0" CssClass="label" runat="server" ID="lblYiShou" />
                    </td>
                    <td align="center" style="width: 25%">
                        未收:<asp:Label Text="0" runat="server" CssClass="label" ID="lblWeiShou" />
                    </td>
                    <td colspan="2" align="left">
                        <a class="btn" href="Mode_Recipt_Data.aspx?id=<%=Id %>" target="_blank"><b>收款</b></a>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-top: 20px; margin-bottom: 20px;">
            <table border="1" cellpadding="10px" cellspacing="6px" align="center" width="98%">
                <tr>
                    <td align="center">
                        <asp:Button Text="保 存" ID="btnAdd" runat="server" CssClass="button" OnClientClick="return confirm('保存？');"
                            OnClick="btnAdd_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button Text="返 回" ID="btnReturn" runat="server" CssClass="button" OnClick="btnReturn_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
