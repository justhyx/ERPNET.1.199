<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialForecast_Purchasing.aspx.cs"
    Inherits="ERPPlugIn.MaterialForecastManager.MaterialForecast_Purchasing" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#txtMName").autocomplete({
                minLength: 1, source: function (request, response) {
                    $.ajax({
                        type: "GET",
                        url: "MData.ashx?keyword=" + request.term,
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
    <style type="text/css">
        .ui-helper-hidden-accessible
        {
            display: none;
        }
        #gvExcel td, th
        {
            border-collapse: collapse;
            border: solid 1px black;
            font-size: 10pt;
        }
        input
        {
            border: solid 1px black;
        }
        span input
        {
            text-align: center;
            border: 0px 0px 0px 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: black">
            <tr>
                <td align="center" style="font-size: 18px; font-weight: bold;">
                    <asp:Literal ID="Literal7" Text="<%$Resources:Resource,wlyc %>" runat="server" />
                    <asp:LinkButton Text="text" runat="server" CssClass="ui-helper-hidden-accessible" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="450px">
                        <asp:GridView ID="gvExcel" runat="server" EnableModelValidation="True" CssClass="css"
                            Width="98%" OnRowDataBound="gvExcel_RowDataBound" OnSelectedIndexChanged="gvExcel_SelectedIndexChanged"
                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                            <RowStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                            <SelectedRowStyle BackColor="#3399FF" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button Text="<%$Resources:Resource,yx %>" runat="server" ID="btnRun" OnClick="btnRun_Click" />
                    <asp:Button Text="<%$Resources:Resource,btnSearch %>" runat="server" ID="btnSearch"
                        OnClick="btnSearch_Click" />
                    <asp:Button Text="<%$Resources:Resource,duizhao %>" runat="server" ID="btnCompair"
                        OnClick="btnCompair_Click" />
                    <asp:Button Text="<%$Resources:Resource,dc %>" runat="server" ID="btnExport" OnClick="btnExport_Click" />
                    <asp:Button Text="<%$Resources:Resource,gx %>" runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal3" Text="<%$Resources:Resource,kssj %>" runat="server" />：<asp:TextBox
                        runat="server" ID="txtStartDate" OnClick="WdatePicker()" />
                    <asp:Literal ID="Literal4" Text="<%$Resources:Resource,jssj %>" runat="server" />：<asp:TextBox
                        runat="server" ID="txtEndDate" OnClick="WdatePicker()" />
                    <asp:Literal Text="<%$Resources:Resource,qy %>" runat="server" />:<asp:DropDownList
                        runat="server" ID="ddlArea">
                        <asp:ListItem Text="<%$Resources:Resource,cx %>" Value="0" />
                        <asp:ListItem Text="<%$Resources:Resource,zl %>" Value="1" />
                    </asp:DropDownList>
                    <asp:Literal ID="Literal5" Text="<%$Resources:Resource,yxlb %>" runat="server" />：<asp:DropDownList
                        runat="server" ID="ddlRunType">
                        <asp:ListItem Text="<%$Resources:Resource,wlyc %>" Value="0" />
                        <asp:ListItem Text="<%$Resources:Resource,wlcg %>" Value="1" />
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span>
                        <asp:CheckBox Text="<%$Resources:Resource,fsx %>" runat="server" ID="cbxNegativeItem" />
                        <asp:CheckBox Text="<%$Resources:Resource,sskc %>" runat="server" ID="cbxStock" />
                        <asp:CheckBox Text="<%$Resources:Resource,xitem %>" runat="server" ID="cbxXItem" />
                    </span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal6" Text="<%$Resources:Resource,wlsx %>" runat="server" />：
                    <asp:DropDownList runat="server" ID="ddlMProperties">
                    </asp:DropDownList>
                    <asp:Literal ID="Literal1" Text="<%$Resources:Resource,ylbh %>" runat="server" />：
                    <asp:TextBox runat="server" ID="txtMName" />
                    <asp:Literal ID="Literal2" Text="<%$Resources:Resource,bf %>" runat="server" />：
                    <asp:TextBox runat="server" ID="txtName" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <span>
                        <asp:CheckBox Text="<%$Resources:Resource,rsl %>" runat="server" ID="cbxRanColor"
                            AutoPostBack="true" OnCheckedChanged="cbxRanColor_CheckedChanged" />
                        <asp:CheckBox Text="<%$Resources:Resource,ysl %>" runat="server" ID="cbxYuanColor"
                            AutoPostBack="true" OnCheckedChanged="cbxYuanColor_CheckedChanged" /></span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="500px">
                        <asp:GridView ID="gvDetail" runat="server" EnableModelValidation="True" CssClass="css"
                            Width="98%">
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>









































































