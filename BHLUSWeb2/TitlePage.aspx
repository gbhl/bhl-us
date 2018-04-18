<%@ Page Title="Biodiversity Heritage Library" Language="C#" MasterPageFile="~/Book.Master" AutoEventWireup="true" CodeBehind="TitlePage.aspx.cs" Inherits="MOBOT.BHL.Web2.TitlePage" %>
<%@ Import Namespace="MOBOT.BHL.DataObjects" %>
<%@ Register TagPrefix="uc" TagName="COinS" Src="~/controls/COinSControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="Mendeley" Src="~/controls/MendeleyShareControl.ascx" %>
<asp:content id="mainContent" contentplaceholderid="mainContentPlaceHolder" runat="server">
    <div id="page-title">
        <div id="volumebar"  style="float:right;" class="js-invisible no-js-hide">
            <a href="/contact/" title="Report an error" class="report"><img alt="Report an error" src="/images/rpterror.png" /></a>
            <uc:Mendeley id="mendeley" runat="server" />
            <% if (!string.IsNullOrWhiteSpace(PageSummary.DownloadUrl)) { %>
                <div class="buttondrop download">Download Contents<div class="play"></div></div> 
                <div class="downloadcontents">
                    <div><a href="<%: (Titles.Count > 1) ? "/biblioselect/" + PageSummary.ItemID : "/bibliography/" + PageSummary.TitleID %>">View Metadata</a></div>
                    <div><a href="#" class="selectpages">Select pages to download</a></div>
                    <div><a href="#" class="downloadbook">Download Book</a></div>
                    <div><a href="<%= string.Format("https://www.archive.org/details/{0}", PageSummary.BarCode) %>" target="_blank">View at Internet Archive</a></div>
                </div>
                <div class="jqmWindow" id="download-dialog">
                    <div class="head">
                        <a class="jqmClose" title="Close Dialog">Close Dialog</a>
                        <h2>Download book</h2>
                        <hr />
                    </div>
                    <a class="large-icon pdf" href="<%= string.Format("https://www.archive.org/download/{0}/{0}.pdf", PageSummary.BarCode) %>">Download PDF</a>
                    <a class="large-icon all" href="<%= PageSummary.DownloadUrl + PageSummary.BarCode %>">Download All</a>
                    <a class="large-icon jp2" href="<%= string.Format("https://www.archive.org/download/{0}/{0}_jp2.{1}", PageSummary.BarCode, ((PageSummary.BarCode.Substring(0,5) == "mobot") ? "tar" : "zip")) %>">Download JPEG 2000</a>
                    <a class="large-icon ocr" href="<%= string.Format("https://www.archive.org/download/{0}/{0}_djvu.txt", PageSummary.BarCode) %>">Download OCR</a>                
                </div>
            <% } %>

            <% if (ddlVolumes.Items.Count > 1) { %>
                 <div id="volumedd">
                    <div class="play"></div>
                    <asp:DropDownList ID="ddlVolumes" runat="server" EnableViewState="false" ClientIDMode="Static"/>
                </div>
            <% } %>
        </div>
        <div id="titletext">
            <a class="ellipsis journaltitlelink" href="<%: (Titles.Count > 1) ? "/biblioselect/" + PageSummary.ItemID : "/bibliography/" + PageSummary.TitleID %>"><%: PageSummary.FullTitle %></a>
            <a id="articleTitleLink" class="ellipsis articletitlelink" href="#"></a>
        </div>
    <uc:COinS ID="COinS" runat="server" />
    </div> <!-- page-title -->

    <div id="bookviewercontainer">
        <div id="left-panel2">
            <div class="left-panel-boxes">
                <div id="leftpanetabswrapper" class="tabs_wrapper">
                    <div id="leftpanetabs">
                        <ul>
                            <li class="panel-box-heading active" id="pagestab"><span>Pages</span></li>
                            <li class="panel-box-heading" id="segmentstab"><span>Table of Contents</span></li>
                        </ul>
                    </div>
                    <div id="leftpanetabs_content">
                        <div id="pagestabcontent" class="optionbox">
                            <asp:ListBox ID="lstPages" runat="server" EnableViewState="false" ClientIDMode="Static" cssClass="ui-pg-selbox pagelist"></asp:ListBox>
                        </div>
                        <div id="segmentstabcontent" class="optionbox">
                            <!-- iDevices -->
                            <asp:ListBox ID="lbSegments" runat="server" EnableViewState="false" ClientIDMode="Static" cssClass="ui-pg-selbox pagelist"></asp:ListBox>
                            <!-- non-iDevices -->
                            <div id="lstSegments" class="ui-pg-selbox pagelist">
                            <ul>
                                <% foreach (Segment segment in Segments)
                                { %>
                                    <li id="<%= segment.SegmentID %>">
                                        <a class="viewSegLinkTitle" id="<%= segment.StartPageID %>" href="#" style="text-decoration:none"><%= segment.Title %></a>
                                        <div class="segListVolPage">
                                            <% if (segment.Volume != string.Empty) { %>Volume <%= segment.Volume %><%} %>
                                            <% if (segment.Volume != string.Empty && segment.StartPageNumber != string.Empty) { %>, <%} %>
                                            <% if (segment.StartPageNumber != string.Empty) { %>Page <%= segment.StartPageNumber%><%} %>
                                        </div>
                                    </li>
                                    <%
                                } %>
                            </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="showmore">
                    <a href="#">Show More</a>
                </div>
                <div class="panel-box-heading">
                    URL for Current Page
                </div>
                <div class="urlbox"><a id="currentpageURL" href="#"></a></div>
                <div class="flickrbox">
                    <span id="flickrurlspan" style="display:none"><a class="flickrurl" id="currentFlickrURL" target="_blank" href="#">View Current Page in Flickr</a>&nbsp;<img width="12" src="/images/flickr-icon-normal.png"></span>&nbsp;
                </div>
            </div>
            <div id="names-container-div" class="left-panel-boxes limit-height">
                <div class="panel-box-heading">
                    Scientific Names on this Page
                </div>
                <div class="optionbox">
                    <h3 id="pagename" class="panel-box-childhead"></h3>
                    <div id="names-panel"></div>
                </div>
                <span class="ubiolink">Indexed by <a href="http://www.globalnames.org/">Global Names</a> <a href="http://www.globalnames.org/"><img src="/images/GNA_revised_icon_14.png" /></a></span>
            </div>
        </div> <!-- left-panel2 -->

        <div id="BRtoolbarwrapper"></div>

        <div id="bookwrapper">
            <div id="right-panel2">
                <div id="right-panel-content">
                    <div id="pageOCR-panel"> </div>

                    <!-- Search Panel -->
                    <div id="pageSearch-panel">
                        <div class="sibHeader">
                            <div class="sibHeaderTitle">Search Inside This Book:</div>
                            <div class="sibHeaderText">
                                <input id="sibSearchText" type="text" value="" /> <input id="sibSearchBtn" class="sibBtn" type="button" value="Search" />
                            </div>
                        </div>
                        <div id="searchResults">
                            <div>
                                <div class="sibResultsHeader">Results For:  <span id="sibTextEcho"></span> <span id="sibNumResults"></span></div>
                            </div>

                            <!-- facets -->
                            <!--
                            <div class="sibFacetContainer">
                                <div class="sibFacetBox">
                                    <div class="sibFacetHeader">Narrow Search By:</div>
                                    <div class="sibFacetLabel"><img src="/images/bib_minus.gif" /> Type</div>
                                    <div class="sibFacetValues"><input type="checkbox" /> Text (38) <input type="checkbox" /> Illustration (5) <input type="checkbox" /> Drawing (4) <input type="checkbox" /> Cover (2)</div>
                                    <hr />
                                    <div class="sibFacetLabel"><img src="/images/bib_minus.gif" /> Name</div>
                                    <div class="sibFacetValues"><input type="checkbox" /> Mollusca (7) <input type="checkbox" /> Lepidoptera (4) <input type="checkbox" /> Brachiopoda (3) <input type="checkbox" /> Astraea (2)</div>
                                </div>
                            </div>
                            -->

                            <div class="sibResultsContainer">
                                <div class="sibResultsBox">
                                 </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div> <!-- right-panel2 -->
            <div id="bookcontent">
                <div id="toolbar-top" class="column-wrap">
                    <div id="mypdfbar" class="disabled">
                        <div style="position: absolute; top: 15px; left: 440px; font-size:13px;"> Click/Shift+Click pages to select for download </div>

                        <div id="BRtoolbar-pdfzoombuttons">
                            <a class="BRicon zoom_out" title="Zoom Out">zoom out</a>
                            <span class="zoomlabel">Zoom</span>
                            <a class="BRicon zoom_in" title="Zoom In">zoom in</a>
                        </div>

                        <span class="cancelpdf button">Cancel</span>
                        <span class="generate button">Generate</span>
                        <span class="review button">Review</span>
                        <span class="counter">No Pages Added</span>
                    </div>
                    <div class="jqmWindow" id="review-dialog">
                        <div class="head">
                            <a class="jqmClose" title="Close Dialog">Close Dialog</a>
                            <a class="button generate modal">Generate My PDF</a>
                            <h2>Review My PDF</h2>
                            <span id="page-count"></span>
                            <a class='dicon list-view' title='List View'>List View</a>
                            <a class='dicon icon-view' title='Icon View'>Icon View</a>
                            <hr />
                        </div>
                        <div class="body">
                            <ul>
                            </ul>
                        </div>
                    </div>
                    <div class="jqmWindow" id="generate-dialog">
                        <div class="head">
                            <a class="jqmClose" title="Close Dialog">Close Dialog</a>
                            <a class="button finish">Finish</a>
                            <a class="button review modal">Review My PDF</a>
                            <h2>Generate My PDF</h2>
                            <hr />
                        </div>
                        <div class="body form">
                            <div class="intro">
                                <p>If you are generating a PDF of a journal article or book chapter, please feel free to enter the title and author information. The information you enter here will be stored in the downloaded file to assist you in managing your downloaded PDFs locally.<br /><br /></p>
                            </div>
                            <div class="success">
                                <p>Thank you for your request. Please wait for an email containing a link to download the PDF.</p>
                                <p>For your reference, the confirmation number for this request is <span id="pdfId"></span>.</p>
                                <div style="float:left;margin:0;width:49%">
                                    <h3>Join Our Mailing List</h3>
                                    <p>Sign up to receive the latest BHL news, content highlights, and promotions.</p>
                                    <a class="featurebutton-home" title="Subscribe to BHL Newsletter" target="_blank" href="https://library.si.edu/bhl-newsletter-signup">Subscribe</a>
                                </div>
                                <div style="float:left;margin:0;width:49%">
                                    <h3>Help Support <span>BHL</span></h3>
                                    <p>BHL relies on donations to provide free PDF downloads and other services.  Help keep BHL free and open!</p>
                                    <a class="featurebutton-home" title="Donate" target="_blank" href="https://library.si.edu/donate-bhl">Donate</a>
                                </div>
                            </div>
                            <div class="failure">
                                <p>There was an issue with the request. Please try again and if the problem persists, please send us <a href="/contact">feedback</a>.</p>
                                <p>For your reference, the confirmation number for this request is <span id="pdfIdFail"></span>.</p>
                            </div>
                            <div class="required">
                                <label for="tbEmail">Email Address<span class="required">(required)</span></label>
                                <div class="field-wrap">
                                    <input class="field required email" type="text" id="tbEmail" name="tbEmail" /><span id="tbEmailErr" style="color:#8a1826; font-size:12px; font-family:Georgia,Times,serif; position:absolute; left:322px; top:6.5px;"></span>
                                </div>
                                <label for="grpImageOCR">Include</label>
                                <div class="field-wrap">
                                    <asp:RadioButton ID="tbImages" ClientIDMode="Static" GroupName="grpImageOCR" runat="server" Text=" Page images only" Checked=true />&nbsp;&nbsp;<asp:RadioButton ID="tbImagesOcr" ClientIDMode="Static" GroupName="grpImageOCR" runat="server" Text=" Page images and OCR text" />
                                </div>
                            </div>
                            <div class="optional">
                                <span>Optional</span>
                                <label for="tbTitle">Article/Chapter Title</label>
                                <div class="field-wrap">
                                    <input class="field" type="text" id="tbTitle" name="tbTitle" /><span id="tbTitleErr" style="color:#8a1826; font-size:12px; font-family:Georgia,Times,serif; position:absolute; left:312px; top:6.5px;"></span>
                                </div>
                                <label for="tbAuthors">Author(s)</label>
                                <div class="field-wrap">
                                    <input class="field" type="text" id="tbAuthors" name="tbAuthors" /><span class="example">Example: Charles Darwin, Carl Linnaeus</span>
                                </div>
                                <label for="tbSubjects">Subject(s)</label>
                                <div class="field-wrap">
                                    <input class="field" type="text" id="tbSubjects" name="tbSubjects" /><span class="example">Example: Birds, Classification, Mammals</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="BookReader"></div>

                <div class="bookattribution">Contributed by <asp:PlaceHolder ID="attributionPlaceHolder" runat="server" /></div>
            </div> <!-- bookcontent -->
        </div> <!-- bookwrapper -->
    </div> <!-- bookwrapper -->

    <div id="AnnotationBoxPlaceholder" class="active">
        <div id="AnnotationBox" class="active" style="display:none">
            <div id="header">
                <a href="/collection/darwinlibrary" title="Collection"><img src="/images/darwin_book_header.png" alt="Charles Darwin's Library"/></a>
                <div id="indicator"><asp:Literal ID="ltlBookIndicator" runat="server" Text=""></asp:Literal><span style="float:right"><a href="/browse/collection/darwinlibrary" title="Books">Book List</a>&nbsp; | &nbsp;<a href="/collection/darwinlibrary">Help</a></span></div>
            </div>
            <div id="contentbox">
                <div id="pageScroller">
                    <a id="aPrev" href="javascript:void(0);" title="Previous"><span class="pagescrollerarrow">&laquo;</span>previous</a>&nbsp;&nbsp;&nbsp;&nbsp;<a id="aNext" href="javascript:void(0);" title="Next">next<span class="pagescrollerarrow">&raquo;</span></a>
                </div>
                <div id="no-annotations-stub" style="display:none;">No Marks</div>  <!--display when no Annotations-->
                <div id="stub" style="display:none;">Marks</div>    <!--display when Annotation Box is collapsed-->
                <div id="content">
                    <div id="notes"></div>
                    <div id="phantom-legend">&nbsp;</div>   <!--handle offset when box is collapsed-->
                    <div id="legendcontainer">
                        <div id="toggle_legend">
                            <img id="show_legend" src="../Images/bib_plus.gif" alt="hide legend" style="display:none"/>
                            <img id="hide_legend" src="../Images/bib_minus.gif" alt="show legend"/>
                            key
                        </div>
                        <div id="legend-items">
                            <div id="items">
                                    <div>
                                    <span class="left-column" style="padding-right:4px;">&#8249;</span>
                                    <span class="right-column" style="padding-left:1px;">&#8250;</span>
                                    <span class="special-char-text" style="padding-right:10px;">deleted</span>
                                    <span class="special-char">&#65517;</span> <span class="special-char-text" style="padding-right:10px;">pinholes visible</span>
                                    <span class="special-char">&#10719;</span> <span class="special-char-text">line connects passages</span>
                                </div>
                                <div>
                                    <span class="left-column">&#171;</span>
                                    <span class="right-column">&#187;</span>
                                    <span class="special-char-text" style="padding-right:6px;">inserted</span>
                                    <span class="special-char">&#8657;</span> <span class="special-char-text" style="padding-right:10px;">count up from last line</span>
                                    <span class="special-char">&#247;*</span> <span class="special-char-text">long division</span>
                                </div>
                            </div>
                        </div>
                    </div> <!-- legendcontainer -->
                    <div id="copyrightcontainer">
                        <div id="copyrighttitle">Credit</div>
                        <div id="copyright">Edition of <i>Charles Darwin's Reading Notes</i><br />by Di Gregorio & Gill<br />(Darwin Manuscripts Project: <a href="https://darwin.amnh.org" title="AMNH" target="_blank">darwin.amnh.org</a>)</div>
                    </div>
                </div> <!-- content --> 
            </div> <!-- contentbox --> 
        </div>
        <div id="annotation-not-available">Annotation Not Available</div>
        <asp:Literal ID="ltlAnnotationContent" runat="server"></asp:Literal>
    </div> <!-- AnnotationBoxPlaceholder -->
</asp:content>

<asp:Content ID="PageHeaderIncludes" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder"
    runat="server">
    <link rel="stylesheet" type="text/css" href="/css/BookReader.css?v=4" />
    <link rel="stylesheet" type="text/css" href="/css/bookviewer_extra.css?v=5" />
</asp:Content>
<asp:content id="scriptContent" contentplaceholderid="scriptContentPlaceHolder" runat="server">
<script src="/js/libs/jquery.easing.1.3.js" type="text/javascript"></script>
<script src="/js/libs/jquery-ui-1.8.11.custom.min.js" type="text/javascript"></script>
<script src="/js/libs/jquery.hoverintent.min.js" type="text/javascript"></script>
<script src="/js/libs/jquery.xcolor.min.js" type="text/javascript"></script>
<script src="/js/libs/jqModal.min.js" type="text/javascript"></script>
<script src="/js/libs/jquery.jscrollpane.min.js" type="text/javascript"></script>
<script src="/js/libs/jquery.mousewheel.min.js" type="text/javascript"></script>
<script src="/js/libs/jquery.validate.min.js" type="text/javascript"></script>
<script src="/js/libs/jquery.bt.min.js" type="text/javascript"></script>
<script src="/js/libs/jquery.printElement.min.js" type="text/javascript"></script>
<script src="/js/libs/BookReader.js?v=4" type="text/javascript"></script>
<script src="/js/libs/dragscrollable.js" type="text/javascript"></script>
<script src="/js/libs/jquery.text-overflow.min.js" type="text/javascript"></script>
<script type="text/javascript">
//<![CDATA[

    var ocrPopUp;
    var pageToolBoxEvent = "click";
    if(Modernizr.touch){
        pageToolBoxEvent = "tap";
    }

    var addthis_share = { 
        url: window.location.pathname,
        title: $(document).find("title").text(),
        templates: {
            twitter: "{{title}} {{url}}"
        }
    }

    // actionMode: either standard reading mode or selecting pages to download
    // actionModeType.Read: default. 
    // actionModeType.Select: triggered when user selects option to download a set of pages.
    var actionModeType = {
        Read: 1,
        Select: 2
    };
    var actionMode = actionModeType.Read;

    if (navigator.userAgent.match(/(iPad)|(iPhone)|(iPod)/i) !== null)
    {
        // Hide elements from iDevices
        $("#lstSegments").hide();
        $(".showmore a").hide();
    }
    else
    {
        $("#lbSegments").hide();
    }

    $(document).ready(function () {
        br = new BookReader();

        // Page info Json object
        var pages = <%= Pages %>;

        var pdfPages = [];
        var pdfBar = $('#mypdfbar');
        var PdfModeType = {
            List: 1,
            Icon: 2
        };
        var pdfMode;
        var lastPdfIndex = -1;
        var isModalDialogChange;
        var cancelPdfSelection = false;
        var newpageOCR = $("#pageOCR-panel");
        var newpageReaderComments = $("#pageReaderComments-panelInner");
        var pageNames = $("#names-panel");
        
        // On Hide Action for Dialogs
        function onHideAction(hash) {
            if(isModalDialogChange) {
                hash.w.hide();
                hash.o.remove();
                resetGenerate();
            } else {
                hash.w.fadeOut(200);
                hash.o.fadeOut(200, function () {
                    hash.o.remove();
                    resetGenerate(); 
                });
            }

            function resetGenerate() {
                $('#generate-dialog div, #generate-dialog .footer, #generate-dialog .finish, #generate-dialog .review').show();
                $('#generate-dialog').css('height', '520px');
                $('#generate-dialog .intro').show();
                $('#generate-dialog .success').hide();
                $('#generate-dialog .failure').hide();

                if (cancelPdfSelection) cancelSelectPages(); 
                cancelPdfSelection = false;
            }

            isModalDialogChange = false;
        };

        function selectPagesToDownload() {
            if (actionMode == actionModeType.Select) return;

            $('.BRtoolbar-container').hide();
            $('#toolbar-top').show();

            // Set up zoom actions on PDF toolbar
            var pdfToolbar = $('#toolbar-top');
            pdfToolbar.find('.zoom_out').unbind('click');   // unbind first so the same event isn't bound twice
            pdfToolbar.find('.zoom_in').unbind('click');
            pdfToolbar.find('.zoom_out').click(function (e) { br.zoom(-1); e.preventDefault(); });
            pdfToolbar.find('.zoom_in').click(function (e) { br.zoom(1); e.preventDefault(); });

            actionMode = actionModeType.Select;
            if(3 != br.mode) { br.switchMode(3); }

            $("#right-panel2").hide("fast", function() { if(br.mode == 3) { br.resizePageView(); } br.centerPageView();});
            resetAnnotationsBox();
            resetSearchBox();
            resetPageOCRBox();

            $(".pagetoolbox").show();
            setInterval('fixIEDisplayIssue()', 1000);
        }

        function cancelSelectPages() {
            if (actionMode == actionModeType.Read) return;

            actionMode = actionModeType.Read;
            $('.BRtoolbar-container').show();
            $('#toolbar-top').hide();

            $('.pagetoolbox.selected').each(function () {
                $(this).trigger(pageToolBoxEvent);
            });
            $(".pagetoolbox").hide();
            lastPdfIndex = -1;
            fixIEDisplayIssue();
        }

        function updateOCR(index){
            newpageOCR.text("");
            newpageOCR.addClass('loading');
            $.ajax({
                type: 'get',
                url: '/pagesummary/',
                data: {
                    'op': 'GetPageOcrText',
                    'pageID': pages[index].PageID
                },
                success: function (data) {
                    newpageOCR.text("");
                    var header = $('<div/>', { 'class' : 'header' }).appendTo(newpageOCR);
                                        
                    if (data.success && (data.ocrText != "OCR text unavailable for this page."))
                    {
                        header.append(
                            $('<a/>', {
                                'class' : 'BRicon pop_ocr',
                                'title' : 'View text in Window',
                                'text' : 'View text in Window'
                            }).click(function() {
                                if ((typeof ocrPopUp == undefined) || !ocrPopUp || ocrPopUp.closed)
                                {
                                    ocrPopUp = window.open('about:blank', '_blank', 'width=650,height=440,scrollbars=yes');

                                    var html = '<html><head><title>Text on: ' + br.getPageName(index) + '</title></head>'
                                    html += '<body><div style="font:14px Georgia, Times, serif;">' + $.trim(data.ocrText).replace(/\n/g, '<br>') + '</div>'
                                    html += '</body></html>';

                                    ocrPopUp.document.write(html);
                                }
                            })
                        );
                    }
                                            
                    header.append(                                          
                        $('<h5/>', { 
                            'text' : 'Viewing Page as Text'
                        })).append(
                        $('<em/>', {
                            'text' : 'This text is generated from uncorrected OCR and as such, may contain, inconsistencies with the actual content of the original page.'
                        }));
                                        
                    var text = $('<div/>', { 'class': 'text' })
                        .html('<span>' + $.trim(data.ocrText).replace(/\n/g, '<br />') + '<br /><br /><br /><br /><br /></span>')
                        .appendTo(newpageOCR);

                    // Create BeautyTips if canvas exists & not IE (results in singlepage view scrolling to top).
                    if(Modernizr.canvas && !$.browser.msie) {
                        $('.pop_ocr').bt({
                            padding: 10,
                            spikeLength:10,
                            spikeGirth:13,
                            fill: '#266099',
                            strokeWidth: 0,
                            shrinkToFit: true,
                            positions: ['most'],
                            overlap: -1,
                            cssStyles: { color: '#fff' },
                            hoverIntentOpts: {
                                interval: 500,
                                timeout: 0
                            },
                            showTip: function(box) {
                                if(!isAnimating) {
                                    $(box).show();
                                }
                            }
                        });
                    }

                    newpageOCR.removeClass('loading');
                },
                error: function (data) {
                    newpageOCR.removeClass('loading');
                    isAnimating = false;
                }
            });
        }

        function updatePageNames(index){
            pageNames.text("Loading...");
            pageNames.addClass('loading');
            $.ajax({
                type: 'get',
                url: '/pagesummary/',
                data: {
                    'op': 'GetPageNameList',
                    'pageID': pages[index].PageID
                },
                success: function (data, textStatus, jqXHR) {
                    if(data.length > 0) {
                        pageNames.empty();
                        $.each(data, function(index, name) {
                            var ubioLink = $('<span/>', { 'class' : 'ubio-links' }).append(
                                $('<a/>', { 
                                    'href' : '/name/' + name.UrlName,
                                    'text' : name.ResolvedNameString
                                })).appendTo(pageNames);

                            if(name.EOLID > 0) {
                                ubioLink.append(
                                    " <a href='http://www.eol.org/pages/" + name.EOLID + "'><img src='/images/eol_11px.png'></a>")
                            }
                        });
                    } else {
                        pageNames.empty();
                        pageNames.append($('<span/>', { 'text' : 'No Scientific Names found' }));
                    } 
                    pageNames.removeClass('loading');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    pageNames.empty();
                    pageNames.removeClass('loading');
                    isAnimating = false;
                }
            });
        }

        // Check for Enter in Search Inside text box
        $("#sibSearchText").keypress(function (event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (keycode == '13') $("#sibSearchBtn").click();
        });

        // Click binder for search button
        $("#sibSearchBtn").click(function () {
            var resultsBox = $(".sibResultsBox");
            $("#sibTextEcho").html($("#sibSearchText").val());

            resultsBox.html("Loading...");
            $("#searchResults").show();

            $.ajax({
                type: 'get',
                url: '/search/pages',
                data: 'q=' + encodeURIComponent($("#sibSearchText").val()) + '&itemId=' + <%: PageSummary.ItemID %>,
                success: function (data, textStatus, jqXHR) {
                    if (data.length > 0) {
                        $("#sibNumResults").html("(" + data.length + ")");
                        resultsBox.empty();
                        $.each(data, function (index, hit) {
                            // Build this for each hit
                            // <div class="sibResultBox">
                            //     <div><a href="#" class="sibResultPage">Page NN</a></div>
                            //     <div class="sibResultText">... hit text ...</div>
                            //     <div class="sibResultText">... hit text ...</div>
                            // </div>

                            var resultBox = $("<div/>", { "class": "sibResultBox" });
                            var resultPageDesc = $("<div/>")
                                .append($("<a/>", {
                                    "class": "sibResultPage",
                                    "href": "javascript:changePage(" + hit.Sequence + ")",
                                    "text": hit.PageDescription
                                }));
                            resultBox.append(resultPageDesc);

                            $.each(hit.Highlights, function (index2, highlight) {
                                resultBox.append($("<div/>", { "class": "sibResultText" }).append("... " + highlight.Item2 + " ..."));
                            });

                            resultBox.appendTo(resultsBox);
                        });
                    } else {
                        resultsBox.empty();
                        resultsBox.append($('<span/>', { 'text': 'No results found' }));
                    }
                    resultsBox.removeClass('loading');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    resultsBox.empty();
                    resultsBox.append($('<span/>', { 'text': 'Error!  Please try again.' }));
                    isAnimating = false;
                }
            });
        });

        // Create Modal Dialogs
        $('#download-dialog').jqm({
            onHide: onHideAction,
            trigger: '.downloadbook'
        });
        $('#review-dialog').jqm({
            toTop: true,
            onHide: onHideAction,
            onShow: function(hash) { 
                hash.w.show();
                $('.ellipsis').textOverflow();
            }
        });
        $('#generate-dialog').jqm({
            toTop: true,
            onHide: onHideAction
        });
        $(".buttondrop.download").click(function(){
            // $(".downloadcontents").slideToggle("fast"); 
            if ($(".downloadcontents").css("display") == "block") {
                $(".downloadcontents").slideUp("fast"); 
            } else {
                $(".downloadcontents").slideDown("fast"); 
            }
            
            $(document).mouseup(function (e){
                var container = $(".downloadcontents");
                //if (container.has(e.target).length === 0) {
                    $(".downloadcontents").slideUp("fast"); 
                    $(document).unbind("mouseup");
                //}
            });
        });
        $(".selectpages").click(function(){
            selectPagesToDownload();
        });
        $(".cancelpdf", pdfBar).click(function(){
            cancelSelectPages();
        });

        // Click Binder for PDF Review button
        var pdfReview = $('.review', pdfBar).click(function() {
            if(pdfPages.length > 0) {
                changePdfMode((pdfMode) ? pdfMode : PdfModeType.Icon, true);

                $('#review-dialog').jqmShow();
                $('#review-dialog .body').jScrollPane();
            }
        });

        // Click Binder for PDF Generate button
        var pdfGenerate = $('.generate', pdfBar).click(function() {
            if(pdfPages.length > 0) {
                $('#generate-dialog').jqmShow();
            }
        });        

        // Click Binder for PDF Generate button on Dialog
        $('#review-dialog .generate').click(function() {
            isModalDialogChange = true;
            $('#review-dialog').jqmHide();
            $('#generate-dialog').jqmShow();            
        });

        // Click Binder for PDF Review button on Dialog
        $('#generate-dialog .review').click(function() {
            isModalDialogChange = true;
            $('#generate-dialog').jqmHide();
            
            changePdfMode((pdfMode) ? pdfMode : PdfModeType.List, true);
            $('#review-dialog').jqmShow();
            $('#review-dialog .body').jScrollPane();
        });

        // Click Binder for list view on Review Dialog
        var pdfListLink = $('#review-dialog .list-view').click(function() {
            changePdfMode(PdfModeType.List);
            $('#review-dialog .body').data('jsp').reinitialise();
        });

        // Click Binder for icon view on Review Dialog
        var pdfIconLink = $('#review-dialog .icon-view').click(function() {
            changePdfMode(PdfModeType.Icon);
            $('#review-dialog .body').data('jsp').reinitialise();
        });

        // Binder for page dropdown list
        $('#lstPages').live("change", function(){
          br.jumpToIndex($('#lstPages').attr("selectedIndex"));
        });

        // Binder for segment list used by iDevices
        $('#lbSegments').live("change", function(){
            var pageNum = $('#lbSegments').attr("value"); 
            var pageIndex = br.getPageIndexWithPageNum(pageNum);

            if ('undefined' != typeof(pageIndex)) {
                var leafTop = 0;
                var h;
                br.jumpToIndex(pageIndex);
                $('#BRcontainer').attr('scrollTop', leafTop);
                return true;
            }
            // Page not found
            return false;
        });

        // Binders for non-iDevice segment list
        $(document).delegate("a.viewSegLinkTitle", "click", function() {
            return selectSeg(this);
        });

        function selectSeg(segLink)
        {
            var pageNum = $(segLink).attr("id"); 
            var pageIndex = br.getPageIndexWithPageNum(pageNum);

            highlightSeg(segLink);

            if ('undefined' != typeof(pageIndex)) {
                var leafTop = 0;
                var h;
                br.jumpToIndex(pageIndex);
                $('#BRcontainer').attr('scrollTop', leafTop);
                return true;
            }
            // Page not found
            return false;
        }

        function highlightSeg(segLink)
        {
            $("#lstSegments ul li").css("background-color", "#FFFFFF");
            $("#lstSegments ul li").css("color", "#444444");
            $("#lstSegments ul li a").css("color", "#444444");
            $("#lstSegments li .segListVolPage").css("color", "#000000");
            $(segLink).parent().css("background-color","Highlight");
            $(segLink).parent().css("color","HighlightText");
            $(segLink).css("color", "HighlightText");
            $(segLink).parent().children(".segListVolPage").css("color","#BFBFBF");
            $("#lbSegments").val($(segLink).attr("id"));
        }

        // Binder for "Show More" link
        $(document).delegate(".showmore a", "click", function() {
            var leftPanelHeight = $("#left-panel2").height();
            var tocHeight = $("#lstPages").height();
            var newHeight = leftPanelHeight;
            var showMore = ($(this).html() === "Show More");

            if (showMore) 
            {
                $(this).html("Show Less");
                newHeight = leftPanelHeight * 0.75;
                $("#names-container-div").hide();
            }
            else 
            {
                $(this).html("Show More");
                newHeight = leftPanelHeight / 4;
                $("#names-container-div").show();
            };

            $("#lstPages").height(newHeight);
            $("#lstPages").css("min-height", newHeight);
            $("#lstSegments").height(newHeight);
            $("#lstSegments ul").height(newHeight);
        });

        // Function used to change the way pdfs are viewed (list/icon) on the Review Dialog
        function changePdfMode (mode, redraw) {
            if(pdfMode != mode || redraw) {
                pdfMode = mode;
                $('#review-dialog ul').empty();

                $('.dicon').removeClass('active');
                if(pdfMode == PdfModeType.List) {
                    pdfListLink.addClass('active');
                } else if(pdfMode == PdfModeType.Icon) {
                    pdfIconLink.addClass('active');
                }

                $.each(pdfPages, function(index, pdfPageIndex) { 
                    var pdfPage;
                    var deletePage = $("<a/>", { 'class': 'delete', text: 'delete' }).click(function() {
                        //$('#ptb' + pdfPageIndex).hide().find('.add_to_pdf').click();
                        $('#ptb' + pdfPageIndex).trigger(pageToolBoxEvent);
                        $(this).parents('li').fadeOut(200, function() { 
                            $(this).remove(); 

                            if($('#review-dialog li').length == 0) {
                                $('#review-dialog').jqmHide();
                            }

                            $('#review-dialog .body').data('jsp').reinitialise();
                        });                        
                    });                    

                    if(mode == PdfModeType.List) {
                        var pageName = $('<span/>', { text : br.getPageName(pdfPageIndex) });
                        pdfPage = $('<li/>', { 'class' : 'listpage' }).append(pageName).append(deletePage);
                    } else if(mode == PdfModeType.Icon) {
                        var pageName = $('<span/>', { 'class' : 'ellipsis', text : br.getPageName(pdfPageIndex) });
                        var thumb = $('<img/>', { src: br.getPageURI(pdfPageIndex, 0, 100) });
                        var text = $('<div/>', { 'class' : 'text' }).append(pageName).append(deletePage);
                        pdfPage = $('<li/>', { 'class' : 'iconpage' }).append(thumb).append(text);
                    }

                    $('#review-dialog ul').append(pdfPage)
                });

                $('.ellipsis').textOverflow();
            }
        }

        var pdfGenerate = $('.generate', pdfBar);
        var pdfCounter = $('.counter', pdfBar);
        var pdfReviewCounter = $('#review-dialog #page-count');

        // Validation for Generate PDF Dialog
        function validatePdfForm() {
            var isValid = true;
            $('#tbEmailErr').html("");
            $('#tbTitleErr').html("");

            if ($.trim($("#tbEmail").val()) == "") {
                $('#tbEmailErr').html("Please enter an email address.");
                isValid = false;
            }

            var title = $.trim($("#tbTitle").val());
            var authors = $.trim($("#tbAuthors").val());
            var subjects = $.trim($("#tbSubjects").val());

            if (title.length == 0 && (authors.length > 1 || subjects.length > 1)) {
                $('#tbTitleErr').html("Please include a title.");
                isValid = false;
            }
            else if (title.length == 1) {
                $('#tbTitleErr').html("Please include a valid title.");
                isValid = false;
            }

            return (isValid);
        }

        // PDF Generation
        $('#generate-dialog .finish').click(function () {
            var finishButton = $(this);
            if(validatePdfForm() && !finishButton.hasClass('loading')) {                
                finishButton                    
                    .css('width', finishButton.width())
                    .addClass('loading');

                var pageIds = [];

                $.each(pdfPages, function(index, value) {
                    pageIds.push(pages[value].PageID);
                });
                
                $.ajax({
                    type: 'post',
                    url: '/generatepdf/',
                    data: {
                        'itemId': <%= PageSummary.ItemID %>,
                        'pages': pageIds.toString(),
                        'email': $('#tbEmail').val(),
                        'title': $('#tbTitle').val(),
                        'authors': $('#tbAuthors').val(),
                        'subjects': $('#tbSubjects').val(),
                        'imagesOnly': $('#tbImages:checked').val()
                    },
                    success: function (data) {
                        if(data.isSuccess) {
                            $('#pdfId').text(data.pdfId);

                            $('#generate-dialog .intro').fadeOut(function() {
                                $('#generate-dialog .success').fadeIn();
                                finishButton.removeClass('loading').removeAttr('style');
                            });

                            $('#generate-dialog .required, #generate-dialog .optional, #generate-dialog .finish').fadeOut(function() {
                                $('#generate-dialog').animate({ 'height': '330px' }, 200);
                            });

                            $('#generate-dialog .required, #generate-dialog .optional, #generate-dialog .review').fadeOut(function() {
                                $('#generate-dialog').animate({ 'height': '330px' }, 200);
                            });

                            // close off the PDF selection stuff.
                            cancelPdfSelection = true;

                        } else {
                            $('#pdfIdFail').text(data.pdfId);

                            $('#generate-dialog .required, #generate-dialog .optional, #generate-dialog .footer').fadeOut(function() {
                                $('#generate-dialog').animate({ 'height': '330px' }, 200);
                            });

                            $('#generate-dialog .intro').fadeOut(function() {
                                $('#generate-dialog .failure').fadeIn();
                                finishButton.removeClass('loading').removeAttr('style');
                            });
                        }
                    },
                    error: function() {
                            // just show the error page for now.
                            location.pathname = '/error';
                    }
                });
            }
        });

        // Return the width of a given page.  Here we assume all images are 1600 pixels wide
        br.getPageWidth = function (index) {
            if (pages && (index >= 0) && pages[index].Width) return pages[index].Width;
            return 1600;
        }

        // Return the height of a given page.  Here we assume all images are 2400 pixels high
        br.getPageHeight = function (index) {
            if (pages && (index >= 0) && pages[index].Height) return pages[index].Height;
            return 2400;
        }

        br.imagesBaseURL = '/images/';

        br.getPageURI = function (index, reduce, width) {
            var url = "";
            if (pages != null) {
                var calculatedWidth = (width) ? width : Math.floor(br.getPageWidth(index) / reduce);
                url = pages[index].ExternalBaseUrl + '/download/' + pages[index].BarCode + '/page/n' + (pages[index].SequenceOrder - 1) + '_w' + calculatedWidth;
            }

            return url;
        }

        // Return which side, left or right, that a given page should be displayed on
        br.getPageSide = function (index) {
            if (0 == (index & 0x1)) {
                return 'R';
            } else {
                return 'L';
            }
        }

        // This function returns the left and right indices for the user-visible
        // spread that contains the given index.  The return values may be
        // null if there is no facing page or the index is invalid.
        br.getSpreadIndices = function (pindex) {
            var spreadIndices = [null, null];
            if ('rl' == this.pageProgression) {
                // Right to Left
                if (this.getPageSide(pindex) == 'R') {
                    spreadIndices[1] = pindex;
                    spreadIndices[0] = pindex + 1;
                } else {
                    // Given index was LHS
                    spreadIndices[0] = pindex;
                    spreadIndices[1] = pindex - 1;
                }
            } else {
                // Left to right
                if (this.getPageSide(pindex) == 'L') {
                    spreadIndices[0] = pindex;
                    spreadIndices[1] = pindex + 1;
                } else {
                    // Given index was RHS
                    spreadIndices[1] = pindex;
                    spreadIndices[0] = pindex - 1;
                }
            }

            return spreadIndices;
        }

        br.bhlPageID = function(index){
         if (pages && (index >= 0) && pages[index].PageID) {             
                              
                return pages[index].PageID;         
            }
        }
        //function to update page URL, Names and OCR
        br.updatePageDetailsUI = function(index){
            var segListItem = $("#lstSegments li[id='" + pages[index].SegmentID + "']");
            var segTitleLink = $("#articleTitleLink");

            // Update the segment list
            var segTitle = $(segListItem).children("a.viewSegLinkTitle");
            if (segTitle != null) {
                segTitleLink.html(segTitle.html());
                segTitleLink.attr("href", "/part/" + pages[index].SegmentID);
            }
            highlightSeg(segTitle);

            // Update the Mendeley link
            if (pages[index].SegmentID != null)
            {
                if (updateMendeleyLink !== undefined) {
                    updateMendeleyLink('part', pages[index].SegmentID);
                }
            }
            else
            {
                if (updateMendeleyLink !== undefined) {
                    updateMendeleyLink('item', '<%: CurrentItemID %>');
                }
            }

            // Update the Altmetric badge
            $(".altmetric-embed").attr("data-uri", "https://www.biodiversitylibrary.org/item/" + "<%: CurrentItemID %>");
            if (typeof _altmetric_embed_init === 'function') _altmetric_embed_init();

            // Update page URL and names
            $("#currentpageURL").text("https://biodiversitylibrary.org/page/" + pages[index].PageID);
            $("#currentpageURL").attr("href", "https://biodiversitylibrary.org/page/" + pages[index].PageID);
            var currentFlickrUrl = $("#currentFlickrURL");
            var flickrUrlSpan = $("#flickrurlspan");
            if (pages[index].FlickrUrl == "")
            {
                flickrUrlSpan.toggle(false);
                currentFlickrUrl.attr("href", "#");
            }
            else
            {
                currentFlickrUrl.attr("href", pages[index].FlickrUrl);
                flickrUrlSpan.toggle(true);
            }

            addthis_share = { 
                url: "https://biodiversitylibrary.org/page/" + pages[index].PageID,
                title: $(document).find("title").text()
            }

            var showOCRButton = $('#showOCRButton'); 
            if (showOCRButton.attr("title") == "Hide OCR") {
                updateOCR(index);   // Update OCR if it is visible
            }
            updatePageNames(index);
            $("#pagename").text(br.getPageName(br.currentIndex()));
            if (typeof renderAnnotations == "function") renderAnnotations(index+1);
        }  
        
        // For a given "accessible page index" return the page number in the book.
        //
        // For example, index 5 might correspond to "Page 1" if there is front matter such
        // as a title page and table of contents.
        br.getPageNum = function (index) {
            return index + 1;
        }
        
        br.getPageIndexWithPageNum = function (pageNum) {
            if (pages) {
                for (var i=0; i<pages.length; i++) {
                    if (pages[i].PageID == pageNum) {
                        return i;
                    }
                }
            }
            return -1;
        }

        // Return the index number for the leaf
        br.leafNumToIndex = function (leaf) {
            return leaf - 1;
        }

        // Get a page name given a certain page index
        br.getPageName = function (index) {
            if (pages && (index >= 0) && pages[index].WebDisplay) {
                return pages[index].WebDisplay;
            }
        }

        // Function used by book reader to create the pagetoolbox
        br.getPageToolbox = function (index) {
            var pageToolbox = $("<div/>", { 'class': 'pagetoolbox', 'id': 'ptb' + index }).bind(pageToolBoxEvent, function(event) {
                var origBG = '#404040'
                var origActiveBG = '#455667';
                var activeBG = '#ffa200';
                var pdfPageCount;
                var startIndex;
                var endIndex;

                if (event.shiftKey && lastPdfIndex !== -1) {
                    // Select multiple pages
                    if (index < lastPdfIndex) 
                        { startIndex = index; endIndex = lastPdfIndex - 1; }
                    else 
                        { startIndex = lastPdfIndex + 1; endIndex = index; }

                    for (x = startIndex; x <= endIndex; x++)
                    {
                        pdfPageIndex = $.inArray(x, pdfPages);

                        // Select/Deselect a single page
                        if(pdfPageIndex < 0) {
                            pdfPageCount = pdfPages.push(x);
                            $('#ptb' + x).addClass('selected').attr('bt-xtitle', 'Remove from My PDF');

                            if(!pdfBar.hasClass('active')) {
                                pdfBar.removeClass('disabled').addClass('active').fadeTo(200, 1);
                            }
                        }
                    }
                    lastPdfIndex = index;
                }
                else {
                    pdfPageIndex = $.inArray(index, pdfPages);

                    // Select/Deselect a single page
                    if(pdfPageIndex < 0) {
                        pdfPageCount = pdfPages.push(index);
                        pageToolbox.addClass('selected').attr('bt-xtitle', 'Remove from My PDF');

                        if(!pdfBar.hasClass('active')) {
                            pdfBar.removeClass('disabled').addClass('active').fadeTo(200, 1);
                        }
                        lastPdfIndex = index;
                    } else {
                        pdfPageCount = pdfPages.remove(pdfPageIndex);
                        pageToolbox.removeClass('selected').attr('bt-xtitle', 'Add to My PDF');
                        lastPdfIndex = -1;
                    }
                }

                // Re-sort pdfs
                pdfPages.sort(function (a, b){ return (a-b); });

                pdfCounter.stop(true, true).animate({ backgroundColor : activeBG }, 100, 'easeOutQuad', function() {
                    if(pdfPageCount <= 0) {
                        pdfCounter.text('No Pages Added');
                        pdfBar.removeClass('active').addClass('disabled'); //.fadeTo(200, 0.5);
                    } else if(pdfPageCount == 1) {
                        pdfCounter.text(pdfPageCount + ' Page Added');
                        pdfReviewCounter.text(pdfPageCount + ' Page');
                    } else {
                        pdfCounter.text(pdfPageCount + ' Pages Added');
                        pdfReviewCounter.text(pdfPageCount + ' Pages');
                    }                        
                }).animate({ backgroundColor : (pdfPageCount <= 0) ? origBG : origActiveBG }, 400, 'easeOutQuad');

                // Prevent event propagating to dragscrollable
                event.stopPropagation();
            });

            var isAnimating = false;

            var pdfPageIndex = $.inArray(index, pdfPages);
            // Page has already been added to pdfPages
            if(pdfPageIndex >= 0) {
                pageToolbox.addClass('selected').css('display', 'block'); // .addClass('active')
            }

            // Hard coded dimensions due to inability to ensure correct dimensions
            pageToolbox.data('info', {
                index: index,
                origWidth: 230,
                origHeight: 70
            });

            return pageToolbox;
        }

        // Create BeautyTips if canvas exists & not IE (results in singlepage view scrolling to top)
        if(Modernizr.canvas && !$.browser.msie) {
            $('.dicon, .jqmClose').bt({
                padding: 10,
                spikeLength: 10,
                spikeGirth: 13,
                fill: '#266099',
                strokeWidth: 0,
                shrinkToFit: true,
                positions: ['most'],
                overlap: -1,
                cssStyles: { color: '#fff' },
                hoverIntentOpts: {
                    interval: 500,
                    timeout: 0
                }
            });
        }

        br.numLeafs = <%: PageCount %>;
        br.bookTitle = '';
        br.imagesBaseURL = '/images/';
        br.titleLeaf = <%: StartPage %>;

        BookReader.prototype.addPageToolBox = function (index, page) {
            var pageToolbox = this.getPageToolbox(index);
            pageToolbox.appendTo(page);
        }
        BookReader.prototype.add2upPageToolBox = function (index, page) {
            return;
        }

        BookReader.prototype.scrollDown = function() {
            br.next();
        }
        BookReader.prototype.scrollUp = function() {
            br.prev();
        }

        // Bookviewer crashes ie6, dont even attempt to load it
        if (!$('html').is('.ie6')) {
            br.init();

            var BRtoolbar = $("#BRtoolbar").detach();
            BRtoolbar.appendTo("#BRtoolbarwrapper");
            $('.BRtoolbar-container').append("<div id='BRtoolbar-extra'><a class='BRicon page_print' title='Print'>Print</a><a id='showSearchButton' title='Search Inside' class='BRButton BRButtonFeatured'>Search Inside</a><a id='showAnnotationsButton' class='BRButton' title='Show Annotations'>Show<br/>Annotations</a><a id='showOCRButton' class='BRButton' title='Show OCR'>Show<br/>OCR</a> </div>");
            var PDFtoolbar = $("#toolbar-top").detach();
            PDFtoolbar.prependTo("#BRtoolbar");
            $('#BRtoolbar').prepend("<div><a id='showPagesButton' class='BRicon book_leftmost' style='display: block;' title='Hide Pages'>Hide Pages</a></div>");
            var AnnotationBox = $("#AnnotationBox").detach();
            AnnotationBox.appendTo("#right-panel-content");

            if (!<%=System.Configuration.ConfigurationManager.AppSettings["UseElasticSearch"].ToLower()%>) $("#showSearchButton").hide();

            // Print page 
            var printPageButton = $('.page_print');
            printPageButton.click(function () {
                $('<img/>', { src: br.getPageURI(br.currentIndex()) })
                    .printElement({
                        printMode: 'popup',
                        printBodyOptions: {
                            classNameToAdd: 'printPopup'
                        },
                        "leaveOpen": true,
                        pageTitle: '<%: PageSummary.ShortTitle %>' + ((pages[br.currentIndex()].WebDisplay) ? ' - ' + pages[br.currentIndex()].WebDisplay : '')
                    });
            });

            // Toggle left hand container for Pages 
            var showPagesButton = $('#showPagesButton');
            showPagesButton.click(function () {
                $("#left-panel2").toggle("fast", function () { if (br.mode == 3) { br.resizePageView(); } br.centerPageView(); });
                if (showPagesButton.attr("title") == "Show Pages") {
                    showPagesButton.attr("title", "Hide Pages");
                    showPagesButton.text("Hide Pages");
                    showPagesButton.toggleClass("book_leftmost", true).toggleClass("book_rightmost", false);
                } else {
                    showPagesButton.attr("title", "Show Pages");
                    showPagesButton.text("Show Pages");
                    showPagesButton.toggleClass("book_leftmost", false).toggleClass("book_rightmost", true);
                }
            });

            // Toggle right hand container for OCR 
            var showOCRButton = $('#showOCRButton');
            showOCRButton.click(function () {
                newpageOCR.text("");
                if (showOCRButton.attr("title") == "Show OCR") {
                    updateOCR(br.currentIndex());
                    $("#right-panel2").show("fast", function () { if (br.mode == 3) { br.resizePageView(); } br.centerPageView(); });
                    $("#pageOCR-panel").show();
                    showOCRButton.attr("title", "Hide OCR");
                    showOCRButton.html("Hide<br/>OCR");
                    showOCRButton.addClass("displayed");
                } else {
                    $("#right-panel2").hide("fast", function () { if (br.mode == 3) { br.resizePageView(); } br.centerPageView(); });
                    showOCRButton.attr("title", "Show OCR");
                    showOCRButton.html("Show<br/>OCR");
                    showOCRButton.removeClass("displayed");
                }

                resetAnnotationsBox();
                resetSearchBox();
            });

            // Toggle right hand container for Search 
            var showSearchButton = $('#showSearchButton');
            showSearchButton.click(function () {
                //newpageOCR.text("");
                if (showSearchButton.attr("title") == "Search Inside") {
                    //updateOCR(br.currentIndex()); 
                    $("#right-panel2").show("fast", function () { if (br.mode == 3) { br.resizePageView(); } br.centerPageView(); });
                    $("#pageSearch-panel").show();
                    $("#sibSearchText").focus();
                    showSearchButton.attr("title", "Hide Search");
                    showSearchButton.html("Hide Search");
                    showSearchButton.addClass("displayed");
                } else {
                    $("#right-panel2").hide("fast", function () { if (br.mode == 3) { br.resizePageView(); } br.centerPageView(); });
                    showSearchButton.attr("title", "Search Inside");
                    showSearchButton.html("Search Inside");
                    showSearchButton.removeClass("displayed");
                }

                resetPageOCRBox();
                resetAnnotationsBox();
            });

            // Toggle right hand container for Annotations
            var showAnnotationsButton = $("#showAnnotationsButton");
            showAnnotationsButton.click(function () {
                if (showAnnotationsButton.attr("title") == "Show Annotations") {
                    $("#right-panel2").show("fast", function () { if (br.mode == 3) { br.resizePageView(); } br.centerPageView(); });
                    $("#AnnotationBox").show();
                    showAnnotationsButton.attr("title", "Hide Annotations");
                    showAnnotationsButton.html("Hide<br/>Annotations");
                    showAnnotationsButton.addClass("displayed");
                } else {
                    $("#right-panel2").hide("fast", function () { if (br.mode == 3) { br.resizePageView(); } br.centerPageView(); });
                    showAnnotationsButton.attr("title", "Show Annotations");
                    showAnnotationsButton.html("Show<br/>Annotations");
                    showAnnotationsButton.removeClass("displayed");
                }

                resetPageOCRBox();
                resetSearchBox();
            });

            if ("<%=HasAnnotations.ToLower()%>" === "false") {
                showAnnotationsButton.hide();
            } else {
                showAnnotationsButton.trigger('click');
                window.setTimeout("br.centerPageView()", 500);
            }

            updateUIHeights(); 
        }

        $("#BRcontainer").bind('scroll', this, function (e) {
            if (self.mode != self.constMode2up) {
                e.data.loadLeafs();
            }
            $('.pagetoolbox').each(function () {
                if (actionMode == actionModeType.Select) {
                    $(this).show();
                }
            })
        });

    });

    $(window).bind('resize', this, function (e) {
        var rightPanelHeight = $("#right-panel2").height();
        var pageOCRHeaderHeight = $("#pageOCR-panel .header").height();
        var pageReaderCommentsHeaderHeight = $("#pageReaderComments-panel .header").height();
        updateUIHeights(); 
    });

    function updateUIHeights() {
        var leftPanelHeight = $("#left-panel2").height();
        var pagesPanel = $("#lstPages").outerHeight();
        var namesPanel = $("#names-panel").outerHeight();
        var topLeftPanelHeight = $("#left-panel2 .left-panel-boxes:first-child").outerHeight();
        var bottomLeftPanelHeight = $("#left-panel2 .left-panel-boxes:last-child").outerHeight();

        var topTotalHeight = 0; 
        $("#left-panel2 .left-panel-boxes:first-child").children().each(function(){
            topTotalHeight = topTotalHeight + $(this).outerHeight();
        });
        topTotalHeight = topTotalHeight-pagesPanel;

        var bottomTotalHeight = 0; 
        $("#left-panel2 .left-panel-boxes:last-child").children().each(function(){
            bottomTotalHeight = bottomTotalHeight + $(this).outerHeight();
        });
        bottomTotalHeight = bottomTotalHeight-namesPanel;

        var lphh = (leftPanelHeight)/4;  // -topLeftPanelHeight-bottomLeftPanelHeight
        var lcpd = (topTotalHeight - bottomTotalHeight)/2; 

        if (leftPanelHeight < 720) {
            topTotalHeight = 0;
            bottomTotalHeight = 0; 
        } else {
            //topTotalHeight += 10;
            //bottomTotalHeight += 10; 
        }

        if (topTotalHeight >= bottomTotalHeight) {
            finalHeight = topTotalHeight; 
        } else {
            finalHeight = bottomTotalHeight; 
        }
        finalHeight += lphh;

        $("#names-panel").height(leftPanelHeight/3);
        var isiPad = navigator.userAgent.match(/(iPad)|(iPhone)|(iPod)/i) != null;
        if (!isiPad) {
            var newHeight = leftPanelHeight/4;
            var showMore = ($(".showmore a").html() === "Show Less");   // current state of list boxes
            if (showMore)
            {
                var newHeight = leftPanelHeight * 0.75;
            }
            $("#lstPages").height(newHeight);
            $("#lstPages").css("min-height", newHeight);
            $("#lstSegments").height(newHeight);
            $("#lstSegments ul").height(newHeight);
        }

        $("div.text span").height(leftPanelHeight-($("#BRtoolbarwrapper").height()+$("div.header").height())); 
    }

    function resetAnnotationsBox() {
        $("#AnnotationBox").hide();
        $("#showAnnotationsButton").attr("title", "Show Annotations");
        $("#showAnnotationsButton").html("Show<br/>Annotations");
        $("#showAnnotationsButton").removeClass("displayed");
    }

    function resetSearchBox() {
        $("#pageSearch-panel").hide();
        $("#showSearchButton").attr("title", "Search Inside");
        $("#showSearchButton").html("Search<br/>Inside");
        $("#showSearchButton").removeClass("displayed");
    }

    function resetPageOCRBox() {
        $("#pageOCR-panel").hide();
        $("#showOCRButton").attr("title", "Show OCR");
        $("#showOCRButton").html("Show<br/>OCR");
        $("#showOCRButton").removeClass("displayed");
    }

    function fixIEDisplayIssue() {
        var pageWidth = $('.BRpagedivthumb img').css('width');
        var pageHeight = $('.BRpagedivthumb img').css('height');

        if ($.browser.msie && (parseInt($.browser.version, 10) === 8 || parseInt($.browser.version, 10) === 7)) {
            for (var i = 0; i < $('.BRpagedivthumb img').length; i++) {
                $('.BRpagedivthumb img').width(pageWidth);
                $('.BRpagedivthumb img').height(pageHeight); 
            }
        }
    }

    $(document).ready(function(){
        	    //	Called when we click on the tab itself
	    $('#pagestab').click(function() {

            if ($('#pagestab').hasClass('active')) return false; 

		    $('#pagestabcontent').show();
            $('#segmentstabcontent').hide();
            $('#pagestab').toggleClass("active");
            $('#segmentstab').toggleClass("active");

		    //	Do nothing when tab is clicked
		    return false;
	    });
	    //	Called when we click on the tab itself
	    $('#segmentstab').click(function() {
        
            if ($('#segmentstab').hasClass('active')) return false; 

		    $('#pagestabcontent').hide();
            $('#segmentstabcontent').show();
            $('#pagestab').toggleClass("active");
            $('#segmentstab').toggleClass("active");

		    //	Do nothing when tab is clicked
		    return false;
	    });
        var segmentCount = <%: SegmentCount %>; 
        if (segmentCount == 0) {
            $('#segmentstab').hide();
        }
    });

//]]>
</script>

<script type="text/javascript">
<!--
    var pageSequence = [<asp:Literal ID="ltlPageSequence" runat="server"></asp:Literal>];   <%--List of references to annotated pages in sequence; --%>

    //initialize annotation box
    var $_AnnotationBox = $('#AnnotationBox'),
        //$_toggleAnnotationBox = $('#AnnotationBox #toggleAnnotationBox'),
        $_hide_annotations = $('#AnnotationBox #hide-annotations'),
        $_stub = $('#AnnotationBox #stub'),
        $_show_annotations = $('#AnnotationBox #show-annotations'),
        $_related_annotations = $('#related-annotations'),
        $_annotationBoxContent = $('#AnnotationBox #content'),
        $_noAnnotationsStub = $('#no-annotations-stub');

    //initialize legend box
    var $_toggle_legend = $('#toggle_legend'),
        $_hide_legend = $('#hide_legend'),
        $_show_legend = $('#show_legend'),
        $_legend_items = $('#legend-items'),
        $_copyright = $('#AnnotationBox #copyright'),
        $_phantom_legend = $('#phantom-legend');

    //set show/hide for legend
    //use phantom_legend to keep rest of Annotation Box content evenly spaced
    $_toggle_legend.click(function() {
        if ($_phantom_legend.height() > 0)
            $_phantom_legend.height($_legend_items.outerHeight());
        $_legend_items.toggle(50, function() {
            $_phantom_legend.toggle(0);
            $_hide_legend.toggle();
            $_show_legend.toggle();
        });  
    });

    function changePage(index)
    {
        br.jumpToIndex(index-1); //Page Annotation index starts at 1, not zero so need to remove one.
    }

    function toggleAnnotationBody()
    {
        $_hide_annotations.toggle();
        $_AnnotationBox.toggleClass('active');
        $_show_annotations.toggle();
    }

    function setActive()
    {
        $_AnnotationBox.toggleClass('active', true);
        $_AnnotationBox.toggleClass('inactive', false);
        $_annotationBoxContent.show();
        $_noAnnotationsStub.hide();
    }

    function setInactive()
    {
        $_AnnotationBox.toggleClass('inactive', true);
        $_AnnotationBox.toggleClass('active', false);
        $_annotationBoxContent.hide();
        $_noAnnotationsStub.show();
    }

    function renderAnnotations(newSequence)
    {
        //Set Annotation Box
        var notes = $("#AnnotationBox #notes");

        //set notes
        var _newVal = $("#pageAnnotations_" + newSequence).html();
        if (_newVal) 
        {
            notes.html(_newVal);
            setActive();
        }
        else
        {
            setInactive();
        }

        set_scrollers(newSequence);
    }

    function set_scrollers(newSequence)
    {              
        /********************************************************************************
        * sets controls for going to first available annotated page in either direction *
        * If user has browsed to an unannotated page, have to determine where that page *
        * lies in the sequence, to determine its adjacent annotated pages. If we've     *
        * browsed to a page outside of the sequence, relabel the the controls to either *
        * "first" or "last"                                                             *
        ********************************************************************************/
        var aPrev = $("#AnnotationBox #pageScroller #aPrev"),
            aNext = $("#AnnotationBox #pageScroller #aNext");

        pIndex = map_sequence(newSequence, pageSequence);

        if (pIndex[0] >= 0) 
        {
            aPrev.html("<span class='pagescrollerarrow'>&laquo;</span>" + (pIndex[0] == (pageSequence.length - 1) ? "last marked page" : "previous marked page"));

            aPrev.attr('href', 'javascript:changePage(' + (pageSequence[pIndex[0]]) + ');');
            if (aPrev.is(':hidden')) 
            {
                aPrev.show();
            }
        }
        else              //no previous annotation, disable control
            aPrev.hide();

        if (pIndex[1] >= 0)
        {
            aNext.html((pIndex[1] == 0 ? "first marked page" : "next marked page") + "<span class='pagescrollerarrow'>&raquo;</span>");
            aNext.attr('href', 'javascript:changePage(' + (pageSequence[pIndex[1]]) + '); ');
            if (aNext.is(':hidden')) 
            {
                aNext.show();
            }
        }
        else              //no next annotation, disable control
            aNext.hide();    
    }
        
    function onRelatedItemHover(id)
    {
        return;
        // find containing page block and extract id for display
        var $_relatedAnnotation = $('#Annotation_' + id);
        var $page = $_relatedAnnotation.parentsUntil('#AnnotationRepository');
        if ($page.attr('id'))
            changePage($page.attr('id').replace("pageAnnotations_", ''));
        else
        {
            var $_relatedItem = $('#related-item' + id);
            var pos = $_relatedItem.offset();
            var $_not_avail = $('#annotation-not-available');
            $_not_avail.offset({top:pos.top - 35, left:pos.left});
            $_not_avail.show();
        }
    }

    function onRelatedItemBlur()
    {
        return;
        var $_not_avail = $('#annotation-not-available');
        $_not_avail.hide();
    }

    function onRelatedItemClick(id)
    {
        // find containing page block and extract id for display
        var $_relatedAnnotation = $('#Annotation_' + id);
        var $page = $_relatedAnnotation.parentsUntil('#AnnotationRepository');
        if ($page.attr('id'))
            changePage($page.attr('id').replace("pageAnnotations_", ''));
    }

    function map_sequence(x, set)
    {
        var left = 0, right = set.length - 1;
        if (x < set[left]) return new Array(-1, left);
        if (x > set[right]) return new Array(right, -1);
        if (x == set[left]) return new Array(-1, right == 0 ? -1 : 1);
        if (x == set[right]) return new Array(right == 0 ? -1 : right - 1, -1);

        for (y=0; y<=right; y++)
        {
            var start = y - 1, end = -1;
            if (x < set[y]) end = y;
            if (x == set[y]) end = y + 1;
            if (end != -1) return new Array(start, end);
        }
    }
-->
</script>
<script type="text/javascript">
<!--
    function toggleSubjectSection(id) {
        var subjItems = Array($('#subject-section-' + id),
                                $('#show-subjects' + id),
                                $('#hide-subjects' + id));
        for (i in subjItems) {
            subjItems[i].toggle();
        }
    }

    function toggleConceptSection(id) {
        var subjItems = Array($('#concept-section-' + id),
                                $('#show-concepts' + id),
                                $('#hide-concepts' + id));
        for (i in subjItems) {
            subjItems[i].toggle();
        }
    }
-->
</script>
</asp:content>
