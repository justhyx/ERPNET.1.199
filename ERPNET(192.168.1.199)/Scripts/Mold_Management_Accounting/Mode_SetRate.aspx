<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mode_SetRate.aspx.cs" Inherits="ERPPlugIn.Mold_Management_Accounting.Mode_SetRate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>税率设置</title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
            if (window.opener.progressWindow)
                window.opener.progressWindow.close();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-top: 20px; height: 50px">
        <table width="40%" border="1" align="center" cellpadding="5" cellspacing="0" style="border-color: #7E7E7E;
            margin-top: 100px; margin-bottom: 20px">
            <tr>
                <td align="center">
                    税率
                </td>
                <td style="width: 50%">
                    <asp:TextBox runat="server" ID="txtRate" Width="85%" />&nbsp;%
                </td>
                <td align="center">
                    <asp:Button Text="修改税率" ID="btnEdit" runat="server" OnClick="btnEdit_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
