// OuterWidth/Height setter
(function ($) {
    if ($) {
        // get references to original functions
        var outerWidth = $.fn.outerWidth;
        var outerHeight = $.fn.outerHeight;

        var workerFunc = function (height, value) {
            var elem = $(this);

            var actualSize;
            if (height) {
                actualSize = value - parseInt(elem.css('borderTopWidth')) - parseInt(elem.css('paddingTop')) - parseInt(elem.css('paddingBottom')) - parseInt(elem.css('borderBottomWidth'));
                return this.height(actualSize);
            } else {
                actualSize = value - parseInt(elem.css('borderLeftWidth')) - parseInt(elem.css('paddingLeft')) - parseInt(elem.css('paddingRight')) - parseInt(elem.css('borderRightWidth'));
                return this.width(actualSize);
            }
        };

        $.fn.extend({
            outerWidth: function (value) {
                if (typeof (value) != "undefined" && value === value * 1.0) {
                    return workerFunc.apply(this, [false, value]);
                } else {
                    // pass onto original function
                    return outerWidth.apply(this, arguments);
                }
            },

            outerHeight: function (value) {
                if (typeof (value) != "undefined" && value === value * 1.0) {
                    return workerFunc.apply(this, [true, value]);
                } else {
                    // pass onto original function
                    return outerHeight.apply(this, arguments);
                }
            }
        });
    }
})(jQuery);

// Rick Strahls get/set querystring params functions
getUrlEncodedKey = function(key, query) {
    if (!query)
        query = window.location.search;    
    var re = new RegExp("[?|&]" + key + "=(.*?)&");
    var matches = re.exec(query + "&");
    if (!matches || matches.length < 2)
        return "";
    return decodeURIComponent(matches[1].replace("+", " "));
}
setUrlEncodedKey = function (key, value, query) {
    query = query || window.location.search;
    var q = query + "&";
    var re = new RegExp("[?|&]" + key + "=.*?&");
    if (!re.test(q) && value != '')
    {
        q += key + "=" + encodeURI(value);
    }
    else
    {
        q = q.replace(re, (value != '') ? "&" + key + "=" + encodeURIComponent(value) + "&" : '');
    }
    q = q.trimStart("&").trimEnd("&");
    return q[0] == "?" ? q : q = "?" + q;
}
String.prototype.trimEnd = function(c) {
    if (c)        
        return this.replace(new RegExp(c.escapeRegExp() + "*$"), '');
    return this.replace(/\s+$/, '');
}
String.prototype.trimStart = function(c) {
    if (c)
        return this.replace(new RegExp("^" + c.escapeRegExp() + "*"), '');
    return this.replace(/^\s+/, '');
}
String.prototype.escapeRegExp = function() {
    return this.replace(/[.*+?^${}()|[\]\/\\]/g, "\\$0");
};

// Array Remove - By John Resig (MIT Licensed)
Array.prototype.remove = function (from, to) {
    var rest = this.slice((to || from) + 1 || this.length);
    this.length = from < 0 ? this.length + from : from;
    return this.push.apply(this, rest);
};

// .Net Style string.Format() function
String.prototype.format = function () {
    var s = this,
        i = arguments.length;

    while (i--) {
        s = s.replace(new RegExp('\\{' + i + '\\}', 'gm'), arguments[i]);
    }
    return s;
};

String.prototype.trimPx = function() {
    // remove "px" from values
    var pos = this.indexOf("px");
    if (pos != 0)
        return parseInt(this.substring(0, pos));
    else
        return 0;
}

/*Copyright (c) 2009 Dimas Begunoff, http://www.farinspace.com*/
function imgpreload(imgs,settings)
{
	// settings = { each:Function, all:Function }
	if (settings instanceof Function) { settings = {all:settings}; }

	// use of typeof required
	// https://developer.mozilla.org/En/Core_JavaScript_1.5_Reference/Operators/Special_Operators/Instanceof_Operator#Description
	if (typeof imgs == "string") { imgs = [imgs]; }

	var loaded = [];
	var t = imgs.length;
	var i = 0;

	for (i; i<t; i++)
	{
		var img = new Image();
		img.onload = function()
		{
			loaded.push(this);
			if (settings.each instanceof Function) { settings.each.call(this); }
			if (loaded.length>=t && settings.all instanceof Function) { settings.all.call(loaded); }
		};
		img.src = imgs[i];
	}
}
if (typeof jQuery != "undefined")
{
	(function($){

		// extend jquery (because i love jQuery)
		$.imgpreload = imgpreload;

		// public
		$.fn.imgpreload = function(settings)
		{
			settings = $.extend({},$.fn.imgpreload.defaults,(settings instanceof Function)?{all:settings}:settings);

			this.each(function()
			{
				var elem = this;

				imgpreload($(this).attr('src'),function()
				{
					if (settings.each instanceof Function) { settings.each.call(elem); }
				});
			});

			// declare urls and loop here (loop a second time) to prevent
			// pollution of above closure with unnecessary variables

			var urls = [];

			this.each(function()
			{
				urls.push($(this).attr('src'));
			});

			var selection = this;

			imgpreload(urls,function()
			{
				if (settings.all instanceof Function) { settings.all.call(selection); }
			});

			return this;
		};

		// public
		$.fn.imgpreload.defaults =
		{
			each: null // callback invoked when each image in a group loads
			, all: null // callback invoked when when the entire group of images has loaded
		};

	})(jQuery);
}

/* jQuery Cookie plugin * Copyright (c) 2010 Klaus Hartl (stilbuero.de) * Dual licensed under the MIT and GPL licenses: */
jQuery.cookie = function (key, value, options) {
    
    // key and at least value given, set cookie...
    if (arguments.length > 1 && String(value) !== "[object Object]") {
        options = jQuery.extend({}, options);

        if (value === null || value === undefined) {
            options.expires = -1;
        }

        if (typeof options.expires === 'number') {
            var days = options.expires, t = options.expires = new Date();
            t.setDate(t.getDate() + days);
        }
        
        value = String(value);
        
        return (document.cookie = [
            encodeURIComponent(key), '=',
            options.raw ? value : encodeURIComponent(value),
            options.expires ? '; expires=' + options.expires.toUTCString() : '', // use expires attribute, max-age is not supported by IE
            options.path ? '; path=' + options.path : '',
            options.domain ? '; domain=' + options.domain : '',
            options.secure ? '; secure' : ''
        ].join(''));
    }

    // key and possibly options given, get cookie...
    options = value || {};
    var result, decode = options.raw ? function (s) { return s; } : decodeURIComponent;
    return (result = new RegExp('(?:^|; )' + encodeURIComponent(key) + '=([^;]*)').exec(document.cookie)) ? decode(result[1]) : null;
};

/*jQuery Upgrade Browsers Plugin v0.1 (Modified For BHL) Copyright (c) 2011 Eduardo Lingán, elingan@gmail.com, http://upgradebrowsers.com*/
(function ($) {
    $.upgradebrowsers = function () {
        var upgradebrowses_html = '<div id="upgradebrowsers">' +
        '<img class="message" src="/images/bhlau images/image_browser_message.png" width="500" height="110" alt="BHL works best with a modern browser. We\'ve noticed that your web browser is out of date. To best experience the Biodiversity Heritage Library, please upgrade to one of the following options..." />' + 
        '<div class="browsers">' +
        '<a class="icon chrome" href="http://www.google.com/chrome" target="_blank" rel="noopener noreferrer"><img src="/images/bhlau images/icon_chrome.png" width="102" height="110" alt="Chrome" /></a>' +
        '<a class="icon firefox" href="http://www.mozilla.org/firefox/" target="_blank" rel="noopener noreferrer"><img src="/images/bhlau images/icon_firefox.png" width="102" height="110" alt="Firefox" /></a>' + 
        '<a class="icon safari" href="http://www.apple.com/safari/download/" target="_blank" rel="noopener noreferrer"><img src="/images/bhlau images/icon_safari.png" width="102" height="110" alt="Safari" /></a>' + 
        '<a class="icon iexplorer" href="http://windows.microsoft.com/ie9" target="_blank" rel="noopener noreferrer"><img src="/images/bhlau images/icon_iexplorer.png" width="102" height="110" alt="Internet Explorer" /></a>' +
        '<a class="close" title="close">close</a>' + 
        '</div>' + 
        '</div>';

        var versions = {
            chrome: 10,
            mozilla: 4,
            opera: 11,
            safari: 5,
            msie: 9
        };

        var userAgent = navigator.userAgent.toLowerCase();

        $.browser_info = {
            version: (userAgent.match(/.+(?:ox|on|ie|me)[\/: ]([\d.]+)/) || [])[1],
            chrome: /chrome/.test(userAgent),
            mozilla: /mozilla/.test(userAgent) && !/(compatible|webkit)/.test(userAgent),
            opera: /opera/.test(userAgent),
            safari: /webkit/.test(userAgent) && !/chrome/.test(userAgent),
            msie: /msie/.test(userAgent) && !/opera/.test(userAgent)
        };
        var version = 0;
        $.each($.browser_info, function (key, value) {
            if (key == "version") {
                version = parseInt(value, 10);
            } else {
                if (value) {
                    if ((version < versions[key]) && ($.cookie('BHL.UpgradeBrowsers') != 'seen')) {
                        $("body").prepend(upgradebrowses_html);
                        // Pre-Load images
                        $('#upgradebrowsers img').imgpreload(function() {
                            $("#upgradebrowsers").fadeIn();
	                    });
                    }
                }
            }
        });

        $("#upgradebrowsers .close").click(function () {
            $("#upgradebrowsers").fadeOut();
            $.cookie('BHL.UpgradeBrowsers', 'seen', { path: '/', expires: 3 }); // Set for every 3 days
        });
    }
})(jQuery);

/*! Copyright (c) 2008 Brandon Aaron (brandon.aaron@gmail.com || http://brandonaaron.net)
 * Dual licensed under the MIT (http://www.opensource.org/licenses/mit-license.php) 
 * and GPL (http://www.opensource.org/licenses/gpl-license.php) licenses.
 */

/**
 * Gets the width of the OS scrollbar
 */
(function($) {
	var scrollbarWidth = 0;
	$.getScrollbarWidth = function() {
		if ( !scrollbarWidth ) {
			if ( $.browser.msie ) {
				var $textarea1 = $('<textarea cols="10" rows="2"></textarea>')
						.css({ position: 'absolute', top: -1000, left: -1000 }).appendTo('body'),
					$textarea2 = $('<textarea cols="10" rows="2" style="overflow: hidden;"></textarea>')
						.css({ position: 'absolute', top: -1000, left: -1000 }).appendTo('body');
				scrollbarWidth = $textarea1.width() - $textarea2.width();
				$textarea1.add($textarea2).remove();
			} else {
				var $div = $('<div />')
					.css({ width: 100, height: 100, overflow: 'auto', position: 'absolute', top: -1000, left: -1000 })
					.prependTo('body').append('<div />').find('div')
						.css({ width: '100%', height: 200 });
				scrollbarWidth = 100 - $div.width();
				$div.parent().remove();
			}
		}
		return scrollbarWidth;
	};
})(jQuery);