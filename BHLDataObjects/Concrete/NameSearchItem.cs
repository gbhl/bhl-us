using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.BHL.DataObjects
{
    public class NameSearchItem
    {
        private int itemID = 0;
        private short itemSequence = 0;
        private string volume = "";
        private List<NameSearchPage> pages = new List<NameSearchPage>();

        public int ItemID
        {
            get
            {
                return itemID;
            }
            set
            {
                itemID = value;
            }
        }

        public short ItemSequence
        {
            get
            {
                return itemSequence;
            }
            set
            {
                itemSequence = value;
            }
        }

        public string Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
            }
        }

        public List<NameSearchPage> Pages
        {
            get
            {
                return pages;
            }
            set
            {
                pages = value;
            }
        }
    }
}
