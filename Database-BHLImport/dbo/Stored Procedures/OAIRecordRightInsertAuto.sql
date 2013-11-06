CREATE PROCEDURE OAIRecordRightInsertAuto

@OAIRecordRightID INT OUTPUT,
@OAIRecordID INT,
@Right NVARCHAR(MAX)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[OAIRecordRight]
(
	[OAIRecordID],
	[Right],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@OAIRecordID,
	@Right,
	getdate(),
	getdate()
)

SET @OAIRecordRightID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordRightInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

GO
 
