using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Web2
{
    public partial class BookBrowseControl : System.Web.UI.UserControl
    {
        private bool _showVolume = false;
        public bool ShowVolume
        {
            get { return _showVolume; }
            set { _showVolume = value; }
        }

        CustomGenericList<SearchBookResult> _data = null;
        public CustomGenericList<SearchBookResult> Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// If SortBy is set to a column name, the data set will be
        /// sorted by that column.
        /// </summary>
        private string _sortBy = string.Empty;
        public string SortBy
        {
            get { return _sortBy; }
            set { _sortBy = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Sort the data, if necessary
                if (_sortBy != string.Empty)
                {
                    SearchBookResultComparer.CompareEnum sortByEnum = SearchBookResultComparer.CompareEnum.Title;
                    switch (_sortBy)
                    {
                        case "title": sortByEnum = SearchBookResultComparer.CompareEnum.Title; break;
                        case "author": sortByEnum = SearchBookResultComparer.CompareEnum.Author; break;
                        case "year": sortByEnum = SearchBookResultComparer.CompareEnum.Year; break;
                    }
                    SearchBookResultComparer comp = new SearchBookResultComparer(sortByEnum, SortOrder.Ascending);
                    Data.Sort(comp);
                }

                bookRepeater.DataSource = Data;
                bookRepeater.DataBind();
            }
        }
    }
}