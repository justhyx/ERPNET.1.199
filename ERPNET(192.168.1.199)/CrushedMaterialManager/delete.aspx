<%@ Page Language="C#" AutoEventWireup="true" Inherits="ERPPlugIn.CrushedMaterialManager.delete" Codebehind="delete.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <span>请输入密码：</span>

        <asp:TextBox ID="TextBoxPwd" runat="server"></asp:TextBox>
        <asp:Button ID="ButtonDelete" runat="server" Text="删除" onclick="ButtonDelete_Click" />
    </div>
    </form>
</body>
</html>
