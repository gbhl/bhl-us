<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>BHL Admin</title>
	<link rel="stylesheet" type="text/css" runat="server" id="link1" href="styles/adminstyle.css" />
</head>
<body>
    <form id="form1" runat="server">
	<div id="firefoxKludge" style="position: absolute; margin: 0; padding: 0; border: none; top: 0px; left: 0px; bottom: 0px; right: 0px; visibility: hidden; overflow: hidden;"></div>
	<div style="left: 0px; top: 0px; position: absolute; width:100%">
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td style="width: 250px;">
					<a href="/">
						<img src="images/BHLlogo.png" width="195" height="70" style="border: 0; margin-left:15px;" /></a></td>
				<td>
					<img src="images/blank.gif" alt="" height="1" width="15" />
					&nbsp;
					<div style="position: absolute; top: 40px; right: 15px;">
						<span class="pageheader">Administration</span>
					</div>
				</td>
			</tr>
            <tr>
                <td style="height:4px; background-color:#27638C" colspan="2"></td>
            </tr>
		</table>
	</div>
	<div style="position:absolute; top:150px; text-align:center; width:100%">
		<table width="35%" border="0" cellspacing="0" cellpadding="0" style="margin:auto">
		    <tr>
		        <td align="center" colspan="2">
		            <div class="pageHeader">You must log in to administer content of the Biodiversity Heritage Library.</div>
		        </td>
		    </tr>
		    <tr><td>&nbsp;</td></tr>
		    <tr>
		        <td align="center">
                    <div class="pagesubheader"><a href="/ligustrum.aspx?send=1">Login</a></div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
