<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="GrowthStats.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.GrowthStats" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<br />
<a href="/">&lt; Return to Dashboard</a><br />
<br />
<span class="pageHeader">Expanded Growth Stats</span><hr />

Content Provider:
<asp:DropDownList ID="ddlInstitutions" runat="server">
	<asp:ListItem Value="" Text="(All)"></asp:ListItem>
</asp:DropDownList>&nbsp;&nbsp;&nbsp;
Dates:
<asp:DropDownList ID="ddlTimespan" runat="server">
	<asp:ListItem Value="0" Text="This Month"></asp:ListItem>
	<asp:ListItem Value="1" Text="This Year"></asp:ListItem>
	<asp:ListItem Value="-3" Text="Last 3 Months"></asp:ListItem>
	<asp:ListItem Value="-6" Text="Last 6 Months" Selected="True"></asp:ListItem>
	<asp:ListItem Value="-12" Text="Last 12 Months"></asp:ListItem>
	<asp:ListItem Value="-24" Text="Last 24 Months"></asp:ListItem>
	<asp:ListItem Value="-36" Text="Last 36 Months"></asp:ListItem>
	<asp:ListItem Value="2" Text="Since Inception"></asp:ListItem>
</asp:DropDownList>&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnShow" runat="server" Text="Show" onclick="btnShow_Click" />
<asp:Button ID="btnDownload" runat="server" Text="Download All For Content Provider" OnClick="btnDownload_Click" />
<br /><br />
<div class="exGraphDiv" id="itemsGraph"></div>
<div class="exGraphDiv" id="pagesGraph"></div>
<div class="exGraphDiv" id="namesGraph"></div>
<div class="exGraphDiv" id="segmentsGraph"></div>

<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
<script type="text/javascript">
	google.charts.load('current', {'packages':['bar']});
	google.charts.setOnLoadCallback(graphData);

	function graphData() {
		var itemsData = new google.visualization.arrayToDataTable([
			['', ''],
			<%= itemsData%>
		]);
		var pagesData = new google.visualization.arrayToDataTable([
			['', ''],
			<%= pagesData%>
		]);
		var namesData = new google.visualization.arrayToDataTable([
			['', ''],
			<%= namesData %>
		]);
		var segmentsData = new google.visualization.arrayToDataTable([
			['', ''],
			<%= segmentsData %>
		]);

		drawChart('itemsGraph', 'Items Per Month', itemsData);
		drawChart('pagesGraph', 'Pages Per Month', pagesData);
		drawChart('namesGraph', 'Names Per Month', namesData);
		drawChart('segmentsGraph', 'Segments Per Month', segmentsData);
	};
	
	function drawChart(divName, title, data) {
		var segmentsChart = new google.charts.Bar(document.getElementById(divName));
		var options = { legend: { position: 'none' }, title: title };
		segmentsChart.draw(data, google.charts.Bar.convertOptions(options));
	}
</script>
</asp:Content>
