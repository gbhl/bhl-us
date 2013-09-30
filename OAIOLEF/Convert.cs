using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOBOT.BHL.OAI2;

namespace MOBOT.BHL.OAIOLEF
{
    public class Convert
    {
        OAIRecord _oaiRecord;

        public Convert(OAIRecord oaiRecord)
        {
            _oaiRecord = oaiRecord;
        }

        #region ToString

        public new String ToString()
        {
            throw new NotImplementedException();

            StringBuilder sb = new StringBuilder();





            return sb.ToString();
        }

        #endregion ToString
    }
}
