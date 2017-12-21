<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexEquipState.aspx.cs" Inherits="ERPPlugIn.Equip.IndexEquipState" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="refresh"content="300">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 12px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
    <table width="1280" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td colspan="2" align="center" style="font-size:24px; font-weight:bold;">设备状态</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
</table>
<table width="100%" border="1" align="center" cellpadding="4" cellspacing="0" bordercolor="#000000">
        <tr>
          <td colspan="7" align="center" style="font-size:24px; font-weight:bold;">G1</td>
        </tr>
        <tr align="center" style="font-size:18px; font-weight:bold;">
          <td ID="C3204" runat="server"><a href="equip_detail.aspx?t_ename=Conn320T04">320T-4</a></td>
          <td ID="C3806" runat="server"><a href="equip_detail.aspx?t_ename=Conn380T06">380T-6</a></td>
          <td ID="C2809" runat="server"><a href="equip_detail.aspx?t_ename=Conn280T09">280T-9</a></td>
          <td ID="C3203" runat="server"><a href="equip_detail.aspx?t_ename=Conn320T03">320T-3</a></td>
          <td ID="C2808" runat="server"><a href="equip_detail.aspx?t_ename=Conn280T08">280T-8</a></td>
          <td ID="C2002" runat="server"><a href="equip_detail.aspx?t_ename=Conn200T02">200T-2</a></td>
          <td ID="C2807" runat="server"><a href="equip_detail.aspx?t_ename=Conn280T07">280T-7</a></td>
        </tr>
        <tr>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td colspan="3" align="center"><asp:Label ID="T3204" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td>良&nbsp;&nbsp;品</td>
              <td rowspan="4"><span class="style1">不良率<br/>
              </span><font size="5">
              <asp:Label ID="NRA3204" runat="server" Text="Label"></asp:Label>
              </font></td>
            </tr>
            <tr>
              <td><asp:Label ID="N3204" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td class="style1">不良品</td>
            </tr>
            <tr>
              <td class="style1"><asp:Label ID="NR3204" runat="server" Text="Label"></asp:Label></td>
            </tr>
          </table></td>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td colspan="3" align="center"><asp:Label ID="T3806" runat="server" Text="Label"></asp:Label>              </td>
            </tr>
            <tr>
              <td>良&nbsp;&nbsp;品</td>
              <td rowspan="4"><span class="style1">不良率<br />
              </span><font size="5"><asp:Label ID="NRA3806" runat="server" Text="Label"></asp:Label>
              </font></td>
            </tr>
            <tr>
              <td><asp:Label ID="N3806" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td class="style1">不良品:</td>
            </tr>
            <tr>
              <td class="style1"><asp:Label ID="NR3806" runat="server" Text="Label"></asp:Label></td>
            </tr>
          </table></td>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td colspan="3" align="center"><asp:Label ID="T2809" runat="server" Text="Label"></asp:Label>              </td>
            </tr>
            <tr>
              <td>良&nbsp;&nbsp;品</td>
              <td rowspan="4"><span class="style1">不良率<br/>
			  </span><font size="5"><asp:Label ID="NRA2809" runat="server" Text="Label"></asp:Label>
              </font></td>
            </tr>
            <tr>
              <td><asp:Label ID="N2809" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td class="style1">不良品</td>
            </tr>
            <tr>
              <td class="style1"><asp:Label ID="NR2809" runat="server" Text="Label"></asp:Label></td>
            </tr>
          </table></td>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td colspan="3" align="center"><asp:Label ID="T3203" runat="server" Text="Label"></asp:Label>              </td>
            </tr>
            <tr>
              <td>良&nbsp;&nbsp;品</td>
              <td rowspan="4"><span class="style1">不良率<br/>
			   </span><font size="5"><asp:Label ID="NRA3203" runat="server" Text="Label"></asp:Label>
              </font></td>
            </tr>
            <tr>
              <td><asp:Label ID="N3203" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td class="style1">不良品</td>
            </tr>
            <tr>
              <td class="style1"><asp:Label ID="NR3203" runat="server" Text="Label"></asp:Label></td>
            </tr>
          </table></td>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td colspan="3" align="center"><asp:Label ID="T2808" runat="server" Text="Label"></asp:Label>              </td>
            </tr>
            <tr>
              <td>良&nbsp;&nbsp;品</td>
              <td rowspan="4"><span class="style1">不良率 <br/> 
			  </span><font size="5"><asp:Label ID="NRA2808" runat="server" Text="Label"></asp:Label>
              </font></td>
            </tr>
            <tr>
              <td><asp:Label ID="N2808" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td class="style1">不良品</td>
            </tr>
            <tr>
              <td class="style1"><asp:Label ID="NR2808" runat="server" Text="Label"></asp:Label></td>
            </tr>
          </table></td>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td colspan="3" align="center"><asp:Label ID="T2002" runat="server" Text="Label"></asp:Label>              </td>
            </tr>
            <tr>
              <td>良&nbsp;&nbsp;品</td>
              <td rowspan="4"><span class="style1">不良率
			  <br/>
			  </span><font size="5"><asp:Label ID="NRA2002" runat="server" Text="Label"></asp:Label>
              </font></td>
            </tr>
            <tr>
              <td><asp:Label ID="N2002" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td class="style1">不良品</td>
            </tr>
            <tr>
              <td class="style1"><asp:Label ID="NR2002" runat="server" Text="Label"></asp:Label></td>
            </tr>
          </table></td>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td colspan="3" align="center"><asp:Label ID="T2807" runat="server" Text="Label"></asp:Label>              </td>
            </tr>
            <tr>
              <td>良&nbsp;&nbsp;品</td>
              <td rowspan="4"><span class="style1">不良率<br/>
			  </span><font size="5"><asp:Label ID="NRA2807" runat="server" Text="Label"></asp:Label>
              </font></td>
            </tr>
            <tr>
              <td><asp:Label ID="N2807" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td class="style1">不良品                </td>
            </tr>
            <tr>
              <td class="style1"><asp:Label ID="NR2807" runat="server" Text="Label"></asp:Label></td>
            </tr>
          </table></td>
        </tr>
        
		        <tr align="center" style="font-size:18px; font-weight:bold;">
          <td ID="C4501" runat="server"><a href="equip_detail.aspx?t_ename=Conn450T01">450T-1</a></td>
          <td ID="C4702" runat="server"><a href="equip_detail.aspx?t_ename=Conn470T02">470t-2</a></td>
          <td ID="C3805" runat="server"><a href="equip_detail.aspx?t_ename=Conn380T05">380T-5</a></td>
          <td ID="C5301" runat="server"><a href="equip_detail.aspx?t_ename=Conn530T01">530T-1</a></td>
          <td ID="C3804" runat="server"><a href="equip_detail.aspx?t_ename=Conn380T04">380T-4</a></td>
          <td ID="C5501" runat="server"><a href="equip_detail.aspx?t_ename=Conn550T01">550T-1</a></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td colspan="3" align="center"><asp:Label ID="T4501" runat="server" Text="Label"></asp:Label>              </td>
            </tr>
            <tr>              
              <td>良&nbsp;&nbsp;品</td>
              <td rowspan="4"><span class="style1">不良率<br/>
			  </span><font size="5"><asp:Label ID="NRA4501" runat="server" Text="Label"></asp:Label>
              </font></td>
            </tr>
            <tr>
              <td><asp:Label ID="N4501" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td class="style1">不良品</td>
            </tr>
            <tr>
              <td class="style1"><asp:Label ID="NR4501" runat="server" Text="Label"></asp:Label></td>
            </tr>
          </table></td>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td colspan="3" align="center"><asp:Label ID="T4702" runat="server" Text="Label"></asp:Label>              </td>
            </tr>
            <tr>
              <td>良&nbsp;&nbsp;品</td>
              <td rowspan="4"><span class="style1">不良率<br/>
			  </span><font size="5"><asp:Label ID="NRA4702" runat="server" Text="Label"></asp:Label>
              </font></td>
            </tr>
            <tr>
              <td><asp:Label ID="N4702" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td class="style1">不良品</td>
            </tr>
            <tr>
              <td class="style1"><asp:Label ID="NR4702" runat="server" Text="Label"></asp:Label></td>
            </tr>
          </table></td>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td colspan="3" align="center"><asp:Label ID="T3805" runat="server" Text="Label"></asp:Label>              </td>
            </tr>
            <tr>
              <td>良 &nbsp;品</td>
              <td rowspan="4"><span class="style1">不良率<br/>
			  </span><font size="5"><asp:Label ID="NRA3805" runat="server" Text="Label"></asp:Label>
              </font></td>
            </tr>
            <tr>
              <td><asp:Label ID="N3805" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td class="style1">不良品</td>
            </tr>
            <tr>
              <td class="style1"><asp:Label ID="NR3805" runat="server" Text="Label"></asp:Label></td>
            </tr>
          </table></td><td><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td colspan="3" align="center"><asp:Label ID="T5301" runat="server" Text="Label"></asp:Label>              </td>
            </tr>
            <tr>
              <td>良&nbsp;&nbsp;品</td>
              <td rowspan="4"><span class="style1">不良率<br/>
			  </span><font size="5"><asp:Label ID="NRA5301" runat="server" Text="Label"></asp:Label>
              </font></td>
            </tr>
            <tr>
              <td><asp:Label ID="N5301" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td class="style1">不良品</td>
            </tr>
            <tr>
              <td class="style1"><asp:Label ID="NR5301" runat="server" Text="Label"></asp:Label></td>
            </tr>
          </table></td>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td colspan="3" align="center"><asp:Label ID="T3804" runat="server" Text="Label"></asp:Label>              </td>
            </tr>
            <tr>
              <td>良&nbsp;&nbsp;品</td>
              <td rowspan="4">不良率<br/>
			  <font size="5"><asp:Label ID="NRA3804" runat="server" Text="Label"></asp:Label>
              </font></td>
            </tr>
            <tr>
              <td><asp:Label ID="N3804" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td class="style1">不良品</td>              
            </tr>
            <tr>
              <td class="style1"><asp:Label ID="NR3804" runat="server" Text="Label"></asp:Label></td>
            </tr>
          </table></td>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td colspan="3" align="center"><asp:Label ID="T5501" runat="server" Text="Label"></asp:Label>              </td>
            </tr>
            <tr>
              <td>良&nbsp;&nbsp;品</td>
              <td rowspan="4"><span class="style1">不良率<br/>
			  </span><font size="5"><asp:Label ID="NRA5501" runat="server" Text="Label"></asp:Label>
              </font></td>
            </tr>
            <tr>
              <td><asp:Label ID="N5501" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
              <td class="style1">不良品</td>
            </tr>
            <tr>
              <td class="style1"><asp:Label ID="NR5501" runat="server" Text="Label"></asp:Label></td>
            </tr>
          </table></td>
          <td>&nbsp;</td>
        </tr>		
		

        <tr>
          <td colspan="7" align="center" style="font-size:24px; font-weight:bold;">G2</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr align="center">
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr align="center">
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td colspan="7" align="center" style="font-size:24px; font-weight:bold;">G3</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr align="center">
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr align="center">
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
      </table>
    </div>
    </form>
</body>
</html>
