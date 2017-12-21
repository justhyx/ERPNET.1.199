<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Goods_Record.aspx.cs" Inherits="ERPPlugIn.GoodsManager.Goods_Record" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设变管理</title>
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
                    设变管理
                </td>
            </tr>
        </table>
        <hr />
        <div style="margin-bottom: 20px; margin-top: 20px">
            <table width="98%" border="1" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                <tr>
                    <td align="right">
                        部番
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtgoodsName" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnExcute" Text="检索" runat="server" OnClientClick="submitTest(this)"
                            OnClick="btnExcute_Click" />
                    </td>
                    <td align="right">
                        当前版本
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtVersion" Enabled="False" />
                    </td>
                    <td align="right">
                        新版本
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNewVersion" />
                    </td>
                    <td align="right">
                        技通号
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNo" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        设变内容
                    </td>
                    <td colspan="7">
                        <asp:TextBox runat="server" ID="txtContent" Width="95%" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        添加附件
                    </td>
                    <td colspan="7" style="color: #FF0000">
                        <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;*只能上传PDF文档
                    </td>
                </tr>
                <tr>
                    <td colspan="8" align="center">
                        <asp:Button Text="确定" ID="btnChange" runat="server" OnClick="btnChange_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
