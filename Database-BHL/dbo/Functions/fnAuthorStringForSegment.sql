SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
		SELECT	'|' +	ISNULL(NULLIF(x.FullName + ' ', ' '), '') + 
						ISNULL(NULLIF(x.Numeration + ' ', ' '), '') + 
						ISNULL(NULLIF(x.Unit + ' ', ' '), '') +
						ISNULL(NULLIF(x.Title + ' ', ' '), '') + 
						ISNULL(NULLIF(x.Location + ' ', ' '), '') + 
						ISNULL(NULLIF(x.FullerForm + ' ', ' ' ), '') + 
						x.StartDate + 
						CASE WHEN x.StartDate <> '' THEN '-' ELSE '' END + 
						x.EndDate
		FROM	(
				SELECT	MIN(ia.SequenceOrder) AS SequenceOrder, 
						n.FullName, a.Numeration, a.Unit, a.Title, a.Location, n.FullerForm, a.StartDate, a.EndDate
				FROM	dbo.Segment s
						INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
						INNER JOIN dbo.ItemAuthor ia ON i.ItemID = ia.ItemID
						INNER JOIN dbo.Author a ON ia.AuthorID = a.AuthorID
						INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
				WHERE	s.SegmentID = @SegmentID
				AND		n.IsPreferredName = 1
				GROUP BY n.FullName, a.Numeration, a.Unit, a.Title, a.Location, n.FullerForm, a.StartDate, a.EndDate
				) x
		ORDER BY x.SequenceOrder, x.FullName ASC
		FOR XML PATH('')
		),1,1,'')

	RETURN SUBSTRING(LTRIM(RTRIM(COALESCE(@AuthorString, ''))), 1, 1024)
END


GO
