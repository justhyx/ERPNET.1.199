<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="goods_ito.aspx.cs" Inherits="ERPPlugIn.Goods_Ito.goods_ito" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>回转率</title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
    <style type="text/css">
        .ui-helper-hidden-accessible
        {
            display: none;
        }
        .none
        {
            border: 0px 0px 0px 0px;
        }
        span input
        {
            text-align: center;
            border: 0px 0px 0px 0px;
        }
        .Divcenter
        {
            margin-right: auto;
            margin-left: auto;
        }
        li
        {
            list-style-type: none;
            text-align: left;
            margin-left: -38px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Divcenter" style="border: 1px solid black; width: 99%; margin-top: 5px;">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td height="30" align="center" style="font-size: 16px; font-weight: bold;">
                    回转率
                </td>
            </tr>
        </table>
        <hr />
        <div style="margin-bottom: 20px; margin-top: 20px">
            开始日期<asp:TextBox runat="server" ID="txtStartDate" onClick="WdatePicker()" />结束日期<asp:TextBox
                runat="server" ID="txtEndDate" onClick="WdatePicker()" />
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnQuery" Text="计算" runat="server" OnClick="btnQuery_Click"
                OnClientClick="submitTest(this)" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnExport"
                    Text="导出" runat="server" OnClick="btnExport_Click" />
        </div>
        <hr />
        <div class="divcenter" style="margin-bottom: 20px; text-align: center">
            <asp:Panel runat="server" ScrollBars="Both" Height="600">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="margin: auto">
                    <tr>
                        <td style="text-align: center">
                            <asp:GridView ID="gvShowData" runat="server" Width="100%" EnableModelValidation="True">
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>
