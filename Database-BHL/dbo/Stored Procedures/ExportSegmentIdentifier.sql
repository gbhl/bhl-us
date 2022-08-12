CREATE PROCEDURE [dbo].[ExportSegmentIdentifier]

AS

BEGIN

SET NOCOUNT ON

SELECT 	s.SegmentID, 
		id.IdentifierName, 
		ii.IdentifierValue, 
		CONVERT(nvarchar(16), MIN(ii.CreationDate), 120) AS CreationDate,
		MAX(scs.HasLocalContent) AS HasLocalContent,
		MAX(scs.HasExternalContent) AS HasExternalContent
FROM	dbo.vwSegment s
		INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID
		INNER JOIN dbo.Identifier id ON ii.IdentifierID = id.IdentifierID
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
		LEFT JOIN dbo.Book b ON s.BookID = b.BookID
		LEFT JOIN dbo.Item i ON b.ItemID = i.ItemID
WHERE	id.IdentifierName <> 'DOI'
AND		(i.ItemStatusID = 40 OR i.ItemStatusID IS NULL)
AND     (s.BookID IS NOT NULL OR ISNULL(s.Url, '') <> '')
GROUP BY
		s.SegmentID,
		id.IdentifierName,
		ii.IdentifierValue
ORDER BY
		s.SegmentID, 
		id.IdentifierName

END

GO
