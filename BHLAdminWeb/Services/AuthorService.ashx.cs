using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Summary description for AuthorService
    /// </summary>
    [WebService(Namespace = "https://biodiversitylibrary.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AuthorService : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            String response;

            // Clean up inputs
            String qsAuthorID = context.Request.QueryString["authorID"] as String;
            int authorID;
            Int32.TryParse(qsAuthorID, out authorID);
            String authorName = context.Request.QueryString["authorName"] as String;
            authorName = (authorName ?? string.Empty);

            switch (context.Request.QueryString["op"])
            {
                case "AuthorSearch":
                    {
                        response = this.AuthorSearch(authorID, authorName);
                        break;
                    }
                default:
                    {
                        response = null;
                        break;
                    }

            }

            context.Response.ContentType = "application/json";
            context.Response.Write(response);
        }

        private string AuthorSearch(int authorId, String authorName)
        {
            try
            {
                List<Author> authors = new List<Author>();
                if (authorId != 0)
                {
                    authors.Add(new BHLProvider().AuthorSelectWithNameByAuthorId(authorId));
                }
                else if (!string.IsNullOrEmpty(authorName))
                {
                    authors = new BHLProvider().SearchAuthorComplete(authorName);
                }
                for (int x = (authors.Count - 1); x >= 0; x--)
                {
                    // Remove inactive or redirected authors.  This leaves active authors without titles in the list.
                    if (authors[x].IsActive == 0 || authors[x].RedirectAuthorID != null) authors.RemoveAt(x);
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(authors);
            }
            catch
            {
                return null;
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}