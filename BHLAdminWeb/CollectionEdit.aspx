<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="CollectionEdit.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.CollectionEdit"
	ValidateRequest="false" %>
	
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<%@ Register TagPrefix="FCKeditorV2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Collections</span><hr />
	<br />	
	<div>
		Collections:
		<asp:DropDownList ID="ddlCollections" runat="server" OnSelectedIndexChanged="ddlCollections_SelectedIndexChanged" AutoPostBack="True" />
		<asp:Button ID="contentsButton" runat="server" Text="Show Contents" Enabled="false" OnClick="contentsButton_Click" />
        <asp:Button ID="bulkAddButton" runat="server" Text="Bulk Add" Enabled="false" OnClick="bulkAddButton_Click" OnClientClick="return ConfirmBulkAdd();" />
        <asp:Button ID="refreshContentsButton" runat="server" Text="Refresh Contents" Enabled="false" OnClick="refreshContentsButton_Click" OnClientClick="return ConfirmRefresh();" />
        <asp:Button ID="clearContentsButton" runat="server" Text="Clear Contents" Enabled="false" OnClick="clearContentsButton_Click" OnClientClick="return ConfirmClear();" />
	</div>
	<br />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<asp:Literal id="litMessage" runat="server"></asp:Literal>
	<br />
	<asp:Panel ID="contentPanel" runat="server" Height="200px" ScrollBars="Auto" Visible="false" CssClass="box" style="margin-right:5px">
		<asp:GridView ID="gvwTitles" runat="server" AutoGenerateColumns="False" CellSpacing="0" CellPadding="5" GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB"
			RowStyle-BackColor="white" Width="100%">
			<Columns>
				<asp:BoundField DataField="TitleID" HeaderText="ID" ItemStyle-VerticalAlign="top" />
				<asp:HyperLinkField HeaderText="Title" DataNavigateUrlFields="TitleID" DataNavigateUrlFormatString="/TitleEdit.aspx?id={0}"
					DataTextField="ShortTitle" NavigateUrl="/TitleEdit.aspx" />
			</Columns>
			<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
		</asp:GridView>
		<br />
		<asp:GridView ID="gvwItems" runat="server" AutoGenerateColumns="False" CellSpacing="0" CellPadding="5" GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB"
			RowStyle-BackColor="white" Width="100%">
			<Columns>
				<asp:BoundField DataField="ItemID" HeaderText="ID" ItemStyle-VerticalAlign="top" />
				<asp:HyperLinkField HeaderText="Item" DataNavigateUrlFields="ItemID" DataNavigateUrlFormatString="/ItemEdit.aspx?id={0}"
					DataTextField="ShortTitle" NavigateUrl="/ItemEdit.aspx" />
				<asp:BoundField DataField="Volume" HeaderText="Volume" ItemStyle-Wrap="false" ItemStyle-VerticalAlign="Top" />
			</Columns>
			<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
		</asp:GridView>
	</asp:Panel>
	<div class="box" style="padding: 5px;margin-right:5px; width:970px">
		<table cellpadding="4" style="width:100%">
			<tr>
				<td style="white-space: nowrap; width:100px" align="right">
					ID:
				</td>
				<td>
				    <asp:Label ID="lblID" runat="server" ForeColor="Blue" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkActive" runat="server" /> Is Active&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkFeatured" ClientIDMode="Static" runat="server" /> Featured
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right">
					Name:
				</td>
				<td>
				    <asp:TextBox ID="txtCollectionName" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top">
					Description:
				</td>
				<td valign="top">
					<asp:TextBox ID="txtCollectionDescription" runat="server" Width="800px" 
                        MaxLength="4000" Wrap="true" Rows="4" TextMode="MultiLine" 
                        onblur="checkDescLength(this, 4000);" onkeyup="checkDescLength(this, 4000);"></asp:TextBox>
				</td>
			</tr>
            <tr>
				<td style="white-space: nowrap" align="right" valign="top">
					Image:
				</td>
                <td valign="top">
                    <asp:Label ID="lblImageUrl" runat="server"></asp:Label><br />
                    <asp:FileUpload ID="imageUpload" runat="server" Width="400px" />
                </td>
            </tr>
            <tr>
                <td style="white-space: nowrap" align="right" valign="top">
                    URL:<br />&nbsp;
                </td>
                <td valign="top">
                    <asp:TextBox ID="txtCollectionURL" ClientIDMode="Static" runat="server" Width="400px" MaxLength="50"></asp:TextBox><br />
                    This value is used to create a stable URL of the form http://www.biodiversitylibrary.org/collection/&lt;URL&gt;<br />
                    <b><i>Once a URL value has been assigned, it CANNOT be changed!</i></b>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td style="padding-bottom:0px;"></td>
                <td style="padding-bottom:0px;"><b><i>The following three values can only be set when a new collection is created.  After that, they CANNOT be changed.</i></b></td>
            </tr>
			<tr>
			    <td style="white-space:nowrap; padding-top:0px" align="right">
			        Can Contain:
			    </td>
			    <td style="padding-top:0px">
			        <asp:RadioButtonList ClientIDMode="Static" runat="server" ID="rdoContents" TextAlign="Right" RepeatDirection="Horizontal">
			            <asp:ListItem Text="Titles" Value="T" Selected="True"></asp:ListItem>
			            <asp:ListItem Text="Items" Value="I"></asp:ListItem>
			        </asp:RadioButtonList>
			    </td>			
			</tr>
            <tr>
                <td style="white-space:nowrap" align="right">
                    Auto-Add If Contributed By:
                </td>
                <td>
                    <asp:DropDownList ID="ddlInstitution" ClientIDMode="Static" runat="server" DataTextField="InstitutionName" DataValueField="InstitutionCode"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="white-space:nowrap" align="right" valign="top">
                    Auto-Add If Written In:
                </td>
                <td valign="top">
                    <asp:DropDownList ID="ddlLanguage" ClientIDMode="Static" runat="server" DataTextField="LanguageName" DataValueField="LanguageCode"></asp:DropDownList><br />
                    The "Auto-Add" values are used to automatically build the collection.  Matching titles/items that 
                    exist when the collection is created will be added to the collection.  As new titles/items are added 
                    to BHL they will be added to the collection if they match the selected criteria.
                </td>
            </tr>
            <tr>
                <td style="white-space:nowrap" align="right">
                    Display Collection At: 
                </td>
                <td>
                    <asp:CheckBox ID="chkBHL" ClientIDMode="Static" runat="server" Text="http://www.biodiversitylibrary.org" TextAlign="Right" onclick="validateBHLDisplay(this);" Checked="true" />&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkITunes" ClientIDMode="Static" runat="server" Text="iTunesU" TextAlign="Right" onclick="validateITunesUDisplay(this);" />
                </td>
            </tr>
            <tr>
                <td style="white-space:nowrap" align="right" valign="top">
                    iTunesU Artwork:
                </td>
                <td>
                    <asp:Label ID="lbliTunesImageUrl" runat="server"></asp:Label><br />
                    <asp:FileUpload ID="iTunesImageUpload" ClientIDMode="Static" runat="server" Width="400px" />

                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top">
                    Landing Page Content:
				</td>
				<td>
                    <asp:HyperLink ID="lnkLandingPage" runat="server" Text="View Collection Landing Page" Target="_blank" Visible="false" />
					<FCKeditorV2:FCKeditor ID="txtHtml" runat="server" BasePath="/Controls/FCKeditor/" Height="500px" />
				</td>
			</tr>
		</table>
		<br />
		<br />
		<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
		<asp:Button ID="clearButton" runat="server" Text="Clear" OnClick="clearButton_Click" />
		<asp:Button ID="saveAsNewButton" runat="server" Text="Save As New" OnClick="saveAsNewButton_Click" />
	</div>

    <script type="text/javascript" language="javascript">
        function ConfirmBulkAdd() {
            if (confirm("Are you sure that you want to navigate away from this page?  Unsaved changes will be lost!"))
                return true;
            else
                return false;
        }

        function ConfirmClear() {
            if (confirm("Are you sure that you want to remove ALL of the books from this collection?  THIS ACTION CANNOT BE UNDONE!"))
                return true;
            else
                return false;
        }

        function ConfirmRefresh() {
            if (confirm("Are you sure that you want to add all books from the selected institution and language to the collection?  THE ACTION CANNOT BE UNDONE!"))
                return true;
            else
                return false;
        }

        function checkDescLength(textControl, maxLength) {
            var textValue = textControl.value;
            if (textValue.length > maxLength) textControl.value = textValue.substring(0, maxLength);
        }

        var origInstDDLDisabled;
        var origLangDDLDisabled;
        function validateITunesUDisplay(chkBox) {
            // Get current state of the dropdowns
            if (origInstDDLDisabled == undefined) {
                origInstDDLDisabled = document.getElementById("ddlInstitution").disabled;
                origLangDDLDisabled = document.getElementById("ddlLanguage").disabled;
            }
            if (chkBox.checked) {
                // Checkbox checked, so confirm the action
                if (confirm("Publishing this collection to iTunesU will PERMANENTLY disable Auto-Add functionality for this collection.  Are you sure you want to do this?")) {
                    document.getElementById("iTunesImageUpload").disabled = false;
                    document.getElementById("ddlInstitution").disabled = true;
                    document.getElementById("ddlLanguage").disabled = true;
                }
                else {
                    chkBox.checked = false;
                }
            }
            else {
                // Checkbox is un-checked, so reset state of dropdowns
                document.getElementById("iTunesImageUpload").disabled = true;
                document.getElementById("ddlInstitution").disabled = origInstDDLDisabled;
                document.getElementById("ddlLanguage").disabled = origLangDDLDisabled;
            }
        }

        var origURLDisabled;
        function validateBHLDisplay(chkBox) {
            // Get current state of URL textbox
            if (origURLDisabled == undefined) origURLDisabled = document.getElementById("txtCollectionURL").disabled;
            if (chkBox.checked) {
                // Checkbox is checked, so reset state of dropdowns
                document.getElementById("txtCollectionURL").disabled = origURLDisabled;
            }
            else {
                document.getElementById("txtCollectionURL").disabled = true;
            }
        }

    </script>
</asp:Content>
