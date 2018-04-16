﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SectionBrowseControl.ascx.cs" Inherits="MOBOT.BHL.Web2.SectionBrowseControl" %>
<asp:Repeater ID="sectionRepeater" runat="server">
    <ItemTemplate>
        <li class="titlelisting">
            <%# (Eval("StartPageID") == null && Eval("URL").ToString() == string.Empty) ?  Eval("Title") : "" %>
            <%# (Eval("StartPageID") != null && Eval("URL").ToString() == string.Empty) ? "<a class=\"title\" href=\"/page/" + Eval("StartPageID").ToString() + "\" \\>" + Eval("Title").ToString() + "</a>" : "" %>
            <%# (Eval("StartPageID") == null && Eval("URL").ToString() != string.Empty) ? "<div class=\"title\">[EXTERNAL] <a target=\"_blank\" href=\"" + Eval("URL").ToString() + "\" \\>" + Eval("Title").ToString() + "</a></div>" : "" %>
            <%# Eval("Authors") == string.Empty ? "" : "<div class=\"titledetails\">By: " + Eval("Authors").ToString().Replace("|", " - ")+"</div>" %>
            <%# Eval("GenreName") == string.Empty ? "" : "<div class=\"titledetails\">Type: " + Eval("GenreName").ToString()+"</div>" %>
            <%# Eval("ContainerTitle") == string.Empty ? "" : "<div class=\"titledetails\">In: " + Eval("ContainerTitle").ToString()+"</div>" %>
            <%# Eval("Volume") == string.Empty ? "" : "<div class=\"titledetails\">Volume: " + Eval("Volume") + "</div>"%>
            <%# Eval("Series") == string.Empty ? "" : "<div class=\"titledetails\">Series: " + Eval("Series") + "</div>"%>
            <%# Eval("Issue") == string.Empty ? "" : "<div class=\"titledetails\">Issue: " + Eval("Issue") + "</div>"%>
            <%# Eval("Date") == string.Empty ? "" : "<div class=\"titledetails\">Date: " + Eval("Date") + "</div>"%>
            <%# Eval("PageRange") == string.Empty ? "" : "<div class=\"titledetails\">Page Range: " + Eval("PageRange") + "</div>"%>
            <%# Eval("PublicationDetails") == string.Empty ? "" : "<div class=\"titledetails\">Publication info: " + Eval("PublicationDetails") + "</div>"%>
            <%# Eval("Keywords") == string.Empty ? "" : "<div class=\"titledetails\">Subjects: " + Eval("Keywords").ToString().Replace("|", "&nbsp;&nbsp;") + "</div>"%>
            <a class="titleviewbook" href="/part/<%# Eval("SegmentID")%>">View Metadata</a>
            <%# (!string.IsNullOrWhiteSpace(Eval("URL").ToString()) && Eval("StartPageID") != null) ? "<a target=\"_blank\" class=\"titleviewbook\" href=\"" + Eval("URL")+ "\">View Alternate [External]</a>" : "" %>
            <%# Eval("DownloadURL") == string.Empty ? "":"<a class=\"titleviewbook\" href=\"" + Eval("DownloadURL")+ "\">Download</a>" %>
        </li>
	</ItemTemplate>
	<HeaderTemplate>
		<ol class="data titles">
	</HeaderTemplate>
	<FooterTemplate>
		</ol>
    </FooterTemplate>
</asp:Repeater>
