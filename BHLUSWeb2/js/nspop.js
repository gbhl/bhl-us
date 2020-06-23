function hideNameSourceList(e) {
    var nameSourcePopup = $(".brNameSourcePopup");
    if (!nameSourcePopup.is(e.target) // if the target of the click isn't the container...
        && (nameSourcePopup.has(e.target).length === 0) // ... nor a descendant of the container
        && (e.target != nameSourcePopup.get(0))) // nor the scrollbar
    {
        closeNameSources();
    }
}

function closeNameSources() {
    var nameSourcePopup = document.getElementsByClassName('brNameSourcePopup')[0];
    nameSourcePopup.style.display = 'none';
    unbindClickOutsideTrigger();
}

function unbindClickOutsideTrigger() {
    document.removeEventListener("mouseup", hideNameSourceList, false);
}
function bindClickOutsideTrigger() {
    document.addEventListener("mouseup", hideNameSourceList, false);
}

function showNameSources(e, lnk) {
    e.stopPropagation();
    e.preventDefault();

    if (document.getElementsByClassName('brNameSourcePopup').length === 0) createBRNameSourcePopup();

    var resolvedName = lnk.getAttribute('data-resolved-name');
    document.getElementsByClassName('brNameSourceLabel')[0].innerText = "Sources For " + resolvedName;

    var nameSourceList = document.getElementsByClassName('brNameSourceList')[0];
    while (nameSourceList.firstChild) {
        nameSourceList.removeChild(nameSourceList.firstChild);
    }

    $.ajax({
        type: 'get',
        url: 'http://localhost:49275/service/getnamedatasources',
        data: {
            'name': resolvedName
        },
        success: function (data, textStatus, jqXHR) {
            if (data.length > 0) {
                data.forEach(element => {
                    var nameSource;
                    if (element.Url.length > 0) {
                        nameSource = document.createElement('a');
                        nameSource.setAttribute('href', element.Url);
                        nameSource.setAttribute('target', '_blank');
                        nameSource.setAttribute('rel', 'noopener noreferrer');
                        nameSource.innerText = element.DataSourceTitle + ' (' + element.NameString + ')';
                    }
                    else {
                        nameSource = document.createTextNode(element.DataSourceTitle + ' (' + element.NameString + ')');
                    }
                    addBRNameSourceName(nameSource, nameSourceList);
                }
                );

                showBRNameSourcePopup(lnk, data.length);
                bindClickOutsideTrigger();
            } else {
                addBRNameSourceName(document.createTextNode('No sources found'), nameSourceList);
                showBRNameSourcePopup(lnk, 1);
                bindClickOutsideTrigger();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            addBRNameSourceName(document.createTextNode('Error getting sources'), nameSourceList);
            showBRNameSourcePopup(lnk, 1);
            bindClickOutsideTrigger();
        }
    });
}

function createBRNameSourcePopup() {
    var nameSourcePopup = document.createElement('div');
    nameSourcePopup.setAttribute('class', 'brNameSourcePopup');

    var nameSourceLabel = document.createElement('div');
    nameSourceLabel.setAttribute('class', 'brNameSourceLabel');
    nameSourcePopup.appendChild(nameSourceLabel);

    var nameSourceCloseLink = document.createElement('a');
    nameSourceCloseLink.setAttribute('href', '#');
    nameSourceCloseLink.setAttribute('class', 'brNameSourceCloseLink');
    nameSourceCloseLink.setAttribute('onClick', 'closeNameSources();');
    nameSourceCloseLink.innerText = "close";

    var nameSourceClose = document.createElement('div');
    nameSourceClose.setAttribute('class', 'brNameSourceClose');
    nameSourceClose.appendChild(nameSourceCloseLink);
    nameSourcePopup.appendChild(nameSourceClose);

    var nameSourceList = document.createElement('div');
    nameSourceList.setAttribute('class', 'brNameSourceList');
    nameSourcePopup.appendChild(nameSourceList);

    document.body.appendChild(nameSourcePopup);
}

function addBRNameSourceName(element, list) {
    var nameDiv = document.createElement('div');
    nameDiv.setAttribute('class', 'brNameSourceName');
    nameDiv.appendChild(element);
    list.appendChild(nameDiv);
}

function showBRNameSourcePopup(anchor, dataLength) {
    var nameSourcePopup = document.getElementsByClassName('brNameSourcePopup')[0];

    var windowBottom = Math.max(document.documentElement.clientHeight || 0, window.innerHeight || 0);
    var popBottom = anchor.getBoundingClientRect().top + window.scrollY + anchor.offsetHeight + 250;
    if (popBottom <= windowBottom) {
        nameSourcePopup.style.top = (anchor.getBoundingClientRect().top + window.scrollY) + 'px';
    }
    else {
        nameSourcePopup.style.top = (anchor.getBoundingClientRect().top + window.scrollY - ((dataLength > 25 ? 240 : dataLength * 10.5))) + 'px';
    }

    var windowRight = Math.max(document.documentElement.clientWidth || 0, window.innerWidth || 0);
    var popRight = anchor.getBoundingClientRect().left + window.scrollX + anchor.offsetWidth + 400;
    if (popRight <= windowRight) {
        nameSourcePopup.style.left = (anchor.getBoundingClientRect().left + window.scrollX + anchor.offsetWidth) + 'px';
    }
    else {
        nameSourcePopup.style.width = '400px';
        nameSourcePopup.style.left = (anchor.getBoundingClientRect().left + window.scrollX - 410) + 'px';
    }

    nameSourcePopup.style.position = 'absolute';
    nameSourcePopup.style.display = 'inline';
}
