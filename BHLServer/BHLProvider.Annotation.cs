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
        #region Annotation methods

        public List<Annotation> AnnotationsSelectByItemID(int itemID)
        {
            AnnotationDAL dal = new AnnotationDAL();
            return dal.AnnotationsSelectByItemID(null, null, itemID);
        }

        public List<CustomDataRow> AnnotationRelationSelectByAnnotationID(int annotationID)
        {
            AnnotationDAL dal = new AnnotationDAL();
            return dal.AnnotationRelationSelectByAnnotationID(null, null, annotationID);
        }

        #region Annotation Parse
        /// <summary>
        /// This was set up to use reference params, since the same parsing used for Display text
        /// might also apply to Clean text, and references would allow that parsing to be applied
        /// to both params simultaneously.  As it turns out, we only need the word blocks for Clean 
        /// text currently, which is handled in _tagWordBlock.  There is, however, a "$$...|h*" encoding 
        /// for t-notes which occurs within word blocks.  
        /// 
        /// The reference format has been kept in place in case this requirement changes.
        /// </summary>
        private static void _parseAnnotationText(ref string txtToDisplay, ref string txtToClean)
        {
            ParseRelatedPages(ref txtToDisplay);
            FormatEditionForAnnotation(ref txtToDisplay);
            TagTextualFeatureMarkupsForReference(ref txtToDisplay);
            TagWordBlock(ref txtToDisplay, ref txtToClean);
            ParseTNoteReferences(ref txtToDisplay, ref txtToClean);
            ParseMultiLines(ref txtToDisplay);
            ParseIconsForAnnotation(ref txtToDisplay);
            ParsePhysicalCharacteristics(ref txtToDisplay);
            YankFootNote(ref txtToDisplay);

            // Hacks, hacks, and more hacks

            // Remove non-print 'p_' designations
            txtToDisplay = txtToDisplay.Replace("p_", "");
            // If any line following a ling break starts with '@', treat the '@' as non-printing
            txtToDisplay = txtToDisplay.Replace("<br>@", "<br>");
            // Wrap unicode for "lines counted from the bottom" in a separate set of span tags
            txtToDisplay = txtToDisplay.Replace("&#8657;", "<span class='countup'>&#8657;</span>");
            // Remove extra spaces added after the opening "inserted" and "deleted" braces
            txtToDisplay = txtToDisplay.Replace("&#171; ", "&#171;");
            txtToDisplay = txtToDisplay.Replace("&#8249; ", "&#8249;");
            // Replace underscores with a blank space
            txtToDisplay = txtToDisplay.Replace("_", " ");
            // Replace top-margin--bottom-margin entries with whole-margin
            txtToDisplay = txtToDisplay.Replace(
                "<span class=\"line-position\">top-margin</span>&#8212;<span class=\"line-position\">bottom-margin</span>",
                "<span class=\"line-position\">whole-margin</span>");
        }

        /// <summary>
        /// remove all unicode and html tags for Clean Text
        /// </summary>
        private static void FormatCleanText(ref string txtClean)
        {
            txtClean = Regex.Replace(txtClean, @"&#?.+?;", " ");
            txtClean = Regex.Replace(txtClean, "<.+?>", " ");
        }

        #region format edition

        private static void FormatEditionForAnnotation(ref string txtToDisplay)
        {
            ParseBook(ref txtToDisplay);
            ParseVolume(ref txtToDisplay);
            ParsePart(ref txtToDisplay);
            ParseFrontSlipOrNote(ref txtToDisplay);
            ParseRoman(ref txtToDisplay);
            ParsePageCount(ref txtToDisplay);
            ParseEndMatter(ref txtToDisplay);
            ParseExtra(ref txtToDisplay);
            ParseFinalEndSlipOrNote(ref txtToDisplay);
            ParseAbstract(ref txtToDisplay);
            ParseIndex(ref txtToDisplay);
        }

        private static void ParseBook(ref string txtToDisplay)
        {
            txtToDisplay = Regex.Replace(txtToDisplay, @"(#|\\)n\d{4}", " ");
        }

        private static void ParseVolume(ref string txtToDisplay)
        {
            string pattern_volume = @"(.*?)(\.v)(\d{2})(\..*)";
            string[] tokens_volume = Regex.Split(txtToDisplay, pattern_volume);

            while (tokens_volume.Length > 1)
            {
                //append preceding unparsed
                StringBuilder sb_volume = new StringBuilder(tokens_volume[1]);

                //parse volume data
                sb_volume.Append("<span class=\"pgcharvolume\">");
                if (tokens_volume[3] == "00")
                    sb_volume.Append("[only volume]");
                else
                    sb_volume.Append("volume ").Append(tokens_volume[3]);
                sb_volume.Append("</span>");

                //append rest of unparsed string
                sb_volume.Append(tokens_volume[4]);

                txtToDisplay = sb_volume.ToString();
                tokens_volume = Regex.Split(txtToDisplay, pattern_volume);
            }
        }

        private static void ParsePart(ref string txtToDisplay)
        {
            txtToDisplay = Regex.Replace(txtToDisplay, @"\.p\d{2}", " ");
        }

        private static void ParseFrontSlipOrNote(ref string txtToDisplay)
        {
            string pattern_frontSlipOrNote = @"\\\\(.*?)(\.a)(0|1)(\d)(.*)";
            string[] tokens_frontSlipOrNote = Regex.Split(txtToDisplay, pattern_frontSlipOrNote);

            while (tokens_frontSlipOrNote.Length > 1)
            {
                StringBuilder sb_frontSlipOrNote = new StringBuilder();

                sb_frontSlipOrNote.Append("Front ").Append(tokens_frontSlipOrNote[2] == "0" ? "Slip " : "Note ");

                if (int.Parse(tokens_frontSlipOrNote[3]) == 0)
                {
                    sb_frontSlipOrNote.Insert(0, '[');
                    sb_frontSlipOrNote.Append("only]");
                }
                else
                {
                    sb_frontSlipOrNote.Append(tokens_frontSlipOrNote[3]);
                }

                int side_num = 0;
                if (tokens_frontSlipOrNote[4].Length >= 2)
                    side_num = int.Parse(tokens_frontSlipOrNote[4].Substring(0, 2));
                if (side_num > 0)
                    sb_frontSlipOrNote.Append(", side ").Append(side_num);

                txtToDisplay = sb_frontSlipOrNote.ToString();
                tokens_frontSlipOrNote = Regex.Split(txtToDisplay, pattern_frontSlipOrNote);
            }
        }

        private static void ParseRoman(ref string txtToDisplay)
        {
            //needs error checking
            string pattern_roman = @"\\\\(.*?)(\.b)(\d+)(r)(.*)";
            string[] tokens_roman = Regex.Split(txtToDisplay, pattern_roman);

            while (tokens_roman.Length > 1)
            {
                StringBuilder sb_roman = new StringBuilder();

                sb_roman.Append(tokens_roman[1]).Append(Server.RomanNumerals.ToRomanNumeral(int.Parse(tokens_roman[3]))).Append(tokens_roman[5]);

                txtToDisplay = sb_roman.ToString();
                tokens_roman = Regex.Split(txtToDisplay, pattern_roman);
            }
        }

        private static void ParsePageCount(ref string txtToDisplay)
        {
            txtToDisplay = Regex.Replace(txtToDisplay, @"\.c\d+", " ");
        }

        private static void ParseEndMatter(ref string txtToDisplay)
        {
            txtToDisplay = Regex.Replace(txtToDisplay, @"\.d\d+", " ");
        }

        private static void ParseExtra(ref string txtToDisplay)
        {
            txtToDisplay = Regex.Replace(txtToDisplay, @"\.e\d+", " ");
        }

        private static void ParseFinalEndSlipOrNote(ref string txtToDisplay)
        {
            string pattern_finalEndSlipOrNote = @"\\\\(.*?)(\.f)(0|1)(\d)(.*)";
            string[] tokens_finalEndSlipOrNote = Regex.Split(txtToDisplay, pattern_finalEndSlipOrNote);

            while (tokens_finalEndSlipOrNote.Length > 1)
            {
                StringBuilder sb_finalEndSlipOrNote = new StringBuilder();

                sb_finalEndSlipOrNote.Append("End-").Append(int.Parse(tokens_finalEndSlipOrNote[2]) == 0 ? "Slip " : "Note ");

                if (int.Parse(tokens_finalEndSlipOrNote[3]) == 0)
                {
                    sb_finalEndSlipOrNote.Insert(0, '[');
                    sb_finalEndSlipOrNote.Append("only]");
                }
                else
                    sb_finalEndSlipOrNote.Append(tokens_finalEndSlipOrNote[3]);

                int side_num = 0;
                if (tokens_finalEndSlipOrNote[4].Length >= 2)
                    side_num = int.Parse(tokens_finalEndSlipOrNote[4].Substring(0, 2));
                if (side_num > 0)
                    sb_finalEndSlipOrNote.Append(", side ").Append(side_num);

                txtToDisplay = sb_finalEndSlipOrNote.ToString();

                tokens_finalEndSlipOrNote = Regex.Split(txtToDisplay, pattern_finalEndSlipOrNote);
            }
        }

        private static void ParseAbstract(ref string txtToDisplay)
        {
            string pattern_abstract = @"\\\\(.*?)(\.g)(\d{4})(.*)";
            string[] tokens_abstract = Regex.Split(txtToDisplay, pattern_abstract);

            while (tokens_abstract.Length > 1)
            {
                StringBuilder sb_abstract = new StringBuilder();

                if (tokens_abstract[2].Equals("0000"))
                    sb_abstract.Append("[only abstract]");
                else
                    sb_abstract.Append("Abstract ").Append(tokens_abstract[2]);

                txtToDisplay = sb_abstract.ToString();

                tokens_abstract = Regex.Split(txtToDisplay, pattern_abstract);
            }
        }

        private static void ParseIndex(ref string txtToDisplay)
        {
            string pattern_getTag = @"(^.*?)(:m\d{2}\.\w+)(.*$)";
            string[] tokens_getTag = Regex.Split(txtToDisplay, pattern_getTag);

            while (tokens_getTag.Length > 1)
            {
                string tag = tokens_getTag[2];
                StringBuilder sb_tag = new StringBuilder();
                sb_tag.Append("(.*?)(").Append(tag).Append(")(.*?)(").Append(tag).Append(")(.*)");
                Regex rgxIndex = new Regex(sb_tag.ToString());
                string[] tokens_index = rgxIndex.Split(txtToDisplay);

                if (tokens_index.Length > 1)
                {
                    StringBuilder sb_index = new StringBuilder();
                    sb_index.Append(tokens_index[1]).Append(" ").Append(tokens_index[3]).Append(" ").Append(tokens_index[5]);
                    txtToDisplay = sb_index.ToString();
                    tokens_getTag = Regex.Split(txtToDisplay, pattern_getTag);
                }
                else
                {
                    //only 1 instance of tag.  Delete, or keep?
                    return;
                }
            }
        }




        public static string ConvertToRoman(int num)
        {
			if (num <= 0)
				return "";
			if (num >= 1000)
				return "M" + ConvertToRoman(num - 1000);
			else if (num >= 900)
				return "CM" + ConvertToRoman(num - 900);
			else if (num >= 500)
				return "D" + ConvertToRoman(num - 500);
			else if (num >= 100)
				return "C" + ConvertToRoman(num - 100);
			else if (num >= 90)
				return "CX" + ConvertToRoman(num - 90);
			else if (num >= 50)
				return "L" + ConvertToRoman(num - 50);
			else if (num >=10)
				return "X" + ConvertToRoman(num - 10);
			else if (num >= 9)
				return "IX" + ConvertToRoman(num - 9);
			else if (num >= 5)
				return "V" + ConvertToRoman(num - 5);
			else if (num >= 4)
				return "IV" + ConvertToRoman(num - 4);
			else 
				return "I" + ConvertToRoman(num - 1);
		}
        #endregion
        /// <summary>
        /// Denotes a range of lines delimited by unicode &#8212; 
        /// with optional &#8657; (to indicate lines from bottom)
        /// </summary>
        private static void ParseMultiLines(ref string text)
        {
            string pattern = @"(^.*?)(\b)(\d+&#8212;\d+|\d+&#8212;&#8657;\d+)(\b.*)";
			string[] tokens = Regex.Split(text, pattern);
            
			if (tokens.Length > 1)
			{
                StringBuilder sb = new StringBuilder();
                sb.Append(tokens[1]).
                    Append("<span class=\"line-count\">lines ").
                    Append(tokens[3]).
                    Append(tokens[2].Length > 0 ? " from bottom" : "").
                    Append("</span>");                                   //close tag

                ParseMultiLines(ref tokens[4]);                                        //recursively parse remainder of string
                sb.Append(tokens[4]);

                text = sb.ToString(); 
			}
		}

        /// <summary>
        /// This is currently the only method to handle parsing data for the column AnnotationTextClean.
        /// Since AnnotationTextClean currently only requires the word block, textClean is simply set to
        /// whatever is parsed out of textDisplay.  
        /// 
        /// word blocks are encoded by "w $...$"
        /// 
        /// t-note encoding involves a "$$...|h*" pattern, and may occur within a word block.  It is presumed
        /// that _parseFeatureMarkups has already been called and formatted this out, but the regular
        /// expression is overloaded to account for the extra "$$" just in case.
        /// </summary>
        private static void TagWordBlock(ref string txtDisplay, ref string txtClean)
        {
            string pattern = @"(^.*?)(\$\s*)(\${0,2}.+?)(\$)(.*$)";
            //string pattern = @"(^.*?)(\$)(.*?)(\$)(.*$)";
            string[] tokens = Regex.Split(txtDisplay, pattern);
            if (tokens.Length > 1)
            {
                StringBuilder sb_Display = new StringBuilder(),
                              sb_Clean = new StringBuilder(); 

                // parse non-word block text for taglines
                ParseNonWordBlock(ref tokens[1]);
                sb_Display.Append(tokens[1]);                

                //wrap word block
                sb_Display.Append("<span class=\"word-block\">").
                Append(tokens[3]).
                Append("</span>");

                //set Clean to word block
                sb_Clean.Append(tokens[3]);

                //parse remainder
                if (tokens[5].Length > 0)
                {
                    string stub = tokens[5];
                    TagWordBlock(ref tokens[5], ref stub);
                    sb_Display.Append(tokens[5]);
                    sb_Clean.Append(stub);
                }
                txtDisplay = sb_Display.ToString();
                txtClean = sb_Clean.ToString();
            }
            else
            {
                // no word blocks; parse for taglines
                ParseNonWordBlock(ref txtDisplay);             
                txtClean = String.Empty;
            }
        }

        private static void ParseNonWordBlock(ref string text)
        {
            TagLinePosition(ref text);
            TagCompositePosition(ref text);
            TagScoredLines(ref text);
            ParseSingleLine(ref text);
            ParseLineBreaks(ref text);
        }

        /// <summary>
        /// Lookup currently inlined, vs. database
        /// </summary>
        private static void TagLinePosition(ref string text)
        {
            Dictionary<string, string> _LINE_POSITION = new Dictionary<string, string>(){
                {"a", "at"}, 
                {"b", "bottom-margin"}, 
                {"c", "crossing-out"},          // was "crossed/deleted"
                {"d", "drawing"},
                {"n", "unmarked"},              // was "blank"
                {"t", "top-margin"},
                {"u", "underline"},             // was "underlined"
                {"w", "annotation"},            // was "words"
                {"p", "mark resembling"},       // was "[punctuation]"
                {"z", "apparently unintentional mark"}
            };

            //loop through keys
            foreach (KeyValuePair<string, string> kvp in _LINE_POSITION)
            {
                string pattern = @"(^|.*?\s+|.*?&#8212;|.*?&#8657;)(" + kvp.Key + ")($|\\s+.*|&#8212;.*)";
                string[] tokens = Regex.Split(text, pattern);

                //exhaust matches for this key
                while (tokens.Length > 1)
                {
                    //may possibly replace with blank space
                    StringBuilder sb = new StringBuilder();
                    sb.Append(tokens[1]).
                       Append("<span class=\"line-position\">").
                       Append(kvp.Value).Append("</span>").
                       Append(tokens[3]);
                    text = sb.ToString();
                    tokens = Regex.Split(text, pattern);
                }
            }
        }

        /// <summary>
        /// Make sure that composite position-designators like "top-margin--line 8" and "line 20--bottom-margin"
        /// do not omit the "line" term before the line number.
        /// </summary>
        /// <param name="text"></param>
        private static void TagCompositePosition(ref string text)
        {
            string pattern = @"(^|.*)(at|bottom-margin|crossing-out|drawing|unmarked|top-margin|underline|annotation|mark resembling|apparently unintentional mark)(</span>&#8212;)(\d+)($|.*)";
            string[] tokens = Regex.Split(text, pattern);

            while (tokens.Length > 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(tokens[0]).
                    Append(tokens[1]).
                    Append(tokens[2]).
                    Append(tokens[3]).
                    Append("<span class=\"line-count\">line ").Append(tokens[4]).Append("</span>").
                    Append(tokens[5]).
                    Append(tokens[6]);
                text = sb.ToString();
                tokens = Regex.Split(text, pattern);
            }

            pattern = @"(^|\D*)(\d+)(&#8212;<span class=.line-position.>)(at|bottom-margin|crossing-out|drawing|unmarked|top-margin|underline|annotation|mark resembling|apparently unintentional mark)($|.*)";
            tokens = Regex.Split(text, pattern);

            while (tokens.Length > 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(tokens[0]).
                    Append(tokens[1]).
                    Append("<span class=\"line-count\">line ").Append(tokens[2]).Append("</span>").
                    Append(tokens[3]).
                    Append(tokens[4]).
                    Append(tokens[5]).
                    Append(tokens[6]);
                text = sb.ToString();
                tokens = Regex.Split(text, pattern);
            }
        }

        private static void TagScoredLines(ref string text)
        {
            Dictionary<string, string> _SCORE_TAGS = new Dictionary<string, string>(){
                {"m\\+m\\+m", "multiple score"},
                {"m\\+m", "double score"},
                {"m\\+", "score at bottom of page that resumes on the next page"},
                {"m", "score"}
            };

            //loop through keys
            foreach (KeyValuePair<string, string> kvp in _SCORE_TAGS)
            {
                //string pattern = @"(^|.*?\d+\s*)(" + kvp.Key + ")($|\\W+.*)";
                //string pattern = @"(.*?\W+)(" + kvp.Key + @")(\W+.*|$)";
                //string pattern = @"(.*?\W+)(" + kvp.Key + @")([^&]\W+.*|$)";
                string pattern = @"(.*?\W+)(" + kvp.Key + @")([^a-zA-Z_0-9&].*|$)";
                string[] tokens = Regex.Split(text, pattern);

                //exhaust matches for this key
                while (tokens.Length > 1)
                {
                    //may possibly replace with blank space
                    StringBuilder sb = new StringBuilder();
                    sb.Append(tokens[1]).
                       Append("<span class=\"line-position\">").
                       Append(kvp.Value).Append("</span>").
                       Append(tokens[3]);
                    text = sb.ToString();
                    tokens = Regex.Split(text, pattern);
                }
            }
        }

        private static void ParseLineBreaks(ref string text)
        {
            text = Regex.Replace(text, @"\s+/\s+", "<br>");
        }

        /// <summary>
        /// Parse for single line markers
        /// Format is string of digits, optionally preceded by &#8657; (unicode marker for "lines from bottom")
        /// Have to filter out: "&#8212;" (for 2nd part of multiline instance) and 
        ///                     "Lines " (for 1st part of multiline instance, following formatting in _parseMultiLines -- 
        ///                               assumes _parseMultiLines has already been run)
        ///                     "from_\n" (for Related Pages)
        /// 
        /// </summary>
        private static void ParseSingleLine(ref string text)
        {
            string pattern_lineTag = @"(^\s*|.*?/\s*@?)(&#8657;|@?\b)(\d+)(?!\d*&#8212;)(.*)";
            string[] tokens_lineTag = Regex.Split(text, pattern_lineTag);
            
            StringBuilder sb = new StringBuilder();
            if (tokens_lineTag.Length > 1)
            {
                sb.Append(tokens_lineTag[1]).
                   Append(tokens_lineTag[2]).
                   Append("<span class=\"line-count\">").
                   Append(tokens_lineTag[3] == "0" ? "<!--0-->" : "line " + tokens_lineTag[3]).
                   Append("</span>");

                    //parse remainder
                   ParseSingleLine(ref tokens_lineTag[4]);
                   sb.Append(tokens_lineTag[4]);
                   text = sb.ToString();
            }
        }

        private static string ParseLineBreaksAndSingleLine(string text)
        {
            string pattern_lineBreak = @"(^.*?\s+)(\/)(\s+.*$)",
                   pattern_lineTag = @"(^|.*?\s+)(\d+)(\s+.*)";
            string[] tokens_lineBreak = Regex.Split(text, pattern_lineBreak),
                     tokens_lineTag;
            while (tokens_lineBreak.Length > 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(tokens_lineBreak[1]).Append("<br>");                      //put in line break
                tokens_lineTag = Regex.Split(tokens_lineBreak[3], pattern_lineTag); //check for line number
                if (tokens_lineTag.Length > 1)
                {
                    sb.Append("<span class=\"line-count\">line ").
                       Append(tokens_lineTag[2]).
                       Append("</span>").
                       Append(tokens_lineTag[3]);
                }
                else
                    sb.Append(tokens_lineBreak[3]);

                text = sb.ToString();
                tokens_lineBreak = Regex.Split(text, pattern_lineBreak);
            }
            return text;
        }

        #region textual feature markups
        /// <summary>
        /// In Annotations, we may have instances of markup for tnotes, as well as in the content itself
        /// ex:  |n0815.v00.p00.c0231:m02= &#8657;9&#8212;5 m / &#8657;7 w \m1a $ (\ua\v) $ / b w $ $$m1a|h1\f(\ua\v)\g\_t01|_ &#160;&#160; No such \ugret\v change has been effected in reclaiming the common Ox of Europe \N V. further on.&#8212; $ 
        /// the instance of "$$m1a|h1" needs to be deleted since it will be explained in the corresponding t-note.
        /// "\m1a" needs to be translated, however.
        /// </summary>
        private static void TagTextualFeatureMarkupsForReference(ref string txtDisplay)
        {
            ParseFeatureMarkupsForReference(ref txtDisplay);
            ParseFeatureMarkupsForContent(ref txtDisplay);
            ParseTags(ref txtDisplay);
        }

        private static void ParseTags(ref string txtDisplay)
        {
            TagEditorBrackets(ref txtDisplay);
            TagItalics(ref txtDisplay);
            TagFaint(ref txtDisplay);
            TagLineBreaks(ref txtDisplay);
            Tag_paragraphs(ref txtDisplay);
            Tag_doubleUnderlined(ref txtDisplay);
            Tag_insertWithCaret(ref txtDisplay);
            Tag_insertWithoutCaret(ref txtDisplay);
            Tag_genderSign(ref txtDisplay, "male", "&#9794;");
            Tag_genderSign(ref txtDisplay, "female", "&#9792;");
            TagSuperAndSubScripts(ref txtDisplay);
            Tag_pinhole(ref txtDisplay);
            Tag_horizontalSpace(ref txtDisplay);
        }

        /// <summary>
        /// used by Annotation - simply remove instances of markups, since their translations
        /// will be linked by t-note in AnnotationNotes
        /// 
        /// markup instances currently appear as "$$...|h*"
        /// </summary>
        private static void ParseFeatureMarkupsForReference(ref string text)
        {
            text = Regex.Replace(text, @"\$\$.+?\|h.", " ");
        }

        /// <summary>
        /// Used for both AnnotationNote and Page Characteristics
        /// </summary>
        private static void ParseFeatureMarkupsForContent(ref string text)
        {
            //first have to parse out "$$...|h*" tokens
            string pattern = @"(.*?)(\$\$)(.+?)(\|h.)(.*)";
            string[] tokens = Regex.Split(text, pattern);
            while (tokens.Length > 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(tokens[1]).
                   Append("\\").                        //add slash for key-matching
                   Append(tokens[3]).
                   Append(" ").
                   Append(tokens[5]);
                text = sb.ToString();
                tokens = Regex.Split(text, pattern);
            }

            foreach (KeyValuePair<string, string> kvp in MARKUPS)
            {
                text = Regex.Replace(text, kvp.Key, kvp.Value);
            }
        }

        private static void TagEditorBrackets(ref string text)
        {
            string pattern_editorBrackets = @"(.*?)(\\p8)(.+?)(\\p9)(.*)";
            string[] tokens_editorBrackets = Regex.Split(text, pattern_editorBrackets);

            if (tokens_editorBrackets.Length > 1)
            {
                StringBuilder sb_editorBrackets = new StringBuilder();

                sb_editorBrackets.Append(tokens_editorBrackets[1]).
                                  Append("[").
                                  Append(tokens_editorBrackets[3]).
                                  Append("]").
                                  Append(tokens_editorBrackets[5]);

                text = sb_editorBrackets.ToString();
			}
        }

        private static void TagItalics(ref string text)
        {
            string pattern_italics = @"(.*?)(\\a)(.+?)(\\c)(.*)";
            string[] tokens_italics = Regex.Split(text, pattern_italics);

            while (tokens_italics.Length > 1)
            {
                StringBuilder sb_italics = new StringBuilder();
                sb_italics.Append(tokens_italics[1]).
                           Append("<i>").
                           Append(tokens_italics[3]).
                           Append("</i>").
                           Append(tokens_italics[5]);
                text = sb_italics.ToString();
                tokens_italics = Regex.Split(text, pattern_italics);
			}
        }

        private static void TagFaint(ref string text)
        {
            string pattern_faint = @"(.*?)(\\f)(.+?)(\\g)(.*)";
            string[] tokens_faint = Regex.Split(text, pattern_faint);

            while (tokens_faint.Length > 1)
            {
                StringBuilder sb_faint = new StringBuilder();
                sb_faint.Append(tokens_faint[1]).
                           Append("<span class=\"faint\">").
                           Append(tokens_faint[3]).
                           Append("</span>").
                           Append(tokens_faint[5]);
                text = sb_faint.ToString();
                tokens_faint = Regex.Split(text, pattern_faint);
            }
        }

        private static void TagLineBreaks(ref string text)
        {
            string pattern_lineBreaks = @"(.*?)(\\b)(.*)";
            string[] tokens_lineBreaks = Regex.Split(text, pattern_lineBreaks);

            while (tokens_lineBreaks.Length > 1)
            {
                StringBuilder sb_lineBreaks = new StringBuilder();
                sb_lineBreaks.Append(tokens_lineBreaks[1]).Append("<br>").Append(tokens_lineBreaks[3]);

                text = sb_lineBreaks.ToString();
                tokens_lineBreaks = Regex.Split(text, pattern_lineBreaks);
            }
        }

        private static void Tag_paragraphs(ref string text)
        {
            string pattern_paragraphs = @"(.*?)(\\N)(.*)";
            string[] tokens_paragraphs = Regex.Split(text, pattern_paragraphs);
            while (tokens_paragraphs.Length > 1)
            {
                StringBuilder sb_paragraphs = new StringBuilder();
                sb_paragraphs.Append(tokens_paragraphs[1]).Append("<br>&nbsp;&nbsp;&nbsp;&nbsp;").Append(tokens_paragraphs[3]);

                text = sb_paragraphs.ToString();
                tokens_paragraphs = Regex.Split(text, pattern_paragraphs);
            }
        }

        private static void Tag_underlined(ref string text)
        {
            string pattern_underlined = @"(.*?)(\\u)(.+?)(\\v)(.*)";
            string[] tokens_underlined = Regex.Split(text, pattern_underlined);

            while (tokens_underlined.Length > 1)
            {
                StringBuilder sb_underlined = new StringBuilder();
                sb_underlined.Append(tokens_underlined[1]).
                              Append("<u>").
                              Append(tokens_underlined[3]).
                              Append("</u>").
                              Append(tokens_underlined[5]);

                text = sb_underlined.ToString();
                tokens_underlined = Regex.Split(text, pattern_underlined);
            }
        }

        private static void Tag_doubleUnderlined(ref string text)
        {
            string pattern_dblUnderlined = @"(.*?)(\\U)(.+?)(\\V)(.*)";
            string[] tokens_dblUnderlined = Regex.Split(text, pattern_dblUnderlined);

            while (tokens_dblUnderlined.Length > 1)
            {
                StringBuilder sb_dblUnderlined = new StringBuilder();
                sb_dblUnderlined.Append(tokens_dblUnderlined[1]).
                                 Append("<u><span style=\"border-bottom: 1px double #000;\">").
                                 Append(tokens_dblUnderlined[3]).
                                 Append("</span></u>").
                                 Append(tokens_dblUnderlined[5]);

                text = sb_dblUnderlined.ToString();
                tokens_dblUnderlined = Regex.Split(text, pattern_dblUnderlined);
            }
        }

        private static void Tag_insertWithCaret(ref string text)
        {
            string pattern_iwc = @"(.*?)(\/\^)(.+?)(\|\^)(.*)";
            string[] tokens_iwc = Regex.Split(text, pattern_iwc);

            while (tokens_iwc.Length > 1)
            {
                StringBuilder sb_iwc = new StringBuilder();
                sb_iwc.Append(tokens_iwc[1]).
                       Append("<span class=\"insert\"><b>^</b>").
                       Append(tokens_iwc[3]).
                       Append("</span>").
                       Append(tokens_iwc[5]);
                text = sb_iwc.ToString();
                tokens_iwc = Regex.Split(text, pattern_iwc);
            }
        }

        private static void Tag_insertWithoutCaret(ref string text)
        {
            string pattern_iwc = @"(.*?)(\/\/\^)(.+?)(\|\/\^)(.*)";
            string[] tokens_iwc = Regex.Split(text, pattern_iwc);

            while (tokens_iwc.Length > 1)
            {
                StringBuilder sb_iwc = new StringBuilder();
                sb_iwc.Append(tokens_iwc[1]).
                       Append("<span class=\"insert\">").
                       Append(tokens_iwc[3]).
                       Append("</span>").
                       Append(tokens_iwc[5]);
                text = sb_iwc.ToString();
                tokens_iwc = Regex.Split(text, pattern_iwc);
            }
        }

        private static void Tag_genderSign(ref string text, string gender, string symbol)
        {
            //should it be in database?
            Regex rgxGender = new Regex("(.*?)(\\\\\\[" + gender + "\\])(.*)");
            text = rgxGender.Replace(text, symbol);
        }

        /// <summary>
        /// pattern for superscript is "\+...\-"
        /// pattern for subscript is "\-...\+"
        /// Since the patterns have the same delimiters, we have to determine which we encounter first
        /// to apply the appropriate formatting
        /// </summary>
        private static void TagSuperAndSubScripts(ref string text)
        {
            string pattern_superscript = @"(.*?)(\\\+)(.+?)(\\-)(.*)",
                   pattern_subscript = @"(.*?)(\\-)(.+?)(\\\+)(.*)";

            int superIndex = text.IndexOf("\\+"),
                subIndex = text.IndexOf("\\-");

            if (superIndex < subIndex) //parse superscript first
            {
                string[] tokens_superscript = Regex.Split(text, pattern_superscript);

                if (tokens_superscript.Length > 1)
                {
                    StringBuilder sb_superscript = new StringBuilder();
                    sb_superscript.Append(tokens_superscript[1]).
                                   Append("<sup>").
                                   Append(tokens_superscript[3]).
                                   Append("</sup>");

                    //parse remainder
                    TagSuperAndSubScripts(ref tokens_superscript[5]);

                    sb_superscript.Append(tokens_superscript[5]);
                    text = sb_superscript.ToString();
                }
            }
            else if (subIndex < superIndex) // parse subscript first
            {
                string[] tokens_subscript = Regex.Split(text, pattern_subscript);

                if (tokens_subscript.Length > 1)
                {
                    StringBuilder sb_subscript = new StringBuilder();
                    sb_subscript.Append(tokens_subscript[1]).
                                 Append("<sub>").
                                 Append(tokens_subscript[3]).
                                 Append("</sub>");

                    //parse remainder
                    TagSuperAndSubScripts(ref tokens_subscript[5]);

                    sb_subscript.Append(tokens_subscript[5]);
                    text = sb_subscript.ToString();
                }		
            }
            //else:  they're equal (-1), so no matches
        }

        private static void Tag_pinhole(ref string text)
        {
            Regex.Replace(text, @"\\!", "&nbsp;<b>&dagger;</b>&nbsp;");
        }

        private static void Tag_horizontalSpace(ref string text)
        {
            Regex.Replace(text, @"\[space\]", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
        }

        #endregion

        /// <summary>
        /// replaces icon tags with desired lookup
        /// Lookup is currently inlined, vs. database
        /// </summary>
        private static void ParseIconsForAnnotation(ref string text)
        {
            foreach (KeyValuePair<string, string> kvp in BHLProvider.ICONS)
            {
                //blank out - decoding handled by tnotes instead?
                if (Regex.IsMatch(text, kvp.Key))
                {
                    text = Regex.Replace(text, kvp.Key, "");
                    text = Regex.Replace(text, @"icon\.end", "");
                }
            }
        }

        /// <summary>
        /// format tnote references.  Content it refers to is parsed in _parseTNoteContent
        /// superscripts references for Display, deletes entirely for Clean
        /// </summary>
        private static void ParseTNoteReferences(ref string txtDisplay, ref string txtClean)
        {
            //parse Display
            string reference_pattern = @"(.*?)(\\_)(t\d+)(\|_)(.*)";
            string[] reference_tokens = Regex.Split(txtDisplay, reference_pattern);
            while (reference_tokens.Length > 1)
            {
                StringBuilder sb_Display = new StringBuilder();
                sb_Display.Append(reference_tokens[1]).
                           Append("<sup class=\"tnote-ref\">").
                           Append(reference_tokens[3]).
                           Append("</sup>").
                           Append(reference_tokens[5]);
                txtDisplay = sb_Display.ToString();
                reference_tokens = Regex.Split(txtDisplay, reference_pattern);
            }

            //parse Clean
            txtClean = Regex.Replace(txtClean, @"\\_t\d+\|_", " ");
        }

        /// <summary>
        /// The tnote reference-content formatting was previously done on the same line.
        /// Now, the references are created in Annotation, while the content referred to
        /// is formatted in AnnotationNote.
        /// </summary>
        private static void ParseTNoteContent(ref string text)
        {
            string content_pattern = @"(.*?)(\\_)(t\d+)(.+?)(\|_)(.*)";
            string[] content_tokens = Regex.Split(text, content_pattern);
            while (content_tokens.Length > 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(content_tokens[1]).
                   Append("<div class=\"tnote-ref\"><span class=\"tnote\">").
                   Append(content_tokens[3]).
                   Append(" - ").
                   Append(content_tokens[4]).
                   Append("</span></div>").
                   Append(content_tokens[6]);
                text = sb.ToString();
                content_tokens = Regex.Split(text, content_pattern);
            }
        }


        private static void ParsePhysicalCharacteristics(ref string text)
        {
            Dictionary<string,string> _PHYSICAL_CHARACTERISTICS = new Dictionary<string,string>{
                 {@"\\Cl00p", "pale cream laid paper"}, 
                 {@"\\Cw00c", "cream wove paper"}, 
                 {@"\\Cw00d", "dark cream wove paper"}, 
                 {@"\\Cw03G", "grey-blue wove paper"},
                 {@"\\Cw00p", "pale cream wove paper"},
                 {@"\\Ct10t", "top edge torn"},
                 {@"\\Cr10s", "right edge serrated"}, 
                 {@"\\Cr10t", "right edge torn"}, 
                 {@"\\Cb10t", "bottom edge torn"}, 
                 {@"\\CB14F", "sometime folded"}, 
                 {@"\\Cm15a", "watermark"}, 
                 {@"\\CB19C", "indistinct circular embossing"},
            };

            foreach (KeyValuePair<string, string> kvp in _PHYSICAL_CHARACTERISTICS)
            {
                text = Regex.Replace(text, kvp.Key, kvp.Value);
            }
        }

        private static string ParseClassifiedTerms(string text)
        {
            Dictionary<string, string> _CLASSIFIED_TERMS = new Dictionary<string, string>(){
               {@"\W+faz\W+", "named fauna"},
               {@"\W+flz\W+", "flora"},
               {@"\W+gez\W+", "geological features"},
               {@"\W+grz\W+", "places"},
               {@"\W+huz\W+", "human populations"},
               {@"\W+tiz\W+", "epochs, strata"},
               {@"\W+znz\W+", "people and references"},
            };

            foreach (KeyValuePair<string, string> kvp in _CLASSIFIED_TERMS)
            {
                Regex.Replace(text, kvp.Key, kvp.Value);
            }

            return text;
        }

        private static void YankFootNote(ref string text)
        {
            string pattern_extern = @"(.*)(\\_\S+)(.*)";
            string[] extern_tokens = Regex.Split(text, pattern_extern);

            while (extern_tokens.Length > 1)
            {
                string pattern_intern = @"(\\_)(\w+)(\|_)(.*)";
                string formatted = "";
                string[] intern_tokens = Regex.Split(extern_tokens[2], pattern_intern);
                while (intern_tokens.Length > 1)
                {
                    StringBuilder sb_intern = new StringBuilder();
                    sb_intern.Append("(")
                             .Append(intern_tokens[2])
                             .Append(") ")
                             .Append(intern_tokens[4]);
                    formatted = sb_intern.ToString();
                    intern_tokens = Regex.Split(formatted, pattern_intern);
                }
                StringBuilder sb_extern = new StringBuilder();
                sb_extern.Append(extern_tokens[1]).Append(formatted).Append(extern_tokens[3]);
                text = sb_extern.ToString();
                extern_tokens = Regex.Split(text, pattern_extern);
            }

        }

        /// <summary>
        /// Parses for related pages (example format "from_\n0283.v00.p00.f1000")
        /// This should be run before _formatEdition since it looks for a similar format
        /// </summary>
        private static void ParseRelatedPages(ref string txtToDisplay)
        {
            string pattern = @"(.*?)(from_\\)(\S+)(.*)";

            string[] tokens = Regex.Split(txtToDisplay, pattern);
            while (tokens.Length > 1)
            {
                StringBuilder sbPrp = new StringBuilder();
                sbPrp.Append(tokens[1]);

                //build related page content
                sbPrp.Append(" from ");
                sbPrp.Append(BuildPageLinkByExternalIdentifier(tokens[3]));
                sbPrp.Append(tokens[4]);

                txtToDisplay = sbPrp.ToString();
                tokens = Regex.Split(txtToDisplay, pattern);
            }
        }

        private static string BuildPageLinkByExternalIdentifier(string str)
        {
            CustomDataRow row = new AnnotatedPageDAL().GetRelatedPageByExternalIdentifier(null, null, str);
            if (row != null)
            {
                StringBuilder sbBuildByEx = new StringBuilder();
                sbBuildByEx.Append("<span class=\"related-page\">");
                if (row["PageID"].Value != null) sbBuildByEx.Append("<a href=\"/page/").Append(row["PageID"].Value).Append("\" title=\"Page\">");
                sbBuildByEx.Append(row["AnnotatedPageTypeName"].Value);
                sbBuildByEx.Append(row["ConvertedPageNumber"].Value);
                if (row["PageID"].Value != null) sbBuildByEx.Append("</a>");
                sbBuildByEx.Append("</span>");
                return sbBuildByEx.ToString();
            }
            return "";
        }
        #endregion

        #endregion Annotation methods

        #region AnnotationNote methods

        private static void ParseAnnotationNote(ref string txtDisplay, ref string txtClean)
        {
            RemoveRelatedPageReference(ref txtDisplay);
            TagTextualFeatureMarkupsForReference(ref txtDisplay);
            ParseTNoteContent(ref txtDisplay);

            // Hacks, hacks, and more hacks

            // Replace underscores with a blank space
            txtDisplay = txtDisplay.Replace("_", " ");
        }

        private static void RemoveRelatedPageReference(ref string text)
        {
            //example of what to remove:  "[from_\n0815.v00.p00.f1000]"
            text = Regex.Replace(text, @"\[?from_\\\S+\]?", String.Empty);
        }


        public List<AnnotationNote> AnnotationNoteSelectByAnnotationID(int annotationID)
        {
            AnnotationNoteDAL dal = new AnnotationNoteDAL();
            return dal.AnnotationNoteSelectByAnnotationID(null, null, annotationID);
        }
        #endregion AnnotationNote methods

        #region Annotation_AnnotationConcept methods

        #endregion Annotation_AnnotationConcept methods

        #region AnnotationSubject methods

        public AnnotationSubjectCategory AnnotationSubjectCategorySelect(int annotationSubjectCategoryID)
        {
            return new AnnotationSubjectCategoryDAL().AnnotationSubjectCategorySelect(null, null, annotationSubjectCategoryID);
        }

        public List<CustomDataRow> AnnotationSubjectSelectByAnnotationID(int annotationId)
        {
            return new AnnotationSubjectDAL().AnnotationSubjectSelectByAnnotationID(null, null, annotationId);
        }

        public List<CustomDataRow> Annotation_AnnotationConceptSelectByAnnotationID(int annotationId)
        {
            return new Annotation_AnnotationConceptDAL().Annotation_AnnotationConceptSelectByAnnotationID(null, null, annotationId);
        }

        public List<SearchBookResult> SearchPageForAnnotationSubject(int annotationSubjectCategoryID, int subjectID)
        {
            return new AnnotationDAL().SearchPageForSubject(null, null, annotationSubjectCategoryID, subjectID);
        }

        public AnnotationSubject AnnotationSubjectSelect(int subjectID)
        {
            return new AnnotationSubjectDAL().AnnotationSubjectSelect(null, null, subjectID);
        }

        public List<AnnotationSubject> AnnotationSubjectSelectBySubjectText(string subjectText, int annotationSourceID)
        {
            return new AnnotationSubjectDAL().AnnotationSubjectSelectBySubjectText(null, null, subjectText, annotationSourceID);
        }

        #endregion AnnotationSubject methods

        #region AnnotationConcept methods

        public List<AnnotationConcept> AnnotationConceptSelectAll(int annotationSourceID)
        {
            return new AnnotationConceptDAL().AnnotationConceptSelectAll(null, null, annotationSourceID);
        }

        public List<AnnotationConcept> AnnotationConceptSelectByConceptText(string conceptText, int annotationSourceID)
        {
            return new AnnotationConceptDAL().AnnotationConceptSelectByConceptText(null, null, conceptText, annotationSourceID);
        }

        public AnnotationConcept AnnotationConceptSelectByCode(string annotationConceptCode)
        {
            return new AnnotationConceptDAL().AnnotationConceptSelectByCode(null, null, annotationConceptCode);
        }

        public List<SearchBookResult> SearchPageForAnnotationConcept(string annotationConceptCode)
        {
            return new AnnotationDAL().SearchPageForConcept(null, null, annotationConceptCode);
        }

        #endregion AnnotationConcept methods
    }
}
