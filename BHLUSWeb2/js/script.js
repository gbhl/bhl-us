$(document).ready(function () {
    var searchDefaultText = "Search";
    var searchDefaultTexts = ["Search the catalog and full-text", "Search the catalog"];

    // Upgrade browsers plugin
    $.upgradebrowsers();

    /* used to prevent flickering of elements while doc is still loading */
    $('.js .js-invisible').css('visibility', 'visible');

    // Search bar
    $('#searchbar #btnSearchSubmit').click(function (e) {
        //if ($('#searchbar .field').val() == searchDefaultText) {
        if ($.inArray($("#searchbar .field").val(), searchDefaultTexts) >= 0) {
            e.preventDefault();
            return false;
        }
    });
    $('#searchbar .field')
        .val(searchDefaultTexts[0])
        .focus(function () {
            //if ($(this).val() == searchDefaultText) {
            //    $(this).val("");
            //}
            if ($.inArray($(this).val(), searchDefaultTexts) >= 0) {
                $(this).val("");
            }
        })
        .blur(function () {
            if ($.trim($(this).val()) == "") {
                //$(this).val(searchDefaultText);
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
        $('select').not('.ui-pg-selbox').each(function () {
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

    $('#ddlVolumes').live("change", function () {
        location.href = '/item/' + $(this).val();
    });


    // Featured books accordian
    var accordion = $('#accordion');
    // Default state
    $('a:not(.active) .push', accordion).hide();
    // Hover states
    if (typeof $().hoverIntent == 'function') {
        $('a', accordion).hoverIntent(function () {
            var book = $(this)
            var push = $('.push', this);

            // Close active
            $('a.active', accordion)
                .not(book)
                .removeClass('active')
                .find('.push')
                .slideUp();

            // Open selected book
            if (!book.hasClass('active')) {
                book.addClass('active');
                push.slideDown();
            }
        }, function () {
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
});