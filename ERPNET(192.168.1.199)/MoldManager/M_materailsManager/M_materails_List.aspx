<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="M_materails_List.aspx.cs"
    Inherits="ERPPlugIn.MoldManager.M_materailsManager.M_materails_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: White">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="1" cellspacing="0" cellpadding="6">
            <tr>
                <td>
                    <asp:Literal ID="Literal1" Text="名称：" runat="server" />
                    <label>
                        <input type="text" name="TxtKey" runat="server" id="txtName" />
                        <asp:Button Text="提交" runat="server" value="提交" ID="btnSearch" OnClick="btnSearch_Click" />
                    </label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table width="800" border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="5" align="center" style="font-size: 18px; font-weight: bold;">
                    <asp:Literal ID="Literal2" Text="材料管理" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView runat="server" ID="dgvVenderList" Width="800px" border="1" align="center"
                        CellPadding="4" BorderColor="#7E7E7E" AutoGenerateColumns="False" EnableModelValidation="True"
                        AllowPaging="True" OnPageIndexChanging="dgvVenderList_PageIndexChanging" PageSize="20"
                        OnRowDeleting="dgvVenderList_RowDeleting" OnDataBound="dgvVenderList_DataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="id">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="名称">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("mc") %>'>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="规格">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("gg") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单价">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("dj") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="币种">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("bz") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="类型">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("lx") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="材质">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("cz") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="颜色">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("ys") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="简称">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("jc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="供应商">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("gys") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text="修改" NavigateUrl='<%# "M_materails_Edit.aspx?Id="+DataBinder.Eval(Container.DataItem,"id")+"&style=1"%>'></asp:HyperLink>|
                                    <asp:LinkButton ID="LinkButton1" CommandName="Delete" OnClientClick="return confirm('Are you sure to delete this item?')"
                                        runat="server" Text="删除"></asp:LinkButton>|
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





























