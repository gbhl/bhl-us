using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MOBOT.BHL.OAI2;
using MOBOT.BHL.OAIMODS;

namespace MOBOT.BHL.OAIOLEF
{
    public class Convert
    {
        OAIRecord _oaiRecord;

        public Convert(OAIRecord oaiRecord)
        {
            _oaiRecord = oaiRecord;
        }

        public Convert(string olefRecord)
        {
            _oaiRecord = new OAIRecord();

            // TODO: Parse the supplied OLEF and store the values in _oaiRecord

            throw new NotImplementedException();
        }

        #region ToOAIRecord

        public OAIRecord ToOAIRecord()
        {
            return _oaiRecord;
        }

        #endregion ToOAIRecord

        #region ToString

        public new String ToString()
        {
            StringBuilder sb = new StringBuilder();

            //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            sb.Append("<olef:olef xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:olef=\"http://www.bhl-europe.eu/bhl-schema/v1/\" xmlns:mods=\"http://www.loc.gov/mods/v3\"  xmlns:dwc=\"http://rs.tdwg.org/dwc/terms/\">\n");
            sb.Append("<olef:element>\n");

            sb.Append(this.GetBibliographicInformationElement());
            sb.Append(this.GetItemInformationElement());
            sb.Append(this.GetIprElement());
            sb.Append(this.GetSequenceElement());
            sb.Append(this.GetGuidElement());
            sb.Append(this.GetLevelElement());
            sb.Append(this.GetParentGuidElement());

            sb.Append("</olef:element>\n");
            sb.Append("</olef:olef>\n");

            return sb.ToString();
        }

        #endregion ToString

        #region Elements

        public string GetBibliographicInformationElement()
        {
            OAIMODS.Convert mods = new OAIMODS.Convert(_oaiRecord);
            string metadata = mods.ToString();

            // Remove the default MODS root element
            metadata = metadata.Substring(metadata.IndexOf(">") + 1);
            metadata = metadata.Replace("</mods>", "");

            // Add a namespace alias to each element
            metadata = metadata.Replace("<", "<mods:");
            metadata = metadata.Replace("mods:/", "/mods:");

            return "\t<olef:bibliographicInformation>" + metadata + "\t</olef:bibliographicInformation>\n";
        }

        public string GetItemInformationElement()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\t<olef:itemInformation>\n");
            sb.Append("\t\t<olef:files>\n");

            foreach (KeyValuePair<string, OAIRecord.Page> page in _oaiRecord.Pages)
            {
                sb.Append("\t\t\t<olef:file>\n");
                sb.AppendFormat("\t\t\t\t<olef:reference olef:type=\"url\">{0}</olef:reference>\n", page.Value.ImageUrl);
                sb.Append("\t\t\t\t<olef:pages>\n");
                sb.AppendFormat("\t\t\t\t\t<olef:page olef:sequence=\"{0}\" olef:pageType=\"{1}\">\n", 
                    page.Value.Sequence.ToString(), HttpUtility.HtmlEncode(GetOlefPageType(page.Value.PageType)));
                if (!string.IsNullOrWhiteSpace(page.Value.PageLabel)) {
                    sb.AppendFormat("\t\t\t\t\t\t<olef:name>{0}</olef:name>\n", HttpUtility.HtmlEncode(page.Value.PageLabel));
                }

                foreach (KeyValuePair<string, OAIRecord.Name> name in page.Value.Names)
                {
                    sb.Append("\t\t\t\t\t\t<olef:taxon>\n");
                    sb.AppendFormat("\t\t\t\t\t\t\t<dwc:scientificName>{0}</dwc:scientificName>\n", HttpUtility.HtmlEncode(name.Value.ScientificName));
                    sb.Append("\t\t\t\t\t\t</olef:taxon>\n");
                }

                sb.Append("\t\t\t\t\t</olef:page>\n");
                sb.Append("\t\t\t\t</olef:pages>\n");
                sb.Append("\t\t\t</olef:file>\n");
            }

            sb.Append("\t\t</olef:files>\n");
            sb.Append("\t</olef:itemInformation>\n");

            return sb.ToString();
        }

        public string GetIprElement()
        {
            // IPR information is included in the bibliographic information (mods:AccessCondition)
            return "\t<olef:ipr />\n";
        }

        public string GetSequenceElement()
        {
            return "\t<olef:sequence>" + _oaiRecord.Sequence + "</olef:sequence>\n";
        }

        public string GetLevelElement()
        {
            StringBuilder sb = new StringBuilder();
            String genre = String.Empty;

            switch (_oaiRecord.Type)
            {
                case OAIRecord.RecordType.BookJournal:
                    ItemType itemType = this.GetItemType();

                    switch (itemType)
                    {
                        case ItemType.Monograph:
                        case ItemType.MultivolumeMonograph:
                            genre = "monograph";
                            break;
                        case ItemType.Serial:
                            genre = "serial";
                            break;
                        default:
                            genre = "monograph";
                            break;
                    }
                    break;
                case OAIRecord.RecordType.Issue:
                    genre = "volume";
                    break;
                case OAIRecord.RecordType.Article:
                case OAIRecord.RecordType.Segment:
                    genre = "article";
                    break;
            }

            sb.Append("\t<olef:level>" + HttpUtility.HtmlEncode(genre) + "</olef:level>\n");

            return sb.ToString();
        }

        public string GetGuidElement()
        {
            return "\t<olef:guid>" + HttpUtility.HtmlEncode(_oaiRecord.Url) + "</olef:guid>\n";
        }

        public string GetParentGuidElement()
        {
            string parentElement = string.Empty;

            if (!string.IsNullOrWhiteSpace(_oaiRecord.ParentUrl))
            {
                parentElement = "\t<olef:parentGUID>" + HttpUtility.HtmlEncode(_oaiRecord.ParentUrl) + "</olef:parentGUID>\n";
            }
            return parentElement;
        }

        #endregion Elements

        #region Utils

        /// <summary>
        /// Determine what type of item we're outputting (article or book, monograph or serial)
        /// </summary>
        /// <returns></returns>
        private ItemType GetItemType()
        {
            if (String.IsNullOrEmpty(_oaiRecord.MarcLeader)) return ItemType.Monograph;
            if (_oaiRecord.MarcLeader.Length < 8) return ItemType.Monograph;

            ItemType type = ItemType.Monograph;
            switch (_oaiRecord.MarcLeader.Substring(7, 1))
            {
                case "a":
                    type = ItemType.MultivolumeMonograph;
                    break;
                case "m":
                    type = ItemType.Monograph;
                    break;
                case "b":
                case "s":
                    type = ItemType.Serial;
                    break;
                case "c":
                case "d":
                case "i":
                    type = ItemType.Monograph;
                    break;
            }

            return type;
        }

        private string GetOlefPageType(string typeDescription)
        {
            // Default to "page"
            string pageType = Enum.GetName(typeof(PageType), PageType.page);

            typeDescription = typeDescription.ToLower();
            if (Enum.IsDefined(typeof(PageType), typeDescription))
            {
                // Use the string exactly as supplied (it matches an OLEF page type)
                pageType = typeDescription;
            }
            else
            {
                // Determine if we have a variation on an OLEF page type
                if (typeDescription.Contains("title")) pageType = Enum.GetName(typeof(PageType), PageType.title);
                else if (typeDescription.Contains("illustration")) pageType = Enum.GetName(typeof(PageType), PageType.figure);
                else if (typeDescription.Contains("map")) pageType = Enum.GetName(typeof(PageType), PageType.figure);
                else if (typeDescription.Contains("index")) pageType = Enum.GetName(typeof(PageType), PageType.index);
                else if (typeDescription.Contains("blank")) pageType = Enum.GetName(typeof(PageType), PageType.blank);
                else if (typeDescription.Contains("foldout")) pageType = Enum.GetName(typeof(PageType), PageType.foldout);
            }

            return pageType;
        }

        private enum ItemType
        {
            Article,
            Monograph,
            MultivolumeMonograph,
            Serial
        }

        private enum PageType
        {
            cover,
            frontcover,
            backcover,
            imprint,
            index,
            title,
            halftitle,
            blank,
            figure,
            foldout,
            page
        }

        #endregion Utils

    }
}
