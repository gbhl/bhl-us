CREATE PROCEDURE dbo.OAIRecordCreatorIdentifierUpdateAuto

@OAIRecordCreatorIdentifierID INT,
@OAIRecordCreatorID INT,
@IdentifierType NVARCHAR(40),
@IdentifierValue NVARCHAR(125)

AS 

SET NOCOUNT ON

UPDATE [dbo].[OAIRecordCreatorIdentifier]
SET
	[OAIRecordCreatorID] = @OAIRecordCreatorID,
	[IdentifierType] = @IdentifierType,
	[IdentifierValue] = @IdentifierValue,
	[LastModifiedDate] = getdate()
WHERE
	[OAIRecordCreatorIdentifierID] = @OAIRecordCreatorIdentifierID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.OAIRecordCreatorIdentifierUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
GO
