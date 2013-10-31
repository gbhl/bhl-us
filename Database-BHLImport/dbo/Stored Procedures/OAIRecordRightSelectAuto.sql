CREATE PROCEDURE OAIRecordRightSelectAuto

@OAIRecordRightID INT

AS 

SET NOCOUNT ON

SELECT 

	[OAIRecordRightID],
	[OAIRecordID],
	[Right],
	[CreationDate],
	[LastModifiedDate]

FROM [dbo].[OAIRecordRight]

WHERE
	[OAIRecordRightID] = @OAIRecordRightID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordRightSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
 
