<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BarcodRecordsExport.aspx.cs"
    Inherits="ERPPlugIn.MaterialControlManager.BarcodRecordsExport" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据导出</title>
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
                        扫描记录导出
                        <asp:LinkButton Text="text" runat="server" CssClass="lbhide" />
                    </td>
                </tr>
            </table>
            <div>
                <div class="Divcenter" style="margin-top: 20px; width: 98%">
                    查询条件</div>
                <div class="Divcenter" style="margin-top: 20px; margin-bottom: 20px; width: 98%">
                    <asp:Label Text="客户:" runat="server" Style="float: left; margin-top: 5px" />
                    <span>
                        <asp:RadioButtonList ID="rbtCustomer" runat="server" RepeatDirection="Horizontal"
                            Style="float: left" OnSelectedIndexChanged="rbtCustomer_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="理光" Value="0" Selected="True" />
                            <asp:ListItem Text="其他" Value="1" />
                        </asp:RadioButtonList>
                    </span>
                </div>
                <div class="Divcenter" style="margin-top: 20px; width: 98%">
                    扫描时间<asp:TextBox runat="server" ID="txtStartDate" OnClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"
                        onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
                    至<asp:TextBox runat="server" ID="txtEndDate" OnClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"
                        onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />&nbsp;&nbsp;&nbsp;&nbsp;
                    指示时间:日期<asp:TextBox runat="server" ID="txtLeaderDate" OnClick="WdatePicker()" />
                    使用时间：<asp:TextBox runat="server" ID="txtsLeaderTime" OnClick="WdatePicker({dateFmt:'HHmm'})" />
                    至<asp:TextBox runat="server" ID="txteLeaderTime" OnClick="WdatePicker({dateFmt:'HHmm'})" />
                    <asp:Label Text="指示号:" runat="server" ID="lblZsh" /><asp:TextBox runat="server" ID="txtZsh" />
                </div>
                <div class="Divcenter" style="margin-top: 20px; width: 98%">
                    <span>
                        <asp:RadioButtonList runat="server" ID="rdoMode" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Sum" Value="0" Selected="True" />
                            <asp:ListItem Text="Detail" Value="1" />
                        </asp:RadioButtonList>
                    </span>
                </div>
                <div class="Divcenter" style="margin-top: 20px; width: 98%">
                    <asp:Button Text="搜 寻" runat="server" value="搜 寻" ID="btnSearch" OnClick="btnSearch_Click"
                        Width="70" Height="25" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button Text="导 出" runat="server" value="导 出" ID="btnExpot" OnClick="btnExpot_Click"
                        Width="70" Height="25" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button Text="导出明细" runat="server" ID="btnDetailExport" Width="70" Height="25"
                        OnClick="btnDetailExport_Click" />
                </div>
                <div class="Divcenter" style="margin-top: 20px; margin-bottom: 20px; width: 98%">
                    <a href="BarcodRecords.aspx">按部番查询导出数据</a></div>
            </div>
            <hr style="height: 3px; border: none; border-top: 1px dashed  #0066CC;" />
            <div class="divcenter" style="margin-top: 20px; margin-bottom: 20px; text-align: center">
                <asp:Panel runat="server" Height="300" ScrollBars="Vertical">
                    <table width="98%" border="0" cellpadding="0" cellspacing="0" style="margin: auto">
                        <tr>
                            <td style="text-align: center">
                                <asp:GridView ID="gvShowData" runat="server" Width="100%" EnableModelValidation="True"
                                    CellPadding="4" ForeColor="#333333" OnRowDataBound="gvShowData_RowDataBound"
                                    OnSelectedIndexChanged="gvShowData_SelectedIndexChanged" OnRowDeleting="gvShowData_RowDeleting">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False" HeaderText="操作">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="取消" OnClientClick="return confirm('取消之后该记录将不从系统中消失,是否确认取消?');"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
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
                </asp:Panel>
                <hr style="height: 3px; border: none; border-top: 1px dashed  #0066CC;" />
                <asp:Panel runat="server" ScrollBars="Vertical" Height="300">
                    <table width="98%" border="0" cellpadding="0" cellspacing="0" style="margin: auto">
                        <tr>
                            <td style="text-align: center">
                                <asp:GridView ID="gvDetail" runat="server" Width="100%" EnableModelValidation="True"
                                    CellPadding="4" ForeColor="#333333" OnRowDataBound="gvDetail_RowDataBound" ShowFooter="True"
                                    OnRowDeleting="gvDetail_RowDeleting">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="操作" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="取消" OnClientClick="return confirm('取消之后该记录将不从系统中消失,是否确认取消?');"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
