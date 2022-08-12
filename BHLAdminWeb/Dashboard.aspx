<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="True" Codebehind="Dashboard.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.Dashboard" %>
<%@ Register TagPrefix="cc" Namespace="MOBOT.BHL.AdminWeb" Assembly="MOBOT.BHL.AdminWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<br />
	<cc:ContentPanel ID="contentPanel" runat="server">
	<span class="pageHeader">Dashboard </span>
    <span runat="server" id="spnMonitor"><a href="/monitor">Server Monitor</a></span>
	<table cellspacing="25px" cellpadding="0px">
		<tr>
			<td class="box" style="width: 250px; background-color:White">
				<table width="100%" cellspacing="0px"  cellpadding="3px">
					<tr>
						<td class="boxHeader" align="center">
							Admin Functions
						</td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdAlertMessage">
							<a href="/AlertEdit.aspx">Alert Message </a>
						</td>
					</tr>
					<tr>
					    <td align="center" runat="server" id="tdCollections">
					        <a href="/CollectionEdit.aspx">Collections</a>
					    </td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdInstitutions">
							<a href="/InstitutionEdit.aspx">Content Providers</a>
						</td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdInstitutionGroups">
							<a href="/admin/groups">Content Provider Groups</a>
						</td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdLanguages">
							<a href="/LanguageEdit.aspx">Languages</a>
						</td>
					</tr>
                    <tr>
                        <td align="center" runat="server" id="tdNoteTypes">
                            <a href="/NoteTypeEdit.aspx">Note Types</a>
                        </td>
                    </tr>
					<tr>
						<td align="center" runat="server" id="tdPageTypes">
							<a href="/PageTypeEdit.aspx">Page Types</a>
						</td>
					</tr>
					<tr>
					    <td align="center" runat="server" id="tdPDFRequests">
					        <a href="/PdfEdit.aspx">PDF Requests</a>
					    </td>
					</tr>
                    <tr>
                        <td align="center" runat="server" id="tdSegmentTypes">
                            <a href="/SegmentTypeEdit.aspx">Segment Types</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" runat="server" id="tdUserAccounts">
                            <a href="/account/">User Accounts</a>
                        </td>
                    </tr>
				</table>
			</td>
			<td class="box" style="width: 250px;background-color:White" valign="top">
				<table width="100%" cellspacing="0px" cellpadding="3px">
					<tr>
						<td class="boxHeader" align="center">
							Library Functions
						</td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdTitles">
							<a href="/TitleSearch.aspx">Titles</a>
						</td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdItems">
							<a href="/ItemEdit.aspx">Items</a>
						</td>
					</tr>
                    <tr>
                        <td align="center" runat="server" id="tdSegments">
                            <a href="/SegmentSearch.aspx">Segments</a>
                        </td>
                    </tr>
					<tr>
						<td align="center" runat="server" id="tdPagination">
							<a href="/TitleSearch.aspx?redir=p">Pagination</a>
						</td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdAuthors">
							<a href="/AuthorSearch.aspx">Authors</a>
						</td>
					</tr>
				</table>
			</td>
			<td class="box" style="width: 250px;background-color:White" valign="top">
				<table width="100%" cellspacing="0px" cellpadding="3px">
					<tr>
						<td class="boxHeader" align="center">
							Science Functions
						</td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdNames">
							<a href="/NamePageEdit.aspx">Names (Taxa) on a Page</a>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="box" style="width: 250px;background-color:White" valign="top">
				<table width="100%" cellspacing="0px">
					<tr>
						<td class="boxHeader" align="center">
							Library Stats<div style="float:right"><a class="small" href="#" title="About Library Stats" onclick="window.open('LibraryStatsAbout.html', 'About', 'resizeable=0,scrollbars=1,height=530,width=500,status=0,toolbar=0,menubar=0,location=0');"><img src="images/help.png" alt="Help" height="16" width="16" /></a></div>
						</td>
					</tr>
					<tr>
						<td>
							<table width="97%" cellspacing="0px" style="margin-left:3px; margin-right:3px">
								<tr>
									<td width="44%">
										&nbsp;
									</td>
									<td align="center" style="white-space: nowrap; width: 28%; border-bottom: 1px solid black">
										All Records
									</td>
									<td align="center" style="white-space: nowrap; width: 28%; border-bottom: 1px solid black">
										Active
									</td>
								</tr>
								<tr>
									<td>
										Titles
									</td>
									<td align="right" class="liveData" runat="server" id="titlesAllCell">
									</td>
									<td align="right" class="liveData" runat="server" id="titlesActiveCell">
									</td>
								</tr>
								<tr>
									<td>
										Items
									</td>
									<td align="right" class="liveData" runat="server" id="itemsAllCell">
									</td>
									<td align="right" class="liveData" runat="server" id="itemsActiveCell">
									</td>
								</tr>
								<tr>
									<td>
										Pages
									</td>
									<td align="right" class="liveData" runat="server" id="pagesAllCell">
									</td>
									<td align="right" class="liveData" runat="server" id="pagesActiveCell">
									</td>
								</tr>
                                <tr>
                                    <td>
                                        Segments
                                    </td>
                                    <td align="right" class="liveData" runat="server" id="segmentsAllCell">
                                    </td>
                                    <td align="right" class="liveData" runat="server" id="segmentsActiveCell">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Items w/ Segments
                                    </td>
                                    <td align="right" class="liveData" runat="server" id="itemSegmentsAllCell">
                                    </td>
                                    <td align="right" class="liveData" runat="server" id="itemSegmentsActiveCell">
                                    </td>
                                </tr>
								<tr>
								    <td colspan="3" align="center" runat="server" id="tdShowNames"><a id="namesShowLink" href="#" onclick="showNames();">Show All Names</a>
								    </td>
								</tr>
								<tr>
									<td>
										<span id="namesNameLabel" style="display:none">Names</span>
									    <img id="imgNamesLoading" alt="Loading" src="images/loading.gif" style="display:none" />
									</td>
									<td align="right" class="liveData" runat="server" id="namesAllCell">
									    <span id="namesAllSpan" style="display:none"></span>
									</td>
									<td align="right" class="liveData" runat="server" id="namesActiveCell">
									    <span id="namesActiveSpan" style="display:none"></span>
									</td>
								</tr>
								<tr>
								    <td colspan="3" align="center" runat="server" id="tdShowUniqueNames"><a id="uniqueNamesShowLink" href="#" onclick="showUniqueNames();">Show Unique Names</a>
								    </td>
								</tr>
								<tr>
									<td>
										<span id="uniqueNameLabel" style="display:none">Unique Names</span>
									    <img id="imgUniqueLoading" alt="Loading" src="images/loading.gif" style="display:none" />
									</td>
									<td align="right" class="liveData" runat="server" id="uniqueAllCell">
									    <span id="uniqueAllSpan" style="display:none"></span>
									</td>
									<td align="right" class="liveData" runat="server" id="uniqueActiveCell">
									    <span id="uniqueActiveSpan" style="display:none"></span>
									</td>
								</tr>
								<tr>
								    <td colspan="3" align="center" runat="server" id="tdShowVerifiedNames"><a id="verifiedNamesShowLink" href="#" onclick="showVerifiedNames();">Show Verified Names</a>
								    </td>
								</tr>
								<tr>
									<td>
										<span id="verifiedNameLabel" style="display:none">Verified Names</span>
									    <img id="imgVerifiedLoading" alt="Loading" src="images/loading.gif" style="display:none" />
									</td>
									<td align="right" class="liveData" runat="server" id="verifiedAllCell">
									    <span id="verifiedAllSpan" style="display:none"></span>
									</td>
									<td align="right" class="liveData" runat="server" id="verifiedActiveCell">
									    <span id="verifiedActiveSpan" style="display:none"></span>
									</td>
								</tr>
								<tr>
								    <td colspan="3" align="center" runat="server" id="tdShowEOLNames"><a id="eolNamesShowLink" href="#" onclick="showEOLNames();">Show EOL Names</a>
								    </td>
								</tr>
								<tr>
									<td>
										<span id="eolNameNameLabel" style="display:none">EOL Names</span>
									    <img id="imgEOLNameLoading" alt="Loading" src="images/loading.gif" style="display:none" />
									</td>
									<td align="right" class="liveData" runat="server" id="eolNameAllCell">
									    <span id="eolNameAllSpan" style="display:none"></span>
									</td>
									<td align="right" class="liveData" runat="server" id="eolNameActiveCell">
									    <span id="eolNameActiveSpan" style="display:none"></span>
									</td>
								</tr>
								<tr>
								    <td colspan="3" align="center" runat="server" id="tdShowEOLPages"><a id="eolPagesShowLink" href="#" onclick="showEOLPages();">Show Pages with EOL Names</a>
								    </td>
								</tr>
								<tr>
									<td>
										<span id="eolPageNameLabel" style="display:none">EOL Pages</span>
									    <img id="imgEOLPageLoading" alt="Loading" src="images/loading.gif" style="display:none" />
									</td>
									<td align="right" class="liveData" runat="server" id="eolPageAllCell">
									    <span id="eolPageAllSpan" style="display:none"></span>
									</td>
									<td align="right" class="liveData" runat="server" id="eolPageActiveCell">
									    <span id="eolPageActiveSpan" style="display:none"></span>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
			<td class="box" style="width: 250px;background-color:White" valign="top">
				<table width="100%" cellspacing="0px">
					<tr>
						<td class="boxHeader" align="center" colspan="2">
							Growth Stats
						</td>
					</tr>
					<tr>
						<td colspan="2" align="center">
							<table>
								<tr>
									<td>&nbsp;</td>
									<td align="center" style="white-space: nowrap; width: 25%; border-bottom: 1px solid black">
										New<br />This Year
									</td>
									<td align="center" style="white-space: nowrap; width: 25%; border-bottom: 1px solid black">
										New<br />This Month
									</td>
									<td align="center" style="white-space: nowrap; width: 25%; border-bottom: 1px solid black">
										New<br />Last Month
									</td>
								</tr>
								<tr>
									<td>Items</td>
									<td align="right" runat="server" id="itemsThisYear">0</td>
									<td align="right" runat="server" id="itemsThisMonth">0</td>
									<td align="right" runat="server" id="itemsPrevMonth">0</td>
								</tr>
								<tr>
									<td>Pages</td>
									<td align="right" runat="server" id="pagesThisYear">0</td>
									<td align="right" runat="server" id="pagesThisMonth">0</td>
									<td align="right" runat="server" id="pagesPrevMonth">0</td>
								</tr>
								<tr>
									<td>Names</td>
									<td align="right" runat="server" id="namesThisYear">0</td>
									<td align="right" runat="server" id="namesThisMonth">0</td>
									<td align="right" runat="server" id="namesPrevMonth">0</td>
								</tr>
                                <tr>
                                    <td>Segments</td>
                                    <td align="right" runat="server" id="segmentsThisYear">0</td>
									<td align="right" runat="server" id="segmentsThisMonth">0</td>
									<td align="right" runat="server" id="segmentsPrevMonth">0</td>
                                </tr>
							</table>
						</td>
					</tr>
					<tr><td>&nbsp;</td></tr>
					<tr>
					    <td align="center" colspan="2" runat="server" id="tdExpandedGrowthStats">
                            <a href="/GrowthStats.aspx">Expanded Growth Stats</a>
                        </td>
                    </tr>
				</table>
			</td>
			<td class="box" style="width: 250px;background-color:White" valign="top">
				<table width="100%" cellspacing="0px">
					<tr>
						<td class="boxHeader" align="center" colspan="2">
							PDF Generation Stats
						</td>
					</tr>
					<tr>
						<td colspan="2">
                            <asp:GridView ID="gvPDFGeneration" runat="server" AutoGenerateColumns="False" GridLines="none" Width="90%" HorizontalAlign="center" CellSpacing="3" HeaderStyle-Font-Underline="true">
                            <Columns>
                            <asp:BoundField HeaderText="Status" HeaderStyle-HorizontalAlign="left" DataField="PdfStatusName" />
                            <asp:BoundField HeaderText="# Of PDFs" HeaderStyle-HorizontalAlign="right" DataField="NumberofPdfs" ItemStyle-HorizontalAlign="right" />
                            </Columns>
                            </asp:GridView>
						</td>
					</tr>
					<tr><td>&nbsp;</td></tr>
					<tr>
					    <td align="center" colspan="2" runat="server" id="tdExpandedPDFStats">
                            <a href="/PdfStats.aspx">Expanded PDF Stats</a>
                        </td>
                    </tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="box" style="width: 250px;background-color:White" valign="top">
				<table width="100%" cellspacing="0px">
					<tr>
						<td class="boxHeader" align="center" colspan="2">
							IA Harvest Stats
						</td>
					</tr>
					<tr>
						<td colspan="2">
                            <asp:GridView ID="gvItemStatus" runat="server" AutoGenerateColumns="False" GridLines="none" Width="90%" HorizontalAlign="center" CellSpacing="3" HeaderStyle-Font-Underline="true">
                            <Columns>
                            <asp:BoundField HeaderText="Item Status" HeaderStyle-HorizontalAlign="left" DataField="Status" />
                            <asp:BoundField HeaderText="# Of Items" HeaderStyle-HorizontalAlign="right" DataField="NumberOfItems" ItemStyle-HorizontalAlign="Right" />
                            </Columns>
                            </asp:GridView>
						</td>
					</tr>
					<tr><td>&nbsp;</td></tr>
					<tr>
					    <td>
					        &nbsp;&nbsp;Items Pending Approval<br />&nbsp;&nbsp;More 
                            Than <asp:Literal ID="litNumDays" runat="server"></asp:Literal> Days
					    </td>
					    <td runat="server" id="tdIAPendingItems"><asp:HyperLink ID="hypNumItems" NavigateUrl="reportiaitemspendingapproval.aspx?age=" runat="server"></asp:HyperLink></td>
					</tr>
					<tr><td>&nbsp;</td></tr>
					<tr>
					    <td align="center" colspan="2" runat="server" id="tdViewUpdateIA">
                            <a href="/IAHarvestItemList.aspx">View/Update Item Details</a><br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" runat="server" id="tdIAHarvestDash">
                            <a href="/IAHarvestDashboard.aspx">IA Harvest Dashboard</a>
                        </td>
                    </tr>
				</table>
		    </td>
		    <td class="box" style="width: 250px;background-color:White" valign="top">
                <table width="100%" cellspacing="0px">
                    <tr>
                        <td class="boxHeader" align="center" colspan="2">
                            BioStor Harvest Stats
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvBSItemStatus" runat="server" AutoGenerateColumns="false" GridLines="None" Width="90%" HorizontalAlign="Center" CellSpacing="3" HeaderStyle-Font-Underline="true">
                            <Columns>
                            <asp:BoundField HeaderText="Item Status" HeaderStyle-HorizontalAlign="Left" DataField="Status" />
                            <asp:BoundField HeaderText="# Of Items" HeaderStyle-HorizontalAlign="Right" DataField="NumberOfItems" ItemStyle-HorizontalAlign="Right" />
                            </Columns>                            
                            </asp:GridView>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" runat="server" id="tdViewUpdateBioStor">
                            <a href="/BioStorHarvestItemList.aspx">View/Update Item Details</a>
                        </td>
                    </tr>
                </table>
            </td>
			<td class="box" style="width: 250px;background-color:White" valign="top">
                <table width="100%" cellspacing="0" cellpadding="3">
                    <tr>
                        <td class="boxHeader" align="center" colspan="2">
                            DOI Assignment Stats
                        </td>
                    </tr>
					<tr>
						<td colspan="2">
                            <asp:GridView ID="gvDOIStatus" runat="server" AutoGenerateColumns="False" GridLines="none" Width="90%" HorizontalAlign="center" CellSpacing="3" HeaderStyle-Font-Underline="true">
                            <Columns>
                            <asp:BoundField HeaderText="DOI Status" HeaderStyle-HorizontalAlign="left" DataField="DOIStatusName" />
                            <asp:BoundField HeaderText="# Of DOIs" HeaderStyle-HorizontalAlign="right" DataField="NumberOfDOIs" ItemStyle-HorizontalAlign="Right" />
                            </Columns>
                            </asp:GridView>
						</td>
					</tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td align="center" colspan="2" runat="server" id="tdManageDoiQueue">
                            <a href="/doi/queue">Manage DOI Queue</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" runat="server" id="tdViewUpdateDoi">
                            <a href="/doilist.aspx">View/Update DOI Status</a>
                        </td>
                    </tr>
                </table>
		    </td>
		</tr>
        <tr>
            <td class="box" style="width: 250px;background-color:White" valign="top">
            	<table width="100%" cellspacing="0" cellpadding="3" runat="server" id="trafficStatsMenu" visible="false">
					<tr>
						<td class="boxHeader" align="center" colspan="2">
							Web Traffic Stats
						</td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdApiV2Stats">
							<a href="/WebStatsDaily.aspx?id={0}&mid=v2" runat="server" id="apiv2StatsLink">API v2</a>
						</td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdApiV3Stats">
							<a href="/WebStatsDaily.aspx?id={0}&mid=v3" runat="server" id="apiv3StatsLink">API v3</a>
						</td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdOpenUrlStats">
							<a href="/WebStatsDaily.aspx?id={0}&mid=ou" runat="server" id="openurlStatsLink">OpenUrl</a>
						</td>
					</tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr><td>&nbsp;</td></tr>
				</table>
            </td>
            <td class="box" style="width: 250px;background-color:White" valign="top">
				<table width="100%" cellspacing="0px" cellpadding="3px">
					<tr>
						<td class="boxHeader" align="center">
							Reports / Downloads
						</td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdRptReportingStats">
							<a href="/Report/ReportingStats">BHL Reporting Statistics</a>
						</td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdRptItemPagination">
							<a href="ReportItemPagination.aspx">Item Pagination</a>
						</td>
					</tr>
					<tr>
					    <td align="center" runat="server" id="tdRptTitleImportHistory">
					        <a href="TitleImportHistory.aspx">Title Import History</a>
					    </td>
					</tr>
					<tr>
					    <td align="center" runat="server" id="tdRptCharEncoding">
					        <a href="ReportCharacterEncodingProblems.aspx">Character Encoding Problems</a>
					    </td>
					</tr>
                    <tr>
                        <td align="center" runat="server" id="tdRptDoiByInstitution">
                            <a href="ReportDOIByInstitution.aspx">DOIs By Content Provider</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" runat="server" id="tdRptMonoContributions">
                            <a href="ReportMonographicContributions.aspx">Monographic Contributions</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" runat="server" id="tdRptItemsByContributor">
                            <a href="ReportItemsByContentProvider.aspx">Items By Content Provider</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" runat="server" id="tdRptRecentlyClustered">
                            <a href="ReportRecentlyClusteredSegments.aspx">Recently Clustered Segments</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" runat="server" id="tdRptOrphan">
                            <a href="/Report/Orphans">Orphaned Titles/Items/Segments</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" runat="server" id="tdDLExtContent">
                            <a href="/Downloads/ExternalContent">Links to External Content [Download]</a>
                        </td>
                    </tr>
				</table>
            </td>
            <td class="box" style="width: 250px;background-color:White" valign="top">
				<table width="100%" cellspacing="0px" cellpadding="3px">
					<tr>
						<td class="boxHeader" align="center">
							Data Import
						</td>
					</tr>
					<tr>
						<td align="center" runat="server" id="tdImportCitations">
							<a href="/CitationImport">Import Segments</a>
						</td>
					</tr>
					<tr>
					    <td align="center" runat="server" id="tdImportCitationHistory">
					        <a href="Report/CitationImportHistory">Segment Import History</a>
					    </td>
					</tr>

					<tr>
						<td align="center" runat="server" id="tdImportItemText">
							<a href="/TextImport">Import Text</a>
						</td>
					</tr>
					<tr>
					    <td align="center" runat="server" id="tdImportTextHistory">
					        <a href="Report/TextImportHistory">Text Import History</a>
					    </td>
					</tr>

				</table>
            </td>
        </tr>
	</table>
	<script type="text/javascript">
        function showNames() {
            document.getElementById('imgNamesLoading').style.display = 'block';
            document.getElementById('namesShowLink').style.display = 'none';
            executeServiceCall("services/statsservice.ashx?op=SelectNames", updateNamesUI);
        }

        function showUniqueNames() {
            document.getElementById('imgUniqueLoading').style.display = 'block';
            document.getElementById('uniqueNamesShowLink').style.display = 'none';
            executeServiceCall("services/statsservice.ashx?op=SelectUniqueNames", updateUniqueNamesUI);
        }

        function showVerifiedNames() {
            document.getElementById('imgVerifiedLoading').style.display = 'block';
            document.getElementById('verifiedNamesShowLink').style.display = 'none';
            executeServiceCall("services/statsservice.ashx?op=SelectVerifiedNames", updateVerifiedNamesUI);
        }

        function showEOLNames() {
            document.getElementById('imgEOLNameLoading').style.display = 'block';
            document.getElementById('eolNamesShowLink').style.display = 'none';
            executeServiceCall("services/statsservice.ashx?op=SelectEOLNames", updateEOLNamesUI);
        }

        function showEOLPages() {
            document.getElementById('imgEOLPageLoading').style.display = 'block';
            document.getElementById('eolPagesShowLink').style.display = 'none';
            executeServiceCall("services/statsservice.ashx?op=SelectEOLPages", updateEOLPagesUI);
        }

        function updateNamesUI(results) {
            updateUI(results.NameTotal, results.NameCount, 'imgNamesLoading', 'namesAllSpan', 'namesActiveSpan', 'namesNameLabel');
        }

        function updateUniqueNamesUI(results) {
            updateUI(results.UniqueNameTotal, results.UniqueNameCount, 'imgUniqueLoading', 'uniqueAllSpan', 'uniqueActiveSpan', 'uniqueNameLabel');
        }

        function updateVerifiedNamesUI(results) {
            updateUI(results.VerifiedNameTotal, results.VerifiedNameCount, 'imgVerifiedLoading', 'verifiedAllSpan', 'verifiedActiveSpan', 'verifiedNameLabel');
        }

        function updateEOLNamesUI(results) {
            updateUI(results.EolNameTotal, results.EolNameCount, 'imgEOLNameLoading', 'eolNameAllSpan', 'eolNameActiveSpan', 'eolNameNameLabel');
        }

        function updateEOLPagesUI(results) {
            updateUI(results.EolPageTotal, results.EolPageCount, 'imgEOLPageLoading', 'eolPageAllSpan', 'eolPageActiveSpan', 'eolPageNameLabel');
        }

        function updateUI(total, count, img, allSpan, activeSpan, nameLabel) {
            document.getElementById(allSpan).innerHTML = total;
            document.getElementById(activeSpan).innerHTML = count;
            document.getElementById(img).style.display = 'none';
            document.getElementById(nameLabel).style.display = 'block';
            document.getElementById(allSpan).style.display = 'block';
            document.getElementById(activeSpan).style.display = 'block';
        }

        function executeServiceCall(url, callback) {
            var request = createXMLHttpRequest();
            request.open("GET", url, true);
            request.onreadystatechange = function () {
                if (request.readyState === 4) {
                    if (request.status === 200) {
                        var result = eval('(' + request.responseText + ')');
                        callback(result);
                    }
                }
            };
            request.send(null);
        }

        function createXMLHttpRequest() {
            if (typeof XMLHttpRequest !== "undefined") {
                return new XMLHttpRequest();
            } else if (typeof ActiveXObject !== "undefined") {
                return new ActiveXObject("Microsoft.XMLHTTP");
            } else {
                throw new Error("XMLHttpRequest not supported");
            }
        }

	</script>
	</cc:ContentPanel>
</asp:Content>

