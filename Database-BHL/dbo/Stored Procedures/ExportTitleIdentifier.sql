CREATE PROCEDURE [dbo].[ExportTitleIdentifier]

AS

BEGIN

SET NOCOUNT ON

SELECT 	tti.TitleID, 
		ti.IdentifierName, 
		tti.IdentifierValue, 
		MAX(c.HasLocalContent) AS HasLocalContent,
		MAX(c.HasExternalContent) AS HasExternalContent,
		CONVERT(nvarchar(16), MIN(tti.CreationDate), 120) AS CreationDate
FROM	dbo.Title t
		INNER JOIN dbo.Title_Identifier tti ON t.TitleID = tti.TitleID
		INNER JOIN dbo.Identifier ti ON tti.IdentifierID = ti.IdentifierID
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	ti.IdentifierName <> 'DOI'
GROUP BY
		tti.TitleID, 
		ti.IdentifierName, 
		tti.IdentifierValue

END

GO
