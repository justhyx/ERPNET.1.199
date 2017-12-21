<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockOutCreate.aspx.cs"
    Inherits="ERPPlugIn.StockInManager.StockOutCreate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>成品出库制单</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        span input
        {
            text-align: center;
            border: 0px 0px 0px 0px;
        }
        .style1
        {
            width: 93px;
        }
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
    <div>
        <table width="800" border="1" align="center" style="border-color: #7E7E7E" cellpadding="3"
            cellspacing="0">
            <tr>
                <td colspan="5" align="center" style="font-size: 18px; font-weight: bold;">
                    成品出库制单
                </td>
            </tr>
            <tr>
                <td class="style1">
                    出库方式:
                </td>
                <td>
                    <span id="rblcellstat" style="display: inline-block; width: 240px;">
                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="flow"
                            ID="rbtType" AutoPostBack="True" OnSelectedIndexChanged="rbtType_SelectedIndexChanged">
                            <asp:ListItem Text="部品报损出库" Value="E" />
                            <asp:ListItem Text="部品转库出库" Value="2" Enabled="false" />
                        </asp:RadioButtonList>
                    </span>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    仓位点:
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlMaterialStock" AutoPostBack="True" OnSelectedIndexChanged="ddlMaterialStock_SelectedIndexChanged">
                        <asp:ListItem Text="无" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    出库日期:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDate" onClick="WdatePicker()" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    出库单号:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtNo" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    备注:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtRemark" Width="690px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button Text="添 加" ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>







































































































