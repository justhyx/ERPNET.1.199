<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockIn_SaveOk.aspx.cs"
    Inherits="ERPPlugIn.StockInManager.StockIn_SaveOk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Save Ok</title>
    <script type="text/javascript">
        var Sec = 6;
        function Start() {
            Sec = Sec - 1;
            if (Sec == 0) {
                window.location.href = "StockIn_Index.aspx";
            }
            else {
                document.getElementById("SecondTime").value = Sec;
                setTimeout("Start()", 1000);
            }
        }
    </script>
    <style type="text/css">
        input
        {
            border-width: 0;
            width: 10px;
            font-size: larger;
            font-weight: 200;
            text-align: center;
            color: Red;
        }
        body
        {
            font-size: 18px;
        }
    </style>
</head>
<body onload="Start()">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center">
                    Operation is successful, the system will return to <a href="StockIn_Index.aspx">Goods store</a> in
                    <input type="text" value="5" id="SecondTime" name="SecondTime" />seconds
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

































































