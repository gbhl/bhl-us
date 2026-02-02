using System.Configuration;

namespace SearchElasticTest
{
    /*
     * This class assumes a working BHL ElasticSearch installation.  The connection to
     * the search server is not mocked; it is a live connection.  The intent is to ensure
     * that the actual index definitions and queries are correct, so a live server is needed.     * 
     */

    [TestClass]
    public class SearchElasticUnitTest
    {
        private ISearch GetSearch()
        {
            ISearch search = GetSearch();
            search.ServerAddress = ConfigurationManager.AppSettings["ElasticSearchServerAddress"];
            return search;
        }

        [TestMethod]
        public void FullTextSingleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchItem("ocean");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextMultipleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchItem("ocean pollution");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextPhraseTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchItem("\"Costa Rica\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextMultiplePhraseTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchItem("\"Costa Rica\" \"Elmas patillas\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextConditionalAndTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchItem("Charles AND Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextConditionalOrTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchItem("Charles OR Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextConditionalCompoundTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchItem("(Darwin AND (Charles OR Erasmus))");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextSearchWithFacetTest()
        {
            ISearch search = GetSearch();
            List<Tuple<SearchField, string>> limits = new()
            {
                new Tuple<SearchField, string>(SearchField.Genre, "Book")
            };
            ISearchResult result = search.SearchItem("Mollusca", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextSearchPagedTest()
        {
            Search search = new()
            {
                StartPage = 2
            };
            ISearchResult result = search.SearchItem("Mollusca");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextPunctuationTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchItem("O'Grady");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextSpecialCharsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchItem("natural history in nigeria (west-Africa) [2016]");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextDiacriticsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchItem("Delbón");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogSingleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog("ocean");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogMultipleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog("ocean waves");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogPhraseTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog("\"Costa Rica\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogMultiplePhraseTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog("\"Costa Rica\" \"Elmas patillas\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogConditionalAndTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog("Charles AND Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogConditionalOrTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog("Charles OR Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogConditionalCompoundTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog("(Darwin AND (Charles OR Erasmus))");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogSearchWithFacetTest()
        {
            ISearch search = GetSearch();
            List<Tuple<SearchField, string>> limits = new()
            {
                new Tuple<SearchField, string>(SearchField.Genre, "Book")
            };
            ISearchResult result = search.SearchCatalog("Mollusca", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogSearchPagedTest()
        {
            Search search = new()
            {
                StartPage = 2
            };
            ISearchResult result = search.SearchCatalog("Mollusca");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogPunctuationTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog("O'Grady");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogSpecialCharsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog("natural history in nigeria (west-Africa) [2016]");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogDiacriticsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog("Delbón");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorSingleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchAuthor("darwin");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorMultipleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchAuthor("charles darwin");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorPhraseTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchAuthor("\"Charles Darwin\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorMultiplePhraseTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchAuthor("\"charles darwin\" \"erasmus darwin\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorConditionalAndTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchAuthor("Charles AND Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorConditionalOrTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchAuthor("Charles OR Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorConditionalCompoundTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchAuthor("(Darwin AND (Charles OR Erasmus))");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorSearchPagedTest()
        {
            Search search = new()
            {
                StartPage = 2
            };
            ISearchResult result = search.SearchAuthor("Darwin");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorPunctuationTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchAuthor("O'Grady");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorSpecialCharsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchAuthor("smith-rowe [2000]");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorDiacriticsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchAuthor("Delbón");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordSingleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchKeyword("periodicals");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordMultipleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchKeyword("united states");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordPhraseTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchKeyword("\"united states\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordMultiplePhraseTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchKeyword("\"atlantic ocean\" \"marine animals\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordConditionalAndTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchKeyword("biology AND ecology");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordConditionalOrTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchKeyword("biology OR ecology");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordConditionalCompoundTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchKeyword("(marine AND (biology OR ecology))");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordSearchPagedTest()
        {
            Search search = new()
            {
                StartPage = 2
            };
            ISearchResult result = search.SearchKeyword("birds");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordPunctuationTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchKeyword("O'Grady");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordSpecialCharsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchKeyword("smith-rowe [2000]");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordDiacriticsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchKeyword("Delbón");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameSingleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchName("mollusca");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameMultipleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchName("Elmas hibbsi");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NamePhraseTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchName("\"Elmas hibbsi\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameMultiplePhraseTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchName("\"Abra brasiliana\" \"E.A. Smith\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameConditionalAndTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchName("Abra AND Smith");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameConditionalOrTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchName("Abra OR Smith");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameConditionalCompoundTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchName("(Abies AND (grandis OR nobilis))");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameSearchPagedTest()
        {
            Search search = new()
            {
                StartPage = 2
            };
            ISearchResult result = search.SearchName("mollusca");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NamePunctuationTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchName("O'Grady");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameSpecialCharsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchName("smith-rowe [2000]");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameDiacriticsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchName("Delbón");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageSingleWordTest()
        {
            ISearch search = GetSearch();
            List<Tuple<SearchField, string>> limits = new()
            {
                new Tuple<SearchField, string>(SearchField.ItemID, "22803")
            };
            ISearchResult result = search.SearchPage("ocean", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageMultipleWordTest()
        {
            ISearch search = GetSearch();
            List<Tuple<SearchField, string>> limits = new()
            {
                new Tuple<SearchField, string>(SearchField.ItemID, "22803")
            };
            ISearchResult result = search.SearchPage("ocean pollution", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PagePhraseTest()
        {
            ISearch search = GetSearch();
            List<Tuple<SearchField, string>> limits = new()
            {
                new Tuple<SearchField, string>(SearchField.ItemID, "22803")
            };
            ISearchResult result = search.SearchPage("\"ocean pollution\"", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageMultiplePhraseTest()
        {
            ISearch search = GetSearch();
            List<Tuple<SearchField, string>> limits = new()
            {
                new Tuple<SearchField, string>(SearchField.ItemID, "22803")
            };
            ISearchResult result = search.SearchPage("\"ocean pollution\" \"marine life\"", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageConditionalAndTest()
        {
            ISearch search = GetSearch();
            List<Tuple<SearchField, string>> limits = new()
            {
                new Tuple<SearchField, string>(SearchField.ItemID, "22803")
            };
            ISearchResult result = search.SearchPage("ocean AND marine", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageConditionalOrTest()
        {
            ISearch search = GetSearch();
            List<Tuple<SearchField, string>> limits = new()
            {
                new Tuple<SearchField, string>(SearchField.ItemID, "22803")
            };
            ISearchResult result = search.SearchPage("ocean OR marine", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageConditionalCompoundTest()
        {
            ISearch search = GetSearch();
            List<Tuple<SearchField, string>> limits = new()
            {
                new Tuple<SearchField, string>(SearchField.ItemID, "22803")
            };
            ISearchResult result = search.SearchPage("(life AND (ocean OR marine))", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PagePunctuationTest()
        {
            ISearch search = GetSearch();
            List<Tuple<SearchField, string>> limits = new()
            {
                new Tuple<SearchField, string>(SearchField.ItemID, "22803")
            };
            ISearchResult result = search.SearchPage("O'Grady", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageSpecialCharsTest()
        {
            ISearch search = GetSearch();
            List<Tuple<SearchField, string>> limits = new()
            {
                new Tuple<SearchField, string>(SearchField.ItemID, "22803")
            };
            ISearchResult result = search.SearchPage("smith-rowe [2000]", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageDiacriticsTest()
        {
            ISearch search = GetSearch();
            List<Tuple<SearchField, string>> limits = new()
            {
                new Tuple<SearchField, string>(SearchField.ItemID, "22803")
            };
            ISearchResult result = search.SearchPage("Delbón", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedTitleAllWordsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean margins", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null, 
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedTitlePunctuationTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("O'Grady", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedTitleSpecialCharsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("smith-rowe [2000]", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedTitleDiacriticsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("Delbón", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }


        [TestMethod]
        public void AdvancedTitleExactPhraseTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean margins", SearchStringParamOperator.Phrase),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedAuthorSingleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("huxley", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedAuthorPunctuationTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("O'Grady", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedAuthorSpecialCharsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("smith-rowe [2000]", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedAuthorDiacriticsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("Delbón", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedAuthorMultipleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("thomas huxley", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedKeywordSingleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("Mammals", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedKeywordPunctuationTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("O'Grady", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedKeywordSpecialCharsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("smith-rowe [2000]", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedKeywordDiacriticsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("Delbón", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedKeywordMultipleWordTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("marine animals", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedNoteAllWordsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("water column", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedNotePunctuationTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("O'Grady", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedNoteSpecialCharsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("smith-rowe [2000]", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedNoteDiacriticsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("Delbón", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedNoteExactPhraseTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("water column", SearchStringParamOperator.Phrase),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedTextAllWordsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("marine life", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedTextPunctuationTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("O'Grady", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedTextSpecialCharsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("smith-rowe [2000]", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedTextDiacriticsTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("Delbón", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedTextExactPhraseTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.Phrase),
                new SearchStringParam("marine life", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedYearTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "1994",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedLanguageTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                new Tuple<string, string>("ENG", "English"),
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedCollectionTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                new Tuple<string, string>("12", "MBLWHOI Library, Woods Hole"),
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedCompoundTest()
        {
            ISearch search = GetSearch();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("guide to birds", SearchStringParamOperator.And),
                new SearchStringParam("hoffmann", SearchStringParamOperator.And),
                "", "1904",
                new SearchStringParam("new england", SearchStringParamOperator.And),
                new Tuple<string, string>("ENG", "English"),
                new Tuple<string, string>("12", "MBLWHOI Library, Woods Hole"),
                new SearchStringParam("aristotle", SearchStringParamOperator.And),
                new SearchStringParam("horned owls", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }
    }
}