<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SectionBrowseControl.ascx.cs" Inherits="MOBOT.BHL.Web2.SectionBrowseControl" %>
<asp:Repeater ID="sectionRepeater" runat="server">
    <ItemTemplate>
        <li class="titlelisting">
            <a href="/part/<%# Eval("SegmentID")%>" title="Segment"><%# Eval("Title") %></a>
            <%# Eval("Authors") == string.Empty ? "" : "<div class=\"titledetails\">By: " + Eval("Authors").ToString().Replace("|", " - ")+"</div>" %>
            <%# Eval("GenreName") == string.Empty ? "" : "<div class=\"titledetails\">Type: " + Eval("GenreName").ToString()+"</div>" %>
            <%# Eval("ContainerTitle") == string.Empty ? "" : "<div class=\"titledetails\">In: " + Eval("ContainerTitle").ToString()+"</div>" %>
            <%# Eval("Volume") == string.Empty ? "" : "<div class=\"titledetails\">Volume: " + Eval("Volume") + "</div>"%>
            <%# Eval("Series") == string.Empty ? "" : "<div class=\"titledetails\">Series: " + Eval("Series") + "</div>"%>
            <%# Eval("Issue") == string.Empty ? "" : "<div class=\"titledetails\">Issue: " + Eval("Issue") + "</div>"%>
            <%# Eval("Date") == string.Empty ? "" : "<div class=\"titledetails\">Date: " + Eval("Date") + "</div>"%>
            <%# Eval("PageRange") == string.Empty ? "" : "<div class=\"titledetails\">Page Range: " + Eval("PageRange") + "</div>"%>
            <%# Eval("PublicationDetails") == string.Empty ? "" : "<div class=\"titledetails\">Publication info: " + Eval("PublicationDetails") + "</div>"%>
            <%# Eval("StartPageID") == null ? "":"<a class=\"titleviewbook\" href=\"/page/" + Eval("StartPageID")+ "\">View "+  Eval("GenreName")+ "</a> "%>
            <%# Eval("URL") == string.Empty ? "":"<a class=\"titleviewbook\" href=\"" + Eval("URL")+ "\">View "+  Eval("GenreName")+ " (External)</a>" %>
            <%# Eval("DownloadURL") == string.Empty ? "":"<a class=\"titleviewbook\" href=\"" + Eval("DownloadURL")+ "\">Download "+  Eval("GenreName")+ "</a>" %>
        </li>
	</ItemTemplate>
	<HeaderTemplate>
		<ol class="data titles">
	</HeaderTemplate>
	<FooterTemplate>
		</ol>
    </FooterTemplate>
</asp:Repeater>
