<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterailsStock_KeyInNoData.aspx.cs"
    Inherits="ERPPlugIn.MaterailsManager.MaterailsStock_KeyInNoData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <asp:Literal ID="Literal3" Text="<%$ Resources:Resource, pdblr%>" runat="server" />
    </title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .gv
        {
            width: 100%;
        }
        .lbhide
        {
            display: none;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function EnterTextBox() {
            if (event.keyCode == 13) {
                event.keyCode = 9;
                event.returnValue = false;
                document.getElementById("sr").click();
            }
        }   
    </script>
</head>
<body onkeypress="return EnterTextBox()">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="6">
            <tr>
                <td align="right">
                    <asp:TextBox ID="txtAlert" runat="server" Width="100%" ReadOnly="True" />
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="6">
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnSave" Text="<%$ Resources:Resource, bc %>" OnClick="btnSave_Click" />
                    <asp:Button Text="<%$ Resources:Resource, fh %>" ID="btnReturn" runat="server" OnClick="btnReturn_Click" />
                </td>
            </tr>
        </table>
        <table width="100% " border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="5" align="center" style="font-size: 18px; font-weight: bold;">
                    <asp:Literal ID="Literal1" Text="<%$ Resources:Resource, pdblr%>" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal2" Text="<%$ Resources:Resource, bpmc%>" runat="server" />
                    <asp:TextBox runat="server" ID="txtbpmc" />
                </td>
                <td>
                    <asp:Literal ID="Literal8" Text="<%$ Resources:Resource, pch%>" runat="server" />
                    <asp:TextBox ID="txtpch" runat="server" />
                </td>
                <td>
                    <asp:Literal ID="Literal9" Text="<%$ Resources:Resource, hwh%>" runat="server" />
                    <asp:TextBox ID="txthwh" runat="server" />
                </td>
                <td>
                    <asp:Literal ID="Literal11" Text="<%$ Resources:Resource, pdsl%>" runat="server" />
                    <asp:TextBox ID="txtpdsl" runat="server" />
                </td>
                <td>
                    <asp:Button Text="<%$ Resources:Resource, tj1%>" ID="btnAdd" runat="server" OnClick="btnAdd_Click" />
                </td>
            </tr>
            <tr>
                <asp:GridView runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True"
                    ID="gvAddData">
                    <Columns>
                        <asp:BoundField HeaderText="<%$ Resources:Resource, bpmc%>" DataField="pm" />
                        <asp:BoundField HeaderText="<%$ Resources:Resource, pch%>" DataField="pch" />
                        <asp:BoundField HeaderText="<%$ Resources:Resource, hwh%>" DataField="hwh" />
                        <asp:BoundField HeaderText="<%$ Resources:Resource, pdsl%>" DataField="pdsl" />
                    </Columns>
                </asp:GridView>
            </tr>
        </table>
    </div>
    <div style="display: none">
        <asp:Button Text="text" ID="sr" runat="server" OnClick="btn_Click" />
    </div>
    </form>
</body>
</html>
