$(document).ready(function () {
    var searchDefaultTexts = ["Search the catalog and full-text", "Search the catalog"];

    /* used to prevent flickering of elements while doc is still loading */
    $('.js .js-invisible').css('visibility', 'visible');

    // Search bar
    $('#searchbar #btnSearchSubmit').click(function (e) {
        if ($.inArray($("#searchbar .field").val(), searchDefaultTexts) >= 0) {
            e.preventDefault();
            return false;
        }
    });
    $('#searchbar .field')
        .val(searchDefaultTexts[0])
        .focus(function () {
            if ($.inArray($(this).val(), searchDefaultTexts) >= 0) {
                $(this).val("");
            }
        })
        .blur(function () {
            if ($.trim($(this).val()) == "") {
                if ($("#rdoSearchTypeF").is(":checked")) $(this).val(searchDefaultTexts[0]);
                if ($("#rdoSearchTypeC").is(":checked")) $(this).val(searchDefaultTexts[1]);
            }
        });

    $('#rdoSearchTypeF')
        .change(function () {
            if ($(this).is(':checked')) {
                if ($.inArray($('#searchbar .field').val(), searchDefaultTexts) >= 0) $('#searchbar .field').val(searchDefaultTexts[0]);
            }
        });

    $('#rdoSearchTypeC')
        .change(function () {
            if ($(this).is(':checked')) {
                if ($.inArray($('#searchbar .field').val(), searchDefaultTexts) >= 0) $('#searchbar .field').val(searchDefaultTexts[1]);
            }
        });

    // Code taken from here to account for deprecated "browser" jQuery attribute
    // https://stackoverflow.com/a/14798444
    if (typeof jQuery.browser == 'undefined') {
        var matched, browser;

        jQuery.uaMatch = function (ua) {
            ua = ua.toLowerCase();

            var match = /(chrome)[ \/]([\w.]+)/.exec(ua) ||
                /(webkit)[ \/]([\w.]+)/.exec(ua) ||
                /(opera)(?:.*version|)[ \/]([\w.]+)/.exec(ua) ||
                /(msie)[\s?]([\w.]+)/.exec(ua) ||
                /(trident)(?:.*? rv:([\w.]+)|)/.exec(ua) ||
                ua.indexOf("compatible") < 0 && /(mozilla)(?:.*? rv:([\w.]+)|)/.exec(ua) ||
                [];

            return {
                browser: match[1] || "",
                version: match[2] || "0"
            };
        };

        matched = jQuery.uaMatch(navigator.userAgent);
        //IE 11+ fix (Trident) 
        matched.browser = matched.browser == 'trident' ? 'msie' : matched.browser;
        browser = {};

        if (matched.browser) {
            browser[matched.browser] = true;
            browser.version = matched.version;
        }

        // Chrome is Webkit, but Webkit is also Safari.
        if (browser.chrome) {
            browser.webkit = true;
        } else if (browser.webkit) {
            browser.safari = true;
        }

        jQuery.browser = browser;
    }

    // Code taken from here to account for deprecated "live()" jQuery method
    // https://stackoverflow.com/a/31826928
    // Forward port jQuery.live().  Wrapper for newer jQuery.on()
    // Uses optimized selector context.  Only add if live() not already existing.
    if (typeof jQuery.fn.live == 'undefined' || !(jQuery.isFunction(jQuery.fn.live))) {
        jQuery.fn.extend({
            live: function (event, callback) {
                if (this.selector) {
                    jQuery(document).on(event, this.selector, callback);
                }
            }
        });
    }

    if (($.browser.msie && (parseInt($.browser.version, 10) < 9))) {
        $("#volumedd > div").css("display", "none");
        $("#volumedd").attr("class", "ieUnder9");
    } else {
        // Style selects
        $('#ddlVolumes').not('.ui-pg-selbox').each(function () {
            var select = $(this);
            var options = $('options', select);

            var styledSelect = $('<div/>')
                .addClass('select')
                .text($('option:selected', select).text());

            select
                .before(styledSelect)
                .addClass('styled');

            styledSelect.outerWidth($.browser.msie ? select.outerWidth() - 2 : select.outerWidth());

            select.change(function (event, index) {
                styledSelect.text($('option:selected', this).text());
                console.log(index);
                if (!index) {
                    console.log("update page view");
                }
            }).hover(function () {
                styledSelect.addClass('hover');
            }, function () {
                styledSelect.removeClass('hover');
            }).mousedown(function () {
                styledSelect.addClass('active');
            }).mouseup(function () {
                styledSelect.removeClass('active');
            });
        });
    } 

    // Volumes Collapsing on Bibliography page
    $('.volume:first').addClass('active');
    $('.volume .body').not(':first').hide();
    $('.volume').show();
    $('.volume .title').click(function () {
        var volume = $(this).parent();
        var volumeBody = $('.body', volume);

        volumeBody.stop(true, true).slideToggle(function () {
            volume.toggleClass('active');
        });
    });

    // See Also section collapsing on part Bibliography page
    $('.partalso:first').addClass('active');
    $('.partalso .body').not(':first').hide();
    $('.partalso').show();
    $('.partalso .title').click(function () {
        var volume = $(this).parent();
        var volumeBody = $('.body', volume);

        volumeBody.stop(true, true).slideToggle(function () {
            volume.toggleClass('active');
        });
    });

    // Abstractsection collapsing on part Bibliography page
    $('.partabstract .title').click(function () {
        var volume = $(this).parent();
        var volumeBody = $('.body', volume);

        volumeBody.stop(true, true).slideToggle(function () {
            volume.toggleClass('active');
        });
    });

    // Note section collapsing on part Bibliography page
    $('.partnote .title').click(function () {
        var volume = $(this).parent();
        var volumeBody = $('.body', volume);

        volumeBody.stop(true, true).slideToggle(function () {
            volume.toggleClass('active');
        });
    });

    // Auto-Ellipsis for browsers that dont support it
    if (typeof $().textOverflow == 'function') {
        $('.ellipsis').textOverflow();
    }

    // Polyfill for Array.includes() (Needed for IE)
    if (![].includes) {
        Array.prototype.includes = function (searchElement /*, fromIndex*/) {
            'use strict';
            var O = Object(this);
            var len = parseInt(O.length) || 0;
            if (len === 0) {
                return false;
            }
            var n = parseInt(arguments[1]) || 0;
            var k;
            if (n >= 0) {
                k = n;
            } else {
                k = len + n;
                if (k < 0) { k = 0; }
            }
            var currentElement;
            while (k < len) {
                currentElement = O[k];
                if (searchElement === currentElement ||
                    (searchElement !== searchElement && currentElement !== currentElement)) {
                    return true;
                }
                k++;
            }
            return false;
        };
    }
});