﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Web2
{
    public partial class SectionPage : BrowsePage
    {

        protected Segment BhlSegment { get; set; }
        protected int SegmentID { get; set; }
        protected string SchemaType { get; set; }
        protected string DOI { get; set; }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            int segmentID;
            DOI = string.Empty;

            if (!int.TryParse((string)RouteData.Values["segmentid"], out segmentID))
            {
                Response.Redirect("~/pagenotfound");
            }
            else
            {
                SegmentID = segmentID;
                BhlSegment = bhlProvider.SegmentSelectExtended(SegmentID);
                BhlSegment.IdentifierList = bhlProvider.SegmentIdentifierSelectForDisplayBySegmentID(SegmentID);
                if (BhlSegment == null)
                {
                    Response.Redirect("~/pagenotfound");
                }
                else
                {
                    // Check to make sure this title hasn't been replaced.  If it has, redirect
                    // to the appropriate titleid.
                    if (BhlSegment.RedirectSegmentID != null)
                    {
                        Response.Redirect("~/part/" + BhlSegment.RedirectSegmentID);
                    }

                    // Make sure the title is published.
                    if (BhlSegment.SegmentStatusID != (int)SegmentStatusValue.New && 
                        BhlSegment.SegmentStatusID != (int)SegmentStatusValue.Published)
                    {
                        Response.Redirect("~/itemunavailable");
                    }
                }


                COinS.SegmentID = SegmentID;

                // Set up the Mendeley share control
                mendeley.SegmentID = SegmentID;

                // Set the Schema.org itemtype
                switch (BhlSegment.GenreName)
                {
                    case "Book":
                    case "Journal":
                        SchemaType = "http://schema.org/Book";
                        break;
                    case "Article":
                    case "Preprint":
                        SchemaType = "http://schema.org/ScholarlyArticle";
                        break;
                    default: // BookItem, Chapter, Issue, Proceeding, Conference, Unknown, Treatment
                        SchemaType = "http://schema.org/CreativeWork";
                        break;
                }

                CustomGenericList<DOI> dois = bhlProvider.DOISelectValidForSegment(SegmentID);
                if (dois.Count > 0) DOI = ConfigurationManager.AppSettings["DOIResolverURL"] + dois[0].DOIName;

                main.Page.Title = string.Format("Details - {0} - Biodiversity Heritage Library", BhlSegment.Title);

                // Get the MODS
                OAI2.OAIRecord record = new OAI2.OAIRecord("oai:" + ConfigurationManager.AppSettings["OAIIdentifierNamespace"] + ":part/" + BhlSegment.SegmentID);
                OAIMODS.Convert mods = new OAIMODS.Convert(record);
                litMods.Text = Server.HtmlEncode(mods.ToString()).Replace("\n", "<br />");

                // Get the BibTex citations
                try
                {
                    litBibTeX.Text = bhlProvider.SegmentBibTeXGetCitationStringForSegmentID(BhlSegment.SegmentID, false).Replace("\n", "<br />");
                }
                catch
                {
                    litBibTeX.Text = string.Empty;
                }

                // Get the EndNote citation
                try
                {
                    litEndNote.Text = bhlProvider.SegmentEndNoteGetCitationStringForSegmentID(BhlSegment.SegmentID, ConfigurationManager.AppSettings["PartPageUrl"], false).Replace("\n", "<br />");
                }
                catch
                {
                    litEndNote.Text = string.Empty;
                }
            }
        }
    }
}