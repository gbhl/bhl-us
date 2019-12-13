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
                    IComparer<SearchBookResult> comp = new SearchBookResultTitleComparer();
                    switch (_sortBy)
                    {
                        case "title": comp = new SearchBookResultTitleComparer(); break;
                        case "author": comp = new SearchBookResultAuthorComparer(); break;
                        case "year": comp = new SearchBookResultYearComparer(); break;
                    }

                    Data.Sort(comp);
                }

                // Replace the contributor name with default text if there is more than one
                if (Data != null)
                {
                    foreach (SearchBookResult result in Data)
                    {
                        if (result.InstitutionName.Contains("|")) result.InstitutionName = "Multiple institutions";
                    }
                }

                bookRepeater.DataSource = Data;
                bookRepeater.DataBind();
            }
        }
    }
}