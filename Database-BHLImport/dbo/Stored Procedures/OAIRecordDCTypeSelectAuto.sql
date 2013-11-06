CREATE PROCEDURE OAIRecordDCTypeSelectAuto

@OAIRecordDCTypeID INT

AS 

SET NOCOUNT ON

SELECT 

	[OAIRecordDCTypeID],
	[OAIRecordID],
	[DCType],
	[CreationDate],
	[LastModifiedDate]

FROM [dbo].[OAIRecordDCType]

WHERE
	[OAIRecordDCTypeID] = @OAIRecordDCTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordDCTypeSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
