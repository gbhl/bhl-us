<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="ReportDOIByInstitution.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.ReportDOIByInstitution" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">DOIs By Institution</span><hr />
	<p>
    This report shows the number of DOIs assigned to titles and segments, grouped by the institutions that contributed the titles and segments.
    </p>
    <table cellpadding="0" cellspacing="0" width="700px">
    <tr>
        <td>
            <b>Order By:</b>
            <asp:RadioButtonList ID="rblOrderBy" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Selected="True" Value="1" Text="Institution Name" />
                <asp:ListItem Value="2" Text="Title DOIs" />
                <asp:ListItem Value="3" Text="Segment DOIs" />
                <asp:ListItem Value="4" Text="Total DOIs" />
            </asp:RadioButtonList>
        </td>
        <td align="right">
            <asp:Button ID="btnUpdate" runat="server" Text="Update Report" />
        </td>
    </tr>
    </table>
    <p>
        <asp:CheckBox ID="chkShow" runat="server" Checked="false" Text="Show Only BHL Libraries" TextAlign="Right" Font-Bold="true" /><br />
        <asp:CheckBox ID="chkIncludeAll" runat="server" Checked="false" Text="Include non-BHL DOIs" TextAlign="Right" Font-Bold="true" />
    </p>
	<asp:GridView ID="institutionList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB"
		Width="700px" CssClass="boxTable">
		<Columns>
			<asp:BoundField DataField="InstitutionName" HeaderText="Institution Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
			<asp:BoundField DataField="TitleDOIs" HeaderText="Title DOIs" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
			<asp:BoundField DataField="SegmentDOIs" HeaderText="Segment DOIs" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
			<asp:BoundField DataField="TotalDOIs" HeaderText="Total DOIs" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
		</Columns>
		<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
	</asp:GridView>
</asp:Content>
