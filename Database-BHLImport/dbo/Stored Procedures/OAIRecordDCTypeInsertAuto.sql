CREATE PROCEDURE OAIRecordDCTypeInsertAuto

@OAIRecordDCTypeID INT OUTPUT,
@OAIRecordID INT,
@DCType NVARCHAR(300)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[OAIRecordDCType]
(
	[OAIRecordID],
	[DCType],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@OAIRecordID,
	@DCType,
	getdate(),
	getdate()
)

SET @OAIRecordDCTypeID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordDCTypeInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[OAIRecordDCTypeID],
		[OAIRecordID],
		[DCType],
		[CreationDate],
		[LastModifiedDate]	

	FROM [dbo].[OAIRecordDCType]
	
	WHERE
		[OAIRecordDCTypeID] = @OAIRecordDCTypeID
	
	RETURN -- insert successful
END

GO
 
