<%@ Page Language="C#" AutoEventWireup="true" Inherits="ERPPlugIn.CrushedMaterialManager.print" Codebehind="print.aspx.cs" %>

<!DOCTYPE html> 

<html>
<head runat="server">
    <title></title>

    
    <script type="text/javascript">
        function doPrint() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
        }
    </script>
    <style type="text/css">
        body
        {
             font-size:smaller;    
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <!--startprint-->
    <table style="margin:0 auto">
    <tr>
    <td>
    <h2 align="center">粉碎部品记录表</h2>
    
        
        
    </td>
    </tr>
    <tr>
        <td>
        <table>
        <tr>
        <td align="right">
            <asp:Label ID="Label1" runat="server" Text="打印时间"></asp:Label>
            <asp:Label ID="LabelTime" runat="server" Text="Label"></asp:Label>
        </td>
        </tr>
        <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </td>
        </tr>
        <tr>
        <td align="right">
          <asp:Label ID="LabelMoney" runat="server" Text="Label"></asp:Label>
          <p></p>
          <asp:Label ID="LabelAdmin" runat="server" Text="Label"></asp:Label>
          </td>
       </tr>
        </table>
        </td>
    </tr>
        <tr>
        <td>
            
         </td>
        
        </tr>
        
    </table>
    <!--endprint--> 
    <div align="center">
    <asp:Button ID="ButtonBack" runat="server" Text="返回" onclick="ButtonBack_Click" />
        <input type="button" onclick="doPrint()" value="打印" />
        </div>
    </div>
    </form>
</body>
</html>
