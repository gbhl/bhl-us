CREATE PROCEDURE [dbo].[ExportDOI]

AS

BEGIN

SET NOCOUNT ON

DECLARE @TitleTypeID int
DECLARE @SegmentTypeID int
SELECT @TitleTypeID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Title'
SELECT @SegmentTypeID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'

SELECT CASE 
		WHEN et.DOIEntityTypeName = 'Segment' THEN 'Part' ELSE et.DOIEntityTypeName END AS EntityType, 
		d.EntityID, 
		d.DOIName AS DOI, 
		COALESCE(t.HasLocalContent, s.HasLocalContent) AS HasLocalContent,
		COALESCE(t.HasExternalContent, s.HasExternalContent) AS HasExternalContent,
		d.CreationDate
INTO	#DOI
FROM	dbo.DOI d WITH (NOLOCK)
		INNER JOIN dbo.DOIEntityType et WITH (NOLOCK) ON d.DOIEntityTypeID = et.DOIEntityTypeID 
		LEFT JOIN dbo.SearchCatalog t WITH (NOLOCK) ON d.EntityID = t.TitleID AND d.DOIEntityTypeID = @TitleTypeID
		LEFT JOIN dbo.SearchCatalogSegment s WITH (NOLOCK) ON d.EntityID = s.SegmentID AND d.DOIEntityTypeID = @SegmentTypeID
WHERE	d.IsValid = 1

SELECT	EntityType,
		EntityID,
		DOI,
		MAX(HasLocalContent) AS HasLocalContent,
		MAX(HasExternalContent) AS HasExternalContent,
		CreationDate
FROM	#DOI
WHERE	HasLocalContent IS NOT NULL
AND		HasExternalContent IS NOT NULL
GROUP BY
		EntityType,
		EntityID,
		DOI,
		CreationDate	

END
