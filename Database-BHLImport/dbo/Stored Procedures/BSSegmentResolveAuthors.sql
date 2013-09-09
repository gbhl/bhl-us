CREATE PROCEDURE [dbo].[BSSegmentResolveAuthors]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

-- Compare BioStor IDs of authors in import DB and production.  If matches are found,
-- update import DB with production author ID.
-- Since matching on a unique ID, assume that we'll only match a single production author.
UPDATE	dbo.SegmentAuthor
SET		BHLAuthorID = ai.AuthorID
FROM	dbo.BSSegment s
		INNER JOIN dbo.SegmentAuthor sa ON s.SegmentID = sa.SegmentID
		-- Only segments contributed by BioStor
		INNER JOIN dbo.ImportSource src ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
		INNER JOIN dbo.BHLAuthorIdentifier ai ON sa.BioStorID = ai.IdentifierValue
		INNER JOIN dbo.BHLIdentifier id ON ai.IdentifierID = id.IdentifierID AND id.IdentifierName = 'BioStor'		
WHERE	s.SegmentID = @SegmentID
AND		sa.BHLAuthorID IS NULL	-- only authors not already resolved

-- Compare VIAF IDs of authors in import DB and production.  If matches are found,
-- update import DB with production author ID.
-- Since matching on a unique ID, assume that we'll only match a single production author.
UPDATE	dbo.SegmentAuthor
SET		BHLAuthorID = ai.AuthorID
FROM	dbo.BSSegment s
		INNER JOIN dbo.SegmentAuthor sa ON s.SegmentID = sa.SegmentID
		-- Only segments contributed by BioStor
		INNER JOIN dbo.ImportSource src ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
		INNER JOIN dbo.BHLAuthorIdentifier ai ON sa.VIAFIdentifier = ai.IdentifierValue
		INNER JOIN dbo.BHLIdentifier id ON ai.IdentifierID = id.IdentifierID AND id.IdentifierName = 'VIAF'		
WHERE	s.SegmentID = @SegmentID
AND		sa.BHLAuthorID IS NULL	-- only authors not already resolved

-- Compare name strings of authors in import DB and production.  If matches are found,
-- update import DB with production author ID.
SELECT	an.AuthorID, sa.SegmentAuthorID
INTO	#tmpNameAuthorID
FROM	dbo.BSSegment s
		INNER JOIN dbo.SegmentAuthor sa ON s.SegmentID = sa.SegmentID
		-- Only segments contributed by BioStor
		INNER JOIN dbo.ImportSource src ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
		INNER JOIN dbo.BHLAuthorName an 
			ON (sa.LastName = an.LastName AND sa.FirstName = an.FirstName)
			OR an.FullName LIKE sa.LastName + '%' + sa.FirstName
WHERE	s.SegmentID = @SegmentID
AND		sa.BHLAuthorID IS NULL	-- only authors not already resolved

-- Only update import DB if a single matching author is found
IF (@@ROWCOUNT = 1)
BEGIN
	UPDATE	dbo.SegmentAuthor
	SET		BHLAuthorID = AuthorID
	FROM	dbo.SegmentAuthor sa INNER JOIN #tmpNameAuthorID t 
				ON sa.SegmentAuthorID = t.SegmentAuthorID
END

END


