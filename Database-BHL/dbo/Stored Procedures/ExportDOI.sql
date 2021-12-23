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
		ti.CreationDate
FROM	dbo.Title_Identifier ti WITH (NOLOCK)
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON ti.TitleID = c.TitleID
WHERE	ti.IdentifierID = @DOIIdentifierID
GROUP BY
		ti.TitleID,
		ti.IdentifierValue,
		ti.CreationDate
UNION	
SELECT	'Part',
		s.SegmentID,
		ii.IdentifierValue AS DOI,
		MAX(c.HasLocalContent) AS HasLocalContent,
		MAX(c.HasExternalContent) AS HasExternalContent,
		ii.CreationDate
FROM	dbo.ItemIdentifier ii WITH (NOLOCK)
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ii.ItemID = s.ItemID
		INNER JOIN dbo.SearchCatalogSegment c  WITH (NOLOCK) ON s.SegmentID = c.SegmentID
WHERE	ii.IdentifierID = @DOIIdentifierID
GROUP BY
		s.SegmentID,
		ii.IdentifierValue,
		ii.CreationDate

END

GO
