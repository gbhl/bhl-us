CREATE PROCEDURE [dbo].[ExportSegmentIdentifier]

AS

BEGIN

SET NOCOUNT ON

SELECT 	s.SegmentID, 
		id.IdentifierName, 
		ii.IdentifierValue, 
		CONVERT(nvarchar(16), ii.CreationDate, 120) AS CreationDate,
		scs.HasLocalContent,
		scs.HasExternalContent
FROM	dbo.Segment s WITH (NOLOCK)
		INNER JOIN dbo.Item i WITH (NOLOCK) ON s.ItemID = i.ItemID
		INNER JOIN dbo.ItemIdentifier ii WITH (NOLOCK) ON i.ItemID = ii.ItemID
		INNER JOIN dbo.Identifier id WITH (NOLOCK) ON ii.IdentifierID = id.IdentifierID
		INNER JOIN dbo.SearchCatalogSegment scs WITH (NOLOCK) ON s.SegmentID = scs.SegmentID
ORDER BY
		s.SegmentID, 
		id.IdentifierName

END

GO
