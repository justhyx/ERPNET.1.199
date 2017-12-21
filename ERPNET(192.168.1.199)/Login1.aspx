<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login1.aspx.cs" Inherits="ERPPlugIn.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录</title>
    <script type="text/javascript">
        function hh() {
            var obj = document.getElementById("div_show");
            var wh = document.documentElement.clientHeight;
            var ww = document.documentElement.clientWidth;
            var sh = document.documentElement.scrollTop;
            var sw = document.documentElement.scrollLeft;
            var dh = parseInt(obj.style.height);
            var dw = parseInt(obj.style.width);
            obj.style.top = wh / 2 - 169;
            obj.style.right = ww / 2 - 300;
            setTimeout("hh();", 50);
        }
    </script>
    <style type="text/css">
        body, td, th
        {
            color: #F0F0F0;
        }
        .button
        {
            background-image: url(Images/button1_n.jpg);
            font-weight: bold;
            border: 0px;
            color: #FFFFFF;
            height: 30px;
            width: 70px;
        }
    </style>
</head>
<body onload="hh();">
    <form id="form1" runat="server">
    <div style="position: absolute; height: 338px; width: 600px; top: 300px; right: 0px;
        background-color: #CCCCCC; z-index: 5;" id="div_show">
        <table width="600" height="338" border="0" cellpadding="0" cellspacing="0" background="Images/index_login.gif">
            <tr>
                <td width="120" height="170">
                    &nbsp;
                </td>
                <td width="300">
                    &nbsp;
                </td>
                <td valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="24">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="64">
                                &nbsp;
                            </td>
                            <td>
                                <span style="font-size: 12px;">V 13.11.0.1 </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td style="font-size: 12px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="100">
                    &nbsp;
                </td>
                <td>
                    <table width="100%" border="0" cellspacing="4" cellpadding="8">
                        <tr>
                            <td width="80" align="left" style="font-size: 18px; font-weight: bold; color: #000000;">
                                UserName
                            </td>
                            <td width="150">
                                <label>
                                    <asp:TextBox runat="server" ID="txtUserName" Width="170px" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 18px; font-weight: bold; color: #000000;">
                                PassWords
                            </td>
                            <td>
                                <label>
                                    <asp:TextBox runat="server" ID="txtPassWords" TextMode="Password" Width="170px" />
                                </label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Text="Chinese" Value="zh-cn" Selected="True" />
                        <asp:ListItem Text="English" Value="en-us" />
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Label Text="" ID="lblResult" runat="server" />
                </td>
                <td>
                    <table width="170" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="43">
                                &nbsp;
                            </td>
                            <td rowspan="2" align="center">
                                <asp:Button Text="Login" runat="server" CssClass="button" ID="btnLogin" OnClick="btnLogin_Click" />
                                &nbsp; &nbsp;
                                <asp:Button Text="Close" runat="server" CssClass="button" ID="btnColse" OnClick="btnColse_Click" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>













