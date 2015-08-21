function FlickrUpload( options )
{
	var mainDiv = $( "#" + options.mainDivId );
	var uploadStatus = $( "#" + options.uploadStatusId );

	$(document).ready(function () {
	    if (options.oAuthAccessToken == "" || options.oAuthAccessTokenSecret == "") {
	        uploadStatus.text("Validating Flickr login");
	        validateFlickr();
	    }

	    if (options.oAuthAccessToken != "" && options.oAuthAccessTokenSecret != "") {
	        var pageIdList = window.opener.document.getElementById(options.pageIdsCtrl).value;
	        var pageIds = pageIdList.split('|');
	        var totalPages = pageIds.length;

	        uploadImage(1, totalPages, pageIds);
	    }
	});

	setStatus = function (pageCount, totalPages) {
	    uploadStatus.text("Uploading image " + pageCount + " of " + totalPages);
	}

	uploadImage = function (pageCount, totalPages, pageIds) {
	    setStatus(pageCount, totalPages);
	    $.ajax(
	    {
	        type: "GET", url: "/AjaxFlickrUpload.aspx", dataType: "json", data:
		    {
		        type: "upload", pageid: pageIds[pageCount - 1], titleid: options.titleId, oauthaccesstoken: options.oAuthAccessToken,
		        oauthaccesstokensecret: options.oAuthAccessTokenSecret, rotate: options.rotate
		    },
	        timeout: 150000, success: function (opts) {
	            if (opts && opts.errorMsg != "") {
	                alert('FlickrImageUpload: ' + opts.errorMsg);
	                window.close();
	            }
	            else {
	                if ((pageCount + 1) <= totalPages) {
	                    uploadImage((pageCount + 1), totalPages, pageIds);
	                }
	                else {
//	                    if (totalPages > 1) {
//	                        alert('Images successfully uploaded to Flickr.');
//	                    }
//	                    else {
//	                        alert('Image successfully uploaded to Flickr.');
//	                    }
	                    window.opener.location.href = window.opener.location.href;
	                    //window.opener.location.reload(true);
	                    window.close();
	                }
	            }
	        }, error: function (xhr, status) {
	            alert('An error occurred uploading the image to Flickr.  Please try again later.');
	            window.close();
	        }
	    });
	}

	validateFlickr = function () {
	    var oAuthToken = window.opener.document.getElementById(options.oAuthTokenCtrl).value;
	    var oAuthTokenSecret = window.opener.document.getElementById(options.oAuthTokenSecretCtrl).value;

	    $.ajax(
	        {

	            type: "GET", url: "/AjaxFlickrUpload.aspx", dataType: "json", async: false, data:
		        {
		            type: "validate",
		            oauthtoken: oAuthToken, oauthtokensecret: oAuthTokenSecret,
		            oauthverifier: options.oAuthVerifier
		        },
	            timeout: 30000, success: function (opts) {
	                if (opts) {
	                    if (opts.errorMsg != "") {
	                        alert('ValidateFlickrLogin: ' + opts.errorMsg);
	                        window.close();
	                    }
	                    else {
	                        options.oAuthAccessToken = opts.oAuthAccessToken;
	                        options.oAuthAccessTokenSecret = opts.oAuthAccessTokenSecret;
	                    }
	                }
	                else {
	                    alert('An error occurred trying to log into Flickr.  Please try again later.');
	                    window.close();
	                }
	            }, error: function (xhr, status) {
	                alert('An error occurred trying to log into Flickr.  Please try again later.');
	                window.close();
	            }
	        });
	};
}