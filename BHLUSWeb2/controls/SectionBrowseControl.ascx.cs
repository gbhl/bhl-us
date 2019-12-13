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
    public partial class SectionBrowseControl : System.Web.UI.UserControl
    {
        CustomGenericList<Segment> _data = null;
        public CustomGenericList<Segment> Data
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
                    IComparer<Segment> comp = new SegmentTitleComparer();
                    switch (_sortBy)
                    {
                        case "title": comp = new SegmentTitleComparer(); break;
                        case "author": comp = new SegmentAuthComparer(); break;
                        case "year": comp = new SegmentDateComparer(); break;
                    }

                    Data.Sort(comp);
                }

                // Replace the contributor name with default text if there is more than one
                if (Data != null)
                    {
                    foreach (Segment result in Data)
                    {
                        if (result.ContributorName.Contains("|")) result.ContributorName = "Multiple institutions";
                    }
                }

                sectionRepeater.DataSource = Data;
                sectionRepeater.DataBind();
            }
        }
    }
}