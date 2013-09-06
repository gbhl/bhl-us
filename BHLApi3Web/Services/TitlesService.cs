using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MOBOT.BHL.API.BHLApi;
using BHLApiData = MOBOT.BHL.API.BHLApiDataObjects2;
using BHLData = MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;

namespace BHLApi3Web.Services
{
    public class TitlesService : BHLApi3Web.Services.ITitlesService
    {
        public Models.Title GetTitle(int id)
        {
            Api2 apiProvider = new Api2();
            BHLApiData.Title bhlTitle = apiProvider.GetTitleMetadata(id.ToString(), "f");

            Models.Title title = null;
            if (bhlTitle != null)
            {
                title = new Models.Title();
                title.TitleId = bhlTitle.TitleID;
                title.FullTitle = bhlTitle.FullTitle;
                title.PartNumber = bhlTitle.PartNumber;
                title.PartName = bhlTitle.PartName;
                title.CallNumber = bhlTitle.CallNumber;
                title.Edition = bhlTitle.Edition;
                title.PublisherPlace = bhlTitle.PublisherPlace;
                title.PublisherName = bhlTitle.PublisherName;
                title.PublicationDate = bhlTitle.PublicationDate;
                title.PublicationFrequency = bhlTitle.PublicationFrequency;
                title.BibliographicLevel = bhlTitle.BibliographicLevel;

                List<Models.Subject> subjects = null;
                foreach (BHLApiData.Subject keyword in bhlTitle.Subjects)
                {
                    subjects = (subjects ?? new List<Models.Subject>());
                    Models.Subject subject = new Models.Subject();
                    subject.SubjectText = keyword.SubjectText;
                    subjects.Add(subject);
                }
                title.Subjects = subjects;

                List<Models.Author> authors = null;
                foreach (BHLApiData.Creator bhlAuthor in bhlTitle.Authors)
                {
                    authors = (authors ?? new List<Models.Author>());
                    Models.Author author = new Models.Author();
                    author.CreatorID = bhlAuthor.CreatorID;
                    author.Type = bhlAuthor.Role;
                    author.Name = bhlAuthor.Name;
                    author.Dates = bhlAuthor.Dates;
                    author.Numeration = bhlAuthor.Numeration;
                    author.Unit = bhlAuthor.Unit;
                    author.Title = bhlAuthor.Title;
                    author.Location = bhlAuthor.Location;
                    author.FullerForm = bhlAuthor.FullerForm;
                    authors.Add(author);
                }
                title.Authors = authors;

                List<Models.Identifier> identifiers = null;
                foreach (BHLApiData.TitleIdentifier bhlIdentifier in bhlTitle.Identifiers)
                {
                    identifiers = (identifiers ?? new List<Models.Identifier>());
                    Models.Identifier identifier = new Models.Identifier();
                    identifier.IdentifierName = bhlIdentifier.IdentifierName;
                    identifier.IdentifierValue = bhlIdentifier.IdentifierValue;
                    identifier.RelationshipType = "same as";
                    identifiers.Add(identifier);
                }
                title.Identifiers = identifiers;
            }

            return title;
        }

        public IEnumerable<Models.Title> GetSearchSince(string since, string until)
        {
            BHLProvider provider = new BHLProvider();
            CustomGenericList<BHLData.OAIIdentifier> bhlTitleIds = provider.OAIIdentifierSelectTitles(1000000, 1, Convert.ToDateTime(since), Convert.ToDateTime(until));

            List<Models.Title> titles = new List<Models.Title>();
            if (bhlTitleIds.Count > 0)
            {
                titles = new List<Models.Title>();
                foreach (BHLData.OAIIdentifier bhlTitleId in bhlTitleIds)
                {
                    Models.Title title = new Models.Title();
                    title.TitleId = bhlTitleId.Id;
                    titles.Add(title);
                }
            }

            return titles;
        }
    }
}