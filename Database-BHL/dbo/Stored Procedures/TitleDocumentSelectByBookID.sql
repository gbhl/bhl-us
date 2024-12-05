CREATE PROCEDURE [dbo].[TitleDocumentSelectByBookID]

@BookID int

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
FROM	dbo.TitleDocument d
		INNER JOIN dbo.DocumentType dt ON d.DocumentTypeID = dt.DocumentTypeID
		INNER JOIN dbo.ItemTitle it ON d.TitleID = it.TitleID
		INNER JOIN dbo.Title t ON d.TitleID = t.TitleID AND t.PublishReady = 1
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
WHERE	b.BookID = @BookID
ORDER BY
		dt.Label, d.Name

END

GO
