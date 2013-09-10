<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PagingUserControl.ascx.cs" Inherits="MOBOT.BHL.AdminWeb.PagingUserControl" %>
<asp:Panel ID="Panel1" DefaultButton="pageButton" runat="server">
<table cellpadding="3" cellspacing="0" class="tableHeader" width="100%">
	<tr>
		<td width="33%" style="height: 30px">
			<asp:Label ID="totalRecordsLabel" runat="server" Text="No Records" />
		</td>
		<td style="white-space: nowrap;" align="center" width="33%">
			<asp:ImageButton ID="firstPageButton" runat="server" ImageUrl="/images/firstpage.png"
				ImageAlign="AbsMiddle" OnClick="firstPageButton_Click" Visible="False" />
			<asp:ImageButton ID="prevPageButton" runat="server" ImageUrl="/images/prevpage.png"
				ImageAlign="AbsMiddle" OnClick="prevPageButton_Click" Visible="False" />
			<asp:Label ID="pageLabel" runat="server" />
			<asp:TextBox ID="pageTextBox" runat="server" Enabled="False" Width="25px" CssClass="navBarPage"
				Visible="False" />
			<asp:ImageButton ID="pageButton" runat="server" ImageUrl="/images/blank.gif"
					OnClick="pageButton_Click" Height="0px" Width="0px" />
			<asp:Label ID="totalPagesLabel" runat="server"></asp:Label>
			<asp:ImageButton ID="nextPageButton" runat="server" ImageUrl="/images/nextpage.png"
				ImageAlign="AbsMiddle" OnClick="nextPageButton_Click" Visible="False" />
			<asp:ImageButton ID="lastPageButton" runat="server" ImageUrl="/images/lastpage.png"
				ImageAlign="AbsMiddle" OnClick="lastPageButton_Click" Visible="False" />
		</td>
		<td width="34%">
			&nbsp;</td>
	</tr>
</table>
</asp:Panel>
