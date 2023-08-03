using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public Page SavePage(int importSourceID, string barCode, string fileNamePrefix,
            int? sequenceOrder, string pageDescription, bool? illustration, string note, 
            int? fileSize_temp, string fileExtension, bool active, string year, string series, 
            string volume, string issue, string externalUrl, string issuePrefix, 
            DateTime? lastPageNameLookupDate, int? paginationUserID, DateTime? paginationDate,
            DateTime? externalCreationDate, DateTime? externalLastModifiedDate,
            int? externalCreationUser, int? externalLastModifiedUser)
        {
            PageDAL dal = new PageDAL();
            Page savedPage = dal.PageSelectNewByKeyValuesAndSource(null, null, barCode, 
                fileNamePrefix, importSourceID);

            if (savedPage == null)
            {
                Page newPage = new Page
                {
                    ImportStatusID = 10,
                    ImportSourceID = importSourceID,
                    BarCode = barCode,
                    FileNamePrefix = fileNamePrefix,
                    SequenceOrder = sequenceOrder,
                    PageDescription = pageDescription,
                    Illustration = illustration,
                    Note = note,
                    FileSize_Temp = fileSize_temp,
                    FileExtension = fileExtension,
                    Active = active,
                    Year = year,
                    Series = series,
                    Volume = volume,
                    Issue = issue,
                    ExternalURL = externalUrl,
                    IssuePrefix = issuePrefix,
                    LastPageNameLookupDate = lastPageNameLookupDate,
                    PaginationUserID = paginationUserID,
                    PaginationDate = paginationDate,
                    ExternalCreationDate = externalCreationDate,
                    ExternalLastModifiedDate = externalLastModifiedDate,
                    ExternalCreationUser = externalCreationUser,
                    ExternalLastModifiedUser = externalLastModifiedUser
                };

                savedPage = dal.PageInsertAuto(null, null, newPage);
            }
            else
            {
                if (savedPage.SequenceOrder != sequenceOrder ||
                    savedPage.PageDescription != pageDescription ||
                    savedPage.Illustration != illustration ||
                    savedPage.Note != note ||
                    savedPage.FileSize_Temp != fileSize_temp ||
                    savedPage.FileExtension != fileExtension ||
                    savedPage.Active != active ||
                    savedPage.Year != year ||
                    savedPage.Series != series ||
                    savedPage.Volume != volume ||
                    savedPage.Issue != issue ||
                    savedPage.ExternalURL != externalUrl ||
                    savedPage.IssuePrefix != issuePrefix ||
                    savedPage.LastPageNameLookupDate != lastPageNameLookupDate ||
                    savedPage.PaginationUserID != paginationUserID ||
                    savedPage.PaginationDate != paginationDate
                    )
                {
                    savedPage.SequenceOrder = sequenceOrder;
                    savedPage.PageDescription = pageDescription;
                    savedPage.Illustration = illustration;
                    savedPage.Note = note;
                    savedPage.FileSize_Temp = fileSize_temp;
                    savedPage.FileExtension = fileExtension;
                    savedPage.Active = active;
                    savedPage.Year = year;
                    savedPage.Series = series;
                    savedPage.Volume = volume;
                    savedPage.Issue = issue;
                    savedPage.ExternalURL = externalUrl;
                    savedPage.IssuePrefix = issuePrefix;
                    savedPage.LastPageNameLookupDate = lastPageNameLookupDate;
                    savedPage.PaginationUserID = paginationUserID;
                    savedPage.PaginationDate = paginationDate;
                    savedPage.ExternalLastModifiedDate = externalLastModifiedDate;
                    savedPage.ExternalLastModifiedUser = externalLastModifiedUser;

                    dal.PageUpdateAuto(null, null, savedPage);
                }
            }

            return savedPage;
        }
    }
}
