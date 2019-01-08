<%@ Page Title="" Language="C#" ViewStateMode="Disabled" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SectionPage.aspx.cs" Inherits="MOBOT.BHL.Web2.SectionPage" %>
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="COinS" Src="~/controls/COinSControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="Mendeley" Src="~/controls/MendeleyShareControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="page-title">
    <div class="column-wrap">
        <div class="ellipsis bibtitle"><%: BhlSegment.GenreName%>: <%: BhlSegment.Title %></div>
        <div class="bibtitleicons">
            <uc:Mendeley id="mendeley" runat="server" />
            <a href="/contact/" title="Report an error" class="report"><img alt="Report an error" src="/images/rpterror.png" /></a>
        </div>
    </div>
</div>
<div id="content" class="column-wrap clearfix" itemscope itemtype="<%: SchemaType %>">
<section>

    <div class="tabs js-hide">
        <ul class="tab-nav no-js-hide">
            <li class="summary first-child"><a href="#/summary">Summary</a></li>
            <li class="details"><a href="#/details">Details</a></li>
            <li class="mods"><a href="#/mods">MODS</a></li>
            <li class="bibtex"><a href="#/bibtex">BibTeX</a></li>
            <li class="ris last-child"><a href="#/ris">RIS</a></li>
        </ul>            
        <uc:COinS ID="COinS" runat="server" />
        <div id="summary" class="tab-body">

            <div class="segmentdetails"><h3>Title</h3> 
            <span itemprop="url" style="display:none"><%: String.Format(ConfigurationManager.AppSettings["PartPageUrl"], BhlSegment.SegmentID.ToString()) %></span>
            <p><span itemprop="name"><%: BhlSegment.Title %></p></span></div>
            <% if (!String.IsNullOrEmpty(BhlSegment.TranslatedTitle)) { %>
                <div class="segmentdetails"><h3>Translated Title</h3> <p><%: BhlSegment.TranslatedTitle%></p></div>
            <% } %>
            <% if (BhlSegment.AuthorList.Count > 0) { %>
                <h3>By</h3>
                <p>
                    <% foreach (SegmentAuthor author in BhlSegment.AuthorList)
                    { %>
                        <span itemprop="author" itemscope itemtype='https://schema.org/Person'>
                        <a href="/creator/<%: author.AuthorID %>">
							<span itemprop="name"><%: author.NameExtended%></span>
						</a>
                        <span itemprop='url' style='display:none'><%: string.Format(ConfigurationManager.AppSettings["AuthorPageUrl"], author.AuthorID.ToString()) %></span>
                        </span>
                        <br />
                    <% } %>
                </p>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.GenreName)) { %>
            <div class="segmentdetails"><h3>Genre</h3> <p><span itemprop="genre"><%: BhlSegment.GenreName %></span></p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.Date)) { %>
            <div class="segmentdetails"><h3>Date of Publication</h3> <p><span itemprop="datePublished"><%: BhlSegment.Date%></span></p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.ContainerTitle)) { %>
            <div class="segmentdetails"><h3>Original Publication</h3> <p><%: BhlSegment.ContainerTitle %></p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.Volume)) { %>
            <div class="segmentdetails"><h3>Volume</h3> <p><%: BhlSegment.Volume%></p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.Series) || !String.IsNullOrWhiteSpace(BhlSegment.Issue)) { %>
                <div class="segmentdetails"><h3>Series / Issue</h3> 
                    <p><% if (!String.IsNullOrWhiteSpace(BhlSegment.Series)) { %>Series: <%: BhlSegment.Series%><br /> <% } %>
                    <% if (!String.IsNullOrWhiteSpace(BhlSegment.Issue)) { %>Issue: <%: BhlSegment.Issue%> <% } %>
                    </p>
                </div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.PageRange)) { %>
            <div class="segmentdetails"><h3>Pages</h3> <p><%: BhlSegment.PageRange%></p></div>
            <% } %>
            <% if (BhlSegment.KeywordList.Count > 0) { %>
                <h3>Subjects</h3>
                <p><span itemprop="keywords">
                <% for (int i = 0; i < BhlSegment.KeywordList.Count; i++)
                    { %>
                    <a href="/subject/<%: Server.UrlPathEncode(BhlSegment.KeywordList[i].Keyword) %>">
                        <%: BhlSegment.KeywordList[i].Keyword%>
                    </a>
                    <%: (i < BhlSegment.KeywordList.Count - 1) ? ", " : string.Empty%>
                <% } %>
                </span></p>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.ContributorName)) { %>
                <div class="segmentdetails"><h3>Contributed by</h3> <p>
                    <%foreach (Institution institution in BhlSegment.ContributorList) {
                        if (!string.IsNullOrWhiteSpace(institution.InstitutionUrl)) {%>
                            <a target="_blank" class="ExtLinkBrowse" style="line-height:19px" href="<%: institution.InstitutionUrl %>" title="Contributor"><%: institution.InstitutionName %></a>
                        <%}
                        else
                        {%>
                            <%: institution.InstitutionName%><br />
                        <%}
                    }%>
                    </p>
                </div>
            <% } %>
            <% if (DOI != string.Empty) { %>
            <h3>DOI</h3>
            <p>
                <a href="<%= DOI%>" title="DOI"><span itemprop="DOI"><%= DOI%></span></a>
            </p>
            <% } %>

        </div>
        <div id="details" class="tab-body">

            <div class="segmentdetails"><h3>Title</h3> <p><%: BhlSegment.Title %></p></div>
            <% if (!String.IsNullOrEmpty(BhlSegment.TranslatedTitle)) { %>
                <div class="segmentdetails"><h3>Translated Title</h3> <p><%: BhlSegment.TranslatedTitle%></p></div>
            <% } %>
            <% if (BhlSegment.RelatedSegmentList.Count > 0) { %>
                <h3>Related To</h3>
                <p>
                    <%foreach (Segment segment in BhlSegment.RelatedSegmentList)
                    { %>
                        <%: segment.SegmentClusterTypeLabel %> :
                        <a href="/part/<%: segment.SegmentID %>">
                            <%: segment.Title %>
                        </a>
                    <% if (!string.IsNullOrWhiteSpace(segment.Authors)) { %> - <%: segment.Authors %> <% } %> 
                    <% if (!string.IsNullOrWhiteSpace(segment.ContainerTitle)) { %> - <%: segment.ContainerTitle%> <% } %> 
                    <% if (!string.IsNullOrWhiteSpace(segment.Date)) { %> - <%: segment.Date%> <% } %> 
                    <% if (!string.IsNullOrWhiteSpace(segment.PageRange)) { %> - p.<%: segment.PageRange%> <br /> <% } %> 
                    <% } %>
                </p>
            <% } %>
            <% if (BhlSegment.AuthorList.Count > 0) { %>
                <h3>By</h3>
                <p>
                    <% foreach (SegmentAuthor author in BhlSegment.AuthorList)
                       { %>
                        <a href="/creator/<%: author.AuthorID %>">
							<%: author.NameExtended%>
						</a>
                        <br />
                    <% } %>
                </p>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.GenreName)) { %>
            <div class="segmentdetails"><h3>Genre</h3> <p><%: BhlSegment.GenreName %></p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.Date)) { %>
            <div class="segmentdetails"><h3>Date of Publication</h3> <p><%: BhlSegment.Date%></p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.ContainerTitle)) { %>
            <div class="segmentdetails"><h3>Original Publication</h3> <p><%: BhlSegment.ContainerTitle %></p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.Volume)) { %>
            <div class="segmentdetails"><h3>Volume</h3> <p><%: BhlSegment.Volume%></p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.Series) || !String.IsNullOrWhiteSpace(BhlSegment.Issue)) { %>
                <div class="segmentdetails"><h3>Series / Issue</h3> 
                    <p><% if (!String.IsNullOrWhiteSpace(BhlSegment.Series)) { %>Series: <%: BhlSegment.Series%><br /> <% } %>
                    <% if (!String.IsNullOrWhiteSpace(BhlSegment.Issue)) { %>Issue: <%: BhlSegment.Issue%> <% } %>
                    </p>
                </div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.PageRange)) { %>
            <div class="segmentdetails"><h3>Pages</h3> <p><%: BhlSegment.PageRange%></p></div>
            <% } %>
            <% if (BhlSegment.KeywordList.Count > 0) { %>
                <h3>Subjects</h3>
                <p>
                <% for (int i = 0; i < BhlSegment.KeywordList.Count; i++)
                    { %>
                    <a href="/subject/<%: Server.UrlPathEncode(BhlSegment.KeywordList[i].Keyword) %>">
                        <%: BhlSegment.KeywordList[i].Keyword%>
                    </a>
                    <%: (i < BhlSegment.KeywordList.Count - 1) ? ", " : string.Empty%>
                <% } %>
                </p>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.ContributorName)) { %>
                <div class="segmentdetails"><h3>Contributed by</h3> <p>
                    <%foreach (Institution institution in BhlSegment.ContributorList) {
                        if (!string.IsNullOrWhiteSpace(institution.InstitutionUrl)) {%>
                            <a target="_blank" class="ExtLinkBrowse" style="line-height:19px" href="<%: institution.InstitutionUrl %>" title="Contributor"><%: institution.InstitutionName %></a>
                        <%}
                        else
                        {%>
                            <%: institution.InstitutionName%><br />
                        <%}
                    }%>
                    </p>
                </div>
            <% } %>
            <% if (DOI != string.Empty) { %>
            <h3>DOI</h3>
            <p>
                <a href="<%= DOI%>" title="DOI"><span><%= DOI%></span></a>
            </p>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.Notes)) { %>
            <div class="segmentdetails"><h3>Notes</h3> <p><%: BhlSegment.Notes%></p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.LanguageName)) { %>
            <div class="segmentdetails"><h3>Language</h3> <p><span itemprop="inLanguage"><%: BhlSegment.LanguageName%></span></p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.PublicationDetails) ||
                   !String.IsNullOrEmpty(BhlSegment.PublisherName) ||
                   !String.IsNullOrEmpty(BhlSegment.PublisherPlace)) { %>
                <div class="segmentdetails"><h3>Published by</h3> 
                <p>
                    <span itemprop="publisher" itemscope itemtype="https://schema.org/Organization"><span itemprop="name">
                    <%if (!String.IsNullOrEmpty(BhlSegment.PublicationDetails)) { %>
                    <%: BhlSegment.PublicationDetails%>
                    <%} else { %>
                    <%: BhlSegment.PublisherPlace%> <%: BhlSegment.PublisherName %>
                    <%} %>
                    </span></span>
                </p>
                </div>
            <% } %>
            <% if (BhlSegment.IdentifierList.Count > 0) { %>
                <h3>Identifiers</h3><p>
                    <%foreach (SegmentIdentifier segmentIdentifier in BhlSegment.IdentifierList) {%>
                        <%: segmentIdentifier.IdentifierLabel %>: 
                        <%if (string.Compare(segmentIdentifier.IdentifierLabel, "BioStor", true) == 0) {%>
                            <a target="blank" class="ExtLinkBrowse" style="line-height:19px" href="https://biostor.org/reference/<%: segmentIdentifier.IdentifierValue%>"><span itemprop="<%: segmentIdentifier.IdentifierLabel%>"><%: segmentIdentifier.IdentifierValue %></span></a><br />
                        <%}
                        else
                        {%>
                            <span itemprop="<%: segmentIdentifier.IdentifierLabel%>"><%: segmentIdentifier.IdentifierValue %></span><br />
                        <%}
                    }%>
                </p>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.RightsStatus)) { %>
            <div class="segmentdetails"><h3>Rights Status</h3> <p><%: BhlSegment.RightsStatus%></p></div> 
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.RightsStatement)) { %>
            <div class="segmentdetails"><h3>Rights Statement</h3> <p>
                <%if (System.Text.RegularExpressions.Regex.IsMatch(BhlSegment.RightsStatement, "^(https?|ftp|file)://.+$")) {%>
                    <a target="_blank" href="<%: BhlSegment.RightsStatement%>"><%: BhlSegment.RightsStatement%></a>
                <% } else {%>
                    <%: BhlSegment.RightsStatement%>
                <% } %>
            </p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.LicenseName)) { %>
            <div class="segmentdetails"><h3>Rights License Name</h3><p><%: BhlSegment.LicenseName%></p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.LicenseUrl)) { %>
            <div class="segmentdetails"><h3>Rights License URL</h3><p>
                <%if (System.Text.RegularExpressions.Regex.IsMatch(BhlSegment.LicenseUrl, "^(https?|ftp|file)://.+$")) {%>
                    <a target="_blank" href="<%: BhlSegment.LicenseUrl%>"><%: BhlSegment.LicenseUrl%></a>
                <% } else {%>
                    <%: BhlSegment.LicenseUrl%>
                <% } %>
            </p></div>
            <% } %>

        </div>
        <div id="mods" class="tab-body">                                
            <p>
                <a class="button" href="/handlers/modsdownload.ashx?pid=<%: BhlSegment.SegmentID %>">Download MODS</a>
            </p>
            <p class="header">
                <asp:Literal ID="litMods" runat="server"></asp:Literal>
            </p>
        </div>
        <div id="bibtex" class="tab-body">
            <% if (!string.IsNullOrEmpty(litBibTeX.Text)) { %>
            <p>
                <a class="button" href="/handlers/bibtexdownload.ashx?pid=<%: BhlSegment.SegmentID %>">Download BibTeX citation</a>
            </p>                
            <p class="header">
                <asp:Literal ID="litBibTeX" runat="server"></asp:Literal>
            </p>
            <% } %>
        </div>
        <div id="ris" class="tab-body">
            <% if (!string.IsNullOrEmpty(litRIS.Text)) { %>
            <p>
                <a class="button" href="/handlers/risdownload.ashx?pid=<%: BhlSegment.SegmentID %>">Download RIS citation</a>
            </p>
            <p class="header">
                <asp:Literal ID="litRIS" runat="server"></asp:Literal>
            </p>
            <% } %>
        </div>

    </div>
</section>
<aside>
    <h3></h3>
    <div class="partlinks">
        <div class="partlink js-hide">
            <h4 class="title">
                    <!--<a class="expand no-js-hide" title="expand or collapse volume description">expand</a>-->
                    <span class="text"><%: BhlSegment.GenreName%> links</span>
            </h4>
            <div class="body" style="border-bottom: 1px solid #C5CED3; padding: 20px 0 0;">
                <div class="partlinks">

                    <% if ((BhlSegment.StartPageID != null) && BhlSegment.StartPageID > 0)
                    { %>
                        <a href="/page/<%: BhlSegment.StartPageID %>">View <%: BhlSegment.GenreName%></a> <br />
                    <% } %>

                    <% if (! string.IsNullOrEmpty(BhlSegment.Url))
                    { %>
                        <a target="_blank" href="<%: BhlSegment.Url %>">View <%: BhlSegment.GenreName%> (External Location)</a> <br />
                    <% } %>

                    <% if (! string.IsNullOrEmpty(BhlSegment.DownloadUrl))
                    { %>
                        <a href="<%: BhlSegment.DownloadUrl %>">Download  <%: BhlSegment.GenreName%></a> <br />
                    <% } %>
                </div>
            </div>
        </div>
    </div>

    <div class="partalsos" <%if (BhlSegment.RelatedSegmentList.Count == 0) { %>  style="display:none" <% } %>>
        <div class="partalso js-hide">
            <h4 class="title">
                    <a class="expand no-js-hide" title="expand or collapse volume description">expand</a>
                    <span class="text">See Also</span>
            </h4>
            <div class="body" style="border-bottom: 1px solid #C5CED3; padding: 20px 0 0;">
                <div class="partlinks">

                    <% if (BhlSegment.RelatedSegmentList.Count > 0) { %>
                        <p>
                            <%foreach (Segment segment in BhlSegment.RelatedSegmentList)
                            { %>
                                <%: segment.SegmentClusterTypeLabel %> :
                                <a href="/part/<%: segment.SegmentID %>">
                                    <%: segment.Title %>
                                </a>
                            <% if (!string.IsNullOrWhiteSpace(segment.Authors)) { %> - <%: segment.Authors %> <% } %> 
                            <% if (!string.IsNullOrWhiteSpace(segment.ContainerTitle)) { %> - <%: segment.ContainerTitle%> <% } %> 
                            <% if (!string.IsNullOrWhiteSpace(segment.Date)) { %> - <%: segment.Date%> <% } %> 
                            <% if (!string.IsNullOrWhiteSpace(segment.PageRange)) { %> - p.<%: segment.PageRange%> <% } %> 
                            <br /><br />
                            <% } %>
                        </p>
                    <% } %>

                </div>
            </div>
        </div>
    </div>

    <div class="partabstracts" <%if (string.IsNullOrWhiteSpace(BhlSegment.Summary)) { %> style="display:none" <% } %>>
        <div class="partabstract js-hide">
            <h4 class="title">
                <a class="expand no-js-hide" title="expand or collapse abstract">expand</a>
                <span class="text">Abstract</span>
            </h4>
            <div class="body" style="border-bottom: 1px solid #C5CED3; padding: 20px 0 0; display:none">
                <div class="partlinks">
                    <p>
                        <%=BhlSegment.Summary%>
                    </p>
                </div>
            </div>
        </div>
    </div>

    <div class="partnotes" <%if (string.IsNullOrWhiteSpace(BhlSegment.Notes)) { %> style="display:none" <% } %>>
        <div class="partnote js-hide">
            <h4 class="title">
                <a class="expand no-js-hide" title="expand or collapse abstract">expand</a>
                <span class="text">Notes</span>
            </h4>
            <div class="body" style="border-bottom: 1px solid #C5CED3; padding: 20px 0 0; display:none">
                <div class="partlinks">
                    <p>
                        <%=BhlSegment.Notes%>
                    </p>
                </div>
            </div>
        </div>
    </div>

</aside>
</asp:Content>
<asp:Content ID="scriptContent" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
<script src="/js/libs/jquery.history.min.js"></script>
<script type="text/javascript">
//<![CDATA[
    $(document).ready(function () {

        // Update the Altmetric badge
        $(".altmetric-embed").attr("data-uri", "https://www.biodiversitylibrary.org/part/" + "<%: BhlSegment.SegmentID %>");
        if (typeof _altmetric_embed_init === 'function') _altmetric_embed_init();

        
        var tabBodys = $('.tab-body').hide();

        // Navigate to the default sub-section if no hash
        if (!location.hash) {
            $.History.go('/summary');
        }

        $.History.bind(function (state) {
            if (!$('.tabs').is(':visible')) {
                $('.tabs').show();
            };
            var stateName = state.replace(/[^a-zA-Z0-9\s]/gi, '');
            var tabBody = $('#' + stateName);

            tabBodys.hide();

            // Highlight selected link
            $('.tab-nav li').removeClass('active');
            $('.tab-nav .' + stateName).addClass('active').blur();

            // If no default sub-section found then head on to the default otherwise show selected sub-section
            if (!tabBody.length) {
                $.History.go('/summary');
                return false;
            } else {
                tabBody.show();
            }
        });
    });
//]]>
</script>
</asp:Content>
