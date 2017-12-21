<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mode_Recipt_Data.aspx.cs"
    Inherits="ERPPlugIn.Mold_Management_Accounting.Mode_Recipt_Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>收款页</title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
            if (window.opener.progressWindow)
                window.opener.progressWindow.close();
            //            window.close();
        }
    </script>
    <style type="text/css">
        .btn
        {
            border-right: #7b9ebd 1px solid;
            padding-right: 2px;
            border-top: #7b9ebd 1px solid;
            padding-left: 2px;
            font-size: 12px;
            border-left: #7b9ebd 1px solid;
            cursor: hand;
            color: black;
            padding-top: 2px;
            border-bottom: #7b9ebd 1px solid;
        }
        .Divcenter
        {
            margin-right: auto;
            margin-left: auto;
            width: 98%;
            border-style: ridge;
            border-width: 2px;
        }
    </style>
</head>
<body onload="refreshParent()">
    <form id="form1" runat="server">
    <div style="margin-top: 20px;" class="Divcenter">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td height="30" align="center" style="font-size: 16px; font-weight: bold;">
                    模具管理
                </td>
            </tr>
        </table>
        <div style="margin-bottom: 20px; margin-top: 20px">
            <table width="98%" border="1" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                <tr>
                    <td align="center">
                        INVOICE提出日期
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtinvoice_date" runat="server" onclick="WdatePicker()" />
                    </td>
                    <td align="center">
                        INVOICE号码
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtinvoice_no" runat="server" />
                    </td>
                    <td align="center">
                        收款日期
                    </td>
                    <td align="center">
                        <asp:TextBox runat="server" ID="txtRecipt_Date" onclick="WdatePicker();" />
                    </td>
                    <td align="center">
                        收款金额
                    </td>
                    <td align="center">
                        <asp:TextBox runat="server" ID="txtAmount" onkeypress="if((event.keyCode<48 ||event.keyCode>57) &&event.keyCode!=46)event.returnValue=false;" />
                    </td>
                    <td align="center">
                        <asp:Button Text="确定" ID="btnSure" runat="server" OnClick="btnSure_Click" CssClass="btn" />
                    </td>
                </tr>
            </table>
        </div>
        <div runat="server" id="div" style="margin-bottom: 20px; margin-top: 20px">
            <fieldset class="Divcenter">
                <legend>收款明细</legend>
                <table width="100%" border="0" align="center" cellpadding="6" cellspacing="0" style="border-color: #7E7E7E">
                    <tr>
                        <td>
                            <asp:GridView runat="server" ID="gvReciptData" Width="100%" AutoGenerateColumns="False"
                                EnableModelValidation="True">
                                <Columns>
                                    <asp:BoundField DataField="M_id" HeaderText="Id" />
                                    <asp:BoundField DataField="invoice_no" HeaderText="Invoice号" />
                                    <asp:BoundField DataField="invoice_date" HeaderText="Invoice提出日期" />
                                    <asp:BoundField DataField="Amount" HeaderText="收款金额" />
                                    <asp:BoundField DataField="receipt_date" HeaderText="收款日期" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
    </form>
</body>
</html>
