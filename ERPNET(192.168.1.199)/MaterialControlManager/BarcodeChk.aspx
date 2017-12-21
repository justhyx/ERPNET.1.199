<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BarcodeChk.aspx.cs" Inherits="ERPPlugIn.MaterialControlManager.BarcodeChk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>出货检</title>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function submitTest(btn) {
            btn.value = "正在检验...";
            btn.onclick = onDealing;
        }
        function onDealing() {
            alert('系统处理中,请稍候...');
            return false;
        }
    </script>
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
                        出货检
                        <asp:LinkButton ID="LinkButton1" Text="text" runat="server" CssClass="lbhide" />
                    </td>
                </tr>
            </table>
            <hr style="height: 3px; border: none; border-top: 1px dashed  #0066CC;" />
            <div class="Divcenter" style="margin-top: 20px; width: 98%">
                扫描时间<asp:TextBox runat="server" ID="txtStartDate" OnClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"
                    onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
                至<asp:TextBox runat="server" ID="txtEndDate" OnClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"
                    onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
                <label>
                    <asp:DropDownList ID="ddlCustomers" runat="server">
                        <asp:ListItem Text="请选择" />
                    </asp:DropDownList>
                </label>
                <asp:Button Text="搜 寻" runat="server" value="搜 寻" ID="btnSearch" OnClick="btnSearch_Click"
                    Width="70" Height="25" />
                <asp:Button Text="开始检验" runat="server" value="开始检验" ID="btnChk" OnClick="btnChk_Click"
                    Width="70" Height="25" OnClientClick="submitTest(this)" />
            </div>
            <div class="divcenter" style="margin-top: 20px; margin-bottom: 20px; text-align: center">
                <asp:Panel ID="Panel1" runat="server" Height="600" ScrollBars="Vertical">
                    <table width="98%" border="0" cellpadding="0" cellspacing="0" style="margin: auto">
                        <tr>
                            <td style="text-align: left">
                                <asp:GridView ID="gvShowData" runat="server" Width="100%" EnableModelValidation="True"
                                    CellPadding="4" ForeColor="#333333">
                                    <AlternatingRowStyle BackColor="White" />
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
            </div>
        </div>
    </div>
    </form>
</body>
</html>
