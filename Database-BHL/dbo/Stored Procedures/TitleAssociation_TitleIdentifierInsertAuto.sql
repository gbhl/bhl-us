
-- TitleAssociation_TitleIdentifierInsertAuto PROCEDURE
-- Generated 2/4/2011 3:01:10 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleAssociation_TitleIdentifier

CREATE PROCEDURE TitleAssociation_TitleIdentifierInsertAuto

@TitleAssociation_TitleIdentifierID INT OUTPUT,
@TitleAssociationID INT,
@TitleIdentifierID INT,
@IdentifierValue VARCHAR(125),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleAssociation_TitleIdentifier]
(
	[TitleAssociationID],
	[TitleIdentifierID],
	[IdentifierValue],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@TitleAssociationID,
	@TitleIdentifierID,
	@IdentifierValue,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @TitleAssociation_TitleIdentifierID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociation_TitleIdentifierInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

