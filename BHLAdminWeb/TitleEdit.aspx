<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="True" Codebehind="TitleEdit.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.TitleEdit"
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
    
    function titleSearch(titleId, title) {
        if (titleId == "" && title == "") {
            alert("Please specify a Title ID or Title.");
            return;
        }

        executeServiceCall('services/titleservice.ashx?op=TitleSearch&titleID=' + titleId + '&title=' + title, showTitleList);
    }

    function authorSearch(authorId, authorName) {
        if (authorId == "" && authorName == "") {
            alert("Please specify an Author ID or Name.");
            return;
        }
        executeServiceCall('services/authorservice.ashx?op=AuthorSearch&authorID=' + authorId + '&authorName=' + authorName, showAuthorList);
    }

    function itemSearch(titleId, marcBibId)
    {
        if (titleId == "" && marcBibId == "") {
            alert("Please specify a Title ID or MARC Bib ID.");
            return;
        }
        
        executeServiceCall('services/itemservice.ashx?op=ItemSearchByTitle&titleID=' + titleId + '&marcBibId=' + marcBibId, showItemList);
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

    function showItemList(result)
    {
        var items = eval(result);
        
        // Clear rows already in table
        var tbody = document.getElementById("srchResultTable").getElementsByTagName("tbody")[0];
        var rows = document.getElementById("srchResultTable").getElementsByTagName("tr");
        for (var j = (rows.length - 1); j >= 2; j--) tbody.removeChild(rows[j]);
        
        // Build the table
        for (var i = 0; i < items.length; i++)
        {
            var tbody = document.getElementById("srchResultTable").getElementsByTagName("tbody")[0];
            var row = document.createElement("tr");
            row.setAttribute("align", "left");
            var td1 = document.createElement("td");
            td1.appendChild(document.createTextNode(items[i].ItemID));
            var td2 = document.createElement("td");
            td2.appendChild(document.createTextNode(items[i].BarCode));
            var td3 = document.createElement("td");
            td3.appendChild(document.createTextNode(items[i].Volume));
            var td4 = document.createElement("td");
            var input = document.createElement("input");
            input.setAttribute("type", "checkbox");
            input.setAttribute("id", "makePrimaryCheckbox");
            input.setAttribute("name", "makePrimaryCheckbox");
            input.setAttribute("value", items[i].ItemID);
            td4.appendChild(input);
            row.appendChild(td1);
            row.appendChild(td2);
            row.appendChild(td3);
            row.appendChild(td4);
            tbody.appendChild(row);
        }

        // Display the table
        document.getElementById('srchTitleTable').style.display = 'none';
        document.getElementById('srchResultTable').style.display = 'block';
    }

    function executeServiceCall(url, callback)
    {
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

    function validateRedirect()
    {
        var publishedCB = document.getElementById("publishReadyCheckBox");
        var replacedByTB = document.getElementById("replacedByTextBox");
        var replaceWarningDiv = document.getElementById("replaceWarning");

        var visibility = "none";
        var border = "";
        if (!publishedCB.checked && replacedByTB.value == "") {
            visibility = "block";
            border = "2px solid #ed7600";
        }

        replaceWarningDiv.style.display = visibility;
        replacedByTB.style.border = border;
    }

    document.getElementById("masterForm").onsubmit = function () {
        var publishedCB = document.getElementById("publishReadyCheckBox");
        var publishedOrig = document.getElementById("publishReadyOrig");
        var replacedByTB = document.getElementById("replacedByTextBox");
        var replacedByOrig = document.getElementById("replacedByOrig");

        if (!publishedCB.checked && replacedByTB.value == '' &&
            (publishedOrig.value == 'True' || replacedByOrig.value != '') ) {
            if (confirm('You are removing TITLE METADATA from BHL\'s SEARCH INDEX.  Continue?')) { return true; } else { return false; }
        }

        return true;
    }

    </script>
	<a href="/">&lt; Return to Dashboard</a><br />
	<a href="/TitleSearch.aspx">&lt; Find a Different Title</a><br />
	<br />
	<span class="pageHeader">Title</span><hr />
	<br />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<asp:Literal id="litMessage" runat="server"></asp:Literal>
	<br />
	<div class="box" style="padding: 5px;margin-right:5px">
		<table cellpadding="4" width="100%">
			<tr>
				<td style="white-space: nowrap;" align="right" valign="top" class="dataHeader">
					Title ID:
				</td>
				<td style="white-space: nowrap; padding-top:0px" colspan="4" valign="middle" width="100%">
					<asp:Label ID="idLabel" runat="server" ForeColor="blue" />
					<asp:CheckBox ID="publishReadyCheckBox" ClientIDMode="Static" onclick="validateRedirect();" runat="server" />Publish On BHL Portal
                    <asp:HiddenField ID="publishReadyOrig" ClientIDMode="Static" runat="server" />
                    <div id="replaceWarning" style="color:#ed7600; display:none;">
                        You are removing TITLE METADATA from BHL's SEARCH INDEX.<br>
                        To redirect to another title use "Replaced By" field below.<br>
                        To remove content files, edit "Item Status" in ITEMS screen.
                    </div>
				</td>
			</tr>
            <tr>
                <td></td>
                <td>
                    <a id="hypMarc" runat="server" href="#" onclick="javascript:window.open('TitleItemMarc.aspx?type=t&id={0}', '', 'width=600,height=600,location=0,status=0,scrollbars=1');" visible="false">View Original MARC Record</a>
                </td>
            </tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Replaced By (Title ID):
				</td>
				<td>
				    <asp:TextBox ID="replacedByTextBox" ClientIDMode="Static" runat="server" onchange="validateRedirect();" Width="200px"></asp:TextBox>
                    <asp:HiddenField ID="replacedByOrig" ClientIDMode="Static" runat="server" />
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					MARC Leader:
				</td>
				<td colspan="4">
					<asp:Label ID="marcLeaderLabel" runat="server" ForeColor="blue" />
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					MARC Bib ID:
				</td>
				<td colspan="4">
				    <asp:Label ID="marcBibIdLabel" runat="server" ForeColor="Blue"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Bibliographic Level (MARC Leader char 07):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:DropDownList ID="ddlBibliographicLevel" runat="server">
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Full Title (MARC 245a,b,c):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="fullTitleTextBox" runat="server" MaxLength="8000" Width="100%" TextMode="MultiLine" Height="50px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Part/Section Number (MARC 245n):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="partNumberTextBox" runat="server" MaxLength="255" Width="400px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Part/Section Name (MARC 245p):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="partNameTextBox" runat="server" MaxLength="255" Width="400px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Short Title (MARC 245a):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="shortTitleTextBox" runat="server" MaxLength="255" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Sort Title:
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="sortTitleTextBox" runat="server" MaxLength="60" Width="400px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Uniform Title (MARC 130a or 240a):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="uniformTitleTextBox" runat="server" MaxLength="255" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Call Number (MARC 050a,b):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="callNumberTextBox" runat="server" MaxLength="100" Width="400px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Language (MARC 008):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:DropDownList ID="ddlLang" runat="server">
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Description:
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="descTextBox" runat="server" MaxLength="8000" Width="100%" TextMode="MultiLine" Height="50px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Publication Place (MARC 260/264a):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="publicationPlaceTextBox" runat="server" MaxLength="150" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Publisher Name (MARC 260/264b):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="publisherNameTextBox" runat="server" MaxLength="255" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Publication Date (MARC 260/264c):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="publicationDateTextBox" runat="server" MaxLength="100" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Start Year (MARC 008 char8-11):
				</td>
				<td>
					<asp:TextBox ID="startYearTextBox" runat="server"></asp:TextBox>
				</td>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					End Year (MARC 008 char12-15):
				</td>
				<td>
					<asp:TextBox ID="endYearTextBox" runat="server"></asp:TextBox>
				</td>
				<td width="100%">
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Original Cataloging Source (MARC 040a):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="OrigCatalogSourceTextBox" runat="server" MaxLength="100" Width="400px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Edition Statement (MARC 250a,b):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="EditionStatementTextBox" runat="server" MaxLength="450" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Publication Frequency (MARC 310a):
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="PubFrequencyTextBox" runat="server" MaxLength="100" Width="400px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Notes:
				</td>
				<td colspan="4" style="width: 100%">
					<asp:TextBox ID="notesTextBox" runat="server" MaxLength="255" Width="100%"></asp:TextBox>
				</td>
			</tr>
		</table>
		<fieldset>
			<legend class="dataHeader">Creators (MARC 1XX and 70X-75X)</legend>
			<asp:GridView ID="creatorsList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
				Width="1000px" CssClass="boxTable" OnRowCancelingEdit="creatorsList_RowCancelingEdit" OnRowEditing="creatorsList_RowEditing"
				OnRowUpdating="creatorsList_RowUpdating" OnRowCommand="creatorsList_RowCommand" DataKeyNames="TitleAuthorID,AuthorID,AuthorRoleID">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
					<asp:TemplateField HeaderText="Name" ItemStyle-Width="300px">
						<ItemTemplate>
							<%# Eval( "FullName" ) %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Role" ItemStyle-Width="300px">
						<ItemTemplate>
							<%# Eval( "RoleDescription" ) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList ID="ddlCreatorRole" runat="server" DataTextField="RoleDescription" DataValueField="AuthorRoleID"
								DataSource="<%# GetAuthorRoles() %>" SelectedIndex="<%# GetAuthorRoleIndex( Container.DataItem ) %>" />
						</EditItemTemplate>
					</asp:TemplateField>
                    <asp:TemplateField HeaderText="Relationship" ItemStyle-Width="75">
                        <ItemTemplate>
                            <%# Eval("Relationship") %>
                        </ItemTemplate>
                        <EditItemTemplate>
						    <asp:TextBox ID="txtRelationship" runat="server" Text='<%# Eval( "Relationship") %>' Width="75" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title Of Work" ItemStyle-Width="225">
                        <ItemTemplate>
                            <%# Eval("TitleOfWork") %>
                        </ItemTemplate>
                        <EditItemTemplate>
						    <asp:TextBox ID="txtTitleOfWork" runat="server" Text='<%# Eval( "TitleOfWork") %>' Width="225"/>
                        </EditItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<ItemTemplate>
							<asp:LinkButton ID="editTitleCreatorButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateTitleCreatorButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelTitleCreatorButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
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
		    <legend class="dataHeader">Subjects (MARC 6XX)</legend>
			<asp:GridView ID="subjectsList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
				Width="800px" CssClass="boxTable" OnRowCancelingEdit="subjectsList_RowCancelingEdit" OnRowEditing="subjectsList_RowEditing"
				OnRowUpdating="subjectsList_RowUpdating" OnRowCommand="subjectsList_RowCommand" DataKeyNames="TitleKeywordID, KeywordID, Keyword">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
					<asp:TemplateField HeaderText="Subject" ItemStyle-Width="220px">
						<ItemTemplate>
							<%# Eval( "Keyword" ) %>
						</ItemTemplate>
						<EditItemTemplate>
						    <asp:TextBox ID="txtKeyword" runat="server" Text='<%# Eval( "Keyword") %>' />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="MARC Data Field" ItemStyle-Width="220px" ItemStyle-HorizontalAlign="Center">
						<ItemTemplate>
							<%# Eval( "MarcDataFieldTag" ) %>
						</ItemTemplate>
						<EditItemTemplate>
						    <asp:TextBox ID="txtMarcDataFieldTag" runat="server" Text='<%# Eval( "MarcDataFieldTag") %>' />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="MARC Code" ItemStyle-Width="220px" ItemStyle-HorizontalAlign="Center">
						<ItemTemplate>
							<%# Eval( "MarcSubFieldCode" ) %>
						</ItemTemplate>
						<EditItemTemplate>
						    <asp:TextBox ID="txtMarcSubFieldCode" runat="server" Text='<%# Eval( "MarcSubFieldCode") %>' />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<EditItemTemplate>
							<asp:LinkButton ID="updateSubjectCreatorButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelSubjectCreatorButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<br />
			<asp:Button ID="addSubjectButton" runat="server" Text="Add Subject" OnClick="addSubjectButton_Click" />
		</fieldset>
		<br />
		<fieldset>
			<legend class="dataHeader">Notes (MARC 5XX)</legend>
			<asp:GridView ID="notesList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
				Width="1000px" CssClass="boxTable" OnRowCancelingEdit="notesList_RowCancelingEdit" OnRowEditing="notesList_RowEditing"
				OnRowUpdating="notesList_RowUpdating" OnRowCommand="notesList_RowCommand" DataKeyNames="TitleNoteID,NoteTypeID,NoteText">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
					<asp:TemplateField HeaderText="Type" ItemStyle-Width="300px">
						<ItemTemplate>
							<%# Eval( "NoteTypeNameExtended" ) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList ID="ddlNoteType" runat="server" DataTextField="NoteTypeNameExtended" DataValueField="NoteTypeID"
								DataSource="<%# GetNoteTypes() %>" SelectedIndex="<%# GetNoteTypeIndex( Container.DataItem ) %>" />
						</EditItemTemplate>
					</asp:TemplateField>
                    <asp:TemplateField HeaderText="Note" ItemStyle-Width="225px">
                        <ItemTemplate>
                            <%# Eval("NoteText") %>
                        </ItemTemplate>
                        <EditItemTemplate>
						    <asp:TextBox ID="txtNoteText" TextMode="MultiLine" Rows="3" runat="server" Text='<%# Eval( "NoteText") %>' Width="225" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Seq #" ItemStyle-Width="50px">
                        <ItemTemplate>
                            <%# Eval("NoteSequence") %>
                        </ItemTemplate>
                        <EditItemTemplate>
						    <asp:TextBox ID="txtNoteSequence" runat="server" Text='<%# Eval( "NoteSequence") %>' Width="50"/>
                        </EditItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<ItemTemplate>
							<asp:LinkButton ID="editTitleNoteButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateTitleNoteButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelTitleNoteButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<br />
			<asp:Button ID="addTitleNoteButton" runat="server" Text="Add Note" OnClick="addTitleNoteButton_Click" />
		</fieldset>
		<br />
		<fieldset>
			<legend class="dataHeader">Title Identifiers (various MARC fields)</legend>
			<asp:GridView ID="identifiersList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
				Width="800px" CssClass="boxTable" OnRowCancelingEdit="identifiersList_RowCancelingEdit" OnRowEditing="identifiersList_RowEditing"
				OnRowUpdating="identifiersList_RowUpdating" OnRowCommand="identifiersList_RowCommand" DataKeyNames="TitleIdentifierID, IdentifierID, IdentifierValue">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
					<asp:TemplateField HeaderText="Identifier" ItemStyle-Width="400px">
						<ItemTemplate>
							<%# Eval( "IdentifierName" ) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList ID="ddlIdentifierName" runat="server" DataTextField="IdentifierName" DataValueField="IdentifierID" 
							    DataSource="<%# GetIdentifiers() %>" SelectedIndex="<%# GetIdentifierIndex( Container.DataItem ) %>" Width="400px" />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Value" ItemStyle-Width="220px">
						<ItemTemplate>
							<%# Eval( "IdentifierValue" ) %>
						</ItemTemplate>
						<EditItemTemplate>
						    <asp:TextBox ID="txtIdentifierValue" runat="server" Text='<%# Eval( "IdentifierValue") %>' />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<ItemTemplate>
							<asp:LinkButton ID="editTitleIdentifierButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateTitleIdentifierButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelTitleIdentifierButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<br />
			<asp:Button ID="addTitleIdentifierButton" runat="server" Text="Add Title Identifier" OnClick="addTitleIdentifierButton_Click" />
		</fieldset>
		<br />
		<fieldset>
			<legend class="dataHeader">Languages (MARC 041)</legend>
			<asp:GridView ID="languagesList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB"
				RowStyle-BackColor="white" Width="400px" CssClass="boxTable" OnRowCancelingEdit="languagesList_RowCancelingEdit" OnRowEditing="languagesList_RowEditing"
				OnRowUpdating="languagesList_RowUpdating" OnRowCommand="languagesList_RowCommand" DataKeyNames="TitleLanguageID,LanguageCode">
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
            <legend class="dataHeader">Title Variants (MARC 210, 242, 246)</legend>
            <asp:GridView ID="variantsList" runat="server" AutoGenerateColumns="false" CellPadding="5" GridLines="None"
            AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white" Width="800px" CssClass="boxTable" 
                OnRowCancelingEdit="variantsList_RowCancelingEdit" OnRowEditing="variantsList_RowEditing"
				OnRowUpdating="variantsList_RowUpdating" OnRowCommand="variantsList_RowCommand" DataKeyNames="TitleVariantID">
                <Columns>
                    <asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
                    <asp:TemplateField HeaderText="Type" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <%# Eval("TitleVariantTypeName") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlVariantTypeName" runat="server" DataTextField="TitleVariantTypeName" DataValueField="TitleVariantTypeID" 
                                DataSource="<%# GetVariants() %>" SelectedIndex="<%# GetVariantIndex( Container.DataItem ) %>" Width="100px" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title (2XXa)" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <%# Eval("Title") %>
                        </ItemTemplate>
						<EditItemTemplate>
						    <asp:TextBox ID="txtVariantTitle" runat="server" Text='<%# Eval( "Title") %>' />
						</EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title Remainder (2XXb)" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <%# Eval("TitleRemainder") %>
                        </ItemTemplate>
						<EditItemTemplate>
						    <asp:TextBox ID="txtVariantTitleRemainder" runat="server" Text='<%# Eval( "TitleRemainder") %>' />
						</EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Part Number (2XXn)" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <%# Eval("PartNumber") %>
                        </ItemTemplate>
						<EditItemTemplate>
						    <asp:TextBox ID="txtVariantPartNumber" runat="server" Text='<%# Eval( "PartNumber") %>' />
						</EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Part Name (2XXp)" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <%# Eval("PartName") %>
                        </ItemTemplate>
						<EditItemTemplate>
						    <asp:TextBox ID="txtVariantPartName" runat="server" Text='<%# Eval( "PartName") %>' />
						</EditItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<ItemTemplate>
							<asp:LinkButton ID="editVariantButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateVariantButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelVariantButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
                </Columns>
                <HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
            </asp:GridView>
			<br />
			<asp:Button ID="addTitleVariantButton" runat="server" Text="Add Title Variant" OnClick="addTitleVariantButton_Click" />
        </fieldset>
		<br />
		<fieldset>
			<legend class="dataHeader">Title Associations (MARC 440, 490, 78X, 830, and 773)</legend>
			<asp:GridView ID="associationsList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white" Width="800px" CssClass="boxTable" 
				OnRowCommand="associationsList_RowCommand" DataKeyNames="TitleAssociationID">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
					<asp:TemplateField HeaderText="Type" ItemStyle-Width="200px">
						<ItemTemplate>
							<%# Eval( "TitleAssociationName" ) %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Title" ItemStyle-Width="400px">
						<ItemTemplate>
							<%# Eval( "Title" ) %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Title ID" ItemStyle-Width="100px">
					    <ItemTemplate>
					        <%# Eval("AssociatedTitleID") %>
					    </ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<ItemTemplate>
						    <span style="text-decoration:underline;color:#a54219;" onclick="document.getElementById('titleAssociationEditLayer').style.display='block';document.getElementById('titleAssociationEditFrame').src='TitleAssociationEdit.aspx?id=<%# Eval("TitleAssociationID") %>&tid=<%# Eval("TitleID") %>';">View/Edit Details</span>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<br />
			<input type="button" onclick="document.getElementById('titleAssociationEditLayer').style.display='block';document.getElementById('titleAssociationEditFrame').src='TitleAssociationEdit.aspx?id=0&tid={0}&type=new';" id="btnTitleAssociationAdd" runat="server" value="Add Title Association" />
		</fieldset>
		<br />
		<fieldset>
			<legend class="dataHeader">Collections</legend>
			<asp:GridView ID="collectionsList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB"
				RowStyle-BackColor="white" Width="400px" CssClass="boxTable" OnRowCancelingEdit="collectionsList_RowCancelingEdit" OnRowEditing="collectionsList_RowEditing"
				OnRowUpdating="collectionsList_RowUpdating" OnRowCommand="collectionsList_RowCommand" DataKeyNames="TitleCollectionID,CollectionID">
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
		Item status filter: <asp:RadioButton ID="showAllRadioButton" runat="server" GroupName="filterItemGroup" Checked="false" AutoPostBack="true" Text="Show all" OnCheckedChanged="itemFilter_CheckedChanged" />
		<asp:RadioButton ID="showPubRadioButton" runat="server" GroupName="filterItemGroup" AutoPostBack="true" Checked="true" Text="Show only published" OnCheckedChanged="itemFilter_CheckedChanged" />
		<fieldset>
            <a name="titleitem"></a>
			<legend class="dataHeader">Title Items</legend>
			<asp:GridView ID="itemsList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" AllowSorting="true" AlternatingRowStyle-BackColor="#F7FAFB"
				RowStyle-BackColor="white" Width="800px" CssClass="boxTable" OnRowCancelingEdit="itemsList_RowCancelingEdit" OnRowEditing="itemsList_RowEditing" OnRowUpdating="itemsList_RowUpdating"
				OnRowCommand="itemsList_RowCommand" OnSorting="itemsList_Sorting" OnRowDataBound="itemsList_RowDataBound" DataKeyNames="ItemID">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="FlickrImage" runat="server" ImageUrl="images/flickr_sml.png" AlternateText="Item in Flickr" ToolTip="Item in Flickr" />
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:BoundField DataField="ItemID" HeaderText="Item ID" SortExpression="ItemID" ItemStyle-Width="80px" ReadOnly="true" />
                    <asp:TemplateField HeaderText="MARC" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <a id="hypItemMarc" href="#titleitem" onclick="javascript:window.open('TitleItemMarc.aspx?type=i&id=<%# Eval("ItemID") %>', '', 'width=600,height=600,location=0,status=0,scrollbars=1');">View</a>
                        </ItemTemplate>                    
                    </asp:TemplateField>
					<asp:HyperLinkField HeaderText="Barcode" DataNavigateUrlFields="ItemID" DataNavigateUrlFormatString="/ItemEdit.aspx?id={0}"
						DataTextField="BarCode" NavigateUrl="/ItemEdit.aspx" SortExpression="BarCode" />
					<asp:TemplateField HeaderText="Sequence" ItemStyle-Width="80px" SortExpression="ItemSequence">
						<ItemTemplate>
							<%# Eval( "ItemSequence" ) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="itemSequenceTextBox" runat="server" Width="80px" Text='<%# Eval( "ItemSequence" ) %>' />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:BoundField DataField="Volume" HeaderText="Volume" SortExpression="Volume" ReadOnly="true" />
					<asp:TemplateField HeaderText="Is Primary" ItemStyle-Width="70px">
						<ItemTemplate>
						    <asp:CheckBox ID="isPrimaryCheckBox" Enabled=false Checked='<%#(Convert.ToInt32(Eval("PrimaryTitleID")) == Convert.ToInt32(Eval("TitleID")))%>' runat="server" />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<ItemTemplate>
							<asp:LinkButton ID="editItemButton" runat="server" CommandName="Edit" Text="Edit Sequence"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateItemButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelItemButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<input type="button" onclick="overlay();document.getElementById('srchTitleID').focus();" id="btnAddItem" value="Add Item" />
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
	    <div style="top:1600px">
	        <table cellpadding="3" class="SearchText">
	            <tr>
	                <td colspan="5" align="left">
	                    <b>Search for a title containing the items you would like to add.</b>
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
	                                <b>Click a title to view and select individual items.</b>
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
	                                <b>Check the box next to the items you want to add to the title.</b><br />
	                                <asp:CheckBox runat="server" ID="makePrimary" Checked="true" />
	                            </td>
	                        </tr>
	                        <tr class="SearchResultsHeader" align="left">
	                            <th scope="col">Item&nbsp;ID</th>
	                            <th scope="col">Barcode</th>
	                            <th scope="col">Volume</th>
	                            <th>&nbsp;</th>
	                        </tr>
	                      </tbody>
	                    </table>
        	        </td>
	            </tr>
	        </table>
	        <a id="hypDone" href="#" onclick="addItemsToTitle();">Done</a>&nbsp;&nbsp;
	        <a id="hypCancel" href="#" onclick="selectAuthor('');">Cancel</a>
	        <input type="hidden" id="selectedItem" value="" runat="server" />
	        <input type="hidden" id="associationsUpdated" value="" runat="server" />
	    </div>	
	</div>
	<div id="titleAssociationEditLayer" style="width:700px;height:515px;border:1px solid #000000;background-color:#FFFFFF;padding:0px;position:absolute;top:1035px;left:50px;z-index:1000;display:none">
	    <iframe id="titleAssociationEditFrame" src="" style="width:100%;height:100%;"></iframe>
	</div>
	<script language="javascript">
	    function addItemsToTitle() {
	        var checkboxes; var x = 0;

	        checkboxes = document.getElementsByName('makePrimaryCheckbox');
	        for (x = 0; x < checkboxes.length; x++) {
	            if (checkboxes[x].checked) selectItem(checkboxes[x].value);
	        }

	        overlay(); __doPostBack('', '');
	    }
    </script>
</asp:Content>
