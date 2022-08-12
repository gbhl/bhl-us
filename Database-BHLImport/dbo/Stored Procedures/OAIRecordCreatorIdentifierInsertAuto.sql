CREATE PROCEDURE dbo.OAIRecordCreatorIdentifierInsertAuto

@OAIRecordCreatorIdentifierID INT OUTPUT,
@OAIRecordCreatorID INT,
@IdentifierType NVARCHAR(40),
@IdentifierValue NVARCHAR(125)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[OAIRecordCreatorIdentifier]
( 	[OAIRecordCreatorID],
	[IdentifierType],
	[IdentifierValue],
	[CreationDate],
	[LastModifiedDate] )
VALUES
( 	@OAIRecordCreatorID,
	@IdentifierType,
	@IdentifierValue,
	getdate(),
	getdate() )

SET @OAIRecordCreatorIdentifierID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.OAIRecordCreatorIdentifierInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[OAIRecordCreatorIdentifierID],
		[OAIRecordCreatorID],
		[IdentifierType],
		[IdentifierValue],
		[CreationDate],
		[LastModifiedDate]	
	FROM [dbo].[OAIRecordCreatorIdentifier]
	WHERE
		[OAIRecordCreatorIdentifierID] = @OAIRecordCreatorIdentifierID
	
	RETURN -- insert successful
END
GO
