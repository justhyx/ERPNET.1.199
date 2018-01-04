﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="ERPPlugIn.CrushedMaterialManager.edit"
    CodeBehind="edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../My97DatePicker/calendar.js"></script>
    <script src="../Scripts/jquery-1.10.2.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#TextgoodsName").autocomplete({
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
    <style type="text/css">
        li
        {
            list-style-type: none;
            text-align: left;
            margin:20px;
        }
        .ui-helper-hidden-accessible
        {
            display: none;
            
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="margin: 0 auto;">
            <ContentTemplate>
        <table  runat="server" style="margin: 0 auto;" border="1" cellspacing="0">
            <tr>
                <td>
                    <span>部番</span>
                    <td>
                    <asp:TextBox ID="TextgoodsName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="TextgoodsName" ErrorMessage="输入不能为空"></asp:RequiredFieldValidator>
                    </td>
                </td>
            </tr>
            <tr>
                <td style="width: 13%">
                    <span>不良数量</span>
                    <td>
                    <asp:TextBox ID="Textcount" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="Textcount" ErrorMessage="输入不能为空"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="Textcount" ErrorMessage="只能输入数字" 
                            ValidationExpression="^\+?[1-9][0-9]*$"></asp:RegularExpressionValidator>
                    </td>
                </td>
                </tr>
                <tr>
                <td >
                    <span>不良内容</span>
                    <td>
                    <asp:TextBox ID="Textbadcontent" Rows="4" cols="15" runat="server" Style="width: 216px;
                        height: 67px; resize: none;" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="Textbadcontent" ErrorMessage="输入不能为空"></asp:RequiredFieldValidator>
                    </td>
                </td>
                </tr>
                <tr>
                <td>
                    <span>生产日期</span>
                    </td>
                     <td>
                    <asp:TextBox ID="TextproduceTime" runat="server" class="Wdate" onfocus="WdatePicker({lang:'zh-cn',readOnly:true})"></asp:TextBox>
                </td>
                </tr>
                <tr>
                <td>
                    <span>作业员</span>
                </td>
                <td>
                    <asp:TextBox ID="TextemployeeName" runat="server"></asp:TextBox>
                </td>
                </tr>
                <tr>
                <td >
                    <span>不良发生区</span>
                </td>
                 <td>
                    <select runat="server" id="DropDownListproduceArea">
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
        
        </table>
    
    <table style="margin: 0 auto;">
        <tr>
            <td>
            <asp:Button ID="ButtonOK" runat="server" OnClick="ButtonOk_Click" Text="确定" />
            <asp:Button ID="ButtonBack" runat="server" OnClick="ButtonBack_Click" Text="返回" />
            </td>
        </tr>
    </table>
    </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
