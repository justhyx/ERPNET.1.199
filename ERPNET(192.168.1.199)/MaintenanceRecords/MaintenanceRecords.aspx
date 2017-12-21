<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaintenanceRecords.aspx.cs"
    Inherits="ERPPlugIn.MaintenanceRecords.MaintenanceRecords" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备维修改善依赖</title>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#tags").autocomplete({
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
        function save() {
            document.getElementById('cap1').cap();
            var save_up = document.getElementById('cap1').savetoFile('d://ttem.jpg');
            document.getElementById('lblResult').innerHTML = "拍照完成";
        }
        function myfunction() {
            document.getElementById('cap1').switchWatchOnly();
        }
        function myfunction1() {
            document.getElementById("cap1").aboutBox();
        }
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
        }
    </style>
</head>
<body onload="myfunction()">
    <form id="form1" runat="server">
    <div style="border: 1px solid black">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td height="30" align="center" style="font-size: 16px; font-weight: bold;">
                    设备维修改善依赖录入
                </td>
            </tr>
        </table>
        <div style="margin-bottom: 20px; margin-top: 20px">
            &nbsp;<table width="98%" border="1" align="center" cellpadding="6" cellspacing="0"
                style="border-color: #7E7E7E">
                <tr>
                    <td align="right">
                        内容
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="保养" Value="1" />
                            <asp:ListItem Text="维修" Value="2" />
                            <asp:ListItem Text="点检" Value="3" />
                            <asp:ListItem Text="检修" Value="4" />
                            <asp:ListItem Text="整改" Value="5" />
                            <asp:ListItem Text="制作" Value="6" />
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        设备编号/名称
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tags" />
                    </td>
                    <td align="right">
                        依赖时间
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtDate" OnClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"
                            onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
                        <%--<input type="text"  />--%>
                    </td>
                    <td align="right">
                        依赖部门
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDept">
                            <asp:ListItem Text="注塑" />
                            <asp:ListItem Text="模具" />
                            <asp:ListItem Text="丝印" />
                            <asp:ListItem Text="粉碎房" />
                            <asp:ListItem Text="仓库" />
                            <asp:ListItem Text="其他" />
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        处理时间
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtHours" onkeypress="if((event.keyCode<48 ||event.keyCode>57) &&event.keyCode!=46)event.returnValue=false;" />
                        <asp:Label ID="Label1" runat="server" Text="小时"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        处置情况
                    </td>
                    <td colspan="9">
                        <asp:TextBox runat="server" ID="txtRecords" Height="96px" Width="1227px" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        设备系处置人
                    </td>
                    <td colspan="9">
                        <span style="float: left">
                            <asp:CheckBoxList ID="cblPerson" runat="server" RepeatDirection="Horizontal" CssClass="none">
                            </asp:CheckBoxList>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        外协或现场人员
                    </td>
                    <td colspan="9">
                        <asp:TextBox runat="server" ID="txtPerson" Width="1227px" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        上传文档
                    </td>
                    <td colspan="9">
                        <asp:FileUpload ID="FileUpload1" runat="server" Height="25px" Width="30%" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="divcenter" style="text-align: center;" style="margin-top: 20px; margin-bottom: 20px">
            <table width="98%" border="1" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                <tr>
                    <td style="height: 50px">
                        <asp:Button Text="添 加" ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click"
                            Width="75" Height="25" />
                        &nbsp;<input type="button" name="Submit22" style="width: 75px; height: 25px" onclick="javascript:history.back();"
                            value="返 回" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
