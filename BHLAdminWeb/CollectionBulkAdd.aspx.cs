using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.AdminWeb
{
    public partial class CollectionBulkAdd : System.Web.UI.Page
    {
        private int collectionID = 0;

        public int CollectionID
        {
            get { return collectionID; }
        }

        private string collectionType = string.Empty;

        public string CollectionType
        {
            get { return collectionType; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string collectionIDStr = Request.QueryString["id"] as string;
                if (!String.IsNullOrEmpty(collectionIDStr))
                {
                    if (Int32.TryParse(collectionIDStr, out collectionID))
                    {
                        ViewState.Add("CollectionID", collectionID.ToString());

                        BHLProvider provider = new BHLProvider();
                        Collection collection = provider.CollectionSelectAuto(collectionID);
                        if (collection != null) collectionType = (collection.CanContainItems == 1 ? "Item" : "Title");

                        ViewState.Add("CollectionType", collectionType);
                    }
                }
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            // Get a list of IDs
            List<string> ids = this.GetIDList(txtIDs.Text, selDelimiter.Items[selDelimiter.SelectedIndex].Value);

            // Load the IDs into the collection
            txtOutput.Text = this.InsertIDs(ids, Convert.ToInt32(ViewState["CollectionID"]), ViewState["CollectionType"].ToString());

            btnCancel.Text = "Done";
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("collectionedit.aspx?id=" + ViewState["CollectionID"]);
        }


        /// <summary>
        /// Get a list containing the title/item IDs specified by the user
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="selectedDelimiter"></param>
        /// <returns></returns>
        private List<string> GetIDList(string idList, string selectedDelimiter)
        {
            // Split list at each delimiter
            char delimiter = '\n';
            switch (selectedDelimiter)
            {
                case ",":
                    delimiter = ',';
                    break;
                case ";":
                    delimiter = ';';
                    break;
                case "s":
                    delimiter = ' ';
                    break;
                case "t":
                    delimiter = '\t';
                    break;
                case "lb":
                default:
                    idList = idList.Replace("\n\r", "\n");
                    idList = idList.Replace("\r\n", "\n");
                    if (idList.IndexOf('\n') >= 0)
                        delimiter = '\n';
                    else
                        delimiter = '\r';
                    break;
            }
            string[] ids = idList.Split(delimiter);

            // Clean up special characters and blank spaces
            List<string> cleanIDList = new List<string>();
            for (int x = 0; x < ids.Length; x++)
            {
                ids[x] = ids[x].Replace("\n", "").Replace("\r", "").Trim();
                if (!string.IsNullOrEmpty(ids[x])) cleanIDList.Add(ids[x]);
            }

            // Return the final list
            return cleanIDList;
        }

        /// <summary>
        /// Insert the IDs in the specified list into the specified collection
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private string InsertIDs(List<string> ids, int collectionID, string collectionType)
        {
            string idListOutput = string.Empty;
            BHLProvider provider = new BHLProvider();
            foreach (string id in ids)
            {
                try
                {
                    int idInt = 0;
                    if (Int32.TryParse(id, out idInt))
                    {
                        bool doInsert = true;
                        if (collectionType == "Item")
                        {
                            Book book = provider.BookSelectAuto(idInt);
                            if (book == null) { idListOutput += id + " - Item Not Found\n"; doInsert = false; }
                            if (doInsert) provider.ItemCollectionInsert(book.ItemID, collectionID);
                        }
                        else
                        {
                            Title title = provider.TitleSelectAuto(idInt);
                            if (title == null) { idListOutput += id + " - Title Not Found\n"; doInsert = false; }
                            if (doInsert) provider.TitleCollectionInsert(idInt, collectionID);
                        }

                        if (doInsert) idListOutput += id + " - ok\n";
                    }
                    else
                    {
                        idListOutput += id + " - Invalid ID\n";
                    }
                }
                catch (Exception ex)
                {
                    idListOutput += id + " - ERROR: " + ex.Message + "\n";
                }
            }

            return idListOutput;
        }
    }
}