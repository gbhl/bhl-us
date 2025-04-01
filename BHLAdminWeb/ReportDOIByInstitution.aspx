<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="ReportDOIByInstitution.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.ReportDOIByInstitution" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">DOIs By Content Provider</span><hr />
	<p>
    This report shows the number of DOIs assigned to titles and segments, grouped by the content providers of the titles and segments.
    </p>
    <table cellpadding="0" cellspacing="0" width="700px">
    <tr>
        <td>
            <b>Order By:</b>
            <asp:RadioButtonList ID="rblOrderBy" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Selected="True" Value="1" Text="Content Provider Name" />
                <asp:ListItem Value="2" Text="Total Title DOIs" />
                <asp:ListItem Value="3" Text="Total Segment DOIs" />
                <asp:ListItem Value="4" Text="Total DOIs" />
            </asp:RadioButtonList>
        </td>
        <td align="right">
            <asp:Button ID="btnUpdate" runat="server" Text="Update Report" />
            <asp:Button ID="btnDownload" runat="server" Text="Download"  OnClick="btnDownload_Click" />
        </td>
    </tr>
    </table>
    <p>
        <asp:CheckBox ID="chkShow" runat="server" Checked="false" Text="Show Only BHL Libraries" TextAlign="Right" Font-Bold="true" /><br />
        <asp:CheckBox ID="chkIncludeAll" runat="server" Checked="false" Text="Include non-BHL DOIs" TextAlign="Right" Font-Bold="true" />
    </p>
	<asp:GridView ID="institutionList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB"
		Width="1100px" CssClass="boxTable">
		<Columns>
			<asp:BoundField DataField="InstitutionName" HeaderText="Content Provider Name" HeaderStyle-Width="310px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom" ItemStyle-Wrap="true" />
			<asp:BoundField DataField="TitleMinted" HeaderText="BHL-Minted Title DOIs" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Right" />
			<asp:BoundField DataField="TitleAcquired" HeaderText="BHL-Acquired Title DOIs" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Right" />
			<asp:BoundField DataField="TitleNonBHL" HeaderText="Non-BHL Title DOIs" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Right" />
			<asp:BoundField DataField="TitleTotalDOIs" HeaderText="Total Title DOIs" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="Beige" />
			<asp:BoundField DataField="SegmentMinted" HeaderText="BHL-Minted Segment DOIs" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Right"/>
			<asp:BoundField DataField="SegmentAcquired" HeaderText="BHL-Acquired Segment DOIs" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Right" />
			<asp:BoundField DataField="SegmentNonBHL" HeaderText="Non-BHL Segment DOIs" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Right" />
			<asp:BoundField DataField="SegmentTotalDOIs" HeaderText="Total Segment DOIs" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="Beige" />
			<asp:BoundField DataField="TotalDOIs" HeaderText="Total DOIs" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="Wheat" />
		</Columns>
		<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
	</asp:GridView>
</asp:Content>
