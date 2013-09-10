<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlickrLoginRedirect.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.FlickrLoginRedirect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="mainDiv" runat="server">
            <span style="font-size: larger; font-weight: bold; text-align: center;"><asp:Label ID="UploadStatus" runat="server" text="Uploading Images" /></span>
            <img src="images/loading.gif" style="text-align:center;" />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
