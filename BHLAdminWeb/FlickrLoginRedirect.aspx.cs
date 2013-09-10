using System;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;
using System.Configuration;
using FlickrUtility;

namespace MOBOT.BHL.AdminWeb
{
    public partial class FlickrLoginRedirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder js = new StringBuilder("");

            try
            {
                string oAuthVerifier = Request["oauth_verifier"];
                string rotate = Request["rotate"];
                string pageIds = Request["pageids"];
                string titleId = Request["titleid"];

                Page.ClientScript.RegisterClientScriptInclude("FlickrUploadJQueryJS", ConfigurationManager.AppSettings["jQueryPath"]);
                Page.ClientScript.RegisterClientScriptInclude("FlickrUploadJS", "/js/FlickrUpload.js");
                Page.ClientScript.RegisterStartupScript(typeof(FlickrUpload), "FlickrUpload", createInitJS());
            }
            catch
            {
                js = new StringBuilder("");
                js.Append("window.close();\n\r");
                ClientScript.RegisterStartupScript(this.GetType(), "CloseFlickrLogin", js.ToString(), true);
            }
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
            sb.Append((Request["oaat"] == null ? "" : Request["oaat"]));
            sb.Append("\", oAuthAccessTokenSecret:\"");
            sb.Append((Request["oaats"] == null ? "" : Request["oaats"]));

            sb.Append("\", oAuthTokenCtrl:\"");
            sb.Append(Request["oat"]);
            sb.Append("\", oAuthTokenSecretCtrl:\"");
            sb.Append(Request["oats"]);
            sb.Append("\", oAuthVerifier:\"");
            sb.Append(Request["oauth_verifier"]);

            sb.Append("\", pageIdsCtrl:\"");
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