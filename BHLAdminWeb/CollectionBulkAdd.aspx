<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CollectionBulkAdd.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.CollectionBulkAdd" ValidateRequest="false" %>

<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="CollectionEdit.aspx?id=<%=CollectionID %>">&lt; Return to Collection</a><br />
	<br />
	<span class="pageHeader">Bulk Add To Collection</span><hr />
    <p>NOTE: To complete this form, you need a list of the <b><%= CollectionType %></b> IDs to be added to the collection.</p>
    <p>Type or paste the list of IDs into the Input text box.</p>
    <span>Input</span><span style="position:relative; left:250px">Output</span><br />
    <span><asp:TextBox ID="txtIDs" runat="server" TextMode="MultiLine" Rows="10" Width="250"></asp:TextBox></span>
    <span style="position:relative; left:18px; vertical-align:top">
        <asp:TextBox ID="txtOutput" runat="server" TextMode="MultiLine" Wrap="false" Rows="9" Width="400" ReadOnly="true"></asp:TextBox>
    </span><br />
    Specify the delimiter that separates the IDs: 
    <select id="selDelimiter" runat="server">
        <option value=",">Comma</option>
        <option value=";">Semicolon</option>
        <option value="s">Space</option>
        <option value="t">Tab</option>
        <option value="lb">Line Break</option>
    </select><br /><br />
    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="addButton_Click" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="cancelButton_Click" />
</asp:Content>
