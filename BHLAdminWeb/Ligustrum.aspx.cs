using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class Ligustrum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];

            if (token != null && token.Length > 0)
            {
                Response.Cookies["MOBOTSecurityToken"].Value = token;
                string expTime = ConfigurationManager.AppSettings["TokenExpirationTime"];
                Response.Cookies["MOBOTSecurityToken"].Expires = DateTime.Now.AddMinutes(int.Parse(expTime));
            }

            HttpCookie cookie = Request.Cookies["CallingUrl"];
            string send = Request.QueryString["send"];
            if (send != null && send.Length > 0)
            {
                if (send.Equals("1"))
                {
                    string urlCode = ConfigurationManager.AppSettings["MOBOTSecurityUrl"];
                    Response.Redirect(urlCode);
                }
                else if (send.ToUpper().Equals("NTLM"))
                {
                    string urlCode = ConfigurationManager.AppSettings["MOBOTSecurityNTLMUrl"];
                    Response.Redirect(urlCode);
                }
                else if (send.Equals("2")) // logout
                {
                    Response.Cookies["MOBOTSecurityToken"].Expires = DateTime.Now.AddDays(-2);
                    if (cookie != null)
                    {
                        // Remove token from the query string of the calling url so that the token cookie doesn't get recreated
                        string retUrl = removeTokenFromQueryString(cookie.Value);
                        Response.Redirect(retUrl);
                    }
                    else
                    {
                        Response.Redirect("/login");
                    }
                }
                else if (send.Equals("3"))
                {
                    string urlCode = ConfigurationManager.AppSettings["MOBOTSecurityUrl"];
                    Response.Redirect(urlCode);
                }
            }
            else
            {
                if (cookie != null)
                {
                    Response.Redirect(cookie.Value);
                }
                else
                {
                    Response.Redirect("/login");
                }
            }
        }

        private string removeTokenFromQueryString(string url)
        {
            int ix = url.IndexOf("token=");
            if (ix > -1)
            {
                int iy = url.IndexOf("?", ix, 1);
                if (iy > -1)
                {
                    url = url.Substring(0, ix - 1) + url.Substring(iy);
                }
                else
                {
                    url = url.Substring(0, ix - 1);
                }
            }

            return url;
        }
    }
}