<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventory_Begin.aspx.cs"
    Inherits="ERPPlugIn.InventoryManager.Inventory_Begin" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <asp:Literal Text="<%$ Resources:Resource, qdpd %>" runat="server" /></title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="800" border="0" align="center" cellspacing="0" cellpadding="6">
            <tr>
                <td align="right">
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="display" />
                    <asp:Button runat="server" ID="btnBegin" Text="<%$ Resources:Resource, qd %>" CssClass="button"
                        OnClick="btnBegin_Click" Height="25px" OnClientClick="return confirm('Are you sure to start inventory?');" />
                    <asp:Button Text="<%$ Resources:Resource, dypdp %>" Height="25px" runat="server"
                        OnClick="Unnamed3_Click" />
                    <asp:Button ID="btnExport" Text="<%$ Resources:Resource, dcpdp %>" runat="server"
                        OnClick="btnExport_Click" Height="25px" />
                </td>
            </tr>
        </table>
        <table width="800" border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="3" align="center" style="font-size: 18px; font-weight: bold;">
                    <asp:Literal ID="lblqdpd" runat="server" Text="<%$ Resources:Resource, qdpd %>" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="lblclck" runat="server" Text="<%$ Resources:Resource, clck %>" />：
                    <asp:DropDownList runat="server" ID="ddlMaterialStock">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Literal ID="lblqy" runat="server" Text="<%$ Resources:Resource, qy %>" />:
                    <asp:DropDownList runat="server">
                        <asp:ListItem Text="<%$ Resources:Resource, sy %>" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Literal ID="lblpdms" runat="server" Text="<%$ Resources:Resource, pdms %>" />:
                    <asp:DropDownList runat="server" Enabled="False">
                        <asp:ListItem Text="按批次盘库" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="lblpdsp" runat="server" Text="<%$ Resources:Resource, pdsp %>" />：
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Text="<%$ Resources:Resource, sy %>" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Literal ID="lblspsx" runat="server" Text="<%$ Resources:Resource, spsx %>" />:
                    <asp:DropDownList ID="DropDownList2" runat="server">
                        <asp:ListItem Text="<%$ Resources:Resource, sy %>" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Literal ID="lblbz" runat="server" Text="<%$ Resources:Resource, bz %>" />:
                    <asp:TextBox runat="server" ID="txtRemark" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView runat="server" ID="gvData" Width="800px" OnRowDataBound="gvData_RowDataBound"
                        OnSelectedIndexChanged="gvData_SelectedIndexChanged" 
                        EnableModelValidation="True" onrowdeleting="gvData_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, sc%>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                        Text="<%$ Resources:Resource, sc%>" OnClientClick="return confirm('Are you sure to delete this sheet?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="#FFFFCC" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
