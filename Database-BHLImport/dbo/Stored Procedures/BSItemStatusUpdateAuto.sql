
-- BSItemStatusUpdateAuto PROCEDURE
-- Generated 10/23/2012 3:24:24 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for BSItemStatus

CREATE PROCEDURE BSItemStatusUpdateAuto

@ItemStatusID INT,
@Status NVARCHAR(30),
@Description NVARCHAR(4000)

AS 

SET NOCOUNT ON

UPDATE [dbo].[BSItemStatus]

SET

	[ItemStatusID] = @ItemStatusID,
	[Status] = @Status,
	[Description] = @Description,
	[LastModifiedDate] = getdate()

WHERE
	[ItemStatusID] = @ItemStatusID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BSItemStatusUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ItemStatusID],
		[Status],
		[Description],
		[CreationDate],
		[LastModifiedDate]

	FROM [dbo].[BSItemStatus]
	
	WHERE
		[ItemStatusID] = @ItemStatusID
	
	RETURN -- update successful
END

