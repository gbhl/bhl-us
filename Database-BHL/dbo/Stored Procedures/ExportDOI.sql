CREATE PROCEDURE dbo.ExportDOI

AS

BEGIN

SET NOCOUNT ON

SELECT CASE 
		WHEN et.DOIEntityTypeName = 'Segment' THEN 'Part' ELSE et.DOIEntityTypeName END AS EntityType, 
		d.EntityID, 
		d.DOIName AS DOI, 
		d.CreationDate 
FROM	dbo.DOI d WITH (NOLOCK) 
		INNER JOIN dbo.DOIEntityType et WITH (NOLOCK) ON d.DOIEntityTypeID = et.DOIEntityTypeID 
WHERE	d.IsValid = 1

END

