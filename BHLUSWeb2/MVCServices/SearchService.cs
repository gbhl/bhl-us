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
        /// <param name="searchEdition"></param>
        /// <param name="searchYear"></param>
        /// <param name="searchSubject"></param>
        /// <param name="searchCollection"></param>
        /// <param name="searchIssue"></param>
        /// <param name="searchStartPage"></param>
        /// <param name="searchAnnotation"></param>
        /// <param name="searchContainerTitle"></param>
        /// <returns></returns>
        public string GetSearchCriteriaLabel(string searchCat, string searchTerm, string searchLang, string searchLastName,
            string searchVolume, string searchEdition, string searchYear, string searchSubject, string searchCollection,
            string searchIssue, string searchStartPage, string searchAnnotation, string searchContainerTitle)
        {
            StringBuilder searchCriteria = new StringBuilder();

            if (searchCat.Length == 0)
            {
                // search box at top of page was used; just echo the input
                searchCriteria.Append(searchTerm);
            }
            else
            {
                if (searchCat.ToUpper() == "A") searchCriteria.Append(searchTerm);    // author search
                if (searchCat.ToUpper() == "N" || searchCat.ToUpper() == "M") searchCriteria.Append(searchTerm);  // name search
                if (searchCat.ToUpper() == "S") searchCriteria.Append(searchTerm);    // subject search
                if (searchCat.ToUpper() == "T")
                {
                    // title search
                    if (searchTerm != string.Empty) searchCriteria.Append(" title:" + searchTerm.Replace(' ', '-'));
                    if (searchLastName != string.Empty) searchCriteria.Append(" author:" + searchLastName.Replace(' ', '-'));
                    if (searchVolume != string.Empty) searchCriteria.Append(" vol:" + searchVolume.Replace(' ', '-'));
                    if (searchEdition != string.Empty) searchCriteria.Append(" ed:" + searchEdition.Replace(' ', '-'));
                    if (searchYear != string.Empty) searchCriteria.Append(" year:" + searchYear.Replace(' ', '-'));
                    if (searchSubject != string.Empty) searchCriteria.Append(" subject:" + searchSubject.Replace(' ', '-'));
                    if (searchLang != string.Empty)
                    {
                        Language lang = new BHLProvider().LanguageSelectAuto(searchLang);
                        if (lang != null) searchCriteria.Append(" lang:" + lang.LanguageName.Replace(' ', '-'));
                    }
                    if (searchCollection != string.Empty)
                    {
                        Collection collection = new BHLProvider().CollectionSelectAuto(Convert.ToInt32(searchCollection));
                        if (collection != null) searchCriteria.Append(" collection:" + collection.CollectionName.Replace(' ', '-'));
                    }
                }
                if (searchCat.ToUpper() == "SG")
                {
                    // article search
                    if (searchTerm != string.Empty) searchCriteria.Append(" article:" + searchTerm.Replace(' ', '-'));
                    if (searchContainerTitle != string.Empty) searchCriteria.Append(" journal:" + searchContainerTitle.Replace(' ', '-'));
                    if (searchLastName != string.Empty) searchCriteria.Append(" author:" + searchLastName.Replace(' ', '-'));
                    if (searchYear != string.Empty) searchCriteria.Append(" year:" + searchYear.Replace(' ', '-'));
                }
                if (searchCat.ToUpper() == "O")
                {
                    // annotation search
                    searchCriteria.Append(searchAnnotation);
                    if (searchTerm != string.Empty) searchCriteria.Append(" title:" + searchTerm.Replace(' ', '-'));
                    if (searchLastName != string.Empty) searchCriteria.Append(" author:" + searchLastName.Replace(' ', '-'));
                    if (searchVolume != string.Empty) searchCriteria.Append(" vol:" + searchVolume.Replace(' ', '-'));
                    if (searchEdition != string.Empty) searchCriteria.Append(" ed:" + searchEdition.Replace(' ', '-'));
                    if (searchYear != string.Empty) searchCriteria.Append(" year:" + searchYear.Replace(' ', '-'));
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