<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventory_FinalConfirm.aspx.cs"
    Inherits="ERPPlugIn.InventoryManager.Inventory_FinalConfirm" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><asp:Literal ID="Literal2" Text="<%$ Resources:Resource, pdbsh%>" runat="server" /></title>
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
    <script type="text/javascript">
        function submitTest(btn) {
            var gnl = (confirm("库单一经审核,便不可更改,您确认库单内容完全正确吗?"));
            if (gnl == true) {
                btn.value = "处理中...";
                btn.onclick = onDealing;
            }
        }
        function onDealing() {
            alert('系统处理中,请稍候...');
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="6">
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnConfirm" Text="<%$ Resources:Resource, sh%>" OnClick="btnConfirm_Click"
                        OnClientClick=" return confirm('After approved, you cannot modify any more,are you sure the content is correct?');" />
                    <asp:Button runat="server" ID="btnDelete" Text="<%$ Resources:Resource, sc%>" OnClick="btnDelete_Click"
                        OnClientClick="return confirm('Are you sure to delete this sheet?');" />
                </td>
            </tr>
        </table>
        <table width="100% " border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="3" align="center" style="font-size: 18px; font-weight: bold;">
                    <asp:Literal Text="<%$ Resources:Resource, pdbsh%>" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Literal ID="Literal1" Text="<%$ Resources:Resource, clck%>" runat="server" />：
                    <asp:DropDownList runat="server" ID="ddlMaterialStock" OnSelectedIndexChanged="ddlMaterialStock_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView runat="server" ID="gvData" OnRowDataBound="gvData_RowDataBound" OnSelectedIndexChanged="gvData_SelectedIndexChanged"
                        CssClass="gv" EnableModelValidation="True">
                        <SelectedRowStyle BackColor="#FFFF99" />
                    </asp:GridView>
                </td>
            </tr>
            <tr style="border: 0px">
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView runat="server" ID="gvDetailData" CssClass="gv" AutoGenerateColumns="False"
                        EnableModelValidation="True" OnRowDataBound="gvDetailData_RowDataBound" ShowFooter="True">
                        <Columns>
                            <asp:BoundField DataField="dm" HeaderText="<%$ Resources:Resource, bpmc%>" />
                            <asp:BoundField DataField="spec" HeaderText="<%$ Resources:Resource, gg%>" />
                            <asp:BoundField DataField="cz" HeaderText="<%$ Resources:Resource, cz%>" />
                            <asp:BoundField DataField="ys" HeaderText="<%$ Resources:Resource, ys%>" />
                            <asp:BoundField DataField="goods_unit" HeaderText="<%$ Resources:Resource, dw%>" />
                            <asp:BoundField DataField="pch" HeaderText="<%$ Resources:Resource, pch%>" />
                            <asp:BoundField DataField="hwh" HeaderText="<%$ Resources:Resource, hwh%>" />
                            <asp:BoundField DataField="zmsl" DataFormatString="{0:0.##}" HeaderText="现账面数量" />
                            <asp:BoundField DataField="pdsl" DataFormatString="{0:0.##}" HeaderText="<%$ Resources:Resource, pdsl%>" />
                            <asp:BoundField DataField="str_in_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="<%$ Resources:Resource, pdrq%>" />
                            <asp:BoundField DataField="zmsl1" DataFormatString="{0:0.##}" HeaderText="原始账面数量" />
                            <asp:BoundField DataField="customer_name" HeaderText="<%$ Resources:Resource, kh%>" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>





































































