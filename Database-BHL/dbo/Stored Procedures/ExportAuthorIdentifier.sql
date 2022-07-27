CREATE PROCEDURE [dbo].[ExportAuthorIdentifier]

AS

BEGIN

SET NOCOUNT ON

-- Get authors associated with active publications
SELECT	ta.AuthorID,
		c.HasLocalContent,
		c.HasExternalContent
INTO	#Author
FROM	dbo.TitleAuthor ta
		INNER JOIN dbo.Title t ON ta.TitleID = t.TitleID
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN dbo.Author a ON ta.AuthorID = a.AuthorID
		INNER JOIN dbo.SearchCatalog c ON ta.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	a.IsActive = 1
UNION
SELECT	ia.AuthorID,
		scs.HasLocalContent,
		scs.HasExternalContent
FROM	dbo.ItemAuthor ia 
		INNER JOIN dbo.Item i ON ia.ItemID = i.ItemID
		INNER JOIN dbo.Segment s ON ia.ItemID = s.ItemID
		INNER JOIN dbo.Author a ON ia.AuthorID = a.AuthorID
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
WHERE	i.ItemStatusID in (30, 40)
AND		a.IsActive = 1

SELECT 	a.AuthorID, 
		id.IdentifierName, 
		ai.IdentifierValue, 
		CONVERT(nvarchar(16), MIN(ai.CreationDate), 120) AS CreationDate,
		MAX(a.HasLocalContent) AS HasLocalContent,
		MAX(a.HasExternalContent) AS HasExternalContent
FROM	#Author a
		INNER JOIN dbo.AuthorIdentifier ai ON a.AuthorID = ai.AuthorID
		INNER JOIN dbo.Identifier id ON ai.IdentifierID = id.IdentifierID
GROUP BY
		a.AuthorID, 
		id.IdentifierName, 
		ai.IdentifierValue

END

GO
