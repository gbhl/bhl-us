﻿CREATE PROCEDURE OAIRecordCreatorUpdateAuto

@OAIRecordCreatorID INT,
@OAIRecordID INT,
@FullName NVARCHAR(300),
@Dates NVARCHAR(50),
@ProductionAuthorID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[OAIRecordCreator]

SET

	[OAIRecordID] = @OAIRecordID,
	[FullName] = @FullName,
	[Dates] = @Dates,
	[ProductionAuthorID] = @ProductionAuthorID,
	[LastModifiedDate] = getdate()

WHERE
	[OAIRecordCreatorID] = @OAIRecordCreatorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordCreatorUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[OAIRecordCreatorID],
		[OAIRecordID],
		[FullName],
		[Dates],
		[ProductionAuthorID],
		[CreationDate],
		[LastModifiedDate]

	FROM [dbo].[OAIRecordCreator]
	
	WHERE
		[OAIRecordCreatorID] = @OAIRecordCreatorID
	
	RETURN -- update successful
END

