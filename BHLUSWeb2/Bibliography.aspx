<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bibliography.aspx.cs" Inherits="MOBOT.BHL.Web2.Bibliography" %>
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="COinS" Src="~/controls/COinSControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="Mendeley" Src="~/controls/MendeleyShareControl.ascx" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="page-title" class="bibliography">
    <div class="column-wrap">
        <div class="ellipsis bibtitle"><%: BhlTitle.ShortTitle %></div>
        <div class="bibtitleicons">
            <uc:Mendeley id="mendeley" runat="server" />
            <a href="/contact/" title="Report an error" class="report"><img alt="Report an error" src="/images/rpterror.png" /></a>
        </div>
    </div>
</div>
<div id="content" class="column-wrap clearfix" itemscope itemtype="https://schema.org/Book">
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
                <h3>Title</h3>
                <span itemprop="url" style="display:none"><%: String.Format(ConfigurationManager.AppSettings["BibPageUrl"], BhlTitle.TitleID.ToString()) %></span>
                <p><span itemprop="name"><%: BhlTitle.FullTitle %> <%: BhlTitle.PartNumber %> <%: BhlTitle.PartName %></span></p>
                <% if (TitleVariants.Count > 0 || !string.IsNullOrWhiteSpace(BhlTitle.UniformTitle)) { %>
                    <h3>Title Variants:</h3>
                    <% foreach (TitleVariant titleVariant in TitleVariants) { %>
                    <p>
                        <i><%: titleVariant.TitleVariantLabel %>:</i>
                        <%: titleVariant.Title %> <%: titleVariant.TitleRemainder %> <%: titleVariant.PartNumber %> <%: titleVariant.PartName %>
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
                    <% foreach (TitleAssociation titleAssociation in TitleAssociations) { %>
                    <p>
                        <i><%: titleAssociation.TitleAssociationLabel %>:</i>
					    <% if (titleAssociation.AssociatedTitleID != null) { %>
                            <a href="/bibliography/<%: titleAssociation.AssociatedTitleID %>">
                                <%: titleAssociation.Title %> <%: titleAssociation.Section %> <%: titleAssociation.Volume %> <%: titleAssociation.Heading %> <%: titleAssociation.Publication %> <%: titleAssociation.Relationship %>
                            </a>
                        <% } else { %>
                            <%: titleAssociation.Title %> <%: titleAssociation.Section %> <%: titleAssociation.Volume %> <%: titleAssociation.Heading %> <%: titleAssociation.Publication %> <%: titleAssociation.Relationship %>
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
                            <p><i>Available from:</i>
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
                    <% foreach (Author author in Authors) { %>
                        <span itemprop="author" itemscope itemtype='<%: (author.AuthorRoleID.ToString() == "1" || author.AuthorRoleID.ToString() == "4") ? "https://schema.org/Person" : "https://schema.org/Organization" %>'>
                        <a href="/creator/<%: author.AuthorID %>" title="Author">
							<span itemprop="name"><%: author.NameExtended %></span>
						</a><%if (!string.IsNullOrWhiteSpace(author.Relationship)) Response.Write(", " + author.Relationship); %>
                        <span itemprop='url' style='display:none'><%: string.Format(ConfigurationManager.AppSettings["AuthorPageUrl"], author.AuthorID.ToString()) %></span>
                        <%if (!string.IsNullOrWhiteSpace(author.StartDate)) { %>
                              <span itemprop='birthDate' style='display:none'><%: author.StartDate %></span>
                        <% } %>
                        <%if (!string.IsNullOrWhiteSpace(author.EndDate)) { %>
                              <span itemprop='deathDate' style='display:none'><%: author.EndDate %></span>
                        <% } %>
                        </span>
                        <br />
                    <% } %>
                    <% if (Authors.Count > 0 && AdditionalAuthors.Count > 0) Response.Write("<br />"); %>
                    <% foreach (Author author in AdditionalAuthors) { %>
                        <span itemprop="author" itemscope itemtype='<%: (author.AuthorRoleID.ToString() == "1" || author.AuthorRoleID.ToString() == "4") ? "https://schema.org/Person" : "https://schema.org/Organization" %>'>
                        <a href="/creator/<%: author.AuthorID %>" title="Author">
							<span itemprop="name"><%: author.NameExtended %></span>
						</a><%if (!string.IsNullOrWhiteSpace(author.Relationship)) Response.Write(", " + author.Relationship); %>
                        <span itemprop='url' style='display:none'><%: string.Format(ConfigurationManager.AppSettings["AuthorPageUrl"], author.AuthorID.ToString()) %></span>
                        <%if (!string.IsNullOrWhiteSpace(author.StartDate)) { %>
                              <span itemprop='birthDate' style='display:none'><%: author.StartDate %></span>
                        <% } %>
                        <%if (!string.IsNullOrWhiteSpace(author.EndDate)) { %>
                              <span itemprop='deathDate' style='display:none'><%: author.EndDate %></span>
                        <% } %>
                        </span>
                        <br />
                    <% } %>
                </p>
                <h3>Type</h3>
                <p>
                    <span itemprop="genre"><%= Genre %></span>
                </p>
                <%if (!string.IsNullOrWhiteSpace(Material)) { %>
                <h3>Material</h3>
                <p>
                    <span><%= Material %></span>
                </p>
                <%} %>
                <h3>Publication info</h3>
                <p>
                    <span itemprop="publisher" itemscope itemtype="https://schema.org/Organization"><span itemprop="name"><%: BhlTitle.PublicationDetails %></span></span>
                    <span itemprop="datePublished" style="display:none"><%: BhlTitle.StartYear.ToString() %></span>
                </p>
                <%if (!String.IsNullOrWhiteSpace(BhlTitle.EditionStatement)) { %>
                    <h3>Edition</h3>
                    <p>
                        <span itemprop="bookEdition"><%: BhlTitle.EditionStatement%></span>
                    </p>
                <% } %>
                <%if (!String.IsNullOrWhiteSpace(BhlTitle.CurrentPublicationFrequency)) { %>
                    <h3>Frequency</h3>
                    <p>
                        <%: BhlTitle.CurrentPublicationFrequency%>
                    </p>
                <% } %>
                <% if (TitleKeywords != null && TitleKeywords.Count > 0)
                   { %>
                    <h3>Subjects</h3>
                    <p><span itemprop="keywords">
                    <% for (int i = 0; i < TitleKeywords.Count; i++)
                       { %>
                        <a href="/subject/<%: Server.UrlPathEncode(TitleKeywords[i].Keyword) %>">
                            <%: TitleKeywords[i].Keyword%>
                        </a>
                        <%: (i < TitleKeywords.Count - 1) ? ", " : string.Empty%>
                    <% } %>
                    </span></p>
                <% } %>
                <% if (Collections.Count > 0) { %>
                    <h3>BHL Collections:</h3>
                    <% foreach (Collection collection in Collections) { %>
                    <p>
                        <a href="/browse/collection/<%: collection.CollectionID %>" title="Collection"><%: collection.CollectionName %></a>
                    </p>
                    <% } %>
                <% } %>
                <% if (DOI != string.Empty) { %>
                <h3>DOI</h3>
                <p>
                    <a href="<%= DOI%>" title="DOI"><span itemprop="DOI"><%= DOI%></span></a>
                </p>
                <% } %>
                <p>
                    <a class="button" href="<%: LocalLibraryUrl %>" rel="noopener noreferrer" target="_blank">Find in a local library</a>
                </p>
            </div>
            <div id="details" class="tab-body">
                <h3>Title</h3>
                <p><%: BhlTitle.FullTitle %> <%: BhlTitle.PartNumber %> <%: BhlTitle.PartName %></p>
                <% if (TitleVariants.Count > 0 || !string.IsNullOrWhiteSpace(BhlTitle.UniformTitle)) { %>
                    <h3>Title Variants:</h3>
                    <% foreach (TitleVariant titleVariant in TitleVariants) { %>
                    <p>
                        <i><%: titleVariant.TitleVariantLabel %>:</i>
                        <%: titleVariant.Title %> <%: titleVariant.TitleRemainder %> <%: titleVariant.PartNumber %> <%: titleVariant.PartName %>
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
                    <% foreach (TitleAssociation titleAssociation in TitleAssociations) { %>
                    <p>
                        <i><%: titleAssociation.TitleAssociationLabel %>:</i>
					    <% if (titleAssociation.AssociatedTitleID != null) { %>
                            <a href="/bibliography/<%: titleAssociation.AssociatedTitleID %>">
                                <%: titleAssociation.Title %> <%: titleAssociation.Section %> <%: titleAssociation.Volume %> <%: titleAssociation.Heading %> <%: titleAssociation.Publication %> <%: titleAssociation.Relationship %>
                            </a>
                        <% } else { %>
                            <%: titleAssociation.Title %> <%: titleAssociation.Section %> <%: titleAssociation.Volume %> <%: titleAssociation.Heading %> <%: titleAssociation.Publication %> <%: titleAssociation.Relationship %>
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
                            <p><i>Available from:</i>
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
                    <h3>Notes:</h3>
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
                        <a href="/subject/<%: Server.UrlPathEncode(TitleKeywords[i].Keyword) %>">
                            <%: TitleKeywords[i].Keyword%>
                        </a>
                        <%: (i < TitleKeywords.Count - 1) ? ", " : string.Empty%>
                    <% } %>
                    </p>
                <% } %>
                <% if (Collections.Count > 0) { %>
                    <h3>BHL Collections:</h3>
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
                    <h3>Identifiers:</h3>
                    <% foreach (Title_Identifier titleIdentifier in TitleIdentifiers) { %>
                        <%: titleIdentifier.IdentifierLabel %>: <span itemprop="<%: titleIdentifier.IdentifierLabel%>"><%: titleIdentifier.IdentifierValue %></span><br />
                    <% } %>
                <% } %>
                <% if (DOI != string.Empty) { %>
                <h3>DOI</h3>
                <p>
                    <a href="<%= DOI%>" title="DOI"><%= DOI%></a>
                </p>
                <% } %>
                <p>
                    <a class="button" href="<%: LocalLibraryUrl %>" rel="noopener noreferrer" target="_blank">Find in a local library</a>
                </p>
            </div>
            <div id="mods" class="tab-body">                                
                <p>
                    <a class="button" href="/modsdownload/<%: BhlTitle.TitleID %>">Download MODS</a>
                </p>
                <p class="header">
                    <asp:Literal ID="litMods" runat="server"></asp:Literal>
                </p>
            </div>
            <div id="bibtex" class="tab-body">
                <% if (!string.IsNullOrEmpty(litBibTeX.Text)) { %>
                <p>
                    <a class="button" href="/bibtexdownload/<%: BhlTitle.TitleID %>">Download BibTeX citations</a>
                </p>                
                <p class="header">
                    <asp:Literal ID="litBibTeX" runat="server"></asp:Literal>
                </p>
                <% } %>
            </div>
            <div id="ris" class="tab-body">
                <% if (!string.IsNullOrEmpty(litRIS.Text)) { %>
                <p>
                    <a class="button" href="/risdownload/<%: BhlTitle.TitleID %>">Download RIS citations</a>
                </p>
                <p class="header">
                    <asp:Literal ID="litRIS" runat="server" ></asp:Literal>
                </p>
                <% } %>
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
                    <span class="text"><%: bibliographyItem.Item.Volume%></span>
                     <a class="viewbook" href="/item/<%: bibliographyItem.Item.ItemID %>" title="View Volume">view volume</a>
                </h4>
                <div class="body">
                    <% if(!string.IsNullOrWhiteSpace(bibliographyItem.ThumbUrl)) { %>
                    <a href="/item/<%: bibliographyItem.Item.ItemID %>">
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
                        <p><%: bibliographyItem.Item.Sponsor%></p>


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



                        <% if (bibliographyItem.Item.ScanningDate != null) { %>                        
                            <h5>Date Scanned:</h5>
                            <p><%: bibliographyItem.Item.ScanningDate.Value.ToString("MM/dd/yyyy")%></p>
                        <% } %>

                        <div class="booklinks">
                            <%if (!string.IsNullOrWhiteSpace(bibliographyItem.Item.ExternalUrl))  { %>
                                <a target="_blank" rel="noopener noreferrer" href="/item/<%: bibliographyItem.Item.ItemID %>">View Volume (External)</a>
                            <% } else { %>
                                <a target="_self" href="/item/<%: bibliographyItem.Item.ItemID %>">View Volume</a>
                            <%} %>
                            <% if (bibliographyItem.Item.NumberOfSegments > 0)
                                { %>
                                <br />
                                <a href="/itemdetails/<%: bibliographyItem.Item.ItemID %>">View Identified Parts</a>
                            <%} %>
                        </div>

                    </div>
                    
                    <%if (!string.IsNullOrWhiteSpace(bibliographyItem.Item.ItemDescription)) { %>
                    
                        <div class="copyspecific copyright">
                            <h5>Copy-specific information:</h5>
                            <p>
                                <%: bibliographyItem.Item.ItemDescription %>
                            </p>
                        </div>

                    <% } %>

                    <div class="copyright">
                        <h5>Copyright &amp; Usage:</h5>
                        <p>
                            <%  bool showNone = true;
                                if (!string.IsNullOrWhiteSpace(bibliographyItem.Item.LicenseUrl)) { %>
                                License Type:
                                <%if (System.Text.RegularExpressions.Regex.IsMatch(bibliographyItem.Item.LicenseUrl, "^(https?|ftp|file)://.+$")) {%>
                                    <a target="_blank" rel="noopener noreferrer" href="<%: bibliographyItem.Item.LicenseUrl%>"><%: bibliographyItem.Item.LicenseUrl%></a>
                                <% } else {%>
                                    <%: bibliographyItem.Item.LicenseUrl%>
                                <% }
                                showNone = false;%>
                                <br />
                            <% } %>
                            <% if (!string.IsNullOrWhiteSpace(bibliographyItem.Item.Rights)) { %>
                                Rights:
                                <%if (System.Text.RegularExpressions.Regex.IsMatch(bibliographyItem.Item.Rights, "^(https?|ftp|file)://.+$")) { %>
                                    <a target="_blank" rel="noopener noreferrer" href="<%: bibliographyItem.Item.Rights%>"><%: bibliographyItem.Item.Rights%></a>
                                <% } else { %>
                                    <%: bibliographyItem.Item.Rights%>
                                <% }
                                showNone = false;%>
                                <br />
                            <% } %>
                            <% if (!string.IsNullOrWhiteSpace(bibliographyItem.Item.DueDiligence)) { %>
                                Due Diligence: 
                                <%if (System.Text.RegularExpressions.Regex.IsMatch(bibliographyItem.Item.DueDiligence, "^(https?|ftp|file)://.+$"))
                                    { %>
                                    <a target="_blank" rel="noopener noreferrer" href="<%: bibliographyItem.Item.DueDiligence%>"><%: bibliographyItem.Item.DueDiligence%></a>
                                <% } else { %>
                                    <%: bibliographyItem.Item.DueDiligence%>
                                <% }
                                showNone = false;%>
                                <br />
                            <% } %>
                            <% if (!string.IsNullOrWhiteSpace(bibliographyItem.Item.CopyrightStatus)) { %>
                                Copyright Status: <%: bibliographyItem.Item.CopyrightStatus%><br />
                                <%showNone = false;%>
                            <% } %>
                            <% if (!string.IsNullOrWhiteSpace(bibliographyItem.Item.CopyrightRegion)) { %>
                                Copyright Region: <%: bibliographyItem.Item.CopyrightRegion%><br />
                                <%showNone = false;%>
                            <% } %>
                            <% if (!string.IsNullOrWhiteSpace(bibliographyItem.Item.CopyrightComment)) { %>
                                Copyright Comments: <%: bibliographyItem.Item.CopyrightComment%><br />
                                <%showNone = false;%>
                            <% } %>
                            <% if (!string.IsNullOrWhiteSpace(bibliographyItem.Item.CopyrightEvidence)) { %>
                                Copyright Evidence: <%: bibliographyItem.Item.CopyrightEvidence%><br />
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
                    <% if (bibliographyItem.Item.ItemSourceID.ToString().Trim() == "1") { %>
                    <div class="download">
                        Download volume:
                        <a class="icon all" href="<%: bibliographyItem.Item.DownloadUrl %>">All</a>
                        <a class="icon jp2" href="/itemimages/<%: bibliographyItem.Item.ItemID %>">JP2</a>
                        <a class="icon ocr" download="<%: bibliographyItem.Item.ItemID %>.txt" href="/itemtext/<%: bibliographyItem.Item.ItemID %>">OCR</a>
                        <a class="icon pdf" download="<%: bibliographyItem.Item.ItemID %>.pdf" href="/itempdf/<%: bibliographyItem.Item.ItemID %>">PDF</a>
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