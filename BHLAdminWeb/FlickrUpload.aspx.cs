using System;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections.Generic;
using System.Threading;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;
using FlickrUtility;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.AdminWeb
{
    public partial class FlickrUpload : System.Web.UI.Page
    {
        private delegate void DoStuff(); //delegate for the action 

        string oAuthToken;
        string oAuthTokenSecret;
        string oAuthVerifier;

        string oAuthAccessToken;
        string oAuthAccessTokenSecret;

        protected void Page_Load(object sender, EventArgs e)
        {
            oAuthToken = Request["oat"];
            oAuthTokenSecret = Request["oats"];
            oAuthVerifier = Request["oauth_verifier"];

            oAuthAccessToken = (Request["oaat"] == null?"":Request["oaat"]);
            oAuthAccessTokenSecret = (Request["oaats"] == null ? "" : Request["oaats"]);

            Page.ClientScript.RegisterClientScriptInclude("FlickrUploadJQueryJS", ConfigurationManager.AppSettings["jQueryPath"]);
            Page.ClientScript.RegisterClientScriptInclude("FlickrUploadJS", "/js/FlickrUpload.js");
            Page.ClientScript.RegisterStartupScript(typeof(FlickrUpload), "FlickrUpload", createInitJS());
        }

        private string createInitJS()
        {
            var sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">\r\n");
            sb.Append("var opts = {mainDivId:\"");
            sb.Append(mainDiv.ClientID);
            sb.Append("\", uploadStatusId:\"");
            sb.Append(UploadStatus.ClientID);
            sb.Append("\", oAuthAccessToken:\"");
            sb.Append(oAuthAccessToken);
            sb.Append("\", oAuthAccessTokenSecret:\"");
            sb.Append(oAuthAccessTokenSecret);

            sb.Append("\", oAuthToken:\"");
            sb.Append(oAuthToken);
            sb.Append("\", oAuthTokenSecret:\"");
            sb.Append(oAuthTokenSecret);
            sb.Append("\", oAuthVerifier:\"");
            sb.Append(oAuthVerifier);

            sb.Append("\", pageIds:\"");
            sb.Append(Request["pageids"]);
            
            sb.Append("\", titleId:\"");
            sb.Append(Request["titleid"]);

            sb.Append("\", rotate:\"");
            sb.Append(Request["rotate"]);

            sb.Append("\"};\n");
            sb.Append("new FlickrUpload( opts );\n");
            sb.Append("</script>\r\n");

            return sb.ToString();
        }
    }
}