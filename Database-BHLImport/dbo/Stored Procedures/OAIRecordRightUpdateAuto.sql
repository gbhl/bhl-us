CREATE PROCEDURE OAIRecordRightUpdateAuto

@OAIRecordRightID INT,
@OAIRecordID INT,
@Right NVARCHAR(MAX)

AS 

SET NOCOUNT ON

UPDATE [dbo].[OAIRecordRight]

SET

	[OAIRecordID] = @OAIRecordID,
	[Right] = @Right,
	[LastModifiedDate] = getdate()

WHERE
	[OAIRecordRightID] = @OAIRecordRightID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordRightUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[OAIRecordRightID],
		[OAIRecordID],
		[Right],
		[CreationDate],
		[LastModifiedDate]

	FROM [dbo].[OAIRecordRight]
	
	WHERE
		[OAIRecordRightID] = @OAIRecordRightID
	
	RETURN -- update successful
END

GO
 
