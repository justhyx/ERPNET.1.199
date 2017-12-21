<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterailsPrice_index.aspx.cs"
    Inherits="ERPPlugIn.MaterailsPriceManager.MaterailsPrice_index" %>

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
    <%--<script type="text/javascript">
                 var s = <%= getJson()%>;
    </script>--%>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="6">
            <tr>
                <td>
                    部番
                    <label>
                        <input type="text" name="TxtKey" runat="server" id="txtName" />
                    </label>
                    供应商
                    <label>
                        <input type="text" name="TxtKey" runat="server" id="txtSccj" />
                        <asp:Button Text="收索" runat="server" ID="btnSearchVendor" OnClick="btnSearchVendor_Click" />
                        <asp:DropDownList runat="server" Width="200px" ID="ddlVendorList">
                            <asp:ListItem Text="请选择" Value="0" />
                        </asp:DropDownList>
                    </label>
                    <asp:Button Text="提交" runat="server" value="提交" ID="btnSearch" OnClick="btnSearch_Click" />
<%--                    <input type="button" value="test" onclick="getJosnData()" />--%>
                    <input type="text" name="TxtKey" runat="server" id="Hf" style="display: none" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnA" Text="导入" CssClass="button" OnClick="btnA_Click" />
                </td>
            </tr>
        </table>
        <table width="800" border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="5" align="center" style="font-size: 18px; font-weight: bold;">
                    材料管理
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView runat="server" ID="dgvMaterailList" Width="800px" border="1" align="center"
                        CellPadding="4" AutoGenerateColumns="False" EnableModelValidation="True" AllowPaging="True"
                        PageSize="15" OnPageIndexChanging="dgvMaterailList_PageIndexChanging" OnRowCancelingEdit="dgvMaterailList_RowCancelingEdit"
                        OnRowEditing="dgvMaterailList_RowEditing" OnRowUpdating="dgvMaterailList_RowUpdating"
                        ForeColor="#333333" GridLines="None" AllowSorting="True" OnSorting="dgvMaterailList_Sorting">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="部番" SortExpression="name">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("name") %>'>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="供应商" SortExpression="sccj">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("sccj") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单价" SortExpression="price">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("price") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPrice" runat="server" Text='<%# Eval("price") %>' Width="50px"
                                        Font-Size="11px" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="币种" SortExpression="currency">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlcurrency" runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("currency") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="汇率" SortExpression="exchangeRate">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("exchangeRate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" HeaderText="操作">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="修改详细">
                                <ItemTemplate>
                                    <asp:HyperLink NavigateUrl='<%#"MaterailsPrice_Detail.aspx?name="+DataBinder.Eval(Container.DataItem,"name")%>'
                                        runat="server" Text="查看" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </td>
            </tr>
            <%--<script type="text/javascript">
                function getJson() {
                    for (var i = 0; i < s.table.length; i++) {
                        with (s.table[i]) {
                            var info = "部番:" + name;
                        }
                    }
                }
            </script>--%>
        </table>
    </div>
    </form>
</body>
</html>










































































































