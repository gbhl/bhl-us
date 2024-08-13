<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bibliography.aspx.cs" Inherits="MOBOT.BHL.Web2.Bibliography" %>
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="COinS" Src="~/controls/COinSControl.ascx" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<link rel="stylesheet" href="/css/bhl-citation-js.css?v=0" />
<uc:NavBar runat="server" />
<div id="page-title" class="bibliography">
    <div class="column-wrap">
        <div class="ellipsis bibtitle"><%: BhlTitle.ShortTitle %></div>
        <div class="bibtitleicons">
            <a href="/contact/" title="Report an error" class="report"><img alt="Report an error" src="/images/rpterror.png" /></a>
        </div>
    </div>
</div>
<div id="content" class="column-wrap clearfix" itemscope itemtype="https://schema.org/Book">
    <section>
        <div class="tabs js-hide">
            <!--
            <ul class="tab-nav no-js-hide">
                <li class="details first-child" style="display:none"><a href="#/details">Details</a></li>
                <li class="mods" style="display:none"><a href="#/mods">MODS</a></li>
                <li class="bibtex" style="display:none"><a href="#/bibtex">BibTeX</a></li>
                <li class="ris last-child" style="display:none"><a href="#/ris">RIS</a></li>
            </ul>
            -->
            <uc:COinS ID="COinS" runat="server" />
            <div id="details" class="tab-body">
                <h3>Title</h3>
                <p><%: BhlTitle.FullTitleExtended %></p>
                <% if (TitleVariants.Count > 0 || !string.IsNullOrWhiteSpace(BhlTitle.UniformTitle)) { %>
                    <h3>Title Variants</h3>
                    <p>
                    <% foreach (TitleVariant tv in TitleVariants) { %>
                        <i><%: tv.TitleVariantLabel %>:</i>
                        <%
                        List<string> tvTitle = new List<string>();
                        if (!string.IsNullOrWhiteSpace(tv.Title)) tvTitle.Add(tv.Title);
                        if (!string.IsNullOrWhiteSpace(tv.TitleRemainder)) tvTitle.Add(tv.TitleRemainder);
                        %>
                        <%: string.Join(", ", tvTitle.ToArray()) %><br />
                    <% }
                    if (!string.IsNullOrWhiteSpace(BhlTitle.UniformTitle))
                    {%>
                          <i>Uniform: </i><%: BhlTitle.UniformTitle %><br />
                    <% } %>
                    </p>  
                <% } %>
                <% if(TitleAssociations.Count > 0) { %>                    
                    <h3>Related Titles</h3>
                    <p>
                    <% foreach (TitleAssociation ta in TitleAssociations) { %>
                        <i><%: ta.TitleAssociationLabel %>:</i>
                        <%
                        List<string> taTitle = new List<string>();
                        if (!string.IsNullOrEmpty(ta.Title)) taTitle.Add(ta.Title);
                        if (!string.IsNullOrEmpty(ta.Section)) taTitle.Add(ta.Section);
                        if (!string.IsNullOrEmpty(ta.Volume)) taTitle.Add(ta.Volume);
                        if (!string.IsNullOrEmpty(ta.Heading)) taTitle.Add(ta.Heading);
                        if (!string.IsNullOrEmpty(ta.Publication)) taTitle.Add(ta.Publication);
                        if (!string.IsNullOrEmpty(ta.Relationship)) taTitle.Add(ta.Relationship);
                        %>
					    <% if (ta.AssociatedTitleID != null) { %>
                            <a href="/bibliography/<%: ta.AssociatedTitleID %>">
                                <%: string.Join(", ", taTitle.ToArray()) %>
                            </a>
                        <% } else { %>
                            <a href='/search?searchTerm="<%:ta.Title %>"&stype=C'>
                                <%: string.Join(", ", taTitle.ToArray()) %>
                            </a>
                        <% } %>
                        <br />
                    <% } %>
                    </p>
                <% } %>
                <%if (TitleExternalResources.Count > 0) { %>
                    <h3>External Resources</h3>
                    <p>
                    <%foreach (TitleExternalResource resource in TitleExternalResources)
                    {%>
                        <%:resource.ExternalResourceTypeLabel%>:
                        <%if (string.IsNullOrWhiteSpace(resource.Url)) { %>
                            <%:resource.UrlText %>
                        <%} else { %>
                            <a class="ExtLinkBrowse" href="<%:resource.Url%>" rel="noopener noreferrer" target="_blank"><%:resource.UrlText%></a>
                        <%}%>
                        <br />
                    <%}%>
                    </p>
                <%}%>
                <h3>By</h3>
                <p>
                    <% foreach (Author author in AuthorsDetail) { %>
                        <a href="/creator/<%: author.AuthorID %>">
							<%: author.NameExtended %>
						</a><%if (!string.IsNullOrWhiteSpace(author.Relationship)) Response.Write(", " + author.Relationship); %><%if (!string.IsNullOrWhiteSpace(author.TitleOfWork)) Response.Write(", " + author.TitleOfWork); %>
                        <br />
                    <% } %>
                    <% if (AuthorsDetail.Count > 0 && AdditionalAuthorsDetail.Count > 0) Response.Write("<br />"); %>
                    <% foreach (Author author in AdditionalAuthorsDetail) { %>
                        <a href="/creator/<%: author.AuthorID %>">
							<%: author.NameExtended %>
						</a><%if (!string.IsNullOrWhiteSpace(author.Relationship)) Response.Write(", " + author.Relationship); %><%if (!string.IsNullOrWhiteSpace(author.TitleOfWork)) Response.Write(", " + author.TitleOfWork); %>
                        <br />
                    <% } %>
                </p>
                <h3>Type</h3>
                <p>
                    <%= Genre %>
                </p>
                <%if (!string.IsNullOrWhiteSpace(Material)) { %>
                <h3>Material</h3>
                <p>
                    <span><%= Material %></span>
                </p>
                <%} %>
                <h3>Publication info</h3>
                <p>
                    <%: BhlTitle.PublicationDetails %>
                </p>
                <%if (!String.IsNullOrWhiteSpace(BhlTitle.EditionStatement)) { %>
                    <h3>Edition</h3>
                    <p>
                        <span itemprop="bookEdition"><%: BhlTitle.EditionStatement%></span>
                    </p>
                <% } %>
                <% if (TitleNotes != null && TitleNotes.Count > 0) { %>
                    <h3>Notes</h3>
                    <% foreach (TitleNote titleNote in TitleNotes) { %>
                    <p>
                        <i><%: titleNote.NoteTypeDisplay %><%: (string.IsNullOrWhiteSpace(titleNote.NoteTypeDisplay) ? "" : ": ") %></i><%: titleNote.NoteText %>
                    </p>
                    <% } 
                } %>
                <% if (TitleKeywords != null && TitleKeywords.Count > 0)
                   { %>
                    <h3>Subjects</h3>
                    <p>
                    <% for (int i = 0; i < TitleKeywords.Count; i++)
                       { %>
                        <a href="/subject/<%: Server.UrlEncode(TitleKeywords[i].Keyword) %>">
                            <%: TitleKeywords[i].Keyword%>
                        </a>
                        <%: (i < TitleKeywords.Count - 1) ? ", " : string.Empty%>
                    <% } %>
                    </p>
                <% } %>
                <% if (Collections.Count > 0) { %>
                    <h3>BHL Collections</h3>
                    <p>
                    <% foreach (Collection collection in Collections) { %>
                        <a href="/browse/collection/<%: collection.CollectionID %>" title="Collection"><%: collection.CollectionName %></a><br />
                    <% } %>
                    </p>
                <% } %>
                <% if (!string.IsNullOrWhiteSpace(BhlTitle.CallNumber)) { %>
                    <h3>Call Number</h3>
                    <p>
                        <%: BhlTitle.CallNumber %>
                    </p>
                <% } %>
                <% if (!string.IsNullOrWhiteSpace(DDC)) { %>
                    <h3>Classification</h3>
                    <p>
                        <%: DDC %>
                    </p>
                <% } %>
                <% if (!string.IsNullOrWhiteSpace(LanguageName)) { %>
                    <h3>Language</h3>
                    <p>
                        <span itemprop="inLanguage"><%: LanguageName %></span>
                    </p>
                <% } %>
                <% if (TitleIdentifiers.Count > 0) { %>
                    <h3>Identifiers</h3>
                    <p>
                    <% foreach (Title_Identifier titleIdentifier in TitleIdentifiers) {%>
                            <%: titleIdentifier.IdentifierLabel %>: 
                            <%if (titleIdentifier.IdentifierValueDisplay.StartsWith("http", true, System.Globalization.CultureInfo.CurrentCulture))
                            {%>
                                <a href="<%= titleIdentifier.IdentifierValueDisplay%>" title="DOI"><span itemprop="<%: titleIdentifier.IdentifierLabel%>"><%: titleIdentifier.IdentifierValueDisplay %></span></a><br />
                            <%}
                            else
                            {%>
                                <span itemprop="<%: titleIdentifier.IdentifierLabel%>"><%: titleIdentifier.IdentifierValueDisplay %></span><br />
                            <%}
                        } %>
                    </p>
                <% } %>
                <p>&nbsp;</p>
                <p>
                    <a class="button" href="<%: LocalLibraryUrl %>" rel="noopener noreferrer" target="_blank">Find in a local library</a>
                </p>
            </div>
        </div>
    </section>
    <aside>
        <% if(BibliographyItems.Count > 0) { %>
            <div id="divCitationModal">
                <a id="btnCite" class="btnCite" onclick="showCitationModal(cmArgs);">Cite This Publication</a>
	            <div id="citeModal" class="citeModal"></div>
            </div>
            <h3 class="volume-heading">Volumes</h3>
            <div class="volumes">
            <% foreach(BibliographyItem bibliographyItem in BibliographyItems) { %>
            <div class="volume js-hide">
                <h4 class="title">
                    <a class="expand no-js-hide" title="expand or collapse volume description">expand</a>
                    <span class="text"><%: bibliographyItem.Book.Volume%></span>
                     <a class="viewbook" href="/item/<%: bibliographyItem.Book.BookID%>" title="View">view</a>
                </h4>
                <div class="body">
                    <% if(!string.IsNullOrWhiteSpace(bibliographyItem.ThumbUrl)) { %>
                    <a href="/item/<%: bibliographyItem.Book.BookID %>">
                        <img src="<%: bibliographyItem.ThumbUrl %>" width="100" />
                    </a>
                    <div class="summary">
                    <% } else { %>
                    <div class="summary noimg">
                    <% } %>
                        <h5>Holding Institution:</h5>
                        <p>
                            <%foreach (Institution institution in bibliographyItem.institutions)
                                {
                                    if (institution.InstitutionRoleName == "Holding Institution") {
                                        if (string.IsNullOrWhiteSpace(institution.InstitutionUrl))
                                        { %>
                                            <%: institution.InstitutionName%>
                                        <% }
                                        else
                                        { %>
                                            <a target='_blank' rel="noopener noreferrer" href="<%: institution.InstitutionUrl %>"><%: institution.InstitutionName%></a>
                                        <% }
                                    }
                                }%>                 
                        </p>
                        <h5>Sponsor:</h5>
                        <p><%: bibliographyItem.Book.Sponsor%></p>


                            <%foreach (Institution institution in bibliographyItem.institutions)
                                {
                                    if (institution.InstitutionRoleName == "Scanning Institution") { %>
                                        <h5>Added By:</h5>
                                        <p>
                                        <%if (string.IsNullOrWhiteSpace(institution.InstitutionUrl))
                                        { %>
                                            <%: institution.InstitutionName%>
                                        <% }
                                        else
                                        { %>
                                            <a target='_blank' rel="noopener noreferrer" href="<%: institution.InstitutionUrl %>"><%: institution.InstitutionName%></a>
                                        <% } %>
                                        </p>
                                    <%}
                                }%>                 



                        <% if (bibliographyItem.Book.ScanningDate != null) { %>                        
                            <h5>Date Scanned:</h5>
                            <p><%: bibliographyItem.Book.ScanningDate.Value.ToString("MM/dd/yyyy")%></p>
                        <% } %>

                        <div class="booklinks">
                            <% if (!string.IsNullOrWhiteSpace(bibliographyItem.Book.ExternalUrl))  { %>
                                <a target="_blank" rel="noopener noreferrer" href="/item/<%: bibliographyItem.Book.BookID %>">View Volume (External)</a>
                            <% } else if (bibliographyItem.Book.IsVirtual == 0) { %>
                                <a target="_self" href="/item/<%: bibliographyItem.Book.BookID %>">View Volume</a>
                            <%} %>
                            <% if (bibliographyItem.Book.NumberOfSegments > 0) { 
                                if (bibliographyItem.Book.IsVirtual == 0 || !string.IsNullOrWhiteSpace(bibliographyItem.Book.ExternalUrl)) {%>
                                    <br />
                                <%} %>
                                <a href="/itemdetails/<%: bibliographyItem.Book.BookID %>">View Parts</a>
                            <%} %>
                        </div>

                    </div>
                    
                    <%if (!string.IsNullOrWhiteSpace(bibliographyItem.Book.ItemDescription)) { %>
                    
                        <div class="copyspecific copyright">
                            <h5>Copy-specific information:</h5>
                            <p>
                                <%: bibliographyItem.Book.ItemDescription %>
                            </p>
                        </div>

                    <% } %>

                    <div class="copyright">
                        <h5>Copyright &amp; Usage:</h5>
                        <p>
                            <%  bool showNone = true;
                                if (!string.IsNullOrWhiteSpace(bibliographyItem.Book.LicenseUrl)) { %>
                                License Type:
                                <%if (System.Text.RegularExpressions.Regex.IsMatch(bibliographyItem.Book.LicenseUrl, "^(https?|ftp|file)://.+$")) {%>
                                    <a target="_blank" rel="noopener noreferrer" href="<%: bibliographyItem.Book.LicenseUrl%>"><%: bibliographyItem.Book.LicenseUrl%></a>
                                <% } else {%>
                                    <%: bibliographyItem.Book.LicenseUrl%>
                                <% }
                                showNone = false;%>
                                <br />
                            <% } %>
                            <% if (!string.IsNullOrWhiteSpace(bibliographyItem.Book.Rights)) { %>
                                Rights:
                                <%if (System.Text.RegularExpressions.Regex.IsMatch(bibliographyItem.Book.Rights, "^(https?|ftp|file)://.+$")) { %>
                                    <a target="_blank" rel="noopener noreferrer" href="<%: bibliographyItem.Book.Rights%>"><%: bibliographyItem.Book.Rights%></a>
                                <% } else { %>
                                    <%: bibliographyItem.Book.Rights%>
                                <% }
                                showNone = false;%>
                                <br />
                            <% } %>
                            <% if (!string.IsNullOrWhiteSpace(bibliographyItem.Book.DueDiligence)) { %>
                                Due Diligence: 
                                <%if (System.Text.RegularExpressions.Regex.IsMatch(bibliographyItem.Book.DueDiligence, "^(https?|ftp|file)://.+$"))
                                    { %>
                                    <a target="_blank" rel="noopener noreferrer" href="<%: bibliographyItem.Book.DueDiligence%>"><%: bibliographyItem.Book.DueDiligence%></a>
                                <% } else { %>
                                    <%: bibliographyItem.Book.DueDiligence%>
                                <% }
                                showNone = false;%>
                                <br />
                            <% } %>
                            <% if (!string.IsNullOrWhiteSpace(bibliographyItem.Book.CopyrightStatus)) { %>
                                Copyright Status: <%: bibliographyItem.Book.CopyrightStatus%><br />
                                <%showNone = false;%>
                            <% } %>
                            <% if (!string.IsNullOrWhiteSpace(bibliographyItem.Book.CopyrightRegion)) { %>
                                Copyright Region: <%: bibliographyItem.Book.CopyrightRegion%><br />
                                <%showNone = false;%>
                            <% } %>
                            <% if (!string.IsNullOrWhiteSpace(bibliographyItem.Book.CopyrightComment)) { %>
                                Copyright Comments: <%: bibliographyItem.Book.CopyrightComment%><br />
                                <%showNone = false;%>
                            <% } %>
                            <% if (!string.IsNullOrWhiteSpace(bibliographyItem.Book.CopyrightEvidence)) { %>
                                Copyright Evidence: <%: bibliographyItem.Book.CopyrightEvidence%><br />
                                <%showNone = false;%>
                            <% } %>


                            <%foreach (Institution institution in bibliographyItem.institutions)
                                {
                                    if (institution.InstitutionRoleName == "Rights Holder") { %>
                                        Rights Holder:
                                        <%if (string.IsNullOrWhiteSpace(institution.InstitutionUrl))
                                        { %>
                                            <%: institution.InstitutionName%>
                                        <% }
                                        else
                                        { %>
                                            <a target='_blank' rel="noopener noreferrer" href="<%: institution.InstitutionUrl %>"><%: institution.InstitutionName%></a>
                                        <% }
                                        showNone = false;
                                    }
                                }%>                 
                            <br/>

                            <% if (showNone) { %>
                                Not specified
                            <% } %>
                        </p>
                    </div>
                    <% if (bibliographyItem.Book.ItemSourceID.ToString().Trim() == "1") { %>
                    <div class="download">
                        <% if (bibliographyItem.Book.IsVirtual == 0) { %>
                            <div class="downloadlabel">Download:</div>
                            <a class="icon all" title="download all" href="<%: bibliographyItem.Book.DownloadUrl %>">All</a>
                            <a class="icon jp2" title="download jp2" href="/itemimages/<%: bibliographyItem.Book.BookID %>">JP2</a>
                            <a class="icon ocr" title="download ocr" download="<%: bibliographyItem.Book.BookID %>.txt" href="/itemtext/<%: bibliographyItem.Book.BookID %>">OCR</a>
                            <a class="icon pdf" title="download pdf" download="<%: bibliographyItem.Book.BookID %>.pdf" href="/itempdf/<%: bibliographyItem.Book.BookID %>">PDF</a>
                        <%} %>
                    </div>
                    <% } %>
                </div>

            </div>
            <% } %>
            </div>
        <% } %>
    </aside>    
</div>
</asp:Content>

<asp:Content ID="scriptContent" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
<script src="/js/libs/jquery.history.min.js"></script>
<script src="/js/citation-js/citation-js@0.6.4.js" type="text/javascript"></script>
<script src="/js/citation-js/bhl-citation-js.js" type="text/javascript"></script>
<script type="text/javascript">
    var cmArgs = new CitationModalArgs();
    cmArgs.init({
        tid: <%: BhlTitle.TitleID %>
    });
</script>
<script type="text/javascript">
//<![CDATA[
    $(document).ready(function () {
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