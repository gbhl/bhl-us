
-- TitleAssociation_TitleIdentifierSelectAuto PROCEDURE
-- Generated 2/4/2011 3:01:10 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for TitleAssociation_TitleIdentifier

CREATE PROCEDURE TitleAssociation_TitleIdentifierSelectAuto

@TitleAssociation_TitleIdentifierID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleAssociation_TitleIdentifierID],
	[TitleAssociationID],
	[TitleIdentifierID],
	[IdentifierValue],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[TitleAssociation_TitleIdentifier]

WHERE
	[TitleAssociation_TitleIdentifierID] = @TitleAssociation_TitleIdentifierID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociation_TitleIdentifierSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

