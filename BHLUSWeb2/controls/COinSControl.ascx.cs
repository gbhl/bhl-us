using System;
using System.Configuration;
using System.Text;
using System.Web.UI;
using MOBOT.BHL.Server;
using Data = MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Web2
{
    public partial class COinSControl : UserControl
    {
        public int TitleID { get; set; }
        public int ItemID { get; set; }
        public int SegmentID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string coinsOutput = string.Empty;

                if ((this.TitleID != 0) || (this.ItemID != 0)) coinsOutput = this.GetCOinSForItem(this.TitleID, this.ItemID);
                if (this.SegmentID != 0) coinsOutput = this.GetCOinSForSegment(this.SegmentID);

                // Render the COinS to the control
                this.Controls.Add(new LiteralControl("<span class=\"Z3988\" title=\"" + coinsOutput + "\"></span>"));
            }
        }

        /// <summary>
        /// Get the COinS string for the specified title/item
        /// </summary>
        /// <returns></returns>
        private string GetCOinSForItem(int titleId, int itemId)
        {
            BHLProvider provider = new BHLProvider();
            Data.ItemCOinSView coins = null;
            StringBuilder output = new StringBuilder();

            // Get the data
            if (titleId != 0)
            {
                coins = provider.ItemCOinSSelectByTitleId(titleId);
                CustomGenericList<Data.DOI> doi = provider.DOISelectValidForTitle(titleId);
                if (doi != null && coins != null)
                {
                    if (doi.Count > 0) coins.Doi = doi[0].DOIName;
                }
            }
            else
            {
                coins = provider.ItemCOinSSelectByItemId(itemId);
            }

            if (coins != null)
            {
                // Build the COinS
                output.Append("ctx_ver=Z39.88-2004");

                // Add identifiers
                if (coins.Doi != String.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("info:doi/" + coins.Doi));
                if (coins.Oclc != String.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("info:oclcnum/" + coins.Oclc));
                if (coins.Rft_issn != String.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("urn:ISSN:" + coins.Rft_issn));
                if (coins.Rft_isbn != String.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("urn:ISBN:" + coins.Rft_isbn));
                if (coins.Lccn != String.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("info:lccn/" + coins.Lccn));
                if (titleId != 0) output.Append("&amp;rft_id=" + Server.UrlEncode(string.Format(ConfigurationManager.AppSettings["BibPageUrl"], titleId)));
                if (itemId != 0) output.Append("&amp;rft_id=" + Server.UrlEncode(string.Format(ConfigurationManager.AppSettings["ItemPageUrl"], itemId)));

                // Add format-specific attributes
                switch (coins.Rft_genre)
                {
                    // Journal COinS do not work with Mendeley or Zotero unless they represent journal articles.
                    // Per discussion with Chris, decided to represent full journal volumes as books.
                    case "book":    // book
                    case "journal":     // journal
                        output.Append("&amp;rft_val_fmt=" + Server.UrlEncode("info:ofi/fmt:kev:mtx:book"));
                        output.Append("&amp;rft.genre=book");
                        if (coins.Rft_title != String.Empty) output.Append("&amp;rft.btitle=" + Server.UrlEncode(coins.Rft_title));

                        // Rft_stitle, Rft_volume, and Rft_coden were added for journals, even 
                        // though they don't technically make sense for book COinS
                        if (coins.Rft_stitle != String.Empty) output.Append("&amp;rft.stitle=" + Server.UrlEncode(coins.Rft_stitle));
                        if (coins.Rft_volume != String.Empty && this.ItemID != 0) output.Append("&amp;rft.volume=" + Server.UrlEncode(coins.Rft_volume));
                        if (coins.Rft_coden != String.Empty) output.Append("&amp;rft.coden=" + Server.UrlEncode(coins.Rft_coden));

                        if (coins.Rft_place != String.Empty) output.Append("&amp;rft.place=" + Server.UrlEncode(coins.Rft_place));
                        if (coins.Rft_pub != String.Empty) output.Append("&amp;rft.pub=" + Server.UrlEncode(coins.Rft_pub));
                        if (coins.Rft_edition != String.Empty) output.Append("&amp;rft.edition=" + Server.UrlEncode(coins.Rft_edition));

                        if (coins.Rft_issn != String.Empty) output.Append("&amp;rft.issn=" + Server.UrlEncode(coins.Rft_issn));
                        if (coins.Rft_isbn != String.Empty) output.Append("&amp;rft.isbn=" + Server.UrlEncode(coins.Rft_isbn));
                        if (coins.Rft_aufirst != String.Empty) output.Append("&amp;rft.aufirst=" + Server.UrlEncode(coins.Rft_aufirst));
                        if (coins.Rft_aulast != String.Empty) output.Append("&amp;rft.aulast=" + Server.UrlEncode(coins.Rft_aulast));
                        if (coins.Rft_aucorp != String.Empty) output.Append("&amp;rft.aucorp=" + Server.UrlEncode(coins.Rft_aucorp));
                        if (coins.Rft_au_BOOK != String.Empty)
                        {
                            String[] authors = coins.Rft_au_BOOK.Split('|');
                            foreach (String author in authors)
                            {
                                if (author != String.Empty) output.Append("&amp;rft.au=" + Server.UrlEncode(author));
                            }
                        }
                        if ((coins.Rft_tpages ?? 0) != 0)
                        {
                            output.Append("&amp;rft.pages=1-" + coins.Rft_tpages.ToString());
                            output.Append("&amp;rft.tpages=" + coins.Rft_tpages.ToString());
                        }

                        break;
                    /*
                    case "journal":   // journal
                        output.Append("&amp;rft_val_fmt=" + Server.UrlEncode("info:ofi/fmt:kev:mtx:journal"));
                        output.Append("&amp;rft.genre=journal");
                        if (coins.Rft_title != String.Empty) output.Append("&amp;rft.jtitle=" + Server.UrlEncode(coins.Rft_title));
                        if (coins.Rft_stitle != String.Empty) output.Append("&amp;rft.stitle=" + Server.UrlEncode(coins.Rft_stitle));
                        if (coins.Rft_volume != String.Empty && this.ItemID != 0) output.Append("&amp;rft.volume=" + Server.UrlEncode(coins.Rft_volume));
                        if (coins.Rft_coden != String.Empty) output.Append("&amp;rft.coden=" + Server.UrlEncode(coins.Rft_coden));

                        if (coins.Rft_issn != String.Empty) output.Append("&amp;rft.issn=" + Server.UrlEncode(coins.Rft_issn));
                        if (coins.Rft_isbn != String.Empty) output.Append("&amp;rft.isbn=" + Server.UrlEncode(coins.Rft_isbn));
                        if (coins.Rft_aufirst != String.Empty) output.Append("&amp;rft.aufirst=" + Server.UrlEncode(coins.Rft_aufirst));
                        if (coins.Rft_aulast != String.Empty) output.Append("&amp;rft.aulast=" + Server.UrlEncode(coins.Rft_aulast));
                        if (coins.Rft_aucorp != String.Empty) output.Append("&amp;rft.aucorp=" + Server.UrlEncode(coins.Rft_aucorp));
                        if (coins.Rft_au_BOOK != String.Empty)
                        {
                            String[] authors = coins.Rft_au_BOOK.Split('|');
                            foreach (String author in authors)
                            {
                                if (author != String.Empty) output.Append("&amp;rft.au=" + Server.UrlEncode(author));
                            }
                        }

                        // rft.pages does not apply to journals
                        //if ((coins.Rft_tpages ?? 0) != 0) output.Append("&amp;rft.pages=" + coins.Rft_tpages.ToString());

                        break;
                    */
                    default:    // dublin core
                        output.Append("&amp;rft_val_fmt=" + Server.UrlEncode("info:ofi/fmt:kev:mtx:dc"));
                        output.Append("&amp;rft.source=" + Server.UrlEncode("Biodiversity Heritage Library"));
                        output.Append("&amp;rft.rights=" + Server.UrlEncode("Creative Commons Attribution 3.0"));
                        if (this.TitleID != 0) output.Append("&amp;rtf.identifier=" + Server.UrlEncode(string.Format(ConfigurationManager.AppSettings["BibPageUrl"], TitleID)));
                        if (this.ItemID != 0) output.Append("&amp;rft.identifier=" + Server.UrlEncode(string.Format(ConfigurationManager.AppSettings["ItemPageUrl"], ItemID)));
                        if (coins.Rft_title != String.Empty) output.Append("&amp;rft.title=" + Server.UrlEncode(coins.Rft_title));
                        if (coins.Rft_language != String.Empty) output.Append("&amp;rft.language=" + Server.UrlEncode(coins.Rft_language));
                        if (coins.Rft_au_DC != String.Empty)
                        {
                            String[] authors = coins.Rft_au_DC.Split('|');
                            foreach (String author in authors)
                            {
                                if (author != String.Empty) output.Append("&amp;rft.creator=" + Server.UrlEncode(author));
                            }
                        }
                        if (coins.Rft_publisher != String.Empty) output.Append("&amp;rft.publisher=" + Server.UrlEncode(coins.Rft_publisher));
                        if (titleId != 0)
                        {
                            if (coins.Rft_contributor_TITLE != String.Empty) output.Append("&amp;rft.contributor=" + Server.UrlEncode(coins.Rft_contributor_TITLE));
                        }
                        else if (itemId != 0)
                        {
                            if (coins.Rft_contributor_ITEM != String.Empty) output.Append("&amp;rft.contributor=" + Server.UrlEncode(coins.Rft_contributor_ITEM));
                        }

                        if (coins.Rft_subject != String.Empty)
                        {
                            String[] subjects = coins.Rft_subject.Split('|');
                            foreach (String subject in subjects)
                            {
                                if (subject != String.Empty) output.Append("&amp;rft.subject=" + Server.UrlEncode(subject));
                            }
                        }

                        break;
                }

                // Add additional elements common to all formats
                if (titleId != 0)
                {
                    if (coins.Rft_date_TITLE != String.Empty) output.Append("&amp;rft.date=" + Server.UrlEncode(coins.Rft_date_TITLE.ToString()));
                }
                else if (itemId != 0)
                {
                    if (coins.Rft_date_ITEM != String.Empty) output.Append("&amp;rft.date=" + Server.UrlEncode(coins.Rft_date_ITEM.ToString()));
                }
            }

            return output.ToString();
        }

        /// <summary>
        /// Get the COinS string for the specified segment
        /// </summary>
        /// <returns></returns>
        private string GetCOinSForSegment(int segmentId)
        {
            BHLProvider provider = new BHLProvider();
            Data.SegmentCOinSView coins = null;
            StringBuilder output = new StringBuilder();

            // Get the data
            coins = provider.SegmentCOinSSelectBySegmentId(segmentId);
            CustomGenericList<Data.DOI> doi = provider.DOISelectValidForSegment(segmentId);
            if (doi != null && coins != null) 
            {
                if (doi.Count > 0) coins.Doi = doi[0].DOIName;
            }

            if (coins != null)
            {
                // Build the COinS
                output.Append("ctx_ver=Z39.88-2004");

                // Add identifiers
                if (coins.Doi != String.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("info:doi/" + coins.Doi));
                if (coins.Rft_issn != String.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("urn:ISSN:" + coins.Rft_issn));
                if (coins.Rft_isbn != String.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("urn:ISBN:" + coins.Rft_isbn));
                output.Append("&amp;rft_id=" + Server.UrlEncode(string.Format(ConfigurationManager.AppSettings["PartPageUrl"], segmentId)));

                // Add format-specific attributes
                switch (coins.Rft_genre)
                {
                    case "article":
                    case "issue":
                    case "proceeding":
                    case "conference":
                    case "preprint":
                    case "unknown":
                        output.Append("&amp;rft_val_fmt=" + Server.UrlEncode("info:ofi/fmt:kev:mtx:journal"));
                        output.Append("&amp;rft.genre=" + Server.UrlEncode(coins.Rft_genre));
                        if (coins.Rft_atitle != String.Empty) output.Append("&amp;rft.atitle=" + Server.UrlEncode(coins.Rft_atitle));
                        if (coins.Rft_jtitle != String.Empty) output.Append("&amp;rft.jtitle=" + Server.UrlEncode(coins.Rft_jtitle));
                        if (coins.Rft_volume != String.Empty) output.Append("&amp;rft.volume=" + Server.UrlEncode(coins.Rft_volume));
                        if (coins.Rft_issue != String.Empty) output.Append("&amp;rft.issue=" + Server.UrlEncode(coins.Rft_issue));
                        if (coins.Rft_spage != String.Empty) output.Append("&amp;rft.spage=" + Server.UrlEncode(coins.Rft_spage));
                        if (coins.Rft_epage != String.Empty) output.Append("&amp;rft.epage=" + Server.UrlEncode(coins.Rft_epage));
                        if (coins.Rft_pages != String.Empty) output.Append("&amp;rft.pages=" + Server.UrlEncode(coins.Rft_pages));
                        if (coins.Rft_coden != String.Empty) output.Append("&amp;rft.coden=" + Server.UrlEncode(coins.Rft_coden));
                        if (coins.Rft_issn != String.Empty) output.Append("&amp;rft.issn=" + Server.UrlEncode(coins.Rft_issn));
                        if (coins.Rft_isbn != String.Empty) output.Append("&amp;rft.isbn=" + Server.UrlEncode(coins.Rft_isbn));
                        if (coins.Rft_aufirst != String.Empty) output.Append("&amp;rft.aufirst=" + Server.UrlEncode(coins.Rft_aufirst));
                        if (coins.Rft_aulast != String.Empty) output.Append("&amp;rft.aulast=" + Server.UrlEncode(coins.Rft_aulast));
                        if (coins.Rft_au != String.Empty)
                        {
                            String[] authors = coins.Rft_au.Split('|');
                            foreach (String author in authors)
                            {
                                if (author != String.Empty) output.Append("&amp;rft.au=" + Server.UrlEncode(author));
                            }
                        }

                        break;
                    default:    // dublin core
                        output.Append("&amp;rft_val_fmt=" + Server.UrlEncode("info:ofi/fmt:kev:mtx:dc"));
                        output.Append("&amp;rft.source=" + Server.UrlEncode("Biodiversity Heritage Library"));
                        output.Append("&amp;rft.rights=" + Server.UrlEncode("Creative Commons Attribution 3.0"));
                        output.Append("&amp;rtf.identifier=" + Server.UrlEncode(string.Format(ConfigurationManager.AppSettings["PartPageUrl"], segmentId)));
                        if (coins.Rft_atitle != String.Empty) output.Append("&amp;rft.title=" + Server.UrlEncode(coins.Rft_atitle));
                        if (coins.Rft_language != String.Empty) output.Append("&amp;rft.language=" + Server.UrlEncode(coins.Rft_language));
                        if (coins.Rft_au != String.Empty)
                        {
                            String[] authors = coins.Rft_au.Split('|');
                            foreach (String author in authors)
                            {
                                if (author != String.Empty) output.Append("&amp;rft.creator=" + Server.UrlEncode(author));
                            }
                        }
                        if (coins.Rft_contributor != String.Empty) output.Append("&amp;rft.contributor=" + Server.UrlEncode(coins.Rft_contributor));

                        if (coins.Rft_subject != String.Empty)
                        {
                            String[] subjects = coins.Rft_subject.Split('|');
                            foreach (String subject in subjects)
                            {
                                if (subject != String.Empty) output.Append("&amp;rft.subject=" + Server.UrlEncode(subject));
                            }
                        }

                        break;
                }

                // Add additional elements common to all formats
                if (coins.Rft_date != String.Empty) output.Append("&amp;rft.date=" + Server.UrlEncode(coins.Rft_date.ToString()));
            }

            return output.ToString();
        }
    }
}