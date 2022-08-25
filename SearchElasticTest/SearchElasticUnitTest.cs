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
        [TestMethod]
        public void FullTextSingleWordTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchItem("ocean");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextMultipleWordTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchItem("ocean pollution");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextPhraseTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchItem("\"Costa Rica\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextMultiplePhraseTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchItem("\"Costa Rica\" \"Elmas patillas\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextConditionalAndTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchItem("Charles AND Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextConditionalOrTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchItem("Charles OR Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextConditionalCompoundTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchItem("(Darwin AND (Charles OR Erasmus))");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextSearchWithFacetTest()
        {
            Search search = new Search();
            List<Tuple<SearchField, string>> limits = new List<Tuple<SearchField, string>>();
            limits.Add(new Tuple<SearchField, string>(SearchField.Genre, "Book"));
            ISearchResult result = search.SearchItem("Mollusca", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextSearchPagedTest()
        {
            Search search = new Search();
            search.StartPage = 2;
            ISearchResult result = search.SearchItem("Mollusca");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogSingleWordTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchCatalog("ocean");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogMultipleWordTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchCatalog("ocean waves");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogPhraseTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchCatalog("\"Costa Rica\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogMultiplePhraseTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchCatalog("\"Costa Rica\" \"Elmas patillas\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogConditionalAndTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchCatalog("Charles AND Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogConditionalOrTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchCatalog("Charles OR Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogConditionalCompoundTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchCatalog("(Darwin AND (Charles OR Erasmus))");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogSearchWithFacetTest()
        {
            Search search = new Search();
            List<Tuple<SearchField, string>> limits = new List<Tuple<SearchField, string>>();
            limits.Add(new Tuple<SearchField, string>(SearchField.Genre, "Book"));
            ISearchResult result = search.SearchCatalog("Mollusca", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogSearchPagedTest()
        {
            Search search = new Search();
            search.StartPage = 2;
            ISearchResult result = search.SearchCatalog("Mollusca");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorSingleWordTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchAuthor("darwin");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorMultipleWordTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchAuthor("charles darwin");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorPhraseTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchAuthor("\"Charles Darwin\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorMultiplePhraseTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchAuthor("\"charles darwin\" \"erasmus darwin\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorConditionalAndTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchAuthor("Charles AND Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorConditionalOrTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchAuthor("Charles OR Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorConditionalCompoundTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchAuthor("(Darwin AND (Charles OR Erasmus))");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorSearchPagedTest()
        {
            Search search = new Search();
            search.StartPage = 2;
            ISearchResult result = search.SearchAuthor("Darwin");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordSingleWordTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchKeyword("periodicals");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordMultipleWordTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchKeyword("united states");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordPhraseTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchKeyword("\"united states\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordMultiplePhraseTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchKeyword("\"atlantic ocean\" \"marine animals\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordConditionalAndTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchKeyword("biology AND ecology");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordConditionalOrTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchKeyword("biology OR ecology");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordConditionalCompoundTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchKeyword("(marine AND (biology OR ecology))");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordSearchPagedTest()
        {
            Search search = new Search();
            search.StartPage = 2;
            ISearchResult result = search.SearchKeyword("birds");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameSingleWordTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchName("mollusca");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameMultipleWordTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchName("Elmas hibbsi");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NamePhraseTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchName("\"Elmas hibbsi\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameMultiplePhraseTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchName("\"Abra brasiliana\" \"E.A. Smith\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameConditionalAndTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchName("Abra AND Smith");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameConditionalOrTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchName("Abra OR Smith");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameConditionalCompoundTest()
        {
            Search search = new Search();
            ISearchResult result = search.SearchName("(Abies AND (grandis OR nobilis))");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameSearchPagedTest()
        {
            Search search = new Search();
            search.StartPage = 2;
            ISearchResult result = search.SearchName("mollusca");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageSingleWordTest()
        {
            Search search = new Search();
            List<Tuple<SearchField, string>> limits = new List<Tuple<SearchField, string>>();
            limits.Add(new Tuple<SearchField, string>(SearchField.ItemID, "22803"));            
            ISearchResult result = search.SearchPage("ocean", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageMultipleWordTest()
        {
            Search search = new Search();
            List<Tuple<SearchField, string>> limits = new List<Tuple<SearchField, string>>();
            limits.Add(new Tuple<SearchField, string>(SearchField.ItemID, "22803"));
            ISearchResult result = search.SearchPage("ocean pollution", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PagePhraseTest()
        {
            Search search = new Search();
            List<Tuple<SearchField, string>> limits = new List<Tuple<SearchField, string>>();
            limits.Add(new Tuple<SearchField, string>(SearchField.ItemID, "22803"));
            ISearchResult result = search.SearchPage("\"ocean pollution\"", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageMultiplePhraseTest()
        {
            Search search = new Search();
            List<Tuple<SearchField, string>> limits = new List<Tuple<SearchField, string>>();
            limits.Add(new Tuple<SearchField, string>(SearchField.ItemID, "22803"));
            ISearchResult result = search.SearchPage("\"ocean pollution\" \"marine life\"", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageConditionalAndTest()
        {
            Search search = new Search();
            List<Tuple<SearchField, string>> limits = new List<Tuple<SearchField, string>>();
            limits.Add(new Tuple<SearchField, string>(SearchField.ItemID, "22803"));
            ISearchResult result = search.SearchPage("ocean AND marine", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageConditionalOrTest()
        {
            Search search = new Search();
            List<Tuple<SearchField, string>> limits = new List<Tuple<SearchField, string>>();
            limits.Add(new Tuple<SearchField, string>(SearchField.ItemID, "22803"));
            ISearchResult result = search.SearchPage("ocean OR marine", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageConditionalCompoundTest()
        {
            Search search = new Search();
            List<Tuple<SearchField, string>> limits = new List<Tuple<SearchField, string>>();
            limits.Add(new Tuple<SearchField, string>(SearchField.ItemID, "22803"));
            ISearchResult result = search.SearchPage("(life AND (ocean OR marine))", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedTitleAllWordsTest()
        {
            Search search = new Search();
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
        public void AdvancedTitleExactPhraseTest()
        {
            Search search = new Search();
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
            Search search = new Search();
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
        public void AdvancedAuthorMultipleWordTest()
        {
            Search search = new Search();
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
            Search search = new Search();
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
        public void AdvancedKeywordMultipleWordTest()
        {
            Search search = new Search();
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
            Search search = new Search();
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
        public void AdvancedNoteExactPhraseTest()
        {
            Search search = new Search();
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
            Search search = new Search();
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
        public void AdvancedTextExactPhraseTest()
        {
            Search search = new Search();
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
            Search search = new Search();
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
            Search search = new Search();
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
            Search search = new Search();
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
            Search search = new Search();
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