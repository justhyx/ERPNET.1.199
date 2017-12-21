<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Goods_List.aspx.cs" Inherits="ERPPlugIn.GoodsManager.Goods_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>生产准备部品构成</title>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../../css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .a
        {
            color: Blue;
        }
        .ui-helper-hidden-accessible
        {
            display: none;
        }
        .button
        {
            background-image: url(../images/button1_n.jpg);
            font-weight: bold;
            height: 30px;
            width: 80px;
        }
        .gv
        {
            width: 160%;
        }
        li
        {
            list-style-type: none;
            text-align: left;
        }
        body
        {
            height: 100%;
            overflow: hidden;
            margin: 0px;
            padding: 0px;
        }
        .box
        {
            height: 86%;
            position: absolute;
            width: 95%;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#tags").autocomplete({
                minLength: 1, source: function (request, response) {
                    $.ajax({
                        type: "GET",
                        url: "GoodsData.ashx?keyword=" + request.term,
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
    <div style="margin-top: 20px">
        <table width="100%" border="0" align="center" cellspacing="0" cellpadding="6">
            <tr>
                <td>
                    客户
                    <label>
                        <asp:DropDownList runat="server" ID="ddlCustomers">
                        </asp:DropDownList>
                    </label>
                    机种
                    <label>
                        <asp:DropDownList runat="server" ID="ddlMachineKind">
                        </asp:DropDownList>
                    </label>
                    担当
                    <label>
                        <asp:DropDownList runat="server" ID="ddlDandang">
                        </asp:DropDownList>
                    </label>
                    部番
                    <label>
                        <input type="text" id="tags" runat="server" />
                        <asp:Button Text="搜 寻" runat="server" value="搜寻" ID="btnSearch" OnClick="btnSearch_Click"
                            OnClientClick="submitTest(this)" />
                    </label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnAddUrl" Text="新增" CssClass="button" OnClick="btnAddUrl_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnInput" Text="批量上传" CssClass="button" OnClick="btnInput_Click" />
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ScrollBars="Both" Width="100%" CssClass="box">
            <asp:GridView runat="server" ID="dgvList" border="1" align="center" CssClass="gv"
                CellPadding="4" BorderColor="#7E7E7E" AutoGenerateColumns="False" EnableModelValidation="True"
                PageSize="20" OnPageIndexChanging="dgvList_PageIndexChanging" OnRowDataBound="dgvList_RowDataBound"
                OnRowEditing="dgvList_RowEditing" OnRowCancelingEdit="dgvList_RowCancelingEdit"
                OnRowUpdating="dgvList_RowUpdating" Width="150%">
                <Columns>
                    <%-- <asp:CommandField ShowEditButton="false" ButtonType="Button" HeaderText="操作"></asp:CommandField>--%>
                    <asp:TemplateField HeaderText="序列">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="部番 (版本)">
                        <ItemTemplate>
                            <asp:HyperLink NavigateUrl='<%# "Goods_Modify.aspx?goodsId="+DataBinder.Eval(Container.DataItem,"goodsId") %>'
                                runat="server" CssClass="a" ID="lnkGoodsName" Text='<%#Eval("goods_name") %>' />
                            <asp:HiddenField runat="server" ID="hfId" Value='<%#Eval("goodsId") %>' />
                            <asp:Label ID="Label27" runat="server" Text='<%#Eval("Version") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="磨具编号">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("mjh") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%#Eval("mjh") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="部品名称">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("goods_ename") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%#Eval("goods_ename") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="机种">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Aircraft") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%#Eval("Aircraft") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编号">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("Materail_Number") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%#Eval("Materail_Number") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="名称">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%#Eval("Materail_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%#Eval("Materail_Name") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="型号">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("Materail_Model") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" Text='<%#Eval("Materail_Model") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="色番">
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%#Eval("ys") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" Text='<%#Eval("ys") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料供应商色番">
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%#Eval("Materail_Vender_Color") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox8" runat="server" Text='<%#Eval("Materail_Vender_Color") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="颜色">
                        <ItemTemplate>
                            <asp:Label ID="Label9" runat="server" Text='<%#Eval("Materail_Color") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox9" runat="server" Text='<%#Eval("Materail_Color") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="部品重量">
                        <ItemTemplate>
                            <asp:Label ID="Label10" runat="server" Text='<%#Eval("cpdz") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox10" runat="server" Text='<%#Eval("cpdz") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="水口重量">
                        <ItemTemplate>
                            <asp:Label ID="Label11" runat="server" Text='<%#Eval("skdz") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox11" runat="server" Text='<%#Eval("skdz") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="干燥温度">
                        <ItemTemplate>
                            <asp:Label ID="Label12" runat="server" Text='<%#Eval("Drying_Temperature") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox12" runat="server" Text='<%#Eval("Drying_Temperature") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="干燥时间">
                        <ItemTemplate>
                            <asp:Label ID="Label13" runat="server" Text='<%#Eval("Drying_Time") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox13" runat="server" Text='<%#Eval("Drying_Time") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="粉碎料比例">
                        <ItemTemplate>
                            <asp:Label ID="Label14" runat="server" Text='<%#Eval("sk_scale") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox14" runat="server" Text='<%#Eval("sk_scale") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <%--                <asp:TemplateField HeaderText="难燃等级">
                    <ItemTemplate>
                        <asp:Label ID="Label15" runat="server" Text='<%#Eval("Fire_Retardant_Grade") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox15" runat="server" Text='<%#Eval("Fire_Retardant_Grade") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>--%>
                    <%--                <asp:TemplateField HeaderText="购买商">
                    <ItemTemplate>
                        <asp:Label ID="Label16" runat="server" Text='<%#Eval("Buyer") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox16" runat="server" Text='<%#Eval("Buyer") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="周期">
                        <ItemTemplate>
                            <asp:Label ID="Label17" runat="server" Text='<%#Eval("cxzq") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox17" runat="server" Text='<%#Eval("cxzq") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <%--                <asp:TemplateField HeaderText="色粉型号">
                    <ItemTemplate>
                        <asp:Label ID="Label18" runat="server" Text='<%#Eval("Toner_Model") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox18" runat="server" Text='<%#Eval("Toner_Model") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="色粉购买商">
                    <ItemTemplate>
                        <asp:Label ID="Label19" runat="server" Text='<%#Eval("Toner_Buyer") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox19" runat="server" Text='<%#Eval("Toner_Buyer") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="取数">
                        <ItemTemplate>
                            <asp:Label ID="Label20" runat="server" Text='<%#Eval("qs") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox20" runat="server" Text='<%#Eval("qs") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="机型">
                        <ItemTemplate>
                            <asp:Label ID="Label21" runat="server" Text='<%#Eval("dw") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox21" runat="server" Text='<%#Eval("dw") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="客户">
                        <ItemTemplate>
                            <asp:Label ID="Label22" runat="server" Text='<%#Eval("khdm") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlKhdm">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <%--                <asp:TemplateField HeaderText="ROHS认证">
                    <ItemTemplate>
                        <asp:Label ID="Label23" runat="server" Text='<%#Eval("Rohs_Certification") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlRohs">
                            <asp:ListItem Text="有" Value="0" />
                            <asp:ListItem Text="无" Value="1" />
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="模具类型">
                        <ItemTemplate>
                            <asp:Label ID="Label26" runat="server" Text='<%#Eval("Model_Type") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox22" runat="server" Text='<%#Eval("Model_Type") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="半成品类型">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label15" runat="server" Text='<%#Eval("Semi_Product_Type") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="半成品对应成品">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label16" runat="server" Text='<%#Eval("Semi_Product_Goods") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="担当人">
                        <ItemTemplate>
                            <asp:Label ID="Label24" runat="server" Text='<%#Eval("Model_Abrasives") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox24" runat="server" Text='<%#Eval("Model_Abrasives") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="Label25" runat="server" Text='<%#Eval("remark") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox25" runat="server" Text='<%#Eval("remark") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>
    </form>
    <%--<script type="text/javascript">
        NS6 = (document.getElementById && !document.all)
        IE = (document.all)
        NS = (navigator.appName == "Netscape" && navigator.appVersion.charAt(0) == "4")
        tempBar = ''; barBuilt = 0; ssmItems = new Array();
        moving = setTimeout('null', 1)
        function moveOut() {
            if ((NS6 || NS) && parseInt(ssm.left) < 0 || IE && ssm.pixelLeft < 0) {
                clearTimeout(moving); moving = setTimeout('moveOut()', slideSpeed); slideMenu(10)
            }
            else { clearTimeout(moving); moving = setTimeout('null', 1) }
        };
        function moveBack() { clearTimeout(moving); moving = setTimeout('moveBack1()', waitTime) }
        function moveBack1() {
            if ((NS6 || NS) && parseInt(ssm.left) > (-menuWidth) || IE && ssm.pixelLeft > (-menuWidth)) {
                clearTimeout(moving); moving = setTimeout('moveBack1()', slideSpeed); slideMenu(-10)
            }
            else { clearTimeout(moving); moving = setTimeout('null', 1) }
        }
        function slideMenu(num) {
            if (IE) { ssm.pixelLeft += num; }
            if (NS || NS6) { ssm.left = parseInt(ssm.left) + num; }
            if (NS) { bssm.clip.right += num; bssm2.clip.right += num; }
        }
        function makeStatic() {
            if (NS || NS6) { winY = window.pageYOffset; }
            if (IE) { winY = document.body.scrollTop; }
            if (NS6 || IE || NS) {
                if (winY != lastY && winY > YOffset - staticYOffset) {
                    smooth = .2 * (winY - lastY - YOffset + staticYOffset);
                }
                else if (YOffset - staticYOffset + lastY > YOffset - staticYOffset) {
                    smooth = .2 * (winY - lastY - (YOffset - (YOffset - winY)));
                }
                else { smooth = 0 }
                if (smooth > 0) smooth = Math.ceil(smooth);
                else smooth = Math.floor(smooth);
                if (IE) bssm.pixelTop += smooth;
                if (NS6 || NS) bssm.top = parseInt(bssm.top) + smooth
                lastY = lastY + smooth;
                setTimeout('makeStatic()', 1)
            }
        }

        function buildBar() {
            if (barText.indexOf('<IMG') > -1) { tempBar = barText }
            else { for (b = 0; b < barText.length; b++) { tempBar += barText.charAt(b) + "<BR>" } }
            document.write('<td align="center" rowspan="' + ssmItems.length + 1 + '" width="' + barWidth + '" bgcolor="' + barBGColor + '" valign="' + barVAlign + '"><p align="center"><font face="' + barFontFamily + '" Size="' + barFontSize + '" COLOR="' + barFontColor + '"><B>' + tempBar + '</B></font></p></TD>')
        }

        function initSlide() {
            if (NS6) {
                ssm = document.getElementById("thessm").style; bssm = document.getElementById("basessm").style;
                bssm.clip = "rect(0 " + document.getElementById("thessm").offsetWidth + " " + document.getElementById("thessm").offsetHeight + " 0)"; ssm.visibility = "visible";
            }
            else if (IE) {
                ssm = document.all("thessm").style; bssm = document.all("basessm").style
                bssm.clip = "rect(0 " + thessm.offsetWidth + " " + thessm.offsetHeight + " 0)"; bssm.visibility = "visible";
            }
            else if (NS) {
                bssm = document.layers["basessm1"];
                bssm2 = bssm.document.layers["basessm2"]; ssm = bssm2.document.layers["thessm"];
                bssm2.clip.left = 0; ssm.visibility = "show";
            }
            if (menuIsStatic == "yes") makeStatic();
        }

        function buildMenu() {
            if (IE || NS6) { document.write('<DIV ID="basessm" style="visibility:hidden;Position : Absolute ;Left : ' + XOffset + ' ;Top : ' + YOffset + ' ;Z-Index : 20;width:' + (menuWidth + barWidth + 10) + '"><DIV ID="thessm" style="Position : Absolute ;Left : ' + (-menuWidth) + ' ;Top : 0 ;Z-Index : 20;" onmouseover="moveOut()" onmouseout="moveBack()">') }
            if (NS) { document.write('<LAYER name="basessm1" top="' + YOffset + '" LEFT=' + XOffset + ' visibility="show"><ILAYER name="basessm2"><LAYER visibility="hide" name="thessm" bgcolor="' + menuBGColor + '" left="' + (-menuWidth) + '" onmouseover="moveOut()" onmouseout="moveBack()">') }
            if (NS6) { document.write('<table border="0" cellpadding="0" cellspacing="0" width="' + (menuWidth + barWidth + 2) + '" bgcolor="' + menuBGColor + '"><TR><TD>') }
            document.write('<table border="0" cellpadding="0" cellspacing="1" width="' + (menuWidth + barWidth + 2) + '" bgcolor="' + menuBGColor + '">');

            for (i = 0; i < ssmItems.length; i++) {
                if (!ssmItems[i][3]) {
                    ssmItems[i][3] = menuCols; ssmItems[i][5] = menuWidth - 1
                } else if (ssmItems[i][3] != menuCols) ssmItems[i][5] = Math.round(menuWidth * (ssmItems[i][3] / menuCols) - 1);
                if (ssmItems[i - 1] && ssmItems[i - 1][4] != "no") {
                    document.write('<TR>')
                }
                if (!ssmItems[i][1]) {
                    document.write('<td bgcolor="' + hdrBGColor + '" HEIGHT="' + hdrHeight + '" ALIGN="' + hdrAlign + '" VALIGN="' + hdrVAlign + '" WIDTH="' + ssmItems[i][5] + '" COLSPAN="' + ssmItems[i][3] + '"> <font face="' + hdrFontFamily + '" Size="' + hdrFontSize + '" COLOR="' + hdrFontColor + '"><b>' + ssmItems[i][0] + '</b></font></td>');
                } else {
                    if (!ssmItems[i][2]) ssmItems[i][2] = linkTarget;
                    //			document.write('<TD BGCOLOR="'+linkBGColor+'" onmouseover="bgColor=\''+linkOverBGColor+'\'" onmouseout="bgColor=\''+linkBGColor+'\'" HEIGHT="'+hdrHeight+'" WIDTH="'+ssmItems[i][5]+'" COLSPAN="'+ssmItems[i][3]+'" style="Cursor:hand;" ALIGN="'+linkAlign+'" onclick="location.href=\''+ssmItems[i][1]+'\'"><FONT face="'+linkFontFamily+'" Size="'+linkFontSize+'">'+ssmItems[i][0]+'</TD>');
                    document.write('<TD BGCOLOR="' + linkBGColor + '" onmouseover="bgColor=\'' + linkOverBGColor + '\'" onmouseout="bgColor=\'' + linkBGColor + '\'" HEIGHT="' + hdrHeight + '" WIDTH="' + ssmItems[i][5] + '" COLSPAN="' + ssmItems[i][3] + '"><ILAYER><LAYER onmouseover="bgColor=\'' + linkOverBGColor + '\'" onmouseout="bgColor=\'' + linkBGColor + '\'" WIDTH="100%" ALIGN="' + linkAlign + '"><DIV ALIGN="' + linkAlign + '"><FONT face="' + linkFontFamily + '" Size="' + linkFontSize + '"><A HREF="' + ssmItems[i][1] + '" target="' + ssmItems[i][2] + '" CLASS="ssmItems">' + ssmItems[i][0] + '</DIV></LAYER></ILAYER></TD>');
                }

                if (ssmItems[i][4] != "no" && barBuilt == 0) { buildBar(); barBuilt = 1; }
                if (ssmItems[i][4] != "no") { document.write('</TR>') }
            }
            document.write('</table>')
            if (NS6) { document.write('</TD></TR></TABLE>') }
            if (IE || NS6) { document.write('</DIV></DIV>') }
            if (NS) { document.write('</LAYER></ILAYER></LAYER>') }

            theleft = -menuWidth;
            lastY = 0;
            setTimeout('initSlide();', 1);
        }

        YOffset = 90;
        XOffset = 0;
        staticYOffset = 30;
        slideSpeed = 12
        waitTime = 100;        
        menuBGColor = "#000000";
        menuIsStatic = "yes";
        menuWidth = 140;
        menuCols = 2;
        hdrFontFamily = "宋体, Arial, Helvetica";
        hdrFontSize = "2";
        hdrFontColor = "mintcream";
        hdrBGColor = "#97BDAE";
        hdrAlign = "center";
        hdrVAlign = "center";
        hdrHeight = "20";
        linkFontFamily = "宋体, Arial, Helvetica";
        linkFontSize = "2";
        linkBGColor = "whitesmoke";
        linkOverBGColor = "#ffffff";
        linkTarget = "_top";
        linkAlign = "center";
        barBGColor = "#FF9933";
        barFontFamily = "宋体, Arial, Helvetica";
        barFontSize = "2";
        barFontColor = "mintcream";
        barVAlign = "center";
        barWidth = 12;
        barText = "网站导航";
        ssmItems[0] = ["首    页", "", ""]
        ssmItems[1] = ["网页素材", "../wysc.html", ""]
        ssmItems[2] = ["图标图库", "../tbtk.html", ""]
        ssmItems[3] = ["作品欣赏", "../mbxz.html", ""]
        ssmItems[4] = ["免费资源", "../mfzy.html", ""]
        ssmItems[5] = ["源码特效", "../wytx.html", ""]
        ssmItems[6] = ["给我留言", "", ""]
        ssmItems[7] = ["网页制作", "../wyzz.html", ""]


        function clickhref(cHref) { window.location.href = cHref }

        //onmousemove="menu_over(this);" onmouseout="menu_out(this);"
        function menu_over(obj) {
            obj.style.backgroundColor = "whitesmoke";
            obj.style.color = "mintcream";
            obj.style.cursor = "hand";
        }

        function menu_out(obj) {
            obj.style.backgroundColor = "#ffffff";
            obj.style.color = "mintcream";
            obj.style.cursor = "";
        }

        buildMenu();
    </script>--%>
</body>
</html>
