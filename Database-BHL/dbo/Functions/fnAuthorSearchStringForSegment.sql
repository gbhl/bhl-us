CREATE FUNCTION [dbo].[fnAuthorSearchStringForSegment] 
(
	@SegmentID int
)
RETURNS nvarchar(MAX)
AS 

BEGIN
	DECLARE @AuthorString nvarchar(max)

	SELECT @AuthorString = STUFF((			
		SELECT	'|' +	ISNULL(NULLIF(x.FullName + ' ', ' '), '') + 
						ISNULL(NULLIF(x.FullerForm + ' ', ' ' ), '') +
						ISNULL(NULLIF(x.Title + ' ', ' '), '') + 
						ISNULL(NULLIF(x.Unit + ' ', ' '), '') +
						ISNULL(NULLIF(x.Location + ' ', ' '), '')
		FROM	(
				SELECT	MIN(sa.SequenceOrder) AS SequenceOrder, 
						n.FullName, a.Unit, a.Title, a.Location, n.FullerForm
				FROM	Segment s
						INNER JOIN SegmentAuthor sa ON s.SegmentID = sa.SegmentID
						INNER JOIN Author a ON sa.AuthorID = a.AuthorID
						INNER JOIN AuthorName n ON a.AuthorID = n.AuthorID
				WHERE	s.SegmentID = @SegmentID
				AND		n.IsPreferredName = 1	-- Remove this to include all forms of a name
				AND		a.IsActive = 1
				GROUP BY n.FullName, n.FullerForm, a.Title, a.Unit, a.Location
				) x
		ORDER BY x.SequenceOrder, x.FullName ASC
		FOR XML PATH('')
		),1,1,'')

	RETURN LTRIM(RTRIM(COALESCE(@AuthorString, '')))
END
