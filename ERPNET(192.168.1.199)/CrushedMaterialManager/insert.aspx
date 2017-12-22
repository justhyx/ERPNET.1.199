<%@ Page Language="C#" AutoEventWireup="true" Inherits="test" Codebehind="insert.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript" src="../My97DatePicker/WdatePicker.js" ></script>
     <script type="text/javascript"  src="../My97DatePicker/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table runat="server">
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
              &nbsp;<asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxgoodsName" 
                    ErrorMessage="输入不能为空"></asp:RequiredFieldValidator>
                <asp:TextBox ID="TextBoxgoodsName" runat="server"></asp:TextBox>
            </td>
            <td style="width:13%">
                 &nbsp;<asp:RequiredFieldValidator 
                     ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxcount" 
                     ErrorMessage="输入不能为空"></asp:RequiredFieldValidator>
                 <asp:TextBox ID="TextBoxcount" runat="server"></asp:TextBox>
            </td>
            <td style="width:13%">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="TextBoxbadcontent" ErrorMessage="输入不能为空"></asp:RequiredFieldValidator>
                <textarea id="TextBoxbadcontent" rows="4" cols="15" runat="server" style="width:216px;height:67px;resize:none;"></textarea>
            </td>
            <td style="width:13%">
                
                 <asp:TextBox ID="TextBoxproduceTime" runat="server"  class="Wdate" onfocus="WdatePicker({lang:'zh-cn',readOnly:true})"></asp:TextBox>
            </td>
            <td style="width:13%">
                 &nbsp;<asp:TextBox ID="TextBoxemployeeName" runat="server"></asp:TextBox>
            </td>
            <td style="width:13%">
                <select runat="server" id="DropDownListproduceArea">
                <option value="客户退货区">客户退货区</option>
                <option value="全检不良品">全检不良品</option>
                <option value="选别不良品">选别不良品</option>
               <option value="G1">G1</option>
                <option value="G2">G2</option>
                <option value="G3">G3</option>
                <option value="丝印区">丝印区</option>
                </select>
            </td>
          
        </tr>
        <tr>
            <td>
                &nbsp;<asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="提交" />
                <asp:Button ID="Button2" runat="server" CausesValidation="False" 
                    onclick="Button2_Click" Text="返回" />
            </td>
        </tr>
    </table>
    </div>
    </form>
   
</body>
</html>
