<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="MOBOT.BHL.Web2.Search" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
<%@ Register TagPrefix="bb" TagName="BookBrowseControl" Src="~/Controls/BookBrowseControl.ascx" %>
<%@ Register TagPrefix="sb" TagName="SectionBrowseControl" Src="~/Controls/SectionBrowseControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" id="navbar" />
<div id="browseContainerDiv">
<nav id="sub-nav-tabs" class="no-js-hide">
    <div class="column-wrap">
        <span class="arrow search"></span>
        <div id="linkbar">
				<div style="display:none;">
					<span class="pageheader">Search Results</span>&nbsp;
					(<span class="pagesubheader"><asp:HyperLink ID="lnkNewSearch" runat="server" Text="New Search" NavigateUrl="/"></asp:HyperLink></span>)
				</div>
                <h3>Results for "<%= uiSearchTerm %>"</h3>
                  <ul>
				
                <li runat="server" id="spanTitleSummary" visible="false" class="titles">
				<a href="#/titles" title="Books" >Books/Journals (<%=titleCount %>)</a>
                </li>
               
              
                <li runat="server" id="spanSectionSummary" visible="false" class="sections">
                <a href="#/sections" title="Parts">Articles/Chapters/Treatments (<%= segmentCount %>)</a>
                </li>    


                <li runat="server" id="spanAuthorSummary" visible="false" class="authors">
				<a href="#/authors" title="Authors">Authors (<%= authorCount %>)</a>

                </li>
                
				
                <li runat="server" id="spanSubjectSummary" visible="false" class="subjects">
				<a href="#/subjects" title="Subjects">Subjects (<%= subjectCount %>)</a>

                </li>
             
			
				<li runat="server" id="spanNameSummary" visible="false" class="names">
                <a href="#/names" title="Names">Scientific Names (<%= nameCount %>)</a>
                </li>
           		
			
                <li runat="server" id="spanAnnotationSummary" visible="false" class="annotations">
				<a href="#/annotations" title="Annotations">Annotations (<%= annotationCount %>)</a>

                </li>

			
                <li runat="server" id="spanAnnotationConceptSummary" visible="false" class="annotationConcepts">
				<a href="#/annotationConcepts" title="Concepts">Annotation Concepts (<%= annoConceptCount %>)</a>

                </li>
                
			
                <li runat="server" id="spanAnnotationSubjectSummary" visible="false" class="annotationSubjects">
				<a href="#/annotationSubjects" title="Subjects">Annotation Subjects (<%= annoSubjectCount %>)</a>

                </li>
               
                </ul>
        </div>
    </div>
</nav>



         <div id="sub-sections" class="clearfix">

			<asp:Panel ID="titles" Visible="false" runat="server" ClientIDMode="Static">	
             
                <div id="sortbar">
                <div class="column-wrap">
                    <span>Sort By:</span> 
                    <ul>
                    <li class="<%= SetSortClass("rank") %>"><asp:HyperLink ID="lnkRankSort" runat="server" Text="Relevance" Enabled="false"></asp:HyperLink></li>
                    <li class="<%= SetSortClass("title") %>"><asp:HyperLink ID="lnkTitleSort" runat="server" Text="Title"></asp:HyperLink></li>
                    <li class="<%= SetSortClass("author") %>"><asp:HyperLink ID="lnkAuthorSort" runat="server" Text="Author"></asp:HyperLink></li>
                    <li class="<%= SetSortClass("date") %>"><asp:HyperLink ID="lnkDateSort" runat="server" Text="Year"></asp:HyperLink></li>
                    </ul>
                    <asp:HyperLink ID="hypBookDownload" runat="server" Text="Download results" Visible="false"  class="searchtitlelinks"></asp:HyperLink>
                    <asp:HyperLink ID="lnkTitleMoreTop" Visible="false" runat="server" Text="View more Books/Journals..." class="searchtitlelinks"/>
                    <asp:Literal ID="litTitleRefineTop" Visible="false" runat="server" Text="" />
                    <div class="floatclear"></div>
                </div>
             </div>

                    
               <div class="content column-wrap">
                    <section class="js-hide">
			        <asp:Literal ID="litTitleRefine" Visible="false" runat="server" Text="<p><br/>More than 500 books/journals were found.  You might want to refine your search if these were more results than you expected.</p>"  />
                    <bb:BookBrowseControl ID="BookBrowse" runat="server" />
			        <asp:HyperLink ID="lnkTitleMore" Visible="false" runat="server" Text="more books/journals"  />
				    </section>
                </div>
           </asp:Panel>
                
				<asp:Panel ID="authors" ClientIDMode="Static" Visible="false" runat="server" class="floatclear">
              
                 <div id="page-title-authors"> 
                 <div class="column-wrap">  
                 				
                <asp:HyperLink ID="lnkAuthorMoreTop" Visible="false" runat="server" Text="View more Authors"  class="searchtitlelinks"/>
                <asp:Literal ID="litAuthorRefineTop" Visible="false" runat="server" Text=""  />
                  </div>
                  </div>
                  <div class="content column-wrap">
                    <section class="js-hide">
			        <asp:Literal ID="litAuthorRefine" Visible="false" runat="server" Text="<p><br />More than 500 authors were found.  You might want to refine your search if these were more results than you expected.</p>" />
					<asp:Repeater ID="authorRepeater" runat="server">
						<ItemTemplate>
							<li><a href="/creator/<%# Eval("AuthorID")%>" title="Author">
                                <%# Eval("NameExtended") %>
							</a></li>
						</ItemTemplate>
						<HeaderTemplate>
							<ol>
						</HeaderTemplate>
						<FooterTemplate>
							</ol></FooterTemplate>
					</asp:Repeater>
			        <asp:HyperLink ID="lnkAuthorMore" Visible="false" runat="server" Text="more authors...<br><br>" />
				</section>
                </div>
               
                </asp:Panel>

                <asp:Panel ID="sections" ClientIDMode="Static" Visible="false" runat="server" CSSclass="floatclear">
                <div id="sortbar">
                <div class="column-wrap">
                    <span>Sort By:</span> 
                    <ul>
                    <li class="<%= SetSortClass("rank") %>"><asp:HyperLink ID="lnkRankSegSort" runat="server" Text="Relevance" Enabled="false"></asp:HyperLink></li>
                    <li class="<%= SetSortClass("title") %>"><asp:HyperLink ID="lnkTitleSegSort" runat="server" Text="Title"></asp:HyperLink></li>
                    <li class="<%= SetSortClass("author") %>"><asp:HyperLink ID="lnkAuthorSegSort" runat="server" Text="Author"></asp:HyperLink></li>
                    <li class="<%= SetSortClass("year") %>"><asp:HyperLink ID="lnkDateSegSort" runat="server" Text="Year"></asp:HyperLink></li>
                    </ul>
                    <asp:HyperLink ID="hypSegmentDownload" runat="server" Text="Download results" Visible="false"  class="searchtitlelinks"></asp:HyperLink>
                    <asp:HyperLink ID="lnkSegmentMoreTop" Visible="false" runat="server" Text="View more Articles/Chapters/Treatments"  class="searchtitlelinks"/>
                    <asp:Literal ID="litSegmentRefineTop" Visible="false" runat="server" Text=""  />
                    <div class="floatclear"></div>
                </div>
                </div>
                  <div class="content column-wrap">
                    <section class="js-hide">
			        <asp:Literal ID="litSegmentRefine" Visible="false" runat="server" Text="<p><br />More than 500 articles/chapters/titles were found.  You might want to refine your search if these were more results than you expected.</p>" />
                    <sb:SectionBrowseControl ID="SectionBrowse" runat="server" />
			        <asp:HyperLink ID="lnkSegmentMore" Visible="false" runat="server" Text="more articles/chapters/treatments...<br><br>" />
				    </section>
                </div>
                </asp:Panel>


				<asp:Panel ID="subjects" ClientIDMode="Static" Visible="false" runat="server" class="floatclear">
              
                
                 <div id="page-title-subjects"> 
                 <div class="column-wrap">  
                    <asp:HyperLink ID="lnkSubjectMoreTop" Visible="false" runat="server" Text="View more Subjects" class="searchtitlelinks" />
                    <asp:Literal ID="litSubjectRefineTop" Visible="false" runat="server" Text=""  />
				</div>
                </div>
                	<div class="content column-wrap">
                    <section class="js-hide">
 			        <asp:Literal ID="litSubjectRefine" Visible="false" runat="server" Text="<p><br />More than 500 subjects were found.  You might want to refine your search if these were more results than you expected.</p>" />
                   <asp:Repeater ID="subjectRepeater" runat="server">
						<ItemTemplate>
							<li><a href="/subject/<%# Server.UrlEncode((string)Eval("MarcDataFieldTag"))%>" title="Subject">
								<%# Eval( "Keyword" )%>
							</a></li>
						</ItemTemplate>
						<HeaderTemplate>
							<ol>
						</HeaderTemplate>
						<FooterTemplate>
							</ol></FooterTemplate>
					</asp:Repeater>
			        <asp:HyperLink ID="lnkSubjectMore" Visible="false" runat="server" Text="more subjects...<br><br>" />
				    </section>
                    </div>
                   
                </asp:Panel>

				<asp:Panel ID="names" ClientIDMode="Static" Visible="false" runat="server" class="floatclear">
                
                 <div id="page-title-names"> 
                 <div class="column-wrap">  
                    <asp:HyperLink ID="lnkNameMoreTop" Visible="false" runat="server" Text="View more Scientific Names"  class="searchtitlelinks"/>
                    <asp:Literal ID="litNameRefineTop" Visible="false" runat="server" Text=""/>
                </div>
                </div>
					 <div class="content column-wrap">
                    <section class="js-hide">
						<p>Biodiversity Heritage Library uses <em>taxonomic intelligence</em> tools, including <a href="https://github.com/gnames/gnfinder" title="gnfinder" target="_blank">gnfinder</a> developed
						by <a href="http://globalnames.org/" title="uBio">Global Names Architecture</a>, to locate, verify, and record scientific names located
						within the text of each digitized page. <b>Note:</b> The text used for this identification is uncorrected OCR, so may not include
						all results expected or visible in the page.</p>
 			        <asp:Literal ID="litNameRefine" Visible="false" runat="server" Text="<p><br />More than 500 scientific names were found.  You might want to refine your search if these were more results than you expected.</p>" />
                   <asp:Repeater ID="nameRepeater" runat="server">
						<ItemTemplate>
							<li><a href="/name/<%# Eval("ResolvedNameString").ToString().Replace(' ', '_').Replace('.', '$').Replace('?', '^').Replace('&', '~') %>" title="Name">
								<%# Eval("ResolvedNameString") %>
							</a>(<%# Eval("PageCount") %>) </li>
						</ItemTemplate>
						<HeaderTemplate>
							<ol>
						</HeaderTemplate>
						<FooterTemplate>
							</ol></FooterTemplate>
					</asp:Repeater>
			        <asp:HyperLink ID="lnkNameMore" Visible="false" runat="server" Text="more scientific names...<br><br>" />
				    </section>
                    </div>
                    
                </asp:Panel>

                <asp:Panel ID="annotations" ClientIDMode="Static" Visible="false" runat="server" class="floatclear">
                 <div id="page-title-annotations"> 
                 <div class="column-wrap">  
                    <asp:HyperLink ID="lnkAnnotationMoreTop" Visible="false" runat="server" Text="View more Annotations" class="searchtitlelinks" />
                    <asp:Literal ID="litAnnotationRefineTop" Visible="false" runat="server" Text=""  /> 
                </div>
                </div>
                    <div class="content column-wrap">
                    <section class="js-hide">
 			            <asp:Literal ID="litAnnotationRefine" Visible="false" runat="server" Text="<p><br/>More than 500 annotations were found.  You might want to refine your search if these were more results than you expected.</p>" />
                       <asp:Repeater ID="annotationRepeater" runat="server">
                            <ItemTemplate>
                                <li>
                                    <span class="BlackHeading"><a href="/page/<%# Eval("PageID") %>" title="Page"> <%# Eval("AnnotationText") %></a></span>
			                        <br /><%# Eval("FullTitleExtended").ToString()%>
                                    <%# Eval("Authors") == string.Empty ? "" : "<br />By: " + Eval("Authors").ToString().Replace("|", " - ") %>
                                    <%# Eval("EditionStatement") == string.Empty ? "" : "<br />Edition: " + Eval("EditionStatement") %>
                                    <%# Eval("PublicationDetails") == string.Empty ? "" : "<br />Publication info: " + Eval("PublicationDetails") %>
                                    <%# Eval("Volume") == string.Empty ? "" : "<br />Volume: " + Eval("Volume") %>
                                    <%# Eval("PageNumbers") == string.Empty ? "" : "<br />" + Eval("PageNumbers") %>
                                </li>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <ol>
                            </HeaderTemplate>
                            <FooterTemplate>
                                </ol>
                            </FooterTemplate>
                        </asp:Repeater>
			            <asp:HyperLink ID="lnkAnnotationMore" Visible="false" runat="server" Text="more annotations...<br><br>" />
                    </section>
                    </div>
                </asp:Panel>

				<asp:Panel ID="annotationConcepts" ClientIDMode="Static" Visible="false" runat="server" class="floatclear">
                 <div id="page-title-annoconcepts"> 
                 <div class="column-wrap">  
                    <asp:HyperLink ID="lnkAnnotationConceptMoreTop" Visible="false" runat="server" Text="View more Annotation Concepts"  class="searchtitlelinks"/>
                    <asp:Literal ID="litAnnotationConceptRefineTop" Visible="false" runat="server" Text=""/>
				</div>
                </div>
                	<div class="content column-wrap">
                    <section class="js-hide">
			        <asp:Literal ID="litAnnotationConceptRefine" Visible="false" runat="server" Text="<p><br/>More than 500 annotation concepts were found.  You might want to refine your search if these were more results than you expected.</p>" />
                    <asp:Repeater ID="annotationConceptRepeater" runat="server">
						<ItemTemplate>
							<li><a href="/DLIndexBrowse.aspx?concept=<%# Eval("AnnotationConceptCode")%>" title="Concept">
								<%# Eval( "ConceptText" )%>
							</a></li>
						</ItemTemplate>
						<HeaderTemplate>
							<ol>
						</HeaderTemplate>
						<FooterTemplate>
							</ol></FooterTemplate>
					</asp:Repeater>
			        <asp:HyperLink ID="lnkAnnotationConceptMore" Visible="false" runat="server" Text="more annotation concepts...<br><br>" />
				    </section>
                    </div>
                
                </asp:Panel>

				<asp:Panel ID="annotationSubjects" ClientIDMode="Static" Visible="false" runat="server" class="floatclear">
				 <div id="page-title-annosubjects"> 
                 <div class="column-wrap">  	
                    <asp:HyperLink ID="lnkAnnotationSubjectMoreTop" Visible="false" runat="server" Text="View more Annotation Subjects"  class="searchtitlelinks"/>
                    <asp:Literal ID="litAnnotationSubjectRefineTop" Visible="false" runat="server" Text=""/>
					</div>
                    </div>
                    <div class="content column-wrap">
                    <section class="js-hide">
			        <asp:Literal ID="litAnnotationSubjectRefine" Visible="false" runat="server" Text="<p><br/>More than 500 annotation subjects were found.  You might want to refine your search if these were more results than you expected.</p>" />
                    <asp:Repeater ID="annotationSubjectRepeater" runat="server">
						<ItemTemplate>
							<li><a href="/DLIndexBrowse.aspx?cat=<%# Eval("AnnotationSubjectCategoryID")%>&sub=<%# Eval("AnnotationSubjectID")%>" title="Subject">
								<%# Eval( "SubjectCategoryName" )%> - <%# Eval( "SubjectText" )%>
							</a></li>
						</ItemTemplate>
						<HeaderTemplate>
							<ol>
						</HeaderTemplate>
						<FooterTemplate>
							</ol></FooterTemplate>
					</asp:Repeater>
			        <asp:HyperLink ID="lnkAnnotationSubjectMore" Visible="false" runat="server" Text="more annotation subjects...<br><br>" />
				</section>
                </div>
                </asp:Panel>
          	</div>  
   
                 <aside id="searchaside">
                    <uc:FeatureBox ID="FeatureBox5" runat="server" FeatureType="support"></uc:FeatureBox>
                    <uc:FeatureBox ID="FeatureBox6" runat="server" FeatureType="collection"></uc:FeatureBox>

                </aside>        
      
		
	</div>
</asp:Content>
<asp:Content ID="scriptContent" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
<script src="/js/libs/jquery.history.min.js"></script>
<script src="/js/libs/jquery.hoverintent.min.js"></script>
<script src="/js/libs/jquery.text-overflow.min.js"></script>
<script type="text/javascript">
//<![CDATA[
    $(document).ready(function () {
        var subSections = $('#sub-sections').children(); //.hide();

        // Navigate to the default sub-section if no hash
        if (!location.hash) {
            if (<%: (titleCount ?? "0") %> > 0) {
                window.location.replace('#/titles');
            }
            else if (<%: (segmentCount ?? "0") %> > 0) {
                window.location.replace('#/sections');
            }
            else if (<%: (authorCount ?? "0") %> > 0) {
                window.location.replace('#/authors');
            }
            else if (<%: (subjectCount ?? "0") %> > 0) {
                window.location.replace('#/subjects');
            }
            else if (<%: (nameCount ?? "0") %> > 0) {
                window.location.replace('#/names');
            }
            else if (<%: (annotationCount ?? "0") %> > 0) {
                window.location.replace('#/annotations');
            }
            else if (<%: (annoConceptCount ?? "0") %> > 0) {
                window.location.replace('#/annotationConcepts');
            }
            else if (<%: (annoSubjectCount ?? "0") %> > 0) {
                window.location.replace('#/annotationSubjects');
            }
            else {
                window.location.replace('#/titles');
            }
        }

        $.History.bind(function (state) {
            if (!$('#sub-sections').is(':visible')) {
                $('#sub-sections').show();
            }
            var stateName = state.replace(/[^a-zA-Z0-9\s]/gi, '');
            var subSection = $('#' + stateName);
            //var subSectionHeader = $('#subhead-' + stateName)
            subSections.hide();

            // Highlight selected link
            $('#linkbar a').removeClass('active');
            $('#linkbar .' + stateName + ' a').addClass('active').blur();

            // Special case
            if (stateName == 'all') {
                subSections.show();
                return false;
            }

            // If no default sub-section found then head on to the default otherwise show selected sub-section
            if (!subSection.length) {
              $.History.go('/all');
                return false;
            } else {
                subSection.show();
            }
        });
    });
//]]>
</script>
</asp:Content>
