<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dlsearch.aspx.cs" Inherits="MOBOT.BHL.Web2.dlsearch" %>
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeatureBox" Src="~/controls/ucFeatureBox.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderIncludesPlaceHolder" runat="server">
        <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.10/themes/base/jquery-ui.css" type="text/css" rel="stylesheet"/>

        <link rel="stylesheet" href="/css/ui.jqgrid.css" />
   <style>
       /* label.caption { float: left; width: 12em; margin-right: 1em; text-align: right; }
        label.header { float: left; width: 12em; margin-right: 1em; text-align: right; font-weight:bold; }
        fieldset { margin: 1.5em 0 1.5em 0; padding: 0; }
        fieldset li { padding-bottom: 0.5em; }
        fieldset ol { padding: 1em 1em 1em 1em; list-style: none; }
        li { padding-bottom: 0.5em; }
        ol { padding: 0em 0em 0em 1em; list-style: none; }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
    <uc:NavBar runat="server" />
    <div id="page-title">    
    <h1 class="column-wrap">
      Search <a class="headinglink" href="/collection/darwinlibrary" title="Darwin's Library Homepage">Charles Darwin's Library</a>
    </h1>
</div>

<div id="content" class="column-wrap clearfix">

    <section class="search">
            <div id="tabs">
                <ul>
                    <li><a href="#divBookSearch" title="Books"><span>Books/Journals</span></a></li>
                    <li><a href="#divAnnotationSearch" title="Annotations"><span>Annotations</span></a></li>
                </ul>
                <div id="divBookSearch">
                    <!--<fieldset>-->
                    <ol>
                    <li>
                    <label class="caption">Title:</label><asp:TextBox ID="txtBookTitle" runat="server" Width="400"></asp:TextBox>
                    </li>
                    <li>
                    <label class="caption">Author Last Name:</label><asp:TextBox ID="txtBookAuthorLastName" runat="server" Width="200"></asp:TextBox>
                    </li>
                    <li>
                    <label class="caption">Volume:</label><asp:TextBox ID="txtBookVolume" runat="server"></asp:TextBox>
                    </li>
                    <li>
                    <label class="caption">Edition:</label><asp:TextBox ID="txtBookEdition" runat="server"></asp:TextBox>
                    </li>
                    <li>
                    <label class="caption">Year (YYYY):</label><asp:TextBox ID="txtBookYear" runat="server"></asp:TextBox><label id="lblBookYearError" class="ErrorText" style="display:none"> Please specify a 4-digit Year.</label>
                    </li>
                    <li>
                    <label class="caption">&nbsp;</label><label id="lblBookError" class="ErrorText" style="display:none">Please specify Title or Author Last Name.</label>
                    </li>
                    </ol>
                    <!--</fieldset>-->
                    <div style="width: 13em; margin-right: 1em; text-align: right;">
                        <asp:Button runat="server" ID="btnSearchTitle" Text="Search" onclick="btnSearchTitle_Click" />
                    </div>
                </div>
                <div id="divAnnotationSearch">
                    <!--<fieldset>-->
                    <ol>
                    <li>
                    <label class="header">Search For&nbsp;</label><label>&nbsp;</label>
                    </li>
                    <li>
                    <label class="caption">Word/Phrase:</label><asp:TextBox ID="txtAnnotationText" runat="server" Width="200"></asp:TextBox><label id="lblAnnotationTextError" class="ErrorText" style="display:none"> Please specify a word or phrase for which to search.</label>
                    <li>
                    <label class="caption">&nbsp;</label><label>&nbsp;</label>
                    </li>
                    <li>
                    <label class="header">Search In&nbsp;</label><label>&nbsp;</label>
                    </li>
                    <li>
                    <label class="caption">Title:</label><asp:TextBox ID="txtAnnoTitle" runat="server" Width="400"></asp:TextBox>
                    </li>
                    <li>
                    <label class="caption">Author Last Name:</label><asp:TextBox ID="txtAnnoAuthorLastName" runat="server" Width="200"></asp:TextBox>
                    </li>
                    <li>
                    <label class="caption">Volume:</label><asp:TextBox ID="txtAnnoVolume" runat="server"></asp:TextBox>
                    </li>
                    <li>
                    <label class="caption">Edition:</label><asp:TextBox ID="txtAnnoEdition" runat="server"></asp:TextBox>
                    </li>
                    <li>
                    <label class="caption">Year (YYYY):</label><asp:TextBox ID="txtAnnoYear" runat="server"></asp:TextBox><label id="lblAnnoYearError" class="ErrorText" style="display:none"> Please specify a 4-digit Year.</label>
                    </li>
                    <li>
                    <label class="caption">&nbsp;</label><label id="lblAnnotationError" class="ErrorText" style="display:none">Please specify Title or Author Last Name.</label>
                    </li>
                    </ol>
                    <!--</fieldset>-->
                    <div style="width: 13em; margin-right: 1em; text-align: right;">
                        <asp:Button runat="server" ID="btnSearchAnnotation" Text="Search" onclick="btnSearchAnnotation_Click" />
                    </div>
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
                $.trim($("#<%= txtBookAuthorLastName.ClientID %>").val()) == '') {
                $("#lblBookError").show();
                isValid = false;
            }
            else {
                $("#lblBookError").hide();
            }

            return isValid;
        });

        // Validate annotation search
        $("#<%= btnSearchAnnotation.ClientID %>").click(function () {
            var isValid = true;

            if ($.trim($("#<%= txtAnnotationText.ClientID %>").val()) == '') {
                $("#lblAnnotationTextError").show();
                isValid = false;
            }
            else {
                $("#lblAnnotationTextError").hide();
            }

            if (!validateYear($("#<%= txtAnnoYear.ClientID %>").val())) {
                $("#lblAnnoYearError").show();
                isValid = false;
            }
            else {
                $("#lblAnnoYearError").hide();
            }

            if ($.trim($("#<%= txtAnnoTitle.ClientID %>").val()) == '' &&
                $.trim($("#<%= txtAnnoAuthorLastName.ClientID %>").val()) == '' &&
                ($.trim($("#<%= txtAnnoVolume.ClientID %>").val()) != '' ||
                $.trim($("#<%= txtAnnoEdition.ClientID %>").val()) != '' ||
                $.trim($("#<%= txtAnnoYear.ClientID %>").val()) != '')
                ) {
                $("#lblAnnotationError").show();
                isValid = false;
            }
            else {
                $("#lblAnnotationError").hide();
            }

            return isValid;
        });

        // Year validation
        function validateYear(value) {
            var re = new RegExp("^[0-9]{4}$");
            var m = re.exec(value);
            if (m == null && value != "") return false;
            return true;
        }

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

        // Search annotations on Enter keypress
        $("#<%= txtAnnotationText.ClientID %>").keypress(function (event) {
            if (event.keyCode == '13') $("#<%= btnSearchAnnotation.ClientID %>").click();
        });
        $("#<%= txtAnnoTitle.ClientID %>").keypress(function (event) {
            if (event.keyCode == '13') $("#<%= btnSearchAnnotation.ClientID %>").click();
        });
        $("#<%= txtAnnoAuthorLastName.ClientID %>").keypress(function (event) {
            if (event.keyCode == '13') $("#<%= btnSearchAnnotation.ClientID %>").click();
        });
        $("#<%= txtAnnoVolume.ClientID %>").keypress(function (event) {
            if (event.keyCode == '13') $("#<%= btnSearchAnnotation.ClientID %>").click();
        });
        $("#<%= txtAnnoEdition.ClientID %>").keypress(function (event) {
            if (event.keyCode == '13') $("#<%= btnSearchAnnotation.ClientID %>").click();
        });
        $("#<%= txtAnnoYear.ClientID %>").keypress(function (event) {
            if (event.keyCode == '13') $("#<%= btnSearchAnnotation.ClientID %>").click();
        });

    </script>
</asp:Content>