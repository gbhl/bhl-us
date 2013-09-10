<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="print.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.Controls.OLBookReader.Viewer.print" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../Bookreader/BookReader.css" />
    <style type='text/css'>
        @media print { .noprint { font-size: 40pt; display: none; } }
    </style>
    <script type='text/javascript'>
        function conditionalPrint() {
            return false;
            var doPrint = true; var agent = navigator.userAgent.toLowerCase();
            if (agent.indexOf('safari') != -1) { doPrint = false; }
            if (doPrint) { print(); }
        }
    </script>
    <title></title>
</head>
<body onload='conditionalPrint(); return false;'>
    <form id="form1" runat="server">
    <p class="noprint" style='text-align:left;float:left;width:70%;font-family:Helvetica;font-size:13px;'>Right-click the page image to download.</p>
    <p class="noprint" style='text-align:right;float:right;width:30%;font-family:Helvetica;font-size:13px;'><button class='BRicon rollover print' title='Print' onclick='print(); return false;'></button> <a href='#' onclick='print(); return false;' title="Print">Print</a></p>
    <br />
    <p style='text-align:center;'>
        <asp:Literal ID="litImage1" runat="server"></asp:Literal>
    </p>
    <p style='text-align: center;'>
        <asp:Literal ID="litImage2" runat="server"></asp:Literal>
    </p>
    </form>
</body>
</html>
