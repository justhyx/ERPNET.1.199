<%@ Page Language="C#" AutoEventWireup="true" Inherits="ERPPlugIn.CrushedMaterialManager.print" Codebehind="print.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript" language="javascript">   </script>   
        
   
    <script type="text/javascript" language="javascript">



       function getSure() {
                var msg = "\n\n请确认！";
                if (confirm(msg) == true) {
                    return true;
                } else {
                    return false;
                }
            }
        
    </script>
    <style type="text/css">
    
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
    <td >
        <asp:Label ID="LabelRecord" runat="server" Text="粉碎部品记录表"  CssClass="lbl"></asp:Label>
        <p></p>
        <asp:Label ID="LabelAdmin" runat="server" Text="Label"></asp:Label>
    </td>
    </tr>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        
        <tr>
            <asp:Label ID="LabelMoney" runat="server" Text="Label"></asp:Label>
        <td>
        <asp:Button ID="ButtonBack" runat="server" Text="返回" onclick="ButtonBack_Click" />
        </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
