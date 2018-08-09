using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web2.Models;
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
        public string GetSearchCriteriaLabel(SearchParams p)
        {
            StringBuilder searchCriteria = new StringBuilder();

            if (string.IsNullOrWhiteSpace(p.SearchCategory))
            {
                // search box at top of page was used; just echo the input
                searchCriteria.Append(p.SearchTerm ?? "");
            }
            else
            {
                if (p.SearchCategory.ToUpper() == "A") searchCriteria.Append(p.SearchTerm ?? "");    // author search
                if (p.SearchCategory.ToUpper() == "N" || p.SearchCategory.ToUpper() == "M") searchCriteria.Append(p.SearchTerm ?? "");  // name search
                if (p.SearchCategory.ToUpper() == "S") searchCriteria.Append(p.SearchTerm ?? "");    // subject search

                // title search
                if (p.SearchCategory.ToUpper() == "T")
                {
                    string languageCode = p.Language != null ? p.Language.Item1 : string.Empty;
                    string collectionId = p.Collection != null ? p.Collection.Item1 : string.Empty;

                    if (!string.IsNullOrWhiteSpace(p.SearchTerm))
                    {
                        //searchCriteria.Append(" title:" + p.SearchTerm.Replace(' ', '-'));
                        //searchCriteria.Append(" " + (p.TermInclude == "A" ? "[All words]" : "[Exact phrase]"));
                        searchCriteria.Append(string.Format(" title:{0} [{1}]",
                            p.SearchTerm.Replace(' ', '-'),
                            p.TermInclude.ToUpper() == "A" ? "All words" : "Exact phrase"));
                    }
                    if (!string.IsNullOrWhiteSpace(p.LastName)) searchCriteria.Append(" author:" + p.LastName.Replace(' ', '-'));
                    if (!string.IsNullOrWhiteSpace(p.Volume)) searchCriteria.Append(" vol:" + p.Volume.Replace(' ', '-'));
                    if (!string.IsNullOrWhiteSpace(p.Year)) searchCriteria.Append(" year:" + p.Year.Replace(' ', '-'));
                    if (!string.IsNullOrWhiteSpace(p.Subject)) searchCriteria.Append(" subject:" + p.Subject.Replace(' ', '-'));
                    if (!string.IsNullOrWhiteSpace(languageCode))
                    {
                        Language lang = new BHLProvider().LanguageSelectAuto(languageCode);
                        if (lang != null) searchCriteria.Append(" lang:" + lang.LanguageName.Replace(' ', '-'));
                    }
                    if (!string.IsNullOrWhiteSpace(collectionId))
                    {
                        Collection collection = new BHLProvider().CollectionSelectAuto(Convert.ToInt32(collectionId));
                        if (collection != null) searchCriteria.Append(" collection:" + collection.CollectionName.Replace(' ', '-'));
                    }
                    if (!string.IsNullOrWhiteSpace(p.Text)) searchCriteria.Append(" text:" + p.Text.Replace(' ', '-'));
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