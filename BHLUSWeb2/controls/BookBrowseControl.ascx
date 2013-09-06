<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookBrowseControl.ascx.cs" Inherits="MOBOT.BHL.Web2.BookBrowseControl" %>
<asp:Repeater ID="bookRepeater" runat="server">
	<ItemTemplate>
                   <li class="titlelisting">
                <a class="title" href="/bibliography/<%# Eval("TitleID") %>"><%# Eval("FullTitle")%> <%# Eval("PartNumber")%> <%# Eval("PartName")%>
				</a>
            <%# Eval("Authors") == string.Empty ? "" : "<div class=\"titledetails\">By: " + Eval("Authors").ToString().Replace("|", " - ")+"</div>" %>
            <%# Eval("EditionStatement") == string.Empty ? "" : "<div class=\"titledetails\">Edition: " + Eval("EditionStatement") + "</div>"%>
            <%# Eval("PublicationDetails") == string.Empty ? "" : "<div class=\"titledetails\">Publication info: " + Eval("PublicationDetails") + "</div>"%>
            <%# Eval("Volume") != string.Empty && ShowVolume ? "<div class=\"titledetails\">Volume: " + Eval("Volume") + "</div>" : ""%>
            <%# Eval("Associations") == string.Empty ? "" : "<div class=\"titledetails\">Series: " + Eval("Associations").ToString().Replace("|", "<br />Series: ") + "</div>"%>
            <%# Eval("InstitutionName") == string.Empty ? "" : "<div class=\"titledetails\">Contributed by: " + Eval("InstitutionName") + "</div>"%>
            <%# Eval("Subjects") == string.Empty ? "" : "<div class=\"titledetails\">Subjects: " + Eval("Subjects").ToString().Replace("|", "&nbsp;&nbsp;") + "</div>"%>
            <%# Eval("Collections") == string.Empty ? "" : "<div class=\"titledetails\">BHL Collections: " + Eval("Collections") + "</div>"%>
             <a class="titleviewbook" href="/item/<%# Eval("ItemID ")%>">View Book<%# Eval("ExternalUrl") == string.Empty ? "" : " (External)" %></a>
            </li>
	</ItemTemplate>
	<HeaderTemplate>
		<ol  class="data titles">
	</HeaderTemplate>
	<FooterTemplate>
		</ol>
	</FooterTemplate>
</asp:Repeater>
