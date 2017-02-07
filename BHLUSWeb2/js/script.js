$(document).ready(function () {
    var searchDefaultText = "Search";

    // Upgrade browsers plugin
    $.upgradebrowsers();

    /* used to prevent flickering of elements while doc is still loading */
    $('.js .js-invisible').css('visibility', 'visible');

    // Search bar
    $('#searchbar #btnSearchSubmit').click(function (e) {
        if ($('#searchbar .field').val() == searchDefaultText) {
            e.preventDefault();
            return false;
        }
    });
    $('#searchbar .field')
            .val(searchDefaultText)
            .focus(function () {
                if ($(this).val() == searchDefaultText) {
                    $(this).val("");
                }
            })
            .blur(function () {
                if ($.trim($(this).val()) == "") {
                    $(this).val(searchDefaultText);
                }
            });

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