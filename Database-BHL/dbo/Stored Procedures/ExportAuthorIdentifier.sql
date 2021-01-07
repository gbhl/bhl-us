CREATE PROCEDURE [dbo].[ExportAuthorIdentifier]

AS

BEGIN

SET NOCOUNT ON

-- Get authors associated with active publications
SELECT	ta.AuthorID,
		c.HasLocalContent,
		c.HasExternalContent
INTO	#Author
FROM	dbo.TitleAuthor ta WITH (NOLOCK)
		INNER JOIN dbo.Author a WITH (NOLOCK) ON ta.AuthorID = a.AuthorID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON ta.TitleID = c.TitleID
UNION
SELECT	ia.AuthorID,
		scs.HasLocalContent,
		scs.HasExternalContent
FROM	dbo.ItemAuthor ia WITH (NOLOCK) 
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ia.ItemID = s.ItemID
		INNER JOIN dbo.SearchCatalogSegment scs WITH (NOLOCK) ON s.SegmentID = scs.SegmentID

SELECT 	a.AuthorID, 
		id.IdentifierName, 
		ai.IdentifierValue, 
		CONVERT(nvarchar(16), ai.CreationDate, 120) AS CreationDate,
		MAX(a.HasLocalContent) AS HasLocalContent,
		MAX(a.HasExternalContent) AS HasExternalContent
FROM	#Author a WITH (NOLOCK)
		INNER JOIN dbo.AuthorIdentifier ai WITH (NOLOCK) ON a.AuthorID = ai.AuthorID
		INNER JOIN dbo.Identifier id WITH (NOLOCK) ON ai.IdentifierID = id.IdentifierID
GROUP BY
		a.AuthorID, 
		id.IdentifierName, 
		ai.IdentifierValue, 
		CONVERT(nvarchar(16), ai.CreationDate, 120)

END

GO
