CREATE FUNCTION dbo.fnAuthorInfoForSegment
(
	@SegmentID int,
	@Delimiter nvarchar(5)
)
RETURNS nvarchar(max)
AS 

BEGIN
	DECLARE @AuthorString nvarchar(max)

	SELECT @AuthorString = REVERSE(STUFF(REVERSE(
			(
			SELECT	ISNULL(CONVERT(nvarchar(10), x.AuthorID), '') +
					'|' + ISNULL(NULLIF(x.FullName + ' ', ' '), '') + 
						ISNULL(NULLIF(x.Numeration + ' ', ' '), '') + 
						ISNULL(NULLIF(x.Unit + ' ', ' '), '') +
						ISNULL(NULLIF(x.Title + ' ', ' '), '') + 
						ISNULL(NULLIF(x.Location + ' ', ' '), '') + 
						ISNULL(NULLIF(x.FullerForm + ' ', ' ' ), '')
					+ ' ' + @Delimiter + ' '
			FROM	(
					SELECT	MIN(ia.SequenceOrder) AS SequenceOrder, 
							a.AuthorID, n.FullName, a.Numeration, a.Unit, a.Title, a.Location, n.FullerForm
					FROM	dbo.Segment s
							INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
							INNER JOIN dbo.ItemAuthor ia ON i.ItemID = ia.ItemID
							INNER JOIN dbo.Author a ON ia.AuthorID = a.AuthorID
							INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
					WHERE	s.SegmentID = @SegmentID
					AND		n.IsPreferredName = 1
					GROUP BY a.AuthorID, n.FullName, a.Numeration, a.Unit, a.Title, a.Location, n.FullerForm
					) x
			ORDER BY x.SequenceOrder, x.FullName ASC
			FOR XML PATH('')
			)
		), 1, 4, ''))

	RETURN LTRIM(RTRIM(COALESCE(@AuthorString, '')))
END

GO
