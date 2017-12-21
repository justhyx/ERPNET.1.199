<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterailsStock_Confirm.aspx.cs"
    Inherits="ERPPlugIn.MaterailsManager.MaterailsStock_Confirm" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><asp:Literal ID="Literal3" Text="<%$ Resources:Resource, pdbqr %>" runat="server" /></title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="6">
            <tr>
                <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="lbhide">LinkButton</asp:LinkButton>
                    <asp:Literal ID="Literal2" Text="<%$ Resources:Resource, pdrq %>" runat="server" />:<asp:TextBox
                        runat="server" ID="txtStartDate" onClick="WdatePicker()" />
                </td>
                <td align="right">
                    <asp:Button runat="server" ID="btnSearch" Text="<%$ Resources:Resource, btnSearch %>"
                        OnClick="btnSearch_Click" />
                    <asp:Button runat="server" ID="btnConfirm" Text="<%$ Resources:Resource, qr %>" OnClick="btnConfirm_Click"
                        OnClientClick="return confirm('Are you sure to confirm this Inventory sheet?');" />
                    <asp:Button runat="server" ID="btnReCon" Text="<%$ Resources:Resource, fs %>" OnClick="btnReCon_Click"
                        OnClientClick="return confirm('Are you sure to reconfirm this Inventory sheet?');" />
                    <asp:Button runat="server" ID="btnConfirmAll" Text="<%$ Resources:Resource, qrdq %>"
                        OnClick="btnConfirmAll_Click" OnClientClick="return confirm('Are you sure to confirm all the Inventory sheets?');" />
                    <asp:Button runat="server" ID="btnReConAll" Text="<%$ Resources:Resource, fsdq %>"
                        OnClick="btnReConAll_Click" OnClientClick="return confirm('Are you sure to  reconfirm all the Inventory sheets?');" />
                    <asp:Button runat="server" ID="btnDelete" Text="<%$ Resources:Resource, sc %>" OnClick="btnDelete_Click"
                        OnClientClick="return confirm('Are you sure to delete this Inventory sheet?');" />
                    <asp:Button Text="<%$ Resources:Resource, cy %>" runat="server" ID="btnExport" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <table width="100% " border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="3" align="center" style="font-size: 18px; font-weight: bold;">
                    <asp:Literal ID="Literal1" Text="<%$ Resources:Resource, pdbqr %>" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView runat="server" ID="gvData" OnRowDataBound="gvData_RowDataBound" OnSelectedIndexChanged="gvData_SelectedIndexChanged"
                        CssClass="gv" AutoGenerateColumns="False" EnableModelValidation="True">
                        <Columns>
                            <asp:BoundField DataField="Bill_Id" HeaderText="Id" FooterStyle-CssClass="lbhide"
                                HeaderStyle-CssClass="lbhide" ItemStyle-CssClass="lbhide">
                                <FooterStyle CssClass="lbhide"></FooterStyle>
                                <HeaderStyle CssClass="lbhide"></HeaderStyle>
                                <ItemStyle CssClass="lbhide"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="store_id" HeaderText="<%$ Resources:Resource, clck %>" />
                            <asp:BoundField DataField="pk_date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Resource, pdrq %>" />
                            <asp:BoundField DataField="Pk_id" HeaderText="<%$ Resources:Resource, pddh %>" />
                            <asp:BoundField DataField="Bill_no" HeaderText="<%$ Resources:Resource, pdbh %>" />
                            <asp:BoundField DataField="Crt_emp" HeaderText="<%$ Resources:Resource, zdr %>" />
                            <asp:BoundField DataField="Crt_Date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Resource, pdbzdrq %>" />
                            <asp:BoundField DataField="Remark" HeaderText="<%$ Resources:Resource, bz %>" />
                            <asp:CheckBoxField DataField="Status" HeaderText="<%$ Resources:Resource, sh %>"
                                ReadOnly="True" />
                        </Columns>
                        <SelectedRowStyle BackColor="#FFFF99" />
                    </asp:GridView>
                </td>
            </tr>
            <hr />
            <tr>
                <td colspan="3">
                    <asp:GridView runat="server" ID="gvDetailData" CssClass="gv" AutoGenerateColumns="False"
                        EnableModelValidation="True" OnRowDataBound="gvDetailData_RowDataBound" ShowFooter="True">
                        <Columns>
                            <asp:BoundField DataField="dm" HeaderText="<%$ Resources:Resource, bpmc %>" />
                            <asp:BoundField DataField="spec" HeaderText="<%$ Resources:Resource, gg %>" />
                            <asp:BoundField DataField="texture" HeaderText="<%$ Resources:Resource, cz %>" />
                            <asp:BoundField DataField="color" HeaderText="<%$ Resources:Resource, ys %>" />
                            <asp:BoundField DataField="unit" HeaderText="<%$ Resources:Resource, dw %>" />
                            <asp:BoundField DataField="pch" HeaderText="<%$ Resources:Resource, pch %>" />
                            <asp:BoundField DataField="hwh" HeaderText="<%$ Resources:Resource, hwh %>" />
                            <asp:BoundField DataField="qty" DataFormatString="{0:0.##}" HeaderText="<%$ Resources:Resource, pdsl %>" />
                            <asp:BoundField DataField="price" DataFormatString="{0:0.##}" HeaderText="<%$ Resources:Resource, dj %>" />
                            <asp:BoundField DataField="total" DataFormatString="{0:0.##}" HeaderText="<%$ Resources:Resource, je %>" />
                            <asp:BoundField DataField="vendor_name" HeaderText="<%$ Resources:Resource, kh %>" />
                            <asp:CheckBoxField DataField="is_can_sale" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>







































































