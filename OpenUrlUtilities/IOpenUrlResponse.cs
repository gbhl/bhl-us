using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.OpenUrl.Utilities
{
    public interface IOpenUrlResponse
    {
        String ToXml();
        String ToJson();

        ResponseStatus Status { get; set; }
        String Message { get; set; }

        List<OpenUrlResponseCitation> citations { get; set; }
    }

    public enum ResponseStatus
    {
        Undefined,  // Query not submitted
        Success,
        Error
    }
}
