using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public ScanningRequest ScanRequestInsertAuto(int? geminiIssueId, string title, string year, string type,
            string volume, string edition, string oclc, string isbn, string issn, string author,
            string publisher, string language, string note)
        {
            return new ScanningRequestDAL().ScanningRequestInsertAuto(null, null, geminiIssueId, title, year, type,
                volume, edition, oclc, isbn, issn, author, publisher, language, note);
        }
    }
}
