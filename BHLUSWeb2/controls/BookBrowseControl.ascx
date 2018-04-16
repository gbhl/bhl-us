<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookBrowseControl.ascx.cs" Inherits="MOBOT.BHL.Web2.BookBrowseControl" %>
<asp:Repeater ID="bookRepeater" runat="server">
	<ItemTemplate>
        <li class="titlelisting">
            <div class="title"><a target="<%# Eval("ExternalUrl") == string.Empty ? "_self" : "_blank\" class=\"ExtLinkBrowse" %>" href="/item/<%# Eval("ItemID ")%>"><%# Eval("FullTitle")%> <%# Eval("PartNumber")%> <%# Eval("PartName")%></a></div>
            <%# Eval("Authors") == string.Empty ? "" : "<div class=\"titledetails\">By: " + Eval("Authors").ToString().Replace("|", " - ")+"</div>" %>
            <%# Eval("EditionStatement") == string.Empty ? "" : "<div class=\"titledetails\">Edition: " + Eval("EditionStatement") + "</div>"%>
            <%# Eval("PublicationDetails") == string.Empty ? "" : "<div class=\"titledetails\">Publication info: " + Eval("PublicationDetails") + "</div>"%>
            <%# Eval("Volume") != string.Empty && ShowVolume ? "<div class=\"titledetails\">Volume: " + Eval("Volume") + "</div>" : ""%>
            <%# Eval("Associations") == string.Empty ? "" : "<div class=\"titledetails\">Series: " + Eval("Associations").ToString().Replace("|", "<br />Series: ") + "</div>"%>
            <%# Eval("InstitutionName") == string.Empty ? "" : "<div class=\"titledetails\">Holding Institution: " + Eval("InstitutionName") + "</div>"%>
            <%# Eval("Subjects") == string.Empty ? "" : "<div class=\"titledetails\">Subjects: " + Eval("Subjects").ToString().Replace("|", "&nbsp;&nbsp;") + "</div>"%>
            <%# Eval("Collections") == string.Empty ? "" : "<div class=\"titledetails\">BHL Collections: " + Eval("Collections") + "</div>"%>
            <a class="titleviewbook" href="/bibliography/<%# Eval("TitleID") %>">View Metadata</a>
        </li>
	</ItemTemplate>
	<HeaderTemplate>
		<ol  class="data titles">
	</HeaderTemplate>
	<FooterTemplate>
		</ol>
	</FooterTemplate>
</asp:Repeater>
