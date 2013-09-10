<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TitleAssociationEdit.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.TitleAssociationEdit" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>BHL Admin</title>
<link rel="stylesheet" type="text/css" href="styles/adminstyle.css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidTitleID" Value="0" runat="server" />
    <asp:HiddenField ID="hidTitleAssociationID" Value="0" runat="server" />
    <div>
	<span class="pageHeader">Title Association</span>
	<br />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<br />
	<div class="box" style="padding: 5px;">
		<table cellpadding="4" width="100%">
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Type:
				</td>
				<td>
					<asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList>
				</td>
				<td align="right">
					<asp:CheckBox ID="activeCheckBox" runat="server" Text="Active" Checked="true" />
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Title:
				</td>
				<td colspan="2">
					<asp:TextBox ID="titleTextBox" runat="server" MaxLength="500" Width="100%" TextMode="MultiLine" Height="35px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Section:
				</td>
				<td colspan="2">
					<asp:TextBox ID="sectionTextBox" runat="server" MaxLength="500" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Volume:
				</td>
				<td colspan="2">
					<asp:TextBox ID="volumeTextBox" runat="server" MaxLength="500" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Heading:
				</td>
				<td colspan="2">
					<asp:TextBox ID="headingTextBox" runat="server" MaxLength="500" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Publication:
				</td>
				<td colspan="2">
					<asp:TextBox ID="publicationTextBox" runat="server" MaxLength="500" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Relationship:
				</td>
				<td colspan="2">
					<asp:TextBox ID="relationshipTextBox" runat="server" MaxLength="500" Width="100%"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader" width="120px">
					Associated BHL Title:
				</td>
				<td valign="top" colspan="2">
				    <input type="hidden" id="hidAssociatedTitleID" runat="server" />
				    <span id="spnAssociatedTitle"><asp:Literal ID="litAssociatedTitle" runat="server" Text="Not associated"></asp:Literal></span>&nbsp;&nbsp;
				    <span style="font-size:10px;text-decoration:underline;color:#a54219;" onclick="overlay();document.getElementById('srchTitleID').focus();">Select Title</span>&nbsp;&nbsp;
				    <span style="font-size:10px;text-decoration:underline;color:#a54219;" onclick="clearAssociatedTitle();">Clear</span>
				</td>
			</tr>
	    </table>
		<br />
		<fieldset>
			<legend class="dataHeader">Title Identifiers</legend>
			<asp:GridView ID="identifiersList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
				Width="630px" CssClass="boxTable" OnRowCancelingEdit="identifiersList_RowCancelingEdit" OnRowEditing="identifiersList_RowEditing"
				OnRowUpdating="identifiersList_RowUpdating" OnRowCommand="identifiersList_RowCommand" DataKeyNames="TitleAssociation_TitleIdentifierID, TitleIdentifierID, IdentifierValue">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
					<asp:TemplateField HeaderText="Identifier" ItemStyle-Width="250px">
						<ItemTemplate>
							<%# Eval( "IdentifierName" ) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList ID="ddlIdentifierName" runat="server" DataTextField="IdentifierName" DataValueField="IdentifierID" 
							    DataSource="<%# GetIdentifiers() %>" SelectedIndex="<%# GetIdentifierIndex( Container.DataItem ) %>" Width="250px" />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Value" ItemStyle-Width="175px">
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
	    
	    <asp:Button ID="btnOK" runat="server" Text="OK" onclick="btnOK_Click" />
        <input type="button" onclick="parent.__doPostBack('', '');" id="btnCancel" value="Cancel" />
    </div>
    </div>
	<div id="overlay" class="overlay" style="background-image:url(/images/blank.gif);">
	    <div style="top:100px;left:60px; margin: 0px auto;">
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
    </form>
</body>
</html>

<script language="javascript">
function overlay() {
    el = document.getElementById("overlay");
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
</script>
