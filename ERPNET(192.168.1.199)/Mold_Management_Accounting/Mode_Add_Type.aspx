<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mode_Add_Type.aspx.cs"
    Inherits="ERPPlugIn.Mold_Management_Accounting.Mode_Add_Type" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加类型</title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
            if (window.opener.progressWindow)
                window.opener.progressWindow.close();
        }
    </script>
</head>
<body onload="refreshParent()">
    <form id="form1" runat="server">
    <div style="margin-top: 20px; height: 50px">
        <table width="40%" border="1" align="center" cellpadding="5" cellspacing="0" style="border-color: #7E7E7E;
            margin-top: 100px; margin-bottom: 20px">
            <tr>
                <td>
                    类型
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTypeName" Width="95%" />
                </td>
                <td>
                    <asp:Button Text="添加" ID="btnAdd" runat="server" OnClick="btnAdd_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    类型
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlMode_Type">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button Text="删除" ID="btnDelete" runat="server" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
