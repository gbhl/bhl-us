<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="True" CodeBehind="PdfEdit.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.PdfEdit" 
    ValidateRequest="false" EnableEventValidation="false" %>
<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <a href="/">&lt; Return to Dashboard</a><br />
	<a href="/PdfWeeklyStats.aspx">&lt; Return to PDF Generation Weekly Stats</a><br />
	<br />
	<span class="pageHeader">Generated PDF</span><hr />
	<br />
	<table>
		<tr>
			<td>
				PDF ID:
				<asp:TextBox ID="pdfIdTextBox" runat="server"></asp:TextBox>
			</td>
			<td style="padding-left: 10px">
				<asp:Button ID="searchButton" runat="server" Text="Search" OnClick="searchButton_Click" />
			</td>
		</tr>
	</table>
	<br />
	<mobot:ErrorControl runat="server" id="errorControl"></mobot:ErrorControl>
	<br />
	<div class="box" style="padding: 5px;margin-right:5px">
		<table cellpadding="4" width="95%">
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					PDF ID:
				</td>
				<td width="100%">
					<asp:Label ID="pdfIdLabel" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Item ID:
				</td>
				<td>
					<asp:HyperLink ID="hypItemID" runat="server"></asp:HyperLink>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Email:
				</td>
				<td>
					<asp:Label ID="emailAddressLabel" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Share With Addresses:
				</td>
				<td>
					<asp:Label ID="shareWithLabel" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Include OCR:
				</td>
				<td>
					<asp:Label ID="includeOCRLabel" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Article Title:
				</td>
				<td>
					<asp:TextBox ID="articleTitleTextBox" runat="server" Width="400px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Article Authors:
				</td>
				<td>
					<asp:TextBox ID="articleAuthorsTextBox" runat="server" Width="400px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Article Subjects:
				</td>
				<td>
					<asp:TextBox ID="articleSubjectsTextBox" runat="server" Width="400px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					File Location:
				</td>
				<td>
					<asp:Label ID="fileLocationLabel" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					File Url:
				</td>
				<td>
					<asp:HyperLink ID="hypFileUrl" runat="server"></asp:HyperLink>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Request Date:
				</td>
				<td>
					<asp:Label ID="creationDateLabel" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					File Generation Date:
				</td>
				<td>
					<asp:Label ID="fileGenerationDateLabel" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Minutes To Generate:
				</td>
				<td>
					<asp:Label ID="timeToGenerateLabel" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					File Deletion Date:
				</td>
				<td>
					<asp:Label ID="fileDeletionDateLabel" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Number of Missing Images:
				</td>
				<td>
					<asp:Label ID="missingImagesLabel" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" class="dataHeader">
					Number of Missing OCR:
				</td>
				<td>
					<asp:Label ID="missingOCRLabel" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
					PDF Status:
				</td>
				<td>
					<asp:DropDownList ID="ddlPdfStatus" runat="server"></asp:DropDownList>
				</td>
			</tr>
			<tr>
			    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
			        Comment:
			    </td>
			    <td>
    			    <asp:TextBox ID="commentTextBox" runat="server" TextMode="MultiLine" Rows="4"  Width="600px"></asp:TextBox>
			    </td>
			</tr>
			<tr>
			    <td style="white-space: nowrap" align="right" valign="top" class="dataHeader">
			        Pages:
			    </td>
			    <td>
                    <div style="overflow:scroll;height:290px;width:600px; border-style:solid; border-color:Gray; border-width:1px; background-color:#E4E2D5">
			        <asp:GridView ID="gvPages" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" AlternatingRowStyle-BackColor="#F7FAFB"
				        Width="583px" RowStyle-BackColor="white" CssClass="boxTable" BorderStyle="None">
				        <Columns>
					        <asp:BoundField DataField="PageID" HeaderText="Page ID" />
					        <asp:BoundField DataField="FileNamePrefix" HeaderText="File Name" />
					        <asp:BoundField DataField="PageTypeName" HeaderText="Page Type" />
					        <asp:BoundField DataField="PagePrefix" HeaderText="Page Prefix" />
					        <asp:BoundField DataField="PageNumber" HeaderText="Page #" />
				        </Columns>
				        <HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
			        </asp:GridView>
			        </div>
			    </td>
			</tr>
		</table>
		<br />
		<asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
	</div>
</asp:Content>
