using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Xml;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.Web2.Services
{
	/// <summary>
	/// Summary description for PageSummaryService
	/// </summary>
	[WebService( Namespace = "http://tempuri.org/" )]
	[WebServiceBinding( ConformsTo = WsiProfiles.BasicProfile1_1 )]
	[ToolboxItem( false )]
	[ScriptService]
	public class PageSummaryService : System.Web.Services.WebService
	{
		[WebMethod]
		[ScriptMethod( UseHttpGet = true )]
		public string[] FetchPageUrl( int pageID )
		{
			string[] sa = new string[ 6 ];
			PageSummaryView psv = new BHLProvider().PageSummarySelectByPageId( pageID );
            if (psv != null)
            {
                sa[0] = psv.PageID.ToString();
                sa[1] = GetTextUrl(psv);    // identifies whether an OCR file was found
                sa[2] = ReplaceReturnsWithBreaks(GetOcrText(pageID));
            }
			return ( sa );
		}

		private string GetTextUrl( PageSummaryView psv )
		{
            return new BHLProvider().GetTextUrl(ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true", psv.OcrTextLocation);
		}

		private string GetOcrText( int pageID)
		{
			try
			{
                SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
                return service.GetOcrText(pageID);
			}
			catch ( Exception ex )
			{
                return new DebugUtility(ConfigurationManager.AppSettings["DebugValue"]).GetErrorInfo(this.Context.Request, ex);
			}
		}

		[WebMethod]
        public List<NameFinderResponse> GetNamesFromOcr(int pageID)
		{
            return new BHLProvider().GetNamesFromOcr(
                ConfigurationManager.AppSettings["NameFinderService"],
                pageID,
                ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true",
                ConfigurationManager.AppSettings["UsePreferredNameResults"] == "true",
                Convert.ToInt32(ConfigurationManager.AppSettings["MaxReadAttempts"]));
		}

		private string ReplaceReturnsWithBreaks( string input )
		{
			return input.Replace( "\n", "<br />" );
		}
	}
}
