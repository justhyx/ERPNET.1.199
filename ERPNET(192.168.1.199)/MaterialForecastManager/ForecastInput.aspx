<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForecastInput.aspx.cs"
    Inherits="ERPPlugIn.MaterialForecastManager.ForecastInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>数据导入</title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="2" align="center" style="font-size: 18px; font-weight: bold;">
                    <asp:Literal Text="<%$Resources:Resource,sjsc %>" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <asp:Label Text="text" runat="server" ID="lblDm" Visible="false" />
                    Date:<asp:TextBox runat="server" ID="txtDate" onclick="WdatePicker({dateFmt:'yyyy-MM'})" />
                    &nbsp;&nbsp;Client name:<asp:DropDownList runat="server" ID="txtCName">
                    </asp:DropDownList>
                    &nbsp;&nbsp;Client F/C:<asp:DropDownList runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;Ver_no:<asp:TextBox runat="server" ID="txtVer_No" />&nbsp;&nbsp;Remark:<asp:TextBox
                        runat="server" ID="txtRemark" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button Text="Query"
                            runat="server" ID="btnQuery" OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button Text="Export Excel" runat="server" 
                        ID="btnExport" onclick="btnExport_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal1" Text="<%$Resources:Resource,xzwj %>" runat="server" />
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button CssClass="button" Text="<%$Resources:Resource,yl %>" ID="btnUpload" runat="server"
                        OnClick="Unnamed2_Click" />&nbsp; &nbsp;
                    <asp:Button CssClass="button" Text="<%$Resources:Resource,fh %>" ID="Button1" runat="server"
                        OnClick="Button1_Click1" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="gvExcel" runat="server" CellPadding="4" EnableModelValidation="True"
                        ForeColor="#333333" GridLines="None" CssClass="css">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
