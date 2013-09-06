CREATE PROCEDURE [dbo].[TitleAssociationSelectByTitleID]

@TitleID INT,
@Active BIT = NULL

AS
BEGIN

SET NOCOUNT ON

SELECT	ta.TitleAssociationID,
		ta.TitleID,
		ta.TitleAssociationTypeID,
		tat.TitleAssociationLabel, 
		tat.TitleAssociationName,
		ta.Title,
		ta.Section,
		ta.Volume,
		ta.Heading,
		ta.Publication,
		ta.Relationship,
		ta.AssociatedTitleID,
		ta.Active,
		ta.CreationDate,
		ta.LastModifiedDate
FROM	dbo.TitleAssociation ta INNER JOIN dbo.TitleAssociationType tat
			ON ta.TitleAssociationTypeID = tat.TitleAssociationTypeID
WHERE	ta.TitleID = @TitleID
AND		(ta.Active = @Active OR @Active IS NULL)
ORDER BY
		tat.TitleAssociationLabel, tat.MarcIndicator2, ta.Title, ta.Section, ta.Volume

END
