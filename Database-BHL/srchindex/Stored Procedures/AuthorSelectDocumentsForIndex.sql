SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [srchindex].[AuthorSelectDocumentsForIndex]

@StartID int,
@EndID int = NULL

AS 

BEGIN

SET NOCOUNT ON

DECLARE @Start int
DECLARE @End int
SET @Start = @StartID
SET @End = @EndID

SELECT DISTINCT
		a.AuthorID,
		dbo.fnAuthorSearchStringForAuthor(a.AuthorID, '|') AS AuthorNames,
		LTRIM(RTRIM(n.FullName + ' ' +
			ISNULL(NULLIF(n.FullerForm + ' ', ' '), '') +
			ISNULL(NULLIF(a.Title + ' ', ' '), '') +
			ISNULL(NULLIF(a.Unit + ' ', ' '), '') +
			ISNULL(NULLIF(a.Location + ' ', ' '), ''))) + 
			CASE 
				WHEN a.StartDate <> '' AND a.EndDate = '' THEN ' ' + a.StartDate + '-'
				WHEN a.StartDate = '' AND a.EndDate <> '' THEN ' -' + a.EndDate
				WHEN a.StartDate <> '' AND a.EndDate <> '' THEN ' ' + a.StartDate + '-' + a.EndDate
				ELSE ''
			END
			AS PrimaryAuthorName
FROM	dbo.Author a WITH (NOLOCK)
        INNER JOIN dbo.AuthorName n WITH (NOLOCK) ON a.AuthorID = n.AuthorID
WHERE	n.IsPreferredName = 1
AND		a.AuthorID IN (
		SELECT TOP 200000 AuthorID
		FROM	(
				SELECT  ta.AuthorID
				FROM    dbo.TitleAuthor ta
						INNER JOIN dbo.Title t WITH (NOLOCK) on ta.TitleID = t.TitleID
				WHERE   t.PublishReady = 1
				UNION
				SELECT  ia.AuthorID
				FROM    dbo.ItemAuthor ia
						INNER JOIN dbo.Item i ON ia.ItemID = i.ItemID
						INNER JOIN dbo.Segment s WITH (NOLOCK) ON i.ItemID = s.ItemID
				WHERE   i.ItemStatusID IN (30, 40)
				) x
	    WHERE	AuthorID >= @Start
		AND		(AuthorID <= @End OR @End IS NULL)
		ORDER BY AuthorID
		)
ORDER BY a.AuthorID

END


GO
