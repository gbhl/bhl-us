SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
		INNER JOIN dbo.ItemKeyword ik ON k.KeywordID = ik.KeywordID
		INNER JOIN dbo.vwSegment s ON ik.ItemID = s.ItemID
WHERE	s.SegmentStatusID IN (30, 40)

SELECT TOP (@ReturnCount) Keyword FROM #tmpFinal ORDER BY Keyword


GO
