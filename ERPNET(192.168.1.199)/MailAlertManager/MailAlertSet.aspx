<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailAlertSet.aspx.cs" Inherits="ERPPlugIn.MailAlertManager.MailAlertSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" border="1" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E"
            width="800">
            <tr>
                <td>
                    收费项
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtItem" />
                </td>
            </tr>
            <tr>
                <td style="width: 130px">
                    有效周期
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtzq" />
                </td>
            </tr>
            <tr>
                <td>
                    生效日期
                </td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    费用
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtMoney" />
                </td>
            </tr>
            <tr>
                <td>
                    供应商
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtVendor" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>










































