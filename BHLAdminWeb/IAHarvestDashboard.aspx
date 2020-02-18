<%@ Page Language="C#" MasterPageFile="/Admin.Master" AutoEventWireup="True" CodeBehind="IAHarvestDashboard.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.IAHarvestDashboard" %>
<%@ Register TagPrefix="cc" Namespace="MOBOT.BHL.AdminWeb" Assembly="MOBOT.BHL.AdminWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <script language="javascript">
        var oldBackColor;
        var oldCursor;

        function addHighlight(target) {
            oldBackColor = target.style.backgroundColor;
            oldCursor = target.style.cursor;
            target.style.backgroundColor = "#00ACA2";
            target.style.cursor = "pointer";
        }

        function removeHighlight(target) {
            target.style.backgroundColor = oldBackColor;
            target.style.cursor = oldCursor;
        }

        function toggleDisplay(target) {
            if (target.style.display == "none") {
                target.style.display = "block";
            } else {
                target.style.display = "none";
            }
        }
    </script>
    <br />
    <a href="/">&lt; Return to Dashboard</a><br /><br />
	<cc:ContentPanel ID="contentPanel" runat="server">
	<div class="pageHeader" style="width:800px">IA Harvest Dashboard</div>
    <br />

	<table cellpadding="0px" cellspacing="0px" width="800px">
		<tr>
			<td class="box" valign="top" width="45%">
				<table width="100%" cellspacing="0px" cellpadding="3px" style="background-color:White">
					<tr>
						<td class="boxHeader" align="center">
							Admin Functions
						</td>
					</tr>
					<tr>
						<td align="center">
							<a href="/queueforharvest.aspx">Queue Item for Download (from IA)</a>
						</td>
					</tr>
					<tr>
					    <td align="center">
					        <a href="/iaharvestitemlist.aspx">View/Update Item Details (IA)</a>
					    </td>
					</tr>
					<tr>
						<td align="center">
							<asp:HyperLink ID="hypNumItems" NavigateUrl="reportiaitemspendingapproval.aspx?age=" runat="server" Text="Download Items Pending Approval More Than 7 Days"></asp:HyperLink>
						</td>
					</tr>
				</table>
			</td>
            <td></td>
            <td class="box" valign="top" width="45%">
				<table width="100%" cellspacing="0px" cellpadding="3px" style="background-color:White">
					<tr>
						<td class="boxHeader" align="center">
							Reports

                            <!-- Possible report:  List of items by status -->
						</td>
					</tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr><td>&nbsp;</td></tr>
				</table>
            </td>
        </tr>
    </table><br /><br />

    <div style="width:810px" class="tableHeader" onmouseover="addHighlight(this);" onmouseout="removeHighlight(this);" onclick="toggleDisplay(getElementById('divStats'));"><span>CURRENT STATUS</span></div>
    <div id="divStats" style="border-color:Black; border-width:1px; border-style:solid; display:block; background-color:White">
        <table width="800px" cellpadding="5" cellspacing="5">
            <tr>
                <td colspan="2">
                    <span class="BlackHeading">Statistics</span><br />
                    <div style="border-style:solid;border-color:Black;border-width:1px">
                        <asp:GridView ID="gvItemCountByStatus" runat="server" AutoGenerateColumns="False" Width="95%" HorizontalAlign="Center" GridLines="none">
                        <Columns>
                            <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-VerticalAlign="top" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField DataField="NumberOfItems" HeaderText="# Of Items" ItemStyle-VerticalAlign="top" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right" />
                        </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
	                <span class="BlackHeading">Data Ready To Be Published</span>
                    <div style="border-style:solid;border-color:Black;border-width:1px; width:50%">
                        <asp:GridView ID="gvIAReadyToPublish" runat="server" AutoGenerateColumns="false" Width="70%" HorizontalAlign="center" GridLines="none">
                        <Columns>
                            <asp:BoundField DataField="Type" HeaderText="Table" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="NumberOfItems" HeaderText="# Of Items" HeaderStyle-HorizontalAlign="right" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="right" /></Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div><br />

    <div style="width:810px" class="tableHeader" onmouseover="addHighlight(this);" onmouseout="removeHighlight(this);" onclick="toggleDisplay(getElementById('divLogs'));"><span>LOGS</span></div>
    <div id="divLogs" style="border-color:Black; border-width:1px; border-style:solid; display:none; background-color:White"">
        <table width="800px" cellpadding="5" cellspacing="5">
            <tr>
                <td>
	                <span class="BlackHeading">Latest Import Logs (Results of Publish To Production)</span>
                    <div style="overflow:scroll;height:400px;width:785px;border-style:solid;border-color:Black;border-width:1px">
                        <table runat="server" clientidmode="Static" id="tblImportLog" style="width:70%;border-collapse:collapse;" cellspacing="0"></table>
                    </div>
                </td>
            </tr>
        </table>
    </div><br />

    <div style="width:810px" class="tableHeader" onmouseover="addHighlight(this);" onmouseout="removeHighlight(this);" onclick="toggleDisplay(getElementById('divErrors'));"><span>ERRORS</span></div>
    <div id="divErrors" style="border-color:Black; border-width:1px; border-style:solid; display:none; background-color:White"">
        <table width="800px" cellpadding="5" cellspacing="5">
            <tr>
                <td>
	                <span class="BlackHeading">Latest Import Errors (Errors Publishing To Production)</span>
                    <div style="overflow:scroll;height:300px;width:785px;border-style:solid;border-color:Black;border-width:1px">
                        <asp:GridView ID="gvLatestPubToProdErrors" runat="server" AutoGenerateColumns="false" HorizontalAlign="center" GridLines="none">
                        <Columns>
                            <asp:BoundField DataField="ErrorDate" HeaderText="Error Date" HeaderStyle-HorizontalAlign="left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="Number" HeaderText="Number" HeaderStyle-HorizontalAlign="left" HeaderStyle-BackColor="#EEEEEE" ItemStyle-BackColor="#EEEEEE" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="Severity" HeaderText="Severity" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="State" HeaderText="State" HeaderStyle-HorizontalAlign="left" HeaderStyle-BackColor="#EEEEEE" ItemStyle-BackColor="#EEEEEE" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="Procedure" HeaderText="Procedure" HeaderStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="Line" HeaderText="Line" HeaderStyle-HorizontalAlign="left" HeaderStyle-BackColor="#EEEEEE" ItemStyle-BackColor="#EEEEEE" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="Message" HeaderText="Message" HeaderStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="KeyValue" HeaderText="Key" HeaderStyle-HorizontalAlign="left" HeaderStyle-BackColor="#EEEEEE" ItemStyle-BackColor="#EEEEEE" ItemStyle-VerticalAlign="top" />
                        </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
	                <span class="BlackHeading">Latest Internet Archive Item Errors (Errors Moving From "Approved" To "Complete" Status)</span>
                    <div style="overflow:scroll;height:400px;width:785px;border-style:solid;border-color:Black;border-width:1px">
                        <asp:GridView ID="gvIAItemErrors" runat="server" AutoGenerateColumns="false" HorizontalAlign="center" GridLines="none">
                        <Columns>
                            <asp:BoundField DataField="ErrorDate" HeaderText="Error Date" HeaderStyle-HorizontalAlign="left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="Number" HeaderText="Number" HeaderStyle-HorizontalAlign="left" HeaderStyle-BackColor="#EEEEEE" ItemStyle-BackColor="#EEEEEE" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="Severity" HeaderText="Severity" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="State" HeaderText="State" HeaderStyle-HorizontalAlign="left" HeaderStyle-BackColor="#EEEEEE" ItemStyle-BackColor="#EEEEEE" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="Procedure" HeaderText="Procedure" HeaderStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="Line" HeaderText="Line" HeaderStyle-HorizontalAlign="left" HeaderStyle-BackColor="#EEEEEE" ItemStyle-BackColor="#EEEEEE" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="Message" HeaderText="Message" HeaderStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="top" />
                            <asp:BoundField DataField="IAIdentifier" HeaderText="IA ID" HeaderStyle-HorizontalAlign="left" HeaderStyle-BackColor="#EEEEEE" ItemStyle-BackColor="#EEEEEE" ItemStyle-VerticalAlign="top" />
                        </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div><br /><br />
    </cc:ContentPanel>

</asp:Content>
