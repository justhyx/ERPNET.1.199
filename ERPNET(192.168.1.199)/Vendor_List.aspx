<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vendor_List.aspx.cs" Inherits="ERPPlugIn._Default" %>

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
        }
    </style>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: White">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="6">
            <tr>
                <td>
                    <asp:Literal Text="<%$ Resources:Resource,qsrgysm%>" runat="server" />
                    <label>
                        <input type="text" name="TxtKey" runat="server" id="txtName" />
                        <asp:Button Text="<%$ Resources:Resource,tj%>" runat="server" value="<%$ Resources:Resource,tj%>" ID="btnSearch" OnClick="btnSearch_Click" />
                    </label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnDeleteManager" Text="<%$ Resources:Resource,ysc%>" CssClass="button" OnClick="btnDeleteManager_Click" />
                    <asp:Button Text="<%$ Resources:Resource,tjgys%>" runat="server" CssClass="button" OnClick="Unnamed1_Click" />
                </td>
            </tr>
        </table>
        <table width="800" border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="5" align="center" style="font-size: 18px; font-weight: bold;">
                    <asp:Literal Text="<%$ Resources:Resource,ggsgl%>" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView runat="server" ID="dgvVenderList" Width="800px" border="1" align="center"
                        CellPadding="4" BorderColor="#7E7E7E" AutoGenerateColumns="False" EnableModelValidation="True"
                        AllowPaging="True" OnPageIndexChanging="dgvVenderList_PageIndexChanging" PageSize="20"
                        OnRowDeleting="dgvVenderList_RowDeleting" OnDataBound="dgvVenderList_DataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource,gysbh%>">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("VendorID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource,gysm%>">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("VenderName") %>'>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource,gysjc%>">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("VenderShortName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource,caozuo%>" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text="<%$ Resources:Resource,ck%>" NavigateUrl='<%# "Vender_Edit.aspx?Id="+DataBinder.Eval(Container.DataItem,"VendorID")+"&style=1"%>'></asp:HyperLink>|
                                    <asp:LinkButton ID="LinkButton1" CommandName="Delete" OnClientClick="return confirm('Are you sure to delete this item?')"
                                        runat="server" Text="<%$ Resources:Resource,sc%>"></asp:LinkButton>|
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







































































