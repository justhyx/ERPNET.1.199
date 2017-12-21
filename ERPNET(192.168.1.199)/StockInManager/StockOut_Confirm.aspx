﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockOut_Confirm.aspx.cs"
    Inherits="ERPPlugIn.StockInManager.StockOut_Confirm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>成品出库单审核</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="6">
            <tr>
                <td align="center" style="font-weight: bold; font-size: 18">
                    <span class="style1">成品出库单审核</span>
                </td>
            </tr>
            <tr>
                <td>
                    出库单号：<asp:Label Text="text" runat="server" ID="lblNo" />
                    | 制单日期:<asp:Label Text="text" runat="server" ID="lblDate" />
                    | 制单人：<asp:Label Text="text" runat="server" ID="lblOper" />
                    | 备注:<asp:Label Text="text" runat="server" ID="lblRemark" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button Text="审核" runat="server" ID="btnConfirm" OnClick="btnConfirm_Click" OnClientClick="return confirm('确认审核?');" />
                    <asp:Button Text="取消" runat="server" ID="btnCannel" OnClick="btnCannel_Click" OnClientClick="return confirm('操作会删除此记录,确认取消?');" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        Width="100%" BorderStyle="Solid" BorderWidth="1px" BorderColor="#030303">
                        <Columns>
                            <asp:TemplateField HeaderText="序列">
                                <ItemTemplate>
                                    <asp:Label Text='<%#Container.DataItemIndex + 1%>' runat="server" ID="Label1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="部番">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("goods_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="出库数量">
                                <EditItemTemplate>
                                    <asp:TextBox ID="qty" runat="server" Text='<%#Eval("Qty") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("Qty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="货位号">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%#Eval("goodsPost") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="goodsPost" runat="server" Text='<%#Eval("goodsPost") %>'></asp:TextBox>
                                </EditItemTemplate>
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







































































































