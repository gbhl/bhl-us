CREATE PROCEDURE [dbo].[TitleDocumentSelectByTitleID]

@TitleID int

AS

BEGIN

SET NOCOUNT ON

SELECT	d.TitleDocumentID,
		d.TitleID,
		d.DocumentTypeID,
		t.[Label] AS TypeLabel,
		d.[Name],
		d.[Url],
		d.CreationDate,
		d.LastModifiedDate,
		d.CreationUserID,
		d.LastModifiedUserID
FROM	TitleDocument d
		INNER JOIN DocumentType t ON d.DocumentTypeID = t.DocumentTypeID
WHERE	d.TitleID = @TitleID
ORDER BY
		t.Label, d.Name

END

GO
