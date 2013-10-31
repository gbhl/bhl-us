CREATE PROCEDURE OAIRecordDCTypeUpdateAuto

@OAIRecordDCTypeID INT,
@OAIRecordID INT,
@DCType NVARCHAR(300)

AS 

SET NOCOUNT ON

UPDATE [dbo].[OAIRecordDCType]

SET

	[OAIRecordID] = @OAIRecordID,
	[DCType] = @DCType,
	[LastModifiedDate] = getdate()

WHERE
	[OAIRecordDCTypeID] = @OAIRecordDCTypeID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordDCTypeUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

GO
 
