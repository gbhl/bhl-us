using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.BHL.DataObjects
{
	public class NameSearchResult
	{
		private string queryDate = "";
		private List<NameSearchTitle> titles = new List<NameSearchTitle>();
        private List<NameSearchPage> pages = new List<NameSearchPage>();

		public string QueryDate
		{
			get { return queryDate; }
			set { queryDate = value; }
		}

		public List<NameSearchTitle> Titles
		{
			get { return titles; }
			set { titles = value; }
		}

		public int TitleCount
		{
			get { return titles.Count; }
			set { }
		}

		public int PageCount
		{
			get
			{
                int k = 0;
                if (Pages.Count > 0)
                {
                    k = Pages[0].TotalPages;
                }
                else
                {
                    foreach (NameSearchTitle title in Titles)
                    {
                        k = k + title.TotalPageCount;
                    }
                }

				return k;
			}
			set { }
		}

        public List<NameSearchPage> Pages
        {
            get { return pages; }
            set { pages = value; }
        }
	}
}
