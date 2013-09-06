
CREATE PROCEDURE [dbo].[TitleKeywordSelectLikeTag]
@Tag NVARCHAR(50),
@LanguageCode NVARCHAR(10) = '',
@ReturnCount INT = 100

AS 

SET NOCOUNT ON

-- NOTE:  @LanguageCode is no longer used (Feb 12, 2013)

SELECT	KeywordID, Keyword
INTO	#tmpKeyword
FROM	dbo.Keyword
WHERE	Keyword LIKE @Tag + '%'

SELECT	Keyword
INTO	#tmpFinal
FROM	#tmpKeyword k
		INNER JOIN dbo.TitleKeyword tk ON k.KeywordID = tk.KeywordID
		INNER JOIN dbo.Title T ON tk.TitleID = T.TitleID
WHERE	T.PublishReady = 1

UNION

SELECT	Keyword
FROM	#tmpKeyword k
		INNER JOIN dbo.SegmentKeyword sk ON k.KeywordID = sk.KeywordID
		INNER JOIN dbo.Segment s ON sk.SegmentID = s.SegmentID
WHERE	s.SegmentStatusID IN (10, 20)

SELECT TOP (@ReturnCount) Keyword FROM #tmpFinal ORDER BY Keyword

