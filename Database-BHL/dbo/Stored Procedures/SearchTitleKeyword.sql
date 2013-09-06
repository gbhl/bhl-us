
CREATE PROCEDURE [dbo].[SearchTitleKeyword]

@Keyword NVARCHAR(300),
@LanguageCode NVARCHAR(10) = '',
@ReturnCount INT = 100

AS 

BEGIN

SET NOCOUNT ON

-- NOTE:  @LanguageCode is no longer used (Feb 12, 2013)

-- Revert to 'normal' SQL search of the search catalog is offline
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus
IF (@CatalogStatus = 0)
BEGIN
	exec dbo.TitleKeywordSelectLikeTag @Keyword, @LanguageCode, @ReturnCount
	RETURN
END


DECLARE @SearchCondition nvarchar(4000)

-- Transform the search term into a full-text search phrase
SELECT @SearchCondition = dbo.fnGetFullTextSearchString(@Keyword)

-- Perform the keyword search
SELECT	cat.KeywordID
INTO	#tmpKeyword
FROM	CONTAINSTABLE(SearchCatalogKeyword, Keyword, @SearchCondition) x
		INNER JOIN SearchCatalogKeyword cat ON cat.SearchCatalogKeywordID = x.[KEY]

-- Limit to only those keywords that are related to active titles/segments
SELECT	k.Keyword
INTO	#tmpFinal
FROM	#tmpKeyword tmp
		INNER JOIN dbo.Keyword k ON tmp.KeywordID = k.KeywordID
		INNER JOIN dbo.TitleKeyword tk ON k.KeywordID = tk.KeywordID
		INNER JOIN dbo.Title t ON tk.TitleID = t.TitleID
WHERE	t.PublishReady = 1

UNION

SELECT	k.Keyword
FROM	#tmpKeyword tmp
		INNER JOIN dbo.Keyword k ON tmp.KeywordID = k.KeywordID
		INNER JOIN dbo.SegmentKeyword sk ON k.KeywordID = sk.KeywordID
		INNER JOIN dbo.Segment s ON sk.SegmentID = s.SegmentID
WHERE	s.SegmentStatusID IN (10, 20)

SELECT TOP (@ReturnCount) Keyword FROM #tmpFinal ORDER BY Keyword

END






