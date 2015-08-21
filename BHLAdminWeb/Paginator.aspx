<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="True" Codebehind="Paginator.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.Paginator"
	Title="BHL Admin - Pagination" %>

<%@ Register Src="/Controls/ErrorControl.ascx" TagName="ErrorControl" TagPrefix="mobot" %>
<%@ Register src="/Controls/OLBookReader/BookReader.ascx" tagname="BookReader" tagprefix="reader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <a href="/">&lt; Return to Dashboard</a><br />
	<a href="/TitleSearch.aspx?redir=p">&lt; Paginate a Different Title</a><br />
	<br />
	<span class="pageHeader">Pagination</span><hr />
	<mobot:ErrorControl runat="server" id="errorControl">
	</mobot:ErrorControl>
	<br />
	<table width="1000px" class="box">
		<tr>
			<td style="width: 500px;" valign="top">
				<table cellpadding="3px" cellspacing="0">
					<tr>
						<td>
							Title
						</td>
						<td style="width:350px;">
							<asp:Literal ID="litTitle" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td>
							Item
						</td>
						<td>
							<asp:DropDownList ID="itemDropDownList" runat="server" Width="100%" CssClass="TextBox" Style="background-color:White; height:20px" AutoPostBack="True" OnSelectedIndexChanged="itemDropDownList_SelectedIndexChanged">
							</asp:DropDownList>
						</td>
					</tr>
					<!--
					<tr>
						<td colspan="2" style="height:16px">
							<asp:RadioButton AutoPostBack="true" Checked="true" GroupName="pageViewTypeGroup" ID="detailViewRadio" runat="server" OnCheckedChanged="detailViewRadio_CheckedChanged" />
							<img src="/images/application_view_detail.png" align="texttop" />&nbsp;Details
							<asp:RadioButton AutoPostBack="true" Checked="false" GroupName="pageViewTypeGroup" ID="thumbnailViewRadio" runat="server" OnCheckedChanged="detailViewRadio_CheckedChanged" />
							<img src="/images/application_view_icons.png" align="texttop" id="thumbnailImg" runat="server" />&nbsp;<asp:Label ID="thumbnailLabel" runat="server" Text="Thumbnails"/>
						</td>
					</tr>
					-->
					<tr>
						<td colspan="2">
							<asp:Panel runat="server" Height="280px" ScrollBars="vertical" ID="gridPanel" CssClass="boxTable">
								<asp:GridView ID="detailGridView" runat="server" AutoGenerateColumns="False" HeaderStyle-Wrap="false" CellPadding="3" GridLines="None"
									RowStyle-BackColor="white" AlternatingRowStyle-BackColor="#F7FAFB" Width="470px" OnRowCommand="detailGridView_RowCommand" OnRowDataBound="detailGridView_RowDataBound"
									DataKeyNames="PageID">
									<Columns>
										<asp:TemplateField>
											<ItemTemplate>
												<asp:CheckBox runat="server" ID="pageCheckBox" />
                                                <asp:HiddenField ID="hidPageId" runat="server" />
											</ItemTemplate>
										</asp:TemplateField>
										<asp:ButtonField ButtonType="Image" CommandName="showPageLinkButton" ItemStyle-Width="20px" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="FlickrLinkButton" runat="server" AlternateText="View in Flickr" ToolTip="View in Flickr" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
										<asp:TemplateField HeaderText="Seq" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="SequenceOrder" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:Literal ID="Year" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Volume" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:Literal ID="Volume" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Piece" ItemStyle-HorizontalAlign="center" >
                                            <ItemTemplate>
                                                <asp:Literal ID="IssuePrefix" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:Literal ID="Issue" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Indicated Pages">
                                            <ItemTemplate>
                                                <asp:Literal ID="IndicatedPages" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Page Types">
                                            <ItemTemplate>
                                                <asp:Literal ID="PageTypes" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
									</Columns>
									<HeaderStyle HorizontalAlign="Left" CssClass="SearchResultsHeader" />
								</asp:GridView>
							</asp:Panel>
							<asp:HiddenField ID="hidScrollPosition" runat="server" />
						</td>
					</tr>
					<tr>
						<td colspan="2" align="center">
							<asp:Button ID="selectAllButton" runat="server" Font-Size="12px" Text="Select All" OnClick="selectAllButton_Click" />&nbsp;
							<asp:Button ID="selectNoneButton" runat="server" Font-Size="12px" Text="Select None" OnClick="selectNoneButton_Click" />&nbsp;
							<asp:Button ID="selectInverseButton" runat="server" Font-Size="12px" Text="Select Inverse" OnClick="selectInverseButton_Click" />&nbsp;
							<asp:Button ID="selectBetweenButton" runat="server" Font-Size="12px" Text="Select Between" OnClick="selectBetweenButton_Click" />
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<table cellpadding="2px">
								<tr>
									<td align="right" style="padding-right: 5px; white-space: nowrap">
										Indicated Pages
									</td>
									<td style="padding-right: 5px">
										<input type="button" Height="22px" id="showIndPageDialogButton" runat="server" value="..." disabled="disabled" onclick="document.getElementById('indicatedPagesDiv').style.display='inline';" />
									</td>
									<td style="padding-right: 5px">
										<asp:Button ID="clearIndicatedPageButton" runat="server" Font-Size="12px" Text="Clear All" Enabled="false" OnClick="clearIndicatedPageButton_Click" />
									</td>
								</tr>
								<tr>
									<td align="right" style="padding-right: 5px; white-space: nowrap">
										Page Type
									</td>
									<td style="padding-right: 5px">
										<asp:DropDownList ID="pageTypeCombo" CssClass="TextBox" Style="background-color:White; height:20px" runat="server">
										</asp:DropDownList>
									</td>
									<td style="padding-right: 5px">
										<asp:Button ID="assignPageTypeButton" runat="server" Font-Size="12px" Text="Assign" Enabled="false" OnClick="assignPageTypeButton_Click" />
										<asp:Button ID="replacePageTypeButton" runat="server" Font-Size="12px" Text="Replace" Enabled = "false" OnClick="replacePageTypeButton_Click" />
										<asp:Button ID="clearPageTypeButton" runat="server" Font-Size="12px" Text="Clear All" Enabled="false" OnClick="clearPageTypeButton_Click" />
									</td>
								</tr>
								<tr>
									<td align="right" style="padding-right: 5px">
										Year
									</td>
									<td>
										<asp:TextBox ID="yearTextBox" CssClass="TextBox" Style="background-color:White; height:13px" runat="server"></asp:TextBox>
									</td>
									<td>
										<asp:Button ID="assignYearButton" runat="server" Font-Size="12px" Text="Assign" Enabled="false" OnClick="assignYearButton_Click" />
										<asp:Button ID="assignYearAndVolumeButton" runat="server" Font-Size="12px" Text="Assign Year & Volume" Width="150px" Enabled="false" OnClick="assignYearAndVolumeButton_Click" />
									</td>
								</tr>
								<tr>
									<td align="right" style="padding-right: 5px">
										Volume
									</td>
									<td style="padding-right: 5px">
										<asp:TextBox ID="volumeTextBox" CssClass="TextBox" Style="background-color:White; height:13px" runat="server"></asp:TextBox>
									</td>
									<td>
										<asp:Button ID="assignVolumeButton" runat="server" Font-Size="12px" Text="Assign" Enabled="false" OnClick="assignVolumeButton_Click" />
									</td>
								</tr>
								<tr>
									<td align="right" style="padding-right: 5px">
										Piece
									</td>
									<td style="padding-right: 5px">
										<asp:DropDownList ID="issuePrefixCombo" CssClass="TextBox" Style="background-color:White; height:20px" runat="server">
										    <asp:ListItem Selected="True"></asp:ListItem>
											<asp:ListItem>Issue</asp:ListItem>
											<asp:ListItem>No.</asp:ListItem>
											<asp:ListItem>Part</asp:ListItem>
										</asp:DropDownList>
										<asp:TextBox ID="issueTextBox" runat="server" CssClass="TextBox" Style="background-color:White; height:13px" Width="90px"></asp:TextBox>
									</td>
									<td>
										<asp:Button ID="assignIssueButton" runat="server" Font-Size="12px" Text="Assign" Enabled="false" OnClick="assignIssueButton_Click" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<table cellpadding="2px">
								<tr>
									<td style="padding-right: 5px; white-space: nowrap">
										Pagination Status
									</td>
									<td style="padding-right: 5px">
										<asp:Label ID="paginationStatusLabel" runat="server" Font-Bold="true" />
									</td>
									<td style="padding-right: 5px">
										<asp:Button ID="lockButton" runat="server" Font-Size="12px" Text="Lock For Editing" OnClick="lockButton_Click" />
									</td>
									<td>
										<asp:Button ID="statusButton" runat="server" Font-Size="12px" Text="Complete" OnClick="statusButton_Click" />
									</td>
								</tr>
								<tr>
									<td colspan="4">
										<asp:Label ID="paginationDetailStatusLabel" runat="server" CssClass="liveData"></asp:Label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
                    <tr>
						<td colspan="2">
							<table cellpadding="2px">
								<tr>
									<td align="right" style="padding-right: 5px">
										Flickr Upload
									</td>
									<td style="padding-right: 5px">
										<asp:DropDownList ID="RotateImage" CssClass="TextBox" Style="background-color:White; height:20px" runat="server">
										    <asp:ListItem Selected="True" Text="No Rotation" Value="0" />
											<asp:ListItem Text="90deg Clockwise" Value="90"/>
											<asp:ListItem Text="180deg Clockwise" Value="180" />
											<asp:ListItem Text="270deg Clockwise" Value="270" />
										</asp:DropDownList>
									</td>
									<td>
										<asp:Button ID="FlickrUpload" runat="server" Font-Size="12px" Text="Upload" Enabled="true" OnClick="FlickrUpload_Click" />
                                        (must use BHL Flickr login)
                                        <asp:HiddenField ID="hidAuthToken" runat="server" />
                                        <asp:HiddenField ID="hidAuthTokenSecret" runat="server" />
                                        <asp:HiddenField ID="hidPageIds" runat="server" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
			<td id="pageViewer" valign="top" style="border-style: solid; border-width: 1px; border-color: #cccccc;" runat="server">
		        <div id="viewer" style="position: absolute; left: 510px; right: 7px; top: 100px; bottom: 5px;">
                    <reader:BookReader ID="BookReader1" runat="server" />
                </div>
			</td>
		</tr>
	</table>
	<div id="indicatedPagesDiv" style="position: absolute; bottom: 150px; left: 50px; width: 300px; display: none;">
		<table cellpadding="0px">
			<tr>
				<td class="box" style="background-color: White">
					<table width="100%" cellspacing="0px" cellpadding="3px">
						<tr>
							<td class="boxHeader" align="center" colspan="2">
								Indicated Pages
							</td>
						</tr>
						<tr>
							<td>
								<table cellpadding="3px" cellspacing="3px">
									<tr>
										<td colspan="2">
											<b>Prefix / Description:</b>
										</td>
									</tr>
									<tr>
										<td colspan="2" style="padding-right: 10px; width: 100%">
											<asp:TextBox ID="prefixTextBox" runat="server" Width="100%" Text="Page" MaxLength="40" />
										</td>
									</tr>
									<tr><td colspan="2"><b>Page Number:</b></td></tr>
									<tr>
										<td colspan="2">
											<table class="boxTable">
												<tr>
													<td style="white-space: nowrap">
														<asp:RadioButton ID="numStyleRadio" runat="server" Checked="true" GroupName="styleRadio" Text="Style" />
														&nbsp;
														<asp:DropDownList ID="styleDropDownList" runat="server">
															<asp:ListItem Value="1" Text="Integer" />
															<asp:ListItem Value="2" Text="Roman Numerals (iv)" />
															<asp:ListItem Value="3" Text="Roman Numerals (IV)" />
															<asp:ListItem Value="4" Text="Roman Numerals (iiii)" />
															<asp:ListItem Value="5" Text="Roman Numerals (IIII)" />
														</asp:DropDownList>
													</td>
												</tr>
												<tr>
													<td style="white-space: nowrap;">
														<asp:Label ID="startValueLabel" runat="server">Start Value</asp:Label>
														&nbsp;
														<asp:TextBox ID="startValueTextBox" runat="server" Text="1" Width="50px" />
														&nbsp;
														<asp:Label ID="incrementLabel" runat="server">Increment</asp:Label>
														&nbsp;
														<asp:TextBox ID="incrementTextBox" runat="server" Text="1" Width="50px" />
													</td>
												</tr>
												<tr>
													<td style="border-bottom: 1px solid #808080; width: 100%">
													</td>
												</tr>
												<tr>
													<td style="white-space: nowrap">
														<asp:RadioButton ID="freeTextStyleRadio" runat="server" Checked="false" GroupName="styleRadio" Text="Alpha-numeric Page No" />
														&nbsp;
														<asp:TextBox ID="freeTextBox" runat="server" MaxLength="20" Width="150px" />
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td colspan="2">
											Implied&nbsp;&nbsp;
											<asp:CheckBox ID="impliedCheckBox" runat="server" />
										</td>
									</tr>
									<tr>
										<td colspan="2" align="center">
											<asp:Button ID="saveIndicatedPagesButton" runat="server" Text="Assign" OnClick="saveIndicatedPagesButton_Click" />&nbsp;&nbsp;
											<asp:Button ID="replaceIndicatedPageButton" runat="server" Text="Replace" OnClick="replaceIndicatedPagesButton_Click" />&nbsp;&nbsp;
											<input type="button" value="Cancel" onclick="document.getElementById('indicatedPagesDiv').style.display='none';" />
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
</asp:Content>
