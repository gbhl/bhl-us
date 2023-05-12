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
            Search search = new();
            ISearchResult result = search.SearchItem("ocean");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextMultipleWordTest()
        {
            Search search = new();
            ISearchResult result = search.SearchItem("ocean pollution");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextPhraseTest()
        {
            Search search = new();
            ISearchResult result = search.SearchItem("\"Costa Rica\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextMultiplePhraseTest()
        {
            Search search = new();
            ISearchResult result = search.SearchItem("\"Costa Rica\" \"Elmas patillas\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextConditionalAndTest()
        {
            Search search = new();
            ISearchResult result = search.SearchItem("Charles AND Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextConditionalOrTest()
        {
            Search search = new();
            ISearchResult result = search.SearchItem("Charles OR Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextConditionalCompoundTest()
        {
            Search search = new();
            ISearchResult result = search.SearchItem("(Darwin AND (Charles OR Erasmus))");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextSearchWithFacetTest()
        {
            Search search = new();
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
            Search search = new();
            ISearchResult result = search.SearchItem("O'Grady");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextSpecialCharsTest()
        {
            Search search = new();
            ISearchResult result = search.SearchItem("natural history in nigeria (west-Africa) [2016]");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void FullTextDiacriticsTest()
        {
            Search search = new();
            ISearchResult result = search.SearchItem("Delb�n");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogSingleWordTest()
        {
            Search search = new();
            ISearchResult result = search.SearchCatalog("ocean");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogMultipleWordTest()
        {
            Search search = new();
            ISearchResult result = search.SearchCatalog("ocean waves");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogPhraseTest()
        {
            Search search = new();
            ISearchResult result = search.SearchCatalog("\"Costa Rica\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogMultiplePhraseTest()
        {
            Search search = new();
            ISearchResult result = search.SearchCatalog("\"Costa Rica\" \"Elmas patillas\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogConditionalAndTest()
        {
            Search search = new();
            ISearchResult result = search.SearchCatalog("Charles AND Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogConditionalOrTest()
        {
            Search search = new();
            ISearchResult result = search.SearchCatalog("Charles OR Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogConditionalCompoundTest()
        {
            Search search = new();
            ISearchResult result = search.SearchCatalog("(Darwin AND (Charles OR Erasmus))");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogSearchWithFacetTest()
        {
            Search search = new();
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
            Search search = new();
            ISearchResult result = search.SearchCatalog("O'Grady");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogSpecialCharsTest()
        {
            Search search = new();
            ISearchResult result = search.SearchCatalog("natural history in nigeria (west-Africa) [2016]");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CatalogDiacriticsTest()
        {
            Search search = new();
            ISearchResult result = search.SearchCatalog("Delb�n");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorSingleWordTest()
        {
            Search search = new();
            ISearchResult result = search.SearchAuthor("darwin");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorMultipleWordTest()
        {
            Search search = new();
            ISearchResult result = search.SearchAuthor("charles darwin");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorPhraseTest()
        {
            Search search = new();
            ISearchResult result = search.SearchAuthor("\"Charles Darwin\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorMultiplePhraseTest()
        {
            Search search = new();
            ISearchResult result = search.SearchAuthor("\"charles darwin\" \"erasmus darwin\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorConditionalAndTest()
        {
            Search search = new();
            ISearchResult result = search.SearchAuthor("Charles AND Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorConditionalOrTest()
        {
            Search search = new();
            ISearchResult result = search.SearchAuthor("Charles OR Erasmus");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorConditionalCompoundTest()
        {
            Search search = new();
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
            Search search = new();
            ISearchResult result = search.SearchAuthor("O'Grady");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorSpecialCharsTest()
        {
            Search search = new();
            ISearchResult result = search.SearchAuthor("smith-rowe [2000]");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AuthorDiacriticsTest()
        {
            Search search = new();
            ISearchResult result = search.SearchAuthor("Delb�n");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordSingleWordTest()
        {
            Search search = new();
            ISearchResult result = search.SearchKeyword("periodicals");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordMultipleWordTest()
        {
            Search search = new();
            ISearchResult result = search.SearchKeyword("united states");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordPhraseTest()
        {
            Search search = new();
            ISearchResult result = search.SearchKeyword("\"united states\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordMultiplePhraseTest()
        {
            Search search = new();
            ISearchResult result = search.SearchKeyword("\"atlantic ocean\" \"marine animals\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordConditionalAndTest()
        {
            Search search = new();
            ISearchResult result = search.SearchKeyword("biology AND ecology");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordConditionalOrTest()
        {
            Search search = new();
            ISearchResult result = search.SearchKeyword("biology OR ecology");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordConditionalCompoundTest()
        {
            Search search = new();
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
            Search search = new();
            ISearchResult result = search.SearchKeyword("O'Grady");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordSpecialCharsTest()
        {
            Search search = new();
            ISearchResult result = search.SearchKeyword("smith-rowe [2000]");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void KeywordDiacriticsTest()
        {
            Search search = new();
            ISearchResult result = search.SearchKeyword("Delb�n");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameSingleWordTest()
        {
            Search search = new();
            ISearchResult result = search.SearchName("mollusca");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameMultipleWordTest()
        {
            Search search = new();
            ISearchResult result = search.SearchName("Elmas hibbsi");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NamePhraseTest()
        {
            Search search = new();
            ISearchResult result = search.SearchName("\"Elmas hibbsi\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameMultiplePhraseTest()
        {
            Search search = new();
            ISearchResult result = search.SearchName("\"Abra brasiliana\" \"E.A. Smith\"");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameConditionalAndTest()
        {
            Search search = new();
            ISearchResult result = search.SearchName("Abra AND Smith");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameConditionalOrTest()
        {
            Search search = new();
            ISearchResult result = search.SearchName("Abra OR Smith");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameConditionalCompoundTest()
        {
            Search search = new();
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
            Search search = new();
            ISearchResult result = search.SearchName("O'Grady");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameSpecialCharsTest()
        {
            Search search = new();
            ISearchResult result = search.SearchName("smith-rowe [2000]");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void NameDiacriticsTest()
        {
            Search search = new();
            ISearchResult result = search.SearchName("Delb�n");
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void PageSingleWordTest()
        {
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
            List<Tuple<SearchField, string>> limits = new()
            {
                new Tuple<SearchField, string>(SearchField.ItemID, "22803")
            };
            ISearchResult result = search.SearchPage("Delb�n", limits);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedTitleAllWordsTest()
        {
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("Delb�n", SearchStringParamOperator.And),
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("Delb�n", SearchStringParamOperator.And),
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("Delb�n", SearchStringParamOperator.And),
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("Delb�n", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedNoteExactPhraseTest()
        {
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
            ISearchResult result = search.SearchCatalog(
                new SearchStringParam("ocean", SearchStringParamOperator.And),
                new SearchStringParam("", SearchStringParamOperator.And),
                "", "",
                new SearchStringParam("", SearchStringParamOperator.And),
                null,
                null,
                new SearchStringParam("", SearchStringParamOperator.And),
                new SearchStringParam("Delb�n", SearchStringParamOperator.And)
                );
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void AdvancedTextExactPhraseTest()
        {
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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
            Search search = new();
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