<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="MOBOT.BHL.Web2.Feedback" %>
    
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="page-title">
    <h1 class="column-wrap">Questions / Comments / Scanning Requests</h1>
</div>

<div id="content" class="column-wrap clearfix">
    <section class="search" style="width:745px">
        <div class="ui-tabs"><div class="ui-tabs-panel">
        <div runat="server" id="divSubmit">

            <div style="display:none; margin-top:15px; margin-left:11px; margin-right:11px;" id="rcvdMsg" ClientIDMode="static" runat="server">
                <div style="background-color:#3E90C8;color:white; padding-left:22px; padding-top:5px; padding-bottom:5px; padding-right:22px;">Thank you for submitting your feedback.  If necessary, a staff member will contact you shortly.</div>
            </div>

            <div class="hidden">Leave this empty<asp:TextBox ID="fooTextBox" runat="server"></asp:TextBox></div>

            <p></p>
            <div>
                <p id="spanErrorText" class="ErrorText" style="margin-left:160px;display:block !important"></p>
            </div>
            <div style="margin-bottom:10px;">
                <label class="caption" for="nameTextBox">Name:</label>
                <asp:TextBox ID="nameTextBox" ClientIDMode="Static" runat="server" Width="300px" class="field"></asp:TextBox><span style="font-style:italic"> (optional)</span>
            </div>
            <div style="margin-bottom:30px;">
                <label class="caption" for="emailTextBox">Email:</label>
                <asp:TextBox ID="emailTextBox" ClientIDMode="Static" runat="server" Width="300px"></asp:TextBox><span style="font-style:italic"> (optional)</span>
            </div>
            <div style="margin-bottom:30px;">
                <label class="caption" for="rdoSubject">Subject:</label>
                <input type="radio" runat="server" name="rdoSubject" ID="subjectTech" ClientIDMode="Static" value="22" checked />Technical Issue
                <input type="radio" runat="server" name="rdoSubject" ID="subjectBibIssue" ClientIDMode="Static" value="55"  />Bibliographic Issue
                <input type="radio" runat="server" name="rdoSubject" ID="subjectSuggest" ClientIDMode="Static" value="36"  />Suggestion
                <input type="radio" runat="server" name="rdoSubject" ID="subjectScanReq" ClientIDMode="Static" value="60"  />Scanning Request
            </div>

		    <!-- Feedback Form -->
		    <div id="FeedbackForm" style="display:none; margin-top:5px;">
                <div><label class="caption">&nbsp;</label><p style="font-weight:bold">Try our <a href="http://biodivlib.wikispaces.com/Help" target="_blank" title="Help">Help</a> page for answers to common questions.</p></div>
                <div style="margin-bottom:10px;">
                    <label class="caption" for="commentTextBox">Comment:</label>
                    <asp:TextBox ID="commentTextBox" ClientIDMode="Static" runat="server" Height="100px" Width="375" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
		    <!-- End Feedback Form -->

		    <!-- Scanning Request Form -->
		    <div id="ScanRequestForm" style="display:none; margin-top:5px">
                <div><label class="caption">&nbsp;</label><p style="font-weight:bold">Please see our <a href="http://biodivlib.wikispaces.com/Guidelines%20for%20Submitting%20Scanning%20Requests" target="_blank" title="Guidelines">guidelines</a> for submitting requests.</p></div>
                <div><label class="caption">&nbsp;</label><p style="font-weight:bold">Search for your title in <a href="http://www.worldcat.org/" target="_blank" title="WorldCat">WorldCat</a> (recommended)</p></div>
                <div style="margin-bottom:10px;">
                    <label class="caption" for="srOCLCTextBox">OCLC:</label>
                    <asp:TextBox ID="srOCLCTextBox" ClientIDMode="Static" runat="server" Width="200px" MaxLength="30"></asp:TextBox><span style="font-style:italic"> (optional)</span>
                </div>
                <div style="margin-bottom:10px;">
                    <label class="caption" for="srTitleTextBox">Book Title:</label>
                    <asp:TextBox ID="srTitleTextBox" ClientIDMode="Static" runat="server" Width="400px" MaxLength="500"></asp:TextBox>
                </div>
                <div style="margin-bottom:10px;">
                    <label class="caption" for="srYearTextBox">Year:</label>
                    <asp:TextBox ID="srYearTextBox" ClientIDMode="Static" runat="server" Width="50px" MaxLength="20" style="width: 50px !important"></asp:TextBox><span style="font-style:italic"> (For © compliance, must be prior to <b>1923</b>)</span>
                </div>
                <div style="margin-bottom:10px;">
                    <label class="caption" for="typeList">Subject:</label>
                    <input type="radio" runat="server" name="rdoType" ID="typeBook" ClientIDMode="Static" value="Book" checked />Book
                    <input type="radio" runat="server" name="rdoType" ID="typeJournal" ClientIDMode="Static" value="Journal"  />Journal
                    <input type="radio" runat="server" name="rdoType" ID="typeUnsure" ClientIDMode="Static" value="Not Sure"  />Not Sure
                </div>
                <div style="margin-bottom:10px;">
                    <label class="caption" for="srVolumeTextBox">Volume:</label>
                    <asp:TextBox ID="srVolumeTextBox" ClientIDMode="Static" runat="server" Width="200px" MaxLength="100" Text="All volumes" class="inlinetextbox"></asp:TextBox><span style="font-style:italic"> (journals only)</span>
                </div>
                <div style="margin-bottom:10px;">
                    <label class="caption" for="srEditionTextBox">Edition:</label>
                    <asp:TextBox ID="srEditionTextBox" ClientIDMode="Static" runat="server" Width="200px" MaxLength="100"></asp:TextBox><span style="font-style:italic"> (optional)</span>
                </div>
                <div style="margin-bottom:10px;">
                    <label class="caption" for="srISBNTextBox">ISBN:</label>
                    <asp:TextBox ID="srISBNTextBox" ClientIDMode="Static" runat="server" Width="200px" MaxLength="30" class="inlinetextbox"></asp:TextBox><span style="font-style:italic"> (optional)</span>
                </div>
                <div style="margin-bottom:10px;">
                    <label class="caption" for="srISSNTextBox">ISSN:</label>
                    <asp:TextBox ID="srISSNTextBox" ClientIDMode="Static" runat="server" Width="200px" MaxLength="30" class="inlinetextbox"></asp:TextBox><span style="font-style:italic"> (optional)</span>
                </div>
                <div style="margin-bottom:10px;">
                    <label class="caption" for="srAuthorTextBox">Author:</label>
                    <asp:TextBox ID="srAuthorTextBox" ClientIDMode="Static" runat="server" Width="400px" MaxLength="200"></asp:TextBox><span style="font-style:italic"> (optional)</span>
                </div>
                <div style="margin-bottom:10px;">
                    <label class="caption" for="srPublisherTextBox">Publisher:</label>
                    <asp:TextBox ID="srPublisherTextBox" ClientIDMode="Static" runat="server" Width="400px" MaxLength="200"></asp:TextBox><span style="font-style:italic"> (optional)</span>
                </div>
                <div style="margin-bottom:10px;">
                    <label class="caption" for="srLanguageList">Language:</label>
                    <asp:DropDownList ID="srLanguageList" ClientIDMode="Static" runat="server"></asp:DropDownList><span style="font-style:italic"> (optional)</span>
                </div>
                <div style="margin-bottom:10px;">
                    <label class="caption" for="srNoteTextBox">Note:</label>
                    <span style="float:right; padding-right:45px;"><span style="font-style:italic"> (optional)</span></span>
                    <asp:TextBox ID="srNoteTextBox" ClientIDMode="Static" runat="server" Height="100px" Width="400px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
		    <!-- End Scanning Request Form -->

            <div>
                <label class="caption">&nbsp;</label><asp:Button ID="submitButton" runat="server" Text="Submit" OnClientClick="submitClick();" OnClick="submitButton_Click" />
            </div>

        </div>
        <div runat="server" id="divConfirm" visible="false">

            <div style="text-align:center">
                <a href="" title="Return to original" id="lnkReturn" runat="server" style="font-size:11px;text-decoration:none">Return to the original page</a>
            </div>

            <div style="border-bottom:1px;border-style:solid;">
                <h3><asp:Literal runat="server" ID="litConfirmationSubject"></asp:Literal></h3>
                <div id="showhideDetailsDiv"><p><a id="showhideDetailsLink" href="#"><span id="showhideText">Show</span> Submitted Feedback</a></p></div>
                <div id="feedbackDetails" style="display:none">
                    <p><asp:Literal runat="server" ID="litConfirmationText"></asp:Literal></p>
                </div>
            </div>

            <div style="border-bottom:1px;border-style:solid;">
                <div style="float:left;margin:0;width:50%">
                    <h3>Join Our Mailing List</h3>
                    <p>Sign up to receive the latest BHL news, content highlights, and promotions.</p>
                    <a class="featurebutton-home" title="Subscribe to BHL Newsletter" target="_blank" href="http://library.si.edu/bhl-newsletter-signup">Subscribe</a>
                </div>

                <div style="float:left;margin:0;width:50%">
                    <h3>Help Support <span>BHL</span></h3>
                    <p>BHL depends on the financial support of its patrons. Help us keep BHL alive!</p>
                    <a class="featurebutton-home" title="Donate" target="_blank" href="http://library.si.edu/donate-bhl">Donate</a>
                </div>

                <div>&nbsp;</div>
            </div>

            <div>
                <p>Thank you for your feedback! BHL is voluntarily staffed by our Partner Libraries and we are limited in our ability to respond personally to each contact with our patrons. We appreciate your patience. A BHL staff member may contact you if we require further information.</p>
            </div>
        </div>

		<br />
		<asp:ValidationSummary ID="validationSummary" runat="server" />
		<asp:Panel ID="errorPanel" runat="server" Visible="false">
			<asp:Label ID="errorLabel" runat="server" ForeColor="red" Text="Label"></asp:Label>
		</asp:Panel>
    </section>
    <aside></aside>
</div>
</asp:Content>

<asp:content id="scriptContent" contentplaceholderid="scriptContentPlaceHolder" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        if (!location.hash || location.hash == "#/comments") {
            showFeedbackForm();
        } else {
            showScanReqForm();
        }
    });    

    $("#subjectTech").click(function () { showFeedbackForm(); });
    $("#subjectBibIssue").click(function () { showFeedbackForm(); });
    $("#subjectSuggest").click(function () { showFeedbackForm(); });
    $("#subjectScanReq").click(function () { showScanReqForm(); });

    function showFeedbackForm() {
        $("#spanErrorText").html("");
        var divComments = $("#FeedbackForm");
        var divRequest = $("#ScanRequestForm");
        divComments.show();
        divRequest.hide();
        location.hash = "#/comments";
    }

    function showScanReqForm() {
        $("#spanErrorText").html("");
        var divComments = $("#FeedbackForm");
        var divRequest = $("#ScanRequestForm");
        divComments.hide();
        divRequest.show();
        $("#subjectScanReq").attr("checked", true);
        location.hash = "#/request";
    }

    var formSubmit = false;

    function submitClick() {
        formSubmit = true;
    }

    $("form").submit(function () {
        if (formSubmit) {
            formSubmit = false;
            return validateForm();
        }
    });

    function validateForm() {
        var errMsg = "";

        if ($("#subjectScanReq").attr("checked")) {
            if ($("#srTitleTextBox").val() == "") errMsg = errMsg + "Please supply the book title.<br>";
            if ($("#srYearTextBox").val() == "") errMsg = errMsg + "Please supply the year of publication.<br>";
            if ($("#srVolumeTextBox").val() == "" && $("#typeJournal").is(":checked")) errMsg = errMsg + "Please supply the volume.<br>";
        }
        else {
            if ($("#commentTextBox").val() == "") errMsg = errMsg + "Please supply a comment.<br>";
        }

        if (errMsg != "") $("#spanErrorText").html(errMsg);
        return (errMsg == "");
    }

    $("#showhideDetailsDiv").click(function () {
        var showHide = $("#showhideText").text();
        if (showHide == "Show") showHide = "Hide"; else showHide = "Show";
        $("#showhideText").text(showHide);
        $("#feedbackDetails").toggle();
    });
</script>
</asp:Content>
