var _nameForms = [];
var _nameSources = [];

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
    document.getElementsByClassName('brNameSourceLabel')[0].innerText = "Sources for " + resolvedName;

    var nameSourceList = document.getElementsByClassName('brNameSourceList')[0];
    while (nameSourceList.firstChild) {
        nameSourceList.removeChild(nameSourceList.firstChild);
    }

    getNameDataSources(lnk, resolvedName, nameSourceList);
}

function getNameDataSources(lnk, resolvedName, nameSourceList) {
    $.ajax({
        type: 'get',
        url: '/service/getnamedatasources',
        data: {
            'name': resolvedName
        },
        success: function (data, textStatus, jqXHR) {
            if (data.length > 0) {
                populateNameSourcesList(data, resolvedName, nameSourceList);
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

function populateNameSourcesList(data, resolvedName, nameSourceList) {
    /*
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
    });
    */

    _nameForms.length = 0;
    _nameSources.length = 0;
    var prevForm = '';
    var formSources = [];
    data.forEach(element => {
        if (prevForm !== element.NameString) {
            if (formSources.length > 0) _nameSources.push(formSources);
            formSources = [];
            _nameForms.push(element.NameString);
            prevForm = element.NameString;
        }

        var nameSource;
        if (element.Url.length > 0) {
            nameSource = document.createElement('a');
            nameSource.setAttribute('href', element.Url);
            nameSource.setAttribute('target', '_blank');
            nameSource.setAttribute('rel', 'noopener noreferrer');
            nameSource.innerText = element.DataSourceTitle; // + ' (' + element.NameString + ')';
        }
        else {
            nameSource = document.createTextNode(element.DataSourceTitle);// + ' (' + element.NameString + ')');
        }

        formSources.push(nameSource);
    });
    if (formSources.length > 0) _nameSources.push(formSources);

    var selectedIndex = fillBRNameSourceDD(resolvedName);
    addBRNameSourceList(selectedIndex, nameSourceList);
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

    var nameSourceCloseImg = document.createElement('img');
    nameSourceCloseImg.setAttribute('src', '/images/close-button.png');
    nameSourceCloseImg.setAttribute('height', '20px');
    nameSourceCloseImg.setAttribute('width', '20px');
    nameSourceCloseLink.appendChild(nameSourceCloseImg);
    
    var nameSourceClose = document.createElement('div');
    nameSourceClose.setAttribute('class', 'brNameSourceClose');
    nameSourceClose.appendChild(nameSourceCloseLink);
    nameSourcePopup.appendChild(nameSourceClose);

    var nameSelect = document.createElement('select');
    nameSelect.setAttribute('class', 'brNameSourceDD');
    nameSelect.setAttribute('onchange', 'brNameSourceDDChange(this)');
    var nameSelectDiv = document.createElement('div')
    nameSelectDiv.setAttribute('class', 'brNameSourceDDDiv');
    nameSelectDiv.appendChild(nameSelect);
    nameSourcePopup.appendChild(nameSelectDiv);

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

function fillBRNameSourceDD(resolvedName) {
    var nameSelect = document.getElementsByClassName('brNameSourceDD')[0];
    while (nameSelect.options.length > 0) nameSelect.remove(0);

    _nameForms.forEach(form => {
        var nameOption = document.createElement('option');
        nameOption.setAttribute('value', form);
        if (stripDiacritic(form) === stripDiacritic(resolvedName)) nameOption.setAttribute('selected', 'selected');
        nameOption.textContent = form;
        nameSelect.appendChild(nameOption);        
    });

    return nameSelect.selectedIndex;
}

function stripDiacritic(name) {
    return name.normalize('NFD').replace(/[\u0300-\u036f]/g, "");
}

function brNameSourceDDChange(dd) {
   addBRNameSourceList(dd.selectedIndex, document.getElementsByClassName('brNameSourceList')[0]);
}

function addBRNameSourceList(element, list) {
    list.innerHTML = '';
    _nameSources[element].forEach(source => {
        addBRNameSourceName(source, list);
    });
}

function showBRNameSourcePopup(anchor, dataLength) {
    var nameSourcePopup = document.getElementsByClassName('brNameSourcePopup')[0];

    var windowBottom = Math.max(document.documentElement.clientHeight || 0, window.innerHeight || 0);
    var popBottom = anchor.getBoundingClientRect().top + window.scrollY + anchor.offsetHeight + 250;
    if (popBottom <= windowBottom) {
        nameSourcePopup.style.top = (anchor.getBoundingClientRect().top + window.scrollY) + 'px';
    }
    else {
        //nameSourcePopup.style.top = (anchor.getBoundingClientRect().top + window.scrollY - ((dataLength > 25 ? 240 : dataLength * 10.5))) + 'px';
        nameSourcePopup.style.top = (anchor.getBoundingClientRect().top + window.scrollY - 250) + 'px';
    }

    var windowRight = Math.max(document.documentElement.clientWidth || 0, window.innerWidth || 0);
    var popRight = anchor.getBoundingClientRect().left + window.scrollX + anchor.offsetWidth + 350;
    if (popRight <= windowRight) {
        nameSourcePopup.style.left = (anchor.getBoundingClientRect().left + window.scrollX + anchor.offsetWidth) + 'px';
    }
    else {
        nameSourcePopup.style.width = '350px';
        nameSourcePopup.style.left = (anchor.getBoundingClientRect().left + window.scrollX - 360) + 'px';
    }

    nameSourcePopup.style.position = 'absolute';
    nameSourcePopup.style.display = 'inline';
}
