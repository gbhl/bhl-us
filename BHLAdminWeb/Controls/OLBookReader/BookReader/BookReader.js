/*
Copyright(c)2008-2009 Internet Archive. Software license AGPL version 3.

This file is part of BookReader.

    BookReader is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    BookReader is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with BookReader.  If not, see <http://www.gnu.org/licenses/>.
    
    The BookReader source is hosted at http://github.com/openlibrary/bookreader/

    archive.org cvs $Revision: 1.2 $ $Date: 2009-06-22 18:42:51 $
*/

// BookReader()
//______________________________________________________________________________
// After you instantiate this object, you must supply the following
// book-specific functions, before calling init().  Some of these functions
// can just be stubs for simple books.
//  - getPageWidth()
//  - getPageHeight()
//  - getPageURI()
//  - getPageSide()
//  - canRotatePage()
//  - getPageNum()
//  - getSpreadIndices()
// You must also add a numLeafs property before calling init().

function BookReader() {
    this.reduce  = 4;
    this.padding = 10;
    //this.mode    = 1; //1 or 2    REMOVED MWL 10/9/2009
    this.ui = 'full'; // UI mode
    
    // If there is a cookie set for "mode", set the mode value - MWL 10/9/2009
    var modeCookie = this.getViewerCookie('viewermode');
    if (modeCookie != '') {
        this.mode = Number(modeCookie);
    }
    else {
        this.mode = 1; //1 or 2
    }
    
    this.displayedIndices = [];	
    //this.indicesToDisplay = [];
    this.imgs = {};
    this.prefetchedImgs = {}; //an object with numeric keys cooresponding to page index
    
    this.timer     = null;
    this.animating = false;
    this.auto      = false;
    this.autoTimer = null;
    this.flipSpeed = 'fast';

    this.twoPagePopUp = null;
    this.leafEdgeTmp  = null;
    this.embedPopup = null;
    this.printPopup = null;
    
    this.searchTerm = '';
    this.searchResults = {};
    
    this.firstIndex = null;
    
    this.lastDisplayableIndex2up = null;
    
    // We link to index.php to avoid redirect which breaks back button
    this.logoURL = 'http://www.archive.org/index.php';
    
    // Base URL for images
    // Modified - MWL 9/8/2009
    //this.imagesBaseURL = '/bookreader/images/';
    this.imagesBaseURL = '/Controls/OLBookReader/BookReader/images/';
    
    // Mode constants
    this.constMode1up = 1;
    this.constMode2up = 2;
    
    // Zoom levels
    this.reductionFactors = [0.5, 1, 2, 4, 8, 16];

    // Object to hold parameters related to 2up mode
    this.twoPage = {
        coverInternalPadding: 10, // Width of cover
        coverExternalPadding: 10, // Padding outside of cover
        bookSpineDivWidth: 30,    // Width of book spine  $$$ consider sizing based on book length
        autofit: true
    };
    
    // Background color for pages (e.g. when loading page image)
    // $$$ TODO dynamically calculate based on page images
    this.pageDefaultBackgroundColor = 'rgb(234, 226, 205)';

    //flag for tweaking layout on books with annotation viewer - EGD 12/15/2010
    this.hasAnnotation = false; 
};

// init()
//______________________________________________________________________________
BookReader.prototype.init = function() {

    var startIndex = undefined;

    // Find start index and mode if set in location hash
    var params = this.paramsFromFragment(window.location.hash);

    if ('undefined' != typeof (params.index)) {
        startIndex = params.index;
    } else if ('undefined' != typeof (params.page)) {
        startIndex = this.getPageIndex(params.page);
    }

    if ('undefined' == typeof (startIndex)) {
        if ('undefined' != typeof (this.titleLeaf)) {
            startIndex = this.leafNumToIndex(this.titleLeaf);
        }
    }

    if ('undefined' == typeof (startIndex)) {
        startIndex = 0;
    }

    if ('undefined' != typeof (params.mode)) {
        this.mode = params.mode;

        // If mode is set in the params, then save the value to the mode cookie - MWL 10/9/2009
        this.setViewerCookie('viewermode', this.mode, 365);
    }

    // Set document title -- may have already been set in enclosing html for
    // search engine visibility
    // Removed following line to prevent page title being replaced by the bookviewer - MWL 2011/08/17
    // document.title = this.shortTitle(50);

    // Sanitize parameters
    if (!this.canSwitchToMode(this.mode)) {
        this.mode = this.constMode1up;
    }

    $("#BookReader").empty();
    this.initToolbar(this.mode, this.ui); // Build inside of toolbar div
    $("#BookReader").append("<div id='BRcontainer'></div>");
    $("#BRcontainer").append("<div id='BRpageview'></div>");

    $("#BRcontainer").on('scroll', this, function(e) {
        e.data.loadLeafs();
    });

    this.setupKeyListeners();
    this.startLocationPolling();

    $(window).on('resize', this, function(e) {
        //console.log('resize!');
        if (1 == e.data.mode) {
            //console.log('centering 1page view');
            e.data.centerPageView();
            $('#BRpageview').empty()
            e.data.displayedIndices = [];
            e.data.updateSearchHilites(); //deletes hilights but does not call remove()            
            e.data.loadLeafs();
        } else {
            //console.log('drawing 2 page view');

            // We only need to prepare again in autofit (size of spread changes)
            if (e.data.twoPage.autofit) {
                e.data.prepareTwoPageView();
            } else {
                // Re-center if the scrollbars have disappeared
                var center = e.data.twoPageGetViewCenter();
                var doRecenter = false;
                if (e.data.twoPage.totalWidth < $('#BRcontainer').prop('clientWidth')) {
                    center.percentageX = 0.5;
                    doRecenter = true;
                }
                if (e.data.twoPage.totalHeight < $('#BRcontainer').prop('clientHeight')) {
                    center.percentageY = 0.5;
                    doRecenter = true;
                }
                if (doRecenter) {
                    e.data.twoPageCenterView(center.percentageX, center.percentageY);
                }
            }
        }
    });

    $('.BRpagediv1up').on('mousedown', this, function(e) {
        // $$$ the purpose of this is to disable selection of the image (makes it turn blue)
        //     but this also interferes with right-click.  See https://bugs.edge.launchpad.net/gnubook/+bug/362626
    });

    if (1 == this.mode) {
        this.resizePageView();
        this.firstIndex = startIndex;
        this.jumpToIndex(startIndex);
    } else {
        //this.resizePageView();

        this.displayedIndices = [0];
        this.firstIndex = startIndex;
        this.displayedIndices = [this.firstIndex];
        //console.log('titleLeaf: %d', this.titleLeaf);
        //console.log('displayedIndices: %s', this.displayedIndices);
        this.prepareTwoPageView();

        //-----------------------------------------------------------------------------------
        // Call the function on the containing page to sync up the page list - MWL 02/02/2011
        this.syncPageList(startIndex);
        //-----------------------------------------------------------------------------------
    }

    // Enact other parts of initial params
    this.updateFromParams(params);
}

BookReader.prototype.setupKeyListeners = function() {
    var self = this;
    
    var KEY_PGUP = 33;
    var KEY_PGDOWN = 34;
    var KEY_END = 35;
    var KEY_HOME = 36;

    var KEY_LEFT = 37;
    var KEY_UP = 38;
    var KEY_RIGHT = 39;
    var KEY_DOWN = 40;

    // We use document here instead of window to avoid a bug in jQuery on IE7
    $(document).on("keydown", function(e) {
    
        // Keyboard navigation        
        if (!self.keyboardNavigationIsDisabled(e)) {
            switch(e.keyCode) {
                case KEY_PGUP:
                case KEY_UP:            
                    // In 1up mode page scrolling is handled by browser
                    if (2 == self.mode) {
                        e.preventDefault();
                        self.prev();
                    }
                    break;
                case KEY_DOWN:
                case KEY_PGDOWN:
                    if (2 == self.mode) {
                        e.preventDefault();
                        self.next();
                    }
                    break;
                case KEY_END:
                    e.preventDefault();
                    self.last();
                    break;
                case KEY_HOME:
                    e.preventDefault();
                    self.first();
                    break;
                case KEY_LEFT:
                    if (2 == self.mode) {
                        e.preventDefault();
                        self.left();
                    }
                    break;
                case KEY_RIGHT:
                    if (2 == self.mode) {
                        e.preventDefault();
                        self.right();
                    }
                    break;
            }
        }
    });
}

// drawLeafs()
//______________________________________________________________________________
BookReader.prototype.drawLeafs = function() {
    if (1 == this.mode) {
        this.drawLeafsOnePage();
    } else {
        this.drawLeafsTwoPage();
    }
}

// setDragHandler()
//______________________________________________________________________________
BookReader.prototype.setDragHandler = function(div) {
    div.dragging = false;

    $(div).off('mousedown').on('mousedown', function(e) {
        e.preventDefault();
        
        //console.log('mousedown at ' + e.pageY);

        this.dragging = true;
        this.prevMouseX = e.pageX;
        this.prevMouseY = e.pageY;
    
        var startX    = e.pageX;
        var startY    = e.pageY;
        var startTop  = $('#BRcontainer').prop('scrollTop');
        var startLeft =  $('#BRcontainer').prop('scrollLeft');

    });
        
    $(div).off('mousemove').on('mousemove', function(ee) {
        ee.preventDefault();

        // console.log('mousemove ' + ee.pageX + ',' + ee.pageY);
        
        var offsetX = ee.pageX - this.prevMouseX;
        var offsetY = ee.pageY - this.prevMouseY;
        
        if (this.dragging) {
            $('#BRcontainer').prop('scrollTop', $('#BRcontainer').prop('scrollTop') - offsetY);
            $('#BRcontainer').prop('scrollLeft', $('#BRcontainer').prop('scrollLeft') - offsetX);
        }
        
        this.prevMouseX = ee.pageX;
        this.prevMouseY = ee.pageY;
        
    });
    
    $(div).off('mouseup').on('mouseup', function(ee) {
        ee.preventDefault();
        //console.log('mouseup');

        this.dragging = false;
    });
    
    $(div).off('mouseleave').on('mouseleave', function(e) {
        e.preventDefault();
        //console.log('mouseleave');

        this.dragging = false;        
    });
    
    $(div).off('mouseenter').on('mouseenter', function(e) {
        e.preventDefault();
        //console.log('mouseenter');
        
        this.dragging = false;
    });
}

// setDragHandler2UP()
//______________________________________________________________________________
BookReader.prototype.setDragHandler2UP = function(div) {
    div.dragging = false;
    
    $(div).off('mousedown').on('mousedown', function(e) {
        e.preventDefault();
        
        //console.log('mousedown at ' + e.pageY);

        this.dragStart = {x: e.pageX, y: e.pageY };
        this.mouseDown = true;
        this.dragging = false; // wait until drag distance
        this.prevMouseX = e.pageX;
        this.prevMouseY = e.pageY;
    
        var startX    = e.pageX;
        var startY    = e.pageY;
        var startTop  = $('#BRcontainer').prop('scrollTop');
        var startLeft =  $('#BRcontainer').prop('scrollLeft');

    });
        
    $(div).off('mousemove').on('mousemove', function(ee) {
        ee.preventDefault();

        // console.log('mousemove ' + ee.pageX + ',' + ee.pageY);
        
        var offsetX = ee.pageX - this.prevMouseX;
        var offsetY = ee.pageY - this.prevMouseY;
        
        var minDragDistance = 5; // $$$ constant

        var distance = Math.max(Math.abs(offsetX), Math.abs(offsetY));
                
        if (this.mouseDown && (distance > minDragDistance)) {
            //console.log('drag start!');
            
            this.dragging = true;
        }
        
        if (this.dragging) {        
            $('#BRcontainer').prop('scrollTop', $('#BRcontainer').prop('scrollTop') - offsetY);
            $('#BRcontainer').prop('scrollLeft', $('#BRcontainer').prop('scrollLeft') - offsetX);
            this.prevMouseX = ee.pageX;
            this.prevMouseY = ee.pageY;
        }
        
        
    });
    
    $(div).off('mouseleave').on('mouseleave', function(e) {
        e.preventDefault();
        //console.log('mouseleave');

        this.dragging = false;  
        this.mouseDown = false;
    });
    
    $(div).off('mouseenter').on('mouseenter', function(e) {
        e.preventDefault();
        //console.log('mouseenter');
        
        this.dragging = false;
        this.mouseDown = false;
    });
}

BookReader.prototype.setClickHandler2UP = function( element, data, handler) {
    //console.log('setting handler');
    //console.log(element.tagName);
    
    $(element).off('click').on('click', data, function(e) {
        e.preventDefault();
        
        //console.log('click!');
        
        if (this.mouseDown && (!this.dragging)) {
            //console.log('click not dragging!');
            handler(e);
        }
        
        this.dragging = false;
        this.mouseDown = false;
    });
}

// drawLeafsOnePage()
//______________________________________________________________________________
BookReader.prototype.drawLeafsOnePage = function () {
    //alert('drawing leafs!');
    this.timer = null;


    var scrollTop = $('#BRcontainer').prop('scrollTop');
    var scrollBottom = scrollTop + $('#BRcontainer').height();
    //console.log('top=' + scrollTop + ' bottom='+scrollBottom);

    var indicesToDisplay = [];

    var i;
    var leafTop = 0;
    var leafBottom = 0;
    for (i = 0; i < this.numLeafs; i++) {
        var height = parseInt(this._getPageHeight(i) / this.reduce);

        leafBottom += height;
        //console.log('leafTop = '+leafTop+ ' pageH = ' + this.pageH[i] + 'leafTop>=scrollTop=' + (leafTop>=scrollTop));
        var topInView = (leafTop >= scrollTop) && (leafTop <= scrollBottom);
        var bottomInView = (leafBottom >= scrollTop) && (leafBottom <= scrollBottom);
        var middleInView = (leafTop <= scrollTop) && (leafBottom >= scrollBottom);
        if (topInView | bottomInView | middleInView) {
            //console.log('displayed: ' + this.displayedIndices);
            //console.log('to display: ' + i);
            indicesToDisplay.push(i);
        }
        leafTop += height + 10;
        leafBottom += 10;
    }

    var firstIndexToDraw = indicesToDisplay[0];
    this.firstIndex = firstIndexToDraw;

    // Update hash, but only if we're currently displaying a leaf
    // Hack that fixes #365790
    if (this.displayedIndices.length > 0) {
        this.updateLocationHash();
    }

    // -- To improve performance for BHL, don't prefetch before-and-after images - MWL 10/8/2009
    if ((0 != firstIndexToDraw) && (1 < this.reduce)) {
        firstIndexToDraw--;
        indicesToDisplay.unshift(firstIndexToDraw);
    }

    var lastIndexToDraw = indicesToDisplay[indicesToDisplay.length - 1];
    if (((this.numLeafs - 1) != lastIndexToDraw) && (1 < this.reduce)) {
        indicesToDisplay.push(lastIndexToDraw + 1);
    }

    leafTop = 0;
    var i;
    for (i = 0; i < firstIndexToDraw; i++) {
        leafTop += parseInt(this._getPageHeight(i) / this.reduce) + 10;
    }

    //var viewWidth = $('#BRpageview').width(); //includes scroll bar width
    var viewWidth = $('#BRcontainer').prop('scrollWidth');


    for (i = 0; i < indicesToDisplay.length; i++) {
        var index = indicesToDisplay[i];
        var height = parseInt(this._getPageHeight(index) / this.reduce);

        if (-1 == jQuery.inArray(indicesToDisplay[i], this.displayedIndices)) {
            var width = parseInt(this._getPageWidth(index) / this.reduce);
            //console.log("displaying leaf " + indicesToDisplay[i] + ' leafTop=' +leafTop);
            var div = document.createElement("div");
            div.className = 'BRpagediv1up';
            div.id = 'pagediv' + index;
            div.style.position = "absolute";
            $(div).css('top', leafTop + 'px');
            var left = (viewWidth - width) >> 1;
            if (left < 0) left = 0;

            //if has Annotation viewer, keep page left, otherwise center - EGD 12/15/2010
            if (!this.hasAnnotation) {
                $(div).css('left', left + 'px');
            }

            $(div).css('width', width + 'px');
            $(div).css('height', height + 'px');
            //$(div).text('loading...');

            this.setDragHandler(div);

            $('#BRpageview').append(div);

            var img = document.createElement("img");
            img.src = this._getPageURI(index, this.reduce, 0);
            $(img).css('width', width + 'px');
            $(img).css('height', height + 'px');
            $(div).append(img);

        } else {
            //console.log("not displaying " + indicesToDisplay[i] + ' score=' + jQuery.inArray(indicesToDisplay[i], this.displayedIndices));            
        }

        leafTop += height + 10;

    }

    for (i = 0; i < this.displayedIndices.length; i++) {
        if (-1 == jQuery.inArray(this.displayedIndices[i], indicesToDisplay)) {
            var index = this.displayedIndices[i];
            //console.log('Removing leaf ' + index);
            //console.log('id='+'#pagediv'+index+ ' top = ' +$('#pagediv'+index).css('top'));
            $('#pagediv' + index).remove();
        } else {
            //console.log('NOT Removing leaf ' + this.displayedIndices[i]);
        }
    }

    this.displayedIndices = indicesToDisplay.slice();
    this.updateSearchHilites();

    if (null != this.getPageNum(firstIndexToDraw)) {
        $("#BRpagenum").val(this.getPageNum(this.currentIndex()));
    } else {
        $("#BRpagenum").val('');
    }

    this.updateToolbarZoom(this.reduce);

}

// drawLeafsTwoPage()
//______________________________________________________________________________
BookReader.prototype.drawLeafsTwoPage = function() {
    var scrollTop = $('#BRtwopageview').prop('scrollTop');
    var scrollBottom = scrollTop + $('#BRtwopageview').height();
    
    // $$$ we should use calculated values in this.twoPage (recalc if necessary)
    
    var indexL = this.twoPage.currentIndexL;
        
    var heightL  = this._getPageHeight(indexL); 
    var widthL   = this._getPageWidth(indexL);

    var leafEdgeWidthL = this.leafEdgeWidth(indexL);
    var leafEdgeWidthR = this.twoPage.edgeWidth - leafEdgeWidthL;
    //var bookCoverDivWidth = this.twoPage.width*2 + 20 + this.twoPage.edgeWidth; // $$$ hardcoded cover width
    var bookCoverDivWidth = this.twoPage.bookCoverDivWidth;
    //console.log(leafEdgeWidthL);

    var middle = this.twoPage.middle; // $$$ getter instead?
    var top = this.twoPageTop();
    var bookCoverDivLeft = this.twoPage.bookCoverDivLeft;

    this.twoPage.scaledWL = this.getPageWidth2UP(indexL);
    this.twoPage.gutter = this.twoPageGutter();
    
    this.prefetchImg(indexL);
    $(this.prefetchedImgs[indexL]).css({
        position: 'absolute',
        left: this.twoPage.gutter-this.twoPage.scaledWL+'px',
        right: '',
        top:    top+'px',
        backgroundColor: this.getPageBackgroundColor(indexL),
        height: this.twoPage.height +'px', // $$$ height forced the same for both pages
        width:  this.twoPage.scaledWL + 'px',
        borderRight: '1px solid black',
        zIndex: 2
    }).appendTo('#BRtwopageview');
    
    var indexR = this.twoPage.currentIndexR;
    var heightR  = this._getPageHeight(indexR); 
    var widthR   = this._getPageWidth(indexR);

    // $$$ should use getwidth2up?
    //var scaledWR = this.twoPage.height*widthR/heightR;
    this.twoPage.scaledWR = this.getPageWidth2UP(indexR);
    this.prefetchImg(indexR);
    $(this.prefetchedImgs[indexR]).css({
        position: 'absolute',
        left:   this.twoPage.gutter+'px',
        right: '',
        top:    top+'px',
        backgroundColor: this.getPageBackgroundColor(indexR),
        height: this.twoPage.height + 'px', // $$$ height forced the same for both pages
        width:  this.twoPage.scaledWR + 'px',
        borderLeft: '1px solid black',
        zIndex: 2
    }).appendTo('#BRtwopageview');
        

    this.displayedIndices = [this.twoPage.currentIndexL, this.twoPage.currentIndexR];
    this.setMouseHandlers2UP();
    this.twoPageSetCursor();

    this.updatePageNumBox2UP();
    this.updateToolbarZoom(this.reduce);
    
    // this.twoPagePlaceFlipAreas();  // No longer used

}

// updatePageNumBox2UP
//______________________________________________________________________________
BookReader.prototype.updatePageNumBox2UP = function() {
    if (null != this.getPageNum(this.twoPage.currentIndexL))  {
        $("#BRpagenum").val(this.getPageNum(this.currentIndex()));
    } else {
        $("#BRpagenum").val('');
    }
    this.updateLocationHash();
}

// loadLeafs()
//______________________________________________________________________________
BookReader.prototype.loadLeafs = function() {


    var self = this;
    if (null == this.timer) {
        this.timer=setTimeout(function(){self.drawLeafs()},250);
    } else {
        clearTimeout(this.timer);
        this.timer=setTimeout(function(){self.drawLeafs()},250);    
    }
}

// zoom(direction)
//
// Pass 1 to zoom in, anything else to zoom out
//______________________________________________________________________________
BookReader.prototype.zoom = function(direction) {
    switch (this.mode) {
        case this.constMode1up:
            return this.zoom1up(direction);
        case this.constMode2up:
            return this.zoom2up(direction);
    }
}

// zoom1up(dir)
//______________________________________________________________________________
BookReader.prototype.zoom1up = function(dir) {

    if (2 == this.mode) {     //can only zoom in 1-page mode
        this.switchMode(1);
        return;
    }
    
    // $$$ with flexible zoom we could "snap" to /2 page reductions
    //     for better scaling
    if (1 == dir) {
        if (this.reduce <= 0.5) return;
        this.reduce*=0.5;           //zoom in
    } else {
        if (this.reduce >= 8) return;
        this.reduce*=2;             //zoom out
    }
    
    this.resizePageView();

    $('#BRpageview').empty()
    this.displayedIndices = [];
    this.loadLeafs();
    
    this.updateToolbarZoom(this.reduce);
    
    // Recalculate search hilites
    this.removeSearchHilites(); 
    this.updateSearchHilites();

}

// resizePageView()
//______________________________________________________________________________
BookReader.prototype.resizePageView = function() {
    var i;
    var viewHeight = 0;
    //var viewWidth  = $('#BRcontainer').width(); //includes scrollBar
    var viewWidth  = $('#BRcontainer').prop('clientWidth');   

    var oldScrollTop  = $('#BRcontainer').prop('scrollTop');
    var oldScrollLeft = $('#BRcontainer').prop('scrollLeft');
    var oldPageViewHeight = $('#BRpageview').height();
    var oldPageViewWidth = $('#BRpageview').width();
    
    var oldCenterY = this.centerY1up();
    var oldCenterX = this.centerX1up();
    
    if (0 != oldPageViewHeight) {
        var scrollRatio = oldCenterY / oldPageViewHeight;
    } else {
        var scrollRatio = 0;
    }
    
    for (i=0; i<this.numLeafs; i++) {
        viewHeight += parseInt(this._getPageHeight(i)/this.reduce) + this.padding; 
        var width = parseInt(this._getPageWidth(i)/this.reduce);
        if (width>viewWidth) viewWidth=width;
    }
    $('#BRpageview').height(viewHeight);
    $('#BRpageview').width(viewWidth);    

    var newCenterY = scrollRatio*viewHeight;
    var newTop = Math.max(0, Math.floor( newCenterY - $('#BRcontainer').height()/2 ));
    $('#BRcontainer').prop('scrollTop', newTop);
    
    // We use clientWidth here to avoid miscalculating due to scroll bar
    var newCenterX = oldCenterX * (viewWidth / oldPageViewWidth);
    var newLeft = newCenterX - $('#BRcontainer').prop('clientWidth') / 2;
    newLeft = Math.max(newLeft, 0);
    $('#BRcontainer').prop('scrollLeft', newLeft);
    //console.log('oldCenterX ' + oldCenterX + ' newCenterX ' + newCenterX + ' newLeft ' + newLeft);
    
    //this.centerPageView();
    this.loadLeafs();
    
    // Not really needed until there is 1up autofit
    this.removeSearchHilites();
    this.updateSearchHilites();
}

// centerX1up()
//______________________________________________________________________________
// Returns the current offset of the viewport center in scaled document coordinates.
BookReader.prototype.centerX1up = function() {
    var centerX;
    if ($('#BRpageview').width() < $('#BRcontainer').prop('clientWidth')) { // fully shown
        centerX = $('#BRpageview').width();
    } else {
        centerX = $('#BRcontainer').prop('scrollLeft') + $('#BRcontainer').prop('clientWidth') / 2;
    }
    centerX = Math.floor(centerX);
    return centerX;
}

// centerY1up()
//______________________________________________________________________________
// Returns the current offset of the viewport center in scaled document coordinates.
BookReader.prototype.centerY1up = function() {
    var centerY = $('#BRcontainer').prop('scrollTop') + $('#BRcontainer').height() / 2;
    return Math.floor(centerY);
}

// centerPageView()
//______________________________________________________________________________
BookReader.prototype.centerPageView = function() {

    var scrollWidth  = $('#BRcontainer').prop('scrollWidth');
    var clientWidth  =  $('#BRcontainer').prop('clientWidth');
    //console.log('sW='+scrollWidth+' cW='+clientWidth);
    if (scrollWidth > clientWidth) {
        $('#BRcontainer').prop('scrollLeft', (scrollWidth-clientWidth)/2);
    }

}

// zoom2up(direction)
//______________________________________________________________________________
BookReader.prototype.zoom2up = function(direction) {

    // Hard stop autoplay
    this.stopFlipAnimations();
    
    // Get new zoom state    
    var newZoom = this.twoPageNextReduce(this.reduce, direction);
    if ((this.reduce == newZoom.reduce) && (this.twoPage.autofit == newZoom.autofit)) {
        return;
    }
    this.twoPage.autofit = newZoom.autofit;
    this.reduce = newZoom.reduce;

    // Preserve view center position
    var oldCenter = this.twoPageGetViewCenter();
    
    // If zooming in, reload imgs.  DOM elements will be removed by prepareTwoPageView
    // $$$ An improvement would be to use the low res image until the larger one is loaded.
    if (1 == direction) {
        for (var img in this.prefetchedImgs) {
            delete this.prefetchedImgs[img];
        }
    }
    
    // Prepare view with new center to minimize visual glitches
    this.prepareTwoPageView(oldCenter.percentageX, oldCenter.percentageY);
}


// quantizeReduce(reduce)
//______________________________________________________________________________
// Quantizes the given reduction factor to closest power of two from set from 12.5% to 200%
BookReader.prototype.quantizeReduce = function(reduce) {
    var quantized = this.reductionFactors[0];
    var distance = Math.abs(reduce - quantized);
    for (var i = 1; i < this.reductionFactors.length; i++) {
        newDistance = Math.abs(reduce - this.reductionFactors[i]);
        if (newDistance < distance) {
            distance = newDistance;
            quantized = this.reductionFactors[i];
        }
    }
    
    return quantized;
}

// twoPageNextReduce()
//______________________________________________________________________________
// Returns the next reduction level
BookReader.prototype.twoPageNextReduce = function(reduce, direction) {
    var result = {};
    var autofitReduce = this.twoPageGetAutofitReduce();

    if (0 == direction) { // autofit
        result.autofit = true;
        result.reduce = autofitReduce;
        
    } else if (1 == direction) { // zoom in
        var newReduce = this.reductionFactors[0];
    
        for (var i = 1; i < this.reductionFactors.length; i++) {
            if (this.reductionFactors[i] < reduce) {
                newReduce = this.reductionFactors[i];
            }
        }
        
        if (!this.twoPage.autofit && (autofitReduce < reduce && autofitReduce > newReduce)) {
            // use autofit
            result.autofit = true;
            result.reduce = autofitReduce;
        } else {        
            result.autofit = false;
            result.reduce = newReduce;
        }
        
    } else { // zoom out
        var lastIndex = this.reductionFactors.length - 1;
        var newReduce = this.reductionFactors[lastIndex];
        
        for (var i = lastIndex; i >= 0; i--) {
            if (this.reductionFactors[i] > reduce) {
                newReduce = this.reductionFactors[i];
            }
        }
         
        if (!this.twoPage.autofit && (autofitReduce > reduce && autofitReduce < newReduce)) {
            // use autofit
            result.autofit = true;
            result.reduce = autofitReduce;
        } else {
            result.autofit = false;
            result.reduce = newReduce;
        }
    }
    
    return result;
}

// jumpToPage()
//______________________________________________________________________________
// Attempts to jump to page.  Returns true if page could be found, false otherwise.
BookReader.prototype.jumpToPage = function(pageNum) {

    var pageIndex = this.getPageIndex(pageNum);

    if ('undefined' != typeof(pageIndex)) {
        var leafTop = 0;
        var h;
        this.jumpToIndex(pageIndex);
        $('#BRcontainer').prop('scrollTop', leafTop);
        return true;
    }
    
    // Page not found
    return false;
}

// jumpToIndex()
//______________________________________________________________________________
BookReader.prototype.jumpToIndex = function (index, pageX, pageY) {

    //-----------------------------------------------------------------------------------
    // Call the function on the containing page to sync up the page list - MWL 02/02/2011
    this.syncPageList(index);
    //-----------------------------------------------------------------------------------

    if (2 == this.mode) {
        this.autoStop();

        // By checking against min/max we do nothing if requested index
        // is current
        if (index < Math.min(this.twoPage.currentIndexL, this.twoPage.currentIndexR)) {
            this.flipBackToIndex(index);
        } else if (index > Math.max(this.twoPage.currentIndexL, this.twoPage.currentIndexR)) {
            this.flipFwdToIndex(index);
        }

    } else {
        var i;
        var leafTop = 0;
        var leafLeft = 0;
        var h;
        for (i = 0; i < index; i++) {
            h = parseInt(this._getPageHeight(i) / this.reduce);
            leafTop += h + this.padding;
        }

        if (pageY) {
            //console.log('pageY ' + pageY);
            var offset = parseInt((pageY) / this.reduce);
            offset -= $('#BRcontainer').prop('clientHeight') >> 1;
            //console.log( 'jumping to ' + leafTop + ' ' + offset);
            leafTop += offset;
        }

        if (pageX) {
            var offset = parseInt((pageX) / this.reduce);
            offset -= $('#BRcontainer').prop('clientWidth') >> 1;
            leafLeft += offset;
        }

        $('#BRcontainer').animate({ scrollTop: leafTop, scrollLeft: leafLeft }, 'fast');
    }
}


// switchMode()
//______________________________________________________________________________
BookReader.prototype.switchMode = function(mode) {

    //console.log('  asked to switch to mode ' + mode + ' from ' + this.mode);
    
    if (mode == this.mode) return;
    
    if (!this.canSwitchToMode(mode)) {
        return;
    }

    this.autoStop();
    this.removeSearchHilites();

    this.mode = mode;
    
    this.switchToolbarMode(mode);
    
    // $$$ TODO preserve center of view when switching between mode
    //     See https://bugs.edge.launchpad.net/gnubook/+bug/416682
    
    if (1 == mode) {
        this.reduce = this.quantizeReduce(this.reduce);
        this.prepareOnePageView();
    } else {
        this.twoPage.autofit = false; // Take zoom level from other mode
        this.reduce = this.quantizeReduce(this.reduce);
        this.prepareTwoPageView();
        this.twoPageCenterView(0.5, 0.5); // $$$ TODO preserve center
    }

    // Save the new mode value to the mode cookie - MWL 10/9/2009
    this.setViewerCookie('viewermode', mode, 365);

}

//prepareOnePageView()
//______________________________________________________________________________
BookReader.prototype.prepareOnePageView = function() {

    // var startLeaf = this.displayedIndices[0];
    var startLeaf = this.currentIndex();
    
    $('#BRcontainer').empty();
    $('#BRcontainer').css({
        overflowY: 'scroll',
        overflowX: 'auto'
    });
    
    var brPageView = $("#BRcontainer").append("<div id='BRpageview'></div>");
    
    this.resizePageView();
    
    this.jumpToIndex(startLeaf);
    this.displayedIndices = [];
    
    this.drawLeafsOnePage();
        
    // Bind mouse handlers
    // Disable mouse click to avoid selected/highlighted page images - bug 354239
    brPageView.on('mousedown', function(e) {
        // $$$ check here for right-click and don't disable.  Also use jQuery style
        //     for stopping propagation. See https://bugs.edge.launchpad.net/gnubook/+bug/362626
        return false;
    })
    // Special hack for IE7
    brPageView[0].onselectstart = function(e) { return false; };
}

// prepareTwoPageView()
//______________________________________________________________________________
// Some decisions about two page view:
//
// Both pages will be displayed at the same height, even if they were different physical/scanned
// sizes.  This simplifies the animation (from a design as well as technical standpoint).  We
// examine the page aspect ratios (in calculateSpreadSize) and use the page with the most "normal"
// aspect ratio to determine the height.
//
// The two page view div is resized to keep the middle of the book in the middle of the div
// even as the page sizes change.  To e.g. keep the middle of the book in the middle of the BRcontent
// div requires adjusting the offset of BRtwpageview and/or scrolling in BRcontent.
BookReader.prototype.prepareTwoPageView = function(centerPercentageX, centerPercentageY) {
    $('#BRcontainer').empty();
    $('#BRcontainer').css('overflow', 'auto');
        
    // We want to display two facing pages.  We may be missing
    // one side of the spread because it is the first/last leaf,
    // foldouts, missing pages, etc

    //var targetLeaf = this.displayedIndices[0];
    var targetLeaf = this.firstIndex;

    if (targetLeaf < this.firstDisplayableIndex()) {
        targetLeaf = this.firstDisplayableIndex();
    }
    
    if (targetLeaf > this.lastDisplayableIndex()) {
        targetLeaf = this.lastDisplayableIndex();
    }
    
    //this.twoPage.currentIndexL = null;
    //this.twoPage.currentIndexR = null;
    //this.pruneUnusedImgs();
    
    var currentSpreadIndices = this.getSpreadIndices(targetLeaf);
    this.twoPage.currentIndexL = currentSpreadIndices[0];
    this.twoPage.currentIndexR = currentSpreadIndices[1];
    this.firstIndex = this.twoPage.currentIndexL;
    
    this.calculateSpreadSize(); //sets twoPage.width, twoPage.height and others

    this.pruneUnusedImgs();
    this.prefetch(); // Preload images or reload if scaling has changed

    //console.dir(this.twoPage);
    
    // Add the two page view
    // $$$ Can we get everything set up and then append?
    $('#BRcontainer').append('<div id="BRtwopageview"></div>');

    // $$$ calculate first then set
    $('#BRtwopageview').css( {
        height: this.twoPage.totalHeight + 'px',
        width: this.twoPage.totalWidth + 'px',
        position: 'absolute'
        });
        
    // If there will not be scrollbars (e.g. when zooming out) we center the book
    // since otherwise the book will be stuck off-center
    if (this.twoPage.totalWidth < $('#BRcontainer').prop('clientWidth')) {
        centerPercentageX = 0.5;
    }
    if (this.twoPage.totalHeight < $('#BRcontainer').prop('clientHeight')) {
        centerPercentageY = 0.5;
    }
        
    this.twoPageCenterView(centerPercentageX, centerPercentageY);
    
    this.twoPage.coverDiv = document.createElement('div');
    $(this.twoPage.coverDiv).attr('id', 'BRbookcover').css({
        border: '1px solid rgb(68, 25, 17)',
        width:  this.twoPage.bookCoverDivWidth + 'px',
        height: this.twoPage.bookCoverDivHeight+'px',
        visibility: 'visible',
        position: 'absolute',
        backgroundColor: '#663929',
        left: this.twoPage.bookCoverDivLeft + 'px',
        top: this.twoPage.bookCoverDivTop+'px',
        MozBorderRadiusTopleft: '7px',
        MozBorderRadiusTopright: '7px',
        MozBorderRadiusBottomright: '7px',
        MozBorderRadiusBottomleft: '7px'
    }).appendTo('#BRtwopageview');
    
    this.leafEdgeR = document.createElement('div');
    this.leafEdgeR.className = 'leafEdgeR'; // $$$ the static CSS should be moved into the .css file
    $(this.leafEdgeR).css({
        borderStyle: 'solid solid solid none',
        borderColor: 'rgb(51, 51, 34)',
        borderWidth: '1px 1px 1px 0px',
        background: 'transparent url(' + this.imagesBaseURL + 'right_edges.png) repeat scroll 0% 0%',
        width: this.twoPage.leafEdgeWidthR + 'px',
        height: this.twoPage.height-1 + 'px',
        /*right: '10px',*/
        left: this.twoPage.gutter+this.twoPage.scaledWR+'px',
        top: this.twoPage.bookCoverDivTop+this.twoPage.coverInternalPadding+'px',
        position: 'absolute'
    }).appendTo('#BRtwopageview');
    
    this.leafEdgeL = document.createElement('div');
    this.leafEdgeL.className = 'leafEdgeL';
    $(this.leafEdgeL).css({ // $$$ static CSS should be moved to file
        borderStyle: 'solid none solid solid',
        borderColor: 'rgb(51, 51, 34)',
        borderWidth: '1px 0px 1px 1px',
        background: 'transparent url(' + this.imagesBaseURL + 'left_edges.png) repeat scroll 0% 0%',
        width: this.twoPage.leafEdgeWidthL + 'px',
        height: this.twoPage.height-1 + 'px',
        left: this.twoPage.bookCoverDivLeft+this.twoPage.coverInternalPadding+'px',
        top: this.twoPage.bookCoverDivTop+this.twoPage.coverInternalPadding+'px',    
        position: 'absolute'
    }).appendTo('#BRtwopageview');

    div = document.createElement('div');
    $(div).attr('id', 'BRbookspine').css({
        border:          '1px solid rgb(68, 25, 17)',
        width:           this.twoPage.bookSpineDivWidth+'px',
        height:          this.twoPage.bookSpineDivHeight+'px',
        position:        'absolute',
        backgroundColor: 'rgb(68, 25, 17)',
        left:            this.twoPage.bookSpineDivLeft+'px',
        top:             this.twoPage.bookSpineDivTop+'px'
    }).appendTo('#BRtwopageview');
    
    var self = this; // for closure
    
    this.prepareTwoPagePopUp();
    
    this.displayedIndices = [];
    
    this.drawLeafsTwoPage();
    this.updateToolbarZoom(this.reduce);
    
    this.prefetch();

    this.removeSearchHilites();
    this.updateSearchHilites();

}

// prepareTwoPagePopUp()
//
// This function prepares the "View Page n" popup that shows while the mouse is
// over the left/right "stack of sheets" edges.  It also binds the mouse
// events for these divs.
//______________________________________________________________________________
BookReader.prototype.prepareTwoPagePopUp = function() {

    this.twoPagePopUp = document.createElement('div');
    $(this.twoPagePopUp).css({
        border: '1px solid black',
        padding: '2px 6px',
        position: 'absolute',
        fontFamily: 'sans-serif',
        fontSize: '14px',
        zIndex: '1000',
        backgroundColor: 'rgb(255, 255, 238)',
        opacity: 0.85
    }).appendTo('#BRcontainer');
    $(this.twoPagePopUp).hide();
    
    $(this.leafEdgeL).add(this.leafEdgeR).on('mouseenter', this, function(e) {
        $(e.data.twoPagePopUp).show();
    });

    $(this.leafEdgeL).add(this.leafEdgeR).on('mouseleave', this, function(e) {
        $(e.data.twoPagePopUp).hide();
    });

    $(this.leafEdgeL).on('click', this, function(e) { 
        e.data.autoStop();
        var jumpIndex = e.data.jumpIndexForLeftEdgePageX(e.pageX);
        e.data.jumpToIndex(jumpIndex);
    });

    $(this.leafEdgeR).on('click', this, function(e) { 
        e.data.autoStop();
        var jumpIndex = e.data.jumpIndexForRightEdgePageX(e.pageX);
        e.data.jumpToIndex(jumpIndex);    
    });

    $(this.leafEdgeR).on('mousemove', this, function(e) {

        var jumpIndex = e.data.jumpIndexForRightEdgePageX(e.pageX);
        $(e.data.twoPagePopUp).text('View ' + e.data.getPageName(jumpIndex));
        
        // $$$ TODO: Make sure popup is positioned so that it is in view
        // (https://bugs.edge.launchpad.net/gnubook/+bug/327456)        
        $(e.data.twoPagePopUp).css({
            left: e.pageX- $('#BRcontainer').offset().left + $('#BRcontainer').scrollLeft() + 20 + 'px',
            top: e.pageY - $('#BRcontainer').offset().top + $('#BRcontainer').scrollTop() + 'px'
        });
    });

    $(this.leafEdgeL).on('mousemove', this, function(e) {
    
        var jumpIndex = e.data.jumpIndexForLeftEdgePageX(e.pageX);
        $(e.data.twoPagePopUp).text('View '+ e.data.getPageName(jumpIndex));

        // $$$ TODO: Make sure popup is positioned so that it is in view
        //           (https://bugs.edge.launchpad.net/gnubook/+bug/327456)        
        $(e.data.twoPagePopUp).css({
            left: e.pageX - $('#BRcontainer').offset().left + $('#BRcontainer').scrollLeft() - $(e.data.twoPagePopUp).width() - 25 + 'px',
            top: e.pageY-$('#BRcontainer').offset().top + $('#BRcontainer').scrollTop() + 'px'
        });
    });
}

// calculateSpreadSize()
//______________________________________________________________________________
// Calculates 2-page spread dimensions based on this.twoPage.currentIndexL and
// this.twoPage.currentIndexR
// This function sets this.twoPage.height, twoPage.width

BookReader.prototype.calculateSpreadSize = function() {

    var firstIndex  = this.twoPage.currentIndexL;
    var secondIndex = this.twoPage.currentIndexR;
    //console.log('first page is ' + firstIndex);

    // Calculate page sizes and total leaf width
    var spreadSize;
    if ( this.twoPage.autofit) {    
        spreadSize = this.getIdealSpreadSize(firstIndex, secondIndex);
    } else {
        // set based on reduction factor
        spreadSize = this.getSpreadSizeFromReduce(firstIndex, secondIndex, this.reduce);
    }
    
    // Both pages together
    this.twoPage.height = spreadSize.height;
    this.twoPage.width = spreadSize.width;
    
    // Individual pages
    this.twoPage.scaledWL = this.getPageWidth2UP(firstIndex);
    this.twoPage.scaledWR = this.getPageWidth2UP(secondIndex);
    
    // Leaf edges
    this.twoPage.edgeWidth = spreadSize.totalLeafEdgeWidth; // The combined width of both edges
    this.twoPage.leafEdgeWidthL = this.leafEdgeWidth(this.twoPage.currentIndexL);
    this.twoPage.leafEdgeWidthR = this.twoPage.edgeWidth - this.twoPage.leafEdgeWidthL;
    
    
    // Book cover
    // The width of the book cover div.  The combined width of both pages, twice the width
    // of the book cover internal padding (2*10) and the page edges
    this.twoPage.bookCoverDivWidth = this.twoPage.scaledWL + this.twoPage.scaledWR + 2 * this.twoPage.coverInternalPadding + this.twoPage.edgeWidth;
    // The height of the book cover div
    this.twoPage.bookCoverDivHeight = this.twoPage.height + 2 * this.twoPage.coverInternalPadding;
    
    
    // We calculate the total width and height for the div so that we can make the book
    // spine centered
    var leftGutterOffset = this.gutterOffsetForIndex(firstIndex);
    var leftWidthFromCenter = this.twoPage.scaledWL - leftGutterOffset + this.twoPage.leafEdgeWidthL;
    var rightWidthFromCenter = this.twoPage.scaledWR + leftGutterOffset + this.twoPage.leafEdgeWidthR;
    var largestWidthFromCenter = Math.max( leftWidthFromCenter, rightWidthFromCenter );
    this.twoPage.totalWidth = 2 * (largestWidthFromCenter + this.twoPage.coverInternalPadding + this.twoPage.coverExternalPadding);
    this.twoPage.totalHeight = this.twoPage.height + 2 * (this.twoPage.coverInternalPadding + this.twoPage.coverExternalPadding);
        
    // We want to minimize the unused space in two-up mode (maximize the amount of page
    // shown).  We give width to the leaf edges and these widths change (though the sum
    // of the two remains constant) as we flip through the book.  With the book
    // cover centered and fixed in the BRcontainer div the page images will meet
    // at the "gutter" which is generally offset from the center.
    this.twoPage.middle = this.twoPage.totalWidth >> 1;
    this.twoPage.gutter = this.twoPage.middle + this.gutterOffsetForIndex(firstIndex);
    
    // The left edge of the book cover moves depending on the width of the pages
    // $$$ change to getter
    this.twoPage.bookCoverDivLeft = this.twoPage.gutter - this.twoPage.scaledWL - this.twoPage.leafEdgeWidthL - this.twoPage.coverInternalPadding;
    // The top edge of the book cover stays a fixed distance from the top
    this.twoPage.bookCoverDivTop = this.twoPage.coverExternalPadding;

    // Book spine
    this.twoPage.bookSpineDivHeight = this.twoPage.height + 2*this.twoPage.coverInternalPadding;
    this.twoPage.bookSpineDivLeft = this.twoPage.middle - (this.twoPage.bookSpineDivWidth >> 1);
    this.twoPage.bookSpineDivTop = this.twoPage.bookCoverDivTop;


    this.reduce = spreadSize.reduce; // $$$ really set this here?
}

BookReader.prototype.getIdealSpreadSize = function(firstIndex, secondIndex) {
    var ideal = {};

    // We check which page is closest to a "normal" page and use that to set the height
    // for both pages.  This means that foldouts and other odd size pages will be displayed
    // smaller than the nominal zoom amount.
    var canon5Dratio = 1.5;
    
    var first = {
        height: this._getPageHeight(firstIndex),
        width: this._getPageWidth(firstIndex)
    }
    
    var second = {
        height: this._getPageHeight(secondIndex),
        width: this._getPageWidth(secondIndex)
    }
    
    var firstIndexRatio  = first.height / first.width;
    var secondIndexRatio = second.height / second.width;
    //console.log('firstIndexRatio = ' + firstIndexRatio + ' secondIndexRatio = ' + secondIndexRatio);

    var ratio;
    if (Math.abs(firstIndexRatio - canon5Dratio) < Math.abs(secondIndexRatio - canon5Dratio)) {
        ratio = firstIndexRatio;
        //console.log('using firstIndexRatio ' + ratio);
    } else {
        ratio = secondIndexRatio;
        //console.log('using secondIndexRatio ' + ratio);
    }

    var totalLeafEdgeWidth = parseInt(this.numLeafs * 0.1);
    var maxLeafEdgeWidth   = parseInt($('#BRcontainer').prop('clientWidth') * 0.1);
    ideal.totalLeafEdgeWidth     = Math.min(totalLeafEdgeWidth, maxLeafEdgeWidth);
    
    var widthOutsidePages = 2 * (this.twoPage.coverInternalPadding + this.twoPage.coverExternalPadding) + ideal.totalLeafEdgeWidth;
    var heightOutsidePages = 2* (this.twoPage.coverInternalPadding + this.twoPage.coverExternalPadding);
    
    ideal.width = ($('#BRcontainer').width() - widthOutsidePages) >> 1;
    ideal.width -= 10; // $$$ fudge factor
    ideal.height = $('#BRcontainer').height() - heightOutsidePages;
    ideal.height -= 20; // fudge factor
    //console.log('init idealWidth='+ideal.width+' idealHeight='+ideal.height + ' ratio='+ratio);

    if (ideal.height/ratio <= ideal.width) {
        //use height
        ideal.width = parseInt(ideal.height/ratio);
    } else {
        //use width
        ideal.height = parseInt(ideal.width*ratio);
    }
    
    // $$$ check this logic with large spreads
    ideal.reduce = ((first.height + second.height) / 2) / ideal.height;
    
    return ideal;
}

// getSpreadSizeFromReduce()
//______________________________________________________________________________
// Returns the spread size calculated from the reduction factor for the given pages
BookReader.prototype.getSpreadSizeFromReduce = function(firstIndex, secondIndex, reduce) {
    var spreadSize = {};
    // $$$ Scale this based on reduce?
    var totalLeafEdgeWidth = parseInt(this.numLeafs * 0.1);
    var maxLeafEdgeWidth   = parseInt($('#BRcontainer').prop('clientWidth') * 0.1); // $$$ Assumes leaf edge width constant at all zoom levels
    spreadSize.totalLeafEdgeWidth     = Math.min(totalLeafEdgeWidth, maxLeafEdgeWidth);

    // $$$ Possibly incorrect -- we should make height "dominant"
    var nativeWidth = this._getPageWidth(firstIndex) + this._getPageWidth(secondIndex);
    var nativeHeight = this._getPageHeight(firstIndex) + this._getPageHeight(secondIndex);
    spreadSize.height = parseInt( (nativeHeight / 2) / this.reduce );
    spreadSize.width = parseInt( (nativeWidth / 2) / this.reduce );
    spreadSize.reduce = reduce;
    
    return spreadSize;
}

// twoPageGetAutofitReduce()
//______________________________________________________________________________
// Returns the current ideal reduction factor
BookReader.prototype.twoPageGetAutofitReduce = function() {
    var spreadSize = this.getIdealSpreadSize(this.twoPage.currentIndexL, this.twoPage.currentIndexR);
    return spreadSize.reduce;
}

// twoPageSetCursor()
//______________________________________________________________________________
// Set the cursor for two page view
BookReader.prototype.twoPageSetCursor = function() {
    // console.log('setting cursor');
    if ( ($('#BRtwopageview').width() > $('#BRcontainer').prop('clientWidth')) ||
         ($('#BRtwopageview').height() > $('#BRcontainer').prop('clientHeight')) ) {
        $(this.prefetchedImgs[this.twoPage.currentIndexL]).css('cursor','move');
        $(this.prefetchedImgs[this.twoPage.currentIndexR]).css('cursor','move');
    } else {
        $(this.prefetchedImgs[this.twoPage.currentIndexL]).css('cursor','');
        $(this.prefetchedImgs[this.twoPage.currentIndexR]).css('cursor','');
    }
}

// currentIndex()
//______________________________________________________________________________
// Returns the currently active index.
BookReader.prototype.currentIndex = function() {
    // $$$ we should be cleaner with our idea of which index is active in 1up/2up
    if (this.mode == this.constMode1up) {
        return this.firstIndex; // $$$ TODO page in center of view would be better
    } else if (this.mode == this.constMode2up) {
        // Only allow indices that are actually present in book
        return BookReader.util.clamp(this.firstIndex, 0, this.numLeafs - 1);    
    } else {
        throw 'currentIndex called for unimplemented mode ' + this.mode;
    }
}

// right()
//______________________________________________________________________________
// Flip the right page over onto the left
BookReader.prototype.right = function() {
    if ('rl' != this.pageProgression) {
        // LTR
        this.next();
    } else {
        // RTL
        this.prev();
    }
}

// rightmost()
//______________________________________________________________________________
// Flip to the rightmost page
BookReader.prototype.rightmost = function() {
    if ('rl' != this.pageProgression) {
        this.last();
    } else {
        this.first();
    }
}

// left()
//______________________________________________________________________________
// Flip the left page over onto the right.
BookReader.prototype.left = function() {
    if ('rl' != this.pageProgression) {
        // LTR
        this.prev();
    } else {
        // RTL
        this.next();
    }
}

// leftmost()
//______________________________________________________________________________
// Flip to the leftmost page
BookReader.prototype.leftmost = function() {
    if ('rl' != this.pageProgression) {
        this.first();
    } else {
        this.last();
    }
}

// next()
//______________________________________________________________________________
BookReader.prototype.next = function() {
    if (2 == this.mode) {
        this.autoStop();
        this.flipFwdToIndex(null);

        //-----------------------------------------------------------------------------------
        // Call the function on the containing page to sync up the page list - MWL 02/02/2011
        if (this.firstIndex < this.lastDisplayableIndex()-1) this.syncPageList(this.firstIndex+2);
        //-----------------------------------------------------------------------------------

    } else {
        if (this.firstIndex < this.lastDisplayableIndex()) {
            this.jumpToIndex(this.firstIndex+1);
        }
    }
}

// prev()
//______________________________________________________________________________
BookReader.prototype.prev = function() {
    if (2 == this.mode) {
        this.autoStop();
        this.flipBackToIndex(null);

        //-----------------------------------------------------------------------------------
        // Call the function on the containing page to sync up the page list - MWL 02/02/2011
        if (this.firstIndex >= 2) this.syncPageList(this.firstIndex-2);
        //-----------------------------------------------------------------------------------

    } else {
        if (this.firstIndex >= 1) {
            this.jumpToIndex(this.firstIndex-1);
        }    
    }
}

BookReader.prototype.first = function() {
    this.jumpToIndex(this.firstDisplayableIndex());
}

BookReader.prototype.last = function() {
    this.jumpToIndex(this.lastDisplayableIndex());
}

// flipBackToIndex()
//______________________________________________________________________________
// to flip back one spread, pass index=null
BookReader.prototype.flipBackToIndex = function(index) {
    
    if (1 == this.mode) return;

    var leftIndex = this.twoPage.currentIndexL;
    
    if (this.animating) return;

    if (null != this.leafEdgeTmp) {
        alert('error: leafEdgeTmp should be null!');
        return;
    }
    
    if (null == index) {
        index = leftIndex-2;
    }
    //if (index<0) return;
    
    var previousIndices = this.getSpreadIndices(index);
    
    if (previousIndices[0] < this.firstDisplayableIndex() || previousIndices[1] < this.firstDisplayableIndex()) {
        return;
    }
    
    this.animating = true;
    
    if ('rl' != this.pageProgression) {
        // Assume LTR and we are going backward    
        this.prepareFlipLeftToRight(previousIndices[0], previousIndices[1]);        
        this.flipLeftToRight(previousIndices[0], previousIndices[1]);
    } else {
        // RTL and going backward
        var gutter = this.prepareFlipRightToLeft(previousIndices[0], previousIndices[1]);
        this.flipRightToLeft(previousIndices[0], previousIndices[1], gutter);
    }
}

// flipLeftToRight()
//______________________________________________________________________________
// Flips the page on the left towards the page on the right
BookReader.prototype.flipLeftToRight = function(newIndexL, newIndexR) {

    var leftLeaf = this.twoPage.currentIndexL;
    
    var oldLeafEdgeWidthL = this.leafEdgeWidth(this.twoPage.currentIndexL);
    var newLeafEdgeWidthL = this.leafEdgeWidth(newIndexL);    
    var leafEdgeTmpW = oldLeafEdgeWidthL - newLeafEdgeWidthL;
    
    var currWidthL   = this.getPageWidth2UP(leftLeaf);
    var newWidthL    = this.getPageWidth2UP(newIndexL);
    var newWidthR    = this.getPageWidth2UP(newIndexR);

    var top  = this.twoPageTop();
    var gutter = this.twoPage.middle + this.gutterOffsetForIndex(newIndexL);
    
    //console.log('leftEdgeTmpW ' + leafEdgeTmpW);
    //console.log('  gutter ' + gutter + ', scaledWL ' + scaledWL + ', newLeafEdgeWL ' + newLeafEdgeWidthL);
    
    //animation strategy:
    // 0. remove search highlight, if any.
    // 1. create a new div, called leafEdgeTmp to represent the leaf edge between the leftmost edge 
    //    of the left leaf and where the user clicked in the leaf edge.
    //    Note that if this function was triggered by left() and not a
    //    mouse click, the width of leafEdgeTmp is very small (zero px).
    // 2. animate both leafEdgeTmp to the gutter (without changing its width) and animate
    //    leftLeaf to width=0.
    // 3. When step 2 is finished, animate leafEdgeTmp to right-hand side of new right leaf
    //    (left=gutter+newWidthR) while also animating the new right leaf from width=0 to
    //    its new full width.
    // 4. After step 3 is finished, do the following:
    //      - remove leafEdgeTmp from the dom.
    //      - resize and move the right leaf edge (leafEdgeR) to left=gutter+newWidthR
    //          and width=twoPage.edgeWidth-newLeafEdgeWidthL.
    //      - resize and move the left leaf edge (leafEdgeL) to left=gutter-newWidthL-newLeafEdgeWidthL
    //          and width=newLeafEdgeWidthL.
    //      - resize the back cover (twoPage.coverDiv) to left=gutter-newWidthL-newLeafEdgeWidthL-10
    //          and width=newWidthL+newWidthR+twoPage.edgeWidth+20
    //      - move new left leaf (newIndexL) forward to zindex=2 so it can receive clicks.
    //      - remove old left and right leafs from the dom [pruneUnusedImgs()].
    //      - prefetch new adjacent leafs.
    //      - set up click handlers for both new left and right leafs.
    //      - redraw the search highlight.
    //      - update the pagenum box and the url.
    
    
    var leftEdgeTmpLeft = gutter - currWidthL - leafEdgeTmpW;

    this.leafEdgeTmp = document.createElement('div');
    $(this.leafEdgeTmp).css({
        borderStyle: 'solid none solid solid',
        borderColor: 'rgb(51, 51, 34)',
        borderWidth: '1px 0px 1px 1px',
        background: 'transparent url(' + this.imagesBaseURL + 'left_edges.png) repeat scroll 0% 0%',
        width: leafEdgeTmpW + 'px',
        height: this.twoPage.height-1 + 'px',
        left: leftEdgeTmpLeft + 'px',
        top: top+'px',    
        position: 'absolute',
        zIndex:1000
    }).appendTo('#BRtwopageview');
    
    //$(this.leafEdgeL).css('width', newLeafEdgeWidthL+'px');
    $(this.leafEdgeL).css({
        width: newLeafEdgeWidthL+'px', 
        left: gutter-currWidthL-newLeafEdgeWidthL+'px'
    });   

    // Left gets the offset of the current left leaf from the document
    var left = $(this.prefetchedImgs[leftLeaf]).offset().left;
    // $$$ This seems very similar to the gutter.  May be able to consolidate the logic.
    var right = $('#BRtwopageview').prop('clientWidth')-left-$(this.prefetchedImgs[leftLeaf]).width()+$('#BRtwopageview').offset().left-2+'px';
    
    // We change the left leaf to right positioning
    // $$$ This causes animation glitches during resize.  See https://bugs.edge.launchpad.net/gnubook/+bug/328327
    $(this.prefetchedImgs[leftLeaf]).css({
        right: right,
        left: ''
    });

    $(this.leafEdgeTmp).animate({left: gutter}, this.flipSpeed, 'easeInSine');    
    //$(this.prefetchedImgs[leftLeaf]).animate({width: '0px'}, 'slow', 'easeInSine');
    
    var self = this;

    this.removeSearchHilites();

    //console.log('animating leafLeaf ' + leftLeaf + ' to 0px');
    $(this.prefetchedImgs[leftLeaf]).animate({width: '0px'}, self.flipSpeed, 'easeInSine', function() {
    
        //console.log('     and now leafEdgeTmp to left: gutter+newWidthR ' + (gutter + newWidthR));
        $(self.leafEdgeTmp).animate({left: gutter+newWidthR+'px'}, self.flipSpeed, 'easeOutSine');

        //console.log('  animating newIndexR ' + newIndexR + ' to ' + newWidthR + ' from ' + $(self.prefetchedImgs[newIndexR]).width());
        $(self.prefetchedImgs[newIndexR]).animate({width: newWidthR+'px'}, self.flipSpeed, 'easeOutSine', function() {
            $(self.prefetchedImgs[newIndexL]).css('zIndex', 2);
            
            $(self.leafEdgeR).css({
                // Moves the right leaf edge
                width: self.twoPage.edgeWidth-newLeafEdgeWidthL+'px',
                left:  gutter+newWidthR+'px'
            });

            $(self.leafEdgeL).css({
                // Moves and resizes the left leaf edge
                width: newLeafEdgeWidthL+'px',
                left:  gutter-newWidthL-newLeafEdgeWidthL+'px'
            });

            // Resizes the brown border div
            $(self.twoPage.coverDiv).css({
                width: self.twoPageCoverWidth(newWidthL+newWidthR)+'px',
                left: gutter-newWidthL-newLeafEdgeWidthL-self.twoPage.coverInternalPadding+'px'
            });
            
            $(self.leafEdgeTmp).remove();
            self.leafEdgeTmp = null;

            // $$$ TODO refactor with opposite direction flip
            
            self.twoPage.currentIndexL = newIndexL;
            self.twoPage.currentIndexR = newIndexR;
            self.twoPage.scaledWL = newWidthL;
            self.twoPage.scaledWR = newWidthR;
            self.twoPage.gutter = gutter;
            
            self.firstIndex = self.twoPage.currentIndexL;
            self.displayedIndices = [newIndexL, newIndexR];
            self.pruneUnusedImgs();
            self.prefetch();            
            self.animating = false;
            
            self.updateSearchHilites2UP();
            self.updatePageNumBox2UP();
            
            // self.twoPagePlaceFlipAreas(); // No longer used
            self.setMouseHandlers2UP();
            self.twoPageSetCursor();
            
            if (self.animationFinishedCallback) {
                self.animationFinishedCallback();
                self.animationFinishedCallback = null;
            }
        });
    });        
    
}

// flipFwdToIndex()
//______________________________________________________________________________
// Whether we flip left or right is dependent on the page progression
// to flip forward one spread, pass index=null
BookReader.prototype.flipFwdToIndex = function(index) {

    if (this.animating) return;

    if (null != this.leafEdgeTmp) {
        alert('error: leafEdgeTmp should be null!');
        return;
    }

    if (null == index) {
        index = this.twoPage.currentIndexR+2; // $$$ assumes indices are continuous
    }
    if (index > this.lastDisplayableIndex()) return;

    this.animating = true;
    
    var nextIndices = this.getSpreadIndices(index);
    
    //console.log('flipfwd to indices ' + nextIndices[0] + ',' + nextIndices[1]);

    if ('rl' != this.pageProgression) {
        // We did not specify RTL
        var gutter = this.prepareFlipRightToLeft(nextIndices[0], nextIndices[1]);
        this.flipRightToLeft(nextIndices[0], nextIndices[1], gutter);
    } else {
        // RTL
        var gutter = this.prepareFlipLeftToRight(nextIndices[0], nextIndices[1]);
        this.flipLeftToRight(nextIndices[0], nextIndices[1]);
    }
}

// flipRightToLeft(nextL, nextR, gutter)
// $$$ better not to have to pass gutter in
//______________________________________________________________________________
// Flip from left to right and show the nextL and nextR indices on those sides
BookReader.prototype.flipRightToLeft = function(newIndexL, newIndexR) {
    var oldLeafEdgeWidthL = this.leafEdgeWidth(this.twoPage.currentIndexL);
    var oldLeafEdgeWidthR = this.twoPage.edgeWidth-oldLeafEdgeWidthL;
    var newLeafEdgeWidthL = this.leafEdgeWidth(newIndexL);  
    var newLeafEdgeWidthR = this.twoPage.edgeWidth-newLeafEdgeWidthL;

    var leafEdgeTmpW = oldLeafEdgeWidthR - newLeafEdgeWidthR;

    var top = this.twoPageTop();
    var scaledW = this.getPageWidth2UP(this.twoPage.currentIndexR);

    var middle = this.twoPage.middle;
    var gutter = middle + this.gutterOffsetForIndex(newIndexL);
    
    this.leafEdgeTmp = document.createElement('div');
    $(this.leafEdgeTmp).css({
        borderStyle: 'solid none solid solid',
        borderColor: 'rgb(51, 51, 34)',
        borderWidth: '1px 0px 1px 1px',
        background: 'transparent url(' + this.imagesBaseURL + 'left_edges.png) repeat scroll 0% 0%',
        width: leafEdgeTmpW + 'px',
        height: this.twoPage.height-1 + 'px',
        left: gutter+scaledW+'px',
        top: top+'px',    
        position: 'absolute',
        zIndex:1000
    }).appendTo('#BRtwopageview');

    var currWidthL = this.getPageWidth2UP(this.twoPage.currentIndexL);
    var currWidthR = this.getPageWidth2UP(this.twoPage.currentIndexR);
    var newWidthL = this.getPageWidth2UP(newIndexL);
    var newWidthR = this.getPageWidth2UP(newIndexR);
    
    $(this.leafEdgeR).css({width: newLeafEdgeWidthR+'px', left: gutter+newWidthR+'px' });

    var self = this; // closure-tastic!

    var speed = this.flipSpeed;

    this.removeSearchHilites();
    
    $(this.leafEdgeTmp).animate({left: gutter}, speed, 'easeInSine');    
    $(this.prefetchedImgs[this.twoPage.currentIndexR]).animate({width: '0px'}, speed, 'easeInSine', function() {
        $(self.leafEdgeTmp).animate({left: gutter-newWidthL-leafEdgeTmpW+'px'}, speed, 'easeOutSine');    
        $(self.prefetchedImgs[newIndexL]).animate({width: newWidthL+'px'}, speed, 'easeOutSine', function() {
            $(self.prefetchedImgs[newIndexR]).css('zIndex', 2);
            
            $(self.leafEdgeL).css({
                width: newLeafEdgeWidthL+'px', 
                left: gutter-newWidthL-newLeafEdgeWidthL+'px'
            });
            
            // Resizes the book cover
            $(self.twoPage.coverDiv).css({
                width: self.twoPageCoverWidth(newWidthL+newWidthR)+'px',
                left: gutter - newWidthL - newLeafEdgeWidthL - self.twoPage.coverInternalPadding + 'px'
            });
            
            $(self.leafEdgeTmp).remove();
            self.leafEdgeTmp = null;
            
            self.twoPage.currentIndexL = newIndexL;
            self.twoPage.currentIndexR = newIndexR;
            self.twoPage.scaledWL = newWidthL;
            self.twoPage.scaledWR = newWidthR;
            self.twoPage.gutter = gutter;

            self.firstIndex = self.twoPage.currentIndexL;
            self.displayedIndices = [newIndexL, newIndexR];
            self.pruneUnusedImgs();
            self.prefetch();
            self.animating = false;


            self.updateSearchHilites2UP();
            self.updatePageNumBox2UP();
            
            // self.twoPagePlaceFlipAreas(); // No longer used
            self.setMouseHandlers2UP();     
            self.twoPageSetCursor();
            
            if (self.animationFinishedCallback) {
                self.animationFinishedCallback();
                self.animationFinishedCallback = null;
            }
        });
    });    
}

// setMouseHandlers2UP
//______________________________________________________________________________
BookReader.prototype.setMouseHandlers2UP = function() {
    this.setDragHandler2UP( this.prefetchedImgs[this.twoPage.currentIndexL] );
    this.setClickHandler2UP( this.prefetchedImgs[this.twoPage.currentIndexL],
        { self: this },
        function(e) {
            e.data.self.left();
        }
    );
        
    this.setDragHandler2UP( this.prefetchedImgs[this.twoPage.currentIndexR] );
    this.setClickHandler2UP( this.prefetchedImgs[this.twoPage.currentIndexR],
        { self: this },
        function(e) {
            e.data.self.right();
        }
    );
}

// prefetchImg()
//______________________________________________________________________________
BookReader.prototype.prefetchImg = function(index) {
    var pageURI = this._getPageURI(index);

    // Load image if not loaded or URI has changed (e.g. due to scaling)
    var loadImage = false;
    if (undefined == this.prefetchedImgs[index]) {
        //console.log('no image for ' + index);
        loadImage = true;
    } else if (pageURI != this.prefetchedImgs[index].uri) {
        //console.log('uri changed for ' + index);
        loadImage = true;
    }
    
    if (loadImage) {
        //console.log('prefetching ' + index);
        var img = document.createElement("img");
        img.src = pageURI;
        img.uri = pageURI; // browser may rewrite src so we stash raw URI here
        this.prefetchedImgs[index] = img;
    }
}


// prepareFlipLeftToRight()
//
//______________________________________________________________________________
//
// Prepare to flip the left page towards the right.  This corresponds to moving
// backward when the page progression is left to right.
BookReader.prototype.prepareFlipLeftToRight = function(prevL, prevR) {

    //console.log('  preparing left->right for ' + prevL + ',' + prevR);

    this.prefetchImg(prevL);
    this.prefetchImg(prevR);
    
    var height  = this._getPageHeight(prevL); 
    var width   = this._getPageWidth(prevL);    
    var middle = this.twoPage.middle;
    var top  = this.twoPageTop();                
    var scaledW = this.twoPage.height*width/height; // $$$ assumes height of page is dominant

    // The gutter is the dividing line between the left and right pages.
    // It is offset from the middle to create the illusion of thickness to the pages
    var gutter = middle + this.gutterOffsetForIndex(prevL);
    
    //console.log('    gutter for ' + prevL + ' is ' + gutter);
    //console.log('    prevL.left: ' + (gutter - scaledW) + 'px');
    //console.log('    changing prevL ' + prevL + ' to left: ' + (gutter-scaledW) + ' width: ' + scaledW);
    
    leftCSS = {
        position: 'absolute',
        left: gutter-scaledW+'px',
        right: '', // clear right property
        top:    top+'px',
        height: this.twoPage.height,
        width:  scaledW+'px',
        backgroundColor: this.getPageBackgroundColor(prevL),
        borderRight: '1px solid black',
        zIndex: 1
    }
    
    $(this.prefetchedImgs[prevL]).css(leftCSS);

    $('#BRtwopageview').append(this.prefetchedImgs[prevL]);

    //console.log('    changing prevR ' + prevR + ' to left: ' + gutter + ' width: 0');

    rightCSS = {
        position: 'absolute',
        left:   gutter+'px',
        right: '',
        top:    top+'px',
        height: this.twoPage.height,
        width:  '0px',
        backgroundColor: this.getPageBackgroundColor(prevR),
        borderLeft: '1px solid black',
        zIndex: 2
    }
    
    $(this.prefetchedImgs[prevR]).css(rightCSS);

    $('#BRtwopageview').append(this.prefetchedImgs[prevR]);
            
}

// $$$ mang we're adding an extra pixel in the middle.  See https://bugs.edge.launchpad.net/gnubook/+bug/411667
// prepareFlipRightToLeft()
//______________________________________________________________________________
BookReader.prototype.prepareFlipRightToLeft = function(nextL, nextR) {

    //console.log('  preparing left<-right for ' + nextL + ',' + nextR);

    // Prefetch images
    this.prefetchImg(nextL);
    this.prefetchImg(nextR);

    var height  = this._getPageHeight(nextR); 
    var width   = this._getPageWidth(nextR);    
    var middle = this.twoPage.middle;
    var top  = this.twoPageTop();               
    var scaledW = this.twoPage.height*width/height;

    var gutter = middle + this.gutterOffsetForIndex(nextL);
        
    //console.log(' prepareRTL changing nextR ' + nextR + ' to left: ' + gutter);
    $(this.prefetchedImgs[nextR]).css({
        position: 'absolute',
        left:   gutter+'px',
        top:    top+'px',
        backgroundColor: this.getPageBackgroundColor(nextR),
        height: this.twoPage.height,
        width:  scaledW+'px',
        borderLeft: '1px solid black',
        zIndex: 1
    });

    $('#BRtwopageview').append(this.prefetchedImgs[nextR]);

    height  = this._getPageHeight(nextL); 
    width   = this._getPageWidth(nextL);      
    scaledW = this.twoPage.height*width/height;

    //console.log(' prepareRTL changing nextL ' + nextL + ' to right: ' + $('#BRcontainer').width()-gutter);
    $(this.prefetchedImgs[nextL]).css({
        position: 'absolute',
        right:   $('#BRtwopageview').prop('clientWidth')-gutter+'px',
        top:    top+'px',
        backgroundColor: this.getPageBackgroundColor(nextL),
        height: this.twoPage.height,
        width:  0+'px', // Start at 0 width, then grow to the left
        borderRight: '1px solid black',
        zIndex: 2
    });

    $('#BRtwopageview').append(this.prefetchedImgs[nextL]);    
            
}

// pruneUnusedImgs()
//______________________________________________________________________________
BookReader.prototype.pruneUnusedImgs = function() {
    //console.log('current: ' + this.twoPage.currentIndexL + ' ' + this.twoPage.currentIndexR);
    for (var key in this.prefetchedImgs) {
        //console.log('key is ' + key);
        if ((key != this.twoPage.currentIndexL) && (key != this.twoPage.currentIndexR)) {
            //console.log('removing key '+ key);
            $(this.prefetchedImgs[key]).remove();
        }
        if ((key < this.twoPage.currentIndexL-4) || (key > this.twoPage.currentIndexR+4)) {
            //console.log('deleting key '+ key);
            delete this.prefetchedImgs[key];
        }
    }
}

// prefetch()
//______________________________________________________________________________
BookReader.prototype.prefetch = function() {

    // prefetch visible pages first
    this.prefetchImg(this.twoPage.currentIndexL);
    this.prefetchImg(this.twoPage.currentIndexR);
    
    var adjacentPagesToLoad = 3;
    
    var lowCurrent = Math.min(this.twoPage.currentIndexL, this.twoPage.currentIndexR);
    var highCurrent = Math.max(this.twoPage.currentIndexL, this.twoPage.currentIndexR);
        
    var start = Math.max(lowCurrent - adjacentPagesToLoad, 0);
    var end = Math.min(highCurrent + adjacentPagesToLoad, this.numLeafs - 1);
    
    // Load images spreading out from current
    for (var i = 1; i <= adjacentPagesToLoad; i++) {
        var goingDown = lowCurrent - i;
        if (goingDown >= start) {
            this.prefetchImg(goingDown);
        }
        var goingUp = highCurrent + i;
        if (goingUp <= end) {
            this.prefetchImg(goingUp);
        }
    }
}

// getPageWidth2UP()
//______________________________________________________________________________
BookReader.prototype.getPageWidth2UP = function(index) {
    // We return the width based on the dominant height
    var height  = this._getPageHeight(index); 
    var width   = this._getPageWidth(index);    
    return Math.floor(this.twoPage.height*width/height); // $$$ we assume width is relative to current spread
}    

// search()
//______________________________________________________________________________
BookReader.prototype.search = function(term) {
    term = term.replace(/\//g, ' '); // strip slashes
    this.searchTerm = term;
    $('#BookReaderSearchScript').remove();
 	var script  = document.createElement("script");
 	script.setAttribute('id', 'BookReaderSearchScript');
	script.setAttribute("type", "text/javascript");
	script.setAttribute("src", '/BookReader/flipbook_search_br.php?url='+escape(this.bookPath + '_djvu.xml')+'&term='+term+'&format=XML&callback=br.BRSearchCallback');
	document.getElementsByTagName('head')[0].appendChild(script);
	$('#BookReaderSearchBox').val(term);
	$('#BookReaderSearchResults').html('Searching...');
}

// BRSearchCallback()
//______________________________________________________________________________
BookReader.prototype.BRSearchCallback = function(txt) {
    //alert(txt);
    if (jQuery.browser.msie) {
        var dom=new ActiveXObject("Microsoft.XMLDOM");
        dom.async="false";
        dom.loadXML(txt);    
    } else {
        var parser = new DOMParser();
        var dom = parser.parseFromString(txt, "text/xml");    
    }
    
    $('#BookReaderSearchResults').empty();    
    $('#BookReaderSearchResults').append('<ul>');
    
    for (var key in this.searchResults) {
        if (null != this.searchResults[key].div) {
            $(this.searchResults[key].div).remove();
        }
        delete this.searchResults[key];
    }
    
    var pages = dom.getElementsByTagName('PAGE');
    
    if (0 == pages.length) {
        // $$$ it would be nice to echo the (sanitized) search result here
        $('#BookReaderSearchResults').append('<li>No search results found</li>');
    } else {    
        for (var i = 0; i < pages.length; i++){
            //console.log(pages[i].getAttribute('file').substr(1) +'-'+ parseInt(pages[i].getAttribute('file').substr(1), 10));
    
            
            var re = new RegExp (/_(\d{4})/);
            var reMatch = re.exec(pages[i].getAttribute('file'));
            var index = parseInt(reMatch[1], 10);
            //var index = parseInt(pages[i].getAttribute('file').substr(1), 10);
            
            var children = pages[i].childNodes;
            var context = '';
            for (var j=0; j<children.length; j++) {
                //console.log(j + ' - ' + children[j].nodeName);
                //console.log(children[j].firstChild.nodeValue);
                if ('CONTEXT' == children[j].nodeName) {
                    context += children[j].firstChild.nodeValue;
                } else if ('WORD' == children[j].nodeName) {
                    context += '<b>'+children[j].firstChild.nodeValue+'</b>';
                    
                    var index = this.leafNumToIndex(index);
                    if (null != index) {
                        //coordinates are [left, bottom, right, top, [baseline]]
                        //we'll skip baseline for now...
                        var coords = children[j].getAttribute('coords').split(',',4);
                        if (4 == coords.length) {
                            this.searchResults[index] = {'l':parseInt(coords[0]), 'b':parseInt(coords[1]), 'r':parseInt(coords[2]), 't':parseInt(coords[3]), 'div':null};
                        }
                    }
                }
            }
            var pageName = this.getPageName(index);
            var middleX = (this.searchResults[index].l + this.searchResults[index].r) >> 1;
            var middleY = (this.searchResults[index].t + this.searchResults[index].b) >> 1;
            //TODO: remove hardcoded instance name
            $('#BookReaderSearchResults').append('<li><b><a href="javascript:br.jumpToIndex('+index+','+middleX+','+middleY+');">' + pageName + '</a></b> - ' + context + '</li>');
        }
    }
    $('#BookReaderSearchResults').append('</ul>');

    // $$$ update again for case of loading search URL in new browser window (search box may not have been ready yet)
	$('#BookReaderSearchBox').val(this.searchTerm);

    this.updateSearchHilites();
}

// updateSearchHilites()
//______________________________________________________________________________
BookReader.prototype.updateSearchHilites = function() {
    if (2 == this.mode) {
        this.updateSearchHilites2UP();
    } else {
        this.updateSearchHilites1UP();
    }
}

// showSearchHilites1UP()
//______________________________________________________________________________
BookReader.prototype.updateSearchHilites1UP = function() {

    for (var key in this.searchResults) {
        
        if (-1 != jQuery.inArray(parseInt(key), this.displayedIndices)) {
            var result = this.searchResults[key];
            if(null == result.div) {
                result.div = document.createElement('div');
                $(result.div).attr('className', 'BookReaderSearchHilite').appendTo('#pagediv'+key);
                //console.log('appending ' + key);
            }    
            $(result.div).css({
                width:  (result.r-result.l)/this.reduce + 'px',
                height: (result.b-result.t)/this.reduce + 'px',
                left:   (result.l)/this.reduce + 'px',
                top:    (result.t)/this.reduce +'px'
            });

        } else {
            //console.log(key + ' not displayed');
            this.searchResults[key].div=null;
        }
    }
}

// twoPageGutter()
//______________________________________________________________________________
// Returns the position of the gutter (line between the page images)
BookReader.prototype.twoPageGutter = function() {
    return this.twoPage.middle + this.gutterOffsetForIndex(this.twoPage.currentIndexL);
}

// twoPageTop()
//______________________________________________________________________________
// Returns the offset for the top of the page images
BookReader.prototype.twoPageTop = function() {
    return this.twoPage.coverExternalPadding + this.twoPage.coverInternalPadding; // $$$ + border?
}

// twoPageCoverWidth()
//______________________________________________________________________________
// Returns the width of the cover div given the total page width
BookReader.prototype.twoPageCoverWidth = function(totalPageWidth) {
    return totalPageWidth + this.twoPage.edgeWidth + 2*this.twoPage.coverInternalPadding;
}

// twoPageGetViewCenter()
//______________________________________________________________________________
// Returns the percentage offset into twopageview div at the center of container div
// { percentageX: float, percentageY: float }
BookReader.prototype.twoPageGetViewCenter = function() {
    var center = {};

    var containerOffset = $('#BRcontainer').offset();
    var viewOffset = $('#BRtwopageview').offset();
    center.percentageX = (containerOffset.left - viewOffset.left + ($('#BRcontainer').prop('clientWidth') >> 1)) / this.twoPage.totalWidth;
    center.percentageY = (containerOffset.top - viewOffset.top + ($('#BRcontainer').prop('clientHeight') >> 1)) / this.twoPage.totalHeight;
    
    return center;
}

// twoPageCenterView(percentageX, percentageY)
//______________________________________________________________________________
// Centers the point given by percentage from left,top of twopageview
BookReader.prototype.twoPageCenterView = function(percentageX, percentageY) {
    if ('undefined' == typeof(percentageX)) {
        percentageX = 0.5;
    }
    if ('undefined' == typeof(percentageY)) {
        percentageY = 0.5;
    }

    var viewWidth = $('#BRtwopageview').width();
    var containerClientWidth = $('#BRcontainer').prop('clientWidth');
    var intoViewX = percentageX * viewWidth;
    
    var viewHeight = $('#BRtwopageview').height();
    var containerClientHeight = $('#BRcontainer').prop('clientHeight');
    var intoViewY = percentageY * viewHeight;
    
    if (viewWidth < containerClientWidth) {
        // Can fit width without scrollbars - center by adjusting offset
        $('#BRtwopageview').css('left', (containerClientWidth >> 1) - intoViewX + 'px');    
    } else {
        // Need to scroll to center
        $('#BRtwopageview').css('left', 0);
        $('#BRcontainer').scrollLeft(intoViewX - (containerClientWidth >> 1));
    }
    
    if (viewHeight < containerClientHeight) {
        // Fits with scrollbars - add offset
        $('#BRtwopageview').css('top', (containerClientHeight >> 1) - intoViewY + 'px');
    } else {
        $('#BRtwopageview').css('top', 0);
        $('#BRcontainer').scrollTop(intoViewY - (containerClientHeight >> 1));
    }
}

// twoPageFlipAreaHeight
//______________________________________________________________________________
// Returns the integer height of the click-to-flip areas at the edges of the book
BookReader.prototype.twoPageFlipAreaHeight = function() {
    return parseInt(this.twoPage.height);
}

// twoPageFlipAreaWidth
//______________________________________________________________________________
// Returns the integer width of the flip areas 
BookReader.prototype.twoPageFlipAreaWidth = function() {
    var max = 100; // $$$ TODO base on view width?
    var min = 10;
    
    var width = this.twoPage.width * 0.15;
    return parseInt(BookReader.util.clamp(width, min, max));
}

// twoPageFlipAreaTop
//______________________________________________________________________________
// Returns integer top offset for flip areas
BookReader.prototype.twoPageFlipAreaTop = function() {
    return parseInt(this.twoPage.bookCoverDivTop + this.twoPage.coverInternalPadding);
}

// twoPageLeftFlipAreaLeft
//______________________________________________________________________________
// Left offset for left flip area
BookReader.prototype.twoPageLeftFlipAreaLeft = function() {
    return parseInt(this.twoPage.gutter - this.twoPage.scaledWL);
}

// twoPageRightFlipAreaLeft
//______________________________________________________________________________
// Left offset for right flip area
BookReader.prototype.twoPageRightFlipAreaLeft = function() {
    return parseInt(this.twoPage.gutter + this.twoPage.scaledWR - this.twoPageFlipAreaWidth());
}

// twoPagePlaceFlipAreas
//______________________________________________________________________________
// Readjusts position of flip areas based on current layout
BookReader.prototype.twoPagePlaceFlipAreas = function() {
    // We don't set top since it shouldn't change relative to view
    $(this.twoPage.leftFlipArea).css({
        left: this.twoPageLeftFlipAreaLeft() + 'px',
        width: this.twoPageFlipAreaWidth() + 'px'
    });
    $(this.twoPage.rightFlipArea).css({
        left: this.twoPageRightFlipAreaLeft() + 'px',
        width: this.twoPageFlipAreaWidth() + 'px'
    });
}
    
// showSearchHilites2UP()
//______________________________________________________________________________
BookReader.prototype.updateSearchHilites2UP = function() {

    for (var key in this.searchResults) {
        key = parseInt(key, 10);
        if (-1 != jQuery.inArray(key, this.displayedIndices)) {
            var result = this.searchResults[key];
            if(null == result.div) {
                result.div = document.createElement('div');
                $(result.div).attr('className', 'BookReaderSearchHilite').css('zIndex', 3).appendTo('#BRtwopageview');
                //console.log('appending ' + key);
            }

            // We calculate the reduction factor for the specific page because it can be different
            // for each page in the spread
            var height = this._getPageHeight(key);
            var width  = this._getPageWidth(key)
            var reduce = this.twoPage.height/height;
            var scaledW = parseInt(width*reduce);
            
            var gutter = this.twoPageGutter();
            var pageL;
            if ('L' == this.getPageSide(key)) {
                pageL = gutter-scaledW;
            } else {
                pageL = gutter;
            }
            var pageT  = this.twoPageTop();
            
            $(result.div).css({
                width:  (result.r-result.l)*reduce + 'px',
                height: (result.b-result.t)*reduce + 'px',
                left:   pageL+(result.l)*reduce + 'px',
                top:    pageT+(result.t)*reduce +'px'
            });

        } else {
            //console.log(key + ' not displayed');
            if (null != this.searchResults[key].div) {
                //console.log('removing ' + key);
                $(this.searchResults[key].div).remove();
            }
            this.searchResults[key].div=null;
        }
    }
}

// removeSearchHilites()
//______________________________________________________________________________
BookReader.prototype.removeSearchHilites = function() {
    for (var key in this.searchResults) {
        if (null != this.searchResults[key].div) {
            $(this.searchResults[key].div).remove();
            this.searchResults[key].div=null;
        }        
    }
}

// printPage
//______________________________________________________________________________
BookReader.prototype.printPage = function() {
    window.open(this.getPrintURI(), 'printpage', 'width=400, height=500, resizable=yes, scrollbars=yes, toolbar=no, location=no');
}

// Get print URI from current indices and mode
BookReader.prototype.getPrintURI = function() {
    var indexToPrint;
    if (this.constMode2up == this.mode) {
        indexToPrint = this.twoPage.currentIndexL;
    } else {
        indexToPrint = this.firstIndex; // $$$ the index in the middle of the viewport would make more sense
    }

    var options = 'type=' + this.bhlObjectType + '&id=' + this.bhlObjectId + '&file=' + indexToPrint + '&width=' +
        this._getPageWidth(indexToPrint) + '&height=' + this._getPageHeight(indexToPrint);

    if (this.constMode2up == this.mode) {
        options += '&file2=' + this.twoPage.currentIndexR + '&width2=' + this._getPageWidth(this.twoPage.currentIndexR);

        options += '&height2=' + this._getPageHeight(this.twoPage.currentIndexR);
        options += '&title=' + encodeURIComponent(this.shortTitle(50) + ' - Pages ' + this.getPageNum(this.twoPage.currentIndexL) + ', ' + this.getPageNum(this.twoPage.currentIndexR));
    } else {
        options += '&title=' + encodeURIComponent(this.shortTitle(50) + ' - Page ' + this.getPageNum(indexToPrint));
    }

    return '/Controls/OLBookReader/Viewer/print.aspx?' + options;
}

// showEmbedCode()
//______________________________________________________________________________
BookReader.prototype.showEmbedCode = function() {
    if (null != this.embedPopup) { // check if already showing
        return;
    }
    this.autoStop();
    this.embedPopup = document.createElement("div");
    $(this.embedPopup).css({
        position: 'absolute',
        top:      '20px',
        left:     ($('#BRcontainer').prop('clientWidth')-400)/2 + 'px',
        width:    '400px',
        padding:  "20px",
        border:   "3px double #999999",
        zIndex:   3,
        backgroundColor: "#fff"
    }).appendTo('#BookReader');

    htmlStr =  '<p style="text-align:center;"><b>Embed Bookreader in your blog!</b></p>';
    htmlStr += '<p>The bookreader uses iframes for embedding. It will not work on web hosts that block iframes. The embed feature has been tested on blogspot.com blogs as well as self-hosted Wordpress blogs. This feature will NOT work on wordpress.com blogs.</p>';
    htmlStr += '<p>Embed Code: <input type="text" size="40" value="' + this.getEmbedCode() + '"></p>';
    htmlStr += '<p style="text-align:center;"><a href="" onclick="br.embedPopup = null; $(this.parentNode.parentNode).remove(); return false">Close popup</a></p>';    

    this.embedPopup.innerHTML = htmlStr;
    $(this.embedPopup).find('input').on('click', function() {
        this.select();
    })
}

// autoToggle()
//______________________________________________________________________________
BookReader.prototype.autoToggle = function() {

    var bComingFrom1up = false;
    if (2 != this.mode) {
        bComingFrom1up = true;
        this.switchMode(2);
    }
    
    // Change to autofit if book is too large
    if (this.reduce < this.twoPageGetAutofitReduce()) {
        this.zoom2up(0);
    }

    var self = this;
    if (null == this.autoTimer) {
        this.flipSpeed = 2000;
        
        // $$$ Draw events currently cause layout problems when they occur during animation.
        //     There is a specific problem when changing from 1-up immediately to autoplay in RTL so
        //     we workaround for now by not triggering immediate animation in that case.
        //     See https://bugs.launchpad.net/gnubook/+bug/328327
        if (('rl' == this.pageProgression) && bComingFrom1up) {
            // don't flip immediately -- wait until timer fires
        } else {
            // flip immediately
            this.flipFwdToIndex();        
        }

        $('#BRtoolbar .play').hide();
        $('#BRtoolbar .pause').show();
        this.autoTimer=setInterval(function(){
            if (self.animating) {return;}
            
            if (Math.max(self.twoPage.currentIndexL, self.twoPage.currentIndexR) >= self.lastDisplayableIndex()) {
                self.flipBackToIndex(1); // $$$ really what we want?
            } else {            
                self.flipFwdToIndex();
            }
        },5000);
    } else {
        this.autoStop();
    }
}

// autoStop()
//______________________________________________________________________________
// Stop autoplay mode, allowing animations to finish
BookReader.prototype.autoStop = function() {
    if (null != this.autoTimer) {
        clearInterval(this.autoTimer);
        this.flipSpeed = 'fast';
        $('#BRtoolbar .pause').hide();
        $('#BRtoolbar .play').show();
        this.autoTimer = null;
    }
}

// stopFlipAnimations
//______________________________________________________________________________
// Immediately stop flip animations.  Callbacks are triggered.
BookReader.prototype.stopFlipAnimations = function() {

    this.autoStop(); // Clear timers

    // Stop animation, clear queue, trigger callbacks
    if (this.leafEdgeTmp) {
        $(this.leafEdgeTmp).stop(false, true);
    }
    jQuery.each(this.prefetchedImgs, function() {
        $(this).stop(false, true);
        });

    // And again since animations also queued in callbacks
    if (this.leafEdgeTmp) {
        $(this.leafEdgeTmp).stop(false, true);
    }
    jQuery.each(this.prefetchedImgs, function() {
        $(this).stop(false, true);
        });
   
}

// keyboardNavigationIsDisabled(event)
//   - returns true if keyboard navigation should be disabled for the event
//______________________________________________________________________________
BookReader.prototype.keyboardNavigationIsDisabled = function(event) {
    if (event.target.tagName == "INPUT") {
        return true;
    }   
    return false;
}

// gutterOffsetForIndex
//______________________________________________________________________________
//
// Returns the gutter offset for the spread containing the given index.
// This function supports RTL
BookReader.prototype.gutterOffsetForIndex = function(pindex) {

    // To find the offset of the gutter from the middle we calculate our percentage distance
    // through the book (0..1), remap to (-0.5..0.5) and multiply by the total page edge width
    var offset = parseInt(((pindex / this.numLeafs) - 0.5) * this.twoPage.edgeWidth);
    
    // But then again for RTL it's the opposite
    if ('rl' == this.pageProgression) {
        offset = -offset;
    }
    
    return offset;
}

// leafEdgeWidth
//______________________________________________________________________________
// Returns the width of the leaf edge div for the page with index given
BookReader.prototype.leafEdgeWidth = function(pindex) {
    // $$$ could there be single pixel rounding errors for L vs R?
    if ((this.getPageSide(pindex) == 'L') && (this.pageProgression != 'rl')) {
        return parseInt( (pindex/this.numLeafs) * this.twoPage.edgeWidth + 0.5);
    } else {
        return parseInt( (1 - pindex/this.numLeafs) * this.twoPage.edgeWidth + 0.5);
    }
}

// jumpIndexForLeftEdgePageX
//______________________________________________________________________________
// Returns the target jump leaf given a page coordinate (inside the left page edge div)
BookReader.prototype.jumpIndexForLeftEdgePageX = function(pageX) {
    if ('rl' != this.pageProgression) {
        // LTR - flipping backward
        var jumpIndex = this.twoPage.currentIndexL - ($(this.leafEdgeL).offset().left + $(this.leafEdgeL).width() - pageX) * 10;

        // browser may have resized the div due to font size change -- see https://bugs.launchpad.net/gnubook/+bug/333570        
        jumpIndex = BookReader.util.clamp(Math.round(jumpIndex), this.firstDisplayableIndex(), this.twoPage.currentIndexL - 2);
        return jumpIndex;

    } else {
        var jumpIndex = this.twoPage.currentIndexL + ($(this.leafEdgeL).offset().left + $(this.leafEdgeL).width() - pageX) * 10;
        jumpIndex = BookReader.util.clamp(Math.round(jumpIndex), this.twoPage.currentIndexL + 2, this.lastDisplayableIndex());
        return jumpIndex;
    }
}

// jumpIndexForRightEdgePageX
//______________________________________________________________________________
// Returns the target jump leaf given a page coordinate (inside the right page edge div)
BookReader.prototype.jumpIndexForRightEdgePageX = function(pageX) {
    if ('rl' != this.pageProgression) {
        // LTR
        var jumpIndex = this.twoPage.currentIndexR + (pageX - $(this.leafEdgeR).offset().left) * 10;
        jumpIndex = BookReader.util.clamp(Math.round(jumpIndex), this.twoPage.currentIndexR + 2, this.lastDisplayableIndex());
        return jumpIndex;
    } else {
        var jumpIndex = this.twoPage.currentIndexR - (pageX - $(this.leafEdgeR).offset().left) * 10;
        jumpIndex = BookReader.util.clamp(Math.round(jumpIndex), this.firstDisplayableIndex(), this.twoPage.currentIndexR - 2);
        return jumpIndex;
    }
}

BookReader.prototype.initToolbar = function(mode, ui) {

    const bookReader = document.getElementById("BookReader");

    const toolbar = document.createElement("div");
    toolbar.id = "BRtoolbar";

    // Toolbar Buttons (right side)
    const toolbarButtons = document.createElement("span");
    toolbarButtons.id = "BRtoolbarbuttons";
    toolbarButtons.style.float = "right";

    const printButton = document.createElement("button");
    printButton.className = "BRicon print rollover";
    toolbarButtons.appendChild(printButton);

    // Mode 2 toolbar
    const mode2 = document.createElement("div");
    mode2.className = "BRtoolbarmode2";
    mode2.style.display = "none";
    ["book_leftmost", "book_left", "book_right", "book_rightmost"].forEach(cls => {
        const btn = document.createElement("button");
        btn.className = `BRicon rollover ${cls}`;
        mode2.appendChild(btn);
    });
    toolbarButtons.appendChild(mode2);

    // Mode 1 toolbar
    const mode1 = document.createElement("div");
    mode1.className = "BRtoolbarmode1";
    mode1.style.display = "none";
    ["book_top", "book_up", "book_down", "book_bottom"].forEach(cls => {
        const btn = document.createElement("button");
        btn.className = `BRicon rollover ${cls}`;
        mode1.appendChild(btn);
    });
    toolbarButtons.appendChild(mode1);

    toolbar.appendChild(toolbarButtons);

    // Zoom and mode controls
    const controlSpan = document.createElement("span");

    const zoomOutBtn = document.createElement("button");
    zoomOutBtn.className = "BRicon rollover zoom_out";
    zoomOutBtn.onclick = () => { br.zoom(-1); return false; };
    controlSpan.appendChild(zoomOutBtn);

    const zoomInBtn = document.createElement("button");
    zoomInBtn.className = "BRicon rollover zoom_in";
    zoomInBtn.onclick = () => { br.zoom(1); return false; };
    controlSpan.appendChild(zoomInBtn);

    // Zoom label
    const zoomLabel = document.createElement("span");
    zoomLabel.className = "label";
    zoomLabel.innerHTML = `Zoom: <span id="BRzoom">${parseInt(100 / this.reduce)}</span>`;
    controlSpan.appendChild(zoomLabel);

    // Mode buttons
    const onePageBtn = document.createElement("button");
    onePageBtn.className = "BRicon rollover one_page_mode";
    onePageBtn.onclick = () => { br.switchMode(1); return false; };
    controlSpan.appendChild(onePageBtn);

    const twoPageBtn = document.createElement("button");
    twoPageBtn.className = "BRicon rollover two_page_mode";
    twoPageBtn.onclick = () => { br.switchMode(2); return false; };
    controlSpan.appendChild(twoPageBtn);

    toolbar.appendChild(controlSpan);

    // Book title
    const titleSpan = document.createElement("span");
    titleSpan.id = "BRbooktitle"; // Removed extra '#' from original

    const titleLink = document.createElement("a");
    titleLink.className = "BRblack title";
    titleLink.href = this.bookUrl;
    titleLink.target = "_blank";
    titleLink.textContent = this.shortTitle(50);
    titleSpan.innerHTML = "&nbsp;&nbsp;";
    titleSpan.appendChild(titleLink);

    toolbar.appendChild(titleSpan);

    // Append toolbar to BookReader
    bookReader.appendChild(toolbar);
    
    this.updateToolbarZoom(this.reduce); // Pretty format
        
    // $$$ turn this into a member variable
    var jToolbar = $('#BRtoolbar'); // j prefix indicates jQuery object
    
    // We build in mode 2
    jToolbar.append();

    this.bindToolbarNavHandlers(jToolbar);
    
    // Setup tooltips -- later we could load these from a file for i18n
    var titles = { '.logo': 'Go to Archive.org',
                   '.zoom_in': 'Zoom in',
                   '.zoom_out': 'Zoom out',
                   '.one_page_mode': 'One-page view',
                   '.two_page_mode': 'Two-page view',
                   '.print': 'Print/download this page',
                   '.embed': 'Embed bookreader',
                   '.book_left': 'Flip left',
                   '.book_right': 'Flip right',
                   '.book_up': 'Page up',
                   '.book_down': 'Page down',
                   '.play': 'Play',
                   '.pause': 'Pause',
                   '.book_top': 'First page',
                   '.book_bottom': 'Last page'
                  };
    if ('rl' == this.pageProgression) {
        titles['.book_leftmost'] = 'Last page';
        titles['.book_rightmost'] = 'First page';
    } else { // LTR
        titles['.book_leftmost'] = 'First page';
        titles['.book_rightmost'] = 'Last page';
    }
                  
    for (var icon in titles) {
        jToolbar.find(icon).attr('title', titles[icon]);
    }
    
    // Hide mode buttons and autoplay if 2up is not available
    // $$$ if we end up with more than two modes we should show the applicable buttons
    if ( !this.canSwitchToMode(this.constMode2up) ) {
        jToolbar.find('.one_page_mode, .two_page_mode, .play, .pause').hide();
    }

    // Switch to requested mode -- binds other click handlers
    this.switchToolbarMode(mode);
    
}


// switchToolbarMode
//______________________________________________________________________________
// Update the toolbar for the given mode (changes navigation buttons)
// $$$ we should soon split the toolbar out into its own module
BookReader.prototype.switchToolbarMode = function(mode) {
    if (1 == mode) {
        // 1-up     
        $('#BRtoolbar .BRtoolbarmode2').hide();
        $('#BRtoolbar .BRtoolbarmode1').show().css('display', 'inline');
    } else {
        // 2-up
        $('#BRtoolbar .BRtoolbarmode1').hide();
        $('#BRtoolbar .BRtoolbarmode2').show().css('display', 'inline');
    }
}

// bindToolbarNavHandlers
//______________________________________________________________________________
// Binds the toolbar handlers
BookReader.prototype.bindToolbarNavHandlers = function(jToolbar) {

    var self = this; // closure

    jToolbar.find('.book_left').on('click', function(e) {
        self.left();
        return false;
    });
         
    jToolbar.find('.book_right').on('click', function(e) {
        self.right();
        return false;
    });
        
    jToolbar.find('.book_up').on('click', function(e) {
        self.prev();
        return false;
    });        
        
    jToolbar.find('.book_down').on('click', function(e) {
        self.next();
        return false;
    });

    jToolbar.find('.print').on('click', function(e) {
        self.printPage();
        return false;
    });
        
    jToolbar.find('.embed').on('click', function(e) {
        self.showEmbedCode();
        return false;
    });

    jToolbar.find('.play').on('click', function(e) {
        self.autoToggle();
        return false;
    });

    jToolbar.find('.pause').on('click', function(e) {
        self.autoToggle();
        return false;
    });
    
    jToolbar.find('.book_top').on('click', function(e) {
        self.first();
        return false;
    });

    jToolbar.find('.book_bottom').on('click', function(e) {
        self.last();
        return false;
    });
    
    jToolbar.find('.book_leftmost').on('click', function(e) {
        self.leftmost();
        return false;
    });
  
    jToolbar.find('.book_rightmost').on('click', function(e) {
        self.rightmost();
        return false;
    });
}

// updateToolbarZoom(reduce)
//______________________________________________________________________________
// Update the displayed zoom factor based on reduction factor
BookReader.prototype.updateToolbarZoom = function(reduce) {
    var value;
    if (this.constMode2up == this.mode && this.twoPage.autofit) {
        value = 'Auto';
    } else {
        value = (100 / reduce).toFixed(2);
        // Strip trailing zeroes and decimal if all zeroes
        value = value.replace(/0+$/,'');
        value = value.replace(/\.$/,'');
        value += '%';
    }
    $('#BRzoom').text(value);
}

// firstDisplayableIndex
//______________________________________________________________________________
// Returns the index of the first visible page, dependent on the mode.
// $$$ Currently we cannot display the front/back cover in 2-up and will need to update
// this function when we can as part of https://bugs.launchpad.net/gnubook/+bug/296788
BookReader.prototype.firstDisplayableIndex = function() {
    if (this.mode != this.constMode2up) {
        return 0;
    }
    
    if ('rl' != this.pageProgression) {
        // LTR
        if (this.getPageSide(0) == 'L') {
            return 0;
        } else {
            return -1;
        }
    } else {
        // RTL
        if (this.getPageSide(0) == 'R') {
            return 0;
        } else {
            return -1;
        }
    }
}

// lastDisplayableIndex
//______________________________________________________________________________
// Returns the index of the last visible page, dependent on the mode.
// $$$ Currently we cannot display the front/back cover in 2-up and will need to update
// this function when we can as pa  rt of https://bugs.launchpad.net/gnubook/+bug/296788
BookReader.prototype.lastDisplayableIndex = function() {

    var lastIndex = this.numLeafs - 1;
    
    if (this.mode != this.constMode2up) {
        return lastIndex;
    }

    if ('rl' != this.pageProgression) {
        // LTR
        if (this.getPageSide(lastIndex) == 'R') {
            return lastIndex;
        } else {
            return lastIndex + 1;
        }
    } else {
        // RTL
        if (this.getPageSide(lastIndex) == 'L') {
            return lastIndex;
        } else {
            return lastIndex + 1;
        }
    }
}

// shortTitle(maximumCharacters)
//________
// Returns a shortened version of the title with the maximum number of characters
BookReader.prototype.shortTitle = function(maximumCharacters) {
    if (this.bookTitle.length < maximumCharacters) {
        return this.bookTitle;
    }
    
    var title = this.bookTitle.substr(0, maximumCharacters - 3);
    title += '...';
    return title;
}

// Parameter related functions

// updateFromParams(params)
//________
// Update ourselves from the params object.
//
// e.g. this.updateFromParams(this.paramsFromFragment(window.location.hash))
BookReader.prototype.updateFromParams = function(params) {
    if ('undefined' != typeof(params.mode)) {
        this.switchMode(params.mode);
    }

    // process /search
    if ('undefined' != typeof(params.searchTerm)) {
        if (this.searchTerm != params.searchTerm) {
            this.search(params.searchTerm);
        }
    }
    
    // $$$ process /zoom
    
    // We only respect page if index is not set
    if ('undefined' != typeof(params.index)) {
        if (params.index != this.currentIndex()) {
            this.jumpToIndex(params.index);
        }
    } else if ('undefined' != typeof(params.page)) {
        // $$$ this assumes page numbers are unique
        if (params.page != this.getPageNum(this.currentIndex())) {
            this.jumpToPage(params.page);
        }
    }
    
    // $$$ process /region
    // $$$ process /highlight
}

// paramsFromFragment(urlFragment)
//________
// Returns a object with configuration parametes from a URL fragment.
//
// E.g paramsFromFragment(window.location.hash)
BookReader.prototype.paramsFromFragment = function(urlFragment) {
    // URL fragment syntax specification: http://openlibrary.org/dev/docs/bookurls
    
    var params = {};
    
    // For convenience we allow an initial # character (as from window.location.hash)
    // but don't require it
    if (urlFragment.substr(0,1) == '#') {
        urlFragment = urlFragment.substr(1);
    }
    
    // Simple #nn syntax
    var oldStyleLeafNum = parseInt( /^\d+$/.exec(urlFragment) );
    if ( !isNaN(oldStyleLeafNum) ) {
        params.index = oldStyleLeafNum;
        
        // Done processing if using old-style syntax
        return params;
    }
    
    // Split into key-value pairs
    var urlArray = urlFragment.split('/');
    var urlHash = {};
    for (var i = 0; i < urlArray.length; i += 2) {
        urlHash[urlArray[i]] = urlArray[i+1];
    }
    
    // Mode
    if ('1up' == urlHash['mode']) {
        params.mode = this.constMode1up;
    } else if ('2up' == urlHash['mode']) {
        params.mode = this.constMode2up;
    }
    
    // Index and page
    if ('undefined' != typeof(urlHash['page'])) {
        // page was set -- may not be int
        params.page = urlHash['page'];
    }
    
    // $$$ process /region
    // $$$ process /search
    
    if (urlHash['search'] != undefined) {
        params.searchTerm = BookReader.util.decodeURIComponentPlus(urlHash['search']);
    }
    
    // $$$ process /highlight
        
    return params;
}

// paramsFromCurrent()
//________
// Create a params object from the current parameters.
BookReader.prototype.paramsFromCurrent = function() {

    var params = {};
    
    var index = this.currentIndex();
    var pageNum = this.getPageNum(index);
    if ((pageNum === 0) || pageNum) {
        params.page = pageNum;
    }
    
    params.index = index;
    params.mode = this.mode;
    
    // $$$ highlight
    // $$$ region

    // search    
    if (this.searchHighlightVisible()) {
        params.searchTerm = this.searchTerm;
    }
    
    return params;
}

// fragmentFromParams(params)
//________
// Create a fragment string from the params object.
// See http://openlibrary.org/dev/docs/bookurls for an explanation of the fragment syntax.
BookReader.prototype.fragmentFromParams = function(params) {
    var separator = '/';
    
    var fragments = [];
    
    if ('undefined' != typeof(params.page)) {
        fragments.push('page', params.page);
    } else {
        // Don't have page numbering -- use index instead
        fragments.push('page', 'n' + params.index);
    }
    
    // $$$ highlight
    // $$$ region
    
    // mode
    if ('undefined' != typeof(params.mode)) {    
        if (params.mode == this.constMode1up) {
            fragments.push('mode', '1up');
        } else if (params.mode == this.constMode2up) {
            fragments.push('mode', '2up');
        } else {
            throw 'fragmentFromParams called with unknown mode ' + params.mode;
        }
    }
    
    // search
    if (params.searchTerm) {
        fragments.push('search', params.searchTerm);
    }
    
    return BookReader.util.encodeURIComponentPlus(fragments.join(separator)).replace(/%2F/g, '/');
}

// getPageIndex(pageNum)
//________
// Returns the *highest* index the given page number, or undefined
BookReader.prototype.getPageIndex = function(pageNum) {
    var pageIndices = this.getPageIndices(pageNum);
    
    if (pageIndices.length > 0) {
        return pageIndices[pageIndices.length - 1];
    }

    return undefined;
}

// getPageIndices(pageNum)
//________
// Returns an array (possibly empty) of the indices with the given page number
BookReader.prototype.getPageIndices = function(pageNum) {
    var indices = [];

    // Check for special "nXX" page number
    if (pageNum.slice(0,1) == 'n') {
        try {
            var pageIntStr = pageNum.slice(1, pageNum.length);
            var pageIndex = parseInt(pageIntStr);
            indices.push(pageIndex);
            return indices;
        } catch(err) {
            // Do nothing... will run through page names and see if one matches
        }
    }

    var i;
    for (i=0; i<this.numLeafs; i++) {
        if (this.getPageNum(i) == pageNum) {
            indices.push(i);
        }
    }
    
    return indices;
}

// getPageName(index)
//________
// Returns the name of the page as it should be displayed in the user interface
BookReader.prototype.getPageName = function(index) {
    return 'Page ' + this.getPageNum(index);
}

// updateLocationHash
//________
// Update the location hash from the current parameters.  Call this instead of manually
// using window.location.replace
BookReader.prototype.updateLocationHash = function() {
    var newHash = '#' + this.fragmentFromParams(this.paramsFromCurrent());
    window.location.replace(newHash);
    
    // This is the variable checked in the timer.  Only user-generated changes
    // to the URL will trigger the event.
    this.oldLocationHash = newHash;
}

// startLocationPolling
//________
// Starts polling of window.location to see hash fragment changes
BookReader.prototype.startLocationPolling = function() {
    var self = this; // remember who I am
    self.oldLocationHash = window.location.hash;
    
    if (this.locationPollId) {
        clearInterval(this.locationPollID);
        this.locationPollId = null;
    }
    
    this.locationPollId = setInterval(function() {
        var newHash = window.location.hash;
        if (newHash != self.oldLocationHash) {
            if (newHash != self.oldUserHash) { // Only process new user hash once
                //console.log('url change detected ' + self.oldLocationHash + " -> " + newHash);
                
                // Queue change if animating
                if (self.animating) {
                    self.autoStop();
                    self.animationFinishedCallback = function() {
                        self.updateFromParams(self.paramsFromFragment(newHash));
                    }                        
                } else { // update immediately
                    self.updateFromParams(self.paramsFromFragment(newHash));
                }
                self.oldUserHash = newHash;
            }
        }
    }, 500);
}

// canSwitchToMode
//________
// Returns true if we can switch to the requested mode
BookReader.prototype.canSwitchToMode = function(mode) {
    if (mode == this.constMode2up) {
        // check there are enough pages to display
        // $$$ this is a workaround for the mis-feature that we can't display
        //     short books in 2up mode
        if (this.numLeafs < 6) {
            return false;
        }
    }
    
    return true;
}

// searchHighlightVisible
//________
// Returns true if a search highlight is currently being displayed
BookReader.prototype.searchHighlightVisible = function() {
    if (this.constMode2up == this.mode) {
        if (this.searchResults[this.twoPage.currentIndexL]
                || this.searchResults[this.twoPage.currentIndexR]) {
            return true;
        }
    } else { // 1up
        if (this.searchResults[this.currentIndex()]) {
            return true;
        }
    }
    return false;
}

// getPageBackgroundColor
//--------
// Returns a CSS property string for the background color for the given page
// $$$ turn into regular CSS?
BookReader.prototype.getPageBackgroundColor = function(index) {
    if (index >= 0 && index < this.numLeafs) {
        // normal page
        return this.pageDefaultBackgroundColor;
    }
    
    return '';
}

// _getPageWidth
//--------
// Returns the page width for the given index, or first or last page if out of range
BookReader.prototype._getPageWidth = function(index) {
    // Synthesize a page width for pages not actually present in book.
    // May or may not be the best approach.
    // If index is out of range we return the width of first or last page
    index = BookReader.util.clamp(index, 0, this.numLeafs - 1);
    return this.getPageWidth(index);
}

// _getPageHeight
//--------
// Returns the page height for the given index, or first or last page if out of range
BookReader.prototype._getPageHeight= function(index) {
    index = BookReader.util.clamp(index, 0, this.numLeafs - 1);
    return this.getPageHeight(index);
}

// _getPageURI
//--------
// Returns the page URI or transparent image if out of range
BookReader.prototype._getPageURI = function(index, reduce, rotate) {
    if (index < 0 || index >= this.numLeafs) { // Synthesize page
        return this.imagesBaseURL + "/transparent.png";
    }

    return this.getPageURI(index, reduce, rotate);
}

// Library functions
BookReader.util = {
    clamp: function(value, min, max) {
        return Math.min(Math.max(value, min), max);
    },

    getIFrameDocument: function(iframe) {
        // Adapted from http://xkr.us/articles/dom/iframe-document/
        var outer = (iframe.contentWindow || iframe.contentDocument);
        return (outer.document || outer);
    },
    
    decodeURIComponentPlus: function(value) {
        // Decodes a URI component and converts '+' to ' '
        return decodeURIComponent(value).replace(/\+/g, ' ');
    },
    
    encodeURIComponentPlus: function(value) {
        // Encodes a URI component and converts ' ' to '+'
        return encodeURIComponent(value).replace(/%20/g, '+');
    }
    // The final property here must NOT have a comma after it - IE7
}

//-------------------------------------------------
// Cookie functions - MWL 10/9/2009
BookReader.prototype.getViewerCookie = function(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) c_end = document.cookie.length;
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return "";
}
BookReader.prototype.setViewerCookie = function(c_name, value, expiredays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + escape(value) +
((expiredays == null) ? "" : ";expires=" + exdate.toGMTString());
}
//-------------------------------------------------

//-------------------------------------------------
// Call the function on the containing page to sync up the page list - MWL 02/02/2011
BookReader.prototype.syncPageList = function (index) {
    var targetIndex = index;
    if (typeof changePage == "function" && br.callbackOnPageChange) changePage(targetIndex + 1);
    if (typeof br.callbackOnPageChange == "boolean") br.callbackOnPageChange = true;
}
//-------------------------------------------------
