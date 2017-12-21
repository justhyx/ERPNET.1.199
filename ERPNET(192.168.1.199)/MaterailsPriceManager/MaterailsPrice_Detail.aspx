<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterailsPrice_Detail.aspx.cs"
    Inherits="ERPPlugIn.MaterailsPriceManager.MaterailsPrice_Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="800" border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td align="center" style="font-size: 18px; font-weight: bold;">
                    材料价格修改详细
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvDataDetail" runat="server" Width="800px" CellPadding="4" 
                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <a href="javascript:history.back()">返回上一页</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>












































































































