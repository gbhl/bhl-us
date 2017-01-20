using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using MOBOT.BHL.Utility;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public IAScandata SaveIAScandata(int itemID, int sequence, string pageType, string pageNumber, string year,
            string volume, string issue, string issuePrefix)
        {
            // Standardize the format of the year value
            year = DataCleaner.CleanYear(year);

            IAScandataDAL dal = new IAScandataDAL();
            IAScandata savedScandata = dal.IAScandataSelectByItemAndSequence(null, null, itemID, sequence);

            if (savedScandata == null)
            {
                IAScandata newScandata = new IAScandata();
                newScandata.ItemID = itemID;
                newScandata.Sequence = sequence;
                newScandata.PageType = pageType;
                newScandata.PageNumber = pageNumber;
                newScandata.Year = year;
                newScandata.Volume = volume;
                newScandata.Issue = issue;
                newScandata.IssuePrefix = issuePrefix;
                savedScandata = dal.IAScandataInsertAuto(null, null, newScandata);
            }
            else
            {
                if ((savedScandata.PageType != pageType) || (savedScandata.PageNumber != pageNumber) ||
                    ((savedScandata.Year ?? string.Empty) != (year ?? string.Empty)) || 
                    ((savedScandata.Volume ?? string.Empty) != (volume ?? string.Empty)) ||
                    ((savedScandata.Issue ?? string.Empty) != (issue ?? string.Empty)) || 
                    ((savedScandata.IssuePrefix ?? string.Empty) != (issuePrefix ?? string.Empty)))
                {
                    savedScandata.PageType = pageType;
                    savedScandata.PageNumber = pageNumber;
                    savedScandata.Year = year;
                    savedScandata.Volume = volume;
                    savedScandata.Issue = issue;
                    savedScandata.IssuePrefix = issuePrefix;
                    dal.IAScandataUpdateAuto(null, null, savedScandata);
                }
            }

            return savedScandata;
        }

        public IAScandataAltPageType SaveIAScandataAltPageType(int scandataID, string pageType)
        {
            IAScandataAltPageTypeDAL dal = new IAScandataAltPageTypeDAL();
            IAScandataAltPageType savedAltPageType = dal.IAScandataAltPageTypeSelectByScandataAndPageType(null, null, scandataID, pageType);

            if (savedAltPageType == null)
            {
                IAScandataAltPageType newAltPageType = new IAScandataAltPageType();
                newAltPageType.ScandataID = scandataID;
                newAltPageType.PageType = pageType;
                savedAltPageType = dal.IAScandataAltPageTypeInsertAuto(null, null, newAltPageType);
            }
            else
            {
                // Nothing to do; we already have this alt page type
            }

            return savedAltPageType;
        }

        public IAScandataAltPageNumber SaveIAScandataAltPageNumber(int scandataID, int sequence, string pagePrefix, string pageNumber, bool implied)
        {
            IAScandataAltPageNumberDAL dal = new IAScandataAltPageNumberDAL();
            IAScandataAltPageNumber savedAltPageNumber = dal.IAScandataAltPageNumberSelectByScandataAndSequence(null, null, scandataID, sequence);

            if (savedAltPageNumber == null)
            {
                IAScandataAltPageNumber newAltPageNumber = new IAScandataAltPageNumber();
                newAltPageNumber.ScandataID = scandataID;
                newAltPageNumber.Sequence = sequence;
                newAltPageNumber.PagePrefix = pagePrefix;
                newAltPageNumber.PageNumber = pageNumber;
                newAltPageNumber.Implied = implied;
                savedAltPageNumber = dal.IAScandataAltPageNumberInsertAuto(null, null, newAltPageNumber);
            }
            else
            {
                if (savedAltPageNumber.PagePrefix != pagePrefix || 
                    savedAltPageNumber.PageNumber != pageNumber ||
                    savedAltPageNumber.Implied != implied)
                {
                    savedAltPageNumber.PagePrefix = pagePrefix;
                    savedAltPageNumber.PageNumber = pageNumber;
                    savedAltPageNumber.Implied = implied;
                    dal.IAScandataAltPageNumberUpdateAuto(null, null, savedAltPageNumber);
                }
            }

            return savedAltPageNumber;
        }

        public void IAScandataDeleteByItem(int itemID)
        {
            new IAScandataDAL().IAScandataDeleteByItem(null, null, itemID);
        }

        /// <summary>
        /// Delete the Scandata record and all associated Scandata alt page type and 
        /// alt page number records for the specified item.
        /// </summary>
        /// <param name="itemID"></param>
        public void IAScandataDeleteAllByItem(int itemID)
        {
            IAScandataDAL dal = new IAScandataDAL();
            CustomGenericList<IAScandata> savedScandata = dal.IAScandataSelectByItem(null, null, itemID);

            foreach (IAScandata scandata in savedScandata)
            {
                new IAScandataAltPageNumberDAL().IAScandataAltPageNumberDeleteByScandataID(null, null, scandata.ScandataID);
                new IAScandataAltPageTypeDAL().IAScandataAltPageTypeDeleteByScandataID(null, null, scandata.ScandataID);
                dal.IAScandataDeleteAuto(null, null, scandata.ScandataID);
            }
        }
    }
}
