CREATE FUNCTION [dbo].[fnAuthorFacetStringForSegment] 
(
	@SegmentID int
)
RETURNS nvarchar(MAX)
AS 

BEGIN
	DECLARE @AuthorString nvarchar(max)

	DECLARE @Person int
	SELECT @Person = AuthorTypeID FROM dbo.AuthorType WHERE AuthorTypeName = 'Person'
	
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
				AND		n.IsPreferredName = 1		-- Only preferred names
				AND		a.AuthorTypeID = @Person	-- Only person names used for facets
				AND		a.IsActive = 1
				GROUP BY n.FullName, n.FullerForm, a.Title, a.Unit, a.Location
				) x
		ORDER BY x.SequenceOrder, x.FullName ASC
		FOR XML PATH('')
		),1,1,'')

	RETURN LTRIM(RTRIM(COALESCE(@AuthorString, '')))
END
