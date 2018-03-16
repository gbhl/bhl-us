CREATE PROCEDURE [srchindex].[AuthorSelectDocumentsForIndex]

@StartID int

AS 

BEGIN

SET NOCOUNT ON

SELECT DISTINCT
		a.AuthorID,
		dbo.fnAuthorSearchStringForAuthor(a.AuthorID, '|') AS AuthorNames,
		LTRIM(RTRIM(n.FullName + ' ' +
			ISNULL(NULLIF(n.FullerForm + ' ', ' '), '') +
			ISNULL(NULLIF(a.Title + ' ', ' '), '') +
			ISNULL(NULLIF(a.Unit + ' ', ' '), '') +
			ISNULL(NULLIF(a.Location + ' ', ' '), ''))) AS PrimaryAuthorName
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
				SELECT  sa.AuthorID
				FROM    dbo.SegmentAuthor sa
						INNER JOIN dbo.Segment s WITH (NOLOCK) ON sa.SegmentID = s.SegmentID
				WHERE   s.SegmentStatusID IN (10, 20)
				) x
	    WHERE AuthorID > @StartID
		ORDER BY AuthorID
		)
ORDER BY a.AuthorID

END
