<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="SegmentEdit.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.SegmentEdit"
 	ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <script language="javascript">
        function overlay() {
            el = document.getElementById("overlayitem");
            el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
        }

        function overlayAuthorSearch() {
            el = document.getElementById("overlayauthor");
            el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
        }

        function overlayRelatedSegmentSearch() {
            el = document.getElementById("overlayrelatedsegment");
            el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
        }

        function overlayPageSelect() {
            var itemIDElement = document.getElementById("itemIDLabel");
            var itemID = (itemIDElement.textContent) ? itemIDElement.textContent : itemIDElement.innerText;

            if (itemID == "") {
                alert("Please associate this segment with an item before assigning pages.");
            }
            else {
                e1 = document.getElementById("overlay");
                e1.style.visibility = (e1.style.visibility == "visible") ? "hidden" : "visible";
            }
        }

        function titleSearch(titleId, title) {
            if (titleId == "" && title == "") {
                alert("Please specify a Title ID or Title.");
                return;
            }

            executeServiceCall('services/titleservice.ashx?op=TitleSearch&titleID=' + titleId + '&title=' + title, showTitleList);
        }

        function itemSearch(titleId, marcBibId) {
            if (titleId == "" && marcBibId == "") {
                alert("Please specify a Title ID or MARC Bib ID.");
                return;
            }

            executeServiceCall('services/itemservice.ashx?op=ItemSearchByTitle&titleID=' + titleId + '&marcBibId=' + marcBibId, showItemList);
        }

        function authorSearch(authorId, authorName) {
            if (authorId == "" && authorName == "") {
                alert("Please specify an Author ID or Name.");
                return;
            }
            executeServiceCall('services/authorservice.ashx?op=AuthorSearch&authorID=' + authorId + '&authorName=' + authorName, showAuthorList);
        }

        function relatedSegmentSearch(segmentId, title) {
            if (segmentId == "" && title == "") {
                alert("Please specify an Segment ID or Title.");
                return;
            }
            executeServiceCall('services/segmentservice.ashx?op=SegmentSearch&segmentID=' + segmentId + '&title=' + title, showRelatedSegmentList);
        }

        function pageSearch() {
            var itemIdElement = document.getElementById("itemIDLabel");
            var itemId = (itemIdElement.textContent) ? itemIdElement.textContent : itemIdElement.innerText;

            if (itemId == "") {
                alert("Please associate an Item with this Segment.");
                return;
            }
            executeServiceCall('services/pageservice.ashx?op=PageSelectByItemID&itemID=' + itemId, showPageList);
        }

        function showTitleList(result) {
            var titles = eval(result);

            // Clear rows already in table
            var tbody = document.getElementById("srchTitleTable").getElementsByTagName("tbody")[0];
            var rows = document.getElementById("srchTitleTable").getElementsByTagName("tr");
            for (var j = (rows.length - 1); j >= 2; j--) tbody.removeChild(rows[j]);

            // Build the table
            for (var i = 0; i < titles.length; i++) {
                var tbody = document.getElementById("srchTitleTable").getElementsByTagName("tbody")[0];
                var row = document.createElement("tr");
                row.setAttribute("align", "left");
                var td1 = document.createElement("td");
                td1.appendChild(document.createTextNode(titles[i].TitleID));
                var td2 = document.createElement("td");
                var span = document.createElement("span");
                span.setAttribute("style", "text-decoration:underline;color:#a54219;");
                span.style.cssText = "text-decoration:underline;color:#a54219;";
                span.onclick = new Function("itemSearch('" + titles[i].TitleID + "', '')");
                span.appendChild(document.createTextNode(titles[i].SortTitle));
                td2.appendChild(span);
                row.appendChild(td1);
                row.appendChild(td2);
                tbody.appendChild(row);
            }

            // Display the table
            document.getElementById('srchResultTable').style.display = 'none';
            document.getElementById('srchTitleTable').style.display = 'block';
        }

        function showAuthorList(result) {
            var authors = eval(result);

            // Clear rows already in table
            var tbody = document.getElementById("srchAuthorResultTable").getElementsByTagName("tbody")[0];
            var rows = document.getElementById("srchAuthorResultTable").getElementsByTagName("tr");
            for (var j = (rows.length - 1); j >= 1; j--) tbody.removeChild(rows[j]);

            // Build the table
            for (var i = 0; i < authors.length; i++) {
                var tbody = document.getElementById("srchAuthorResultTable").getElementsByTagName("tbody")[0];
                var row = document.createElement("tr");
                row.setAttribute("align", "left");
                var td1 = document.createElement("td");
                td1.appendChild(document.createTextNode(authors[i].AuthorID));
                var td2 = document.createElement("td");
                var a = document.createElement("a");
                a.setAttribute("href", "#");
                a.onclick = new Function("selectAuthor('" + authors[i].AuthorID + "')");
                a.appendChild(document.createTextNode(authors[i].FullName + ' ' + authors[i].Numeration + ' ' + authors[i].Unit + ' ' + authors[i].Title + ' ' + authors[i].Location + ' ' + authors[i].FullerForm + ' ' + authors[i].Dates));
                td2.appendChild(a);
                row.appendChild(td1);
                row.appendChild(td2);
                tbody.appendChild(row);
            }

            // Display the table
            document.getElementById('srchAuthorResultTable').style.display = 'block';
        }

        function showItemList(result) {
            var items = eval(result);

            // Clear rows already in table
            var tbody = document.getElementById("srchResultTable").getElementsByTagName("tbody")[0];
            var rows = document.getElementById("srchResultTable").getElementsByTagName("tr");
            for (var j = (rows.length - 1); j >= 2; j--) tbody.removeChild(rows[j]);

            // Build the table
            for (var i = 0; i < items.length; i++) {
                var tbody = document.getElementById("srchResultTable").getElementsByTagName("tbody")[0];
                var row = document.createElement("tr");
                row.setAttribute("align", "left");
                var td1 = document.createElement("td");
                var a = document.createElement("a");
                a.setAttribute("href", "#");
                a.onclick = new Function("selectItem('" + items[i].ItemID + "')");
                a.appendChild(document.createTextNode(items[i].ItemID));
                td1.appendChild(a);
                var td2 = document.createElement("td");
                td2.appendChild(document.createTextNode(items[i].BarCode));
                var td3 = document.createElement("td");
                td3.appendChild(document.createTextNode(items[i].Volume));
                row.appendChild(td1);
                row.appendChild(td2);
                row.appendChild(td3);
                tbody.appendChild(row);
            }

            // Display the table
            document.getElementById('srchTitleTable').style.display = 'none';
            document.getElementById('srchResultTable').style.display = 'block';
        }

        function showRelatedSegmentList(result) {
            var segments = eval(result);

            // Clear rows already in table
            var tbody = document.getElementById("srchRelatedSegmentResultTable").getElementsByTagName("tbody")[0];
            var rows = document.getElementById("srchRelatedSegmentResultTable").getElementsByTagName("tr");
            for (var j = (rows.length - 1); j >= 1; j--) tbody.removeChild(rows[j]);

            // Build the table
            for (var i = 0; i < segments.length; i++) {
                var tbody = document.getElementById("srchRelatedSegmentResultTable").getElementsByTagName("tbody")[0];
                var row = document.createElement("tr");
                row.setAttribute("align", "left");
                var td1 = document.createElement("td");
                td1.appendChild(document.createTextNode(segments[i].SegmentID));
                var td2 = document.createElement("td");
                td2.appendChild(document.createTextNode(segments[i].Title));
                var td3 = document.createElement("td");
                var input = document.createElement("input");
                input.setAttribute("type", "checkbox");
                input.setAttribute("id", "addRelatedSegmentCheckbox");
                input.setAttribute("name", "addRelatedSegmentCheckbox");
                input.setAttribute("value", segments[i].SegmentID);
                td3.appendChild(input);
                row.appendChild(td1);
                row.appendChild(td2);
                row.appendChild(td3);
                tbody.appendChild(row);
            }

            // Display the table
            document.getElementById('srchRelatedSegmentResultTable').style.display = 'block';
        }

        function showPageList(result) {
            var pages = eval(result);

            // Clear rows already in table
            var tbody = document.getElementById("pagesTable").getElementsByTagName("tbody")[0];
            var rows = document.getElementById("pagesTable").getElementsByTagName("tr");
            for (var j = (rows.length - 1); j >= 2; j--) tbody.removeChild(rows[j]);

            // Build the table
            for (var i = 0; i < pages.length; i++) {
                var tbody = document.getElementById("pagesTable").getElementsByTagName("tbody")[0];
                var row = document.createElement("tr");
                row.setAttribute("align", "left");
                var td1 = document.createElement("td");
                var a = document.createElement("a");
                a.setAttribute("href", "/pagethumb/" + pages[i].PageID + ",400,400");
                a.setAttribute("target", "_blank");
                a.appendChild(document.createTextNode(pages[i].PageID));
                td1.appendChild(a);
                var td2 = document.createElement("td");
                td2.appendChild(document.createTextNode(pages[i].PageTypes));
                var td3 = document.createElement("td");
                td3.appendChild(document.createTextNode(pages[i].IndicatedPages));
                var td4 = document.createElement("td");
                var input = document.createElement("input");
                input.setAttribute("type", "checkbox");
                input.setAttribute("id", "addPageCheckbox");
                input.setAttribute("name", "addPageCheckbox");
                input.setAttribute("value", pages[i].PageID);
                td4.appendChild(input);
                row.appendChild(td1);
                row.appendChild(td2);
                row.appendChild(td3);
                row.appendChild(td4);
                tbody.appendChild(row);
            }

            // Display the table
            document.getElementById('pagesTable').style.display = 'block';
        }

        function addRelatedSegments() {
            var checkboxes; var x = 0;

            var ddlClustType = document.getElementById('selClusterType');
            var selectedType = ddlClustType.options[ddlClustType.selectedIndex].value;
            var selectedTypeLabel = ddlClustType.options[ddlClustType.selectedIndex].text;

            checkboxes = document.getElementsByName('addRelatedSegmentCheckbox');
            for (x = 0; x < checkboxes.length; x++) {
                if (checkboxes[x].checked) selectRelatedSegment(checkboxes[x].value, selectedType, selectedTypeLabel);
            }

            overlay(); __doPostBack('', '');
        }

        function addPagesToSegment() {
            var checkboxes; var x = 0;

            checkboxes = document.getElementsByName('addPageCheckbox');
            for (x = 0; x < checkboxes.length; x++) {
                if (checkboxes[x].checked) selectPage(checkboxes[x].value);
            }

            overlay(); __doPostBack('', '');
        }

        function selectPages(checkedValue) {
            var checkboxes; var x = 0;

            checkboxes = document.getElementsByName('addPageCheckbox');
            for (x = 0; x < checkboxes.length; x++) {
                checkboxes[x].checked = checkedValue;
            }
        }

        function selectPagesBetween() {
            var checkboxes; var x = 0; var doCheck = false;
            var firstChecked = -1; var lastChecked = -1;

            // Identify the first and last checked boxes
            checkboxes = document.getElementsByName('addPageCheckbox');
            for (x = 0; x < checkboxes.length; x++) {
                if (checkboxes[x].checked) {
                    if (firstChecked === -1) {
                        firstChecked = x;
                    }
                    else {
                        lastChecked = x;
                    }
                }
            }

            // Check all boxes between the first and last checked box
            if (firstChecked > -1 && lastChecked > -1)
            {
                for (x = firstChecked; x < lastChecked; x++) {
                    checkboxes[x].checked = true;
                }
            }
        }

        function executeServiceCall(url, callback) {
            var request = createXMLHttpRequest();
            request.open("GET", url, true);
            request.onreadystatechange = function () {
                if (request.readyState == 4) {
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

        function keyDownHandler(event, btn) {
            // process only the Enter key
            if ((document.all ? event.keyCode : event.which) == 13) {
                // cancel the default submit
                event.preventDefault ? event.preventDefault() : event.returnValue = false;
                event.cancel = true;
                // submit the form by programmatically clicking the specified button
                btn.click();
            }
        }
    </script>

	<a href="/">&lt; Return to Dashboard</a><br />
	<a href="/SegmentSearch.aspx">&lt; Find a Different Segment</a><br />
	<br />
	<span class="pageHeader">Segment</span><hr />
	<br />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<asp:Literal id="litMessage" runat="server"></asp:Literal>
	<br />
	<div class="box" style="padding: 5px;margin-right:15px">
		<table cellpadding="4" width="100%">
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">Segment ID:</td>
				<td style="white-space: nowrap" colspan="2" valign="middle" width="100%"><asp:Label ID="idLabel" runat="server" ForeColor="blue" /></td>
			</tr>
            <tr>
                <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Item ID:</td>
				<td style="white-space: nowrap" colspan="2" valign="middle" width="100%">
                    <asp:Label ID="itemIDLabel" ClientIDMode="Static" runat="server" />&nbsp;
                    <asp:Label ID="itemDescLabel" Text="Not Selected" style="font-style:italic" runat="server" /><br />
                    <input type="button" onclick="overlay();document.getElementById('srchTitleID').focus();" id="btnAddItem" value="Select Item" />
                    <input type="button" onclick="if (document.getElementById('pagesList')) alert('Remove all Pages before removing the Item.'); else clearItem();"; id="btnClearItem" value="Remove Item" />
                </td>
            </tr>
            <tr>
                <td style="white-space: nowrap" align="right" class="dataHeader">DOI:</td>
				<td><asp:TextBox ID="doiTextBox" runat="server" Width="300px"></asp:TextBox></td>
            </tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Status:</td>
				<td><asp:DropDownList ID="ddlSegmentStatus" DataTextField="StatusName" DataValueField="SegmentStatusID" runat="server"></asp:DropDownList></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Replaced By (Segment ID):</td>
				<td><asp:TextBox ID="replacedByTextBox" runat="server" Width="200px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Contributors:</td>
				<td colspan="4" style="width: 100%"><asp:DropDownList ID="ddlContributor" DataTextField="InstitutionName" DataValueField="InstitutionCode" runat="server"></asp:DropDownList></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader"></td>
				<td colspan="4" style="width: 100%"><asp:DropDownList ID="ddlContributor2" DataTextField="InstitutionName" DataValueField="InstitutionCode" runat="server"></asp:DropDownList></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Genre:</td>
				<td><asp:DropDownList ID="ddlSegmentGenre" DataTextField="GenreName" DataValueField="SegmentGenreID" runat="server"></asp:DropDownList></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Title:</td>
				<td colspan="4" style="width: 100%"><asp:TextBox ID="titleTextBox" runat="server" MaxLength="2000" Width="100%" TextMode="MultiLine" Height="50px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Sort Title:</td>
				<td colspan="4" style="width: 100%"><asp:TextBox ID="sortTitleTextBox" runat="server" MaxLength="2000" Width="100%" TextMode="MultiLine" Height="50px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Translated Title:</td>
				<td colspan="4" style="width: 100%"><asp:TextBox ID="translatedTitleTextBox" runat="server" MaxLength="2000" Width="100%" TextMode="MultiLine" Height="50px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">Page Range:</td>
				<td><asp:TextBox ID="pageRangeTextBox" runat="server" MaxLength="50" Width="200px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">Start Page:</td>
                <td><asp:TextBox ID="startPageTextBox" runat="server" MaxLength="20" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                End Page: <asp:TextBox ID="endPageTextBox" runat="server" MaxLength="20" Width="100px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">Start Page BHL ID:</td>
				<td><asp:TextBox ID="bhlStartPageIDTextBox" runat="server" MaxLength="10" Width="75px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">External URL:</td>
				<td><asp:TextBox ID="urlTextBox" runat="server" MaxLength="200" Width="300px"></asp:TextBox> (Example: a link to an external site's viewer)</td>
			</tr>
            <tr>
                <td style="white-space:nowrap" align="right" class="dataHeader">Download URL:</td>
                <td><asp:TextBox ID="downloadUrlTextBox" runat="server" MaxLength="200" Width="300px"></asp:TextBox> (Example: a link to a PDF of the article)</td>
            </tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">Rights Status:</td>
				<td><asp:TextBox ID="rightsStatusTextBox" runat="server" MaxLength="500" Width="400px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">Rights Statement:</td>
				<td><asp:TextBox ID="rightsStatementTextBox" runat="server" MaxLength="500" Width="400px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">License Name:</td>
				<td><asp:TextBox ID="licenseNameTextBox" runat="server" MaxLength="200" Width="400px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">License URL:</td>
				<td><asp:TextBox ID="licenseUrlTextBox" runat="server" MaxLength="200" Width="400px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Notes:</td>
				<td colspan="4" style="width: 100%"><asp:TextBox ID="notesTextBox" runat="server" MaxLength="8000" Width="100%" TextMode="MultiLine" Height="50px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Summary/Abstract:</td>
				<td colspan="4" style="width: 100%"><asp:TextBox ID="summaryTextBox" runat="server" Width="100%" TextMode="MultiLine" Height="75px"></asp:TextBox></td>
			</tr>
		</table>
        <br />
        <fieldset>
            <legend class="dataHeader">Original Publication Details</legend>
            <table cellpadding="4" width="100%">
			    <tr>
				    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Title:</td>
				    <td colspan="4" style="width: 100%"><asp:TextBox ID="containerTitleTextBox" runat="server" MaxLength="2000" Width="100%"></asp:TextBox></td>
			    </tr>
			    <tr>
                    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Publication Details:</td>
            	    <td colspan="4" style="width: 100%"><asp:TextBox ID="publicationDetailsTextBox" runat="server" MaxLength="400" Width="100%"></asp:TextBox></td>
			    </tr>
			    <tr>
				    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Publisher Name:</td>
				    <td colspan="4" style="width: 100%"><asp:TextBox ID="publisherNameTextBox" runat="server" MaxLength="250" Width="100%"></asp:TextBox></td>
			    </tr>
			    <tr>
				    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Publisher Place:</td>
				    <td colspan="4" style="width: 100%"><asp:TextBox ID="publisherPlaceTextBox" runat="server" MaxLength="150" Width="100%"></asp:TextBox></td>
			    </tr>
			    <tr>
				    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">Language:</td>
				    <td colspan="4" style="width: 100%"><asp:DropDownList ID="ddlLanguage" DataTextField="LanguageName" DataValueField="LanguageCode" runat="server"></asp:DropDownList></td>
			    </tr>
			    <tr>
				    <td style="white-space: nowrap" align="right" class="dataHeader">Volume:</td>
				    <td><asp:TextBox ID="volumeTextBox" runat="server" MaxLength="100" Width="400px"></asp:TextBox></td>
			    </tr>
			    <tr>
				    <td style="white-space: nowrap" align="right" class="dataHeader">Series:</td>
				    <td><asp:TextBox ID="seriesTextBox" runat="server" MaxLength="100" Width="400px"></asp:TextBox></td>
			    </tr>
			    <tr>
				    <td style="white-space: nowrap" align="right" class="dataHeader">Issue:</td>
				    <td><asp:TextBox ID="issueTextBox" runat="server" MaxLength="100" Width="400px"></asp:TextBox></td>
			    </tr>
			    <tr>
				    <td style="white-space: nowrap" align="right" class="dataHeader">Date:</td>
				    <td><asp:TextBox ID="dateTextBox" runat="server" MaxLength="20" Width="200px"></asp:TextBox></td>
			    </tr>
            </table>
        </fieldset>
        <br />
		<fieldset>
			<legend class="dataHeader">Segment Authors</legend>
			<asp:GridView ID="authorsList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			    AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
				Width="800px" CssClass="boxTable" OnRowCancelingEdit="authorsList_RowCancelingEdit" OnRowEditing="authorsList_RowEditing"
				OnRowUpdating="authorsList_RowUpdating" OnRowCommand="authorsList_RowCommand" DataKeyNames="SegmentAuthorID,AuthorID">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
				    <asp:BoundField DataField="AuthorID" HeaderText="Author ID" ItemStyle-Width="60px" ReadOnly="true" />
					<asp:TemplateField HeaderText="Name" ItemStyle-Width="350px" HeaderStyle-HorizontalAlign="Left">
						<ItemTemplate>
							<%# Eval( "FullName" ) %> <%# Eval("FullerForm") %> <%# Eval("Numeration") %> <%# Eval("Unit") %> <%# Eval("Title") %> <%# Eval("Location") %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Sequence" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
						<ItemTemplate>
							<%# Eval( "SequenceOrder" ) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="authorSequenceTextBox" runat="server" Width="80px" Text='<%# Eval( "SequenceOrder" ) %>' />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<ItemTemplate>
							<asp:LinkButton ID="editAuthorButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateAuthorButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelAuthorButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<br />
			<input type="button" onclick="overlayAuthorSearch();document.getElementById('srchAuthorID').focus();" id="btnAddAuthor" value="Add Author" />
		</fieldset>
		<br />
		<fieldset>
		    <legend class="dataHeader">Segment Keywords</legend>
			<asp:GridView ID="keywordsList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
				Width="800px" CssClass="boxTable" OnRowCancelingEdit="keywordsList_RowCancelingEdit" OnRowEditing="keywordsList_RowEditing"
				OnRowUpdating="keywordsList_RowUpdating" OnRowCommand="keywordsList_RowCommand" DataKeyNames="SegmentKeywordID, KeywordID, Keyword">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
					<asp:TemplateField HeaderText="Subject" ItemStyle-Width="220px" HeaderStyle-HorizontalAlign="Left">
						<ItemTemplate>
							<%# Eval( "Keyword" ) %>
						</ItemTemplate>
						<EditItemTemplate>
						    <asp:TextBox ID="txtKeyword" runat="server" Text='<%# Eval( "Keyword") %>' />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<EditItemTemplate>
							<asp:LinkButton ID="updateKeywordCreatorButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelKeywordCreatorButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<br />
			<asp:Button ID="addKeywordButton" runat="server" Text="Add Keyword" OnClick="addKeywordButton_Click" />
		</fieldset>
		<br />
		<fieldset>
			<legend class="dataHeader">Identifiers</legend>
			<asp:GridView ID="identifiersList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
				Width="800px" CssClass="boxTable" OnRowCancelingEdit="identifiersList_RowCancelingEdit" OnRowEditing="identifiersList_RowEditing"
				OnRowUpdating="identifiersList_RowUpdating" OnRowCommand="identifiersList_RowCommand" DataKeyNames="SegmentIdentifierID, IdentifierID, IdentifierValue">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
					<asp:TemplateField HeaderText="Identifier" ItemStyle-Width="400px" HeaderStyle-HorizontalAlign="Left">
						<ItemTemplate>
							<%# Eval( "IdentifierName" ) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList ID="ddlIdentifierName" runat="server" DataTextField="IdentifierName" DataValueField="IdentifierID" 
							    DataSource="<%# GetIdentifiers() %>" SelectedIndex="<%# GetIdentifierIndex( Container.DataItem ) %>" Width="400px" />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Value" ItemStyle-Width="220px" HeaderStyle-HorizontalAlign="Left">
						<ItemTemplate>
							<%# Eval( "IdentifierValue" ) %>
						</ItemTemplate>
						<EditItemTemplate>
						    <asp:TextBox ID="txtIdentifierValue" runat="server" Text='<%# Eval( "IdentifierValue") %>' />
						</EditItemTemplate>
					</asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Original Publication ID" ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false">
                        <ItemTemplate>
						    <asp:CheckBox ID="isContainerIdentifierCheckBox" Enabled="false" Checked='<%# Convert.ToInt32(Eval("IsContainerIdentifier")) == 1 %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
						    <asp:CheckBox ID="isContainerIdentifierCheckBoxEdit" Checked='<%# Convert.ToInt32(Eval("IsContainerIdentifier")) == 1 %>' runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<ItemTemplate>
							<asp:LinkButton ID="editSegmentIdentifierButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateSegmentIdentifierButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelSegmentIdentifierButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<br />
			<asp:Button ID="addSegmentIdentifierButton" runat="server" Text="Add Segment Identifier" OnClick="addSegmentIdentifierButton_Click" />
		</fieldset>
		<br />
        <fieldset>
            <legend class="dataHeader">Related Segments</legend>
            <p>
            Examples of "Related Segments" are:<br /><br />
            1) Three segments that describe the same article.<br />
            2) Two segments that describe a treatment (segment #1) which is part of an article (segment #2).<br />
            <asp:Literal ID="litPrimaryMsg" runat="server" Visible="false"><br />If one or more related segments describe the same thing as the segment being edited, use the appropropriate "Primary" checkbox to indicate the canonical segment.<br /><br /></asp:Literal>
            <asp:CheckBox ID="chkPrimary" runat="server" Text="Make segment above the Primary segment in the group" Visible="false" AutoPostBack="true"  OnCheckedChanged="chkPrimary_Click "/>
            </p>
			<asp:GridView ID="relatedSegmentsList" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			    AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white" OnRowDataBound="relatedSegmentsList_RowDataBound"
				Width="800px" CssClass="boxTable" OnRowCancelingEdit="relatedSegmentsList_RowCancelingEdit" OnRowEditing="relatedSegmentsList_RowEditing"
				OnRowUpdating="relatedSegmentsList_RowUpdating" OnRowCommand="relatedSegmentsList_RowCommand" DataKeyNames="SegmentID">
                <Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" ItemStyle-VerticalAlign="Top" />
				    <asp:BoundField DataField="SegmentID" HeaderText="ID" ItemStyle-Width="35px" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"/>
				    <asp:HyperLinkField HeaderText="Title" DataNavigateUrlFields="SegmentID" DataNavigateUrlFormatString="/SegmentEdit.aspx?id={0}" 
						ItemStyle-Width="250px" DataTextField="Title" NavigateUrl="/SegmentEdit.aspx" ItemStyle-VerticalAlign="Top" 
                        HeaderStyle-HorizontalAlign="Left"/>
					<asp:TemplateField HeaderText="Vol" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<%# Eval( "Volume" ) %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Pages" ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<%# Eval( "PageRange" ) %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Authors" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<%# Eval( "Authors" ) %>
						</ItemTemplate>
					</asp:TemplateField>
                    <asp:TemplateField HeaderText="Type" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%# Eval( "SegmentClusterTypeLabel" ) %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlClusterType" runat="server">
                                <asp:ListItem Text="Same as" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Continued in" Value="20"></asp:ListItem>
                                <asp:ListItem Text="Related to" Value="30"></asp:ListItem>
                                <asp:ListItem Text="Contains" Value="40"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField HeaderText="Primary" ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
						    <asp:CheckBox ID="isPrimaryCheckBox" Enabled="false" Checked='<%# (short)Eval("IsPrimary") == 1 %>' runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
						    <asp:CheckBox ID="isPrimaryCheckBoxEdit" Checked='<%# (short)Eval("IsPrimary") == 1%>' runat="server" />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="110px" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:LinkButton ID="editRelatedSegmentButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateRelatedSegmentButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelRelatedSegmentButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
                </Columns>
                <HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
            </asp:GridView>
            <br />
			<input type="button" onclick="overlayRelatedSegmentSearch();document.getElementById('srchRelatedSegmentID').focus();" id="btnAddRelatedSegment" value="Add Related Segment" />
        </fieldset>
        <br />
		<fieldset>
		    <legend class="dataHeader">Pages</legend>
            <br />
			<input type="button" onclick="pageSearch();overlayPageSelect();" id="btnAddPage" value="Add Page" />
			<asp:GridView ID="pagesList" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			    AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
				Width="800px" CssClass="boxTable" OnRowCancelingEdit="pagesList_RowCancelingEdit" OnRowEditing="pagesList_RowEditing"
				OnRowUpdating="pagesList_RowUpdating" OnRowCommand="pagesList_RowCommand" DataKeyNames="SegmentPageID, PageID">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
					<asp:TemplateField HeaderText="Page ID" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left">
						<ItemTemplate>
							<%# Eval( "PageID" ) %>
						</ItemTemplate>
					</asp:TemplateField>
                    <asp:TemplateField HeaderText="Sequence" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
						<ItemTemplate>
							<%# Eval( "SequenceOrder" ) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="pageSequenceTextBox" runat="server" Width="80px" Text='<%# Eval( "SequenceOrder" ) %>' />
						</EditItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left">
						<ItemTemplate>
							<%# Eval( "PageTypes" ) %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Number" HeaderStyle-HorizontalAlign="Left">
						<ItemTemplate>
							<%# Eval( "IndicatedPages" ) %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<ItemTemplate>
							<asp:LinkButton ID="editPageButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updatePageButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelPageButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<br />
		</fieldset>
		<br />
		<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
	</div>
    <div id="overlayauthor" class="overlay">
        <div style="top:1000px">
            <table cellpadding="3" class="SearchText">
	            <tr>
	                <td style="white-space: nowrap">Author ID:</td>
	                <td><input id="srchAuthorID" type="text" class="SearchText" onkeydown="keyDownHandler(event, btnAuthorSearch);" /></td>
	                <td style="white-space: nowrap">Name:</td>
	                <td><input id="srchAuthorName" type="text" class="SearchText" onkeydown="keyDownHandler(event, btnAuthorSearch);" /></td>
	                <td><input id="btnAuthorSearch" type="button" onclick="authorSearch(document.getElementById('srchAuthorID').value, document.getElementById('srchAuthorName').value);" value="Search" class="SearchText" /></td>
	            </tr>
	            <tr>
	                <td colspan="5">
	                    <table id="srchAuthorResultTable" style="display:none" cellpadding="3" cellspacing="3" width="100%">
	                      <tbody>
	                        <tr class="SearchResultsHeader" align="left">
	                            <th scope="col">Author&nbsp;ID</th>
	                            <th scope="col">Name</th>
	                        </tr>
	                      </tbody>
	                    </table>
        	        </td>
	            </tr>
            </table>
	        <a id="hypAuthorCancel" href="#" onclick="clearAuthor('');">Cancel</a>
	        <input type="hidden" id="selectedAuthor" value="" runat="server" />
        </div>
    </div>
	<div id="overlayitem" class="overlay">
	    <div style="top:100px">
	        <table cellpadding="3" class="SearchText">
	            <tr>
	                <td colspan="5" align="left">
	                    <b>Search for a title containing the item you would like to select.</b>
	                </td>
	            </tr>
	            <tr>
	                <td style="white-space: nowrap">Title ID:</td>
	                <td><input id="srchTitleID" type="text" class="SearchText" onkeydown="keyDownHandler(event, btnItemSearch);" /></td>
	                <td style="white-space: nowrap">Full Title:</td>
	                <td><input id="srchTitle" type="text" class="SearchText" onkeydown="keyDownHandler(event, btnItemSearch);" /></td>
	                <td><input id="btnItemSearch" type="button" onclick="titleSearch(document.getElementById('srchTitleID').value, document.getElementById('srchTitle').value);" value="Search" class="SearchText" /></td>
	            </tr>
	            <tr>
	                <td colspan="5">
	                    <table id="srchTitleTable" style="display:none" cellpadding="3" cellspacing="3" width="100%">
	                      <tbody>
	                        <tr>
	                            <td colspan="2" align="left">
	                                <b>Click a title to view and select an item.</b>
	                            </td>
	                        </tr>
	                        <tr class="SearchResultsHeader" align="left">
	                            <th scope="col">Title&nbsp;ID</th>
	                            <th scope="col">Title</th>
	                        </tr>
	                      </tbody>
	                    </table>
	                    <table id="srchResultTable" style="display:none" cellpadding="3" cellspacing="3" width="100%">
	                      <tbody>
	                        <tr>
	                            <td colspan="4" align="left">
	                                <b>Select the item you want to add to the title.</b>
	                            </td>
	                        </tr>
	                        <tr class="SearchResultsHeader" align="left">
	                            <th scope="col">Item&nbsp;ID</th>
	                            <th scope="col">Barcode</th>
	                            <th scope="col">Volume</th>
	                        </tr>
	                      </tbody>
	                    </table>
        	        </td>
	            </tr>
	        </table>
            <a id="hypCancel" href="#" onclick="selectItem('');">Cancel</a>
	        <input type="hidden" id="selectedItem" value="" runat="server" />
	    </div>	
	</div>
	<div id="overlay" class="overlay">
	    <div style="top:1300px; width:450px;">
            <a id="#selectpg"></a>
	        <table cellpadding="3" class="SearchText">
	            <tr>
	                <td colspan="5" align="left">
	                    <b>Check the box next to the pages you want to add to the segment.</b>
	                </td>
	            </tr>
                <tr>
                    <td colspan="5" align="center">
                        <div id="divSelectAll" onclick="selectPages(true)" class="selectPageButton">Select All</div>&nbsp;&nbsp;
                        <div id="divSelectNone" onclick="selectPages(false)" class="selectPageButton">Select None</div>&nbsp;&nbsp;
                        <div id="divSelectBtw" onclick="selectPagesBetween()" class="selectPageButton">Select Between</div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="height:250px; overflow:auto; position:inherit; width:inherit; margin:inherit;">
                        <table id="pagesTable" style="display:none" cellpadding="1" cellspacing="1" width="100%">
                            <tbody>
	                            <tr class="SearchResultsHeader" align="left">
	                                <th scope="col" style="text-align:left">Page&nbsp;ID</th>
	                                <th scope="col" style="text-align:left">Type</th>
	                                <th scope="col" style="text-align:left">Number</th>
                                    <th>&nbsp;</th>
	                            </tr>
                            </tbody>
                        </table>
                        </div>
                    </td>
                </tr>
	        </table>
	        <a id="hypPageDone" href="#" onclick="addPagesToSegment();">Done</a>&nbsp;&nbsp;
	        <a id="hypPageCancel" href="#" onclick="cancelPages();">Cancel</a>
	        <input type="hidden" id="selectedPage" value="" runat="server" />
	    </div>
	</div>
    <div id="overlayrelatedsegment" class="overlay">
        <div style="top:1400px; width:585px">
            <table cellpadding="3" class="SearchText">
	            <tr>
	                <td colspan="6" align="left">
	                    <b>Enter an ID or Title and click "Search" to find segments you would like to add, <del>or click "Suggest" to automatically find segments that may be related.</del></b>
	                </td>
	            </tr>
	            <tr>
	                <td style="white-space: nowrap">Segment ID:</td>
	                <td><input id="srchRelatedSegmentID" type="text" class="SearchText" onkeydown="keyDownHandler(event, btnRelatedSegmentSearch);" /></td>
	                <td style="white-space: nowrap">Title:</td>
	                <td><input id="srchRelatedSegmentTitle" type="text" class="SearchText" onkeydown="keyDownHandler(event, btnRelatedSegmentSearch);" /></td>
	                <td><input id="btnRelatedSegmentSearch" type="button" onclick="relatedSegmentSearch(document.getElementById('srchRelatedSegmentID').value, document.getElementById('srchRelatedSegmentTitle').value);" value="Search" class="SearchText" /></td>
                    <td><input id="btnSuggestRelatedSegments" type="button" class="SearchText" value="Suggest" disabled="true" /></td>
	            </tr>
	            <tr>
	                <td colspan="6">
                        <div style="height:250px; overflow:auto; position:inherit; width:inherit; margin:inherit;">
	                    <table id="srchRelatedSegmentResultTable" style="display:none" cellpadding="3" cellspacing="3" width="100%">
	                      <tbody>
                            <tr>
	                            <td colspan="5" align="left">
	                                <b>Check the box next to the segments you want to add.</b>
	                            </td>
	                        </tr>
	                        <tr class="SearchResultsHeader" align="left">
	                            <th scope="col">Segment&nbsp;ID</th>
	                            <th scope="col">Title</th>
                                <th scope="col">&nbsp;</th>
	                        </tr>
	                      </tbody>
	                    </table>
                        </div>
        	        </td>
	            </tr>
                <tr>
                    <td colspan="6" align="left">
                        Select the type of relationship for these segments:
                        <select id="selClusterType">
                            <option value="10">Same as</option>
                            <option value="20">Continued in</option>
                            <option value="30">Related to</option>
                            <option value="40">Contains</option>
                        </select>
                    </td>
                </tr>
            </table>
            <br />
	        <a id="hypRelatedSegmentDone" href="#" onclick="addRelatedSegments();">Done</a>&nbsp;&nbsp;
	        <a id="hypRelatedSegmentCancel" href="#" onclick="clearRelatedSegment('');">Cancel</a>
	        <input type="hidden" id="selectedRelatedSegments" value="" runat="server" />
            <input type="hidden" id="selectedClusterType" value="" runat="server" />
        </div>
    </div>
</asp:Content>
