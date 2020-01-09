<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookBrowseControl.ascx.cs" Inherits="MOBOT.BHL.Web2.BookBrowseControl" %>
<asp:Repeater ID="bookRepeater" runat="server">
	<ItemTemplate>
        <%
            bool useOriginal = false;
            if (Request.Cookies.AllKeys.Contains("originalsearch"))
            {
                if (Request.Cookies["originalsearch"].Value == "1") useOriginal = true;
            }

            if (System.Configuration.ConfigurationManager.AppSettings["UseElasticSearch"].ToLower() == "true" && !useOriginal)
            {%>
            <li class="titlelisting">
                <div style="display:inline-block; width:620px">
                    <div style="float:left">
                        <a target="<%# Eval("ExternalUrl") == string.Empty ? "_self\" class=\"title" : "_blank\" rel=\"noopener noreferrer\" class=\"title ExtLinkBrowse" %>" href="/item/<%# Eval("ItemID ")%>"><%# Eval("FullTitle")%> <%# Eval("PartNumber")%> <%# Eval("PartName")%></a>
                    </div>
                    <div style="float:right">
                        <a class="titleviewbook" href="/bibliography/<%# Eval("TitleID") %>">View Metadata</a>
                    </div>
                </div>
                <%# Eval("Authors") == string.Empty ? "" : "<div class=\"titledetails\">By: " + Eval("Authors").ToString().Replace("|", " - ")+"</div>" %>
                <%# Eval("EditionStatement") == string.Empty ? "" : "<div class=\"titledetails\">Edition: " + Eval("EditionStatement") + "</div>"%>
                <%# Eval("PublicationDetails") == string.Empty ? "" : "<div class=\"titledetails\">Publication info: " + Eval("PublicationDetails") + "</div>"%>
                <%# Eval("Volume") != string.Empty && ShowVolume ? "<div class=\"titledetails\">Volume: " + Eval("Volume") + "</div>" : ""%>
                <%# Eval("Associations") == string.Empty ? "" : "<div class=\"titledetails\">Series: " + Eval("Associations").ToString().Replace("|", "<br />Series: ") + "</div>"%>
                <%# Eval("InstitutionName") == string.Empty ? "" : "<div class=\"titledetails\">Holding Institution: " + Eval("InstitutionName") + "</div>"%>
                <%# Eval("Subjects") == string.Empty ? "" : "<div class=\"titledetails\">Subjects: " + Eval("Subjects").ToString().Replace("|", "&nbsp;&nbsp;") + "</div>"%>
                <%# Eval("Collections") == string.Empty ? "" : "<div class=\"titledetails\">BHL Collections: " + Eval("Collections") + "</div>"%>
            </li>
        <%}
        else
        {%>
            <li class="titlelisting">
                <a class="title" style="width:540px" href="/bibliography/<%# Eval("TitleID") %>"><%# Eval("FullTitle")%> <%# Eval("PartNumber")%> <%# Eval("PartName")%></a>
                <%# Eval("Authors") == string.Empty ? "" : "<div class=\"titledetails\">By: " + Eval("Authors").ToString().Replace("|", " - ")+"</div>" %>
                <%# Eval("EditionStatement") == string.Empty ? "" : "<div class=\"titledetails\">Edition: " + Eval("EditionStatement") + "</div>"%>
                <%# Eval("PublicationDetails") == string.Empty ? "" : "<div class=\"titledetails\">Publication info: " + Eval("PublicationDetails") + "</div>"%>
                <%# Eval("Volume") != string.Empty && ShowVolume ? "<div class=\"titledetails\">Volume: " + Eval("Volume") + "</div>" : ""%>
                <%# Eval("Associations") == string.Empty ? "" : "<div class=\"titledetails\">Series: " + Eval("Associations").ToString().Replace("|", "<br />Series: ") + "</div>"%>
                <%# Eval("InstitutionName") == string.Empty ? "" : "<div class=\"titledetails\">Holding Institution: " + Eval("InstitutionName") + "</div>"%>
                <%# Eval("Subjects") == string.Empty ? "" : "<div class=\"titledetails\">Subjects: " + Eval("Subjects").ToString().Replace("|", "&nbsp;&nbsp;") + "</div>"%>
                <%# Eval("Collections") == string.Empty ? "" : "<div class=\"titledetails\">BHL Collections: " + Eval("Collections") + "</div>"%>
                 <a class="titleviewbook" style="position:relative;top:-25px;" target="<%# Eval("ExternalUrl") == string.Empty ? "_self" : "_blank" %>" rel="noopener noreferrer" href="/item/<%# Eval("ItemID ")%>">View Book<%# Eval("ExternalUrl") == string.Empty ? "" : " (External)" %></a>
            </li>
        <%} %>
	</ItemTemplate>
	<HeaderTemplate>
		<ol  class="data titles">
	</HeaderTemplate>
	<FooterTemplate>
		</ol>
	</FooterTemplate>
</asp:Repeater>
