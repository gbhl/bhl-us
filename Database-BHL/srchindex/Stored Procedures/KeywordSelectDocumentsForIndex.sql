CREATE PROCEDURE [srchindex].[KeywordSelectDocumentsForIndex]

@StartID int,
@EndID int = NULL

AS 

BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		KeywordID, 
		Keyword
FROM	dbo.Keyword
WHERE	KeywordID IN (
    SELECT TOP 200000 KeywordID
    FROM	(
			SELECT  KeywordID
			FROM    dbo.TitleKeyword tk
					INNER JOIN dbo.Title t WITH (NOLOCK) ON tk.TitleID = t.TitleID
			WHERE   t.PublishReady = 1
			UNION
			SELECT  KeywordID
			FROM    dbo.SegmentKeyword sk
					INNER JOIN dbo.Segment s WITH (NOLOCK) ON sk.SegmentID = s.SegmentID
			WHERE   s.SegmentStatusID IN(10, 20)
			) x
	WHERE	KeywordID >= @StartID
	AND		(KeywordID <= @EndID OR @EndID IS NULL)
    ORDER BY KeywordID
    )
ORDER BY KeywordID

END
