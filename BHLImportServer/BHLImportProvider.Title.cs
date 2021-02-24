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
                Title newTitle = new Title();
                newTitle.ImportKey = marcBibID;
                newTitle.ImportStatusID = 10;
                newTitle.ImportSourceID = importSourceID;
                newTitle.MARCBibID = marcBibID;
                newTitle.MARCLeader = marcLeader;
                newTitle.FullTitle = fullTitle;
                newTitle.ShortTitle = shortTitle;
                newTitle.UniformTitle = uniformTitle;
                newTitle.SortTitle = sortTitle;
                newTitle.CallNumber = callNumber;
                newTitle.PublicationDetails = publicationDetails;
                newTitle.StartYear = startYear;
                newTitle.EndYear = endYear;
                newTitle.Datafield_260_a = datafield_260_a;
                newTitle.Datafield_260_b = datafield_260_b;
                newTitle.Datafield_260_c = datafield_260_c;
                newTitle.InstitutionCode = institutionCode;
                newTitle.LanguageCode = languageCode;
                newTitle.TitleDescription = titleDescription;
                newTitle.TL2Author = tl2Author;
                newTitle.PublishReady = publishReady;
                newTitle.RareBooks = rareBooks;
                newTitle.Note = note;
                newTitle.ExternalCreationDate = externalCreationDate;
                newTitle.ExternalLastModifiedDate = externalLastModifiedDate;
                newTitle.ExternalCreationUser = externalCreationUser;
                newTitle.ExternalLastModifiedUser = externalLastModifiedUser;
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
