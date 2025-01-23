﻿CREATE PROCEDURE [dbo].[OpenUrlCitationSelectByDOI]

@DOIName nvarchar(50)

AS

BEGIN

SET NOCOUNT ON

DECLARE @DOIEntityTypeName nvarchar(50)
DECLARE @EntityID int

-- Get the entity details for the DOI
SELECT	@DOIEntityTypeName = 'Title',
		@EntityID = ti.TitleID
FROM	dbo.Title_Identifier ti
WHERE	IdentifierValue = @DOIName

IF (@DOIEntityTypeName IS NULL)
BEGIN
	SELECT	@DOIEntityTypeName = 'Segment',
			@EntityID = s.SegmentID
	FROM	dbo.ItemIdentifier ii
			INNER JOIN dbo.Segment s ON ii.ItemID = s.ItemID
	WHERE	IdentifierValue = @DOIName
END

-- Call the appropriate OpenUrl Citation resolver for the entity type
IF @DOIEntityTypeName = 'Title' exec dbo.OpenUrlCitationSelectByTitleID @EntityID
IF @DOIEntityTypeName = 'Item' exec dbo.OpenUrlCitationSelectByItemID @EntityID
IF @DOIEntityTypeName = 'Page' exec dbo.OpenUrlCitationSelectByPageID @EntityID
IF @DOIEntityTypeName = 'Segment' exec dbo.OpenUrlCitationSelectBySegmentID @EntityID

END

GO
