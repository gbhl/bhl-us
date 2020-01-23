CREATE PROCEDURE [dbo].[BSSegmentResolveAuthors]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

-- Compare BioStor IDs of authors in import DB and production.  If matches are found,
-- update import DB with production author ID.
-- Since matching on a unique ID, assume that we'll only match a single production author.
UPDATE	dbo.BSSegmentAuthor
SET		BHLAuthorID = ai.AuthorID
FROM	dbo.BSSegment s
		INNER JOIN dbo.BSSegmentAuthor sa ON s.SegmentID = sa.SegmentID
		-- Only segments contributed by BioStor
		INNER JOIN dbo.ImportSource src ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
		INNER JOIN dbo.BHLAuthorIdentifier ai ON sa.BioStorID = ai.IdentifierValue
		INNER JOIN dbo.BHLIdentifier id ON ai.IdentifierID = id.IdentifierID AND id.IdentifierName = 'BioStor'		
WHERE	s.SegmentID = @SegmentID
AND		sa.BHLAuthorID IS NULL	-- only authors not already resolved

-- Compare VIAF IDs of authors in import DB and production.  If matches are found,
-- update import DB with production author ID.
-- Since matching on a unique ID, assume that we'll only match a single production author.
UPDATE	dbo.BSSegmentAuthor
SET		BHLAuthorID = ai.AuthorID
FROM	dbo.BSSegment s
		INNER JOIN dbo.BSSegmentAuthor sa ON s.SegmentID = sa.SegmentID
		-- Only segments contributed by BioStor
		INNER JOIN dbo.ImportSource src ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
		INNER JOIN dbo.BHLAuthorIdentifier ai ON sa.VIAFIdentifier = ai.IdentifierValue
		INNER JOIN dbo.BHLIdentifier id ON ai.IdentifierID = id.IdentifierID AND id.IdentifierName = 'VIAF'		
WHERE	s.SegmentID = @SegmentID
AND		sa.BHLAuthorID IS NULL	-- only authors not already resolved
AND		sa.VIAFIdentifier <> ''

-- Compare name strings of authors in import DB and production.  If matches are found,
-- update import DB with production author ID.
SELECT	an.AuthorID, sa.SegmentAuthorID
INTO	#tmpNameAuthorID
FROM	dbo.BSSegment s
		INNER JOIN dbo.BSSegmentAuthor sa ON s.SegmentID = sa.SegmentID
		-- Only segments contributed by BioStor
		INNER JOIN dbo.ImportSource src ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
		INNER JOIN dbo.BHLAuthorName an 
			ON (sa.LastName = an.LastName AND sa.FirstName = an.FirstName)
			OR an.FullName = sa.LastName + ', ' + sa.FirstName
			OR an.FullName = sa.LastName + ',' + sa.FirstName
WHERE	s.SegmentID = @SegmentID
AND		sa.BHLAuthorID IS NULL	-- only authors not already resolved

-- Only update import DB if a single matching author is found
IF (@@ROWCOUNT = 1)
BEGIN
	-- Do the update
	UPDATE	dbo.BSSegmentAuthor
	SET		BHLAuthorID = AuthorID
	FROM	dbo.BSSegmentAuthor sa INNER JOIN #tmpNameAuthorID t 
				ON sa.SegmentAuthorID = t.SegmentAuthorID
END

-- If the selected production author ID has been redirected to a different 
-- author, then use that author instead.  Follow the "redirect" chain up 
-- to ten levels.
UPDATE	dbo.BSSegmentAuthor
SET		BHLAuthorID = COALESCE(a10.AuthorID, a9.AuthorID, a8.AuthorID, a7.AuthorID, a6.AuthorID,
							a5.AuthorID, a4.AuthorID, a3.AuthorID, a2.AuthorID, a1.AuthorID)
FROM	dbo.BSSegmentAuthor sa INNER JOIN dbo.BHLAuthor a1 ON sa.BHLAuthorID = a1.AuthorID
		LEFT JOIN dbo.BHLAuthor a2 ON a1.RedirectAuthorID = a2.AuthorID
		LEFT JOIN dbo.BHLAuthor a3 ON a2.RedirectAuthorID = a3.AuthorID
		LEFT JOIN dbo.BHLAuthor a4 ON a3.RedirectAuthorID = a4.AuthorID
		LEFT JOIN dbo.BHLAuthor a5 ON a4.RedirectAuthorID = a5.AuthorID
		LEFT JOIN dbo.BHLAuthor a6 ON a5.RedirectAuthorID = a6.AuthorID
		LEFT JOIN dbo.BHLAuthor a7 ON a6.RedirectAuthorID = a7.AuthorID
		LEFT JOIN dbo.BHLAuthor a8 ON a7.RedirectAuthorID = a8.AuthorID
		LEFT JOIN dbo.BHLAuthor a9 ON a8.RedirectAuthorID = a9.AuthorID
		LEFT JOIN dbo.BHLAuthor a10 ON a9.RedirectAuthorID = a10.AuthorID
WHERE	sa.SegmentID = @SegmentID
AND		BHLAuthorID IS NOT NULL

END
