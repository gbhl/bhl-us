<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PdfStats.aspx.cs" Inherits="MOBOT.BHL.AdminWeb.PdfStats" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<br />
    <a href="/">&lt; Return to Dashboard</a><br />
	<br />
	<span class="pageHeader">PDF Generation Stats</span><hr />
	<table cellpadding="7" cellspacing="7">
	<tr>
	    <td valign="top">
            <span class="tableHeader">Summary Statistics</span><br />
            <div style="overflow:auto;height:auto;width:800px;border-style:solid;border-color:Black;border-width:1px;text-align:center">
                <asp:GridView ID="gvSummary" runat="server" 
                    AutoGenerateColumns="False" Width="475" HorizontalAlign="Center" 
                    GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true"
                    RowStyle-BackColor="#FFFFD7"
                    OnRowDataBound="gv_SummaryRowDataBound">
                <Columns>
                    <asp:BoundField DataField="PdfStatusName" HeaderText="Status" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="left" HeaderStyle-VerticalAlign="Bottom" FooterStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="NumberofPdfs" HeaderText="# Of PDFs" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right" HeaderStyle-VerticalAlign="Bottom" FooterStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PdfsWithOcr" HeaderText="With OCR" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right" HeaderStyle-VerticalAlign="Bottom" FooterStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PdfsWithArticleMetadata" HeaderText="With Article<br>Info" HtmlEncode="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right" HeaderStyle-VerticalAlign="Bottom" FooterStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PdfsWithMissingImages" HeaderText="With Missing<br>Images" HtmlEncode="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right" HeaderStyle-VerticalAlign="Bottom" FooterStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PdfsWithMissingOcr" HeaderText="With Missing<br>OCR" HtmlEncode="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign=right HeaderStyle-HorizontalAlign="right" HeaderStyle-VerticalAlign="Bottom" FooterStyle-HorizontalAlign="Right" />
                </Columns>
                </asp:GridView>
            </div>
        </td>
    </tr>
	<tr>
	    <td valign="top">
            <span class="tableHeader">Expanded Statistics</span><br />
            <div style="overflow:auto;height:300px;width:800px;border-style:solid;border-color:Black;border-width:1px">
                <asp:GridView ID="gvExpanded" runat="server" 
                    AutoGenerateColumns="False" Width="90%" HorizontalAlign="Center" 
                    GridLines="none" ShowFooter="true" FooterStyle-Font-Bold="true"
                    AlternatingRowStyle-BackColor="#F7FAFB" OnRowDataBound="gv_ExpandedRowDataBound">
                <Columns>
                    <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField DataField="Week" HeaderText="Week" ItemStyle-VerticalAlign="top" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"/>
                    <asp:HyperLinkField DataTextField="PdfStatusName" HeaderText="Status" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="false" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Left" DataNavigateUrlFormatString="PdfWeeklyStats.aspx?y={0}&w={1}&s={2}&sn={3}" DataNavigateUrlFields="Year,Week,PdfStatusID,PdfStatusName" />
                    <asp:BoundField DataField="NumberofPdfs" HeaderText="# Of PDFs" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign=right HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PdfsWithOcr" HeaderText="PDFs With<br>OCR" HtmlEncode="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign=right HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PdfsWithArticleMetadata" HeaderText="PDFs With<br>Article Info" HtmlEncode="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign=right HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PdfsWithMissingImages" HeaderText="PDFs<br>Missing<br>Images" HtmlEncode="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign=right HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PdfsWithMissingOcr" HeaderText="PDFs<br>Missing<br>OCR" HtmlEncode="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign=right HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TotalMissingImages" HeaderText="Total<br>Missing<br>Images" HtmlEncode="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign=right HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TotalMissingOcr" HeaderText="Total<br>Missing<br>OCR" HtmlEncode="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign=right HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="AveMinutesToGenerate" HeaderText="Avg Minutes<br>To Generate" HtmlEncode="false" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign=right HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" />
                </Columns>
                </asp:GridView>
            </div>
        </td>
    </tr>
    </table>
</asp:Content>
