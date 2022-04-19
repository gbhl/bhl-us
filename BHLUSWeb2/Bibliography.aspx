﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bibliography.aspx.cs" Inherits="MOBOT.BHL.Web2.Bibliography" %>
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="COinS" Src="~/controls/COinSControl.ascx" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="page-title" class="bibliography">
    <div class="column-wrap">
        <div class="ellipsis bibtitle"><%: BhlTitle.ShortTitle %></div>
        <div class="bibtitleicons">
            <a href="<%= System.Configuration.ConfigurationManager.AppSettings["WikiPageFAQ"] %>" title="FAQ" class="report"><img alt="FAQ" src="/images/rpterror.png" /></a>
        </div>
    </div>
</div>
<div id="content" class="column-wrap clearfix" itemscope itemtype="https://schema.org/Book">
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
                <h3>Title</h3>
                <p><%: BhlTitle.FullTitle %> <%: BhlTitle.PartNumber %> <%: BhlTitle.PartName %></p>
                <% if (TitleVariants.Count > 0 || !string.IsNullOrWhiteSpace(BhlTitle.UniformTitle)) { %>
                    <h3>Title Variants</h3>
                    <% foreach (TitleVariant tv in TitleVariants) { %>
                    <p>
                        <i><%: tv.TitleVariantLabel %>:</i>
                        <%
                        List<string> tvTitle = new List<string>();
                        if (!string.IsNullOrWhiteSpace(tv.Title)) tvTitle.Add(tv.Title);
                        if (!string.IsNullOrWhiteSpace(tv.TitleRemainder)) tvTitle.Add(tv.TitleRemainder);
                        %>
                        <%: string.Join(", ", tvTitle.ToArray()) %>
                    </p>
                    <% } 
                    if (!string.IsNullOrWhiteSpace(BhlTitle.UniformTitle))
                    {%>
                      <p>
                          <i>Uniform: </i><%: BhlTitle.UniformTitle %>
                      </p>  
                    <%}
                } %>
                <% if(TitleAssociations.Count > 0) { %>                    
                    <h3>Related Titles</h3>
                    <% foreach (TitleAssociation ta in TitleAssociations) { %>
                    <p>
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
                            <%: string.Join(", ", taTitle.ToArray()) %>
                        <% } %>							
                    </p>
                    <% } %>
                <% } %>
                <%if (Institutions.Count > 0) {%>
                    <%foreach (Institution institution in Institutions) {
                        if (institution.InstitutionRoleName == "External Content Holder") {%>
                            <h3>More Content</h3>
                            <%break;
                        }
                    }%>
                    <%foreach (Institution institution in Institutions) {
                        if (institution.InstitutionRoleName == "External Content Holder") {%>
                            <p><i>Available from</i>
                            <%if (string.IsNullOrWhiteSpace(institution.Url)) { %>
                                <%:institution.InstitutionName%>
                            <%} else {%>
                                <a class="ExtLinkBrowse" href="<%:institution.Url%>" rel="noopener noreferrer" target="_blank"><%:institution.InstitutionName%></a>
                            <%}%>
                            </p>
                        <%}
                    }
                } %>
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
                    <% foreach (Collection collection in Collections) { %>
                    <p>
                        <a href="/browse/collection/<%: collection.CollectionID %>" title="Collection"><%: collection.CollectionName %></a>
                    </p>
                    <% } %>
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
                    <a class="button" style="float:right" href="/modsdownload/<%: BhlTitle.TitleID %>">Download MODS</a>
                </p>
            </div>
        </div>
    </section>
    <aside>
        <% if(BibliographyItems.Count > 0) { %>
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
                        <div class="downloadlabel">Cite:</div>
                        <a class="icon ris" title="download ris" download="bhlitem<%: bibliographyItem.Book.BookID %>.ris" href="/risdownload/<%: bibliographyItem.Book.BookID %>">RIS</a>
                        <a class="icon bibtex" title="download bibtex" download="bhlitem<%: bibliographyItem.Book.BookID %>.bib" href="/bibtexdownload/<%: bibliographyItem.Book.BookID %>">BibTeX</a>
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