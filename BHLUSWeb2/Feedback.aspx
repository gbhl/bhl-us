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
                <label class="caption">Name:</label><asp:TextBox ID="nameTextBox" runat="server" Width="200px" class="field"></asp:TextBox>
            </li>
            <li>
                <label class="caption">Email:</label><asp:TextBox ID="emailTextBox" runat="server" Width="200px"></asp:TextBox>
            </li>
            <li>
                <label class="caption">Subject:</label>
			    <asp:DropDownList runat="server" ID="ddlList">
			        <asp:ListItem Text="" Value=""></asp:ListItem>
				    <asp:ListItem Text="Technical Issues" Value="22"></asp:ListItem>
					<asp:ListItem Text="Suggestion" Value="36"></asp:ListItem>
					<asp:ListItem Text="Bibliographic Issues" Value="55"></asp:ListItem>
			    </asp:DropDownList> *
            </li>
            <li>
                <label class="caption">Comment:</label>
                <span style="float: right; padding-right: 30px;">*</span>
                <asp:TextBox ID="commentTextBox" runat="server" Height="100px" Width="375" TextMode="MultiLine"></asp:TextBox>
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
                <label class="caption">Your Name:</label><asp:TextBox ID="srNameTextBox" runat="server" Width="200px"></asp:TextBox> *
            </li>
            <li>
                <label class="caption">Email:</label><asp:TextBox ID="srEmailTextBox" runat="server" Width="200px"></asp:TextBox> *
            </li>
            <div class="tabbed">
                Search for your title in <a href="http://www.worldcat.org/" target="_blank" title="WorldCat">WorldCat</a> (recommended)
            </div>
            <li>
                <label class="caption">OCLC:</label><asp:TextBox ID="srOCLCTextBox" runat="server" Width="200px" MaxLength="30"></asp:TextBox>
            </li>
            <li>
                <label class="caption">Book Title:</label><asp:TextBox ID="srTitleTextBox" runat="server" Width="400px" MaxLength="500"></asp:TextBox> *
            </li>
            <li>
                <label class="caption">Year:</label><asp:TextBox ID="srYearTextBox" runat="server" Width="50px" MaxLength="20" style="width: 50px !important"></asp:TextBox> *
                &nbsp;&nbsp;&nbsp;
				(For © compliance, must be prior to <b>1923</b>)
            </li>
            <li>
                <label class="caption">Type:</label>
                <asp:DropDownList ID="srTypeList" runat="server">
				    <asp:ListItem Value="Book" Text="Book" />
				    <asp:ListItem Value="Journal" Text="Journal" />
				    <asp:ListItem Value="Not Sure" Text="Not Sure" />
				</asp:DropDownList> *
            </li>
            <li>
                <label class="caption">Volume:</label><asp:TextBox ID="srVolumeTextBox" runat="server" Width="200px" MaxLength="100" Text="All volumes" class="inlinetextbox"></asp:TextBox> * (journals only)
            </li>
            <li>
                <label class="caption">Edition:</label><asp:TextBox ID="srEditionTextBox" runat="server" Width="200px" MaxLength="100"></asp:TextBox>
            </li>
            <li>
                <label class="caption">ISBN:</label><asp:TextBox ID="srISBNTextBox" runat="server" Width="200px" MaxLength="30" class="inlinetextbox"></asp:TextBox>
            </li>
            <li>
                <label class="caption">ISSN:</label><asp:TextBox ID="srISSNTextBox" runat="server" Width="200px" MaxLength="30" class="inlinetextbox"></asp:TextBox>
            </li>
            <li>
                <label class="caption">Author:</label><asp:TextBox ID="srAuthorTextBox" runat="server" Width="400px" MaxLength="200"></asp:TextBox>
            </li>
            <li>
                <label class="caption">Publisher:</label><asp:TextBox ID="srPublisherTextBox" runat="server" Width="400px" MaxLength="200"></asp:TextBox>
            </li>
            <li>
                <label class="caption">Language:</label><asp:DropDownList ID="srLanguageList" runat="server"></asp:DropDownList>
            </li>
            <li>
                <label class="caption">Note:</label><asp:TextBox ID="srNoteTextBox" runat="server" Height="100px" Width="400px" TextMode="MultiLine"></asp:TextBox>
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
