<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="True" Codebehind="NamePageEdit.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.NamePageEdit"
	Title="BHL Admin - Names (Taxa)" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <script language="javascript">
        function overlayNameSearch() {
            el = document.getElementById("overlayname");
            el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
        }

        function nameSearch(name) {
            if (name == "") {
                alert("Please specify a Name.");
                return;
            }
            document.getElementById('srchNameResultTable').style.display = 'none';
            document.getElementById('srchNameInProgress').style.display = 'block';
            executeServiceCall('services/nameservice.ashx?op=NameSearch&name=' + name, showNameList);
        }

        function showNameList(result) {
            var names = eval(result);

            // Clear rows already in table
            var tbody = document.getElementById("srchNameResultTable").getElementsByTagName("tbody")[0];
            var rows = document.getElementById("srchNameResultTable").getElementsByTagName("tr");
            for (var j = (rows.length - 1); j >= 2; j--) tbody.removeChild(rows[j]);

            // Build the table
            for (var i = 0; i < names.length; i++) {
                var tbody = document.getElementById("srchNameResultTable").getElementsByTagName("tbody")[0];
                var row = document.createElement("tr");
                row.setAttribute("align", "left");
                var td1 = document.createElement("td");
                var a = document.createElement("a");
                a.setAttribute("href", "#");
                a.onclick = new Function("selectName('" + names[i].NameID + "')");
                a.appendChild(document.createTextNode(names[i].NameString));
                td1.appendChild(a);
                var td2 = document.createElement("td");
                td2.appendChild(document.createTextNode(names[i].ResolvedNameString));
                row.appendChild(td1);
                row.appendChild(td2);
                tbody.appendChild(row);
            }

            // Display the table
            document.getElementById('srchNameInProgress').style.display = 'none';
            document.getElementById('srchNameResultTable').style.display = 'block';
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
	<br />
	<span class="pageHeader">Names (Taxa) On a Page</span><hr />
	<br />
	<table>
		<tr>
			<td>
				Page ID:
				<asp:TextBox ID="pageIdTextBox" runat="server"></asp:TextBox>
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
	<table width="100%" class="box">
		<tr>
			<td style="width: 550px;" valign="top">
				<table cellpadding="4">
					<tr>
						<td style="white-space: nowrap" align="right" class="dataHeader">
							Page ID:
						</td>
						<td>
							<asp:Label ID="pageIdLabel" runat="server" ForeColor="Blue"></asp:Label>
						</td>
					</tr>
					<tr>
						<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
							Title (Title ID):
						</td>
						<td>
							<asp:HyperLink ID="titleLink" runat="server"></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td style="white-space: nowrap" align="right" class="dataHeader">
							Volume:
						</td>
						<td>
							<asp:HyperLink ID="itemLink" runat="server"></asp:HyperLink>
						</td>
					</tr>
					<tr>
						<td style="white-space: nowrap" align="right" class="dataHeader">
							Description:
						</td>
						<td>
							<asp:Label ID="descriptionLabel" runat="server" ForeColor="Blue"></asp:Label>
						</td>
					</tr>
                </table>
                <table cellpadding="4">
					<tr>
						<td colspan="2">
							<asp:GridView ID="namePageList" runat="server" AutoGenerateColumns="False" CssClass="boxTable" AllowSorting="true"
								CellPadding="5" GridLines="None" RowStyle-BackColor="white" AlternatingRowStyle-BackColor="#F7FAFB" Width="550px" OnRowCancelingEdit="namePageList_RowCancelingEdit"
								OnRowEditing="namePageList_RowEditing" OnRowUpdating="namePageList_RowUpdating" OnRowCommand="namePageList_RowCommand" OnSorting="namePageList_Sorting"
								OnRowDataBound="namePageList_RowDataBound" DataKeyNames="namePageID,NameString,ResolvedNameString,NameBankID">
								<Columns>
									<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
									<asp:TemplateField HeaderText="Name Found" SortExpression="NameString" HeaderStyle-HorizontalAlign="Left">
										<ItemTemplate>
											<%# Eval("NameString") %>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="nameStringTextBox" runat="server" Text='<%# Eval("NameString") %>'></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resolved Name" SortExpression="ResolvedNameString" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%# Eval("ResolvedNameString") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="resolvedNameStringTextBox" runat="server" Text='<%# Eval("ResolvedNameString") %>'></asp:TextBox>
                                        </EditItemTemplate>                                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Namebank ID" SortExpression="NameBankID" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%# Eval("NamebankID") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="namebankIDTextBox" runat="server" Text='<%# Eval("NamebankID") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EOL ID" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%# Eval("EOLID") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="eolIDTextBox" runat="server" Text='<%# Eval("EOLID") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="First Occurrence" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="firstCheckBox" runat="server" Checked='<%# (short)Eval( "IsFirstOccurrence" ) == 1 %>' Enabled="false" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="firstCheckBox" runat="server" Checked='<%# (short)Eval( "IsFirstOccurrence" ) == 1 %>' Enabled="true" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
									<asp:TemplateField ItemStyle-Width="130px">
										<ItemTemplate>
											<asp:LinkButton ID="editnamePageButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:LinkButton ID="updatenamePageButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
											<asp:LinkButton ID="cancelnamePageButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
										</EditItemTemplate>
									</asp:TemplateField>
								</Columns>
								<HeaderStyle VerticalAlign="Bottom" HorizontalAlign="Left" CssClass="SearchResultsHeader" Wrap="true" />
							</asp:GridView>
							<br />
                			<input runat="server" type="button" onclick="overlayNameSearch();document.getElementById('srchName').focus();" id="btnFindName" value="Find Name" disabled />
							<asp:Button ID="addNamePageButton" runat="server" Text="Add New Name" OnClick="addNamePageButton_Click" Enabled="false" />
						</td>
					</tr>
				</table>
			</td>
			<td id="pageViewer" valign="top" style="border-style: solid; border-width: 1px; border-color: #cccccc;" runat="server">
                <img id="imgPage" width="400" height="600" runat="server" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" Enabled="false" />
			</td>
		</tr>
	</table>
    <div id="overlayname" class="overlay">
        <div style="top:150px; width:400px">
            <table cellpadding="3" class="SearchText">
	            <tr>
	                <td style="white-space: nowrap; text-align:left" colspan="3">Name:
	                <input id="srchName" type="text" class="SearchText" onkeydown="keyDownHandler(event, btnNameSearch);" />
	                <input id="btnNameSearch" type="button" onclick="nameSearch(document.getElementById('srchName').value);" value="Search" class="SearchText" /></td>
	            </tr>
	            <tr>
	                <td colspan="3">
                        <div style="height:250px; padding:0px; overflow:auto; position:inherit; width:inherit; margin:inherit; border-style:none;">
	                    <table id="srchNameResultTable" style="display:none" cellpadding="3" cellspacing="3" width="100%">
	                      <tbody>
	                        <tr>
	                            <td colspan="2" align="left">
	                                <b>Select the name you want to add to the page.</b>
	                            </td>
	                        </tr>
	                        <tr class="SearchResultsHeader" align="left">
	                            <th scope="col">Name</th>
	                            <th scope="col">Resolved&nbsp;Name</th>
	                        </tr>
	                      </tbody>
	                    </table>
                        <table id="srchNameInProgress" style="display:none" cellpadding="20" cellspacing="20" width="100%">
                         <tbody>
                          <tr>
                                <td align="center"><b>Searching...</b></td>
                          </tr>
                         </tbody>
                        </table>
                        </div>
        	        </td>
	            </tr>
            </table>
	        <a id="hypNameSearchCancel" href="#" onclick="clearName('');">Cancel</a>
	        <input type="hidden" id="selectedName" value="" runat="server" />
        </div>
    </div>
</asp:Content>
