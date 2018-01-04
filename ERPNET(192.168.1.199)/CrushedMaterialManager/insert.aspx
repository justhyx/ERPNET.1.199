﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="ERPPlugIn.CrushedMaterialManager.test"
    CodeBehind="insert.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../My97DatePicker/calendar.js"></script>
    <script src="../Scripts/jquery-1.10.2.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#TextBoxgoodsName").autocomplete({
                minLength: 1, source: function (request, response) {
                    $.ajax({
                        type: "GET",
                        url: "ServerData.ashx?keyword=" + request.term,
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
        function submitTest(btn) {
            btn.value = "处理中...";
            btn.onclick = onDealing;
        }
        function onDealing() {
            alert('系统处理中,请稍候...');
            return false;
        }
    </script>
    <script type="text/javascript" language="javascript">



        $(document).ready(function () {
            $("#Button3").click(function () {
                var msg = "\n\n确认发送！";
                if (confirm(msg) == true) {
                    return true;
                } else {
                    return false;
                }
            });
        });
    </script>
    <style type="text/css">
        li
        {
            list-style-type: none;
            text-align: left;
            margin: 20px;
        }
        .ui-helper-hidden-accessible
        {
            display: none;
        }
        span.lbl
        {
            float: right;
        }
        .inputControl
        {
            width: 210px;
        }
    </style>
</head>
<body style="width: 100%;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="margin: 0 auto;">
        <ContentTemplate>
            <div id="div_first">
                <table runat="server" style="" border="1" cellspacing="0" align="center">
                    <tr>
                        <td class="" style="width: 120px;">
                            <span class="lbl">录入区域</span>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                <asp:ListItem Value="1">现场录入</asp:ListItem>
                                <asp:ListItem Value="2">品检录入</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="RadioButtonList1"
                                ErrorMessage="必须选择现场或者品检"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="" style="width: 120px;">
                            <span class="lbl">部番</span>
                        </td>
                        <td style="width: 350px;">
                            <asp:TextBox ID="TextBoxgoodsName" class="inputControl" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxgoodsName"
                                ErrorMessage="输入不能为空"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="lbl">不良数量</span>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxcount" class="inputControl" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxcount"
                                ErrorMessage="输入不能为空"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxcount"
                                ErrorMessage="只能输入数字" ValidationExpression="^\+?[1-9][0-9]*$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="lbl">不良内容</span>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxbadcontent" class="inputControl" runat="server" Style="height: 67px;
                                resize: none;" TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server" ControlToValidate="TextBoxbadcontent" ErrorMessage="输入不能为空"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="lbl">生产日期</span>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxproduceTime" runat="server" class="Wdate inputControl" onfocus="WdatePicker({lang:'zh-cn',readOnly:true})"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="lbl">作业员</span>
                        </td>
                        <td>
                            <asp:TextBox class="inputControl" ID="TextBoxemployeeName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="lbl">不良发生区</span>
                        </td>
                        <td>
                            <select runat="server" class="inputControl" id="DropDownListproduceArea">
                                <option value="客户退货区">客户退货区</option>
                                <option value="全检">全检不良品</option>
                                <option value="选别不良品">选别不良品</option>
                                <option value="G1">G1</option>
                                <option value="G2">G2</option>
                                <option value="G3">G3</option>
                                <option value="丝印区">丝印区</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: center;">
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="提交" />
                            <asp:Button ID="Button2" runat="server" CausesValidation="False" OnClick="Button2_Click"
                                Text="返回" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
