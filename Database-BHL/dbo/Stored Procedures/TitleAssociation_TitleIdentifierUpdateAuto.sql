
-- TitleAssociation_TitleIdentifierUpdateAuto PROCEDURE
-- Generated 2/4/2011 3:01:10 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for TitleAssociation_TitleIdentifier

CREATE PROCEDURE TitleAssociation_TitleIdentifierUpdateAuto

@TitleAssociation_TitleIdentifierID INT,
@TitleAssociationID INT,
@TitleIdentifierID INT,
@IdentifierValue VARCHAR(125),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleAssociation_TitleIdentifier]

SET

	[TitleAssociationID] = @TitleAssociationID,
	[TitleIdentifierID] = @TitleIdentifierID,
	[IdentifierValue] = @IdentifierValue,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[TitleAssociation_TitleIdentifierID] = @TitleAssociation_TitleIdentifierID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociation_TitleIdentifierUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

