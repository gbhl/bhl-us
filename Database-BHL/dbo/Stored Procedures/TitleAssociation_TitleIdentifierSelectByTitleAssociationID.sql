
CREATE PROCEDURE [dbo].[TitleAssociation_TitleIdentifierSelectByTitleAssociationID]

@TitleAssociationID INT

AS 

SET NOCOUNT ON

SELECT 

	tati.[TitleAssociation_TitleIdentifierID],
	tati.[TitleAssociationID],
	tati.[TitleIdentifierID],
	i.[IdentifierName],
	tati.[IdentifierValue],
	tati.[CreationDate],
	tati.[LastModifiedDate]

FROM [dbo].[TitleAssociation_TitleIdentifier] tati INNER JOIN [dbo].[Identifier] i
		ON tati.TitleIdentifierID = i.IdentifierID

WHERE
	tati.[TitleAssociationID] = @TitleAssociationID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociation_TitleIdentifierSelectByTitleAssociationID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

