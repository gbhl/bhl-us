CREATE PROCEDURE [dbo].[OpenUrlCitationSelectByDOI]

@DOIName nvarchar(50)

AS

BEGIN

SET NOCOUNT ON

DECLARE @DOIEntityTypeName nvarchar(50)
DECLARE @EntityID int

DECLARE @DOIIdentifierID int
SELECT @DOIIdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

-- Get the entity details for the DOI	
SELECT	@DOIEntityTypeName = 'Segment',
		@EntityID = s.SegmentID
FROM	dbo.ItemIdentifier ii WITH (NOLOCK) 
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON ii.ItemID = s.ItemID
WHERE	ii.IdentifierValue = @DOIName
AND		ii.IdentifierID = @DOIIdentifierID

IF (@DOIEntityTypeName IS NULL)
BEGIN
	SELECT	@DOIEntityTypeName = 'Title',
			@EntityID = ti.TitleID
	FROM	dbo.Title_Identifier ti WITH (NOLOCK) 
	WHERE	ti.IdentifierValue  = @DOIName
	AND		ti.IdentifierID = @DOIIdentifierID
END

-- Call the appropriate OpenUrl Citation resolver for the entity type
IF @DOIEntityTypeName = 'Title' exec dbo.OpenUrlCitationSelectByTitleID @EntityID
IF @DOIEntityTypeName = 'Item' exec dbo.OpenUrlCitationSelectByItemID @EntityID
IF @DOIEntityTypeName = 'Page' exec dbo.OpenUrlCitationSelectByPageID @EntityID
IF @DOIEntityTypeName = 'Segment' exec dbo.OpenUrlCitationSelectBySegmentID @EntityID

END

GO
