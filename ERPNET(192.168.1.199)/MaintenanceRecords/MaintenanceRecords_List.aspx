<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaintenanceRecords_List.aspx.cs"
    Inherits="ERPPlugIn.MaintenanceRecords.MaintenanceRecords_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备维修改善依赖记录</title>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#txtEQp").autocomplete({
                minLength: 1, source: function (request, response) {
                    $.ajax({
                        type: "GET",
                        url: "EquipData.ashx?keyword=" + request.term,
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
        .none
        {
            border: 0px 0px 0px 0px;
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
            margin-left: -38px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Divcenter" style="border: 1px solid black; width: 99%; margin-top: 5px;">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td height="30" align="center" style="font-size: 16px; font-weight: bold;">
                    设备维修改善依赖记录
                </td>
            </tr>
        </table>
        <div style="margin-bottom: 20px; margin-top: 20px">
            <div class="Divcenter" style="margin-bottom: 10px; margin-top: 20px; width: 98%">
                查询条件</div>
            <table width="98%" border="1" align="center" cellspacing="0" cellpadding="6">
                <tr>
                    <td>
                        内容:
                        <label>
                            <asp:DropDownList ID="ddlType" runat="server">
                                <asp:ListItem Text="请选择" Value="0" />
                                <asp:ListItem Text="保养" Value="1" />
                                <asp:ListItem Text="维修" Value="2" />
                                <asp:ListItem Text="点检" Value="3" />
                                <asp:ListItem Text="检修" Value="4" />
                                <asp:ListItem Text="整改" Value="5" />
                                <asp:ListItem Text="制作" Value="6" />
                            </asp:DropDownList>
                        </label>
                        &nbsp;依赖时间:
                        <label>
                            <asp:TextBox runat="server" Width="50px" ID="txtYear" onClick="WdatePicker({dateFmt:'yyyy'})" /><%--<img
                                onclick="WdatePicker({el:'txtYear'})" src="../My97DatePicker/skin/datePicker.gif"
                                width="16" height="22" align="absmiddle" style="cursor: pointer" />--%>年
                            <asp:TextBox ID="txtMoth" runat="server" Width="25px" onClick="WdatePicker({dateFmt:'MM'})" />月
                            <asp:TextBox ID="txtDay" runat="server" Width="25px" onClick="WdatePicker({dateFmt:'dd'})" />日
                        </label>
                        &nbsp;依赖部门:
                        <asp:DropDownList runat="server" ID="ddlDept">
                            <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="注塑" />
                            <asp:ListItem Text="模具" />
                            <asp:ListItem Text="丝印" />
                            <asp:ListItem Text="粉碎房" />
                            <asp:ListItem Text="仓库" />
                            <asp:ListItem Text="其他" />
                        </asp:DropDownList>
                        &nbsp; 设备:
                        <label>
                            <asp:TextBox runat="server" ID="txtEQp" Width="100" />
                        </label>
                        &nbsp;维修人员:
                        <label>
                            <asp:DropDownList runat="server" ID="ddlPerson">
                            </asp:DropDownList>
                        </label>
                        &nbsp;处理结果:
                        <label>
                            <asp:TextBox runat="server" ID="txtResult" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button Text="搜 寻" runat="server" value="搜 寻" ID="btnSearch" OnClick="btnSearch_Click"
                                OnClientClick="submitTest(this)" Width="70" Height="25" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button Text="导 出" runat="server" value="导 出" ID="btnExpot" OnClick="btnExpot_Click"
                                Width="70" Height="25" />
                        </label>
                    </td>
                </tr>
            </table>
        </div>
        <hr style="height: 3px; border: none; border-top: 1px dashed  #0066CC;" />
        <div class="divcenter" style="margin-top: 20px; margin-bottom: 20px; text-align: center">
            <table width="98%" border="0" cellpadding="0" cellspacing="0" style="margin: auto">
                <tr>
                    <td style="text-align: center">
                        <asp:GridView ID="gvShowData" runat="server" Width="100%" AutoGenerateColumns="False"
                            EnableModelValidation="True" OnRowDataBound="gvShowData_RowDataBound" ShowFooter="True"
                            CellPadding="4" ForeColor="#333333" GridLines="Both">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="内容" HeaderText="内容" />
                                <asp:BoundField DataField="设备编号" HeaderText="设备编号" />
                                <asp:BoundField DataField="依赖日期" HeaderText="依赖日期" />
                                <asp:BoundField DataField="依赖部门" HeaderText="依赖部门" />
                                <asp:BoundField DataField="处置结果" HeaderText="处置结果" />
                                <asp:BoundField DataField="维修时间" HeaderText="维修时间" />
                                <asp:BoundField DataField="维修人员" HeaderText="维修人员" />
                                <asp:TemplateField HeaderText="附件">
                                    <ItemTemplate>
                                        <%--'<%# "Goods_Modify.aspx?goodsId="+DataBinder.Eval(Container.DataItem,"goodsId") %>'--%>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%# "../UploadPics/"+DataBinder.Eval(Container.DataItem,"imgId")%>'
                                            Text="附件浏览"></asp:HyperLink>
                                        <%-- <a target="_blank" href="../UploadPics/<%=imgId%>">附件浏览</a>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
