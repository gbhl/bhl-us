CREATE PROCEDURE [dbo].[ExportDOI]

AS

BEGIN

SET NOCOUNT ON

DECLARE @DOIIdentifierID int
SELECT @DOIIdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

SELECT	'Title' AS EntityType,
		ti.TitleID AS EntityID,
		ti.IdentifierValue AS DOI,
		MAX(c.HasLocalContent) AS HasLocalContent,
		MAX(c.HasExternalContent) AS HasExternalContent,
		MIN(ti.CreationDate) AS CreationDate
FROM	dbo.Title_Identifier ti
		INNER JOIN dbo.Title t ON ti.TitleID = t.TitleID
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c ON ti.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	ti.IdentifierID = @DOIIdentifierID
GROUP BY
		ti.TitleID,
		ti.IdentifierValue
UNION	
SELECT	'Part',
		s.SegmentID,
		ii.IdentifierValue AS DOI,
		MAX(c.HasLocalContent) AS HasLocalContent,
		MAX(c.HasExternalContent) AS HasExternalContent,
		MIN(ii.CreationDate) AS CreationDate
FROM	dbo.ItemIdentifier ii
		INNER JOIN dbo.vwSegment s ON ii.ItemID = s.ItemID
		INNER JOIN dbo.SearchCatalogSegment c  ON s.SegmentID = c.SegmentID
		LEFT JOIN dbo.Book b ON s.BookID = b.BookID
		LEFT JOIN dbo.Item i ON b.ItemID = i.ItemID
WHERE	ii.IdentifierID = @DOIIdentifierID
AND		(i.ItemStatusID = 40 OR i.ItemStatusID IS NULL)
AND     (s.BookID IS NOT NULL OR ISNULL(s.Url, '') <> '')
GROUP BY
		s.SegmentID,
		ii.IdentifierValue

END

GO
