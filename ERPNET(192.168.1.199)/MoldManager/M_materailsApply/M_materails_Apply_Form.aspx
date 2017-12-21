<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="M_materails_Apply_Form.aspx.cs"
    Inherits="ERPPlugIn.MoldManager.M_materailsApply.M_materails_Apply_Form" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .lbhide
        {
            display: none;
        }
    </style>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function EnterTextBox() {
            if (event.keyCode == 13) {
                if (document.all["txtM_name"].value != "") {
                    event.keyCode = 9;
                    event.returnValue = false;
                    document.getElementById("btnFind").click();
                }
                else {
                    alert("请输入名称")
                    document.getElementById("test").focus();
                }
            }
        }   
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="div">
        <table width="100%" border="0" cellspacing="0" cellpadding="6">
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnSave" Text="<%$ Resources:Resource, bc %>" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
        <table width="800" border="1" id="table" align="center" cellpadding="6" cellspacing="0"
            runat="server" style="border-color: black">
            <tr>
                <td colspan="2" align="center" style="font-size: 18px; font-weight: bold;">
                    材料申请单
                </td>
            </tr>
            <tr>
                <td>
                    类型:
                </td>
                <td>
                    <asp:CheckBox ID="cbxSpec" Text="专用件" AutoPostBack="true" runat="server" OnCheckedChanged="cbxSpec_CheckedChanged" />
                    <asp:CheckBox ID="cbxNomal" Text="标准件" AutoPostBack="true" runat="server" OnCheckedChanged="cbxNomal_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td>
                    模具号:
                </td>
                <td>
                    <asp:TextBox ID="txtModeNo" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    社内编号:
                </td>
                <td>
                    <asp:TextBox ID="txtInternalNo" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    申请日期:
                </td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" onClick="WdatePicker()" onfocus="WdatePicker()" />
                </td>
            </tr>
            <tr>
                <td>
                    备注:
                </td>
                <td>
                    <asp:TextBox ID="txtRemark" runat="server" Height="76px" TextMode="MultiLine" Width="716px" />
                </td>
            </tr>
        </table>
        <hr noshade="noshade" />
        <table width="100%" border="1" align="center" cellpadding="6" cellspacing="0" style="border-color: black">
            <tr>
                <td>
                    名称:
                    <asp:TextBox runat="server" ID="txtM_name" />
                </td>
                <td>
                    规格:
                    <asp:TextBox runat="server" ID="txtSpec" />
                </td>
                <td>
                    材质:
                    <asp:TextBox runat="server" ID="txtCz" />
                </td>
                <td>
                    数量:
                    <asp:TextBox runat="server" ID="txtQty" />
                </td>
                <td>
                    <asp:Button Text="查询" runat="server" ID="btnFind" OnClick="btnFind_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button Text="添加" runat="server" ID="btnAdd" OnClick="btnAdd_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    导入申请单:
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button Text="预览" runat="server" ID="btnImport" OnClick="btnImport_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button Text="确定" runat="server" ID="btnSure" OnClick="btnSure_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <span runat="server" id="dgv" style="float: left">
                        <asp:GridView ID="gvShowData" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="100%" OnRowDataBound="gvShowData_RowDataBound" OnSelectedIndexChanged="gvShowData_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderText="序列">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" Text='<%#Container.DataItemIndex+1 %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="名称">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="规格">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("spec") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="材质">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("cz") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#FFCC66" />
                        </asp:GridView>
                    </span>
                    <asp:GridView ID="gvAddData" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        Width="100%" OnRowEditing="gvAddData_RowEditing" 
                        onrowcancelingedit="gvAddData_RowCancelingEdit" 
                        onrowupdating="gvAddData_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="序列">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" Text='<%#Container.DataItemIndex+1 %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="名称">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("name") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="规格">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("spec") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Eval("spec") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="材质">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("cz") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Eval("cz") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="数量">
                                <ItemTemplate>
                                    <asp:Label ID="txtInputQty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Eval("qty") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="操作" ShowEditButton="True" />
                        </Columns>
                    </asp:GridView>
                    <asp:GridView runat="server" ID="gvdata" Width="100%" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>






