<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmailGeneratePdfReceived.ascx.cs" Inherits="MOBOT.BHL.Web2.EmailGeneratePdfReceived" %>
<!DOCTYPE HTML>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>BHL: PDF Request</title>
    <style type="text/css">
        body { background: #f5f1ee; }
    </style>
</head>
<body bgcolor="#f5f1ee" style="background:#f5f1ee;">
<table width="100%" cellpadding="0" cellspacing="0" border="0" align="center" style="background-color:#f5f1ee;"><tr><td align="center">
<table width="600" align="center" border="0" style="background-color:#ffffff;border-right:1px solid #dddddd;border-bottom:1px solid #d5d5d5;border-left:1px solid #e5e5e5;width:600px;margin:40px 0;text-align:left;">
    <tr>
        <td style="background-color:#ffffff;color:#2b3c4d;font-family:Georgia,'Times New Roman',Times,serif;font-size:14px;line-height:22px;margin:0;padding:10px 120px 100px 120px;">
            <p>Your PDF generation request <strong>#<%= PdfID  %></strong> has been received and will be processed shortly.</p>
            <p>When the PDF has been created, an email, which will contain a link to download your PDF, will be sent to this address.</p>
            <p style="margin-top:60px">Kind Regards,<br>
            <a href="http://www.biodiversitylibrary.org/" target="_blank" style="color:#255f99;text-decoration:underline;line-height:28px;"><em>Biodiversity Heritage Library</em></a></p>
        </td>
    </tr>
</table>
</td></tr></table>
</body>
</html>