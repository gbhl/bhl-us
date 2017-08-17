using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MOBOT.BHL.Web2.MVCServices
{
    public class SearchService
    {
        /// <summary>
        /// Build the text for the label that echoes the search criteria
        /// </summary>
        /// <param name="searchCat"></param>
        /// <param name="searchTerm"></param>
        /// <param name="searchLang"></param>
        /// <param name="searchLastName"></param>
        /// <param name="searchVolume"></param>
        /// <param name="searchYear"></param>
        /// <param name="searchSubject"></param>
        /// <param name="searchCollection"></param>
        /// <returns></returns>
        public string GetSearchCriteriaLabel(string searchCat, string searchTerm, string searchLang, 
            string searchLastName, string searchVolume, string searchYear, string searchSubject, 
            string searchCollection)
        {
            StringBuilder searchCriteria = new StringBuilder();

            if (string.IsNullOrWhiteSpace(searchCat))
            {
                // search box at top of page was used; just echo the input
                searchCriteria.Append(searchTerm ?? "");
            }
            else
            {
                if (searchCat.ToUpper() == "A") searchCriteria.Append(searchTerm ?? "");    // author search
                if (searchCat.ToUpper() == "N" || searchCat.ToUpper() == "M") searchCriteria.Append(searchTerm ?? "");  // name search
                if (searchCat.ToUpper() == "S") searchCriteria.Append(searchTerm ?? "");    // subject search

                // title search
                if (searchCat.ToUpper() == "T")
                {
                    if (!string.IsNullOrWhiteSpace(searchTerm)) searchCriteria.Append(" title:" + searchTerm.Replace(' ', '-'));
                    if (!string.IsNullOrWhiteSpace(searchLastName)) searchCriteria.Append(" author:" + searchLastName.Replace(' ', '-'));
                    if (!string.IsNullOrWhiteSpace(searchVolume)) searchCriteria.Append(" vol:" + searchVolume.Replace(' ', '-'));
                    if (!string.IsNullOrWhiteSpace(searchYear)) searchCriteria.Append(" year:" + searchYear.Replace(' ', '-'));
                    if (!string.IsNullOrWhiteSpace(searchSubject)) searchCriteria.Append(" subject:" + searchSubject.Replace(' ', '-'));
                    if (!string.IsNullOrWhiteSpace(searchLang))
                    {
                        Language lang = new BHLProvider().LanguageSelectAuto(searchLang);
                        if (lang != null) searchCriteria.Append(" lang:" + lang.LanguageName.Replace(' ', '-'));
                    }
                    if (!string.IsNullOrWhiteSpace(searchCollection))
                    {
                        Collection collection = new BHLProvider().CollectionSelectAuto(Convert.ToInt32(searchCollection));
                        if (collection != null) searchCriteria.Append(" collection:" + collection.CollectionName.Replace(' ', '-'));
                    }
                }
            }

            return searchCriteria.ToString().Trim();
        }

        public List<Language> LanguageList()
        {
            BHLProvider provider = new BHLProvider();
            return provider.LanguageSelectWithPublishedItems().ToList<Language>();
        }

        public List<Collection> CollectionList()
        {
            BHLProvider provider = new BHLProvider();
            return provider.CollectionSelectActive().ToList<Collection>();
        }
    }
}