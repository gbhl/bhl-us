<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TitleItemMarc.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.TitleItemMarc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Original MARC Record</title>
	<link rel="stylesheet" type="text/css" runat="server" href="/styles/adminstyle.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
	    <asp:Literal ID="litMarc" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
