<%@ Page Language="C#" AutoEventWireup="true" Inherits="query" Codebehind="query.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .print
        { 
            list-style-type: none;
            margin-left: 45%;
         }
        li
        {
            display: inline;
            list-style-type: none;
            margin-left: 3px;
            margin-right: 2px;
        }
    </style>
    <script type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript"  src="../My97DatePicker/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-top: 8px;">
        <ul>
            <li>
                <span>部番</span>
                <asp:TextBox ID="TextBoxGoodsName" runat="server" Width="100px"></asp:TextBox>
            </li>
            <li>
                    <span>数量</span>
                    <asp:TextBox ID="TextBoxcountLess" runat="server" Width="100px"></asp:TextBox>
                    <span>-</span>
                    <asp:TextBox ID="TextBoxcountMore" runat="server" Width="100px"></asp:TextBox>
            </li>
            <li>
                    <span>单价</span>
                    <asp:TextBox ID="TextBoxPriceLess" runat="server" Width="100px"></asp:TextBox>
                    <span>-</span>
                    <asp:TextBox ID="TextBoxPriceMore" runat="server" Width="100px"></asp:TextBox>
            </li>
            <li>
                    <span>金额</span>
                    <asp:TextBox ID="TextBoxMoneyLess" runat="server" Width="100px"></asp:TextBox>
                    <span>-</span>
                    <asp:TextBox ID="TextBoxMoneyMore" runat="server" Width="100px"></asp:TextBox>
            </li>
            
            <li>
                <span style="font-family: KaiTi_GB2312;">生产时间</span> <span style="font-family: KaiTi_GB2312;">
                    <input runat="server" id="beginTime" class="Wdate" onfocus="WdatePicker({lang:'zh-cn',readOnly:true})" />
                    -
                    <input runat="server" id="endTime" class="Wdate" onfocus="WdatePicker({lang:'zh-cn',readOnly:true})" />
                    </span>
            </li>
            <li>
                <span style="font-family: KaiTi_GB2312;">录入时间</span> <span style="font-family: KaiTi_GB2312;">
                    <input runat="server" id="inputTimeBegign" class="Wdate" onfocus="WdatePicker({lang:'zh-cn',readOnly:true})" />
                    -
                    <input runat="server" id="inputTimeEnd" class="Wdate" onfocus="WdatePicker({lang:'zh-cn',readOnly:true})" />
                    </span>
            </li>
            <li> 
                <asp:DropDownList ID="DropDownListArea" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                </asp:DropDownList>
            </li>
            <li>
                <asp:DropDownList ID="DropDownListIndex" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </li>
        </ul>
    </div>
    <div>
    <ul>
        <li>
            <asp:Button ID="ButtonQuery" runat="server" Text="查询" OnClick="ButtonQuery_Click" />        
            <asp:Button ID="ButtonReset" runat="server" Text="清空" OnClick="ButtonReset_Click" />
        </li>
        <li class="print">
            <asp:Button ID="Buttonprint" runat="server" Text="打印" OnClick="Buttonprint_Click" />
            <asp:Button ID="ButtonprintQuery" runat="server" Text="打印查询" onclick="ButtonprintQuery_Click"  />
        </li>
    </ul>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="部番" DataField="goodsName" />
            <asp:BoundField HeaderText="材料编号" DataField="spec" />
            <asp:BoundField HeaderText="材料材质" DataField="cz" />
            <asp:BoundField HeaderText="材料色番" DataField="ys" />
            <asp:BoundField HeaderText="数量" DataField="count" />
            <asp:BoundField HeaderText="单价" DataField="price" />
            <asp:BoundField HeaderText="金额" DataField="moneySum" />
            <asp:BoundField HeaderText="不良内容" DataField="badContent">
                <ItemStyle Width="300px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="生产日期" DataField="produceTime">
                <ItemStyle Width="90px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="录入时间" DataField="inputTime">
                <ItemStyle Width="90px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="作业员" DataField="eployeeName" />            
            <asp:BoundField HeaderText="不良发生区" DataField="produceArea" />
        </Columns>
    </asp:GridView>
    <asp:Button ID="ButtonBack" runat="server" OnClick="ButtonBack_Click" Style="height: 21px"
        Text="返回" />
    </form>
</body>
</html>
