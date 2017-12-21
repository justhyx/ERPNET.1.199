<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaintenanceRecords_PicView.aspx.cs"
    Inherits="ERPPlugIn.MaintenanceRecords.MaintenanceRecords_PicView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td>
                    <asp:Image ID="PicView" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <a href="#" onclick="javascript:history.back();">返回</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
