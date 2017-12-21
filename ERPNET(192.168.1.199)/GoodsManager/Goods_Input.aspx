<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Goods_Input.aspx.cs" Inherits="ERPPlugIn.GoodsManager.Goods_Input" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据导入</title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .button
        {
            background-image: url(../images/button1_n.jpg);
            font-weight: bold;
            height: 30px;
            width: 80px;
        }
        .css
        {
            width: 100%;
        }
        .box
        {
            height: 75%;
            position: absolute;
            width: 100%;
            border-width: 1px;
            border-style: solid;
            margin-top: 5px;
        }
        .style1
        {
            height: 41px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="2" align="center" style="font-size: 18px; font-weight: bold;">
                    数据上传
                </td>
            </tr>
            <tr>
                <td class="style1">
                    客户
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCustomer">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    选择文件
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <a href="../DownLoadFile/部品上传模板.xls" style="text-decoration: underline; color: Blue">
                        下载部品上传模板.xls</a>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" class="style1">
                    <asp:Button CssClass="button" Text="预 览" ID="btnUpload" runat="server" OnClick="Unnamed2_Click" />
                    &nbsp; &nbsp;
                    <asp:Button CssClass="button" Text="返 回" ID="Button1" runat="server" OnClick="Button1_Click1" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ScrollBars="Both" CssClass="box">
            <asp:GridView ID="gvExcel" runat="server" CellPadding="4" EnableModelValidation="True"
                ForeColor="#333333" GridLines="Both" CssClass="css" Width="150%">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
