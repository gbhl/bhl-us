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