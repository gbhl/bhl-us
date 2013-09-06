using System;

namespace MOBOT.BHL.Web2
{
    public partial class NavBar : System.Web.UI.UserControl
    {
        public string searchTerm { get; set; }
        public string activeItem { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
               // tbSearchTerm.Text = searchTerm;
              //  tbSearchTerm.DataBind();
            }
            else
            {

            }
        }

        protected void btnSearchSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/search?searchTerm={0}", Server.UrlEncode(tbSearchTerm.Text)));
        }

        public string SetClass(string page)
        {
            return Request.Url.PathAndQuery.ToLower().Contains(page.ToLower()) ? string.Format("active {0}", page) : page;
        }
    }
}