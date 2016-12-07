<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="MOBOT.BHL.Web2.Feedback" %>
    
<%@ Register TagPrefix="uc" TagName="NavBar" Src="~/controls/NavBar.ascx" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
<uc:NavBar runat="server" />
<div id="page-title">
    <h1 class="column-wrap">	Questions / Comments / Scanning Requests</h1>
</div>

<nav id="sub-nav">
    <div class="column-wrap">
        <div id="linkbar">
            <ul>
                <li class="comments first-child"><a class="active" href="#/comments">Question or Comment</a></li>
                <li class="request"><a href="#/request">Scanning Request</a></li>
            </ul>
        </div>

    </div>
</nav>

<div id="content" class="column-wrap clearfix">
    <section class="search">
        <div style="display:none; margin-top:15px; margin-left:11px; margin-right:11px;" id="rcvdMsg" ClientIDMode="static" runat="server">
            <div style="background-color:#3E90C8;color:white; padding-left:22px; padding-top:5px; padding-bottom:5px; padding-right:22px;">Thank you for submitting your feedback.  If necessary, a staff member will contact you shortly.</div>
        </div>

        <div class="hidden">Leave this empty<asp:TextBox ID="fooTextBox" runat="server"></asp:TextBox></div>

		<!-- Feedback Form -->
        <div class="ui-tabs">
		<div id="FeedbackForm" style="display:block" class="ui-tabs-panel">
        <ol>
            <div class="tabbed">
                <li><span id="spanErrorText" class="ErrorText" style="display:block !important"></span></li>
                <li>Try our <a href="http://biodivlib.wikispaces.com/Help" target="_blank" title="Help">Help</a> page for answers to common questions.</li>
                <li>* = required field</li>
            </div>
            <li>
                <label class="caption" for="nameTextBox">Name:</label><asp:TextBox ID="nameTextBox" ClientIDMode="Static" runat="server" Width="200px" class="field"></asp:TextBox>
            </li>
            <li>
                <label class="caption" for="emailTextBox">Email:</label><asp:TextBox ID="emailTextBox" ClientIDMode="Static" runat="server" Width="200px"></asp:TextBox>
            </li>
            <li>
                <label class="caption" for="ddlList">Subject:</label>
			    <asp:DropDownList runat="server" ID="ddlList" ClientIDMode="Static">
			        <asp:ListItem Text="" Value=""></asp:ListItem>
				    <asp:ListItem Text="Technical Issues" Value="22"></asp:ListItem>
					<asp:ListItem Text="Suggestion" Value="36"></asp:ListItem>
					<asp:ListItem Text="Bibliographic Issues" Value="55"></asp:ListItem>
			    </asp:DropDownList> *
            </li>
            <li>
                <label class="caption" for="commentTextBox">Comment:</label>
                <span style="float: right; padding-right: 30px;">*</span>
                <asp:TextBox ID="commentTextBox" ClientIDMode="Static" runat="server" Height="100px" Width="375" TextMode="MultiLine"></asp:TextBox>
            </li>
        </ol>
        <div class="tabbed">
            <asp:Button ID="submitButton" runat="server" Text="Submit" OnClientClick="return ValidateFeedbackForm();" OnClick="submitButton_Click" />
        </div>

        </div>
		</div>
		<!-- End Feedback Form -->
		<!-- Scanning Request Form -->
		
        <div class="ui-tabs">
		<div id="ScanRequestForm" style="display:none" class="ui-tabs-panel">
        <ol>
            <div class="tabbed">
                <li><span id="srSpanErrorText" class="ErrorText" style="display:block !important"></span></li>
                <li>Please see our <a href="http://biodivlib.wikispaces.com/Guidelines%20for%20Submitting%20Scanning%20Requests" target="_blank" title="Guidelines">guidelines</a> for submitting requests.</li>
                <li>* = required field</li>
            </div>
            <li>
                <label class="caption" for="srNameTextBox">Your Name:</label><asp:TextBox ID="srNameTextBox" ClientIDMode="Static" runat="server" Width="200px"></asp:TextBox> *
            </li>
            <li>
                <label class="caption" for="srEmailTextBox">Email:</label><asp:TextBox ID="srEmailTextBox" ClientIDMode="Static" runat="server" Width="200px"></asp:TextBox> *
            </li>
            <div class="tabbed">
                Search for your title in <a href="http://www.worldcat.org/" target="_blank" title="WorldCat">WorldCat</a> (recommended)
            </div>
            <li>
                <label class="caption" for="srOCLCTextBox">OCLC:</label><asp:TextBox ID="srOCLCTextBox" ClientIDMode="Static" runat="server" Width="200px" MaxLength="30"></asp:TextBox>
            </li>
            <li>
                <label class="caption" for="srTitleTextBox">Book Title:</label><asp:TextBox ID="srTitleTextBox" ClientIDMode="Static" runat="server" Width="400px" MaxLength="500"></asp:TextBox> *
            </li>
            <li>
                <label class="caption" for="srYearTextBox">Year:</label><asp:TextBox ID="srYearTextBox" ClientIDMode="Static" runat="server" Width="50px" MaxLength="20" style="width: 50px !important"></asp:TextBox> *
                &nbsp;&nbsp;&nbsp;
				(For © compliance, must be prior to <b>1923</b>)
            </li>
            <li>
                <label class="caption" for="srTypeList">Type:</label>
                <asp:DropDownList ID="srTypeList" runat="server" ClientIDMode="Static">
				    <asp:ListItem Value="Book" Text="Book" />
				    <asp:ListItem Value="Journal" Text="Journal" />
				    <asp:ListItem Value="Not Sure" Text="Not Sure" />
				</asp:DropDownList> *
            </li>
            <li>
                <label class="caption" for="srVolumeTextBox">Volume:</label><asp:TextBox ID="srVolumeTextBox" ClientIDMode="Static" runat="server" Width="200px" MaxLength="100" Text="All volumes" class="inlinetextbox"></asp:TextBox> * (journals only)
            </li>
            <li>
                <label class="caption" for="srEditionTextBox">Edition:</label><asp:TextBox ID="srEditionTextBox" ClientIDMode="Static" runat="server" Width="200px" MaxLength="100"></asp:TextBox>
            </li>
            <li>
                <label class="caption" for="srISBNTextBox">ISBN:</label><asp:TextBox ID="srISBNTextBox" ClientIDMode="Static" runat="server" Width="200px" MaxLength="30" class="inlinetextbox"></asp:TextBox>
            </li>
            <li>
                <label class="caption" for="srISSNTextBox">ISSN:</label><asp:TextBox ID="srISSNTextBox" ClientIDMode="Static" runat="server" Width="200px" MaxLength="30" class="inlinetextbox"></asp:TextBox>
            </li>
            <li>
                <label class="caption" for="srAuthorTextBox">Author:</label><asp:TextBox ID="srAuthorTextBox" ClientIDMode="Static" runat="server" Width="400px" MaxLength="200"></asp:TextBox>
            </li>
            <li>
                <label class="caption" for="srPublisherTextBox">Publisher:</label><asp:TextBox ID="srPublisherTextBox" ClientIDMode="Static" runat="server" Width="400px" MaxLength="200"></asp:TextBox>
            </li>
            <li>
                <label class="caption" for="srLanguageList">Language:</label><asp:DropDownList ID="srLanguageList" ClientIDMode="Static" runat="server"></asp:DropDownList>
            </li>
            <li>
                <label class="caption" for="srNoteTextBox">Note:</label><asp:TextBox ID="srNoteTextBox" ClientIDMode="Static" runat="server" Height="100px" Width="400px" TextMode="MultiLine"></asp:TextBox>
            </li>
        </ol>
        <div class="tabbed">
            <asp:Button ID="srSubmitButton" runat="server" Text="Submit" OnClientClick="return ValidateRequestForm();" OnClick="srSubmitButton_Click" />
        </div>

		</div>
        </div>
		<!-- End Scanning Request Form -->
		<br />
		<asp:ValidationSummary ID="validationSummary" runat="server" />
		<asp:Panel ID="errorPanel" runat="server" Visible="false">
			<br />
			<asp:Label ID="errorLabel" runat="server" ForeColor="red" Text="Label"></asp:Label>
		</asp:Panel>
    </section>
    <aside></aside>
</div>
</asp:Content>

<asp:content id="scriptContent" contentplaceholderid="scriptContentPlaceHolder" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        var hideMsg = false;
        var comments = $(".comments");
        var request = $(".request");

        var fromComments = $("#FeedbackForm");
        var fromRequest = $("#ScanRequestForm");

        comments.click(function () {
            fromComments.show('fast');
            fromRequest.hide('fast');
            $('#linkbar a').removeClass('active');
            $('a', this).addClass('active').blur();
            if (hideMsg) $('#rcvdMsg').hide();
            hideMsg = true;
        });

        request.click(function () {
            fromComments.hide('fast');
            fromRequest.show('fast');
            $('#linkbar a').removeClass('active');
            $('a', this).addClass('active').blur();
            if (hideMsg) $('#rcvdMsg').hide();
            hideMsg = true;
        });

        if (!location.hash || location.hash == "#/comments") {
            comments.trigger("click");
        } else {
            request.trigger("click");
        }

    });
</script>
</asp:Content>
