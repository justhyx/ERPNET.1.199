﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="M_materails_OrderList.aspx.cs"
    Inherits="ERPPlugIn.MoldManager.M_materailsOrder.M_materails_OrderList" %>

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
                    &nbsp;&nbsp;&nbsp; 模具号
                    <asp:TextBox ID="txtMNo" runat="server" />
                </td>
                <td align="right">
                    <asp:Button runat="server" ID="btnSearch" Text="搜索" OnClick="btnSearch_Click" />
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
                    <asp:GridView runat="server" ID="gvData" CssClass="gv" AutoGenerateColumns="False"
                        EnableModelValidation="True">
                        <Columns>
                            <asp:TemplateField HeaderText="申请单号">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"M_materails_OrderPurchase.aspx?id="+DataBinder.Eval(Container.DataItem,"apply_no")
                                        +"&Mno="+DataBinder.Eval(Container.DataItem,"mode_no")
                                    %>' Text='<%# Eval("apply_no") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
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
        </table>
    </div>
    </form>
</body>
</html>


























