<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="True" Codebehind="ItemEdit.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.ItemEdit"
	ValidateRequest="false" EnableEventValidation="false" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<%@ Register Src="/Controls/EditHistoryLink.ascx" TagName="EditHistoryControl" TagPrefix="mobot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <script language="javascript">
    var _submitMousedown = false;

    function overlay() {
	    el = document.getElementById("overlay");
	    el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
	}

	function overlaySegmentSearch() {
	    el = document.getElementById("overlaysegment");
	    el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
	}

    function titleSearch(titleId, title)
    {
        if (titleId == "" && title == "") {
            alert("Please specify a Title ID or Full Title.");
            return;
        }
        
        executeServiceCall('services/titleservice.ashx?op=TitleSearch&titleID=' + titleId + '&title=' + title, showTitleList);
    }

    function segmentSearch(segmentId, title) {
        if (segmentId == "" && title == "") {
            alert("Please specify an Segment ID or Title.");
            return;
        }
        executeServiceCall('services/segmentservice.ashx?op=SegmentSearch&segmentID=' + segmentId + '&title=' + title, showSegmentList);
    }

    function validateYear(year, updateFunc) {
        year = year.trim().replace(" ", "");
        executeServiceCall('services/utilityservice.ashx?op=ValidYear&year=' + encodeURIComponent(year), updateFunc);
    }

    function showTitleList(result)
    {
        var titles = eval(result);
        
        // Clear rows already in table
        var tbody = document.getElementById("srchResultTable").getElementsByTagName("tbody")[0];
        var rows = document.getElementById("srchResultTable").getElementsByTagName("tr");
        for (var j = (rows.length - 1); j >= 1; j--) tbody.removeChild(rows[j]);
        
        // Build the table
        for (var i = 0; i < titles.length; i++)
        {
            var tbody = document.getElementById("srchResultTable").getElementsByTagName("tbody")[0];
            var row = document.createElement("tr");
            row.setAttribute("align", "left");
            var td1 = document.createElement("td");
            td1.appendChild(document.createTextNode(titles[i].TitleID));
            var td2 = document.createElement("td");
            td2.appendChild(document.createTextNode(titles[i].MARCBibID));
            var td3 = document.createElement("td");
            var a = document.createElement("a");
            a.setAttribute("href", "#");
            a.onclick = new Function("selectTitle('" + titles[i].TitleID + "')");
            a.appendChild(document.createTextNode(titles[i].SortTitle));
            td3.appendChild(a);
            row.appendChild(td1);
            row.appendChild(td2);
            row.appendChild(td3);
            tbody.appendChild(row);
        }

        // Display the table
        document.getElementById('srchResultTable').style.display='block';
    }

    function showSegmentList(result) {
        var segments = eval(result);

        // Clear rows already in table
        var tbody = document.getElementById("srchSegmentResultTable").getElementsByTagName("tbody")[0];
        var rows = document.getElementById("srchSegmentResultTable").getElementsByTagName("tr");
        for (var j = (rows.length - 1); j >= 1; j--) tbody.removeChild(rows[j]);

        // Build the table
        for (var i = 0; i < segments.length; i++) {
            var tbody = document.getElementById("srchSegmentResultTable").getElementsByTagName("tbody")[0];
            var row = document.createElement("tr");
            row.setAttribute("align", "left");
            var td1 = document.createElement("td");
            td1.appendChild(document.createTextNode(segments[i].SegmentID));
            var td2 = document.createElement("td");
            var a = document.createElement("a");
            a.setAttribute("href", "#");
            a.onclick = new Function("selectSegment('" + segments[i].SegmentID + "')");
            a.appendChild(document.createTextNode(segments[i].Title));
            td2.appendChild(a);
            var td3 = document.createElement("td");
            td3.appendChild(document.createTextNode(segments[i].BookID == null ? "" : segments[i].BookID));
            row.appendChild(td1);
            row.appendChild(td2);
            row.appendChild(td3);
            tbody.appendChild(row);
        }

        // Display the table
        document.getElementById('srchSegmentResultTable').style.display = 'block';
    }

    function updateStartYear(result) { updateYear(result, 'spanStartYearMessage'); }
    function updateEndYear(result) { updateYear(result, 'spanEndYearMessage'); }

    function updateYear(result, span)
    {
        var spanStyle = "none";
        if (result != true) spanStyle = "block";
        document.getElementById(span).style.display = spanStyle;

        if (_submitMousedown) {
            _submitMousedown = false;
            document.getElementById("saveButton").click();
        }
    }

    function executeServiceCall(url, callback)
    {
        var request = createXMLHttpRequest();
        request.open("GET", url, true);
        request.onreadystatechange = function() {
            if (request.readyState == 4)
            {
                if (request.status == 200) {
                    var result = eval('(' + request.responseText + ')');
                    callback(result);
                }
            }
        }
        request.send(null);
    }

    function createXMLHttpRequest() {
        if (typeof XMLHttpRequest != "undefined") {
            return new XMLHttpRequest();
        } else if (typeof ActiveXObject != "undefined") {
            return new ActiveXObject("Microsoft.XMLHTTP");
        } else {
            throw new Error("XMLHttpRequest not supported");
        }
    }
    
    function keyDownHandler(event, btn)
    {
        // process only the Enter key
        if ((document.all ? event.keyCode : event.which) == 13)
        {
            // cancel the default submit
            event.preventDefault ? event.preventDefault() : event.returnValue = false;
            event.cancel = true;
            // submit the form by programmatically clicking the specified button
            btn.click();
        }
    }

    function validateRedirect() {
        var statusDDL = document.getElementById("ddlItemStatus");
        var replacedByTB = document.getElementById("replacedByTextBox");
        var replaceWarningDiv = document.getElementById("replaceWarning");

        var visibility = "none";
        var border = "";
        if (statusDDL.value != 40 && replacedByTB.value == "") {
            visibility = "block";
            border = "2px solid #ed7600";
        }

        replaceWarningDiv.style.display = visibility;
        replacedByTB.style.border = border;
    }

    document.getElementById("masterForm").onsubmit = function () {
        var statusDDL = document.getElementById("ddlItemStatus");
        var statusOrig = document.getElementById("itemStatusOrig");
        var replacedByTB = document.getElementById("replacedByTextBox");
        var replacedByOrig = document.getElementById("replacedByOrig");

        if (statusDDL.value != 40 && replacedByTB.value == '' &&
            (statusOrig.value == '40' || replacedByOrig.value != '')) {
            if (confirm('You are REMOVING CONTENT without REPLACING it.  Continue?')) { return true; } else { return false; }
        }

        return true;
    }
    </script>
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Item</span><hr />
	<br />
	<table>
		<tr>
			<td>
				Item ID:
				<asp:TextBox ID="itemIdTextBox" runat="server"></asp:TextBox>
			</td>
			<td style="padding-left: 10px">
				Barcode:
				<asp:TextBox ID="barCodeTextBox" runat="server"></asp:TextBox>
			</td>
			<td style="padding-left: 10px">
				<asp:Button ID="searchButton" runat="server" Text="Search" OnClick="searchButton_Click" />
			</td>
		</tr>
	</table>
	<br />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<asp:Literal id="litMessage" runat="server"></asp:Literal>
	<br />
	<div class="box" style="padding: 5px;margin-right:5px">
		<table cellpadding="4" width="100%">
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Item ID:
				</td>
				<td width="100%">
					<asp:Label ID="itemIdLabel" runat="server" ForeColor="Blue"></asp:Label>&nbsp;
                    <asp:Image ID="FlickrImage" runat="server" ImageUrl="images/flickr_sml.png" ImageAlign="Bottom" AlternateText="Item in Flickr" ToolTip="Item in Flickr" Visible="false" />
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Barcode:
				</td>
				<td>
					<asp:Label ID="barcodeLabel" runat="server" ForeColor="Blue"></asp:Label>
				</td>
			</tr>
            <tr>
                <td></td>
                <td>
                    <asp:HyperLink ID="hypMarc" runat="server" NavigateUrl="#" Text="View Original MARC Record" Visible="false"></asp:HyperLink>
                </td>
            </tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Marc Item ID:
				</td>
				<td>
					<asp:TextBox ID="marcItemIDTextBox" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Call Number:
				</td>
				<td>
					<asp:TextBox ID="callNumberTextBox" runat="server" MaxLength="100" Width="400px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Volume:
				</td>
				<td>
					<asp:TextBox ID="volumeTextBox" runat="server" MaxLength="100" Width="400px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Holding Institution:
				</td>
				<td>
					<asp:DropDownList ID="ddlInst" runat="server">
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
			    <td style="white-space: nowrap" align="right" class="dataHeader">
			        Sponsor:
			    </td>
			    <td>
					<asp:TextBox ID="sponsorTextBox" runat="server" MaxLength="100" Width="400px"></asp:TextBox>
			    </td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Language (MARC 008):
				</td>
				<td>
					<asp:DropDownList ID="ddlLang" runat="server">
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Copy-specific Information:
				</td>
				<td>
					<asp:TextBox ID="itemDescriptionTextBox" runat="server" TextMode="MultiLine" Rows="4" Width="600px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Notes (not for public display):
				</td>
				<td>
					<asp:TextBox ID="notesTextBox" runat="server" TextMode="MultiLine" Rows="2" Width="600px" MaxLength="255"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Vault:
				</td>
				<td>
					<asp:DropDownList ID="ddlVault" runat="server">
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Item Status:
				</td>
				<td>
					<asp:DropDownList ID="ddlItemStatus" ClientIDMode="Static" runat="server" onchange="validateRedirect();"></asp:DropDownList>
                    <asp:HiddenField ID="itemStatusOrig" ClientIDMode="Static" runat="server" />
                    <div id="replaceWarning" style="color:#ed7600; display:none;">
                        DEAD LINK WARNING:  Are you replacing this item with another copy?<br />
                        Enter the ID of the replacement ITEM in the "Replaced By" field.<br />
                        To REMOVE content click "Save" below.
                    </div>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Replaced By (Item ID):
				</td>
				<td>
				    <asp:TextBox ID="replacedByTextBox" ClientIDMode="Static" runat="server" Width="200px" onchange="validateRedirect();"></asp:TextBox>
                    <asp:HiddenField ID="replacedByOrig" ClientIDMode="Static" runat="server" />
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					<span style="position:relative;top:8px;">Year:</span>
				</td>
				<td style="position:relative">
					<span>Start</span><span style="position:absolute;left:70px">End</span><br />
					<asp:TextBox ID="startYearTextBox" ClientIDMode="Static" runat="server" MaxLength="20" Width="50px" onblur="validateYear(document.getElementById('startYearTextBox').value, updateStartYear);" ></asp:TextBox>
					-
					<asp:TextBox ID="endYearTextBox" style="position:absolute;left:70px" ClientIDMode="Static" runat="server" MaxLength="20" Width="50px" onblur="validateYear(document.getElementById('endYearTextBox').value, updateEndYear);" ></asp:TextBox>
                    <span id="spanStartYearMessage" style="display:none; color:red">Start Year must be formatted as "YYYY".</span>
                    <span id="spanEndYearMessage" style="display:none; color:red">End Year must be formatted as "YYYY".</span>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Identifier Bib:
				</td>
				<td>
					<asp:TextBox ID="identifierBibTextBox" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
				</td>
			</tr>
			<tr>
			    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
			        ZQuery:
			    </td>
			    <td>
    			    <asp:TextBox ID="zQueryTextBox" runat="server" MaxLength="200" Width="400px"></asp:TextBox>
			    </td>
			</tr>
			<tr>
			    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
			        License Type:
			    </td>
			    <td>
    			    <asp:TextBox ID="licenseUrlTextBox" runat="server" Width="400px"></asp:TextBox>
			    </td>
			</tr>
			<tr>
			    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
			        Rights:
			    </td>
			    <td>
    			    <asp:TextBox ID="rightsTextBox" runat="server" Width="400px"></asp:TextBox>
			    </td>
			</tr>
			<tr>
			    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
			        Due Diligence:
			    </td>
			    <td>
    			    <asp:TextBox ID="dueDiligenceTextBox" runat="server" Width="400px"></asp:TextBox>
			    </td>
			</tr>
			<tr>
			    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
			        Copyright Status:
			    </td>
			    <td>
    			    <asp:TextBox ID="copyrightStatusTextBox" runat="server" Width="600px"></asp:TextBox>
			    </td>
			</tr>
			<tr>
			    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
			        Copyright Region:
			    </td>
			    <td>
    			    <asp:TextBox ID="copyrightRegionTextBox" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
			    </td>
			</tr>
			<tr>
			    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
			        Copyright Comment:
			    </td>
			    <td>
    			    <asp:TextBox ID="copyrightCommentTextBox" runat="server" TextMode="MultiLine" Rows="4"  Width="600px"></asp:TextBox>
			    </td>
			</tr>
			<tr>
			    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
			        Copyright Evidence:
			    </td>
			    <td>
    			    <asp:TextBox ID="copyrightEvidenceTextBox" runat="server" TextMode="MultiLine" Rows="4" Width="600px"></asp:TextBox>
			    </td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Rights Holder:
				</td>
				<td>
					<asp:DropDownList ID="ddlRights" runat="server"></asp:DropDownList>
				</td>
			</tr>
			<tr>
			    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
			        External URL:
			    </td>
			    <td>
    			    <asp:TextBox ID="externalUrlTextBox" runat="server" Width="400px"></asp:TextBox><br />(Include this if the item content is not stored in BHL.)
			    </td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Scanning Institution:
				</td>
				<td>
					<asp:DropDownList ID="ddlScanningInstitution" runat="server"></asp:DropDownList><br />(Labeled "Added By:" on the BHL web site.)
				</td>
			</tr>
            <tr>
                <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
                    Thumbnail Page ID:
                </td>
                <td>
					<asp:DropDownList ID="ddlThumbnailPageID" runat="server"></asp:DropDownList><br />(Used for the item thumbnail on the BHL web site.  Defaults to Title Page if no selection here.)
                </td>
            </tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Page Progression:
				</td>
				<td>
					<asp:DropDownList ID="ddlPageProgression" runat="server">
						<asp:ListItem Value=""></asp:ListItem>
						<asp:ListItem Value="lr">Left-to-Right</asp:ListItem>
						<asp:ListItem Value="rl">Right-to-Left</asp:ListItem>
					</asp:DropDownList><br />
					(If no value is selected, the item will default to Left-to-Right page progression.)
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Scanned By:
				</td>
				<td>
					<asp:Label ID="scannedByLabel" runat="server" ForeColor="Blue"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Scanned Date:
				</td>
				<td>
					<asp:Label ID="scannedDateLabel" runat="server" ForeColor="blue"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Creation Date:
				</td>
				<td>
					<asp:Label ID="creationDateLabel" runat="server" ForeColor="blue"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Last Modified Date:
				</td>
				<td>
					<asp:Label ID="lastModifiedDateLabel" runat="server" ForeColor="blue"></asp:Label>
				</td>
			</tr>
		</table>
		<br />
		<fieldset>
		    <legend class="dataHeader">Titles</legend>
		    <asp:GridView ID="titleList" runat="server" AutoGenerateColumns="false" CellPadding="5" GridLines="None" AllowSorting="false" AlternatingRowStyle-BackColor="#F7FAFB"
				Width="600px" RowStyle-BackColor="white" CssClass="boxTable" OnRowCancelingEdit="titleList_RowCancelingEdit"  OnRowCommand="titleList_RowCommand" 
				OnRowEditing="titleList_RowEditing" OnRowUpdating="titleList_RowUpdating" DataKeyNames="TitleID">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
				    <asp:BoundField DataField="TitleID" HeaderText="Title ID" ItemStyle-Width="60px" ReadOnly="true" />
				    <asp:HyperLinkField HeaderText="Title" DataNavigateUrlFields="TitleID" DataNavigateUrlFormatString="/TitleEdit.aspx?id={0}" 
						ItemStyle-Width="360px" DataTextField="ShortTitle" NavigateUrl="/TitleEdit.aspx" />

					<asp:TemplateField HeaderText="Primary" ItemStyle-Width="70px">
						<ItemTemplate>
						    <asp:CheckBox ID="isPrimaryCheckBox" Enabled=false Checked='<%# Eval("IsPrimary").ToString() == "1" %>' runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
						    <asp:CheckBox ID="isPrimaryCheckBoxEdit" Checked='<%# Eval("IsPrimary").ToString() == "1" %>' runat="server" />
						</EditItemTemplate>
					</asp:TemplateField>

					<asp:TemplateField ItemStyle-Width="110px">
						<ItemTemplate>
							<asp:LinkButton ID="editItemButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateItemButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelItemButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<input type="button" onclick="overlay();document.getElementById('srchTitleID').focus();" id="btnAddTitle" value="Add Title" />
		</fieldset>
		<br />
		<fieldset>
			<legend class="dataHeader">Languages (MARC 041)</legend>
			<asp:GridView ID="languagesList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB"
				RowStyle-BackColor="white" Width="400px" CssClass="boxTable" OnRowCancelingEdit="languagesList_RowCancelingEdit" OnRowEditing="languagesList_RowEditing"
				OnRowUpdating="languagesList_RowUpdating" OnRowCommand="languagesList_RowCommand" DataKeyNames="ItemLanguageID,LanguageCode">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
					<asp:TemplateField HeaderText="Language Code" ItemStyle-Width="200px">
						<ItemTemplate>
							<%# Eval( "LanguageName" ) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList ID="ddlLanguageName" runat="server" DataTextField="LanguageName" DataValueField="LanguageCode" DataSource="<%# GetLanguages() %>"
								SelectedIndex="<%# GetLanguageIndex( Container.DataItem ) %>" Width="200px" />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<ItemTemplate>
							<asp:LinkButton ID="editLanguageButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateLanguageButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelLanguageButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<br />
			<asp:Button ID="addLanguageButton" runat="server" Text="Add Language" OnClick="addLanguageButton_Click" />
		</fieldset>
		<br />
		<fieldset>
			<legend class="dataHeader">Collections</legend>
			<asp:GridView ID="collectionsList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB"
				RowStyle-BackColor="white" Width="400px" CssClass="boxTable" OnRowCancelingEdit="collectionsList_RowCancelingEdit" OnRowEditing="collectionsList_RowEditing"
				OnRowUpdating="collectionsList_RowUpdating" OnRowCommand="collectionsList_RowCommand" DataKeyNames="ItemCollectionID,CollectionID">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
					<asp:TemplateField HeaderText="Collection Name" ItemStyle-Width="200px">
						<ItemTemplate>
							<%# Eval( "CollectionName" ) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList ID="ddlCollection" runat="server" DataTextField="CollectionName" DataValueField="CollectionID" DataSource="<%# GetCollections() %>"
								SelectedIndex="<%# GetCollectionIndex( Container.DataItem ) %>" Width="200px" />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<ItemTemplate>
							<asp:LinkButton ID="editCollectionButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateCollectionButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelCollectionButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<br />
			<asp:Button ID="addCollectionButton" runat="server" Text="Add Collection" OnClick="addCollectionButton_Click" />
		</fieldset>
		<br />
		<fieldset>
			<legend class="dataHeader">Segments</legend>
            <br />
			<input type="button" onclick="overlaySegmentSearch();document.getElementById('srchSegmentID').focus();" id="btnAddSegment" value="Add Segment" />
			<asp:GridView ID="segmentsList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			    AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
				Width="800px" CssClass="boxTable" OnRowCancelingEdit="segmentsList_RowCancelingEdit" OnRowEditing="segmentsList_RowEditing"
				OnRowUpdating="segmentsList_RowUpdating" OnRowCommand="segmentsList_RowCommand" DataKeyNames="SegmentID">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" ItemStyle-VerticalAlign="Top "/>
				    <asp:HyperLinkField HeaderText="ID" DataNavigateUrlFields="SegmentID" DataNavigateUrlFormatString="/SegmentEdit.aspx?id={0}" 
						ItemStyle-Width="35px" DataTextField="SegmentID" NavigateUrl="/SegmentEdit.aspx" ItemStyle-VerticalAlign="Top" />
					<asp:TemplateField HeaderText="Title" ItemStyle-Width="350px" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<%# Eval( "Title" ) %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Volume" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<%# Eval( "Volume" ) %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Pages" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<%# Eval( "PageRange" ) %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Status" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<%# Eval( "StatusName" ) %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Sequence" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<%# Eval( "SequenceOrder" ) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="segmentSequenceTextBox" runat="server" Width="80px" Text='<%# Eval( "SequenceOrder" ) %>' />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:LinkButton ID="editSegmentButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateSegmentButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelSegmentButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<br />
		</fieldset>
		<br />
		<fieldset id="pageFieldSet" runat="server">
			<legend class="dataHeader">Pages</legend>
			<asp:Button ID="PaginatorButton" runat="server" Text="Paginate This Item" 
                onclick="PaginatorButton_Click" />
			<asp:GridView ID="pageList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" AllowSorting="true" AlternatingRowStyle-BackColor="#F7FAFB"
				Width="600px" RowStyle-BackColor="white" CssClass="boxTable" OnRowCancelingEdit="pageList_RowCancelingEdit" OnRowEditing="pageList_RowEditing" OnRowUpdating="pageList_RowUpdating"
				OnSorting="pageList_Sorting" OnRowDataBound="pageList_RowDataBound">
				<Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <ItemTemplate>
                            <asp:ImageButton ID="FlickrLinkButton" runat="server" AlternateText="View in Flickr" ToolTip="View in Flickr" />
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:BoundField DataField="PageID" HeaderText="Page ID" SortExpression="PageID" ItemStyle-Width="80px" ReadOnly="true" />
					<asp:BoundField HeaderText="File Name Prefix" DataField="FileNamePrefix" ItemStyle-Width="200px" SortExpression="FileNamePrefix" />
					<asp:TemplateField HeaderText="Sequence" ItemStyle-Width="80px" SortExpression="SequenceOrder">
						<ItemTemplate>
							<%# Eval( "SequenceOrder" ) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="sequenceOrderTextBox" runat="server" Width="80px" Text='<%# Eval( "SequenceOrder" ) %>' />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:HyperLinkField DataNavigateUrlFields="PageID" DataNavigateUrlFormatString="/NamePageEdit.aspx?id={0}"
						ItemStyle-Width="130px" Text="Edit Names" NavigateUrl="/NamePageEdit.aspx" />
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
		</fieldset>
		<br />
		<asp:Button ID="saveButton" runat="server" ClientIDMode="Static" OnClick="saveButton_Click" Text="Save" />
		<div style="float:right;"><mobot:EditHistoryControl runat="server" id="editHistoryControl" /></div>
	</div>
	<div id="overlay" class="overlay">
	    <div style="top:900px">
	        <table cellpadding="3" class="SearchText">
	            <tr>
	                <td style="white-space: nowrap">Title ID:</td>
	                <td><input id="srchTitleID" type="text" class="SearchText" onkeydown="keyDownHandler(event, btnTitleSearch);" /></td>
	                <td style="white-space: nowrap">Full Title:</td>
	                <td><input id="srchTitle" type="text" class="SearchText" onkeydown="keyDownHandler(event, btnTitleSearch);" /></td>
	                <td><input id="btnTitleSearch" type="button" onclick="titleSearch(document.getElementById('srchTitleID').value, document.getElementById('srchTitle').value);" value="Search" class="SearchText" /></td>
	            </tr>
	            <tr>
	                <td colspan="5">
	                    <table id="srchResultTable" style="display:none" cellpadding="3" cellspacing="3" width="100%">
	                      <tbody>
	                        <tr class="SearchResultsHeader" align="left">
	                            <th scope="col">Title&nbsp;ID</th>
	                            <th scope="col">MARC Bib ID</th>
	                            <th scope="col">Title</th>
	                        </tr>
	                      </tbody>
	                    </table>
        	        </td>
	            </tr>
	        </table>
	        <a id="hypCancel" href="#" onclick="selectTitle('');">Cancel</a>
	        <input type="hidden" id="selectedTitle" runat="server" />
	    </div>	
	</div>
    <div id="overlaysegment" class="overlay">
        <div style="top:1200px">
            <table cellpadding="3" class="SearchText">
	            <tr>
	                <td style="white-space: nowrap">Segment ID:</td>
	                <td><input id="srchSegmentID" type="text" class="SearchText" onkeydown="keyDownHandler(event, btnSegmentSearch);" /></td>
	                <td style="white-space: nowrap">Title:</td>
	                <td><input id="srchSegmentTitle" type="text" class="SearchText" onkeydown="keyDownHandler(event, btnSegmentSearch);" /></td>
	                <td><input id="btnSegmentSearch" type="button" onclick="segmentSearch(document.getElementById('srchSegmentID').value, document.getElementById('srchSegmentTitle').value);" value="Search" class="SearchText" /></td>
	            </tr>
	            <tr>
	                <td colspan="5">
	                    <table id="srchSegmentResultTable" style="display:none" cellpadding="3" cellspacing="3" width="100%">
	                      <tbody>
	                        <tr class="SearchResultsHeader" align="left">
	                            <th scope="col">Segment&nbsp;ID</th>
	                            <th scope="col">Title</th>
                                <th scope="col">Part of Item</th>
	                        </tr>
	                      </tbody>
	                    </table>
        	        </td>
	            </tr>
            </table>
	        <a id="hypSegmentCancel" href="#" onclick="clearSegment('');">Cancel</a>
	        <input type="hidden" id="selectedSegment" value="" runat="server" />
        </div>
    </div>
    <script>
    document.getElementById("saveButton").onmousedown = function () {
        _submitMousedown = true;
    }
    </script>
</asp:Content>
