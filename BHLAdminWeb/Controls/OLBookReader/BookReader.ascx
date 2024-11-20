<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookReader.ascx.cs" Inherits="MOBOT.BHL.AdminWeb.Controls.OLBookReader.BookReader" %>
<style type="text/css">
        #AnnotationBox
        {
            border:thin solid #000000;
            font-size:12px;
            font-family:Trebuchet MS;
            padding:2px;position:relative;float:right;background-color:White;left:-2.4%;
            display:none;
            overflow:hidden;
            height:auto;
            width:300px;            
            opacity:1.00;
         }
         
         #page-characteristics
         {
             color:purple;
             background-color:transparent;
             font-family:Trebuchet MS;
             font-size:10px;
             font-style:italic;
             margin:2px;
             padding:2px;
         }
         
        #no-annotations-stub
        {
            text-align:center;
            /*top:5.5%;*/
        }
        
         .active
         {
             height:90% !important;/**/
         }
        
         .inactive
         {
             height:auto !important;/**/
         }
         
         .rolledUp
         {
            height:auto;
         }
         
         #AnnotationBox #content
         {
            height:69%;
            padding-bottom:0px;
         }
         
         #AnnotationBox #content #header
         {
             background-repeat:no-repeat;
             color:#FFFFFF;
             font-size:large;
             text-align:center;
             width:300px;
             height:70px;
             vertical-align:bottom;
             padding:0px;
             margin:0px;
         }
         

         #legend-items
         {
             background-color:#CCCCCC;
             padding:1px;
             vertical-align:top;
             height:auto;
             font-size:10px;
         }
         
        #legend-items .left-column
        {
            padding-left:3px;
            font-size:16px;
        }
        
        #legend-items .right-column
        {
            padding-left:0px;
            font-size:16px;
        }
        
        #legend-items .special-char
        {
            font-size:16px;
        }
        
        #toggle_legend {font-size:10px;}
        
        .page-header {font-size:12px; font-style:normal;}
         
         #AnnotationBox #content #indicator
         {
             font-size:12px;
             color:#AAAAAA;
             border-bottom:thin inset;
         }
         
         #AnnotationBox #notes
         {
             height:93%;   
             clear:both;
             font-style:italic;
             font-size:11px;
             
             overflow:auto; /* Added back in by MWL */
         }
         
         #phantom-legend
         {
             display:none;             
         }
         
         #AnnotationBox #copyright
         {
             background-color:transparent;
             border-top:solid thin #000000;
             text-align:center;
             margin-top:1px;
             /*height:10px;*/
             height:auto;   /* Added back in by MWL */
             font-size:12px;
         }
         
         #AnnotationBox #pageScroller
         {
             text-align:center;
             font-size:10px;
         }
         
         #AnnotationBox #toggleAnnotationBox
         {
             float:left;
         }
         
         #AnnotationBox #stub
         {
             text-align:center;
             font-variant:small-caps;
             font-weight:bold;             
         }
         
         .separator {border-top:thin thin #000000;clear:both;}
         
         .toggler
         {
             background:url('images/up.gif');
             width:11px;
             height:7px;
         }
         
         .toggler .rolledUp
         {
             background:url('images/down.gif');
         }
        
        .countup {font-style: normal;}
        
        .line-count {font-weight:normal; margin-bottom:2px; }
        
        .line-position {background-color:transparent;}
        
        .page-data {display:none;} 
        
        .subject-list, .concept-list
        {
            padding-top:3px;
            padding-bottom:3px;
            margin-top:2px;
            margin-bottom:2px;
            font-size:10px;
            font-style:normal;
        }
        
        .subject-list .title, .concept-list .title, #related-annotations
        {
            font-style:italic;
            font-size:10px;
        }
        
        .subject-list .target-section, .concept-list .target-section, #related-annotations
        {
            font-variant:small-caps;
            font-weight:bolder;
            padding-top:2px;
            margin-top:2px;
        }
        
        .subject-item, .concept-item, .related-annotation-item
        {
            font-variant:normal;
            font-weight:normal;
            font-style:normal;
        }
        
        .target-section
        {
            /*display:none;*/
        }
        
        .tnote
        {
            font-family:Trebuchet MS;
            margin-top:5px;
            cursor:default;
        }

        .tnote-hover 
        {
            color:blue;
            font-style:italic;
        }
        
        .tnote-ref
        {
            font-size:10px;
        }
        
        .pgcharvolume 
        {
            display:none;
            color:Red;
        }
        
        .word-block {color:#000000; border-bottom:1px; font-style:normal; font-size:12px}
        .faint {color:#000000;}
        
        #annotation-not-available
        {
            position:absolute;
            background:#CCCCCC;
            font-weight:bold;
            padding:10px;
            border:1px solid #000000;
            display:none;
        }
</style>

<div id="BookReader" style="left:0px; right:0px; top:0px; bottom:0em;">
</div>
<div id="output"></div>
<asp:PlaceHolder ID="plhAnnotations" runat="server" Visible="false">
<div id="AnnotationBox" class="active">
    <div id="toggleAnnotationBox">                                                              <%--header--%>
        <img id="hide-annotations" src="../Images/up.gif" alt="hide"/>
        <img id="show-annotations" src="../Images/down.gif" alt="show" style="display:none"/>
    </div>
    <div id="no-annotations-stub" style="display:none;">No Marks</div>                    <%--display when no Annotations--%>
    <div id="stub" style="display:none;">Marks</div>                                      <%--display when Annotation Box is collapsed--%>
    <div id="pageScroller">
        <a id="aPrev" href="javascript:void(0);" title="Previous">&laquo;previous</a>&nbsp;&nbsp;&nbsp;&nbsp;<a id="aNext" href="javascript:void(0);" title="Next">next&raquo;</a>
    </div>
    <div id="content">
        <div id="header">
            <a href="/collection/darwinlibrary" title="Collection"><img src="/images/darwinHeaderSmall.jpg" alt="Charles Darwin's Library"/></a>
        </div>
        <div id="indicator"><asp:Literal ID="ltlBookIndicator" runat="server" Text=""></asp:Literal><span style="float:right"><a href="/browse/collection/darwinlibrary" title="Books">Book List</a>&nbsp;&nbsp;<a href="/collection/darwinlibrary">Help</a></span></div>
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
                        <span style="padding-right:10px;">deleted</span>
                        <span class="special-char">&#65517;</span> <span style="padding-right:10px;">pinholes visible</span>
                        <span class="special-char">&#10719;</span> <span>line connects passages</span>
                    </div>
                    <div>
                        <span class="left-column">&#171;</span>
                        <span class="right-column">&#187;</span>
                        <span style="padding-right:6px;">inserted</span>
                        <span class="special-char">&#8657;</span> <span style="padding-right:10px;">count up from last line</span>
                        <span class="special-char">&#247;*</span> <span>long division</span>
                    </div>
                </div>
            </div>

        <div id="notes"></div>
        <div id="phantom-legend">&nbsp;</div>                                                   <%--handle offset when box is collapsed--%>
        <div id="copyright">Edition of <i>Charles Darwin's Reading Notes</i><br />by Di Gregorio & Gill<br />(Darwin Manuscripts Project: <a href="http://darwin.amnh.org" title="AMNH" target="_blank">darwin.amnh.org</a>)</div>
    </div>
</div>
<div id="annotation-not-available">Annotation Not Available</div>
    <asp:Literal ID="ltlAnnotationContent" runat="server"></asp:Literal>
    <script type="text/javascript">
    <!--
        var pageSequence = [<asp:Literal ID="ltlPageSequence" runat="server"></asp:Literal>];   <%--List of references to annotated pages in sequence; --%>

        //initialize annotation box
        var $_AnnotationBox = $('#AnnotationBox'),
            $_toggleAnnotationBox = $('#AnnotationBox #toggleAnnotationBox'),
            $_hide_annotations = $('#AnnotationBox #hide-annotations'),
            $_stub = $('#AnnotationBox #stub'),
            $_show_annotations = $('#AnnotationBox #show-annotations'),
            $_related_annotations = $('#related-annotations'),
            $_annotationBoxContent = $('#AnnotationBox #content'),
            $_noAnnotationsStub = $('#no-annotations-stub');

            //set show/hide for Annotation Box
            $_toggleAnnotationBox.click(
                                        function(){
                                                    $_annotationBoxContent.toggle(50, toggleAnnotationBody());
                                                  }
                                       );    
        
        //initialize legend box
        var $_toggle_legend = $('#toggle_legend'),
            $_hide_legend = $('#hide_legend'),
            $_show_legend = $('#show_legend'),
            $_legend_items = $('#legend-items'),
            $_copyright = $('#AnnotationBox #copyright'),
            $_phantom_legend = $('#phantom-legend');

            //set show/hide for legend
            //use phantom_legend to keep rest of Annotation Box content evenly spaced
            $_toggle_legend.click(
                                    function(){
                                                if ($_phantom_legend.height() > 0)
                                                    $_phantom_legend.height($_legend_items.outerHeight());
                                                $_legend_items.toggle(50,
                                                                        function()
                                                                        {
                                                                            $_phantom_legend.toggle(0);
                                                                            $_hide_legend.toggle();
                                                                            $_show_legend.toggle();
                                                                        }
                                                                      );  
                                            }
                                  );
/*
        var CUTOFF = $(window).height() - 100;
        window.onresize=function(){
            window.status = $(window).height() + ", " + CUTOFF;
            if ($(window).height() < CUTOFF)
            {
                $_annotationBoxContent.css("overflow","auto");
            }
            else
            {
                $_annotationBoxContent.css("overflow","visible");
             }
        };
*/
        function toggleAnnotationBody()
        {
            $_hide_annotations.toggle();
            $_AnnotationBox.toggleClass('active');
            //$_stub.toggle();
            $_show_annotations.toggle();
        }

        function setActive()
        {
            $_AnnotationBox.toggleClass('active', true);
            $_AnnotationBox.toggleClass('inactive', false);
            $_annotationBoxContent.show();
            $_toggleAnnotationBox.show();

            $_noAnnotationsStub.hide();
        }

        function setInactive()
        {
            $_AnnotationBox.toggleClass('inactive', true);
            $_AnnotationBox.toggleClass('active', false);
            $_annotationBoxContent.hide();
            $_toggleAnnotationBox.hide();

            $_noAnnotationsStub.show();
        }

        function renderAnnotations(newSequence)
        {
            //all the annotation content has been written to the page on Page Load, and hidden
            //retrieve appropriate section by newSequence

            if ($("#AnnotationBox").css("display") == "none")
            {
                $("#AnnotationBox").css("top", $("#BRtoolbar").height() + 1).css("display", "block");
            }

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
                  aPrev.html("&laquo;" + (pIndex[0] == (pageSequence.length - 1) ? "last marked page" : "previous marked page"));

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
                    aNext.html((pIndex[1] == 0 ? "first marked page" : "next marked page") + "&raquo;");
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

</asp:PlaceHolder>
<script type="text/javascript">

    // Set the parameters for the page
    var qsParm = new Array();
    qsParm["objectType"] = "<%= ObjectType%>";
    qsParm["objectId"] = "<%= ObjectID %>";
    qsParm["pgs"] = "<%= NumPages %>";
    qsParm["fixedWidth"] = "<%= FixedImageWidth %>";
    qsParm["fixedHeight"] = "<%= FixedImageHeight %>";
    if (!loadIE6Viewer()) {

        // Create the BookReader object
        br = new BookReader();
        
        // Make AJAX call here to get an object containing page information
        var pages = null;
        if (qsParm["objectId"] != "") {
            var operation = "PageSummarySelectForViewerByItemID";
            if (qsParm["objectType"] == "Segment") operation = "PageSummarySelectForViewerBySegmentID";
            $.getJSON("/Services/PageSummaryService.ashx?op=" + operation + "&itemID=" + qsParm["objectId"],
                function (data) {
                    pages = data;
                    InitializeViewer(<%= StartPage %>);
                }
            );
        }
    }

    // If IE6, notify user that a more modern browser is needed for the book reader
    function loadIE6Viewer() {
        var isLoaded = false;

        if ($.browser.msie && parseFloat($.browser.version) < 7) {
            ifrm = document.getElementById("bookReaderFrame");
            if (ifrm == null) {
                ifrm = document.createElement("iframe");
                ifrm.setAttribute("name", "bookReaderFrame");
                ifrm.setAttribute("id", "bookReaderFrame");
                ifrm.width = "800px";
                ifrm.height = "600px";
                document.getElementById("BookReader").appendChild(ifrm);
            }
            ifrm.setAttribute("src", "/Controls/OLBookReader/Viewer/index.html");
            isLoaded = true;
        }

        return isLoaded;
    }

    // Initialize the primary book viewer (for everything other than IE6)
    function InitializeViewer(startPage) {

        // Return the width of a given page.  Here we assume all images are 1600 pixels wide
        br.getPageWidth = function (index) {
            if (qsParm["fixedWidth"] != 0) return qsParm["fixedWidth"];
            if (pages != null) return pages[index].Width;
            return 1600;
        }

        // Return the height of a given page.  Here we assume all images are 2400 pixels high
        br.getPageHeight = function (index) {
            if (qsParm["fixedHeight"] != 0) return qsParm["fixedHeight"];
            if (pages != null) return pages[index].Height;
            return 2400;
        }

        // Load the images from archive.org -- modify this function to retrieve images using
        // a different URL structure
        br.getPageURI = function (index) {
            var url = "";
            if (pages != null) {
                if (pages[index].ExternalBaseUrl.includes("archive.org")) {
                    url = pages[index].ExternalBaseUrl + '/download/' + pages[index].BarCode + '/page/n' + (pages[index].SequenceOrder - 1) + '_w1150.jpg';
                }
                else {
                    url = pages[index].ExternalBaseUrl + '/web/' + pages[index].BarCode + '/' + pages[index].BarCode + '_' + ('0000' + (index + 1)).slice(-4) + '_large.webp';
                }
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

        // For a given "accessible page index" return the page number in the book.
        //
        // For example, index 5 might correspond to "Page 1" if there is front matter such
        // as a title page and table of contents.
        br.getPageNum = function (index) {
            return index + 1;
        }

        // Return the index number for the leaf
        br.leafNumToIndex = function (leaf) {
            return leaf - 1;
        }

        // Indicate whether or not to tell the container page to update on a page change
        br.callbackOnPageChange = true;

        // Total number of leafs
        br.numLeafs = qsParm["pgs"];

        // Book title and the URL used for the book title link
        br.bookTitle = '';
        br.bookUrl = '';

        // Store the item identifier for later use
        br.bhlObjectType = qsParm["objectType"];
        br.bhlObjectId = qsParm["objectId"];

        br.ui = "embed";

        // Set flag indicating whether the annotations exist (if so, then page scans are left justified)
        br.hasAnnotation = false;

        // Set the starting page (generally the title page) for the book
        br.titleLeaf = startPage;

        // Let's go!
        br.init();
    }
</script>
 