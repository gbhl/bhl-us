using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using CustomDataAccess;
using MOBOT.BHL.API.BHLApiDataObjects;
using MOBOT.BHL.API.BHLApiDAL;
using System.Collections.Generic;

namespace MOBOT.BHL.Web2.Services
{
    /// <summary>
    /// Summary description for NameService
    /// </summary>
    [WebService(Namespace = "http://mobot.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class NameService : System.Web.Services.WebService
    {
        /// <summary>
        /// Returns a count of the unique names.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public int NameCount()
        {
            return (new NameApiDAL().NameCountUniqueConfirmed(null, null));
        }

        /// <summary>
        /// Returns a count of the unique names added or updated between the specified dates.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public int NameCountBetweenDates(string startDate, string endDate)
        {
            DateTime startDT;
            DateTime endDT;
            if (!DateTime.TryParse(startDate, out startDT))
            {
                throw new SoapException("startDate (" + startDate + ") must be a valid date value (MM/DD/YYYY).", SoapException.ClientFaultCode);
            }
            if (!DateTime.TryParse(endDate, out endDT))
            {
                throw new SoapException("endDate (" + endDate + ") must be a valid date value (MM/DD/YYYY).", SoapException.ClientFaultCode);
            }

            return (new NameApiDAL().NameCountUniqueConfirmedBetweenDates(null, null, startDT, endDT));
        }

        [WebMethod]
        public CustomGenericList<Name> NameList(string startRow, string batchSize)
        {
            // Validate the input
            int startRowValid;
            int batchSizeValid;
            this.ValidateNameListStartAndBatch(startRow, batchSize, out startRowValid, out batchSizeValid);

            return new NameApiDAL().NameListActive(null, null, startRowValid, batchSizeValid);
        }

        [WebMethod]
        public CustomGenericList<Name> NameListBetweenDates(string startRow, string batchSize, string startDate, string endDate)
        {
            // Validate the input
            int startRowValid;
            int batchSizeValid;
            this.ValidateNameListStartAndBatch(startRow, batchSize, out startRowValid, out batchSizeValid);

            DateTime startDT;
            DateTime endDT;
            if (!DateTime.TryParse(startDate, out startDT))
            {
                throw new SoapException("startDate (" + startDate + ") must be a valid date value (MM/DD/YYYY).", SoapException.ClientFaultCode);
            }
            if (!DateTime.TryParse(endDate, out endDT))
            {
                throw new SoapException("endDate (" + endDate + ") must be a valid date value (MM/DD/YYYY).", SoapException.ClientFaultCode);
            }

            return new NameApiDAL().NameListActiveBetweenDates(null, null, startRowValid, batchSizeValid, startDT, endDT);
        }

        /// <summary>
        /// Validate the startRow and batchSize parameters used by the NameList* web methods.
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="batchSize"></param>
        /// <param name="startRowValid"></param>
        /// <param name="batchSizeValid"></param>
        private void ValidateNameListStartAndBatch(string startRow, string batchSize, out int startRowValid, out int batchSizeValid)
        {
            // Validate the input
            double startRowDouble;
            double batchSizeDouble;
            if (!Double.TryParse(startRow, out startRowDouble))
            {
                throw new SoapException("startRow (" + startRow + ") must be a valid integer value.", SoapException.ClientFaultCode);
            }
            else
            {
                startRowValid = (int)Math.Floor(startRowDouble);
            }
            if (!Double.TryParse(batchSize, out batchSizeDouble))
            {
                throw new SoapException("batchSize (" + batchSize + ") must be a valid integer value.", SoapException.ClientFaultCode);
            }
            else
            {
                batchSizeValid = (int)Math.Floor(batchSizeDouble);
            }

            if (batchSizeValid > 1000)
            {
                throw new SoapException("batchSize (" + batchSize + ") must be between 1 and 1000.", SoapException.ClientFaultCode);
            }
        }

        /*
        [WebMethod]
        // FOR TESTING PURPOSES ONLY!!!!!
        public CustomGenericList<Name> NameList2(int startRow, int batchSize)
        {
            if (batchSize > 1000)
            {
                throw new SoapException("batchSize (" + batchSize + ") must be between 1 and 1000.", SoapException.ClientFaultCode);
            }
            return NameServiceDAL.PageNameListActive(null, null, startRow, batchSize);
        }
         */

        [WebMethod]
        public CustomGenericList<Name> NameSearch(string name)
        {
            if (name == String.Empty)
            {
                throw new SoapException("Please supply a name for which to search.", SoapException.ClientFaultCode);
            }

            try
            {
                CustomGenericList<Name> names = new CustomGenericList<Name>();

                // Use the existing name search functionality
                Server.BHLProvider provider = new Server.BHLProvider();
                List<DataObjects.NameResolved> namesResolved = provider.NameResolvedSelectByNameLike(name, 100000);
                foreach (DataObjects.NameResolved nameResolved in namesResolved)
                {
                    // Add names to the list to be returned
                    Name nameResult = new Name();
                    nameResult.NameConfirmed = nameResolved.ResolvedNameString;
                    nameResult.NameBankID = nameResolved.NameBankID;
                    names.Add(nameResult);
                }

                return names;
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }

        [WebMethod]
        public Name NameGetDetail(string nameBankID)
        {
            // Validate the input
            double nameBankIDDouble;
            if (!Double.TryParse(nameBankID, out nameBankIDDouble))
            {
                throw new SoapException("nameBankID (" + nameBankID + ") must be a valid integer value.", SoapException.ClientFaultCode);
            }

            Name name = null;
            MOBOT.BHL.API.BHLApiDataObjects.Title currentTitle = null;
            Item currentItem = null;
            Page currentPage = null;
            PageType pageType = null;

            try
            {
                CustomGenericList<PageDetail> pageDetails = new NameApiDAL().PageSelectByNameBankID(null, null, nameBankID);

                if (pageDetails.Count > 0)
                {
                    // Get the name information
                    name = new Name();
                    name.NameBankID = pageDetails[0].NameBankID;
                    name.NameConfirmed = pageDetails[0].NameConfirmed;
                    name.Titles = new CustomGenericList<MOBOT.BHL.API.BHLApiDataObjects.Title>();

                    currentTitle = new MOBOT.BHL.API.BHLApiDataObjects.Title();
                    currentItem = new Item();
                    currentPage = new Page();

                    // Get the title, item, and page information
                    foreach (PageDetail pageDetail in pageDetails)
                    {
                        if (pageDetail.TitleID != currentTitle.TitleID)
                        {
                            // Add a new title
                            MOBOT.BHL.API.BHLApiDataObjects.Title title = new MOBOT.BHL.API.BHLApiDataObjects.Title();
                            title.TitleID = pageDetail.TitleID;
                            title.MarcBibID = pageDetail.MarcBibID;
                            title.PublicationTitle = pageDetail.PublicationTitle;
                            title.PublicationDetails = pageDetail.PublicationDetails;
                            title.Author = pageDetail.Author;
                            title.BPH = pageDetail.BPH;
                            title.TL2 = pageDetail.TL2;
                            title.Abbreviation = pageDetail.Abbreviation;
                            title.TitleUrl = pageDetail.TitleUrl;
                            title.Items = new CustomGenericList<Item>();
                            name.Titles.Add(title);
                            currentTitle = title;
                        }

                        if (pageDetail.ItemID != currentItem.ItemID)
                        {
                            // Add a new item
                            Item item = new Item();
                            item.ItemID = pageDetail.ItemID;
                            item.BarCode = pageDetail.BarCode;
                            item.MarcItemID = pageDetail.MarcItemID;
                            item.CallNumber = pageDetail.CallNumber;
                            item.VolumeInfo = pageDetail.VolumeInfo;
                            item.ItemUrl = pageDetail.ItemUrl;
                            item.Pages = new CustomGenericList<Page>();
                            currentTitle.Items.Add(item);
                            currentItem = item;
                        }

                        if (pageDetail.PageID != currentPage.PageID || pageDetail.Number != currentPage.Number)
                        {
                            // Add a new page
                            Page page = new Page();
                            page.PageID = pageDetail.PageID;
                            page.Year = pageDetail.Year;
                            page.Volume = pageDetail.Volume;
                            page.Issue = pageDetail.Issue;
                            page.Prefix = pageDetail.Prefix;
                            page.Number = pageDetail.Number;
                            page.PageUrl = pageDetail.PageUrl;
                            page.ThumbnailUrl = pageDetail.ThumbnailUrl;
                            page.FullSizeImageUrl = pageDetail.FullSizeImageUrl;
                            page.OcrUrl = pageDetail.OcrUrl;
                            page.ImageUrl = pageDetail.ImageUrl;
                            page.PageTypes = new CustomGenericList<PageType>();
                            currentItem.Pages.Add(page);
                            currentPage = page;
                        }

                        if (pageDetail.PageTypeName != String.Empty)
                        {
                            // Add a new page type
                            pageType = new PageType();
                            pageType.PageTypeName = pageDetail.PageTypeName;
                            currentPage.PageTypes.Add(pageType);
                        }
                    }
                }

                return name;
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ServerFaultCode);
            }
        }
    }
}
