using System;
using System.Collections.Generic;
namespace BHLApi3Web.Services
{
    public interface IResolverService
    {
        List<Models.ResolutionResult> Resolve(string title, string authors, string year, string type, List<Models.ResolutionResult> results);
    }
}
