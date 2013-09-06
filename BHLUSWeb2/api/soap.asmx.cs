using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using CustomDataAccess;
using MOBOT.BHL.API.BHLApiDataObjects;

namespace MOBOT.BHL.Web2.api
{
    /// <summary>
    /// Summary description for soap
    /// </summary>
    [WebService(Namespace = "http://www.biodiversitylibrary.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class soap : System.Web.Services.WebService
    {
        // These methods wrap the previously existing Name Services.  This service endpoint becomes the
        // preferred endpoint for the Name Services, but the existing endpoints will remain available
        // for the forseeable future.     MWL 2010/1/4

        [WebMethod]
        public int NameCount()
        {
            MOBOT.BHL.Web2.Services.NameService nameService = new MOBOT.BHL.Web2.Services.NameService();
            return nameService.NameCount();
        }

        [WebMethod]
        public int NameCountBetweenDates(string startDate, string endDate)
        {
            MOBOT.BHL.Web2.Services.NameService nameService = new MOBOT.BHL.Web2.Services.NameService();
            return nameService.NameCountBetweenDates(startDate, endDate);
        }
        
        [WebMethod]
        public Name NameGetDetail(string nameBankID)
        {
            MOBOT.BHL.Web2.Services.NameService nameService = new MOBOT.BHL.Web2.Services.NameService();
            return nameService.NameGetDetail(nameBankID);
        }

        [WebMethod]
        public CustomGenericList<Name> NameList(string startRow, string batchSize)
        {
            MOBOT.BHL.Web2.Services.NameService nameService = new MOBOT.BHL.Web2.Services.NameService();
            return nameService.NameList(startRow, batchSize);
        }

        [WebMethod]
        public CustomGenericList<Name> NameListBetweenDates(string startRow, string batchSize, string startDate, string endDate)
        {
            MOBOT.BHL.Web2.Services.NameService nameService = new MOBOT.BHL.Web2.Services.NameService();
            return nameService.NameListBetweenDates(startRow, batchSize, startDate, endDate);
        }
        
        [WebMethod]
        public CustomGenericList<Name> NameSearch(string name)
        {
            MOBOT.BHL.Web2.Services.NameService nameService = new MOBOT.BHL.Web2.Services.NameService();
            return nameService.NameSearch(name);
        }
    }
}
