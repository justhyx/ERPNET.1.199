<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockIn_Index.aspx.cs"
    Inherits="ERPPlugIn.StockInManager.StockIn_Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>成品仓库管理</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var menu1 = new Array()
        menu1[0] = '<a href=../InventoryManager/Inventory_Begin.aspx target=view_window><%=Resources.Resource.qdpd%></a><br>'
        menu1[1] = '<a href=../InventoryManager/Inventory_KeyIn.aspx target=view_window><%=Resources.Resource.pdblr%></a><br>'
        menu1[2] = '<a href=../InventoryManager/Inventory_Confirm.aspx target=view_window><%=Resources.Resource.pdbqr%></a><br>'
        menu1[3] = '<a href=../InventoryManager/Inventory_FinalConfirm.aspx target=view_window><%=Resources.Resource.pdbsh%></a><br>'
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="580" border="0" align="center" cellpadding="4" cellspacing="0">
            <tr>
                <td align="center">
                    <a href="StockIn_Create.aspx">
                        <img alt="" src="str_stock_img/str_in_do.gif" width="70" height="70" border="0" /></a>
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    <a href="StockIn_ConfirmList.aspx">
                        <img alt="" src="str_stock_img/str_in_auditer.gif" width="70" height="70" border="0" /></a>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <a href="StockIn_Create.aspx">成品入库制单</a>
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    <a href="StockIn_ConfirmList.aspx">成品入库制单审核</a>
                </td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <a href="StockOutCreate.aspx">
                        <img src="str_stock_img/str_out_do.gif" alt="" width="70" height="70" border="0" /></a>
                </td>
                <td align="center">
                    <a href="StockOut_ConfirmList.aspx">
                        <img src="str_stock_img/str_out_audit.gif" width="70" height="70" border="0" /></a>
                </td>
                <td align="center" onclick="dropit2(dropmenu0,65,60);event.cancelBubble=true;return false">
                    <a href="#" onclick="if(document.layers) return dropit(event, 'document.dropmenu0')">
                        <img src="str_stock_img/str_pd.gif" width="70" height="70" border="0" /></a>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <a href="StockOutCreate.aspx" style="cursor: pointer;">成品出库制单</a>
                </td>
                <td align="center">
                    <a href="StockOut_ConfirmList.aspx">成品出库制单审核</a>
                </td>
                <td align="center" onclick="dropit2(dropmenu0,135,15);event.cancelBubble=true;return false">
                    <a href="#" onclick="if(document.layers) return dropit(event, 'document.dropmenu0')">
                        成品盘点</a>
                </td>
            </tr>
        </table>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <table width="760" height="90" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="90" rowspan="5">
                    <img src="str_stock_img/search.gif" width="90" height="90" />
                </td>
                <td width="20" height="30">
                    <img src="str_stock_img/b_frst.gif" width="20" height="30" />
                </td>
                <td background="str_stock_img/down_lin.gif">
                    入、出、转、盘库单查询
                </td>
                <td width="20">
                    <img src="str_stock_img/b_frst.gif" width="20" height="30" />
                </td>
                <td background="str_stock_img/down_lin.gif">
                    <a href="str_in_bill_list.asp">成品入库台帐</a>
                </td>
                <td width="20">
                    <img src="str_stock_img/b_frst.gif" width="20" height="30" />
                </td>
                <td background="str_stock_img/down_lin.gif">
                    <a href="str_out_bill_list.asp">成品出库台帐</a>
                </td>
            </tr>
            <tr>
                <td height="30">
                    <img src="str_stock_img/b_frst.gif" width="20" height="30" />
                </td>
                <td background="str_stock_img/down_lin.gif">
                    <a href="str_stock_remian.asp" style="cursor: pointer;">成品库存余额</a>
                </td>
                <td>
                    <img src="str_stock_img/b_frst.gif" width="20" height="30" />
                </td>
                <td background="str_stock_img/down_lin.gif">
                    成品流水明细
                </td>
                <td>
                    <img src="str_stock_img/b_frst.gif" width="20" height="30" />
                </td>
                <td background="str_stock_img/down_lin.gif">
                    成品缺货、超库限查询
                </td>
            </tr>
            <tr>
                <td height="30">
                    <img src="str_stock_img/b_frst.gif" width="20" height="30" />
                </td>
                <td background="str_stock_img/down_lin.gif">
                    成品在单查询
                </td>
                <td background="str_stock_img/down_lin.gif">
                    <img src="str_stock_img/b_frst.gif" width="20" height="30" />
                </td>
                <td background="str_stock_img/down_lin.gif">
                    成品库存历史记录查询
                </td>
                <td background="str_stock_img/down_lin.gif">
                    &nbsp;
                </td>
                <td background="str_stock_img/down_lin.gif">
                    &nbsp;
                </td>
            </tr>
        </table>
        <p>
            &nbsp;</p>
        <script type="text/javascript">


            var zindex = 100

            function dropit2(whichone, left, right) {

                if (window.themenu && themenu.id != whichone.id)

                    themenu.style.visibility = "hidden"

                themenu = whichone

                if (document.all) {

                    themenu.style.left = document.body.scrollLeft + event.clientX - event.offsetX + left

                    themenu.style.top = document.body.scrollTop + event.clientY - event.offsetY + right

                    if (themenu.style.visibility == "hidden") {

                        themenu.style.visibility = "visible"

                        themenu.style.zIndex = zindex++

                    }

                    else {

                        hidemenu()

                    }

                }

            }



            function dropit(e, whichone) {

                if (window.themenu && themenu.id != eval(whichone).id)

                    themenu.visibility = "hide"

                themenu = eval(whichone)

                if (themenu.visibility == "hide")

                    themenu.visibility = "show"

                else

                    themenu.visibility = "hide"

                themenu.zIndex++

                themenu.left = e.pageX - e.layerX

                themenu.top = e.pageY - e.layerY + 19

                return false

            }



            function hidemenu(whichone) {

                if (window.themenu)

                    themenu.style.visibility = "hidden"

            }

            if (document.all)

                document.body.onclick = hidemenu



            //以上不要改

        </script>
        <div id="dropmenu0" style="position: absolute; left: 0; top: 0; layer-background-color: white;
            background-color: white; width: 200; visibility: hidden; border: 1px solid black;
            padding: 0px">
            <script type="text/javascript">

                if (document.all)

                    dropmenu0.style.padding = "4px"

                for (i = 0; i < menu1.length; i++)

                    document.write(menu1[i])

            </script>
        </div>
        <script type="text/javascript">

            if (document.layers) {

                document.dropmenu0.captureEvents(Event.CLICK)

                document.dropmenu0.onclick = hidemenu2

            }

        </script>
        <script type="text/javascript">

            if (document.layers) {

                document.dropmenu1.captureEvents(Event.CLICK)

                document.dropmenu1.onclick = hidemenu2

            }

        </script>
    </div>
    </form>
</body>
</html>







































































































