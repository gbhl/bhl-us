using System;
using System.Collections.Generic;
namespace BHLApi3Web.Services
{
    public interface ITitlesService
    {
        BHLApi3Web.Models.Title GetTitle(int id);
        IEnumerable<BHLApi3Web.Models.Title> GetSearchSince(string since, string until);
    }
}
