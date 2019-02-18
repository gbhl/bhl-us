<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ReportItemsByContentProvider.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.ReportItemsByContentProvider" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">Items By Content Provider</span><hr />
	<p>
        This report shows items from a specific content provider.
    </p>
    <p>
        NOTE: Use this report to find items not assigned to a particular content provider by selecting "Unknown" from the Content Provider drop-down.
    </p>
    <table>
        <tr>
            <td style="padding-right:50px;">
                <table>
                    <tr>
                        <td><b>Content Provider:</b></td>
                        <td><asp:DropDownList ID="ddlInstitutions" runat="server" DataTextField="InstitutionName" DataValueField="InstitutionCode" Width="300px"/></td>
                    </tr>
                    <tr>
                        <td><b>Role:</b></td>
                        <td><asp:DropDownList ID="ddlInstitutionRoles" runat="server" DataTextField="InstitutionRoleName" DataValueField="InstitutionRoleID"/></td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td><b>Only Items Where IA Identifier Contains:</b></td>
                        <td><asp:TextBox ID="txtIAIdentifier" runat="server" placeholder="(Optional)"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            <td>
        </tr>
    </table>      
    <p>
        <asp:Button ID="btnShow" runat="server" Text="Show Items" OnClick="btnShow_Click" />&nbsp;
        <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click" />
    </p>
    <p></p>
    <asp:Literal ID="litDisplayed" runat="server"></asp:Literal>

    <div id="listDiv" style="height:100%; overflow:auto;">
        <table id="list"></table>
        <div id="pager"></div>
    </div>
    Change&nbsp;<asp:DropDownList ID="ddlInstitutionRoleChange" runat="server"></asp:DropDownList>&nbsp;of Selected Items to:&nbsp;<asp:DropDownList ID="ddlInstitutionChange" runat="server"></asp:DropDownList>&nbsp;<asp:Button ID="btnChange" runat="server" Text="Update" OnClientClick="return getSelected();" OnClick="btnChange_Click" />
    <asp:HiddenField ID="hidSelected" runat="server" ClientIDMode="Static" /><br />
    <asp:Literal ID="litUpdateResult" runat="server"></asp:Literal>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#list").jqGrid({
                url: 'services/rptItemByContentProviderService.ashx?id=<%=selectedInstitutionCode%>&role=<%=selectedRoleID%>&barcode=<%=specifiedBarcode%>', // tells where to get the data
                datatype: 'xml',    // format of the data (xml,json,jsonp,array,xmlstring,jsonstring,script,function)
                mtype: 'GET',   // specify if AJAX call is a GET or POST
                colNames: ['Item ID', 'IA Identifier', 'Title', 'Volume', 'Year', 'Holding Institution', 'Rights Holder', 'Scanning Institution', 'Date Added', 'Date Updated'],    // column names
                colModel: [
                  { name: 'ItemID', index: 'ItemID', sortable: false, width: 60 },
                  { name: 'Barcode', index: 'Barcode' },
                  //{ name: 'TitleName', index: 'TitleName', cellattr: function (rowId, tv, rawObject, cm, rdata) { return 'style="white-space: normal;"' } },
                  { name: 'TitleName', index: 'TitleName' },
                  { name: 'Volume', index: 'Volume', sortable: false },
                  { name: 'Year', index: 'Year', width: 75},
                  { name: 'HoldingInstitution', index: 'HoldingInstitution', sortable: false },
                  { name: 'RightsHolder', index: 'RightsHolder', sortable: false },
                  { name: 'ScanningInstitution', index: 'ScanningInstitution', sortable: false },
                  { name: 'CreationDate', index: 'CreationDate', width: 100 },
                  { name: 'LastModifiedDate', index: 'LastModifiedDate', sortable: false, width: 75 }
                ],  // model of the columns to display
                pager: '#pager',    // show a pager bar for record navigation
                rowNum: 200,    // rows in grid
                rowList: [100, 200, 300],  // options in select box for changing number of rows displayed
                sortname: 'CreationDate',  // sort column
                sortorder: 'desc',  // sort direction
                viewrecords: true,  // display total number of records
                caption: '',    // grid caption; blank to hide
                loadui: 'block',    // block actions on the grid while data is being retrieved
                multiselect: true   // allow multiple row selection (adds checkbox at left)
            });
        });

        // Resize the grid to fill the space
        $(window).load(function () { resizeGrid(); });
        $(window).resize(function () { resizeGrid(); });

        function resizeGrid() {
            $("#list").setGridWidth($(window).width() - 40);
            $("#list").setGridHeight($(window).height() - 415);
        }

        function getSelected() {
            if (confirm("Are you sure you want to change the provider of the selected items?")) {

                $("#hidSelected").attr("value", "");
                var selected = $(".cbox:checked");
                for (x = 0; x < selected.length; x++) {
                    var hidValue = $("#hidSelected").attr("value");
                    if (hidValue.length > 0) hidValue += "|";
                    $("#hidSelected").attr("value", hidValue += selected[x].id);
                }

                return true;
            }
            else {
                return false
            }
        }
    </script>
</asp:Content>
