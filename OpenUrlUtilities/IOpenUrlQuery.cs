using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.OpenUrl.Utilities
{
    public interface IOpenUrlQuery
    {
        void SetQuery(String queryString);
        bool ValidateQuery();

        String Version { get; set; }                 // OpenUrl version
        String ValidationError { get; set; }         // Validation error message

        String Format { get; set; }                  // rft_val_fmt (OpenUrl 1.0)
        String Genre { get; set; }                   // genre / rft.genre
        String PrivateData { get; set; }             // rft_dat (OpenURL 1.0)

        String AuthorLast { get; set; }              // aulast / rft.aulast
        String AuthorFirst { get; set; }             // aufirst / rft.aufirst
        String AuthorInitial { get; set; }           // auinit / rft.auinit
        String AuthorInitial1 { get; set; }          // auinit1 / rft.auinit1
        String AuthorInitialMiddle { get; set; }     // auinitm / rft.auinitm
        String AuthorSuffix { get; set; }            // rft.ausuffix
        String AuthorCorporation { get; set; }       // rft.aucorp

        String Issn { get; set; }                    // issn / rft.issn
        String Eissn { get; set; }                   // eissn
        String Coden { get; set; }                   // coden
        String Isbn { get; set; }                    // isbn / rft.isbn
        //String Lccn { get; set; }                    // pid=lccn:XXXX / rft_id=info:lccn/XXXX
        //String Oclc { get; set; }                    // pid=oclcnum:XXXX / rft_id=info:oclcnum:XXXX
        NonUniqueHashtable Identifiers { get; set; } // pid=title:XXXX / rft_id

        String Publisher { get; set; }               // publisher / rft.publisher  (combines publisher place + name)
        String PublisherName { get; set; }           // rft.pub
        String PublisherPlace { get; set; }          // rft.place

        String BookTitle { get; set; }               // title / rft.btitle
        String JournalTitle { get; set; }            // title / rft.jtitle
        String ArticleTitle { get; set; }            // atitle / rft.atitle
        String ShortTitle { get; set; }              // stitle / rft.stitle

        String Volume { get; set; }                  // volume / rft.volume
        String Issue { get; set; }                   // issue / rft.issue
        String Part { get; set; }                    // part
        String StartPage { get; set; }               // spage / rft.spage
        String EndPage { get; set; }                 // epage / rft.epage
        String Pages { get; set; }                   // pages
        String ArticleNumber { get; set; }           // artnum
        String Season { get; set; }                  // ssn
        String Quarter { get; set; }                 // quarter
        OpenUrlQueryDate Date { get; set; }          // date / rft.date
    }
}
