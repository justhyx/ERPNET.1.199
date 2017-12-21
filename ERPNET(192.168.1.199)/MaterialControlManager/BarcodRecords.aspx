<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BarcodRecords.aspx.cs"
    Inherits="ERPPlugIn.MaterialControlManager.BarcodRecords" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .ui-helper-hidden-accessible
        {
            display: none;
        }
        span input
        {
            text-align: center;
            border: 0px 0px 0px 0px;
        }
        .Divcenter
        {
            margin-right: auto;
            margin-left: auto;
        }
        li
        {
            list-style-type: none;
            text-align: left;
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
        <div class="Divcenter" style="border: 1px solid black; width: 99%; margin-top: 5px;">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border-color: #7E7E7E">
                <tr>
                    <td height="30" align="center" style="font-size: 16px; font-weight: bold;">
                        扫描记录
                        <asp:LinkButton ID="LinkButton1" Text="text" runat="server" CssClass="lbhide" />
                    </td>
                </tr>
            </table>
            <div>
                <div class="Divcenter" style="width: 98%;">
                    部番：<asp:TextBox runat="server" ID="txtGoods_name" />
                    扫描时间<asp:TextBox runat="server" ID="txtStartDate" OnClick="WdatePicker()" onfocus="WdatePicker()" />
                    至<asp:TextBox runat="server" ID="txtEndDate" OnClick="WdatePicker()" onfocus="WdatePicker()" />&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
            </div>
            <div class="Divcenter" style="width: 98%; margin: 20px 0px 20px 10px;">
                <asp:Button Text="搜 寻" runat="server" value="搜 寻" ID="btnSearch" OnClick="btnSearch_Click"
                    Width="70" Height="25" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button Text="导 出" runat="server" value="导 出" ID="btnExpot" OnClick="btnExpot_Click"
                    Width="70" Height="25" />&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
        </div>
        <hr style="height: 3px; border: none; border-top: 1px dashed  #0066CC;" />
        <div class="divcenter" style="margin-top: 20px; margin-bottom: 20px; text-align: center">
            <table width="98%" border="0" cellpadding="0" cellspacing="0" style="margin: auto">
                <tr>
                    <td style="text-align: center">
                        <asp:GridView ID="gvShowData" runat="server" Width="100%" EnableModelValidation="True"
                            CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" OnRowDeleting="gvShowData_RowDeleting">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="goods_name" HeaderText="部番" />
                                <asp:BoundField DataField="qty" HeaderText="数量" />
                                <asp:BoundField DataField="pdate" HeaderText="生产日期" />
                                <asp:BoundField DataField="sn" HeaderText="箱号" />
                                <asp:BoundField DataField="create_date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                    HeaderText="扫描日期" />
                                <asp:TemplateField HeaderText="操作" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            Text="删除" OnClientClick="return confirm('是否要删除该条记录?')"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#FFFF99" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
