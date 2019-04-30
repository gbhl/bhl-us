using MOBOT.BHL.Server;
using System;

namespace MOBOT.BHL.AdminWeb
{
    public partial class EditHistoryList : System.Web.UI.Page
    {
        public string entityName = string.Empty;
        public string entityID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                entityName = Request.QueryString["name"] == null ? "" : Request.QueryString["name"].ToString();
                entityID = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();

                int idInt;
                bool isInt = Int32.TryParse(entityID, out idInt);

                BHLProvider provider = new BHLProvider();

                switch (entityName.ToLower())
                {
                    case "title":
                        entityName = "Title";
                        if (isInt) historyList.DataSource = provider.EditHistorySelectByTitleID(idInt);
                        break;
                    case "item":
                        entityName = "Item";
                        if (isInt) historyList.DataSource = provider.EditHistorySelectByItemID(idInt);
                        break;
                    case "segment":
                        entityName = "Segment";
                        if (isInt) historyList.DataSource = provider.EditHistorySelectBySegmentID(idInt);
                        break;
                    case "author":
                        entityName = "Author";
                        if (isInt) historyList.DataSource = provider.EditHistorySelectByAuthorID(idInt);
                        break;
                    case "collection":
                        entityName = "Collection";
                        if (isInt) historyList.DataSource = provider.EditHistorySelectByEntityAndID("dbo", "Collection", entityID);
                        break;
                    case "language":
                        entityName = "Language";
                        historyList.DataSource = provider.EditHistorySelectByEntityAndID("dbo", "Language", entityID);
                        break;
                    case "institution":
                        entityName = "Content Provider";
                        historyList.DataSource = provider.EditHistorySelectByEntityAndID("dbo", "Institution", entityID);
                        break;
                    case "notetype":
                        entityName = "Note Type";
                        if (isInt) historyList.DataSource = provider.EditHistorySelectByEntityAndID("dbo", "NoteType", entityID);
                        break;
                    case "pagetype":
                        entityName = "Page Type";
                        if (isInt) historyList.DataSource = provider.EditHistorySelectByEntityAndID("dbo", "PageType", entityID);
                        break;
                    case "segmentgenre":
                        entityName = "Segment Type";
                        if (isInt) historyList.DataSource = provider.EditHistorySelectByEntityAndID("dbo", "SegmentGenre", entityID);
                        break;
                    case "pdf":
                        entityName = "PDF";
                        if (isInt) historyList.DataSource = provider.EditHistorySelectByEntityAndID("dbo", "PDF", entityID);
                        break;
                    case "namepage":
                        entityName = "Names for Page";
                        if (isInt) historyList.DataSource = provider.EditHistorySelectNameByPageID(idInt);
                        break;
                    case "pagination":
                        entityName = "Item Pagination";
                        if (isInt) historyList.DataSource = provider.EditHistorySelectPageByItemID(idInt);
                        break;
                    default:
                        entityName = "Unknown";
                        break;
                }

                historyList.DataBind();
            }
        }
    }
}