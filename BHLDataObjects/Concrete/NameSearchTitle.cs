using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.BHL.DataObjects
{
	public class NameSearchTitle
	{
		private int titleID = 0;
		private string marcBibID = "";
        private string fullTitle = "";
		private string shortTitle = "";
		private string sortTitle = "";
		private int totalPageCount = 0;
		private List<NameSearchItem> items = new List<NameSearchItem>();

		public int TitleID
		{
			get
			{
				return titleID;
			}
			set
			{
				titleID = value;
			}
		}

		public string MarcBibID
		{
			get
			{
				return marcBibID;
			}
			set
			{
				marcBibID = value;
			}
		}

        public string FullTitle
        {
            get
            {
                return fullTitle;
            }
            set
            {
                fullTitle = value;
            }
        }
		public string ShortTitle
		{
			get
			{
				return shortTitle;
			}
			set
			{
				shortTitle = value;
			}
		}

		public string SortTitle
		{
			get
			{
				return sortTitle;
			}
			set
			{
				sortTitle = value;
			}
		}

		public int TotalPageCount
		{
			get
			{
				return totalPageCount;
			}
			set
			{
				totalPageCount = value;
			}
		}

		public List<NameSearchItem> Items
		{
			get
			{
				return items;
			}
			set
			{
				items = value;
			}
		}
	}
}
