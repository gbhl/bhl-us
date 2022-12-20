CREATE PROCEDURE [dbo].[ApiTitleAssociationSelectByTitleID]

@TitleID int

AS

BEGIN

SET NOCOUNT ON

SELECT	tat.TitleAssociationName AS TitleAssociationTypeName,
		RTRIM(ta.Title + ISNULL(' ' + ta.Section, '') +
		ISNULL(' ' + ta.Volume, '') +
		ISNULL(' ' + ta.Heading, '') +
		ISNULL(' ' + ta.Publication, '') +
		ISNULL(' ' + ta.Relationship, '')) AS Title,
		ta.AssociatedTitleID
FROM	dbo.TitleAssociation ta 
		INNER JOIN dbo.TitleAssociationType tat ON ta.TitleAssociationTypeID = tat.TitleAssociationTypeID
		INNER JOIN dbo.Title t ON ta.TitleID = t.TitleID
WHERE	ta.TitleID = @TitleID
AND		ta.Active = 1
AND		t.PublishReady = 1
ORDER BY
		tat.TitleAssociationName,
		ta.Title, 
		ta.Section, 
		ta.Volume

END

GO
