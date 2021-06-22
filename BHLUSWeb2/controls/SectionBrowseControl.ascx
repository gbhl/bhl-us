<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SectionBrowseControl.ascx.cs" Inherits="MOBOT.BHL.Web2.SectionBrowseControl" %>
<asp:Repeater ID="sectionRepeater" runat="server">
    <ItemTemplate>
        <%bool useOriginal = false;
        if (Request.Cookies.AllKeys.Contains("originalsearch"))
        {
            if (Request.Cookies["originalsearch"].Value == "1") useOriginal = true;
        }

        if (System.Configuration.ConfigurationManager.AppSettings["UseElasticSearch"].ToLower() == "true" && !useOriginal)
        {%>
            <li class="titlelisting">
                <div style="display:inline-block; width:620px">
                    <div style="float:left">
                        <%# (Eval("StartPageID") == null && Eval("URL").ToString() == string.Empty && ((byte?)Eval("BookIsVirtual") ?? 0) == 0) ?  Eval("Title") : "" %>
                        <%# (Eval("StartPageID") != null && Eval("URL").ToString() == string.Empty && ((byte?)Eval("BookIsVirtual") ?? 0) == 0) ? "<a class=\"title\" href=\"/page/" + Eval("StartPageID").ToString() + "\" \\>" + Eval("Title").ToString() + "</a>" : "" %>
                        <%# (Eval("StartPageID") == null && Eval("URL").ToString() != string.Empty && ((byte?)Eval("BookIsVirtual") ?? 0) == 0) ? "<a target=\"_blank\" rel=\"noopener noreferrer\" class=\"title ExtLinkBrowse\" href=\"" + Eval("URL").ToString() + "\">" + Eval("Title").ToString() + "</a>" : "" %>
                        <%# (((byte?)Eval("BookIsVirtual") ?? 0) == 1) ? "<a class=\"title\" href=\"/segment/" + Eval("SegmentID").ToString() + "\" \\>" + Eval("Title").ToString() + "</a>" : "" %>
                    </div>
                    <div style="float:right">
                        <a class="titleviewbook" href="/part/<%# Eval("SegmentID")%>">View Metadata</a>
                    </div>
                </div>
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
            </li>
        <%}
        else
        {%>
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
                <%# Eval("Keywords") == string.Empty ? "" : "<div class=\"titledetails\">Subjects: " + Eval("Keywords").ToString().Replace("|", "&nbsp;&nbsp;") + "</div>"%>
                <%# (Eval("StartPageID") != null && ((byte?)Eval("BookIsVirtual") ?? 0) == 0) ? "<a class=\"titleviewbook\" style=\"position:relative;top:-25px;\" href=\"/page/" + Eval("StartPageID")+ "\">View "+  Eval("GenreName")+ "</a> " : ""%>
                <%# (((byte?)Eval("BookIsVirtual") ?? 0) == 1) ? "<a class=\"titleviewbook\" style=\"position:relative;top:-25px;\" href=\"/segment/" + Eval("SegmentID")+ "\">View "+  Eval("GenreName")+ "</a> " : ""%>
                <%# Eval("URL") == string.Empty ? "":"<a target=\"_blank\" rel=\"noopener noreferrer\" class=\"titleviewbook\" style=\"position:relative;top:-25px;\" href=\"" + Eval("URL")+ "\">View "+  Eval("GenreName")+ " (External)</a>" %>
                <%# Eval("DownloadURL") == string.Empty ? "":"<a class=\"titleviewbook\" style=\"position:relative;top:-25px;\" href=\"" + Eval("DownloadURL")+ "\">Download "+  Eval("GenreName")+ "</a>" %>
            </li>
        <%} %>
	</ItemTemplate>
	<HeaderTemplate>
		<ol class="data titles">
	</HeaderTemplate>
	<FooterTemplate>
		</ol>
    </FooterTemplate>
</asp:Repeater>
