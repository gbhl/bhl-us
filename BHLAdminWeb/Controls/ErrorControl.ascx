<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ErrorControl.ascx.cs" Inherits="MOBOT.BHL.AdminWeb.ErrorControl" %>
<asp:Panel runat="server" ID="errorPanel" style="margin-right:5px">
	<table style="border: 2px #E00000 solid; width: 100%" cellspacing="0">
		<tr>
			<td style="background-color: #E00000; color: White; font-weight: bold; padding-left: 5px">
				Validation Error(s)
			</td>
		</tr>
		<tr>
			<td runat="server" id="errorCell" style="padding: 5px">
			</td>
		</tr>
	</table>
</asp:Panel>
