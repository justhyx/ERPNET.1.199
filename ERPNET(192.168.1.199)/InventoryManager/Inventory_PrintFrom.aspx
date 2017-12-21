<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventory_PrintFrom.aspx.cs"
    Inherits="ERPPlugIn.InventoryManager.Inventory_PrintFrom" %>

<object id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0"
    width="0">
</object>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body onload="document.all.WebBrowser.ExecWB(7,1)">
    <form id="form1" runat="server">
    <div>
        <asp:GridView runat="server" ID="dgvData" Width="99%" AutoGenerateColumns="False"
            EnableModelValidation="True">
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="goods_name" HeaderText="<%$ Resources:Resource, bpmc%>">
                    <ItemStyle Width="130px" />
                </asp:BoundField>
                <asp:BoundField DataField="spec" HeaderText="<%$ Resources:Resource, gg%>"></asp:BoundField>
                <asp:BoundField DataField="ys" HeaderText="<%$ Resources:Resource, ys%>" />
                <asp:BoundField DataField="hwh" HeaderText="<%$ Resources:Resource, hwh%>" />
                <asp:BoundField DataField="pch" HeaderText="<%$ Resources:Resource, pch%>" />
                <asp:BoundField HeaderText="<%$ Resources:Resource, bpmc%>">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="修正">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>





































































