<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="M_materails_Apply_Confirm.aspx.cs"
    Inherits="ERPPlugIn.MoldManager.M_materailsApply.M_materails_Apply_Confirm" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
                    <asp:Literal ID="Literal1" Text="申请日期" runat="server" />:<asp:TextBox runat="server"
                        ID="txtStartDate" onClick="WdatePicker()" />
                </td>
                <td align="right">
                    <asp:Button runat="server" ID="btnSearch" Text="搜索" OnClick="btnSearch_Click" />
                    <asp:Button runat="server" ID="btnConfirm" Text="审核" OnClick="btnConfirm_Click" OnClientClick="return confirm('Are you sure to confirm this sheet?');" />
                </td>
            </tr>
        </table>
        <table width="100% " border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="3" align="center" style="font-size: 18px; font-weight: bold;">
                    <asp:Literal ID="Literal2" Text="申请单审核" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView runat="server" ID="gvData" OnRowDataBound="gvData_RowDataBound" OnSelectedIndexChanged="gvData_SelectedIndexChanged"
                        CssClass="gv" AutoGenerateColumns="False" EnableModelValidation="True">
                        <Columns>
                            <asp:BoundField DataField="apply_no" HeaderText="申请单号" FooterStyle-CssClass="lbhide"
                                HeaderStyle-CssClass="lbhide" ItemStyle-CssClass="lbhide"></asp:BoundField>
                            <asp:BoundField DataField="mode_no" HeaderText="模具号" />
                            <asp:BoundField DataField="internal_no" HeaderText="社内编号" />
                            <asp:BoundField DataField="apply_date" HeaderText="申请日期" />
                            <asp:BoundField DataField="apply_by" HeaderText="申请人" />
                            <asp:BoundField DataField="remark" HeaderText="备注" />
                        </Columns>
                        <SelectedRowStyle BackColor="#FFFF99" />
                    </asp:GridView>
                </td>
            </tr>
            <hr />
            <tr>
                <td colspan="3">
                    <asp:GridView runat="server" ID="gvDetailData" CssClass="gv" AutoGenerateColumns="False"
                        EnableModelValidation="True" ShowFooter="True">
                        <Columns>
                            <asp:TemplateField HeaderText="序列">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" Text="<%#Container.DataItemIndex+1 %>" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="name" HeaderText="名称" />
                            <asp:BoundField DataField="texture" HeaderText="材质" />
                            <asp:BoundField DataField="spec" HeaderText="规格" />
                            <asp:BoundField DataField="qty" HeaderText="数量" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>


























