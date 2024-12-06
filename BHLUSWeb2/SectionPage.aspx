<%@ Page Title="" Language="C#" ViewStateMode="Disabled" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SectionPage.aspx.cs" Inherits="MOBOT.BHL.Web2.SectionPage" %>
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="COinS" Src="~/controls/COinSControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<link rel="stylesheet" href="/css/bhl-citation-js.css?v=0" />
<uc:NavBar runat="server" />
<div id="page-title">
    <div class="column-wrap">
        <div class="ellipsis bibtitle"><%: BhlSegment.GenreName%>: <%: BhlSegment.Title %></div>
        <div class="bibtitleicons">
            <a href="/contact/" title="Report an error" class="report"><img alt="Report an error" src="/images/rpterror.png" /></a>
        </div>
    </div>
</div>
<div id="content" class="column-wrap clearfix" itemscope itemtype="<%: SchemaType %>">
<section>

    <div class="tabs js-hide">
        <ul class="tab-nav no-js-hide">
            <li class="details first-child" style="display:none"><a href="#/details">Details</a></li>
            <li class="mods" style="display:none"><a href="#/mods">MODS</a></li>
            <li class="bibtex" style="display:none"><a href="#/bibtex">BibTeX</a></li>
            <li class="ris last-child" style="display:none"><a href="#/ris">RIS</a></li>
        </ul>            
        <uc:COinS ID="COinS" runat="server" />
        <div id="details" class="tab-body">

            <div class="segmentdetails"><h3>Title</h3> <p><%: BhlSegment.Title %></p></div>
            <% if (!String.IsNullOrEmpty(BhlSegment.TranslatedTitle)) { %>
                <div class="segmentdetails"><h3>Translated Title</h3> <p><%: BhlSegment.TranslatedTitle%></p></div>
            <% } %>
            <%if (BhlSegment.SegmentExternalResources.Count > 0) { %>
                <h3>External Resources</h3>
                <p>
                    <%foreach (SegmentExternalResource resource in BhlSegment.SegmentExternalResources)
                    {%>
                        <%:resource.ExternalResourceTypeLabel%>:
                        <%if (string.IsNullOrWhiteSpace(resource.Url)) { %>
                            <%:resource.UrlText %>
                        <%} else { %>
                            <a class="ExtLinkBrowse" href="<%:resource.Url%>" rel="noopener noreferrer" target="_blank"><%:resource.UrlText%></a>
                        <%}%>
                        <br />
                    <% } %>
                </p>
            <% } %>
            <% if (BhlSegment.AuthorList.Count > 0) { %>
                <h3>By</h3>
                <p>
                    <% foreach (ItemAuthor author in BhlSegment.AuthorList)
                       { %>
                        <a href="/creator/<%: author.AuthorID %>">
							<%: author.NameExtended%>
						</a>
                        <br />
                    <% } %>
                </p>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.GenreName)) { %>
            <div class="segmentdetails"><h3>Type</h3> <p><%: BhlSegment.GenreName %></p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.Date)) { %>
            <div class="segmentdetails"><h3>Date of Publication</h3> <p><%: BhlSegment.Date%></p></div>
            <% } %>
            <% if (!String.IsNullOrEmpty(BhlSegment.ContainerTitle)) { %>
            <div class="segmentdetails"><h3>Original Publication</h3> <p><%: BhlSegment.ContainerTitleExtended %></p></div>
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
                    <a href="/subject/<%: Server.UrlEncode(BhlSegment.KeywordList[i].Keyword) %>">
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
                            <a target="_blank" rel="noopener noreferrer" class="ExtLinkBrowse" style="line-height:19px" href="<%: institution.InstitutionUrl %>" title="Contributor"><%: institution.InstitutionName %></a>
                        <%}
                        else
                        {%>
                            <%: institution.InstitutionName%><br />
                        <%}
                    }%>
                    </p>
                </div>
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
                    <%foreach (ItemIdentifier itemIdentifier in BhlSegment.IdentifierList) {%>
                        <%: itemIdentifier.IdentifierLabel %>: 
                        <%if (itemIdentifier.IdentifierValueDisplay.StartsWith("http", true, System.Globalization.CultureInfo.CurrentCulture)) 
                        {%>
                            <a target="blank" class="ExtLinkBrowse" style="line-height:19px" href="<%: itemIdentifier.IdentifierValueDisplay%>"><span itemprop="<%: itemIdentifier.IdentifierLabel%>"><%: itemIdentifier.IdentifierValueDisplay %></span></a><br />
                        <%}
                        else
                        {%>
                            <span itemprop="<%: itemIdentifier.IdentifierLabel%>"><%: itemIdentifier.IdentifierValueDisplay %></span><br />
                        <%}
                    }%>
                </p>
            <% } %>
            <p>&nbsp;</p>
        </div>
    </div>
</section>
<aside>
    <div id="divCitationModal">
        <a id="btnCite" class="btnCite" onclick="showCitationModal(cmArgs);">Cite This Publication</a>
	    <div id="citeModal" class="citeModal"></div>
    </div>
    <h3></h3>
    <div class="partlinks">
        <div class="partlink js-hide">
            <h4 class="title">
                    <!--<a class="expand no-js-hide" title="expand or collapse volume description">expand</a>-->
                    <span class="text"><%: BhlSegment.GenreName%> access</span>
            </h4>
            <div class="body" style="border-bottom: 1px solid #C5CED3; padding: 20px 0 0;">
                <div class="partlinks">

                    <%if ((BhlSegment.StartPageID != null) && BhlSegment.StartPageID > 0) { %>
                        <a href="/page/<%: BhlSegment.StartPageID %>">View <%: BhlSegment.GenreName%></a> <br />
                    <%}%>
                    <% if (! string.IsNullOrEmpty(BhlSegment.Url))
                    { %>
                        <a target="_blank" rel="noopener noreferrer" href="<%: BhlSegment.Url %>">View <%: BhlSegment.GenreName%> (External Location)</a> <br />
                    <% } %>

                    <% if (! string.IsNullOrEmpty(BhlSegment.DownloadUrl))
                    { %>
                        <a href="<%: BhlSegment.DownloadUrl %>">Download  <%: BhlSegment.GenreName%></a> <br />
                    <% } %>
                </div>
                <div class="volume"">
                    <div class="copyright">
                        <h5>Copyright &amp; Usage:</h5>
                        <p>
                            <% if (!String.IsNullOrEmpty(BhlSegment.LicenseName)) { %>
                            License Type Name: <%: BhlSegment.LicenseName%><br />
                            <% } %>
                            <% if (!String.IsNullOrEmpty(BhlSegment.LicenseUrl)) { %>
                            License Type URL:
                                <%if (System.Text.RegularExpressions.Regex.IsMatch(BhlSegment.LicenseUrl, "^(https?|ftp|file)://.+$")) {%>
                                    <a target="_blank" rel="noopener noreferrer" href="<%: BhlSegment.LicenseUrl%>"><%: BhlSegment.LicenseUrl%></a>
                                <% } else {%>
                                    <%: BhlSegment.LicenseUrl%>
                                <% } %>
                            <br />
                            <% } %>
                            <% if (!String.IsNullOrEmpty(BhlSegment.RightsStatement)) { %>
                            Rights: 
                                <%if (System.Text.RegularExpressions.Regex.IsMatch(BhlSegment.RightsStatement, "^(https?|ftp|file)://.+$")) {%>
                                    <a target="_blank" rel="noopener noreferrer" href="<%: BhlSegment.RightsStatement%>"><%: BhlSegment.RightsStatement%></a>
                                <% } else {%>
                                    <%: BhlSegment.RightsStatement%>
                                <% } %>
                            <br />
                            <% } %>
                            <% if (!String.IsNullOrEmpty(BhlSegment.RightsStatus)) { %>
                            Copyright Status: <%: BhlSegment.RightsStatus%><br/> 
                            <% } %>
                            <% if (RightsHolder != null) { %>
                                Rights Holder:
                                <%if (string.IsNullOrWhiteSpace(RightsHolder.InstitutionUrl)) { %>
                                    <%: RightsHolder.InstitutionName%>
                                <% }
                                else
                                { %>
                                    <a rel="noopener noreferrer" target='_blank' href="<%: RightsHolder.InstitutionUrl %>"><%: RightsHolder.InstitutionName%></a>
                                <% } %>
                            <br />
                            <% } %>
                        </p>
                    </div>
                </div>
            </div>
            <div class="download">
                <% if (IsVirtual == 1 || (HasLocalContent == 1 && System.Configuration.ConfigurationManager.AppSettings["UsePregeneratedPDFs"] == "true")) { %>
                    <div class="downloadlabel">Download:</div>
                    <% if (IsVirtual == 1) { %>
                        <a class="icon all" title="download all" href="https://www.archive.org/download/<%: BhlSegment.BarCode %>">All</a>
                        <a class="icon jp2" title="download jp2" href="/partimages/<%: BhlSegment.SegmentID %>">JP2</a>
                        <a class="icon ocr" title="download ocr" download="<%: BhlSegment.SegmentID %>.txt" href="/parttext/<%: BhlSegment.SegmentID %>">OCR</a>
                    <% } %>
                    <a class="icon pdf" title="download pdf" download="<%: BhlSegment.SegmentID %>.pdf" href="/partpdf/<%: BhlSegment.SegmentID %>">PDF</a>
                <%} %>
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
                            <% if (!string.IsNullOrWhiteSpace(segment.ContainerTitle)) { 
                                %> - <%: segment.ContainerTitle%> 
                                <% if (!string.IsNullOrWhiteSpace(segment.Volume)) { %> (<%: segment.Volume %>)<% } %>
                            <% } %> 
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
<script src="/js/citation-js/citation-js@0.6.4.js" type="text/javascript"></script>
<script src="/js/citation-js/bhl-citation-js.js?v=2" type="text/javascript"></script>
<script type="text/javascript">
    var cmArgs = new CitationModalArgs();
    cmArgs.init({
        sid: <%: BhlSegment.SegmentID %>
    });
</script>
<script type="text/javascript">
//<![CDATA[
    $(document).ready(function () {

        // Update the Altmetric badge
        $(".altmetric-embed").attr("data-uri", "https://www.biodiversitylibrary.org/part/" + "<%: BhlSegment.SegmentID %>");
        if (typeof _altmetric_embed_init === 'function') _altmetric_embed_init();
/*
        var tabBodys = $('.tab-body').hide();

        // Navigate to the default sub-section if no hash
        if (!location.hash) {
            $.History.go('/details');
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
                $.History.go('/details');
                return false;
            } else {
                tabBody.show();
            }
        });
*/
    });
//]]>
</script>
</asp:Content>
