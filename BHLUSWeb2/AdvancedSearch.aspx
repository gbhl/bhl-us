<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdvancedSearch.aspx.cs" Inherits="MOBOT.BHL.Web2.AdvancedSearch" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
        <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.10/themes/base/jquery-ui.css" type="text/css" rel="stylesheet"/>
        <link rel="stylesheet" href="/css/ui.jqgrid.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="page-title"><h1 class="column-wrap">Advanced Search</h1></div>
<div id="content" class="column-wrap clearfix">
    <section class="search">
        <div id="tabs">
            <ul>
            <li><a href="#divBookSearch" title="Books"><span>Books/Journals</span></a></li>
            <li><a href="#divArticleSearch" title="Articles"><span>Articles/Chapters</span></a></li>
            <li><a href="#divAuthorSearch" title="Authors"><span>Authors</span></a></li>
            <li><a href="#divSubjectSearch" title="Subjects"><span>Subjects</span></a></li>
            <li><a href="#divNameSearch" title="Names"><span>Scientific Names</span></a></li>
            </ul>
            <div id="divBookSearch">

                <ol>
                <li>
                <label class="caption" for="txtBookTitle">Title:</label><asp:TextBox ID="txtBookTitle" ClientIDMode="Static" runat="server" Width="400"></asp:TextBox>
                </li>
                <li>
                <label class="caption" for="txtBookAuthorLastName">Author Last Name:</label><asp:TextBox ID="txtBookAuthorLastName" ClientIDMode="Static" runat="server" Width="200"></asp:TextBox>
                </li>
                <li>
                <label class="caption" for="txtBookVolume">Volume:</label><asp:TextBox ID="txtBookVolume" ClientIDMode="Static" runat="server"></asp:TextBox>
                </li>
                <li>
                <label class="caption" for="txtBookEdition">Edition:</label><asp:TextBox ID="txtBookEdition" ClientIDMode="Static" runat="server"></asp:TextBox>
                </li>
                <li>
                <label class="caption" for="txtBookYear">Year (YYYY):</label><asp:TextBox ID="txtBookYear" ClientIDMode="Static" runat="server"></asp:TextBox><label id="lblBookYearError" for="txtBookYear" class="ErrorText" style="display:none"> Please specify a 4-digit Year.</label>
                </li>
                <li>
                <label class="caption" for="txtBookSubject">Subject:</label><asp:TextBox ID="txtBookSubject" ClientIDMode="Static" runat="server" Width="200"></asp:TextBox>
                </li>
                <li>
                <label class="caption" for="ddlBookLanguage">Language:</label><asp:DropDownList ID="ddlBookLanguage" ClientIDMode="Static" runat="server" DataTextField="LanguageName" DataValueField="LanguageCode"></asp:DropDownList>
                </li>
                <li>
                <label class="caption" for="ddlBookCollection">Collection:</label><asp:DropDownList ID="ddlBookCollection" ClientIDMode="Static" runat="server" DataTextField="CollectionName" DataValueField="CollectionID"></asp:DropDownList>
                </li>
                </ol>
                <div style="margin-left: 152px;">
                    <div id="lblBookError" class="ErrorText" style="display:none;">Please specify Title or Author Last Name, or select a Collection.</div>
                <div style="width: 13em; margin-right: 1em; margin-top: -1em; margin-bottom: 20px;">
                    <asp:Button runat="server" ID="btnSearchTitle" Text="Search" onclick="btnSearchTitle_Click" />
                </div>
                </div>

            </div>
            <div id="divArticleSearch">

                <ol>
                <li>
                <label class="caption" for="txtArticleTitle">Article/Chapter Title:</label><asp:TextBox ID="txtArticleTitle" ClientIDMode="Static" runat="server" Width="400"></asp:TextBox>
                </li>
                <li>
                <label class="caption" for="txtJournalTitle">Journal/Book Title:</label><asp:TextBox ID="txtJournalTitle" ClientIDMode="Static" runat="server" Width="400"></asp:TextBox>
                </li>
                <li>
                <label class="caption" for="txtArticleAuthor">Author Last Name:</label><asp:TextBox ID="txtArticleAuthor" ClientIDMode="Static" runat="server"></asp:TextBox>
                </li>
                <li>
                <label class="caption" for="txtArticleYear">Year:</label><asp:TextBox ID="txtArticleYear" ClientIDMode="Static" runat="server"></asp:TextBox>
                <br />
                <label id="lblArticleYearError" for="txtArticleYear" class="ErrorText" style="display:none; margin-left: 150px; padding-top: 10px;"> Please specify a 4-digit Year.</label>
                </li>
                </ol>
                <div style="margin-left: 152px;">
                    <div id="lblArticleError" class="ErrorText" style="display:none;">Please specify a Article/Chapter Title.</div>
                    <div style="width: 13em; margin-right: 1em; margin-top: -1em; margin-bottom: 20px;">
                    <asp:Button runat="server" ID="btnSearchArticle" Text="Search" onclick="btnSearchArticle_Click" />
                    </div>
                </div>

            </div>
            <div id="divAuthorSearch">

                <ol>
                <li>
                <label class="caption" for="txtAuthorLastName">Author Last Name:</label><asp:TextBox ID="txtAuthorLastName" ClientIDMode="Static" runat="server" Width="200"></asp:TextBox>
                </li>
                <li>
                <label class="caption" for="txtAuthorFirstName">Author First Name:</label><asp:TextBox ID="txtAuthorFirstName" ClientIDMode="Static" runat="server" Width="200"></asp:TextBox>
                </li>
                <div style="background: #E6ECF1; border-radius: 5px; -webkit-border-radius: 5px; padding: 10px 0 17px; margin: 10px;">
                <li style="margin: 0px;">
                <label class="caption" for="txtAuthorCorporateName">- or -</label>
                </li>
                <li style="height:0px">&nbsp;</li>
                <li>
                <label class="caption" for="txtAuthorCorporateName">Corporation/Meeting:</label><asp:TextBox ID="txtAuthorCorporateName" ClientIDMode="Static" runat="server" Width="370" style="width: 370px !important"></asp:TextBox>
                </li>
                </div>
                </ol>
                <div style="margin-left: 152px;">
                    <div id="lblAuthorError" class="ErrorText" style="display:none;">Please specify Author Last Name or Corporate/Meeting Name.</div>
                <div style="width: 13em; margin-bottom: 20px; margin-top: -0.5em">
                    <asp:Button runat="server" ID="btnSearchAuthor" Text="Search" onclick="btnSearchAuthor_Click" />
                </div>
                </div>

            </div>
            <div id="divSubjectSearch">

                <ol style="margin-bottom: 50px;">
                <li>
                <label class="caption" for="txtSubject">Subject:</label><asp:TextBox ID="txtSubject" ClientIDMode="Static" runat="server" Width="200" class="inlinetextbox"></asp:TextBox>
                &nbsp;<asp:Button runat="server" ID="btnSearchSubject" Text="Search" onclick="btnSearchSubject_Click" class="inlinebutton" style="margin-left: 10px;" />
                </li>
                </ol>
                <div style="margin-left: 152px; margin-top: -2em; margin-bottom: 20px;">
                    <div id="lblSubjectError" class="ErrorText" style="display:none;">Please specify a Subject.</div>
                </div>

            </div>
            <div id="divNameSearch">

                <ol style="margin-bottom: 50px;">
                <li>
                <label class="caption" for="txtName">Scientific Name:</label><asp:TextBox ID="txtName" ClientIDMode="Static" runat="server" Width="250" class="inlinetextbox"></asp:TextBox>&nbsp;<asp:Button runat="server" ID="btnSearchName" Text="Search" onclick="btnSearchName_Click" class="inlinebutton" />
                </li>
                </ol>
                <div style="margin-left: 152px; margin-top: -2em; ">
                    <div id="lblNameError" class="ErrorText" style="display:none;">Please specify a Name.</div>
                </div>
                <div class="searchinfoblock">
                    <p>Biodiversity Heritage Library uses Global Names Architecture's Global Names Recognition and Discovery (GNRD), a taxonomic name recognition algorithm, to search through all of the texts digitized in BHL and extract the scientific names.  Searching for a name will return a list of all the individual pages where that name occurs.  NOTE: The text is currently uncorrected text automatically generated from Optical Character Recognition (OCR) programs and is of variable quality for each scanned book.</p>
                    <p>Here are some examples of scientific names that you can find in the Biodiversity Heritage Library:</p>
                    <table width="100%" cellpadding="3" cellspacing="3" role="presentation">
                        <tr>
                            <td><b>Corn</b>: <a href="/name/zea_mays" title="Name">Zea mays</a></td>
                            <td><b>Great white shark</b>: <a href="/name/carcharodon_carcharias" title="Name">Carcharodon carcharias</a></td>
                        </tr>
                        <tr>
                            <td><b>Tiger</b>: <a href="/name/panthera_tigris" title="Name">Panthera tigris</a></td>
                            <td><b>African elephant</b>: <a href="/name/loxodonta_africana" title="Name">Loxodonta africana</a></td>
                        </tr>
                        <tr>
                            <td><b>Monarch butterfly</b>: <a href="/name/danaus_plexippus" title="Name">Danaus plexippus</a></td>
                            <td><b>Domestic dog</b>: <a href="/name/canis_lupus_familiaris" title="Name">Canis lupus familiaris</a></td>
                        </tr>
                        <tr>
                            <td><b>Strawberry</b>: <a href="/name/fragaria_magna" title="Name">Fragaria magna</a></td>
                            <td><b>Bottlenose Dolphin</b>: <a href="/name/Tursiops_truncatus" title="Name">Tursiops truncatus</a></td>
                        </tr>
                        <tr>
                            <td><b>Annual meadow grass</b>: <a href="/name/poa_annua" title="Name">Poa annua</a></td>
                            <td><b>Blue whale</b>: <a href="/name/balaenoptera_musculus" title="Name">Balaenoptera musculus</a></td>
                        </tr>
                    </table>
                </div>
                <br />

            </div>
        </div>
    </section>
    <aside>
      <uc:FeatureBox ID="FeatureBox1" runat="server" FeatureType="support"></uc:FeatureBox>
      <uc:FeatureBox ID="FeatureBox2" runat="server" FeatureType="collection"></uc:FeatureBox>
    </aside>		
</div>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContentPlaceHolder" runat="server">

        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" language="javascript"></script>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.4/jquery-ui.min.js" language="javascript"></script>
        <script type="text/javascript">
            // Set up tabs
            $(document).ready(function () {
                $("#tabs").tabs();
                $("#tabs").tabs('select', '#<%= startTabDiv %>');    // Default to a particular tab
            });

            // Validate book search
            $("#<%= btnSearchTitle.ClientID %>").click(function () {
                var isValid = true;

                if (!validateYear($("#<%= txtBookYear.ClientID %>").val())) {
                    $("#lblBookYearError").show();
                    isValid = false;
                }
                else {
                    $("#lblBookYearError").hide();
                }

                if ($.trim($("#<%= txtBookTitle.ClientID %>").val()) == '' &&
            $.trim($("#<%= txtBookAuthorLastName.ClientID %>").val()) == '' &&
            $("#<%= ddlBookCollection.ClientID %>").val() == '') {
                    $("#lblBookError").show();
                    isValid = false;
                }
                else {
                    $("#lblBookError").hide();
                }

                return isValid;
            });

            // Validate article search
            $("#<%= btnSearchArticle.ClientID %>").click(function () {
                var isValid = true;

                if (!validateYear($("#<%= txtArticleYear.ClientID %>").val())) {
                    $("#lblArticleYearError").css('display', 'block');
                    isValid = false;
                }
                else {
                    $("#lblArticleYearError").hide();
                }

                if ($.trim($("#<%= txtArticleTitle.ClientID %>").val()) == '') {
                    $("#lblArticleError").show();
                    isValid = false;
                }
                else {
                    $("#lblArticleError").hide();
                }

                return isValid;
            });

            function validateYear(value) {
                var re = new RegExp("^[0-9]{4}$");
                var m = re.exec(value);
                if (m == null && value != "") return false;
                return true;
            }

            // Validate author search
            $("#<%= btnSearchAuthor.ClientID %>").click(function () {
                if ($.trim($("#<%= txtAuthorLastName.ClientID %>").val()) == '' && $.trim($("#<%= txtAuthorCorporateName.ClientID %>").val()) == '') {
                    $("#lblAuthorError").show();
                    return false;
                }
                else {
                    return true;
                }
            });

            // Validate subject search
            $("#<%= btnSearchSubject.ClientID %>").click(function () {
                if ($.trim($("#<%= txtSubject.ClientID %>").val()) == '') {
                    $("#lblSubjectError").show();
                    return false;
                }
                else {
                    return true;
                }
            });

            // Validate name search
            $("#<%= btnSearchName.ClientID %>").click(function () {
                if ($.trim($("#<%= txtName.ClientID %>").val()) == '') {
                    $("#lblNameError").show();
                    return false;
                }
                else {
                    return true;
                }
            });

            // Search books on Enter keypress
            $("#<%= txtBookTitle.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchTitle.ClientID %>").click();
            });
            $("#<%= txtBookAuthorLastName.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchTitle.ClientID %>").click();
            });
            $("#<%= txtBookVolume.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchTitle.ClientID %>").click();
            });
            $("#<%= txtBookEdition.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchTitle.ClientID %>").click();
            });
            $("#<%= txtBookYear.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchTitle.ClientID %>").click();
            });
            $("#<%= txtBookSubject.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchTitle.ClientID %>").click();
            });

            // Search articles on Enter keypress
            $("#<%= txtArticleTitle.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchArticle.ClientID %>").click();
            });
            $("#<%= txtJournalTitle.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchArticle.ClientID %>").click();
            });
            $("#<%= txtArticleAuthor.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchArticle.ClientID %>").click();
            });
            $("#<%= txtArticleYear.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchArticle.ClientID %>").click();
            });

            // Search authors on Enter keypress
            $("#<%= txtAuthorLastName.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchAuthor.ClientID %>").click();
            });
            $("#<%= txtAuthorFirstName.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchAuthor.ClientID %>").click();
            });
            $("#<%= txtAuthorCorporateName.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchAuthor.ClientID %>").click();
            });

            // Search subjects on Enter keypress
            $("#<%= txtSubject.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchSubject.ClientID %>").click();
            });

            // Search names on Enter keypress
            $("#<%= txtName.ClientID %>").keypress(function (event) {
                if (event.keyCode == '13') $("#<%= btnSearchName.ClientID %>").click();
            });

</script>
</asp:Content>
