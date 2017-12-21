<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ERPPlugIn.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="Images/admin.js" type="text/javascript"></script>
    <script src="Images/Admin1.js" type="text/javascript"></script>
    <link href="Images/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .main_left
        {
            table-layout: auto;
            background: url(../images/left_bg.gif);
        }
        .main_left_top
        {
            background: url(../images/left_menu_bg.gif);
            padding-top: 5px;
        }
        .main_left_title
        {
            padding-left: 15px;
            font-weight: bold;
            font-size: 14px;
            color: #fff;
            text-align: left;
        }
        .left_iframe
        {
            background: none transparent scroll repeat 0% 0%;
            visibility: inherit;
            width: 180px;
            height: 800px;
        }
        .main_iframe
        {
            z-index: 1;
            visibility: inherit;
            width: 98%;
            height: 800px;
            margin-right: 25px;
        }
        TABLE
        {
            font-size: 12px;
            font-family: tahoma, 宋体, fantasy;
        }
        TD
        {
            font-size: 12px;
            font-family: tahoma, 宋体, fantasy;
        }
        .style1
        {
            width: 538px;
        }
        .style2
        {
            width: 565px;
        }
    </style>
    <script type="text/javascript">
        var status = 1;
        var Menus = new DvMenuCls;
        document.onclick = Menus.Clear;
        function switchSysBar() {
            if (1 == window.status) {
                window.status = 0;
                switchPoint.innerHTML = '<img src="../images/left.gif">';
                document.all("frmTitle").style.display = "none"
            }
            else {
                window.status = 1;
                switchPoint.innerHTML = '<img src="../images/right.gif">';
                document.all("frmTitle").style.display = ""
            }
        }
        function window.onunload() {
            if (event.clientX < 0 && event.clientY < 0)
                window.open("logout.aspx", "logout");
        }
        //        function tick() {
        //            document.getElementById("UserCount").innerHTML = <%=getCount() %>;
        //        }
        //        window.setInterval(tick, 1000);
    </script>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
    <div>
        <div class="top_table">
            <div class="top_table_leftbg">
                <div class="system_logo">
                </div>
                <div class="menu">
                    <ul>
                        <li id="menu_0" onmouseover="Menus.Show(this,0)" onclick="getleftbar(this);"><a href="#">
                            系统维护</a>
                            <div class="menu_childs" onmouseout="Menus.Hide(0);">
                                <ul>
                                    <li><a href="../oper/oper_add.asp?action=add" target="frmright" runat="server" id="UserManager">
                                        用户管理</a></li>
                                    <li style="color: #D2D2D2;"><a href="../customer/customer_list.asp" target="frmright">
                                        客户管理</a></li>
                                    <li style="color: #D2D2D2;"><a href="Vendor_List.aspx" target="frmright">供应商管理</a></li>
                                    <li><a href="../goods/goods_list.asp" target="frmright">商品档案</a></li>
                                    <li><a href="../price/uplaod.asp" target="frmright">商品单价</a></li>
                                    <li><a href="../price/price_set.asp" target="frmright">币种设置</a> </li>
                                    <li style="color: #D2D2D2;"><a href="../equip/equip_list.asp" target="frmright">设备档案</a>
                                    </li>
                                    <li style="color: #D2D2D2;"><a>采购环境</a> </li>
                                    <li><a href="../materials/materials_list.asp" target="frmright">材料档案</a></li>
                                    <li style="color: #D2D2D2;"><a>仓库环境</a> </li>
                                    <li><a href="../goods/chang_key.asp" target="frmright">单价查询</a></li>
                                    <li style="color: #D2D2D2;"><a>新规维护</a></li>
                                    <li style="color: #D2D2D2;"><a href="dbback.asp" target="frmright">数据备份</a> </li>
                                </ul>
                            </div>
                            <div class="menu_div">
                                <img style="vertical-align: bottom" src="../images/menu01_right.gif"></div>
                        </li>
                        <li id="menu_1" onmouseover="Menus.Show(this,0)" onclick="getleftbar(this);"><a href="../xsht/xsht_index.asp"
                            target="frmright">销售管理</a>
                            <div class="menu_childs" onmouseout="Menus.Hide(0);">
                                <ul>
                                    <li><a href="../xsht/xsht_a_1.asp" target="frmright">销售订单制单</a> </li>
                                    <li><a href="../xsht/xsht_chk_list.asp" target="frmright">销售订单审核</a> </li>
                                    <li><a href="../delivery_plan/delivery_a_1.asp" target="frmright">交货计划制单</a> </li>
                                    <li style="color: #D2D2D2;"><a>销售订单更改</a> </li>
                                    <li style="color: #D2D2D2;"><a>交货制单更改</a> </li>
                                    <li><a href="StockManager/Stock_Output.aspx" target="frmright">发货制单</a></li>
                                    <li><a href="../xsht/fhzd_2_a.asp" target="frmright">发货审核</a></li>
                                    <li style="color: #D2D2D2;"><a href="StockManager/Stock_Output.aspx" target="frmright">
                                        交货指示</a></li>
                                </ul>
                            </div>
                            <div class="menu_div">
                                <img style="vertical-align: bottom" src="../images/menu01_right.gif" /></div>
                        </li>
                        <li id="menu_2" onmouseover="Menus.Show(this,0)" onclick="getleftbar(this);"><a href="#">
                            生产作业</a>
                            <div class="menu_childs" onmouseout="Menus.Hide(0);">
                                <ul>
                                    <li><a href="../prd_dictate/dic_list.asp" target="frmright">制令编辑</a></li>
                                    <li style="color: #D2D2D2;"><a>制令更改</a></li>
                                    <li style="color: #D2D2D2;"><a>材料预测</a></li>
                                    <li><a href="../ribao/Pic_add.asp" target="frmright">日报上传</a></li>
                                    <li><a href="../ribao/pic_rb_list.asp" target="frmright">日报查询</a></li>
                                    <li><a href="../goods/goods_area.asp" target="frmright">分类区域设定</a></li>
                                    <li><a href="mainfirm.aspx" target="frmright">量产准备</a></li>
                                </ul>
                            </div>
                            <div class="menu_div">
                                <img style="vertical-align: bottom" src="../images/menu01_right.gif" /></div>
                        </li>
                        <li id="menu_3" onmouseover="Menus.Show(this,0)" onclick="getleftbar(this);"><a href="#">
                            物流作业</a>
                            <div class="menu_childs" onmouseout="Menus.Hide(0);">
                                <ul>
                                    <li><a href="../Maintain_Detection/peishan_canku_y.asp">配善仓库</a></li>
                                    <li><a href="../Maintain_Detection/peishan_y.asp">配善现场</a></li>
                                </ul>
                            </div>
                            <div class="menu_div">
                                <img style="vertical-align: bottom" src="../images/menu01_right.gif"></div>
                        </li>
                        <li id="menu_5" onmouseover="Menus.Show(this,0)" onclick="getleftbar(this);"><a href="../prd_pro/prd_pro_index.asp"
                            target="frmright">原料采购</a>
                            <div class="menu_childs" onmouseout="Menus.Hide(0);">
                                <ul>
                                    <li><a href="../prd_pro/prd_pro_add.asp" target="frmright">原料采购订单制单</a></li>
                                    <li><a href="../prd_pro/prd_materials_check_list.asp" target="frmright">原料采购订单审核</a></li>
                                    <li style="color: #D2D2D2;"><a>染色外发加工制单</a></li>
                                    <li style="color: #D2D2D2;"><a>染色外发加工审核</a></li>
                                    <li style="color: #D2D2D2;"><a href="MaterialForecastManager/ForecastInput.aspx"
                                        target="frmright">原料预测</a></li>
                                    <li style="color: #D2D2D2;"><a>造粒料查询</a></li>
                                </ul>
                            </div>
                            <div class="menu_div">
                                <img style="vertical-align: bottom" src="../images/menu01_right.gif"></div>
                        </li>
                        <li id="menu_7" onmouseover="Menus.Show(this,0)" onclick="getleftbar(this);"><a href="../prd_stock/prd_stock_index.asp"
                            target="frmright">原料仓库</a>
                            <div class="menu_childs" onmouseout="Menus.Hide(0);">
                                <ul>
                                    <li><a href="../prd_stock/prd_stock_index.asp" target="frmright">材料仓库管理</a></li>
                                    <li style="color: #D2D2D2;"><a>材料期初入库</a></li>
                                    <li style="color: #D2D2D2;"><a>原料采购入库</a></li>
                                    <li style="color: #D2D2D2;"><a>回收加工入库</a></li>
                                    <li style="color: #D2D2D2;"><a href="MaterailsManager/MaterailsStock_Begin.aspx"
                                        target="frmright">启动盘点</a></li>
                                    <li style="color: #D2D2D2;"><a href="MaterailsManager/MaterailsStock_KeyIn.aspx"
                                        target="frmright">数据录入</a></li>
                                    <li style="color: #D2D2D2;"><a href="MaterailsManager/MaterailsStock_Confirm.aspx"
                                        target="frmright">审核</a></li>
                                    <li style="color: #D2D2D2;"><a href="MaterailsManager/MaterailsStock_FinalConfirm.aspx"
                                        target="frmright">最终审核</a></li>
                                    <li><a href="../prd_stock/prd_out_chk_list.asp" target="frmright">材料出库审核</a></li>
                                    <li><a href="../prd_stock/prd_in_check_list.asp" target="frmright">材料入库单审核</a></li>
                                </ul>
                            </div>
                            <div class="menu_div">
                                <img style="vertical-align: bottom" src="../images/menu01_right.gif"></div>
                        </li>
                        <li id="menu_8" onmouseover="Menus.Show(this,0)" onclick="getleftbar(this);"><a href="StockInManager/StockIn_Index.aspx"
                            target="frmright">成品仓库</a>
                            <div class="menu_childs" onmouseout="Menus.Hide(0);">
                                <ul>
                                    <li><a href="StockInManager/StockIn_Index.aspx" target="frmright">成品入库制单</a></li>
                                    <li style="color: #D2D2D2;"><a>成品退库制单</a></li>
                                    <li><a href="../str_stock/str_in_check_list.asp" target="frmright">成品入库审核</a></li>
                                </ul>
                            </div>
                            <div class="menu_div">
                                <img style="vertical-align: bottom" src="../images/menu01_right.gif"></div>
                        </li>
                        <li id="menu_10" onmouseover="Menus.Show(this,0)" onclick="getleftbar(this);"><a
                            href="#">盘点报告</a>
                            <div class="menu_childs" onmouseout="Menus.Hide(0);">
                                <ul>
                                    <li style="color: #D2D2D2;"><a>盘点数据</a></li>
                                    <li style="color: #D2D2D2;"><a>盘点票处置</a></li>
                                    <li style="color: #D2D2D2;"><a>盘点票修正</a></li>
                                </ul>
                            </div>
                            <div class="menu_div">
                                <img style="vertical-align: bottom" src="../images/menu01_right.gif"></div>
                        </li>
                        <li id="menu_9" onmouseover="Menus.Show(this,0)" onclick="getleftbar(this);"><a href="#">
                            成形日报</a>
                            <div class="menu_childs" onmouseout="Menus.Hide(0);">
                                <ul>
                                    <li style="color: #D2D2D2;"><a>日报汇报</a></li>
                                    <!--<LI><A href="#" target=_blank>XXX</A></LI>   
   <LI><A href="#" target=_blank>XXX</A></LI>-->
                                </ul>
                            </div>
                            <div class="menu_div">
                                <img alt="" style="vertical-align: bottom" src="../images/menu01_right.gif"></div>
                        </li>
                        <li id="menu_11" onmouseover="Menus.Show(this,0)" onclick="getleftbar(this);"><a
                            href="../molditem/molditem_index.asp" target="frmright">模具管理</a>
                            <div class="menu_childs" onmouseout="Menus.Hide(0);">
                                <ul>
                                    <li><a href="../molditem/mold_list.asp" target="frmright">模具列表</a></li>
                                    <li><a href="../molditem/shot_wh.asp" target="frmright">Shot保养列表</a></li>
                                    <li><a href="../molditem/modify_mold_a.asp" target="frmright">修模履历</a></li>
                                    <li><a href="../molditem/lifetime_wh.asp" target="frmright">寿命保养列表</a></li>
                                    <li><a href="../molditem/repair_list.asp" target="frmright">修模中列表</a></li>
                                    <li><a href="../molditem/b_wh_list.asp" target="frmright">保养中列表</a></li>
                                    <li><a href="../molditem/repair_add.asp" target="frmright">修模依赖</a></li>
                                    <li><a href="../molditem/xb_qs_list_a.asp" target="frmright">修模保养签收</a></li>
                                    <li><a href="../molditem/pd_wh_list_a.asp" target="frmright">修理保养待判定列表</a></li>
                                    <li><a href="../molditem/by_bc.asp" target="frmright">修理保养汇报</a></li>
                                </ul>
                            </div>
                            <div class="menu_div">
                                <img alt="" style="vertical-align: bottom" src="../images/menu01_right.gif" /></div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div style="background: #337abb; height: 50px" align="left">
            <table width="100%" height="50" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="190">
                        <img alt="" src="../Images/500_03.gif" width="190" height="50" />
                    </td>
                    <td width="160">
                        <img alt="" src="../Images/500_04.gif" width="150" height="31" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table style="background: #337abb; height: 100%;" cellspacing="0" cellpadding="0"
                width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="main_left" id="frmTitle" valign="top" align="center" name="fmTitle">
                            <table class="main_left_top" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr style="height: 32px">
                                        <td valign="top">
                                        </td>
                                        <td class="main_left_title" id="leftmenu_title">
                                            常用快捷功能
                                        </td>
                                        <td valign="top" align="right">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <iframe class="left_iframe" id="frmleft" name="frmleft" src="left.htm" frameborder="0">
                            </iframe>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr style="height: 32px">
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td style="width: 10px" bgcolor="#337abb">
                            <table style="height: 100%" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td style="height: 100%" onclick="switchSysBar()">
                                            <span class="navPoint" id="switchPoint" title="关闭/打开左栏">
                                                <img src="../images/right.gif"></span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td valign="top" width="100%" bgcolor="#337abb">
                            <table cellspacing="0" cellpadding="6" width="100%" bgcolor="#c4d8ed" border="0">
                                <tbody>
                                    <tr height="32">
                                        <td valign="top" width="10" background="../images/bg2.gif">
                                            &nbsp;
                                        </td>
                                        <td width="28" background="../images/bg2.gif">
                                            <span style="font-size: 14px; font-weight: bold;">HUDSON_ERP</span>
                                        </td>
                                        <td background="../images/bg2.gif" style="font-size: 14px; font-weight: bold;">
                                            &nbsp;
                                        </td>
                                        <td style="color: #135294; text-align: right;" background="../images/bg2.gif" class="style2">
                                            <a href="javascript:history.go(-1);">后退</a> | <a href="#" target="_top">工作台</a>
                                            |<a href="logout.aspx" target="_top"> 退出</a>
                                        </td>
                                        <td align="right" background="../images/bg2.gif" style="width: 150px">
                                            在线人数：
                                            <asp:Label Text="0" ID="UserCount" runat="server" />
                                        </td>
                                        <td align="right" width="13" bgcolor="#337abb">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <iframe class="main_iframe" id="frmright" name="frmright" src="syscome.html" frameborder="0"
                                scrolling="yes" height="800px" onload="javascript:dyniframesize('frmright');">
                            </iframe>
                            <table style="background: #c4d8ed;" cellspacing="0" cellpadding="0" width="100%"
                                border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <img height="6" alt="" src="images/teble_bottom_left.gif" width="5" />
                                        </td>
                                        <td align="right">
                                            <img height="6" alt="" src="images/teble_bottom_right.gif" width="5" />
                                        </td>
                                        <td align="right" width="16" bgcolor="#337abb">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        function dyniframesize(down) {
            var pTar = null;
            if (document.getElementById) {
                pTar = document.getElementById(down);
            }
            else {
                eval('pTar = ' + down + ';');
            }
            if (pTar && !window.opera) {
                //begin resizing iframe 
                pTar.style.display = "block"
                if (pTar.contentDocument && pTar.contentDocument.body.offsetHeight) {
                    //ns6 syntax 
                    pTar.height = pTar.contentDocument.body.offsetHeight + 20;
                    pTar.width = pTar.contentDocument.body.scrollWidth + 20;
                }
                else if (pTar.Document && pTar.Document.body.scrollHeight) {
                    //ie5+ syntax 
                    pTar.height = pTar.Document.body.scrollHeight;
                    pTar.width = pTar.Document.body.scrollWidth;
                }
            }
        } 

    </script>
    </form>
</body>
</html>
