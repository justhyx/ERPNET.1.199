<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetBarcod.aspx.cs" Inherits="ERPPlugIn.MaterialControlManager.GetBarcod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function EnterTextBox() {
            if (event.keyCode == 13) {
                event.keyCode = 9;
                event.returnValue = false;
                document.getElementById("sr").click();
            }
        }
    </script>
</head>
<body onkeypress="return EnterTextBox()">
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" ID="txtInput" Width="99.5%" />
        <asp:TextBox ID="txtAlertMsg" runat="server" TextMode="MultiLine" Width="99.5%" 
            Height="400px" ReadOnly="True" />
    </div>
    <div style="display: none">
        <asp:Button Text="text" ID="sr" runat="server" OnClick="sr_Click" />
    </div>
    </form>
</body>
</html>
