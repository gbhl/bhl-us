
CREATE PROCEDURE [dbo].[ApiTitleSelectUnpublished]

AS

SET NOCOUNT ON

-- Get all titles that are unpublished and not redirected to another title
SELECT	TitleID
FROM	dbo.Title
WHERE	PublishReady = 0
AND		RedirectTitleID IS NULL

UNION

-- Get the titles that are unpublished and redirected to other unpublished titles
SELECT	x.OrigTitleID as TitleID
FROM	(
		-- Get the replacement TitleID for the unpublished and redirected titles.
		-- Look at up to 10 levels of redirection.
		SELECT	t1.TitleID AS OrigTitleID,
				COALESCE(t10.TitleID, t9.TitleID, t8.TitleiD, t7.TitleID, t6.TitleID,
						t5.TitleID, t4.TitleID, t3.TitleID, t2.TitleID) AS ReplacementTitleID
		FROM	dbo.Title t1
				LEFT JOIN dbo.Title t2 ON t1.RedirectTitleID = t2.TitleID
				LEFT JOIN dbo.Title t3 ON t2.RedirectTitleID = t3.TitleID
				LEFT JOIN dbo.Title t4 ON t3.RedirectTitleID = t4.TitleID
				LEFT JOIN dbo.Title t5 ON t4.RedirectTitleID = t5.TitleID
				LEFT JOIN dbo.Title t6 ON t5.RedirectTitleID = t6.TitleID
				LEFT JOIN dbo.Title t7 ON t6.RedirectTitleID = t7.TitleID
				LEFT JOIN dbo.Title t8 ON t7.RedirectTitleID = t8.TitleID
				LEFT JOIN dbo.Title t9 ON t8.RedirectTitleID = t9.TitleID
				LEFT JOIN dbo.Title t10 ON t9.RedirectTitleID = t10.TitleID
		WHERE	t1.PublishReady = 0
		AND		t1.RedirectTitleID IS NOT NULL
		) x INNER JOIN dbo.Title t
			ON x.ReplacementTitleID = t.TitleID
WHERE	t.PublishReady = 0
ORDER BY TitleID

