using System;
using System.Web.Services;
using System.ComponentModel;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.WebService
{
    public partial class BHLWS : System.Web.Services.WebService
    {
        [WebMethod]
        public void IndicatedPageSave(int[] pageIDs, string pagePrefix,
            int style, string start, int increment, bool implied, int userID)
        {
            IndicatedPageStyle indicatedPageStyle = (IndicatedPageStyle)style;
            
            BHLProvider bhlServer = new BHLProvider();
            bhlServer.IndicatedPageSave(pageIDs, pagePrefix, 
                indicatedPageStyle, start, increment, implied, userID);
        }

        [WebMethod]
        public void IndicatedPageDeleteAllForPage(int[] pageIDs, int userID)
        {
            BHLProvider bhlServer = new BHLProvider();
            bhlServer.IndicatedPageDeleteAllForPage(pageIDs, userID);
        }
    }
}
