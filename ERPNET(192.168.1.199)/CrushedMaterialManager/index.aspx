<%@ Page Language="C#" AutoEventWireup="true" Inherits="ERPPlugIn.CrushedMaterialManager.index"
    CodeBehind="index.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript" language="javascript">   </script>
    <script type="text/javascript" language="javascript">
 
    </script>
    <style type="text/css">
        .hidden
        {
            display: none;
        }
        body
        {
           font-size:smaller;    
        }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
   
    <div style="width: 100%;">
     <asp:HyperLink ID="HyperLinkSuccess" runat="server" NavigateUrl="query.aspx">已完成的履历</asp:HyperLink>
    <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="insert.aspx">新增记录</asp:HyperLink>
    <asp:DropDownList ID="DropDownListArea" runat="server" OnSelectedIndexChanged="DropDownListArea_SelectedIndexChanged"
            AutoPostBack="True">
        </asp:DropDownList>
     <asp:Label ID="LabelMoney" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="Button1" runat="server" Text="品质审核" onclick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="生管审核" onclick="Button2_Click"  />
        <asp:Button ID="Button3" runat="server" Text="采购审核" onclick="Button3_Click" />
        <asp:Button ID="Button4" runat="server" Text="成型审核" onclick="Button4_Click"  Enabled="false"/>
        <asp:Button ID="Button5" runat="server" Text="总经理审核" onclick="Button5_Click" Enabled="false"/>
           <%-- <asp:Button ID="buttonConfirm" runat="server" Visible="false" onclick="buttonConfirm_Click" 
            />--%>
        
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
            Style="margin-bottom: 27px">
            <Columns>
             <asp:TemplateField HeaderText="成型确认">
                    <ItemTemplate>
                        <asp:Label ID="lblModing" runat="server" Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="90px" />
                </asp:TemplateField>
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
                <asp:TemplateField HeaderText="品质确认">
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
                <asp:BoundField HeaderText="材料编号" DataField="cz">
                    <ItemStyle Width="70px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="数量" DataField="count">
                    <ItemStyle Width="40px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="材料用量" DataField="cpdz">
                    <ItemStyle Width="70px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="单价" DataField="price">
                    <ItemStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="金额" DataField="moneySum">
                    <ItemStyle Width="70px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="不良内容" DataField="badContent" >
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="不良发生区" DataField="produceArea">
                    <ItemStyle Width="70px" />
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
    </form>
</body>
</html>
