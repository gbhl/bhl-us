using CustomDataAccess;
using System;

namespace MOBOT.BHL.DataObjects
{
    [Serializable]
    public class KBART : ISetValues
    {
        public string PublicationTitle { get; set; }
        public string PrintIdentifier { get; set; }
        public string OnlineIdentifier { get; set; }
        public string DateFirstIssueOnline { get; set; }
        public string NumFirstVolOnline { get; set; }
        public string NumFirstIssueOnline { get; set; }
        public string DateLastIssueOnline { get; set; }
        public string NumLastVolOnline { get; set; }
        public string NumLastIssueOnline { get; set; }
        public string TitleUrl { get; set; }
        public string FirstAuthor { get; set; }
        public string TitleID { get; set; }
        public string EmbargoInfo { get; set; }
        public string CoverageDepth { get; set; }
        public string Notes { get; set; }
        public string PublisherName { get; set; }
        public string PublicationType { get; set; }
        public string DateMonographPublishedPrint { get; set; }
        public string DateMonographPublishedOnline { get; set; }
        public string MonographVolume { get; set; }
        public string MonographEdition { get; set; }
        public string FirstEditor { get; set; }
        public string ParentPublicationTitleID { get; set; }
        public string PrecedingPublicationTitleID { get; set; }
        public string AccessType { get; set; }


        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "publication_title": { PublicationTitle = Utility.EmptyIfNull(column.Value); break; }
                    case "print_identifier": { PrintIdentifier = Utility.EmptyIfNull(column.Value); break; }
                    case "online_identifier": { OnlineIdentifier = Utility.EmptyIfNull(column.Value); break; }
                    case "date_first_issue_online": { DateFirstIssueOnline = Utility.EmptyIfNull(column.Value); break; }
                    case "num_first_vol_online": { NumFirstVolOnline = Utility.EmptyIfNull(column.Value); break; }
                    case "num_first_issue_online": { NumFirstIssueOnline = Utility.EmptyIfNull(column.Value); break; }
                    case "date_last_issue_online": { DateLastIssueOnline = Utility.EmptyIfNull(column.Value); break; }
                    case "num_last_vol_online": { NumLastVolOnline = Utility.EmptyIfNull(column.Value); break; }
                    case "num_last_issue_online": { NumLastIssueOnline = Utility.EmptyIfNull(column.Value); break; }
                    case "title_url": { TitleUrl = Utility.EmptyIfNull(column.Value); break; }
                    case "first_author": { FirstAuthor = Utility.EmptyIfNull(column.Value); break; }
                    case "title_id": { TitleID = Utility.EmptyIfNull(column.Value); break; }
                    case "embargo_info": { EmbargoInfo = Utility.EmptyIfNull(column.Value); break; }
                    case "coverage_depth": { CoverageDepth = Utility.EmptyIfNull(column.Value); break; }
                    case "notes": { Notes = Utility.EmptyIfNull(column.Value); break; }
                    case "publisher_name": { PublisherName = Utility.EmptyIfNull(column.Value); break; }
                    case "publication_type": { PublicationType = Utility.EmptyIfNull(column.Value); break; }
                    case "date_monograph_published_print": { DateMonographPublishedPrint = Utility.EmptyIfNull(column.Value); break; }
                    case "date_monograph_published_online": { DateMonographPublishedOnline = Utility.EmptyIfNull(column.Value); break; }
                    case "monograph_volume": { MonographVolume = Utility.EmptyIfNull(column.Value); break; }
                    case "monograph_edition": { MonographEdition = Utility.EmptyIfNull(column.Value); break; }
                    case "first_editor": { FirstEditor = Utility.EmptyIfNull(column.Value); break; }
                    case "parent_publication_title_id": { ParentPublicationTitleID = Utility.EmptyIfNull(column.Value); break; }
                    case "preceeding_publication_title_id": { PrecedingPublicationTitleID = Utility.EmptyIfNull(column.Value); break; }
                    case "access_type": { AccessType = Utility.EmptyIfNull(column.Value); break; }
                }
            }
        }
    }
}
