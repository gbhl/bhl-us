﻿CREATE PROCEDURE [dbo].[OpenUrlCitationSelectByDOI]

@DOIName nvarchar(50)

AS

BEGIN

SET NOCOUNT ON

DECLARE @DOIEntityTypeName nvarchar(50)
DECLARE @EntityID int

-- Get the entity details for the DOI	
SELECT	@DOIEntityTypeName = et.DOIEntityTypeName,
		@EntityID = d.EntityID
FROM	dbo.DOI d INNER JOIN dbo.DOIEntityType et
			ON d.DOIEntityTypeID = et.DOIEntityTypeID
WHERE	d.DOIName = @DOIName
AND		d.IsValid = 1

-- Call the appropriate OpenUrl Citation resolver for the entity type
IF @DOIEntityTypeName = 'Title' exec dbo.OpenUrlCitationSelectByTitleID @EntityID
IF @DOIEntityTypeName = 'Item' exec dbo.OpenUrlCitationSelectByItemID @EntityID
IF @DOIEntityTypeName = 'Page' exec dbo.OpenUrlCitationSelectByPageID @EntityID
IF @DOIEntityTypeName = 'Segment' exec dbo.OpenUrlCitationSelectBySegmentID @EntityID

END

GO
