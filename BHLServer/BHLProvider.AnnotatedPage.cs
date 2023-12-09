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

        public AnnotatedPage AnnotatedPageSelectByPageID(int pageID)
        {
            AnnotatedPageDAL dal = new AnnotatedPageDAL();
            AnnotatedPage page = dal.AnnotatedPageSelectByPageID(null, null, pageID);
            return page;
        }

        #endregion AnnotatedPage methods

        #region AnnotatedPageCharacteristic methods

        public AnnotatedPageCharacteristic AnnotatedPageCharacteristicByPageID(int annotatedPageId)
        {
            return new AnnotatedPageCharacteristicDAL().AnnotatedPageCharacteristicSelectByPageID(null, null, "BHL", annotatedPageId);
        }

        private static string _parseCharacteristicDetail(string characteristicDetail)
        {
            _formatEditionForPageCharacteristic(ref characteristicDetail);
            ParseFeatureMarkupsForContent(ref characteristicDetail);
            _parseTNotesForPageCharacteristic(ref characteristicDetail);
            _parseIconsForPageCharacteristic(ref characteristicDetail);
            TagItalics(ref characteristicDetail);
            TagEditorBrackets(ref characteristicDetail);
            ParsePhysicalCharacteristics(ref characteristicDetail);
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

            ParseBook(ref characteristicDetail);
            ParseVolume(ref characteristicDetail);
            ParsePart(ref characteristicDetail);
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
