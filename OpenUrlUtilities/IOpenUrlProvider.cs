using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.OpenUrl.Utilities
{
    public interface IOpenUrlProvider
    {
        IOpenUrlResponse FindCitation(IOpenUrlQuery query);
    }
}
