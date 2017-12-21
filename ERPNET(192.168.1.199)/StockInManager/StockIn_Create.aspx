<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockIn_Create.aspx.cs"
    Inherits="ERPPlugIn.StockInManager.StockIn_Create" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>入库制单</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <style type="text/css">
        span input
        {
            text-align: center;
            border: 0px 0px 0px 0px;
        }
        .style1
        {
            width: 93px;
        }
        .button
        {
            background-image: url(../images/button1_n.jpg);
            font-weight: bold;
            height: 30px;
            width: 80px;
        }
        .ui-helper-hidden-accessible
        {
            display: none;
        }
        .lbhide
        {
            display: none;
        }
    </style>
    <script type="text/javascript" language="javascript">

        function EnterTextBox(button) {

            if (event.keyCode == 13 && document.all["txtgoodsName"].value != "") {

                event.keyCode = 9;

                event.returnValue = false;

                document.all[button].click();

            }

        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("#txtgoodsName").autocomplete({
                minLength: 1, source: function (request, response) {
                    $.ajax({
                        type: "GET",
                        url: "StockIn_GoodsData.ashx?keyword=" + request.term,
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="800" border="1" align="center" style="border-color: #7E7E7E" cellpadding="3"
            cellspacing="0">
            <tr>
                <td colspan="5" align="center" style="font-size: 18px; font-weight: bold;">
                    成品入库制单
                </td>
            </tr>
            <tr>
                <td class="style1">
                    入库方式:
                </td>
                <td>
                    <span id="rblcellstat" style="display: inline-block; width: 240px;">
                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="flow"
                            ID="rbtType" AutoPostBack="True" OnSelectedIndexChanged="rbtType_SelectedIndexChanged">
                            <asp:ListItem Text="期初入库" Value="1" />
                            <asp:ListItem Text="生产入库" Value="C" />
                        </asp:RadioButtonList>
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="lbhide">LinkButton</asp:LinkButton>
                    </span>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    仓位点:
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlMaterialStock" AutoPostBack="True" OnSelectedIndexChanged="ddlMaterialStock_SelectedIndexChanged">
                        <asp:ListItem Text="无" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    入库日期:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDate" onClick="WdatePicker()" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    入库单号:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtNo" />
                </td>
            </tr>
            <tr runat="server" id="trGoodsName">
                <td class="style1">
                    部番:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtgoodsName" />
                </td>
            </tr>
            <tr style="display: none">
                <td>
                    <asp:Button Text="查询" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    备注:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtRemark" Width="690px" />
                </td>
            </tr>
            <tr id="trgvList" runat="server">
                <td colspan="2">
                    <asp:GridView Width="100%" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        ID="gvList" onrowdatabound="gvList_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="ID" DataField="dictate_id"></asp:BoundField>
                            <asp:BoundField HeaderText="部番" DataField="goods_name" />
                            <asp:BoundField HeaderText="计划日期" DataField="dictate_date" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField HeaderText="计划数量" DataField="prd_qty" />
                            <asp:BoundField HeaderText="机台" DataField="equip_name" />
                        </Columns>
                        <SelectedRowStyle BackColor="#FFFF99" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button Text="添 加" ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>







































































































