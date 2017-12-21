<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vender_Edit.aspx.cs" Inherits="ERPPlugIn.Vender_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .button
        {
            background-image: url(../images/button1_n.jpg);
            font-weight: bold;
        }
    </style>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
// <![CDATA[

        function btnCannel_onclick() {

        }

// ]]>
    </script>
</head>
<body style="background-color: White">
    <form id="form1" runat="server">
    <div id="divData">
        <table width="800" border="1" align="center" cellpadding="6" cellspacing="0" bordercolor="#000000">
            <tr>
                <td height="30" align="center" style="font-size: 16px; font-weight: bold;" colspan="4">
                    <asp:Literal ID="Literal1" Text="<%$ Resources:Resource,gysxxwh%>" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="79" bgcolor="#FFFFFF">
                    ID
                </td>
                <td width="303" bgcolor="#FFFFFF">
                    <asp:TextBox runat="server" ID="vendor_id" Enabled="False" />
                </td>
                <td width="87" bgcolor="#FFFFFF">
                    <asp:Literal ID="Literal2" Text="<%$ Resources:Resource,gysm%>" runat="server" />
                </td>
                <td width="273" bgcolor="#FFFFFF">
                    <asp:TextBox runat="server" ID="vendor_aname" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF">
                    <asp:Literal ID="Literal3" Text="<%$ Resources:Resource,gysjc%>" runat="server" />
                </td>
                <td bgcolor="#FFFFFF">
                    <asp:TextBox runat="server" ID="vendor_name" Enabled="False" />
                </td>
                <td bgcolor="#FFFFFF">
                    <asp:Literal ID="Literal4" Text="<%$ Resources:Resource,cs%>" runat="server" />
                </td>
                <td bgcolor="#FFFFFF">
                    <asp:TextBox runat="server" ID="city" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF">
                    <asp:Literal ID="Literal5" Text="<%$ Resources:Resource,dh%>" runat="server" />
                </td>
                <td bgcolor="#FFFFFF">
                    <asp:TextBox runat="server" ID="tel" Enabled="False" />
                </td>
                <td bgcolor="#FFFFFF">
                    <asp:Literal ID="Literal6" Text="<%$ Resources:Resource,cz%>" runat="server" />
                </td>
                <td bgcolor="#FFFFFF">
                    <asp:TextBox runat="server" ID="fax" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF">
                    <asp:Literal ID="Literal7" Text="<%$ Resources:Resource,lxr%>" runat="server" />
                </td>
                <td bgcolor="#FFFFFF">
                    <asp:TextBox runat="server" ID="contact" Enabled="False" />
                </td>
                <td bgcolor="#FFFFFF">
                    <asp:Literal ID="Literal8" Text="<%$ Resources:Resource,cjrq%>" runat="server" />
                </td>
                <td bgcolor="#FFFFFF">
                    <asp:TextBox runat="server" ID="crt_date" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF">
                    <asp:Literal ID="Literal9" Text="<%$ Resources:Resource,cjr%>" runat="server" />
                </td>
                <td bgcolor="#FFFFFF">
                    <asp:TextBox runat="server" ID="operator_name" Enabled="False" />
                </td>
                <td bgcolor="#FFFFFF">
                    <asp:Literal ID="Literal10" Text="<%$ Resources:Resource,issccj%>" runat="server" />
                </td>
                <td bgcolor="#FFFFFF" align="center">
                    <asp:CheckBox runat="server" ID="cboIsSCCJ" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF">
                    <asp:Literal ID="Literal11" Text="<%$ Resources:Resource,dz%>" runat="server" />
                </td>
                <td bgcolor="#FFFFFF" colspan="3">
                    <asp:TextBox runat="server" ID="vendor_address" Text="" Enabled="False" Width="100%" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <table width="800" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="center">
                                <%--                                <input type="button" name="Submit2" runat="server" id="btnEdit" onclick="btnEdit_ServerClick()"
                                    style="background-image: url(../images/button1_n.jpg); font-weight: bold; height: 30px;
                                    width: 80px;" value="修 改" />--%>
                                <asp:Button Text="<%$ Resources:Resource,xg%>" runat="server" Height="30px" Width="80px"
                                    CssClass="button" ID="btnEdit" OnClick="btnEdit_Click" />
                            </td>
                            <td align="center">
                                <%--<input type="button" runat="server" id="btnCannel" name="Submit22" onclick="javascript:history.back();"
                                    style="background-image: url(../images/button1_n.jpg); font-weight: bold; height: 30px;
                                    width: 80px;" value="返 回" onclick="return btnCannel_onclick()" />--%>
                                <asp:Button Text="<%$ Resources:Resource,fh%>" runat="server" Height="30px" Width="80px"
                                    CssClass="button" ID="btnCannel" OnClick="btnCannel_Click" />
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







































































