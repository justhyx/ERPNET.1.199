<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock_Output.aspx.cs" Inherits="ERPPlugIn.StockManager.Stock_Output" %>

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
        .td
        {
            text-align: center;
        }
        .flot
        {
            margin-left: 300px;
        }
    </style>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="800px" border="0" align="center" cellspacing="0" cellpadding="6">
            <tr>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnCommit" runat="server" CssClass="button" Text="提 交" OnClick="btnCommit_Click" />
                    <asp:Button Text="交货上传" runat="server" ID="btnUpLoad" CssClass="button" OnClick="btnUpLoad_Click" />
                </td>
            </tr>
        </table>
        <table align="center" border="1" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E"
            width="800">
            <tr>
                <td align="center" colspan="2" style="font-size: 18px; font-weight: bold;">
                    数据管理
                </td>
            </tr>
            <tr>
                <td style="width: 130px">
                    客户
                </td>
                <td>
                    <label>
                        <asp:DropDownList ID="ddlCustomers" runat="server">
                            <asp:ListItem Text="请选择" />
                        </asp:DropDownList>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    备货指示号
                </td>
                <td>
                    <asp:TextBox ID="txtPrapareID" runat="server" />
                    <asp:Button Text="验证可用性" ID="btnCheckVisible" runat="server" CssClass="flot" OnClick="btnCheckVisible_Click" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:GridView ID="gvExcel" runat="server" Width="800px" AutoGenerateColumns="False"
                        EnableModelValidation="True" OnDataBound="gvExcel_DataBound" OnRowUpdating="gvExcel_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="客户代码">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("khdm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备料指示号">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("pId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="送货日期">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Visible">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Visible") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" ID="linkId" CssClass="td" NavigateUrl='<%#"Stock_Output_Detail.aspx?Pid="+DataBinder.Eval(Container.DataItem,"pId")
                        +"&khdm="+DataBinder.Eval(Container.DataItem,"khdm") %>' Text="备料" />
                                    <asp:LinkButton Text="未满足数据" CommandName="Update" runat="server" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:GridView Width="800" runat="server" ID="gvData" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>






































































