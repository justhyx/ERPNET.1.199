<%@ Page Language="C#" AutoEventWireup="true" Inherits="ERPPlugIn.CrushedMaterialManager.index"
    CodeBehind="index.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript" language="javascript">   </script>
    <script type="text/javascript" language="javascript">



        $(document).ready(function () {
            $(".delete").click(function () {
                var msg = "\n\n确认删除！";
                if (confirm(msg) == true) {
                    return true;
                } else {
                    return false;
                }
            });
        });
    </script>
    <style type="text/css">
        .hidden
        {
            display: none;
        }
    </style>
</head>
<body>
    <asp:HyperLink ID="HyperLinkSuccess" runat="server" NavigateUrl="query.aspx">查看已经审核完成的部品</asp:HyperLink>
    <form id="form1" runat="server">
    <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="insert.aspx">返回插入页面</asp:HyperLink>
    <div style="width: 120%;">
        <asp:DropDownList ID="DropDownListArea" runat="server" OnSelectedIndexChanged="DropDownListArea_SelectedIndexChanged"
            AutoPostBack="True">
        </asp:DropDownList>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
            Style="margin-bottom: 27px">
            <Columns>
                <asp:TemplateField HeaderText="采购确认">
                    <ItemTemplate>
                        <asp:Label ID="lblBudget" runat="server" Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="90px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="生管确认">
                    <ItemTemplate>
                        <asp:Label ID="lblPMC" runat="server" Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="90px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="主管确认">
                    <ItemTemplate>
                        <asp:Label ID="lblLeader" runat="server" Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="90px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="id" DataField="ID">
                    <FooterStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                    <ItemStyle CssClass="hidden" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部番" DataField="goodsName">
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="材料编号" DataField="spec">
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="数量" DataField="count">
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="单价" DataField="price">
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="金额" DataField="moneySum">
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="不良内容" DataField="badContent">
                    <ItemStyle Width="300px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="不良发生区" DataField="produceArea">
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="生产日期" DataField="produceTime">
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="录入时间" DataField="inputTime">
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="作业员" DataField="employeeName">
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="编辑">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlinkedit" runat="server"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlinkdelete" runat="server"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:Label ID="LabelMoney" runat="server" Text="Label"></asp:Label>
    <asp:Button ID="ButtonCEO" runat="server" Text="总经理承认" Visible="false" OnClick="ButtonCEO_Click" />
    <asp:Button ID="ButtonManager" runat="server" Text="经理承认" Visible="false" OnClick="Buttonmanager_Click" />
    <asp:Button ID="ButtonPurchase" runat="server" Text="采购确认" OnClick="ButtonPurchase_Click" />
    <asp:Button ID="ButtonPMC" runat="server" Text="生管确认" OnClick="ButtonPMC_Click" />
    <asp:Button ID="Buttondirector" runat="server" Text="主管确认" OnClick="Buttondirector_Click" />
    </form>
</body>
</html>
