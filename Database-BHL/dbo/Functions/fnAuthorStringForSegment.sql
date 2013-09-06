
CREATE FUNCTION [dbo].[fnAuthorStringForSegment] 
(
	@SegmentID int,
	@Delimiter nvarchar(5)
)
RETURNS nvarchar(1024)
AS 

BEGIN
	DECLARE @AuthorString nvarchar(max)

	SELECT @AuthorString = STUFF((			
		SELECT	'|' +	ISNULL(NULLIF(n.FullName + ' ', ' '), '') + 
						ISNULL(NULLIF(a.Numeration + ' ', ' '), '') + 
						ISNULL(NULLIF(a.Unit + ' ', ' '), '') +
						ISNULL(NULLIF(a.Title + ' ', ' '), '') + 
						ISNULL(NULLIF(a.Location + ' ', ' '), '') + 
						ISNULL(NULLIF(n.FullerForm + ' ', ' ' ), '') + 
						a.StartDate + 
						CASE WHEN a.StartDate <> '' THEN '-' ELSE '' END + 
						a.EndDate
		FROM	Segment s
				INNER JOIN SegmentAuthor sa ON s.SegmentID = sa.SegmentID
				INNER JOIN Author a ON sa.AuthorID = a.AuthorID
				INNER JOIN AuthorName n ON a.AuthorID = n.AuthorID
		WHERE	s.SegmentID = @SegmentID
		AND		n.IsPreferredName = 1
		ORDER BY sa.SequenceOrder, n.FullName ASC
		FOR XML PATH('')
		),1,1,'')

	RETURN SUBSTRING(LTRIM(RTRIM(COALESCE(@AuthorString, ''))), 1, 1024)
END

