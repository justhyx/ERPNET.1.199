<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="equip_detail.aspx.cs" Inherits="ERPPlugIn.Equip.equip_detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="file:///E|/Mr.Hu/ERPPlugIn2.0/ERPPlugIn/css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td colspan="3">开始时间  
              <asp:TextBox ID="T_sTime" runat="server" Width="90px"></asp:TextBox>
              结束时间 
              <asp:TextBox ID="T_eTime" runat="server" Width="90px"></asp:TextBox>
			  生产部番<asp:TextBox ID="goods_name" runat="server" Width="90px"></asp:TextBox> 
			  不良内容<asp:TextBox ID="Ng" runat="server" Width="90px"></asp:TextBox>
&nbsp;&nbsp;
<asp:Button ID="Button1" runat="server" Text="查询" onclick="Button1_Click" />
	      </td>
        </tr>
      </table>
      <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td colspan="2" valign="top"><asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True">
                  <Columns>
                      <asp:BoundField DataField="goods_name" HeaderText="部番" />
                      <asp:BoundField DataField="equip" HeaderText="机台" />
                      <asp:BoundField DataField="causation" HeaderText="不良内容" />
                      <asp:BoundField DataField="qty" HeaderText="数量" />
                      <asp:BoundField DataField="operate_date" HeaderText="生产日期" />
                      <asp:BoundField FooterText="operator" HeaderText="作业员" />
                      <asp:BoundField DataField="dictate_date" HeaderText="计划日期" />
                      <asp:BoundField DataField="BC" HeaderText="班次" />
                  </Columns>
              </asp:GridView>
          <td valign="top">
              <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                  EnableModelValidation="True">
                  <Columns>
                      <asp:BoundField DataField="生产部番" HeaderText="生产部番" >
                      <ControlStyle Height="20px" />
                      </asp:BoundField>
                      <asp:BoundField DataField="起始时间" HeaderText="起始时间">
                      <ControlStyle Height="18px" />
                      </asp:BoundField>
                      <asp:BoundField DataField="终止时间" HeaderText="结束时间" />
                      <asp:BoundField DataField="合计时间" HeaderText="合计时间" />
                      <asp:BoundField DataField="停机原因" HeaderText="停机原因" />
                      <asp:BoundField DataField="作业日期" HeaderText="作业日期" />
                      <asp:BoundField DataField="班别" HeaderText="班别" />
                  </Columns>
          </asp:GridView>        </tr>
      </table>
              

    </div>
    </form>
</body>
</html>
