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
                Page newPage = new Page();
                newPage.ImportStatusID = 10;
                newPage.ImportSourceID = importSourceID;
                newPage.BarCode = barCode;
                newPage.FileNamePrefix = fileNamePrefix;
                newPage.SequenceOrder = sequenceOrder;
                newPage.PageDescription = pageDescription;
                newPage.Illustration = illustration;
                newPage.Note = note;
                newPage.FileSize_Temp = fileSize_temp;
                newPage.FileExtension = fileExtension;
                newPage.Active = active;
                newPage.Year = year;
                newPage.Series = series;
                newPage.Volume = volume;
                newPage.Issue = issue;
                newPage.ExternalURL = externalUrl;
                newPage.IssuePrefix = issuePrefix;
                newPage.LastPageNameLookupDate = lastPageNameLookupDate;
                newPage.PaginationUserID = paginationUserID;
                newPage.PaginationDate = paginationDate;
                newPage.ExternalCreationDate = externalCreationDate;
                newPage.ExternalLastModifiedDate = externalLastModifiedDate;
                newPage.ExternalCreationUser = externalCreationUser;
                newPage.ExternalLastModifiedUser = externalLastModifiedUser;

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
