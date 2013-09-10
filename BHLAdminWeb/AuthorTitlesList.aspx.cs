using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.AdminWeb
{
    public partial class AuthorTitlesList : System.Web.UI.Page
    {
        public string authorName = string.Empty;
        public string numberOfTitles = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the Marc ID
                String authorID = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();

                int idInt;
                if (Int32.TryParse(authorID, out idInt))
                {
                    BHLProvider provider = new BHLProvider();
                    titleList.DataSource = provider.TitleSimpleSelectByAuthor(idInt);
                    titleList.DataBind();
                    numberOfTitles = titleList.Rows.Count.ToString();

                    Author author = provider.AuthorSelectWithNameByAuthorId(idInt);
                    if (author != null)
                    {
                        authorName = author.FullName + " ";
                        if (!string.IsNullOrEmpty(author.Numeration)) authorName += author.Numeration + " ";
                        if (!string.IsNullOrEmpty(author.Title)) authorName += author.Title + " ";
                        if (!string.IsNullOrEmpty(author.Unit)) authorName += author.Unit + " ";
                        if (!string.IsNullOrEmpty(author.Location)) authorName += author.Location + " ";
                        if (!string.IsNullOrEmpty(author.FullerForm)) authorName += author.FullerForm + " ";
                        authorName += author.StartDate + (author.StartDate == string.Empty ? " " : "-") + author.EndDate;
                    }
                }
            }
        }
    }
}