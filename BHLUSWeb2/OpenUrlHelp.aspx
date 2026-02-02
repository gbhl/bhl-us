<%@ Page Title="Biodiversity Heritage Library" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OpenUrlHelp.aspx.cs" Inherits="MOBOT.BHL.Web2.OpenUrlHelp" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />

<div id="page-title">
    <h1 class="column-wrap">OpenUrl Resolver Help</h1>
</div>

<div id="content" class="column-wrap clearfix">
    <p><asp:Literal runat="server" ID="litMessage"></asp:Literal></p>

        <p>
            The Biodiversity Heritage Library's OpenURL query interface is available 
            at <code><%= MOBOT.BHL.Web.Utilities.AppConfig.BaseUrl %>/openurl</code>.
        </p>
        <p>
            Both OpenURL 0.1 and OpenURL 1.0 queries are supported.
        </p>
        <h3>Request Parameters</h3>
        <p>
            The following table summarizes the parameters that are accepted by the OpenURL 0.1 and 1.0 
            query interfaces.
        </p>
            <table class="example" cellspacing="0">
                <colgroup>
                   <col width="33%">
                   <col width="33%">
                   <col width="33%">
                </colgroup>
                <thead>
                    <tr>
                        <th scope="col">OpenURL 0.1</th>
                        <th scope="col">OpenURL 1.0</th>
                        <th scope="col">Description</th>
                    </tr>
                </thead>
                <tbody>                    
                    <tr class="odd">
                        <td></td>
                        <td>url_ver=z39.88-2004</td>
                        <td>Indicates OpenURL version</td>                        
                    </tr>
                    <tr>
                        <td>title</td>
                        <td></td>
                        <td>Book/Journal/Article title</td>
                    </tr>
                    <tr class="odd">
                        <td></td>
                        <td>rft.btitle</td>
                        <td>Book title</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>rft.jtitle</td>
                        <td>Journal title</td>
                    </tr>
                    <tr class="odd">
                        <td></td>
                        <td>rft.atitle</td>
                        <td>Article title</td>
                    </tr>
                    <tr>
                        <td>aulast</td>
                        <td>rft.aulast</td>
                        <td>Author last name</td>
                    </tr>
                    <tr class="odd">
                        <td>aufirst</td>
                        <td>rft.aufirst</td>
                        <td>Author first name</td>
                    </tr>
                    <tr>
                        <td>date</td>
                        <td>rft.date</td>
                        <td>Publication date (YYYY or YYYY-MM or YYYY-MM-DD)</td>
                    </tr>
                    <tr class="odd">
                        <td>volume</td>
                        <td>rft.volume</td>
                        <td>Volume</td>
                    </tr>
                    <tr>
                        <td>issue</td>
                        <td>rft.issue</td>
                        <td>Issue</td>
                    </tr>
                    <tr class="odd">
                        <td>spage</td>
                        <td>rft.spage</td>
                        <td>Start page</td>
                    </tr>
                    <tr>
                        <td>id=doi:XXXX</td>
                        <td>rft_id=info:doi/XXXX</td>
                        <td>DOI (where XXXX is the ID value)</td>
                    </tr>
                    <tr class="odd">
                        <td>pid=title:XXXX</td>
                        <td>rft_id=<%= MOBOT.BHL.Web.Utilities.AppConfig.BaseUrl %>/bibliography/XXXX</td>
                        <td>BHL title ID (where XXXX is the ID value)</td>
                    </tr>
                    <tr>
                        <td>pid=item:XXXX</td>
                        <td>rft_id=<%= MOBOT.BHL.Web.Utilities.AppConfig.BaseUrl %>/item/XXXX</td>
                        <td>BHL item ID (where XXXX is the ID value)</td>
                    </tr>
                    <tr class="odd">
                        <td>pid=page:XXXX</td>
                        <td>rft_id=<%= MOBOT.BHL.Web.Utilities.AppConfig.BaseUrl %>/page/XXXX</td>
                        <td>BHL page ID (where XXXX is the ID value)</td>
                    </tr>
                    <tr>
                        <td>pid=part:XXXX</td>
                        <td>rft_id=<%= MOBOT.BHL.Web.Utilities.AppConfig.BaseUrl %>/part/XXXX</td>
                        <td>BHL part ID (where XXXX is the ID value)</td>
                    </tr>
                </tbody>
            </table>

        <h3>Output Types</h3>
        <p>
            By default, the query interface will (if possible) redirect to the 
            Biodiversity Heritage Library page containing the citation described 
            by the query arguments.  If more than one possible citation is found, 
            the query interface redirects to a page from which the appropriate 
            citation can be selected.
        </p>
        <p>
            There are several additional ways that results from the query interface 
            can be returned: JSON, XML, and HTML.  To get the citation data in 
            those formats, add the "format" argument to the end of the OpenURL 
            query with one of the following values: "json", "xml", "html".  If
            results are returned as JSON, a callback function may also be
            specified by adding a "callback" argument to the query.
        </p>
        <h3>Examples</h3>
        <p>
            Following are some example queries and responses.
        </p>
            <div style="font-weight:bold">OpenUrl 0.1</div>
            <p>
                The following query references Samual W. Williston, <i>Manual of North American Diptera</i> (New Haven :J.T. Hathaway) 16.
            </p>
            <code>
                /openurl?<br />
                &amp;genre=book<br />
                &amp;title=Manual+of+North+American+Diptera<br />
                &amp;aufirst=Samuel<br />
                &amp;aulast=Williston<br />
                &amp;date=1908<br />
                &amp;spage=16<br />
            </code>
            <p><a href="/openurl?genre=book&amp;title=Manual+of+North+American+Diptera&amp;aufirst=Samuel&amp;aulast=Williston&amp;date=1908&amp;spage=16" title="Example">Click here to try it</a></p>
            <div style="font-weight:bold">OpenURL 1.0</div>
            <p>
                Here's the same query, using the OpenURL 1.0 specification.
            </p>
            <code>
            /openurl?url_ver=Z39.88-2004<br />
            &amp;ctx_ver=Z39.88-2004<br />
            &amp;rft_val_fmt=info%3Aofi%2Ffmt%3Akev%3Amtx%3Abook<br />
            &amp;rft.genre=book<br />
            &amp;rft.btitle=Manual+of+North+American+Diptera<br />
            &amp;rft.aufirst=Samuel<br />
            &amp;rft.aulast=Williston<br />
            &amp;rft.date=1908<br />
            &amp;rft.spage=16<br />
            </code>
            <p><a href="/openurl?url_ver=Z39.88-2004&amp;ctx_ver=Z39.88-2004&amp;rft_val_fmt=info%3Aofi%2Ffmt%3Akev%3Amtx%3Abook&amp;rft.genre=book&amp;rft.btitle=Manual+of+North+American+Diptera&amp;rft.aufirst=Samuel&amp;rft.aulast=Williston&amp;rft.date=1908&amp;rft.spage=16" title="Example">Click here to try it</a></p>
            <div style="font-weight:bold">Response in JSON</div>
            <p>
                To receive the response in JSON, append "&amp;format=json" to the end of the query, as shown here.
                This example shows the OpenURL 0.1 query syntax, but it will also work for OpenURL 1.0 queries.
            </p>
            <code>
                /openurl?<br />
                &amp;genre=book<br />
                &amp;title=Manual+of+North+American+Diptera<br />
                &amp;aufirst=Samuel<br />
                &amp;aulast=Williston<br />
                &amp;date=1908<br />
                &amp;spage=16<br />
                <span style="color:Blue">&amp;format=json</span><br />
            </code>
            <p><a href="/openurl?genre=book&amp;title=Manual+of+North+American+Diptera&amp;aufirst=Samuel&amp;aulast=Williston&amp;date=1908&amp;spage=16&amp;format=json" title="Example">Click here to try it</a></p>
            <div style="font-weight:bold">Response in JSON (with a callback function)</div>
            <p>
                To receive the response in JSON and specify a callback function, append 
                "&amp;format=json&amp;callback=&lt;functionname&gt;" to the end of the query, as shown here. This
                example shows the OpenURL 0.1 query syntax, but it will also work for OpenURL 1.0 queries.
            </p>
            <code>
                /openurl?<br />
                &amp;genre=book<br />
                &amp;title=Manual+of+North+American+Diptera<br />
                &amp;aufirst=Samuel<br />
                &amp;aulast=Williston<br />
                &amp;date=1908<br />
                &amp;spage=16<br />
                <span style="color:Blue">&amp;format=json</span><br />
                <span style="color:Blue">&amp;callback=functionname</span><br />
            </code>
            <p><a href="/openurl?genre=book&amp;title=Manual+of+North+American+Diptera&amp;aufirst=Samuel&amp;aulast=Williston&amp;date=1908&amp;spage=16&amp;format=json&amp;callback=functionname" title="Example">Click here to try it</a></p>
            <div style="font-weight:bold">Response in XML</div>
            <p>
                To receive the response in XML, append "&amp;format=xml" to the end of the query, as shown here.
                Again, this will work for both OpenURL 0.1 and OpenURL 1.0 queries.
            </p>
            <code>
                /openurl?<br />
                &amp;genre=book<br />
                &amp;title=Manual+of+North+American+Diptera<br />
                &amp;aufirst=Samuel<br />
                &amp;aulast=Williston<br />
                &amp;date=1908<br />
                &amp;spage=16<br />
                <span style="color:Blue">&amp;format=xml</span><br />
            </code>
            <p><a href="/openurl?genre=book&amp;title=Manual+of+North+American+Diptera&amp;aufirst=Samuel&amp;aulast=Williston&amp;date=1908&amp;spage=16&amp;format=xml" title="Example">Click here to try it</a></p>
            <div style="font-weight:bold">Response in HTML</div>
            <p>
                To receive the response as an HTML fragment, append "&amp;format=html" to the end of the query, as shown here.
                As with JSON and XML responses, this will work for both OpenURL 0.1 and OpenURL 1.0 queries.
            </p>
            <code>
                /openurl?<br />
                &amp;genre=book<br />
                &amp;title=Manual+of+North+American+Diptera<br />
                &amp;aufirst=Samuel<br />
                &amp;aulast=Williston<br />
                &amp;date=1908<br />
                &amp;spage=16<br />
                <span style="color:Blue">&amp;format=html</span><br />
            </code>
            <p><a href="/openurl?genre=book&amp;title=Manual+of+North+American+Diptera&amp;aufirst=Samuel&amp;aulast=Williston&amp;date=1908&amp;spage=16&amp;format=html" title="Example">Click here to try it</a></p>


</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">
</asp:Content>
