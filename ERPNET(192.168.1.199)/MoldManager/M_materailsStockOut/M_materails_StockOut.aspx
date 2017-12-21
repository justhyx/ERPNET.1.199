<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="M_materails_StockOut.aspx.cs"
    Inherits="ERPPlugIn.MoldManager.M_materailsStockOut.M_materails_StockOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .lbhide
        {
            display: none;
        }
    </style>
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
                    <asp:Button ID="btnReturn" runat="server" Text="返回" OnClick="btnReturn_Click" />
                </td>
            </tr>
        </table>
        <table width="800" border="1" align="center" style="border-color: #7E7E7E" cellpadding="3"
            cellspacing="0">
            <tr id="trgvList" runat="server">
                <td colspan="2">
                    <asp:GridView runat="server" ID="gvDetailData" Width="100%" CssClass="gv" AutoGenerateColumns="False"
                        EnableModelValidation="True">
                        <Columns>
                            <%--<asp:TemplateField HeaderText="货位号"> 
                                <ItemTemplate>
                                    <asp:TextBox ID="txtHwh" runat="server"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>--%>
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
                            <asp:BoundField DataField="hwh" HeaderText="货位号" />
                            <asp:BoundField DataField="qty" HeaderText="库存" DataFormatString="{0:0}" />
                            <asp:TemplateField HeaderText="出库数量">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPrice" runat="server" Text='<%#Eval("qty") %>' onkeypress="if((event.keyCode<48 ||event.keyCode>57) &&event.keyCode!=46)event.returnValue=false;"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
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






















