<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mode_List.aspx.cs" Inherits="ERPPlugIn.Mold_Management_Accounting.Mode_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模具管理系统</title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .ui-helper-hidden-accessible
        {
            display: none;
            text-align: left;
        }
        li
        {
            list-style-type: none;
            text-align: left;
        }
        .c
        {
        }
        #content
        {
            margin: 0 auto;
            width: 99%;
            position: absolute;
            top: 5px;
            bottom: 0px;
            left: 5px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#tags").autocomplete({
                minLength: 1, source: function (request, response) {
                    $.ajax({
                        type: "GET",
                        url: "getMode.ashx?keyword=" + request.term,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",

                        success: function (data) {
                            response($.map(data, function (item) {
                                return { value: item };
                            }));
                        }
                    });
                }
            })
        });
        function openNewForm() {
            window.open('Mode_Index.aspx', 'newwindow');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="content" class="c" style="border: 1px solid black; height: 99%;">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td height="30" align="center" style="font-size: 16px; font-weight: bold;">
                    模具管理列表
                </td>
            </tr>
        </table>
        <div style="margin-bottom: 30px; margin-top: 20px">
            <table width="98%" border="0" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                <tr>
                    <td>
                        模具型号 &nbsp;<input type="text" id="tags" runat="server" />&nbsp;&nbsp;客户订单接收日期<asp:TextBox
                            ID="txtCyear" runat="server" Width="40" onclick="WdatePicker({dateFmt:'yyyy'})" />年<asp:TextBox
                                ID="txtCmoth" runat="server" Width="20" onclick="WdatePicker({dateFmt:'MM'})" />月<asp:TextBox
                                    ID="txtCday" runat="server" Width="20" onclick="WdatePicker({dateFmt:'dd'})" />日&nbsp;&nbsp;&nbsp;&nbsp;送货日期
                        <asp:TextBox ID="txtSyear" runat="server" Width="40" onclick="WdatePicker({dateFmt:'yyyy'})" />年<asp:TextBox
                            ID="txtSmoth" runat="server" Width="20" onclick="WdatePicker({dateFmt:'MM'})" />月<asp:TextBox
                                ID="txtSday" runat="server" Width="20" onclick="WdatePicker({dateFmt:'dd'})" />日&nbsp;<asp:Button
                                    Text="查询" ID="btnQuery" runat="server" OnClick="btnQuery_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="90%" border="0" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                <tr>
                    <td align="right">
                        <asp:Button Text="新增" runat="server" ID="btnAdd" OnClientClick="openNewForm()" />
                    </td>
                </tr>
            </table>
        </div>
        <hr />
        <div style="margin-bottom: 20px; margin-top: 20px" runat="server" id="div">
            <table width="98%" border="1" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                <tr>
                    <td>
                        <asp:GridView runat="server" ID="gvData" Width="100%" EnableModelValidation="True"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="Id">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" CssClass="a" ID="lnkGoodsName" Text='<%#Bind("id") %>'
                                            NavigateUrl='<%#"Mode_Modify.aspx?id="+DataBinder.Eval(Container.DataItem,"id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Mode_type_name" HeaderText="类型" />
                                <asp:BoundField DataField="mode" HeaderText="模具型号" />
                                <asp:BoundField DataField="goods_name" HeaderText="部品名称" />
                                <asp:BoundField DataField="goods_no" HeaderText="部品号" />
                                <asp:BoundField DataField="qty" HeaderText="数量" />
                                <asp:BoundField DataField="quotation_number" HeaderText="报价单番号" />
                                <asp:BoundField DataField="price" HeaderText="单价（未税）" />
                                <asp:BoundField DataField="rate" HeaderText="税金" />
                                <asp:BoundField DataField="total" HeaderText="金额" />
                                <asp:BoundField DataField="tax_rate" HeaderText="税率" />
                                <asp:BoundField DataField="currency" HeaderText="币种" />
                                <asp:BoundField DataField="order_receipt_date" HeaderText="客户订单接收日期" />
                                <asp:BoundField DataField="delivery_date" HeaderText="送货日期" />
                                <asp:TemplateField HeaderText="内容备注">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#BindTitle( Eval("content_remark"),14) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
