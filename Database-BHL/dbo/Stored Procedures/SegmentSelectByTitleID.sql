CREATE PROCEDURE [dbo].[SegmentSelectByTitleID]

@TitleID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @ContributorRoleID INT
SELECT @ContributorRoleID = InstitutionRoleID FROM dbo.InstitutionRole WHERE InstitutionRoleName = 'Contributor'

------------------------------------------------------------------------------------------------------
-- Accumulate a list of segments for the specified title, with the Start, End, and Additional Page IDs

SELECT	s.SegmentID, ip.PageID, ip.SequenceOrder, bip.SequenceOrder AS BookSequenceOrder
INTO	#Pages
FROM	vwSegment s
		INNER JOIN dbo.ItemPage ip ON s.ItemID = ip.ItemID
		INNER JOIN dbo.Book b ON s.BookID = b.BookID
		INNER JOIN dbo.Item i ON b.ItemID = i.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
		-- LEFT JOIN here to include virtual items
		LEFT JOIN dbo.ItemPage bip ON b.ItemID = bip.ItemID AND ip.PageID = bip.PageID		
WHERE	i.ItemStatusID = 40
AND		s.SegmentStatusID IN (30, 40)
AND		t.TitleID = @TitleID
ORDER BY s.SegmentID, ip.SequenceOrder

/*
-- Start pages of segments
SELECT	p.SegmentID, MIN(p.PageID) AS StartPageID
INTO	#Start
FROM	#Pages p
		INNER JOIN (SELECT SegmentID, MIN(SequenceOrder) AS Start FROM #Pages GROUP BY SegmentID) x
			ON p.SegmentID = x.SegmentID AND p.SequenceOrder = x.Start
GROUP BY p.SegmentID
*/

-- End pages of segments (last page of first contiguous set of pages).  Use LEAD() windowing function to detect breaks in SequenceOrder values.
SELECT	p.SegmentID, PageID AS EndPageID
INTO	#End
FROM	#Pages p
		INNER JOIN (
			SELECT	SegmentID, MIN(SequenceOrder) AS SequenceOrder
			FROM	(	SELECT	*,
								LEAD(SegmentID) OVER (ORDER BY SegmentID, BookSequenceOrder) AS next_segment,
								LEAD(BookSequenceOrder) OVER (ORDER BY SegmentID, BookSequenceOrder) AS next_bookseq
						FROM	#Pages
					) x
			WHERE	next_bookseq <> BookSequenceOrder + 1
			OR		next_segment <> SegmentID
			OR		(next_bookseq IS NULL AND next_segment IS NULL)
			GROUP BY SegmentID
			) e
			ON p.SegmentID = e.SegmentID AND p.SequenceOrder = e.SequenceOrder

-- Additional pages
SELECT	p.*
INTO	#Addl
FROM	#Pages p
		INNER JOIN (
			SELECT	p.SegmentID, p.SequenceOrder
			FROM	#Pages p
					INNER JOIN #End e ON p.SegmentID = e.SegmentID AND p.PageID = e.EndPageID
			) x ON p.SegmentID = x.SegmentID AND p.SequenceOrder > x.SequenceOrder
ORDER BY p.SegmentID, p.SequenceOrder

-- Pivot the additional pages into a single row per segment.  Use the FOR XML PATH sql server command to achieve the data pivot.
SELECT DISTINCT 
		a2.SegmentID, 
		SUBSTRING(
			(
				SELECT ';'+ CONVERT(nvarchar(20), a1.PageID)
				FROM #Addl a1
				WHERE a1.SegmentID = a2.SegmentID
				ORDER BY a1.SegmentID, a1.SequenceOrder
				FOR XML PATH ('')
			), 2, 1000) AdditionalPages
INTO	#AddlBySeg
FROM	#Addl a2

/*
-- Get final list of segments with start, end, and additional Page IDs
SELECT	s.SegmentID, s.StartPageID, e.EndPageID, a.AdditionalPages
INTO	#SegmentPages
FROM	#Start s
		LEFT JOIN #End e ON s.SegmentID = e.SegmentID
		LEFT JOIN #AddlBySeg a ON s.SegmentID = a.SegmentID
*/

-- Get final list of segments with end and additional Page IDs
SELECT	e.SegmentID, e.EndPageID, a.AdditionalPages
INTO	#SegmentPages
FROM	#End e
		LEFT JOIN #AddlBySeg a ON e.SegmentID = a.SegmentID

-- Clean up the temp tables that are no longer needed
DROP TABLE #Addl
DROP TABLE #AddlBySeg
--DROP TABLE #Start
DROP TABLE #End
DROP TABLE #Pages

------------------------------------------------------------------------------------------------------

-- Produce the final result set
SELECT	s.SegmentID,
		s.Title, 
		s.TranslatedTitle, 
		b.BookID, 
		s.Volume, 
		s.Issue, 
		s.Series, 
		s.Date, 
		l.LanguageName,
		dbo.fnAuthorIDStringForSegment(s.SegmentID) AS AuthorIDs,
		dbo.fnCOinSAuthorStringForSegment(s.SegmentID) AS Authors,
		dbo.fnInstitutionCodesForItem(s.ItemID, @ContributorRoleID) AS ContributorCodes,
		s.StartPageNumber,
		s.EndPageNumber,
		s.StartPageID,
		sp.EndPageID,
		/*
		(	SELECT	PageID 
			FROM	dbo.ItemPage ip
					INNER JOIN (	SELECT	ItemID, MAX(SequenceOrder) AS MaxSeq 
									FROM	dbo.ItemPage 
									WHERE	ItemID = s.ItemID 
									GROUP BY ItemID	) x 
					ON ip.ItemID = x.ItemID AND ip.SequenceOrder = x.MaxSeq
		) AS EndPageID,
		*/
		sp.AdditionalPages,
		d.DOIName
FROM	dbo.Title t
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
		INNER JOIN dbo.Item bi ON it.ItemID = bi.ItemID
		INNER JOIN dbo.Book b ON bi.ItemID = b.ItemID
		INNER JOIN dbo.ItemRelationship ir ON bi.ItemID = ir.ParentID
		INNER JOIN dbo.Item si ON ir.ChildID = si.ItemID
		INNER JOIN dbo.Segment s ON si.ItemID = s.ItemID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		LEFT JOIN dbo.DOI d ON s.SegmentID = d.EntityID AND d.DOIEntityTypeID = 40
		LEFT JOIN #SegmentPages sp ON s.SegmentID = sp.SegmentID
WHERE	bi.ItemStatusID = 40
AND		si.ItemStatusID IN (30, 40)
AND		t.TitleID = @TitleID
ORDER BY
		it.ItemSequence, ir.SequenceOrder
END

GO
