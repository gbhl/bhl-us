using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {

        #region AnnotatedPage methods

        public AnnotatedPage AnnotatedPageSave(int annotatedItemId, string externalIdentifier,
            int annotatedPageTypeID, string pageNumber)
        {
            AnnotatedPageDAL dal = new AnnotatedPageDAL();
            AnnotatedPage page = dal.AnnotatedPageSelectByExternalIdentifer(null, null,
                externalIdentifier, annotatedItemId);

            if (page != null)
            {
                page.AnnotatedPageTypeID = annotatedPageTypeID;
                page.PageNumber = pageNumber;
                page = dal.AnnotatedPageUpdateAuto(null, null, page);
            }
            else
            {
                page = new AnnotatedPage();
                page.ExternalIdentifier = externalIdentifier;
                page.AnnotatedItemID = annotatedItemId;
                page.AnnotatedPageTypeID = annotatedPageTypeID;
                page.PageNumber = pageNumber;
                page = dal.AnnotatedPageInsertAuto(null, null, page);
            }
            return page;
        }

        public AnnotatedPage AnnotatedPageSelectByPageID(int pageID)
        {
            AnnotatedPageDAL dal = new AnnotatedPageDAL();
            AnnotatedPage page = dal.AnnotatedPageSelectByPageID(null, null, pageID);
            return page;
        }

        #endregion AnnotatedPage methods

        #region AnnotatedPageCharacteristic methods

        public bool AnnotatedPageCharacteristicDeleteByPageID(int annotatedPageId)
        {
            return new AnnotatedPageCharacteristicDAL().AnnotatedPageCharacteristicDeleteByPageID(
                null, null, annotatedPageId);
        }

        public AnnotatedPageCharacteristic AnnotatedPageCharacteristicByPageID(int annotatedPageId)
        {
            return new AnnotatedPageCharacteristicDAL().AnnotatedPageCharacteristicSelectByPageID(null, null, "BHL", annotatedPageId);
        }

        public AnnotatedPageCharacteristic AnnotatedPageCharactersticSave(int annotatedPageId,
            string characteristicDetail)
        {
            AnnotatedPageCharacteristic pageCharacteristic = new AnnotatedPageCharacteristic();
            pageCharacteristic.AnnotatedPageID = annotatedPageId;
            pageCharacteristic.CharacteristicDetail = characteristicDetail;
            pageCharacteristic.CharacteristicDetailClean = _parseCharacteristicDetail(characteristicDetail);
            return new AnnotatedPageCharacteristicDAL().AnnotatedPageCharacteristicInsertAuto(null, null,
                pageCharacteristic);
        }

        private static string _parseCharacteristicDetail(string characteristicDetail)
        {
            _formatEditionForPageCharacteristic(ref characteristicDetail);
            _parseFeatureMarkupsForContent(ref characteristicDetail);
            _parseTNotesForPageCharacteristic(ref characteristicDetail);
            _parseIconsForPageCharacteristic(ref characteristicDetail);
            _tagItalics(ref characteristicDetail);
            _tagEditorBrackets(ref characteristicDetail);
            _parsePhysicalCharacteristics(ref characteristicDetail);
            return characteristicDetail;
        }

        /// <summary>
        /// calls some of the same methods as formatting Edition for Annotations, just fewer
        /// </summary>
        private static void _formatEditionForPageCharacteristic(ref string characteristicDetail)
        {
            ///.f???? [Darwin's final end-notes/slips:
            ///    f0?   end-note (f00 'only') 
            ///    f1?   end-slip (f10 'only')
            ///    last 2 digits numbering the sides]

            string pattern_end = @"(.*?)(\.f)(\d{4})(.*)";
            string[] tokens_end = Regex.Split(characteristicDetail, pattern_end);
            if (tokens_end.Length > 1)
            {
                int end_type = int.Parse(tokens_end[3].Substring(0, 1)),
                    end_num = int.Parse(tokens_end[3].Substring(1, 1)),
                    side_num = int.Parse(tokens_end[3].Substring(2, 2));

                StringBuilder sb_end = new StringBuilder();
                switch (end_type)
                {
                    case 0:
                        sb_end.Append(" End Note ");
                        break;
                    case 1:
                        sb_end.Append(" End Slip ");
                        break;
                }
                if (sb_end.Length > 0 && end_num > 0)
                {
                    sb_end.Append(end_num);
                }

                if (side_num > 0)
                    sb_end.Append(", side ").Append(side_num);

                sb_end.Insert(0, tokens_end[1]);
                sb_end.Append(tokens_end[4]);
                characteristicDetail = sb_end.ToString();
            }

            //string[] patterns = { 
            //                        "\\+([^=\\r\\n]*)",
            //                        "=a([^=\\r\\n]*)",
            //                        "=t([^=\\r\\n]*)",
            //                        "=e([^=\\r\\n]*)",
            //                        "=v([^=\\r\\n]*)",
            //                        "=p([^=\\r\\n]*)",
            //                        "=d([^=\\r\\n]*)",
            //                        "=l([^=\\r\\n]*)",
            //                        "=b([^=\\r\\n]*)",
            //                        "=x([^=\\r\\n]*)",
            //                    };

            //foreach (string expression in patterns)
            //{
            //    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(expression);
            //    System.Text.RegularExpressions.Match match = regex.Match(characteristicDetail);
            //    if (match.Groups.Count > 1)
            //        Console.Write(match.Groups.Count);
            //}

            // Read the information from the line
            //string pattern_book = @"(.*?)(\\n\S+)(.*)";
            //string[] tokens_book = Regex.Split(characteristicDetail, pattern);
            //string pattern_pubDetails = @"(.*?)(\\n\S+)(.*)";
            //string[] tokens_pubDetails = Regex.Split(characteristicDetail, pattern);
            //string pattern_book = @"(.*?)(\\n\S+)(.*)";
            //string[] tokens_book = Regex.Split(characteristicDetail, pattern);
            //string pattern_book = @"(.*?)(\\n\S+)(.*)";
            //string[] tokens_book = Regex.Split(characteristicDetail, pattern);
            //string pattern_book = @"(.*?)(\\n\S+)(.*)";
            //string[] tokens_book = Regex.Split(characteristicDetail, pattern);

            //string externalId = this.RegExGetValue(line, "\\+([^=\\r\\n]*)", 2);
            //string author = this.RegExGetValue(line, "=a([^=\\r\\n]*)", 2);
            //string title = this.RegExGetValue(line, "=t([^=\\r\\n]*)", 2);
            //string edition = this.RegExGetValue(line, "=e([^=\\r\\n]*)", 2);
            //string volume = this.RegExGetValue(line, "=v([^=\\r\\n]*)", 2);
            //string pubDetails = this.RegExGetValue(line, "=p([^=\\r\\n]*)", 2);
            //string date = this.RegExGetValue(line, "=d([^=\\r\\n]*)", 2);
            //string location = this.RegExGetValue(line, "=l([^=\\r\\n]*)", 2);
            //string isBeagleEra = this.RegExGetValue(line, "=b([^=\\r\\n]*)", 2);
            //string inscription = this.RegExGetValue(line, "=x([^=\\r\\n]*)", 2);
            _parseBook(ref characteristicDetail);
            _parseVolume(ref characteristicDetail);
            _parsePart(ref characteristicDetail);
        }      

        private static void _parseIconsForPageCharacteristic(ref string text)
        {
            foreach (KeyValuePair<string, string> kvp in T_NOTES)
            {
                //blank out - decoding handled by tnotes instead?
                if (Regex.IsMatch(text, kvp.Key))
                {
                    StringBuilder sb_icons = new StringBuilder();
                    sb_icons.Append(@"""icon\d+\.").Append(kvp.Key).Append(@"""");
                    text = Regex.Replace(text, sb_icons.ToString(), "");
                }
            }
        }

        private static void _parseTNotesForPageCharacteristic(ref string text)
        {
            foreach (KeyValuePair<string, string> kvp in BHLProvider.T_NOTES)
            {
                if (Regex.IsMatch(text, kvp.Key))
                {
                    StringBuilder sb_tnote = new StringBuilder();
                    sb_tnote.Append(@"\\").
                             Append(kvp.Key);
                    text = Regex.Replace(text, sb_tnote.ToString(), kvp.Value);
                }
            }
        }

        #endregion AnnotatedPageCharacteristic methods
    }
}
