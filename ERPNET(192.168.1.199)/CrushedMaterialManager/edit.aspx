<%@ Page Language="C#" AutoEventWireup="true" Inherits="edit" Codebehind="edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <script type="text/javascript" src= "../My97DatePicker/WdatePicker.js" ></script>
   <script type="text/javascript"src="../My97DatePicker/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" runat="server">
        <tr>
            <td style="width:13%">
                <span>部番</span>
            </td>
            <td style="width:13%">
                <span>不良数量</span>
            </td>          
            <td style="width:13%">
                <span>不良内容</span>
            </td>
            <td style="width:13%">
                <span>生产日期</span>
            </td>
            <td style="width:13%">
                <span>作业员</span>
            </td>
            <td style="width:13%">
                <span>不良发生区</span>
            </td>           
        </tr>
       <tr>
       
       <td style="width:13%">                
                <asp:TextBox ID="TextgoodsName" runat="server"></asp:TextBox>
        </td>
         <td style="width:13%">                
                 <asp:TextBox ID="Textcount" runat="server"></asp:TextBox>
        </td>
         <td style="width:13%">                
                
                <asp:TextBox id="Textbadcontent" rows="4" cols="15" runat="server" 
                    style="width:216px;height:67px;resize:none; " TextMode="MultiLine"></asp:TextBox>
        </td>
         <td style="width:13%">                
             <asp:TextBox ID="TextproduceTime" runat="server" class="Wdate" onfocus="WdatePicker({lang:'zh-cn',readOnly:true})"></asp:TextBox>
        </td>
         <td style="width:13%">                
               <asp:TextBox ID="TextemployeeName" runat="server"></asp:TextBox>
        </td>
        <td style="width:13%">            
                <select runat="server" id="DropDownListproduceArea">
                <option value="客户退货区">客户退货区</option>
                <option value="全检">全检不良品</option>
                <option value="选别不良品">选别不良品</option>
               <option value="G1">G1</option>
                <option value="G2">G2</option>
                <option value="G3">G3</option>
                <option value="丝印区">丝印区</option>
                </select>
        </td>
       </tr>  
       </table>
    </div>
    <asp:Button ID="ButtonOK" runat="server" onclick="ButtonOk_Click" Text="确定" />
    <asp:Button ID="ButtonBack" runat="server" onclick="ButtonBack_Click" Text="返回" />
    </form>
</body>
</html>
