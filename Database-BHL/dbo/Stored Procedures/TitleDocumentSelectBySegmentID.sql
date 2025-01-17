CREATE PROCEDURE [dbo].[TitleDocumentSelectBySegmentID]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

SELECT	d.TitleDocumentID,
		d.TitleID,
		d.DocumentTypeID,
		dt.[Label] AS TypeLabel,
		d.[Name],
		d.[Url],
		d.CreationDate,
		d.LastModifiedDate,
		d.CreationUserID,
		d.LastModifiedUserID
FROM	dbo.vwSegment s 
		INNER JOIN dbo.Book b ON b.BookID = s.BookID
		INNER JOIN dbo.Item i ON b.ItemID = i.ItemID AND i.ItemStatusID = 40
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID AND t.PublishReady = 1
		INNER JOIN dbo.TitleDocument d ON t.TitleID = d.TitleID
		INNER JOIN dbo.DocumentType dt ON d.DocumentTypeID = dt.DocumentTypeID
WHERE	s.SegmentID = @SegmentID
ORDER BY
		dt.Label, d.Name

END

GO
