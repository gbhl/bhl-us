SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
			FROM    dbo.ItemKeyword ik
					INNER JOIN dbo.Item i ON ik.ItemID = i.ItemID
					INNER JOIN dbo.Segment s WITH (NOLOCK) ON i.ItemID = s.ItemID
			WHERE   i.ItemStatusID IN (30, 40)
			) x
	WHERE	KeywordID >= @StartID
	AND		(KeywordID <= @EndID OR @EndID IS NULL)
    ORDER BY KeywordID
    )
ORDER BY KeywordID

END


GO
