using CustomDataAccess;
using MOBOT.IAAnalysis.DAL;
using MOBOT.IAAnalysis.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.IAAnalysis.Server
{
    public partial class IAAnalysisProvider
    {
        private const int ITEMSTATUS_NEW = 10;
        private const int ITEMSTATUS_DONE = 20;
        private const int ITEMSTATUS_ERROR = 99;

        public Item SaveItemIdentifier(string identifier)
        {
            ItemDAL dal = new ItemDAL();
            Item savedItem = dal.ItemSelectByIdentifier(null, null, identifier);

            if (savedItem == null)
            {
                Item newItem = new Item();
                newItem.Identifier = identifier;
                newItem.ItemStatusID = ITEMSTATUS_NEW;
                savedItem = dal.ItemInsertAuto(null, null, newItem);
            }

            return savedItem;
        }

        public Item ItemSelectAuto(int itemID)
        {
            return (new ItemDAL().ItemSelectAuto(null, null, itemID));
        }

        public List<Item> ItemSelectForXMLDownload()
        {
            return (new ItemDAL().ItemSelectForXMLDownload(null, null));
        }

        public Item ItemUpdateItemStatusIDAfterDataHarvest(int itemID)
        {
            ItemDAL dal = new ItemDAL();
            Item savedItem = dal.ItemSelectAuto(null, null, itemID);
            if (savedItem != null)
            {
                if ((String.Compare(savedItem.MetaGetStatus, "OK", true) == 0) &&
                    (String.Compare(savedItem.MarcGetStatus, "OK", true) == 0))
                    savedItem.ItemStatusID = ITEMSTATUS_DONE;

                savedItem = dal.ItemUpdateAuto(null, null, savedItem);
            }
            return savedItem;
        }

        public Item ItemUpdateMetaGetStatus(int itemID, string metaGetStatus)
        {
            ItemDAL dal = new ItemDAL();
            Item savedItem = dal.ItemSelectAuto(null, null, itemID);
            if (savedItem != null)
            {
                savedItem.MetaGetStatus = metaGetStatus;
                savedItem = dal.ItemUpdateAuto(null, null, savedItem);
            }
            return savedItem;
        }

        public Item ItemUpdateMarcGetStatus(int itemID, string marcGetStatus)
        {
            ItemDAL dal = new ItemDAL();
            Item savedItem = dal.ItemSelectAuto(null, null, itemID);
            if (savedItem != null)
            {
                savedItem.MarcGetStatus = marcGetStatus;
                savedItem = dal.ItemUpdateAuto(null, null, savedItem);
            }
            return savedItem;
        }

        public Item ItemUpdateMARCLeader(int itemID, string marcLeader)
        {
            ItemDAL dal = new ItemDAL();
            Item savedItem = dal.ItemSelectAuto(null, null, itemID);
            if (savedItem != null)
            {
                savedItem.MARCLeader = marcLeader;
                savedItem = dal.ItemUpdateAuto(null, null, savedItem);
            }
            return savedItem;
        }

        /// <summary>
        /// Update the item with metadata information.
        /// </summary>
        public Item ItemUpdateMetadata(int itemID, string sponsor, string contributor,
            string scanningCenter, string collectionLibrary, string callNumber, int imageCount, 
            string curationState, string possibleCopyrightStatus, string volume, string scanDate, 
            DateTime? addedDate, DateTime? publicDate, DateTime? updateDate, string sponsorDate)
        {
            ItemDAL dal = new ItemDAL();
            Item savedItem = dal.ItemSelectAuto(null, null, itemID);
            if (savedItem != null)
            {
                savedItem.Sponsor = sponsor;
                savedItem.Contributor = contributor;
                savedItem.ScanningCenter = scanningCenter;
                savedItem.CollectionLibrary = collectionLibrary;
                savedItem.CallNumber = callNumber;
                savedItem.ImageCount = imageCount;
                savedItem.CurationState = curationState;
                savedItem.PossibleCopyrightStatus = possibleCopyrightStatus;
                savedItem.Volume = volume;
                savedItem.ScanDate = scanDate;
                savedItem.AddedDate = addedDate;
                savedItem.PublicDate = publicDate;
                savedItem.UpdateDate = updateDate;
                savedItem.SponsorDate = sponsorDate;
                savedItem = dal.ItemUpdateAuto(null, null, savedItem);
            }
            else
            {
                throw new Exception("Could not find existing Item record.");
            }
            return savedItem;
        }

        public DateTime ItemSelectNextStartDate()
        {
            List<CustomDataRow> rows = new ItemDAL().ItemSelectNextStartDate(null, null);

            // Convert custom DAL object to generic values
            DateTime returnDate = DateTime.Parse("1/1/1980");
            if (rows.Count > 0)
            {
                CustomDataRow row = rows[0];
                returnDate = (DateTime)row[0].Value;
            }

            return returnDate;
        }
    }
}
