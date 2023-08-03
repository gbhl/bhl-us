using MOBOT.BHL.Utility;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public Title SaveTitle(int importSourceID, string marcBibID, string marcLeader, string fullTitle,
            string shortTitle, string uniformTitle, string sortTitle, string callNumber,
            string publicationDetails, short? startYear, short? endYear, string datafield_260_a,
            string datafield_260_b, string datafield_260_c, string institutionCode, string languageCode,
            string titleDescription, string tl2Author,
            bool? publishReady, bool? rareBooks, string note, DateTime? externalCreationDate, 
            DateTime? externalLastModifiedDate, int? externalCreationUser, int? externalLastModifiedUser)
        {
            sortTitle = DataCleaner.CleanSortTitle(sortTitle);

            TitleDAL dal = new TitleDAL();
            Title savedTitle = dal.TitleSelectNewByKeyAndSource(null, null, marcBibID, importSourceID);

            if (savedTitle == null)
            {
                Title newTitle = new Title
                {
                    ImportKey = marcBibID,
                    ImportStatusID = 10,
                    ImportSourceID = importSourceID,
                    MARCBibID = marcBibID,
                    MARCLeader = marcLeader,
                    FullTitle = fullTitle,
                    ShortTitle = shortTitle,
                    UniformTitle = uniformTitle,
                    SortTitle = sortTitle,
                    CallNumber = callNumber,
                    PublicationDetails = publicationDetails,
                    StartYear = startYear,
                    EndYear = endYear,
                    Datafield_260_a = datafield_260_a,
                    Datafield_260_b = datafield_260_b,
                    Datafield_260_c = datafield_260_c,
                    InstitutionCode = institutionCode,
                    LanguageCode = languageCode,
                    TitleDescription = titleDescription,
                    TL2Author = tl2Author,
                    PublishReady = publishReady,
                    RareBooks = rareBooks,
                    Note = note,
                    ExternalCreationDate = externalCreationDate,
                    ExternalLastModifiedDate = externalLastModifiedDate,
                    ExternalCreationUser = externalCreationUser,
                    ExternalLastModifiedUser = externalLastModifiedUser
                };
                savedTitle = dal.TitleInsertAuto(null, null, newTitle);
            }
            else
            {
                if (savedTitle.MARCLeader != marcLeader || savedTitle.FullTitle != fullTitle || 
                    savedTitle.UniformTitle != uniformTitle || savedTitle.ShortTitle != shortTitle ||
                    savedTitle.SortTitle != sortTitle || savedTitle.CallNumber != callNumber || 
                    savedTitle.PublicationDetails != publicationDetails || 
                    savedTitle.StartYear != startYear || savedTitle.EndYear != endYear || 
                    savedTitle.Datafield_260_a != datafield_260_a || 
                    savedTitle.Datafield_260_b != datafield_260_b || 
                    savedTitle.Datafield_260_c != datafield_260_c ||
                    savedTitle.InstitutionCode != institutionCode || savedTitle.LanguageCode != languageCode ||
                    savedTitle.TitleDescription != titleDescription || 
                    savedTitle.TL2Author != tl2Author ||
                    savedTitle.PublishReady != publishReady || savedTitle.RareBooks != rareBooks ||
                    savedTitle.Note != note || savedTitle.ExternalCreationUser != externalCreationUser ||
                    savedTitle.ExternalLastModifiedUser != externalLastModifiedUser)
                {
                    savedTitle.MARCLeader = marcLeader;
                    savedTitle.FullTitle = fullTitle;
                    savedTitle.UniformTitle = uniformTitle;
                    savedTitle.ShortTitle = shortTitle;
                    savedTitle.SortTitle = sortTitle;
                    savedTitle.CallNumber = callNumber;
                    savedTitle.PublicationDetails = publicationDetails;
                    savedTitle.StartYear = startYear;
                    savedTitle.EndYear = endYear;
                    savedTitle.Datafield_260_a = datafield_260_a;
                    savedTitle.Datafield_260_b = datafield_260_b;
                    savedTitle.Datafield_260_c = datafield_260_c;
                    savedTitle.InstitutionCode = institutionCode;
                    savedTitle.LanguageCode = languageCode;
                    savedTitle.TitleDescription = titleDescription;
                    savedTitle.TL2Author = tl2Author;
                    savedTitle.PublishReady = publishReady;
                    savedTitle.RareBooks = rareBooks;
                    savedTitle.Note = note;
                    savedTitle.ExternalCreationDate = externalCreationDate;
                    savedTitle.ExternalLastModifiedDate = externalLastModifiedDate;
                    savedTitle.ExternalCreationUser = externalCreationUser;
                    savedTitle.ExternalLastModifiedUser = externalLastModifiedUser;

                    dal.TitleUpdateAuto(null, null, savedTitle);
                }
            }

            return savedTitle;
        }
    }
}
