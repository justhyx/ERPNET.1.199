<%@ Page Language="C#" AutoEventWireup="true" Inherits="ERPPlugIn.CrushedMaterialManager.query"
    CodeBehind="query.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .print
        {
            list-style-type: none;
            margin-left: 70%;
        }
        li
        {
            list-style-type: none;
            margin-left: 3px;
            margin-right: 2px;
        }
        .ui-helper-hidden-accessible
        {
            display: none;
        }
        
        .block
        {
            white-space:nowrap; overflow:hidden; text-overflow:ellipsis;
            margin-left:10px;
            display: inline;
            width: 320px;
        }
        .lbl
        {
            
            display: inline;
        }
        .inputCotnrol
        {
            width: 120px;
        }
    </style>
    <script type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../My97DatePicker/calendar.js"></script>
    <script src="../Scripts/jquery-1.10.2.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#TextBoxGoodsName").autocomplete({
                minLength: 1, source: function (request, response) {
                    $.ajax({
                        type: "GET",
                        url: "ServerData.ashx?keyword=" + request.term,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",

                        success: function (data) {
                            response($.map(data, function (item) {
                                return { value: item };
                            }));
                        }
                    });
                }
            })
        });
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
    <div style="width:80%;margin:0 auto;">
        <div  class="block">
            <div class="lbl">
                部番</div>
            <asp:TextBox ID="TextBoxGoodsName" runat="server" CssClass="inputCotnrol"></asp:TextBox>
        </div>
        <div class="block">
            <div class="lbl">数量</div>
            <asp:TextBox ID="TextBoxcountLess" runat="server" CssClass="inputCotnrol"></asp:TextBox>
            <div class="lbl">-</div >
            <asp:TextBox ID="TextBoxcountMore" runat="server" CssClass="inputCotnrol"></asp:TextBox>
        </div>
        <div class="block" >
            <div class="lbl">单价</div>
            <asp:TextBox ID="TextBoxPriceLess" runat="server" CssClass="inputCotnrol"></asp:TextBox>
            <div class="lbl">-</div>
            <asp:TextBox ID="TextBoxPriceMore" runat="server" CssClass="inputCotnrol"></asp:TextBox>
        </div>
        <div class="block"><div class="lbl">金额</div>
            <asp:TextBox ID="TextBoxMoneyLess" runat="server" CssClass="inputCotnrol"></asp:TextBox>
            <div class="lbl">-</div>
            <asp:TextBox ID="TextBoxMoneyMore" runat="server" CssClass="inputCotnrol"></asp:TextBox>
        </div>
        <p></p>
        <div class="block"><div style="font-family: KaiTi_GB2312;" class="lbl">生产时间</div>
            
                <input runat="server" id="beginTime" class="Wdate" onfocus="WdatePicker({lang:'zh-cn',readOnly:true})" />
                <div style="font-family: KaiTi_GB2312;" class="lbl">
                -
                </div>
                <input runat="server" id="endTime" class="Wdate" onfocus="WdatePicker({lang:'zh-cn',readOnly:true})" />
            </div>
        <div class="block"><div style="font-family: KaiTi_GB2312;" class="lbl">录入时间</div> 
            <input runat="server" id="inputTimeBegign" class="Wdate" onfocus="WdatePicker({lang:'zh-cn',readOnly:true})" />
           <div style="font-family: KaiTi_GB2312;" class="lbl">
            -
            </div>
            <input runat="server" id="inputTimeEnd" class="Wdate" onfocus="WdatePicker({lang:'zh-cn',readOnly:true})" />
            
        </div>
    
    
   
        
            <div class="block">
                <asp:Button ID="ButtonQuery" runat="server" Text="查询" OnClick="ButtonQuery_Click" />
                <asp:Button ID="ButtonReset" runat="server" Text="清空" OnClick="ButtonReset_Click" />
            </div>
            </div>
   <div style="width:100%;" align="center">
       <div>
            <ul>
            <li class="print">
                <asp:DropDownList ID="DropDownListArea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownListIndex" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Button ID="Buttonprint" runat="server" Text="打印" OnClick="Buttonprint_Click" />
                <asp:Button ID="ButtonprintQuery" runat="server" Text="打印查询" OnClick="ButtonprintQuery_Click" />
            </li>
        </ul>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="部番" DataField="goodsName" />
            <asp:BoundField HeaderText="材料编号" DataField="cz" />
            <asp:BoundField HeaderText="材料材质" DataField="spec" />
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
            <asp:BoundField HeaderText="作业员" DataField="employeeName" />
            <asp:BoundField HeaderText="不良发生区" DataField="produceArea" />
        </Columns>
    </asp:GridView>
    
    </div>
    <div style="margin-left:7%;">
    <asp:Button ID="ButtonBack" runat="server" OnClick="ButtonBack_Click" Style="height: 21px"
        Text="返回" />
        </div>
    </form>
</body>
</html>
