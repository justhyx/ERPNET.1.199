<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vender_DeletedItems.aspx.cs"
    Inherits="ERPPlugIn.Vender_DeletedItems" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .button
        {
            background-image: url(../images/button1_n.jpg);
            font-weight: bold;
        }
    </style>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: White">
    <form id="form1" runat="server">
    <div>
        <table width="800" border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="5" align="center" style="font-size: 18px; font-weight: bold;">
                    <asp:Literal ID="Literal1" Text="<%$ Resources:Resource,ggsgl%>" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView runat="server" ID="dgvVenderList" Width="800px" border="1" align="center"
                        CellPadding="4" BorderColor="#7E7E7E" AutoGenerateColumns="False" EnableModelValidation="True"
                        AllowPaging="True" PageSize="20" OnRowDeleting="dgvVenderList_RowDeleting" OnRowUpdating="dgvVenderList_RowUpdating">
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
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text="<%$ Resources:Resource,ck%>"
                                        NavigateUrl='<%# "Vender_Edit.aspx?Id="+DataBinder.Eval(Container.DataItem,"VendorID")+"&style=2" %>'></asp:HyperLink>
                                    |<asp:LinkButton ID="LinkButton2" CommandName="Update" OnClientClick="return confirm('Are you sure to restore this item')"
                                        runat="server" Text="<%$ Resources:Resource,hf%>"></asp:LinkButton>|
                                    <asp:LinkButton ID="LinkButton1" CommandName="Delete" OnClientClick="return confirm('Are you sure to completely remove this item')"
                                        runat="server" Text="<%$ Resources:Resource,sc%>"></asp:LinkButton>|
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="5" align="right" style="font-size: 18px; font-weight: bold;">
                    <asp:Button Text="<%$ Resources:Resource,fh%>" runat="server" Height="30px" Width="80px"
                        CssClass="button" ID="btnCannel" OnClick="btnCannel_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>







































































