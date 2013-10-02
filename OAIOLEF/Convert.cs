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

        #region ToString

        public new String ToString()
        {
            StringBuilder sb = new StringBuilder();

            //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            sb.Append("<olef:olef xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:olef=\"http://www.bhl-europe.eu/bhl-schema/v1/\" xmlns:mods=\"http://www.loc.gov/mods/v3\"  xmlns:dwc=\"http://rs.tdwg.org/dwc/terms/\">\n");
            sb.Append("<olef:element>\n");

            sb.Append(this.GetBibliographicInformationElement());
            sb.Append(this.GetItemInformationElement());
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

        public string GetItemInformationElement()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\t<olef:itemInformation>\n");
            sb.Append("\t\t<olef:files>\n");


            
            // TODO: Add information about files, pages, and names here



            sb.Append("\t\t</olef:files>\n");
            sb.Append("\t</olef:itemInformation>\n");

            return sb.ToString();
        }

        public string GetGuidElement()
        {
            return "\t<olef:guid>" + HttpUtility.HtmlEncode(_oaiRecord.Url) + "</olef:guid>\n";
        }

        public string GetParentGuidElement()
        {

            // TODO: Check if parent Guid is available (item and segments only), and output the olef:parentGUID element if necessary


            return string.Empty;
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

        private enum ItemType
        {
            Article,
            Monograph,
            MultivolumeMonograph,
            Serial
        }

        #endregion Utils

    }
}
