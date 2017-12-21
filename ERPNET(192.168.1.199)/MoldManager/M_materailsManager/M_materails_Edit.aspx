<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="M_materails_Edit.aspx.cs"
    Inherits="ERPPlugIn.MoldManager.M_materailsManager.M_materails_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="800" border="1" align="center" cellpadding="6" cellspacing="0" runat="server"
            id="table">
            <tr>
                <td height="30" align="center" style="font-size: 16px; font-weight: bold;" colspan="6">
                    材料资料修改
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" Text="名称" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label2" Text="规格" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label3" Text="供应商" runat="server" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlVendor" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" Text="单价" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="txtPrie" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label5" Text="币种" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="txtmoneyType" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label6" Text="类型" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="txttype" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" Text="材质" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="txtCz" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label8" Text="颜色" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="txtColor" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label9" Text="简称" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="txtShotName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <table width="800" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="center">
                                <%--                                <input type="button" name="Submit2" runat="server" id="btnEdit" onclick="btnEdit_ServerClick()"
                                    style="background-image: url(../images/button1_n.jpg); font-weight: bold; height: 30px;
                                    width: 80px;" value="修 改" />--%>
                                <asp:Button Text="修改" runat="server" Height="30px" Width="80px" CssClass="button"
                                    ID="btnEdit" OnClick="btnEdit_Click" OnClientClick="return confirm('确认要保存吗？');" />
                            </td>
                            <td align="center">
                                <%--<input type="button" runat="server" id="btnCannel" name="Submit22" onclick="javascript:history.back();"
                                    style="background-image: url(../images/button1_n.jpg); font-weight: bold; height: 30px;
                                    width: 80px;" value="返 回" onclick="return btnCannel_onclick()" />--%>
                                <asp:Button Text="返回" runat="server" Height="30px" Width="80px" CssClass="button"
                                    ID="btnCannel" OnClick="btnCannel_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>





























