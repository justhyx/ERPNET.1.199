<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockOut_AddData.aspx.cs"
    Inherits="ERPPlugIn.StockInManager.StockOut_AddData" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>成品出库制单</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#txtGoodsName").autocomplete({
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

        function EnterTextBox(button) {

            if (event.keyCode == 13 && document.all["qty"].value != "") {

                event.keyCode = 9;

                event.returnValue = false;

                document.all[button].click();

            }

        }
    </script>
    <style type="text/css">
        .ui-helper-hidden-accessible
        {
            display: none;
        }
        .lbhide
        {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="float: left">
        <div>
            <table width="96%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td height="20">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr style="font-weight: bold;">
                    <td colspan="5" align="center" style="font-size: 18px; font-weight: bold;">
                        成品出库制单
                    </td>
                </tr>
                <tr>
                    <td width="313" align="left">
                        出库单号:<asp:Label Text="" ID="lblNo" runat="server" Font-Bold="true" Font-Size="15px" />
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="lbhide">LinkButton</asp:LinkButton>
                    </td>
                    <td width="24" align="right">
                        &nbsp;
                    </td>
                    <td width="476" align="right">
                    </td>
                    <td>
                    </td>
                    <td width="44" align="right">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        部品名称
                        <label>
                            <asp:TextBox runat="server" ID="txtGoodsName" />
                            出</label>库数量
                        <label>
                            <input name="qty" type="text" id="qty" size="8" runat="server" onkeypress="if((event.keyCode<48 ||event.keyCode>57) &&event.keyCode!=46)event.returnValue=false;" />
                            出</label>库货位号
                        <label>
                            <input name="hwh" type="text" id="hwh" size="8" runat="server" />
                        </label>
                        <label>
                        </label>
                        <asp:Button Text="添加" ID="btnAdd" runat="server" OnClick="btnAdd_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <label>
                            <asp:Button Text="库存状况" runat="server" ID="btnStock" OnClick="btnStock_Click" /></label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <span style="float: right; margin-right: 20px;">
            <asp:Button Text="保存" ID="btnSave" runat="server" OnClick="btnSave_Click" /></span>
        <div style="height: 200px; width: 100%; overflow: scroll; float: left;">
            <asp:GridView runat="server" ID="gvList" AutoGenerateColumns="False" EnableModelValidation="True"
                Width="98.5%" BorderStyle="Solid" BorderWidth="1px" BorderColor="#030303" OnRowDataBound="gvList_RowDataBound"
                OnSelectedIndexChanged="gvList_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="goods_name" HeaderText="部番" />
                    <asp:BoundField DataField="store" HeaderText="仓位点" />
                    <asp:BoundField DataField="hwh" HeaderText="货位号" />
                    <asp:BoundField DataField="qty" HeaderText="数量" DataFormatString="{0:0.#}" />
                </Columns>
                <SelectedRowStyle BackColor="#FFFF99" />
            </asp:GridView>
        </div>
        <div>
            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                Width="100%" BorderStyle="Solid" BorderWidth="1px" BorderColor="#030303" OnRowCancelingEdit="gvData_RowCancelingEdit"
                OnRowEditing="gvData_RowEditing" OnRowUpdating="gvData_RowUpdating" OnRowDeleting="gvData_RowDeleting"
                OnRowDataBound="gvData_RowDataBound">
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
                    <asp:CommandField HeaderText="修改" ShowEditButton="True" />
                    <asp:CommandField HeaderText="删除" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>







































































































