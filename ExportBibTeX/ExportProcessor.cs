using BHL.Export.BibTeX.BHLWS;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BHL.Export.BibTeX
{
    public class ExportProcessor : IBHLExport
    {
        // Create a default logger for use in this class
        private ExportLogger _log = new ExportLogger();

        private ConfigParms configParms = new ConfigParms();
        private Dictionary<string, int> _stats = new Dictionary<string, int>();
        private List<string> _errors = new List<string>();

        public Dictionary<string, int> Stats() { return _stats; }
        public List<string> Errors() { return _errors; }

        public void SetLogger(ExportLogger log)
        {
            if (log != null) _log = log;
        }

        /// <summary>
        /// Read and validate configuration parameters, and initiate the export.
        /// </summary>
        public void Process()
        {
            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // validate config values
            if (!ValidateConfiguration()) return;

            // Generate the BibTeX
            this.ProcessBibTeX();
        }

        private void ProcessBibTeX()
        {
            BHLWS.BHLWSSoapClient service = null;

            try
            {
                service = new BHLWS.BHLWSSoapClient();
                service.InnerChannel.OperationTimeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                TitleBibTeX[] citations = null;

                _log.Info("Processing items...");
                _log.Info("Getting BibTeX data for all items.");
                citations = service.TitleBibTeXSelectAllItemCitations();
                this.GenerateCitations(citations, configParms.BibTexItemTempFile,
                    configParms.BibTexItemFile, configParms.BibTexItemZipFile, "Items");
                _log.Info("Item processing complete.");

                _log.Info("Processing titles...");
                _log.Info("Getting BibTeX data for all titles.");
                citations = service.TitleBibTeXSelectAllTitleCitations();
                this.GenerateCitations(citations, configParms.BibTexTitleTempFile,
                    configParms.BibTexTitleFile, configParms.BibTexTitleZipFile, "Titles");
                _log.Info("Title processing complete.");

                _log.Info("Processing segments...");
                _log.Info("Getting BibTeX data for all segments.");
                citations = service.SegmentSelectAllBibTeXCitations();
                this.GenerateCitations(citations, configParms.BibTexSegmentTempFile,
                    configParms.BibTexSegmentFile, configParms.BibTexSegmentZipFile, "Segments");
                _log.Info("Segment processing complete.");
            }
            catch (Exception ex)
            {
                _log.Error("Exception processing citations.", ex);
                _errors.Add(string.Format("Exception processing citation:  {0}", ex.Message));
            }
        }

        private void GenerateCitations(TitleBibTeX[] citations, String bibtexTempFile,
            String bibtexFile, String bibtexZipFile, string statsKey)
        {
            // Clean up an existing temp file
            if (File.Exists(bibtexTempFile)) File.Delete(bibtexTempFile);

            if (citations.Length > 0)
            {
                foreach (TitleBibTeX citation in citations)
                {
                    try
                    {
                        String citationText = this.GenerateCitation(citation);
                        File.AppendAllText(bibtexTempFile, citationText, Encoding.UTF8);
                        UpdateStats(statsKey);
                    }
                    catch (ArgumentException ae)
                    {
                        _log.Error(string.Format("Invalid metadata for citation: {0}", citation.CitationKey), ae);
                        UpdateStats(string.Format("{0} with Invalid Metadata", statsKey));
                    }
                    catch (Exception ex)
                    {
                        _log.Error(string.Format("Exception processing citation: {0}", citation.CitationKey), ex);
                        _errors.Add(string.Format("Exception processing citation {0}: {1}", citation.CitationKey, ex.Message));
                        // don't bomb.  try next citation
                    }
                }

                if ((_errors.Count / Convert.ToDouble(citations.Length) * 100) > 1.0)
                {
                    _log.Error(string.Format("BibTeX processing failed. {0} out of {1} item citations produced errors.", _errors.Count.ToString(), citations.Length));
                    _errors.Add(string.Format("BibTeX processing failed. {0} out of {1} item citations produced errors.", _errors.Count.ToString(), citations.Length));
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(bibtexFile);
                    File.Move(bibtexTempFile, bibtexFile);

                    // Create a compressed version of the file
                    new ExportFile(_log).Compress(bibtexFile, bibtexZipFile);
                }
            }

            _log.Info("Citation processing complete.");
        }

        private String GenerateCitation(TitleBibTeX citation)
        {
            String citationText = String.Empty;

            String citationType = String.Empty;
            switch (citation.Type)
            {
                case "Article":
                    citationType = BibTeXRefType.ARTICLE;
                    break;
                case "Chapter":
                case "Treatment":
                    citationType = BibTeXRefType.INBOOK;
                    break;
                default:
                    citationType = BibTeXRefType.BOOK;
                    break;
            }
            String citationKey = citation.CitationKey;

            String title = citation.Title;
            String journal = citation.Journal;
            String volume = citation.Volume;
            String series = citation.Series;
            String number = citation.Issue;
            String copyrightStatus = citation.CopyrightStatus;
            String url = citation.Url;
            String note = citation.Note;
            String publisher = citation.Publisher;
            String year = citation.Year;
            String pages = String.Empty;
            if (citationType == BibTeXRefType.ARTICLE || citationType == BibTeXRefType.INBOOK)
                pages = citation.PageRange;
            else
                pages = citation.Pages.ToString();
            String typeOfWork = String.Empty;
            if (citationType == BibTeXRefType.INBOOK) typeOfWork = citation.Type;
            String authors = citation.Authors;
            String author = authors.Replace("|", " and ");
            String keywords = citation.Keywords.Replace("|", ",");

            Dictionary<String, String> elements = new Dictionary<string, string>();
            elements.Add(BibTeXRefElementName.TITLE, title);
            if (journal != String.Empty) elements.Add(BibTeXRefElementName.JOURNAL, journal);
            if (volume != String.Empty) elements.Add(BibTeXRefElementName.VOLUME, volume);
            if (series != String.Empty) elements.Add(BibTeXRefElementName.SERIES, series);
            if (number != String.Empty) elements.Add(BibTeXRefElementName.NUMBER, number);
            if (typeOfWork != String.Empty) elements.Add(BibTeXRefElementName.TYPEOFWORK, typeOfWork);
            if (copyrightStatus != String.Empty) elements.Add(BibTeXRefElementName.COPYRIGHT, copyrightStatus);
            if (url != String.Empty) elements.Add(BibTeXRefElementName.URL, url);
            if (note != String.Empty) elements.Add(BibTeXRefElementName.NOTE, note);
            elements.Add(BibTeXRefElementName.PUBLISHER, publisher);
            elements.Add(BibTeXRefElementName.AUTHOR, author);
            elements.Add(BibTeXRefElementName.YEAR, year);
            if (pages != String.Empty && pages != "0") elements.Add(BibTeXRefElementName.PAGES, pages);
            if (keywords != String.Empty) elements.Add(BibTeXRefElementName.KEYWORDS, keywords);

            MOBOT.BHL.Web.Utilities.BibTeX bibtex = new MOBOT.BHL.Web.Utilities.BibTeX(citationType, citationKey, elements);
            citationText = bibtex.GenerateReference();

            return citationText;
        }

        /// <summary>
        /// Update the statistics for the given key value
        /// </summary>
        /// <param name="statsKey"></param>
        private void UpdateStats(string statsKey)
        {
            if (_stats.ContainsKey(statsKey))
                _stats[statsKey]++;
            else
                _stats.Add(statsKey, 1);
            if (_stats[statsKey] % 1000 == 0) _log.Info(string.Format("{0} {1} processed.", _stats[statsKey].ToString(), statsKey));
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            return true;
        }
    }
}
