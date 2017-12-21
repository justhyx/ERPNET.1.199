<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock_Input.aspx.cs" Inherits="ERPPlugIn.StockManager.Stock_Input" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .button
        {
            background-image: url(../images/button1_n.jpg);
            font-weight: bold;
            height: 30px;
            width: 80px;
        }
    </style>
    <script src="../../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: white">
    <form id="form1" runat="server">
    <div>
        <table width="800" border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="2" align="center" style="font-size: 18px; font-weight: bold;">
                    <asp:Literal ID="Literal1" Text="<%$Resources:Resource,sjsc %>" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 130px">
                    <asp:Literal ID="Literal2" Text="<%$Resources:Resource,kh %>" runat="server" />
                </td>
                <td>
                    <label>
                        <asp:DropDownList runat="server" ID="ddlCustomers">
                            <asp:ListItem Text="<%$Resources:Resource,qxz %>" />
                        </asp:DropDownList>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal3" Text="<%$Resources:Resource,shrq %>" runat="server" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtdelivery_date" OnClick="WdatePicker()" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal4" Text="<%$Resources:Resource,bhzsh %>" runat="server" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtPrapareID" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal5" Text="<%$Resources:Resource,xzwj %>" runat="server" />
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
                    <asp:GridView ID="gvExcel" runat="server" Width="800px" CellPadding="4" EnableModelValidation="True"
                        ForeColor="#333333" GridLines="None">
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






































































