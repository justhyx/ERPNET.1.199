<%@ Page Language="C#" AutoEventWireup="true" Inherits="ERPPlugIn.CrushedMaterialManager.getSure"
    CodeBehind="getSure.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <span>请输入密码：</span>
    <asp:TextBox ID="TextBoxPwd" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="确定" />
    <asp:HiddenField ID="HiddenFieldArea" runat="server" />
    <asp:HiddenField ID="HiddenFieldPeople" runat="server" />
    <asp:HiddenField ID="HiddenFieldPwd" runat="server" />
    <asp:HiddenField ID="HiddenFieldurl" runat="server" />
    </form>
</body>
</html>
