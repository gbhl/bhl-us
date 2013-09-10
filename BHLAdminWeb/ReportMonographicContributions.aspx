<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ReportMonographicContributions.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.ReportMonographicContributions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Monographic Contributions</span><hr />
	<p>
    This report shows monographic contributions by the selected institutions in a format suitable for import into the monograph deduper tool.
    </p>
    <table cellpadding="0" cellspacing="0" width="900px">
    <tr>
        <td><b>Show Contributions From:</b></td>
    </tr>
    <tr>
        <td>
            <input type="radio" id="rdoAllNonMembers" class="ContribFrom" clientidmode="Static" runat="server" checked />All non-BHL Members<br />
            <input type="radio" id="rdoNonMember" class="ContribFrom" clientidmode="Static" runat="server" />A selected non-BHL member<br />
            <asp:DropDownList runat="server" ID="ddlNonMembers" ClientIDMode="Static" Enabled="false" style="position:relative; left:20px;"></asp:DropDownList><br />
            <input type="radio" id="rdoAllMembers" class="ContribFrom" clientidmode="Static" runat="server" />All BHL members<br />
            <input type="radio" id="rdoMember" class="ContribFrom" clientidmode="Static" runat="server" />A selected BHL member<br />
            <asp:DropDownList runat="server" ID="ddlMembers" ClientIDMode="Static" Enabled="false" style="position:relative; left:20px;"></asp:DropDownList><br />
        </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td>
            <b>Since (MM/DD/YYYY): </b><asp:TextBox ID="txtSinceDate" runat="server"></asp:TextBox>&nbsp;
            <asp:Button ID="btnShow" runat="server" Text="Show Report" onclick="btnShow_Click" />&nbsp;
            <asp:Button ID="btnDownload" runat="server" Text="Download to CSV" onclick="btnDownload_Click" />&nbsp;
            <asp:Button ID="btnClear" runat="server" Text="Clear Results" OnClick="btnClearResults_Click" />
        </td>
    </tr>
    </table>
    <p></p>
    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
	<asp:GridView ID="monographList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
        AlternatingRowStyle-BackColor="#F7FAFB" Width="900px" CssClass="boxTable" HeaderStyle-VerticalAlign="Bottom">
		<Columns>
			<asp:BoundField DataField="TitleID" HeaderText="Title ID" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
			<asp:BoundField DataField="OCLC" HeaderText="OCLC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
			<asp:BoundField DataField="FullTitle" HeaderText="Full Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
			<asp:BoundField DataField="Authors" HeaderText="Authors" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
			<asp:BoundField DataField="Volume" HeaderText="Volume" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="false" />
			<asp:BoundField DataField="StartYear" HeaderText="Start Year" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
			<asp:BoundField DataField="CallNumber" HeaderText="Call Number" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="false" />
			<asp:BoundField DataField="Publisher" HeaderText="Publisher" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
			<asp:BoundField DataField="PublisherPlace" HeaderText="Publisher Place" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="false" />
			<asp:BoundField DataField="ItemID" HeaderText="Item ID" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
			<asp:BoundField DataField="IdentifierBib" HeaderText="Identifier Bib" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
		</Columns>
		<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
	</asp:GridView>
    <script type="text/javascript">
        $(".ContribFrom").click(function () {
            $("#ddlNonMembers").attr("disabled", true);
            $("#ddlMembers").attr("disabled", true);
            if ($(this).attr('id') == 'rdoNonMember') {
                $("#ddlNonMembers").removeAttr("disabled");
            }
            if ($(this).attr('id') == 'rdoMember') {
                $("#ddlMembers").removeAttr("disabled");
            }
        });
    </script>
</asp:Content>
