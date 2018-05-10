using CustomDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class Publications : DataObjectBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Publications()
        {
        }

        #endregion Constructors

        #region Properties

        CustomGenericList<Title> _titles = null;
        public CustomGenericList<Title> Titles
        {
            get { return _titles; }
            set { _titles = value; }
        }

        CustomGenericList<Item> _items = null;
        public CustomGenericList<Item> Items
        { 
            get { return _items; }
            set { _items = value; }
        }

        CustomGenericList<Part> _parts = null;
        public CustomGenericList<Part> Parts
        {
            get { return _parts; }
            set { _parts = value; }
        }

        #endregion Properties
    }
}
