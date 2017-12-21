<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="M_materails_OrderPurchase.aspx.cs"
    Inherits="ERPPlugIn.MoldManager.M_materailsOrder.M_materails_OrderPurchase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function checkAll() {
            var checklist = document.getElementsByTagName("input");
            for (var i = 0; i < checklist.length; i++) {
                if (checklist[i].type == "checkbox") {
                    checklist[i].checked = document.form1.ck.checked;
                }
            }
        } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="6">
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnConfirm" Text="保存" OnClick="btnConfirm_Click" OnClientClick="return confirm('Are you sure to confirm this sheet?');" />
                    <asp:Button ID="btnReturn" runat="server" Text="返回" onclick="btnReturn_Click" />
                </td>
            </tr>
        </table>
        <table width="800" border="1" id="table" align="center" cellpadding="6" cellspacing="0"
            runat="server" style="border-color: black">
            <tr>
                <td colspan="2" align="center" style="font-size: 18px; font-weight: bold;">
                    材料采购单
                </td>
            </tr>
            <tr>
                <td>
                    供应商:
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlVendor">
                    </asp:DropDownList>
                    币种
                    <asp:DropDownList runat="server" ID="ddlwb">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    采购日期:
                </td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" onClick="WdatePicker()" />
                </td>
            </tr>
            <tr>
                <td>
                    备注:
                </td>
                <td>
                    <asp:TextBox ID="txtRemark" runat="server" Height="76px" TextMode="MultiLine" Width="716px" />
                </td>
            </tr>
        </table>
        <table width="100% " border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="3" align="center" style="font-size: 18px; font-weight: bold;">
                    <asp:Literal ID="Literal2" Text="材料列表" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView runat="server" ID="gvDetailData" CssClass="gv" AutoGenerateColumns="False"
                        EnableModelValidation="True">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    全选<input id="ck" type="checkbox" onclick="checkAll();" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="checkbox1" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="序列">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" Text="<%#Container.DataItemIndex+1 %>" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="name" HeaderText="名称" />
                            <asp:BoundField DataField="texture" HeaderText="材质" />
                            <asp:BoundField DataField="spec" HeaderText="规格" />
                            <asp:BoundField DataField="qty" HeaderText="数量" />
                            <asp:TemplateField HeaderText="价格">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPrice" runat="server" onkeypress="if((event.keyCode<48 ||event.keyCode>57) &&event.keyCode!=46)event.returnValue=false;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Id">
                            <ControlStyle CssClass="lbhide" />
                            <HeaderStyle CssClass="lbhide" />
                            <ItemStyle CssClass="lbhide" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>


























