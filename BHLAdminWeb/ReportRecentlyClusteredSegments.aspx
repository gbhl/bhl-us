<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ReportRecentlyClusteredSegments.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.ReportRecentlyClusteredSegments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Recently Clustered Segments</span><hr />
	<p>
    This report shows the 100 most recently created or updated segment "clusters".  A segment "cluster" is two or more segments that have been associated with one another.<br />
    </p>
    <p><b><a href="/services/RecentlyClusteredSegmentsDownloadService.ashx">Click here to download the details of ALL segment clusters</a></b></p>
    <table cellpadding="5" class="boxTable" width="700px" id="segmentList">
        <tr class="SearchResultsHeader" align="left">
            <th align="left">Add/Update Date</th>
            <th align="right">Segment ID</th>
            <th align="left">Relationship</th>
            <th align="left">Edit By</th>
            <th align="right">Item ID</th>
            <th align="right">Start Page ID</th>
            <th align="left">Segment Detail</th>
        </tr>
        <asp:Literal ID="litSegmentListTableRows" runat="server"></asp:Literal>
    </table>
</asp:Content>
