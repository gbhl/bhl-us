 /*
# Based on Citation.js, https://cdn.jsdelivr.net/npm/citation-js@0.6.4
# Unminified, https://cdn.jsdelivr.net/npm/citation-js@0.6.4/build/citation.js
*/

function CitationModalArgs() {
    this.titleId = null;
    this.itemId = null;
    this.segmentId = null;
    this.pageId = null;
    this.titleText = 'Title';
    this.itemText = 'Volume';
    this.segmentText = 'Article';
    this.pageText = 'Page';

    this.target = null;

    return this;
}

CitationModalArgs.prototype.init = function(args) {
    if (typeof args.tid !== 'undefined') this.titleId = args.tid;
    if (typeof args.iid !== 'undefined') this.itemId = args.iid;
    if (typeof args.sid !== 'undefined') this.segmentId = args.sid;
    if (typeof args.pid !== 'undefined') this.pageId = args.pid;
    if (typeof args.ttext !== 'undefined') this.titleText = args.ttext;
    if (typeof args.itext !== 'undefined') this.itemText = args.itext;
    if (typeof args.stext !== 'undefined') this.segmentText = args.stext;
    if (typeof args.ptext !== 'undefined') this.pageText = args.ptext;
}

CitationModalArgs.prototype.citeTarget = function () {
    var tgt = null;
    if (this.segmentId !== null) tgt = 's';
    if (this.pageId !== null && tgt === null) tgt = 'p';
    if (this.itemId != null && tgt === null) tgt = 'i';
    if (this.titleId !== null && tgt === null) tgt = 't';
    return tgt;
}

CitationModalArgs.prototype.numChoices = function () {
    var choices = 0;
    if (this.segmentId !== null) choices++;
    if (this.pageId !== null) choices++;
    if (this.itemId != null) choices++;
    if (this.titleId !== null) choices++;
    return choices;
}

// Render and display the citation modal
async function showCitationModal(cmArgs) {
    if (cmArgs.numChoices() > 0) {
        createCiteModal(citeModal, cmArgs);
        showCitation(((typeof selTarget !== 'undefined') ? selTarget.value : cmArgs.citeTarget()), selCitation.value, cmArgs);
        citeModal.style.display = "block";
    }
    else{
        alert("Citation dialog not correctly initialized.");
    }
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
    if (event.target == citeModal) {
        citeModal.style.display = "none";
    }
}	

async function showCitation(target, format, cmArgs)
{
    let targetId = null;
    // Get the target identifier and set up the download links
    if (target == 't') {
        targetId = cmArgs.titleId;
        divDLBibTex.style.display = 'block';
        lnkDLBibTex.href = '/bibtexdownload/title/' + cmArgs.titleId;
        divDLCSL.style.display = 'block';
        lnkDLCSL.href = '/csldownload/title/' + cmArgs.titleId;
        divDLMODS.style.display = 'block';
        lnkDLMODS.href = '/modsdownload/title/' + cmArgs.titleId;
        divDLRIS.style.display = 'block';
        lnkDLRIS.href = '/risdownload/title/' + cmArgs.titleId;
    }
    if (target == 'i') {
        targetId = cmArgs.itemId;
        divDLBibTex.style.display = 'block';
        lnkDLBibTex.href = '/bibtexdownload/item/' + cmArgs.itemId;
        divDLCSL.style.display = 'block';
        lnkDLCSL.href = '/csldownload/item/' + cmArgs.itemId;
        divDLMODS.style.display = 'block';
        lnkDLMODS.href = '/modsdownload/item/' + cmArgs.itemId;
        divDLRIS.style.display = 'block';
        lnkDLRIS.href = '/risdownload/item/' + cmArgs.itemId;
    }
    if (target == 's') {
        targetId = cmArgs.segmentId;
        divDLBibTex.style.display = 'block';
        lnkDLBibTex.href = '/bibtexdownload/part/' + cmArgs.segmentId;
        divDLCSL.style.display = 'block';
        lnkDLCSL.href = '/csldownload/part/' + cmArgs.segmentId;
        divDLMODS.style.display = 'block';
        lnkDLMODS.href = '/modsdownload/part/' + cmArgs.segmentId;
        divDLRIS.style.display = 'block';
        lnkDLRIS.href = '/risdownload/part/' + cmArgs.segmentId;
    }
    if (target == 'p') {
        targetId = cmArgs.pageId;
        divDLBibTex.style.display = 'block';
        lnkDLBibTex.href = '/bibtexdownload/page/' + cmArgs.pageId;
        divDLCSL.style.display = 'block';
        lnkDLCSL.href = '/csldownload/page/' + cmArgs.pageId;
        divDLMODS.style.display = 'none';
        lnkDLMODS.href = '#';
        divDLRIS.style.display = 'block';
        lnkDLRIS.href = '/risdownload/page/' + cmArgs.pageId;
    }
    // Show the citation, formatted appropriately
    await getCitation(citationText, "innerHTML", format, target, targetId);
}

// Display temporary messages
function delay(time) {
    return new Promise(resolve => setTimeout(resolve, time));
}
async function showTempMessage(msg) {
    tempMsgSpan.innerHTML = msg;
    tempMsgSpan.className = 'tempMsgSpanShow';
    await delay(500);
    tempMsgSpan.className = 'tempMsgSpanHide';
}


// Wrap embedded links in anchor tags
String.prototype.wrapLinks = function (new_window) {
    var url_pattern = /(?:(?:https?|ftp|file):\/\/|www\.|ftp\.)(?:\([-A-Z0-9+&@#\/%=~_|$?!:,.]*\)|[-A-Z0-9+&@#\/%=~_|$?!:,.])*(?:\([-A-Z0-9+&@#\/%=~_|$?!:,.]*\)|[A-Z0-9+&@#\/%=~_|$])/igm;
    var target = (new_window === true || new_window == null) ? '_blank' : '';
    
    return this.replace(url_pattern, function (url) {
        var protocol_pattern = /^(?:(?:https?|ftp):\/\/)/i;
        var href = protocol_pattern.test(url) ? url : 'https://' + url;
        return '<a href="' + href + '" target="' + target + '">' + url + '</a>';
    });
};
  
// The citation.js plug-ins were adapted from the source files found at https://github.com/citation-style-language/styles
//	apa-7th.js
//	chicago-author-date-17th.js
//	chicago-note-bibliography-17th.js
//	modern-language-association-9th.js
//  american-medical-association-11th.js
//  wikipedia-templates.js
async function getCitation(ref, prop, templateName, idType, id)
{
    ref[prop] = "Loading...";

    // Instantiate an instance of citation.js with the specified data
    const Cite = await require('citation-js');
    
    // Set up specified citation template (default to Chicago (Note Bibliography)
    let templateSchema = null;// await import("./plugins/chicago-note-bibliography-17th.js");

    switch(templateName)
    {
        case "ama":
            {
                templateSchema = await import("./plugins/american-medical-association-11th.js");
                break;
            }
        case "apa":
            {
                templateSchema = await import("./plugins/apa-7th.js");
                break;
            }
        case "mla":
            {
                templateSchema = await import("./plugins/modern-language-association-9th.js");
                break;
            }
        case "chicago-ad":
            {
                templateSchema = await import("./plugins/chicago-author-date-17th.js");
                break;
            }
        case "chicago-nb":
            {
                templateSchema = await import("./plugins/chicago-note-bibliography-17th.js");
                break;
            }
		case "wikipedia":
			{
                templateSchema = await import("./plugins/wikipedia-templates.js");
				break;
			}
    }
    if (templateSchema !== null) {
        await Cite.CSL.register.addTemplate(templateName, templateSchema.default);

        // Get the data in CSL-JSON format
        // Docs for CSL-JSON
        //      https://citeproc-js.readthedocs.io/en/latest/csl-json/markup.html
        //      https://github.com/citation-style-language/schema/blob/master/schemas/input/csl-data.json (schema)
        //      https://gist.github.com/larsgw/e5e7e7a5552df67d4dab4bd9378e5412 (example)
        await fetch('/service/GetCitationJSON?idType=' + idType + '&id=' + id + '&r=' + Math.random() * 1000000)
            .then((response) => {
                response.json().then((citationData) => {
                    // Produce an HTML formatted citation string
                    let citation = new Cite(citationData);
                    let citationHtml = citation.format('bibliography', {
                        format: 'html',
                        template: templateName,
                        lang: 'en-US'
                    });

                    ref[prop] = citationHtml.wrapLinks();
                });
            })
            .catch((err) => {
                ref[prop] = '<div>ERROR: Could not generate a citation</div>';
            });
    }
    else {
        ref[prop] = '';
    }
}

function createCiteModal(modalContainer, cmArgs) {
    // Create the modal content div
    const modalContent = document.createElement('div');
    modalContent.className = 'citeModalContent';

    // Create and append the message span
    const tempMessageSpan = document.createElement('span');
    tempMessageSpan.id = 'tempMsgSpan';
    tempMessageSpan.className = 'tempMsgSpanHide';
    modalContent.appendChild(tempMessageSpan);

    // Create and append the close button
    const closeButton = document.createElement('span');
    closeButton.id = 'closeModal';
    closeButton.className = 'closeModal';
    closeButton.innerHTML = '&times;';
    closeButton.onclick = function() {
        modalContainer.style.display = "none";
    }
    modalContent.appendChild(closeButton);

    // Create the first section of the modal
    const section2 = document.createElement('div');
    section2.className = 'citeModalSection';

    const headModal2 = document.createElement('div');
    headModal2.className = 'headModal';
    headModal2.textContent = 'Cite This Publication';
    section2.appendChild(headModal2);

    const citeModalDDDiv = document.createElement('div');
    citeModalDDDiv.className = 'citeModalDDDiv';

    const citeModalGridCol1 = document.createElement('div');
    citeModalGridCol1.className = 'citeModalGridColumn';

    if (cmArgs.numChoices() > 1) {
        const citeModalGridCol1Row1 = document.createElement('div');
        citeModalGridCol1Row1.className = 'citeModalGridRow';
        citeModalGridCol1Row1.innerHTML = 'Citation For:';
        citeModalGridCol1.appendChild(citeModalGridCol1Row1);
    }

    const citeModalGridCol1Row2 = document.createElement('div');
    citeModalGridCol1Row2.className = 'citeModalGridRow';
    citeModalGridCol1Row2.innerHTML = 'Style:';
    citeModalGridCol1.appendChild(citeModalGridCol1Row2);

    citeModalDDDiv.appendChild(citeModalGridCol1);

    const citeModalGridCol2 = document.createElement('div');
    citeModalGridCol2.className = 'citeModalGridColumn';

    if (cmArgs.numChoices() > 1) {
        const citeModalGridCol2Row1 = document.createElement('div');
        citeModalGridCol2Row1.className = 'citeModalGridRow';

        const selectTarget = document.createElement('select');
        selectTarget.id = 'selTarget';
        selectTarget.className = 'citeModalDDTarget';

        const options = [];
        if (cmArgs.titleId !== null) options.push({ value: 't', text: cmArgs.titleText });
        if (cmArgs.itemId !== null) options.push({ value: 'i', text: cmArgs.itemText });
        if (cmArgs.segmentId !== null) options.push({ value: 's', text: cmArgs.segmentText, selected: true });
        if (cmArgs.pageId !== null) options.push({ value: 'p', text: cmArgs.pageText, selected: (cmArgs.segmentId === null || cmArgs.segmentText === null) });

        options.forEach(option => {
            const optElement = document.createElement('option');
            optElement.value = option.value;
            optElement.textContent = option.text;
            if (option.selected) optElement.selected = true;
            selectTarget.appendChild(optElement);
        });

        selectTarget.onchange = function () {
            showCitation(this.value, selCitation.value, cmArgs);
        }

        citeModalGridCol2Row1.appendChild(selectTarget);
        citeModalGridCol2.appendChild(citeModalGridCol2Row1);
    }

    const citeModalGridCol2Row2 = document.createElement('div');
    citeModalGridCol2Row2.className = 'citeModalGridRow';

    const citeModalStyleDiv = document.createElement('div');
    citeModalStyleDiv.className = 'citeModalDDDiv';
    citeModalStyleDiv.innerHTML = 'Style: ';

    const selectStyle = document.createElement('select');
    selectStyle.id = 'selCitation';

    const styleOptions = [
        { value: '', text: '(Select a citation style)', selected: true },
        { value: 'apa', text: 'APA'},
        { value: 'mla', text: 'MLA' },
        { value: 'chicago-ad', text: 'Chicago (Author-Date)' },
        { value: 'chicago-nb', text: 'Chicago (Note-Bibliography)' },
        { value: 'wikipedia', text: 'Wikipedia' }
    ];

    styleOptions.forEach(option => {
        const optElement = document.createElement('option');
        optElement.value = option.value;
        optElement.textContent = option.text;
        if (option.selected) optElement.selected = true;
        selectStyle.appendChild(optElement);
    });

    selectStyle.onchange = function () {
        showCitation(((typeof selTarget !== "undefined") ? selTarget.value : cmArgs.citeTarget()), this.value, cmArgs);
    }

    citeModalGridCol2Row2.appendChild(selectStyle);
    citeModalGridCol2.appendChild(citeModalGridCol2Row2);

    citeModalDDDiv.appendChild(citeModalGridCol2);
    section2.appendChild(citeModalDDDiv);

    const citationText = document.createElement('div');
    citationText.id = 'citationText';
    citationText.className = 'contentText';
    section2.appendChild(citationText);

    const copyBtn2 = document.createElement('a');
    copyBtn2.id = 'btnLinkCopy';
    copyBtn2.className = 'copyButton';
    copyBtn2.textContent = 'Copy';
    copyBtn2.href = '#';
    copyBtn2.onclick = function () {
        navigator.clipboard.writeText(citationText.textContent.trim());
        showTempMessage('Citation copied to clipboard');
    }    

    const copyBtnContainer2 = document.createElement('div');
    copyBtnContainer2.className = 'citeModalCopyBtn';
    copyBtnContainer2.appendChild(copyBtn2);

    section2.appendChild(copyBtnContainer2);
    section2.appendChild(document.createElement('div')).style.clear = 'both';
    section2.appendChild(document.createTextNode('Please review citations for accuracy before using.'));

    modalContent.appendChild(section2);

    // Create the second section of the modal
    const section3 = document.createElement('div');
    section3.className = 'citeModalSection';

    const headModal3 = document.createElement('div');
    headModal3.className = 'headModal';
    headModal3.textContent = 'Export To';
    section3.appendChild(headModal3);

    const downloads = [
        { divid: 'divDLBibTex', lnkid: 'lnkDLBibTex', text: 'BibTeX' },
        { divid: 'divDLCSL', lnkid: 'lnkDLCSL', text: 'CSL' },
        { divid: 'divDLMODS', lnkid: 'lnkDLMODS', text: 'MODS' },
        { divid: 'divDLRIS', lnkid: 'lnkDLRIS', text: 'RIS' }
    ];
    
    downloads.forEach((download, index) => {
        const downloadDiv = document.createElement('div');
        downloadDiv.id = download.divid;
        downloadDiv.className = 'citeDownload';

        const link = document.createElement('a');
        link.id = download.lnkid;
        link.href = '#';
        link.textContent = download.text;
        downloadDiv.appendChild(link);

        section3.appendChild(downloadDiv);
    });

    modalContent.appendChild(section3);

    // Append the modal to the container element (replacing existing children in case user opens dialog multiple times)
    modalContainer.replaceChildren(modalContent);
}
