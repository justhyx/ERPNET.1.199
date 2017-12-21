<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockIn_ConfirmList.aspx.cs"
    Inherits="ERPPlugIn.StockInManager.StockIn_ConfirmList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>成品入库单审核</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="800" border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="5" align="center" style="font-size: 18px; font-weight: bold;">
                    成品入库单审核
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView runat="server" ID="gvData" Width="800px" border="1" align="center"
                        CellPadding="4" BorderColor="#7E7E7E" AutoGenerateColumns="False" EnableModelValidation="True"
                        OnRowDataBound="gvData_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="序列">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="ID" DataField="str_in_bill_id" />
                            <asp:BoundField DataField="str_in_bill_no" HeaderText="入库单号" />
                            <asp:BoundField HeaderText="入库方式" DataField="str_in_type_name" />
                            <asp:BoundField HeaderText="制单日期" DataField="operator_date" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField HeaderText="制单人" DataField="operator_name" />
                            <asp:BoundField HeaderText="备注" DataField="remark" />
                            <asp:TemplateField HeaderText="操作" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text="查看" NavigateUrl='#'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>







































































































