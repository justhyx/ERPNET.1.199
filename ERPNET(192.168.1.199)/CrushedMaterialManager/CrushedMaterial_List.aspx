<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrushedMaterial_List.aspx.cs"
    Inherits="ERPPlugIn.CrushedMaterialManager.CrushedMaterial_List" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试作品处置</title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 196px;
        }
        .style2
        {
            width: 197px;
        }
        .style3
        {
            width: 121px;
        }
        .button
        {
            background-image: url(../images/button1_n.jpg);
            font-weight: bold;
            height: 30px;
            width: 80px;
        }
    </style>
    <script type="text/javascript">
        function submitTest(btn) {
            btn.value = "处理中...";
            btn.onclick = onDealing;
        }
        function onDealing() {
            alert('系统处理中,请稍候...');
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="800" border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="5" align="center" style="font-size: 18px; font-weight: bold;">
                    试作品粉碎列表
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView runat="server" ID="dgvList" Width="800px" border="1" align="center"
                        CellPadding="4" BorderColor="#7E7E7E" AutoGenerateColumns="False" EnableModelValidation="True"
                        AllowPaging="True" PageSize="20" OnPageIndexChanging="dgvList_PageIndexChanging"
                        OnRowDataBound="dgvList_RowDataBound" OnSelectedIndexChanged="dgvList_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox Text="" ID="cboCheckItem" runat="server" />
                                    <asp:HiddenField runat="server" ID="HfId" Value='<%# Eval("id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="部番">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="材质编号">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("MaterialNo") %>'>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="材质">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Material") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="模具担当">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("JigLeader") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="提交日期">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("addtime") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    审核意见：
                </td>
                <td class="style2">
                    <asp:DropDownList runat="server" ID="ddlVerify" AutoPostBack="True" OnSelectedIndexChanged="ddlVerify_SelectedIndexChanged">
                        <asp:ListItem Text="OK" Value="0" />
                        <asp:ListItem Text="NG" Value="1" />
                    </asp:DropDownList>
                </td>
                <td class="style1">
                    原因:
                </td>
                <td colspan="2" align="right">
                    <asp:TextBox runat="server" ID="txtReason" Width="500px" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:Button Text="确认审核" runat="server" ID="btnVerify" CssClass="button" OnClick="btnVerify_Click"
                        OnClientClick="submitTest(this)" />
                </td>
            </tr>
        </table>
        <p>
            &nbsp;
        </p>
        <p>
            &nbsp;
        </p>
        <p>
            &nbsp;
        </p>
    </div>
    </form>
</body>
</html>











































































































