<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="AuthorEdit.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.AuthorEdit" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<%@ Register Src="/Controls/EditHistoryLink.ascx" TagName="EditHistoryControl" TagPrefix="mobot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<a href="/AuthorSearch.aspx">&lt; Find a Different Author</a><br />
	<br />
	<span class="pageHeader">Author</span><hr />
	<br />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<asp:Literal id="litMessage" runat="server"></asp:Literal>
	<br />
	<div class="box" style="padding: 5px;margin-right:5px">
		<table cellpadding="4" width="900px">
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Author ID:
				</td>
				<td style="white-space: nowrap" colspan="4" valign="middle" width="100%">
					<asp:Label ID="lblID" runat="server" ForeColor="blue" />&nbsp;&nbsp;&nbsp;
					<asp:CheckBox ID="chkIsActive" runat="server" />Is Active
				</td>
                <td align="left" nowrap>
                    <a id="hypTitles" runat="server" href="#" onclick="javascript:window.open('AuthorTitlesList.aspx?id={0}', '', 'width=800,height=600,location=0,status=0,scrollbars=1');">View Titles</a>&nbsp;&nbsp;
                    <a id="hypSegments" runat="server" href="#" onclick="javascript:window.open('AuthorSegmentsList.aspx?id={0}', '', 'width=800,height=600,location=0,status=0,scrollbars=1');">View Segments</a>
                </td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Replaced By (Author ID):
				</td>
				<td>
				    <asp:TextBox ID="txtReplacedBy" runat="server" Width="200px"></asp:TextBox>
				</td>
			</tr>
        </table>
		<fieldset>
			<legend class="dataHeader">Author Names</legend>
			<asp:GridView ID="namesList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
				Width="800px" CssClass="boxTable" OnRowCancelingEdit="namesList_RowCancelingEdit" OnRowEditing="namesList_RowEditing"
				OnRowUpdating="namesList_RowUpdating" OnRowCommand="namesList_RowCommand" DataKeyNames="AuthorNameID,AuthorID,FullName,LastName,FirstName,FullerForm">
				<Columns>
					<asp:ButtonField ButtonType="Link" Text="Remove" CommandName="RemoveButton" ItemStyle-Width="50px" />
					<asp:TemplateField HeaderText="Full Name (last, first)" HeaderStyle-HorizontalAlign="Left">
						<ItemTemplate>
							<%# Eval( "FullName" ) %>
						</ItemTemplate>
						<EditItemTemplate>
						    <asp:TextBox ID="txtFullName" runat="server" Text='<%# Eval( "FullName") %>' />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Last Name" HeaderStyle-HorizontalAlign="Left">
						<ItemTemplate>
							<%# Eval( "LastName" ) %>
						</ItemTemplate>
						<EditItemTemplate>
						    <asp:TextBox ID="txtLastName" runat="server" Text='<%# Eval( "LastName") %>' />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="First Name" HeaderStyle-HorizontalAlign="Left">
						<ItemTemplate>
							<%# Eval( "FirstName" ) %>
						</ItemTemplate>
						<EditItemTemplate>
						    <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Eval( "FirstName") %>' />
						</EditItemTemplate>
					</asp:TemplateField>
                    <asp:TemplateField HeaderText="Fuller Form" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <%# Eval( "FullerForm") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFullerForm" runat="server" Text='<%# Eval("FullerForm") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField HeaderText="Preferred" ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="Left">
						<ItemTemplate>
						    <asp:CheckBox ID="chkIsPreferred" Enabled="false" Checked='<%# ((short)Eval("IsPreferredName") == 1 ? true : false) %>' runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
						    <asp:CheckBox ID="chkIsPreferredEdit" Checked='<%# ((short)Eval("IsPreferredName") == 1 ? true : false) %>' runat="server" />
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="130px">
						<ItemTemplate>
							<asp:LinkButton ID="editAuthorNameButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateAuthorNameButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelAuthorNameButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<br />
			<asp:Button ID="addAuthorNameButton" runat="server" Text="Add Author Name" OnClick="addAuthorNameButton_Click" />
		</fieldset>
		<br />
		<table cellpadding="4" width="900px">
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					Type:
				</td>
				<td colspan="4" style="width: 100%">
					<asp:DropDownList ID="ddlAuthorType" ClientIDMode="Static" runat="server"></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Start/Birth Date:
				</td>
				<td>
					<asp:TextBox ID="txtStartDate" ClientIDMode="Static" runat="server" MaxLength="25" Width="200px"></asp:TextBox>
				</td>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					End/Death Date:
				</td>
				<td>
					<asp:TextBox ID="txtEndDate" ClientIDMode="Static" runat="server" MaxLength="25" Width="200px"></asp:TextBox>
				</td>
				<td width="100%">
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Numeration:
				</td>
				<td>
					<asp:TextBox ID="txtNumeration" ClientIDMode="Static" runat="server" MaxLength="300" Width="300px"></asp:TextBox>
				</td>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Title:
				</td>
				<td>
					<asp:TextBox ID="txtTitle" ClientIDMode="Static" runat="server" MaxLength="200" Width="300px"></asp:TextBox>
				</td>
				<td width="100%">
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Unit:
				</td>
				<td>
					<asp:TextBox ID="txtUnit" ClientIDMode="Static" runat="server" MaxLength="300" Width="300px"></asp:TextBox>
				</td>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Location:
				</td>
				<td>
					<asp:TextBox ID="txtLocation" ClientIDMode="Static" runat="server" MaxLength="200" Width="300px"></asp:TextBox>
				</td>
				<td width="100%">
				</td>
			</tr>
        </table>
        <br />
		<fieldset>
			<legend class="dataHeader">Author Identifiers</legend>
			<asp:GridView ID="identifiersList" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" 
			    AlternatingRowStyle-BackColor="#F7FAFB" RowStyle-BackColor="white"
				Width="800px" CssClass="boxTable" OnRowCancelingEdit="identifiersList_RowCancelingEdit" OnRowEditing="identifiersList_RowEditing"
				OnRowUpdating="identifiersList_RowUpdating" OnRowCommand="identifiersList_RowCommand" DataKeyNames="AuthorIdentifierID, IdentifierID, IdentifierValue">
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
							<asp:LinkButton ID="editAuthorIdentifierButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="updateAuthorIdentifierButton" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
							<asp:LinkButton ID="cancelAuthorIdentifierButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			</asp:GridView>
			<br />
			<asp:Button ID="addAuthorIdentifierButton" runat="server" Text="Add Author Identifier" OnClick="addAuthorIdentifierButton_Click" />
		</fieldset>
		<br />
		<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
		<div style="float:right;"><mobot:EditHistoryControl runat="server" id="editHistoryControl" /></div>
	</div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(window).load(function () { toggleFieldsForAuthorType() } );
        });

        $("#ddlAuthorType").change(function () {
            toggleFieldsForAuthorType();
        });

        function toggleFieldsForAuthorType() {
            switch ($("#ddlAuthorType").val()) {
                case "1":
                    this.enableField("#txtNumeration");
                    this.enableField("#txtTitle");
                    this.disableField("#txtUnit");
                    this.disableField("#txtLocation");
                    break;
                case "2":
                    this.disableField("#txtNumeration");
                    this.disableField("#txtTitle");
                    this.enableField("#txtUnit");
                    this.enableField("#txtLocation");
                    break;
                case "3":
                    this.disableField("#txtNumeration");
                    this.disableField("#txtTitle");
                    this.disableField("#txtUnit");
                    this.enableField("#txtLocation");
                    break;
            }
        }

        function enableField(fieldName) {
            $(fieldName).removeAttr("disabled");
            $(fieldName).css("backgroundColor", "");
        }

        function disableField(fieldName) {
            $(fieldName).attr("disabled", "disabled");
            $(fieldName).val("");
            $(fieldName).css("backgroundColor", "#DDDDDD");
        }
    </script>
</asp:Content>
