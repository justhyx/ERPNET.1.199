<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterailsStock_KeyIn.aspx.cs"
    Inherits="ERPPlugIn.MaterailsManager.MaterailsStock_KeyIn" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><asp:Literal ID="Literal8" Text="<%$ Resources:Resource, pdblr%>" runat="server" /></title>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .gv
        {
            width: 100%;
        }
        .lbhide
        {
            display: none;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function EnterTextBox() {
            if (event.keyCode == 13) {
                if (document.all["txtGoodsName"].value != "") {
                    event.keyCode = 9;
                    event.returnValue = false;
                    document.getElementById("sr").click();
                }
                else {
                    alert("请输入商品")
                }
            }
        }   
    </script>
</head>
<body onkeypress="return EnterTextBox()">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="6">
            <tr>
                <td align="right">
                    <asp:Button ID="txtAdd" Text="<%$ Resources:Resource, tj1 %>" runat="server" OnClick="txtAdd_Click"
                        Enabled="false" />
                    <asp:Button ID="btninput" Text="<%$ Resources:Resource, dy %>" runat="server" OnClick="btninput_Click" />
                    <asp:Button runat="server" ID="btnSave" Text="<%$ Resources:Resource, bc %>" OnClick="btnSave_Click" />
                    <asp:Button ID="btnEdit" Text="<%$ Resources:Resource, xg %>" runat="server" OnClick="btnEdit_Click" />
                </td>
            </tr>
        </table>
        <table width="100% " border="1" align="center" cellpadding="4" cellspacing="0" style="border-color: #7E7E7E">
            <tr>
                <td colspan="3" align="center" style="font-size: 18px; font-weight: bold;">
                    <asp:Literal ID="Literal1" Text="<%$ Resources:Resource, pdblr%>" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Literal ID="Literal2" Text="<%$ Resources:Resource, clck%>" runat="server" />：
                    <asp:DropDownList runat="server" ID="ddlMaterialStock" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialStock_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="txtMaterialStock" Enabled="False" />
                    <asp:Literal ID="Literal7" Text="<%$ Resources:Resource, ylbh%>" runat="server" />：
                    <asp:TextBox ID="txtGoodsName" runat="server" Enabled="False" />
                    <asp:Literal ID="Literal9" Text="<%$ Resources:Resource, hwh%>" runat="server" />：
                    <asp:TextBox ID="txthwh" runat="server" />
                    <asp:Literal ID="Literal3" Text="<%$ Resources:Resource, pdbh%>" runat="server" />：
                    <asp:TextBox ID="txtPK_No" runat="server" />
                    <asp:Literal ID="Literal4" Text="<%$ Resources:Resource, bz%>" runat="server" />：
                    <asp:TextBox ID="txtRemark" runat="server" />
                    <asp:Literal ID="Literal5" Text="<%$ Resources:Resource, pdrq%>" runat="server" />：
                    <asp:TextBox ID="txtPK_Date" runat="server" Enabled="False" />
                    <asp:Literal ID="Literal6" Text="<%$ Resources:Resource, pddh%>" runat="server" />：
                    <asp:TextBox ID="txtPK_Id" runat="server" Enabled="False" />
                </td>
            </tr>
            <tr runat="server" id="tr" align="center">
                <td colspan="3">
                    <asp:GridView runat="server" ID="gvData" OnRowDataBound="gvData_RowDataBound" OnSelectedIndexChanged="gvData_SelectedIndexChanged"
                        CssClass="gv" />
                </td>
            </tr>
            <tr runat="server" id="trgoods" align="center">
                <td colspan="3">
                    <asp:GridView runat="server" ID="gvGoodsData" OnRowDataBound="gvGoodsData_RowDataBound"
                        OnSelectedIndexChanged="gvGoodsData_SelectedIndexChanged" CssClass="gv" AutoGenerateColumns="False"
                        EnableModelValidation="True">
                        <Columns>
                            <asp:BoundField DataField="name" HeaderText="<%$ Resources:Resource, ylbh%>" />
                            <asp:BoundField DataField="name" HeaderText="<%$ Resources:Resource, pm%>" />
                            <asp:BoundField DataField="spec" HeaderText="<%$ Resources:Resource, gg%>" />
                            <asp:BoundField DataField="texture" HeaderText="<%$ Resources:Resource, cz%>" />
                            <asp:BoundField DataField="color" HeaderText="<%$ Resources:Resource, ys%>" />
                            <asp:BoundField DataField="unit" HeaderText="<%$ Resources:Resource, dw%>" />
                            <asp:BoundField DataField="pch" HeaderText="<%$ Resources:Resource, pch%>" />
                            <asp:BoundField DataField="hwh" HeaderText="<%$ Resources:Resource, hwh%>" />
                            <asp:BoundField DataField="Qty" HeaderText="<%$ Resources:Resource, pdsl%>" />
                            <asp:BoundField DataField="new_price" HeaderText="<%$ Resources:Resource, dj%>" />
                            <asp:BoundField DataField="vendor_name" HeaderText="<%$ Resources:Resource, sccj%>" />
                            <asp:BoundField DataField="stock_remain_id" HeaderText="stock_remain_id" />
                            <asp:BoundField DataField="prd_batch_id" HeaderText="prd_batch_id" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr runat="server" id="tredit">
                <td colspan="3">
                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="3" align="right">
                                <asp:Button Text="<%$ Resources:Resource, xz1%>" runat="server" ID="btnSelect" OnClick="btnSelect_Click" />
                                <asp:Button Text="<%$ Resources:Resource, qx%>" runat="server" ID="btnCannel" OnClick="btnCannel_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView runat="server" ID="gvEditData" OnRowDataBound="gvEditData_RowDataBound"
                                    OnSelectedIndexChanged="gvEditData_SelectedIndexChanged" CssClass="gv" AutoGenerateColumns="False"
                                    EnableModelValidation="True">
                                    <Columns>
                                        <asp:BoundField DataField="Bill_Id" HeaderText="Id" FooterStyle-CssClass="lbhide"
                                            HeaderStyle-CssClass="lbhide" ItemStyle-CssClass="lbhide">
                                            <FooterStyle CssClass="lbhide"></FooterStyle>
                                            <HeaderStyle CssClass="lbhide"></HeaderStyle>
                                            <ItemStyle CssClass="lbhide"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="store_id" HeaderText="<%$ Resources:Resource, clck%>" />
                                        <asp:BoundField DataField="pk_date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Resource, pdrq%>" />
                                        <asp:BoundField DataField="Pk_id" HeaderText="<%$ Resources:Resource, pddh%>" />
                                        <asp:BoundField DataField="Bill_no" HeaderText="<%$ Resources:Resource, pdbh%>" />
                                        <asp:BoundField DataField="Crt_emp" HeaderText="<%$ Resources:Resource, zdr%>" />
                                        <asp:BoundField DataField="Crt_Date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="<%$ Resources:Resource, pdbzdrq%>" />
                                        <asp:BoundField DataField="Remark" HeaderText="<%$ Resources:Resource, bz%>" />
                                        <asp:CheckBoxField DataField="Status" HeaderText="<%$ Resources:Resource, sh%>" ReadOnly="True" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="#FFFF99" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView runat="server" ID="gvDetailData" CssClass="gv" AutoGenerateColumns="False"
                                    EnableModelValidation="True" OnRowDataBound="gvDetailData_RowDataBound" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="name" HeaderText="<%$ Resources:Resource, pm%>" />
                                        <asp:BoundField DataField="spec" HeaderText="<%$ Resources:Resource, gg%>" />
                                        <asp:BoundField DataField="texture" HeaderText="<%$ Resources:Resource, cz%>" />
                                        <asp:BoundField DataField="color" HeaderText="<%$ Resources:Resource, ys%>" />
                                        <asp:BoundField DataField="unit" HeaderText="<%$ Resources:Resource, dw%>" />
                                        <asp:BoundField DataField="vendor_name" HeaderText="<%$ Resources:Resource, sccj%>" />
                                        <asp:BoundField DataField="pch" HeaderText="<%$ Resources:Resource, pch%>" />
                                        <asp:BoundField DataField="hwh" HeaderText="<%$ Resources:Resource, hwh%>" />
                                        <asp:BoundField DataField="qty" DataFormatString="{0:0.##}" HeaderText="<%$ Resources:Resource, pdsl%>" />
                                        <asp:BoundField DataField="price" DataFormatString="{0:0.##}" HeaderText="<%$ Resources:Resource, dj%>" />
                                        <asp:BoundField DataField="total" DataFormatString="{0:0.##}" HeaderText="<%$ Resources:Resource, je%>" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView runat="server" CssClass="gv" ID="gvAddData" AutoGenerateColumns="False"
                        EnableModelValidation="True" ShowFooter="True" OnRowDataBound="gvAddData_RowDataBound"
                        OnSelectedIndexChanged="gvAddData_SelectedIndexChanged" 
                        OnRowDeleting="gvAddData_RowDeleting" onrowupdating="gvAddData_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, pm%>">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hfDetailId" Value='<%#Eval("id") %>' />                                    
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("pm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, gg%>">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("spec") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, cz%>">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("cz") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, ys%>">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("ys") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, dw%>">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("goods_unit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="生产厂家">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%#Eval("customer_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, pch%>">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPch" runat="server" Text='<%#Eval("pch") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, hwh%>">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txthwh" runat="server" Text='<%#Eval("hwh") %>'></asp:TextBox>--%>
                                    <asp:Label ID="txthwh" runat="server" Text='<%#Eval("hwh") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, pdsl%>">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtpdsl" runat="server" Text='<%#Eval("pdsl") %>' AutoPostBack="true"
                                        OnTextChanged="TextBox_TextChanged" onkeypress="if((event.keyCode<48 ||event.keyCode>57) &&event.keyCode!=46)event.returnValue=false;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, dj%>">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtprice" runat="server" Text='<%#Eval("Price") %>' AutoPostBack="true"
                                        OnTextChanged="TextBox_TextChanged" ReadOnly="true" onkeypress="if((event.keyCode<48 ||event.keyCode>57) &&event.keyCode!=46)event.returnValue=false;"> </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, je%>">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%#Eval("total") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, caozuo%>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="BtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                        Text="<%$ Resources:Resource, sc%>" OnClientClick="return confirm('是否要删除该条记录?')"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Resource, caozuo%>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="BtnUpdate" runat="server" CausesValidation="False" CommandName="Update"
                                        Text="<%$ Resources:Resource, sc%>" OnClientClick="return confirm('是否要删除该条记录?')"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <%--<div style="display: none">--%>
            <asp:Button Text="text" ID="sr" runat="server" OnClick="btn_Click" />
        </div>
    </div>
    </form>
</body>
</html>



















































