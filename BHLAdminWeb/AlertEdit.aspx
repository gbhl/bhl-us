<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="True" CodeBehind="AlertEdit.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.AlertEdit" 
    ValidateRequest="false" %>
    
<%@ Register TagPrefix="FCKeditorV2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Alert Message</span><hr />
	<br />	
	<div style="width: 850px"><table width=100%><tr><td>
		<FCKeditorV2:FCKeditor ID="txtAlertMessage" runat="server" BasePath="/Controls/FCKeditor/" />
	</td></tr></table></div>
	<br />
	Click one of the following message templates to copy it to the editor.<br />
	<asp:LinkButton ID="lnkButton1" Text="Site maintenance lasting {hours} hour(s) is scheduled to begin at {time} CST on {date}." runat="server" OnClick="lnkButton_Click"></asp:LinkButton><br />
	<asp:LinkButton ID="lnkButton2" Text="We are experiencing slow response times.  Please be patient." runat="server" OnClick="lnkButton_Click"></asp:LinkButton><br />
	<br />
	<br />
	<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
	<asp:Button ID="clearButton" runat="server" Text="Clear" OnClick="clearButton_Click" />
</asp:Content>
