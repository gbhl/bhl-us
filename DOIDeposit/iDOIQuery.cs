using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.DOIDeposit
{
    public interface iDOIQuery
    {
        string ToString();
        string ToString(string template);
    }
}
