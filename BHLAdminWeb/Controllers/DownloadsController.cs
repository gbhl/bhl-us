﻿using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    public class DownloadsController : Controller
    {
        // GET: Downloads
        public ActionResult ExternalContent()
        {
            BHLProvider provider = new BHLProvider();
            List<Tuple<string, string, string>> links = provider.LinkSelectToExternalContent();

            var data = new List<dynamic>();
            foreach (Tuple<string, string, string> link in links)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("Entity", link.Item1);
                record.Add("ID", link.Item2.Split('|')[0]);
                record.Add("Title/Name", link.Item2.Split('|')[1]);
                record.Add("External URL", link.Item3);
                data.Add(record);
            }

            byte[] csvData = new CSV().FormatCSVData(data);
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            Response.Charset = "utf-8";
            return File(csvData, "text/csv", string.Format("LinksToExternalContent{0}.csv", date));
        }

        public ActionResult SegmentsForTitle(int id)
        {
            BHLProvider provider = new BHLProvider();
            List<DataObjects.Segment> segments = provider.SegmentSelectByTitleID(id);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Segment ID\tTitle\tTranslated Title\tItem ID\tVolume\tIssue\tSeries\tDate\tLanguage\tAuthor IDs\tAuthor Names\tStart Page\tEnd Page\tStart Page BHL ID\tEnd Page BHL ID\tAdditional Page IDs\tArticle DOI\tContributors");
            foreach(DataObjects.Segment segment in segments)
            {
                List<string> contributorList = new List<string>();
                string contributors = string.Empty;
                foreach (DataObjects.Institution contributor in segment.ContributorList) contributorList.Add(contributor.InstitutionCode);
                if (contributorList.Count > 0) contributors = string.Join(";", contributorList);

                List<string> authorIDs = new List<string>();
                List<string> authors = new List<string>();
                string[] authorList= segment.Authors.Split(new char[] { '$', '$', '$' }, StringSplitOptions.RemoveEmptyEntries);
                foreach(string auth in authorList)
                {
                    string[] a = auth.Split('|');
                    if (a.Length == 2)
                    {
                        authorIDs.Add(a[0].Trim());
                        authors.Add(a[1].Trim());
                    }
                }

                sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}",
                    segment.SegmentID.ToString(), segment.Title, segment.TranslatedTitle, segment.BookID, segment.Volume, segment.Issue, segment.Series, segment.Date, 
                    segment.LanguageName, string.Join(";", authorIDs), string.Join(";", authors), segment.StartPageNumber, segment.EndPageNumber, segment.StartPageID, segment.EndPageID,
                    segment.AdditionalPages, segment.DOIName, contributors));
            }

            byte[] tsvData = Encoding.UTF8.GetBytes(sb.ToString());
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            Response.Charset = "utf-8";
            return File(tsvData, "text/tab-separated-values", string.Format("SegmentsForTitle{0}-{1}.txt", id.ToString(), date));
        }
    }
}