using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Web2.Models
{
    public class AuthorsBrowseModel
    {
        public string Start { get; set; }
        public int AuthorPage { get; set; }
        public int NumPerPage { get; set; }
        public int TotalAuthors { get; set; }
        public List<Author> AuthorResults { get; set; } = new List<Author>();
    }
}