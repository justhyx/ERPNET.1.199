<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrushedMaterial_Approved.aspx.cs"
    Inherits="ERPPlugIn.CrushedMaterialManager.CrushedMaterial_Approved" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试作品粉碎</title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css" media="print">
        .button
        {
            display: none;
        }
        .notPrint
        {
            display: none;
        }
    </style>
    <style type="text/css">
        .gv
        {
            width: 100%;
        }
        .button
        {
            background-image: url(../images/button1_n.jpg);
            font-weight: bold;
            height: 30px;
            width: 80px;
            border: 0px;
            cursor: hand;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table width="100%" border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="5" align="center" style="font-size: 18px; font-weight: bold;">
                    试作品粉碎审核通过列表
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView runat="server" CssClass="gv" ID="dgvList" border="1" align="center"
                        CellPadding="4" BorderColor="#7E7E7E" AutoGenerateColumns="False" EnableModelValidation="True"
                        AllowPaging="True" PageSize="20" OnPageIndexChanging="dgvList_PageIndexChanging"
                        OnRowDataBound="dgvList_RowDataBound" OnSelectedIndexChanged="dgvList_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="取数">
                                <ItemTemplate>
                                    <asp:CheckBox Text="" ID="cboCheckItem" runat="server" />
                                    <asp:HiddenField runat="server" ID="HfId" Value='<%# Eval("id") %>' />
                                </ItemTemplate>
                                <ControlStyle CssClass="notPrint" />
                                <FooterStyle CssClass="notPrint" />
                                <HeaderStyle CssClass="notPrint" />
                                <ItemStyle CssClass="notPrint" />
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
                            <asp:TemplateField HeaderText="成型1课">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("area1style") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle CssClass="notPrint" />
                                <FooterStyle CssClass="notPrint" />
                                <HeaderStyle CssClass="notPrint" />
                                <ItemStyle CssClass="notPrint" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="成型2课">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("area2style") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle CssClass="notPrint" />
                                <FooterStyle CssClass="notPrint" />
                                <HeaderStyle CssClass="notPrint" />
                                <ItemStyle CssClass="notPrint" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="选别">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("area3style") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle CssClass="notPrint" />
                                <FooterStyle CssClass="notPrint" />
                                <HeaderStyle CssClass="notPrint" />
                                <ItemStyle CssClass="notPrint" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="仓库">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("area4style") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle CssClass="notPrint" />
                                <FooterStyle CssClass="notPrint" />
                                <HeaderStyle CssClass="notPrint" />
                                <ItemStyle CssClass="notPrint" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="处理数量">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="notPrint" onkeypress="if((event.keyCode<48 ||event.keyCode>57) &&event.keyCode!=46)event.returnValue=false;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr class="notPrint">
                <td colspan="5" align="center">
                    处理区域:
                    <asp:DropDownList runat="server" ID="ddlDemp">
                        <asp:ListItem Text="请选择" Value="0" />
                        <asp:ListItem Text="成型1课" Value="1" />
                        <asp:ListItem Text="成型2课" Value="2" />
                        <asp:ListItem Text="选别" Value="3" />
                        <asp:ListItem Text="仓库" Value="4" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="notPrint">
                <td colspan="5" align="center">
                    <asp:Button Text="开始粉碎" runat="server" ID="btnBegin" CssClass="button" OnClick="btnBegin_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button Text="粉碎完成" runat="server" ID="btnDone" CssClass="button" OnClick="btnDone_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" value="打印" onclick="preview()" class="button" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        function preview() {
            window.print();
        }

    </script>
</body>
</html>










































































































