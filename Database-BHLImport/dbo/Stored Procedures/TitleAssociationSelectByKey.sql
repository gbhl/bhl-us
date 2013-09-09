
CREATE PROCEDURE [dbo].[TitleAssociationSelectByKey]

@ImportKey nvarchar(50),
@MARCTag nvarchar(20),
@MARCIndicator2 nchar(1),
@Title nvarchar(500),
@Section nvarchar(500),
@Volume nvarchar(500),
@Heading nvarchar(500),
@Publication nvarchar(500),
@Relationship nvarchar(500)

AS
BEGIN

SET NOCOUNT ON

SELECT	TitleAssociationID,
		ImportKey,
		ImportStatusID,
		ImportSourceID,
		MARCTag,
		MARCIndicator2,
		Title,
		Section,
		Volume,
		Heading,
		Publication,
		Relationship,
		Active,
		ProductionDate,
		CreatedDate,
		LastModifiedDate
FROM	dbo.TitleAssociation
WHERE	ImportKey = @ImportKey
AND		MARCTag = @MARCTag
AND		MARCIndicator2 = @MARCIndicator2
AND		Title = @Title
AND		Section = @Section
AND		Volume = @Volume
AND		Heading = @Heading
AND		Publication = @Publication
AND		Relationship = @Relationship

END
